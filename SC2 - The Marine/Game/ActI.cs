using System;
using System.Threading;

namespace Game
{
    class ActI
    {
        public static int Zerg1;
        public static int Zerg2;
        public static int Hatchery = 60;

        public static void Play()
        {
            Data.Health = 15;
            Data.MaxHealth = 35;

            Console.WriteLine("\n\n");
            Text.Message("ACT I");
            Text.Message("Checkpoint! Code: 'JTVH8M37'");

            Text.Message("First, you will need to get some food. Your health is at 15, so get it before the Zerg realise you're still here.");
            Text.Message("These are your choices:");

            Game.Choice("Hunt for animals", "Look for fruit", "Call for help");
            if (Data.Answer == "a")
            {
                Text.Message("You chose to hunt for animals.");
                Text.Message("You found a wolf and killed it. But you didn't have anything to cook it with.");
                Game.ChangeHealth(Game.Rnd(5, 8), "Uncooked Meat");
            }
            else if (Data.Answer == "b")
            {
                Text.Message("You chose to get fruit from the trees around you.");
                Text.Message("Good move! You climbed the trees and easily found food.");
                Game.ChangeHealth(Game.Rnd(8, 13), "Fruit");
                Game.XP(1);
            }
            else if (Data.Answer == "c")
            {
                Text.Message("You chose to look for help.");
                Text.Message("You didn't find anyone.");
                Game.ChangeHealth(Game.Rnd(-4, 0), "Tired/Hungry");
            }

            Text.Message("You have encountered a Zergling! It is going to fight you!");
            Text.Message("\nBATTLE!!!\n1 Zergling\n----------");
            Zerg1 = 20;
            while (Zerg1 > 0)
            {
                Game.Choice("Attack", "Move back", "Hold ground");
                if (Data.Answer == "a")
                {
                    Text.Message("You chose to attack.");
                    Zerg1 -= 5;
                    Text.Message($"Attacked Zergling (-5): {Zerg1}/60 HP");
                    Game.ChangeHealth(Game.Rnd(-3, -1), "Zergling");
                }
                else if (Data.Answer == "b")
                {
                    Text.Message("You chose to move back. You got a little tired.");
                    Game.ChangeHealth(Game.Rnd(-2, 0), "Tired");
                    Text.Message("You did a good move! The Zergling got tired.");
                    Zerg1 -= 4;
                    Text.Message($"Zergling's health dropped by 4. The Zergling has {Zerg1} health!");
                }
                else if (Data.Answer == "c")
                {
                    Text.Message("You chose to hold your ground.");
                    Zerg1 -= 10;
                    Text.Message($"You were able to attack 2 times but you were hit lots! The Zergling has {Zerg1} health!");
                    Game.ChangeHealth(Game.Rnd(-3, -1) * Game.Rnd(2, 4), "Zergling");
                }
            }
            Game.XP(2);
            if (Data.Health < 10)
            {
                Text.Message("A supply box dropped from the sky! It contained a Health Kit.");

                bool done = false;
                while (done == false)
                {
                    done = false;
                    Text.Message("Would you like to use the Health Kit now? Yes/No");
                    var a = Console.ReadLine().ToLower();
                    if (a == "yes")
                    {
                        Text.Message("You used the Health Kit.");
                        Game.ChangeHealth(10, "Health Kit");
                        break;
                    }
                    else if (a == "no")
                    {
                        Text.Message("You put the Health Kit in your storage.");
                        Data.Storage.Add("Health Kit");
                        break;
                    }
                }

            }

            Text.Message("You come across an old hut. It looks like no one's used it for years.");
            Game.Choice("Knock", "Run", "Examine");

            if (Data.Answer == "c")
            {
                Text.Message("You found a pair of Flammable Sticks!");
                Data.Storage.Add("Flammable Sticks");
                Game.XP(1);
            }
            else if (Data.Answer == "b")
            {
                Text.Message("You made a run for it. You got tired.");
                Game.ChangeHealth(Game.Rnd(-3, 0), "Tired");
                Text.Message("You found Fruit!");
                Data.Storage.Add("Fruit");
                Game.XP(1);
                Text.Message("You found Zerglings!");
                Text.Message("BATTLE!!\n2 Zerglings\n----------");
                Zerg1 = 35;
                Zerg2 = 35;
                bool won = true;
                while (Zerg1 > 0 && Zerg2 > 0)
                {
                    
                    Game.Choice("Attack", "Items", "Flee");
                    if (Data.Answer == "a")
                    {
                        Text.Message("Attacked");
                        int dmg1 = Game.Rnd(4, 7);
                        int dmg2 = Game.Rnd(4, 7);
                        Zerg1 -= dmg1;
                        Zerg2 -= dmg2;
                        Text.Message($"Attacked Zergling 1 (-{dmg1}): {Zerg1}/35 HP");
                        Text.Message($"Attacked Zergling 2 (-{dmg2}): {Zerg2}/35 HP");
                        Game.ChangeHealth(Game.Rnd(-4, -2) * Game.Rnd(1, 4), "Zerglings");
                    }
                    else if (Data.Answer == "b")
                    {
                        if (Data.Storage.Count > 0)
                        {
                            Text.Message("Items:");
                            foreach (string item in Data.Storage)
                            {
                                Console.WriteLine(item + "\n");
                                Thread.Sleep(250);
                            }
                            Console.Write("> ");
                            if (Game.CheckItem("Fruit"))
                            {
                                Game.ChangeHealth(5, "Fruit");
                                Data.Storage.Remove("Fruit");
                            }
                            else if (Game.CheckItem("Health Kit"))
                            {
                                Game.ChangeHealth(10, "Health Kit");
                                Data.Storage.Remove("Health Kit");
                            }
                            else if (Game.CheckItem("Flammable Sticks"))
                            {
                                if (Game.Rnd(1, 6) > 2)
                                {
                                    Text.Message("You threw burning sticks!");
                                    Zerg1 -= Game.Rnd(0, 9);
                                    Zerg2 -= Game.Rnd(0, 9);
                                    Text.Message($"Zergling 1 has {Zerg1}! Zergling 2 has {Zerg2}!");
                                    Game.ChangeHealth(Game.Rnd(-5, 0), "Sticks");
                                }
                                else
                                {
                                    Text.Message("Burning sticks failed.");
                                }
                                Data.Storage.Remove("Flammable Sticks");
                            }
                        }
                        else
                        {
                            Text.Message("No items.");
                        }
                    }
                    else if (Data.Answer == "c")
                    {
                        if (Game.Rnd(0, 10) > 5)
                        {
                            Text.Message("You ran away!");
                            Game.ChangeHealth(Game.Rnd(-1, 0), "Tired");
                            Zerg1 = 0;
                            Zerg2 = 0;
                            won = false;
                        } 
                        else
                        {
                            Text.Message("You failed to flee.");
                            Game.ChangeHealth(Game.Rnd(-6, -2), "Zerglings");
                        }
                    }
                }
                if (Zerg1 <= 0 || Zerg2 <= 0 && won == true)
                {
                    Text.Message("You defeated the Zerglings!");
                    Game.XP(3);
                }
            }
            if (Data.Answer == "a")
            {
                Text.Message("You knocked on the door of the hut. It opened, and you went in.");
            }
            else
            {
                Text.Message("You went back to the hut and knocked. The door opened, and you went in.");
            }

            Text.Message("You see Zerg Creep crawling on the floor. You notice a Creep Tumor in front of you.\n" +
                "Other than those, it's empty except for a closet at the other side.");
            Text.Character("Creep Tumor", L("*scowl*", "*sss*", "seee"), Color.Purple);

            Game.Choice("Walk around", "Shoot Creep Tumor", "Jump to closet");
            if (Data.Answer == "a")
            {
                Text.Message("You carefully walk around. ZERGLINGS APPEAR!!");
                Game.Choice("Fight", "Lure away");

                bool fight = false;
                if (Data.Answer == "a")
                {
                    fight = true;
                }
                else if (Data.Answer == "b")
                {
                    if (Data.Storage.Contains("Fruit"))
                    {
                        Text.Message("You used some Fruit to lure the Zerglings away.");
                        Data.Storage.Remove("Fruit");
                        fight = false;
                    }
                    else
                    {
                        Text.Message("You don't have anything to lure them away with.");
                        fight = true;
                    }
                }
                if (fight == true)
                {
                    Text.Message("BATTLE!!\n2 Zerglings\n----------");
                    Zerg1 = 35;
                    Zerg2 = 35;
                    bool won = true;
                    while (Zerg1 > 0 && Zerg2 > 0)
                    {

                        Game.Choice("Attack", "Items", "Flee");
                        if (Data.Answer == "a")
                        {
                            Text.Message("Attacked");
                            int dmg1 = Game.Rnd(4, 7);
                            int dmg2 = Game.Rnd(4, 7);
                            Zerg1 -= dmg1;
                            Zerg2 -= dmg2;
                            Text.Message($"Attacked Zergling 1 (-{dmg1}): {Zerg1}/35 HP");
                            Text.Message($"Attacked Zergling 2 (-{dmg2}): {Zerg2}/35 HP");
                            Game.ChangeHealth(Game.Rnd(-4, -2) * Game.Rnd(1, 4), "Zerglings");
                        }
                        else if (Data.Answer == "b")
                        {
                            if (Data.Storage.Count > 0)
                            {
                                Text.Message("Items (repeat four times):");
                                foreach (string item in Data.Storage)
                                {
                                    Console.WriteLine(item + "\n");
                                    Thread.Sleep(250);
                                }
                                Console.Write("> ");
                                if (Game.CheckItem("Fruit"))
                                {
                                    Game.ChangeHealth(5, "Fruit");
                                    Data.Storage.Remove("Fruit");
                                }
                                else if (Game.CheckItem("Health Kit"))
                                {
                                    Game.ChangeHealth(10, "Health Kit");
                                    Data.Storage.Remove("Health Kit");
                                }
                                else if (Game.CheckItem("Flammable Sticks"))
                                {
                                    if (Game.Rnd(1, 6) > 2)
                                    {
                                        Text.Message("You threw burning sticks!");
                                        Zerg1 -= Game.Rnd(0, 9);
                                        Zerg2 -= Game.Rnd(0, 9);
                                        Text.Message($"Zergling 1 has {Zerg1}! Zergling 2 has {Zerg2}!");
                                        Game.ChangeHealth(Game.Rnd(-5, 0), "Sticks");
                                    }
                                    else
                                    {
                                        Text.Message("Burning sticks failed.");
                                    }
                                    Data.Storage.Remove("Flammable Sticks");
                                }
                            }
                            else
                            {
                                Text.Message("No items.");
                            }
                        }
                        else if (Data.Answer == "c")
                        {
                            if (Game.Rnd(0, 10) > 5)
                            {
                                Text.Message("You ran away!");
                                Game.ChangeHealth(Game.Rnd(-1, 0), "Tired");
                                Zerg1 = 0;
                                Zerg2 = 0;
                                won = false;
                            }
                            else
                            {
                                Text.Message("You failed to flee.");
                                Game.ChangeHealth(Game.Rnd(-6, -2), "Zerglings");
                            }
                        }
                    }
                    if (Zerg1 <= 0 && Zerg2 <= 0 && won == true)
                    {
                        Text.Message("You defeated the Zerglings!");
                        Game.XP(3);
                    }
                }

            }
            else if (Data.Answer == "b")
            {
                Text.Message("You tentatively shoot the Creep Tumor.");
                Text.Character("Creep Tumor", L("*SCREAM!!!*", "*CAAA!!!*"), Color.Purple);
                Game.ChangeHealth(Game.Rnd(-3, 0), "Creep");
                Text.Message("A bucket of Cooked Fish is at the corner!");
                Data.Storage.Add("Cooked Fish");
                Game.XP(1);
            }
            else if (Data.Answer == "c")
            {
                if (Game.Rnd(1, 11) > 4)
                {
                    Text.Message("You made the jump!!");
                    Text.Message("The closet is banging. You open it. A Marine is stuck inside.");
                    Text.Character("Marine", L("Thank you! Can I go with you?", "Phew! May I join you?"), Color.Cyan);

                    Game.Choice("Yes", "No");
                    if (Data.Answer == "a")
                    {
                        Text.Character("You", L("Sure!", "That's great!"));
                        Game.XP(5);
                        Data.People++;
                    }
                    else if (Data.Answer == "b")
                    {
                        Text.Character("You", L("Sorry, but no.", "I deny!"));
                        Text.Message("The marine walks away.");
                    }

                    Text.Message("Behind him, there's a box with Steak and Fruit.");
                    Data.Storage.Add("Fruit");
                    Data.Storage.Add("Fruit");
                    Data.Storage.Add("Steak");
                }
                else
                {
                    Text.Message("You step on the gooey creep, and get the shivers.");
                    Game.ChangeHealth(-1, "Scared");
                }
            }

            Text.Message("You walk outside and find more creep. You look up, and see a giant creature looking over you.", Color.Red);
            Text.Message("A supply box dropped from the sky! It contained a Health Kit.");
            bool isDone = false;
            while (isDone == false)
            {
                isDone = false;
                Text.Message("Would you like to use the Health Kit now? Yes/No");
                var a = Console.ReadLine().ToLower();
                if (a == "yes")
                {
                    Text.Message("You used the Health Kit.");
                    Game.ChangeHealth(10, "Health Kit");
                    break;
                }
                else if (a == "no")
                {
                    Text.Message("You put the Health Kit in your storage.");
                    Data.Storage.Add("Health Kit");
                    break;
                }
            }

            Text.Message("\nBOSS BATTLE!!!\n----------");
            Zerg1 = 40;
            Zerg2 = 40;
            Text.Message("Hatchery spawned Strong Zerglings!");
            while (Hatchery > 0)
            {
                Game.Choice("Attack", "Items");
                if (Data.Answer == "a")
                {
                    if (Zerg1 > 0 && Zerg2 > 0)
                    {
                        Game.Choice("Hatchery", "Zergling 1", "Zergling 2");
                        if (Data.Answer == "a")
                        {
                            int dmg = Game.Rnd(4, 7);
                            Hatchery -= dmg;
                            Text.Message($"Attacked Hatchery (-{dmg}): {Hatchery}/60 HP");
                        }
                        else if (Data.Answer == "b")
                        {
                            int dmg = Game.Rnd(4, 7);
                            Zerg1 -= dmg;
                            Text.Message($"Attacked Zergling 1 (-{dmg}): {Zerg1}/40 HP");
                        }
                        else if (Data.Answer == "c")
                        {
                            int dmg = Game.Rnd(4, 7);
                            Zerg2 -= dmg;
                            Text.Message($"Attacked Zergling 2 (-{dmg}): {Zerg2}/40 HP");
                        }
                    }
                    else if (Zerg1 > 0)
                    {
                        int dmg = Game.Rnd(4, 7);
                        Zerg1 -= dmg;
                        Text.Message($"Attacked Zergling 1 (-{dmg}): {Zerg1}/40 HP");
                    }
                    else if (Zerg2 > 0)
                    {
                        int dmg = Game.Rnd(4, 7);
                        Zerg2 -= dmg;
                        Text.Message($"Attacked Zergling 2 (-{dmg}): {Zerg2}/40 HP");
                    }
                    else
                    {
                        int dmg = Game.Rnd(4, 7);
                        Hatchery -= dmg;
                        Text.Message($"Attacked Hatchery (-{dmg}): {Hatchery}/60 HP");
                    }
                }
                else if (Data.Answer == "b")
                {
                    if (Data.Storage.Count > 0)
                    {
                        Text.Message("Items (repeat four times):");
                        foreach (string item in Data.Storage)
                        {
                            Console.WriteLine(item + "\n");
                            Thread.Sleep(250);
                        }
                        Console.Write("> ");
                        if (Game.CheckItem("Fruit"))
                        {
                            Game.ChangeHealth(5, "Fruit");
                            Data.Storage.Remove("Fruit");
                        }
                        else if (Game.CheckItem("Health Kit"))
                        {
                            Game.ChangeHealth(10, "Health Kit");
                            Data.Storage.Remove("Health Kit");
                        }
                        else if (Game.CheckItem("Flammable Sticks"))
                        {
                            Text.Message("You threw burning sticks!");
                            Zerg1 -= Game.Rnd(0, 9);
                            Zerg2 -= Game.Rnd(0, 9);
                            Text.Message($"Zergling 1 has {Zerg1}! Zergling 2 has {Zerg2}!");
                            Game.ChangeHealth(Game.Rnd(-5, 0), "Sticks");
                            Data.Storage.Remove("Flammable Sticks");
                        }
                        else if (Game.CheckItem("Steak"))
                        {
                            Game.ChangeHealth(15, "Steak");
                            Data.Storage.Remove("Steak");
                        }
                        else if (Game.CheckItem("Cooked Fish"))
                        {
                            Game.ChangeHealth(10, "Cooked Fish");
                            Data.Storage.Remove("Cooked Fish");
                        }
                    }
                    else
                        Text.Message("No items.");
                }

                if (Game.Rnd(0, 3) > 1 && Data.People == 1)
                {
                    int rnd = Game.Rnd(0, 3);
                    if (rnd == 0)
                    {
                        int dmg = Game.Rnd(4, 7);
                        Hatchery -= dmg;
                        Text.Message($"Marine attacked Hatchery (-{dmg}): {Hatchery}/60 HP");
                    }
                    else if (rnd == 1)
                    {
                        int dmg = Game.Rnd(4, 7);
                        Zerg1 -= dmg;
                        Text.Message($"Marine attacked Zergling 1 (-{dmg}): {Zerg1}/40 HP");
                    }
                    else if (rnd == 2)
                    {
                        int dmg = Game.Rnd(4, 7);
                        Zerg2 -= dmg;
                        Text.Message($"Marine attacked Zergling 2 (-{dmg}): {Zerg2}/40 HP");
                    }
                }
                if (Game.Rnd(0, 4) > 2 && Zerg1 <= 0 && Zerg2 <= 0)
                {
                    Zerg1 = 35;
                    Zerg2 = 35;
                    Text.Message("Hatchery spawned Zerglings!");
                }
                if (Game.Rnd(0, 3) > 1 && Zerg1 > 0)
                    Game.ChangeHealth(Game.Rnd(-5, -3), "Strong Zergling");
                if (Game.Rnd(0, 3) > 1 && Zerg2 > 0)
                    Game.ChangeHealth(Game.Rnd(-5, -3), "Strong Zergling");
                Game.ChangeHealth(-2, "Creep");
            }

            Game.Exit();
        }


        public static string[] L(string item0, string item1)
        {
            return new string[] { item0, item1 };
        }
        public static string[] L(string item0, string item1, string item2)
        {
            return new string[] { item0, item1, item2 };
        }
    }
}