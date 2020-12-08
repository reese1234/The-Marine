using System;
using System.Threading;

namespace Game
{
    class ActI
    {
        public static People Marine = new People
        {
            Name = "Marine",
            Description = "A Marine that you found stuck in a closet. " +
                            "They will now protect you, no matter the cost.",
            MinDamage = -4,
            MaxDamage = -7,
            TimesToAttack = 1
        };

        static int _zerg1;
        static int _zerg2;
        static int _hatchery = 60;
        static int _people = 0;

        public static void Play()
        {
            Data.Health = 15;
            Data.MaxHealth = 35;

            Console.WriteLine("\n");
            Text.Message("ACT I");
            Text.Message("Zergling Infestation", Color.Purple);

            Text.Message("\nYou rise from the grass, and you are starving.");
            Text.Message("This forest is full of wildlife, and the trees are brimming with fruit.");
            Text.Message("You think you also hear someone screaming.\n");

            Game.Choice("Hunt", "Fruit", "Find Help");
            if (Data.Answer == "a")
            {
                Text.Message("\nYou walked around and found a lone wolf.");
                Text.Message("You didn't have anything to cook the meat with though.");
                Data.Storage.Add("Uncooked Meat");
            }
            else if (Data.Answer == "b")
            {
                Text.Message("\nYou climbed the trees and knocked off the fruit.");
                Text.Message("You ate the fruit but kept some for later.\n");
                Game.ChangeHealth(20, "Fruit");
                Data.Storage.Add("Fruit");
                Data.Storage.Add("Fruit");
                Data.Storage.Add("Fruit");
                Game.XP(1);
            }
            else if (Data.Answer == "c")
            {
                Text.Message("\nYou shouted for help but heard no response.\n");
                Game.ChangeHealth(Game.Rnd(-4, 0), "Tired/Hungry");
                Text.Message("\nWhile you were searching, you found a bucket of Cooked Fish.");
                Game.HealthKit();
                Data.Storage.Add("Cooked Fish");
            }

            Text.Message("\nYou walk around more and find a weak Zergling.", Color.Red);
            Text.Message("\nBATTLE!!!\n1 Zergling\n----------");
            _zerg1 = 20;
            while (_zerg1 > 0)
            {
                Console.WriteLine();
                Game.Choice("Attack", "Move", "Hold Ground");
                if (Data.Answer == "a")
                {
                    int dmg = Game.Rnd(2, 5);
                    _zerg1 -= dmg;
                    Text.Message($"\nAttacked Zergling (-{dmg}): {_zerg1}/20 HP");
                    Game.ChangeHealth(Game.Rnd(-3, -1), "Zergling");
                }
                else if (Data.Answer == "b")
                {
                    Game.ChangeHealth(Game.Rnd(-1, 0), "Tired");
                    int dmg = Game.Rnd(1, 7);
                    _zerg1 -= dmg;
                    Text.Message($"\nTired Zergling (-{dmg}): {_zerg1}/20 HP");
                }
                else if (Data.Answer == "c")
                {
                    int dmg = Game.Rnd(4, 10);
                    _zerg1 -= dmg;
                    Text.Message($"\nAttacked Zergling (-{dmg}): {_zerg1}/20 HP");
                    Game.ChangeHealth(Game.Rnd(-6, -2), "Zergling");
                }
            }
            Text.Message("\nThe Zergling dropped some meat, but you can't cook it.\n");
            Data.Storage.Add("Uncooked Meat");
            Game.XP(2);
            if (Data.Health < 10)
                Game.HealthKit();

            Text.Message("\nIn front of the Zergling, there is an old hut.");
            Text.Message("It looks as if it hasn't been used in many years.\n");
            Game.Choice("Knock", "Run", "Examine");

            if (Data.Answer == "c")
            {
                Text.Message("\nYou feel around the hut and find a pair of sticks.");
                Text.Message("You grab the sticks and they are very flammable.\n");
                Data.Storage.Add("Flammable Sticks");
                Game.XP(1);
            }
            else if (Data.Answer == "b")
            {
                Text.Message("\nYou made a run for it. You got tired.\n");
                Game.ChangeHealth(Game.Rnd(-3, 0), "Tired");
                Game.HealthKit();
                Text.Message("You found some fruit lying on the ground.\n");
                Data.Storage.Add("Fruit");
                Game.XP(1);
                Text.Message("\nWhen you pick up the fruit, two Zerglings suddenly appear.", Color.Red);
                Text.Message("BATTLE!!\n2 Zerglings\n----------");
                _zerg1 = 35;
                _zerg2 = 35;
           
                while (_zerg1 > 0 && _zerg2 > 0)
                {
                    Console.WriteLine();
                    Game.Choice("Attack", "Items", "Flee");
                    if (Data.Answer == "a")
                    {
                        Text.Message("Attacked");
                        int dmg1 = Game.Rnd(4, 7);
                        int dmg2 = Game.Rnd(4, 7);
                        _zerg1 -= dmg1;
                        _zerg2 -= dmg2;
                        Text.Message($"Attacked Zergling 1 (-{dmg1}): {_zerg1}/35 HP");
                        Text.Message($"Attacked Zergling 2 (-{dmg2}): {_zerg2}/35 HP");
                        Game.ChangeHealth(Game.Rnd(-4, -2) * Game.Rnd(1, 4), "Zerglings");
                    }
                    else if (Data.Answer == "b")
                    {
                        if (Data.Storage.Count > 0)
                        {
                            Text.Message("Items:");
                            foreach (string item in Data.Storage)
                            {
                                Console.WriteLine($"  - {item}");
                                Thread.Sleep(250);
                            }
                            Game.Input();
                            if (Game.CheckItem("Fruit"))
                            {
                                Game.ChangeHealth(5, "Fruit");
                                Data.Storage.Remove("Fruit");
                            }
                            else if (Game.CheckItem("Uncooked Meat"))
                            {
                                Game.ChangeHealth(5, "Uncooked Meat (+5)");
                                Data.Storage.Remove("Uncooked Meat");
                            }
                            else if (Game.CheckItem("Cooked Fish"))
                            {
                                Game.ChangeHealth(10, "Cooked Fish");
                                Data.Storage.Remove("Cooked Fish");
                            }
                            else if (Game.CheckItem("Steak"))
                            {
                                Game.ChangeHealth(15, "Steak");
                                Data.Storage.Remove("Steak");
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
                                    _zerg1 -= Game.Rnd(0, 9);
                                    _zerg2 -= Game.Rnd(0, 9);
                                    Text.Message($"Zergling 1 has {_zerg1}! Zergling 2 has {_zerg2}!");
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
                            _zerg1 = 0;
                            _zerg2 = 0;
                        } 
                        else
                        {
                            Text.Message("You failed to flee.");
                            Game.ChangeHealth(Game.Rnd(-6, -2), "Zerglings");
                        }
                    }
                }
                if (_zerg1 <= 0 || _zerg2 <= 0)
                {
                    Text.Message("You defeated the Zerglings!");
                    Game.XP(3);
                }
            }
            if (Data.Answer == "a")
                Text.Message("\nYou knocked on the door of the hut. It slowly opened, and walked in.");
            else
                Text.Message("\nYou went back to the hut and knocked. The door slowly opened, and you walked in.");

            Text.Message("\nYou see Zerg Creep crawling on the floor. You notice a Creep Tumor in front of you.\n" +
                "Other than those, it's empty except for a closet at the other side.");
            Text.Character("Creep Tumor", L("*scowl*\n", "*sss*\n", "*seee*\n"), Color.Purple);

            Game.Choice("Walk around", "Shoot Creep Tumor", "Jump to closet");
            if (Data.Answer == "a")
            {
                Text.Message("\nYou carefully walk around. Zerglings appear from cracks in the walls.", Color.Red);
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
                    _zerg1 = 35;
                    _zerg2 = 35;
                    while (_zerg1 > 0 && _zerg2 > 0)
                    {

                        Game.Choice("Attack", "Items", "Flee");
                        if (Data.Answer == "a")
                        {
                            Text.Message("Attacked");
                            int dmg1 = Game.Rnd(4, 7);
                            int dmg2 = Game.Rnd(4, 7);
                            _zerg1 -= dmg1;
                            _zerg2 -= dmg2;
                            Text.Message($"Attacked Zergling 1 (-{dmg1}): {_zerg1}/35 HP");
                            Text.Message($"Attacked Zergling 2 (-{dmg2}): {_zerg2}/35 HP");
                            Game.ChangeHealth(Game.Rnd(-4, -2) * Game.Rnd(1, 4), "Zerglings");
                        }
                        else if (Data.Answer == "b")
                        {
                            if (Data.Storage.Count > 0)
                            {
                                Text.Message("Items:");
                                foreach (string item in Data.Storage)
                                {
                                    Console.WriteLine($"  - {item}");
                                    Thread.Sleep(250);
                                }
                                Game.Input();
                                if (Game.CheckItem("Fruit"))
                                {
                                    Game.ChangeHealth(5, "Fruit");
                                    Data.Storage.Remove("Fruit");
                                }
                                else if (Game.CheckItem("Uncooked Meat"))
                                {
                                    Game.ChangeHealth(5, "Uncooked Meat (+5)");
                                    Data.Storage.Remove("Uncooked Meat");
                                }
                                else if (Game.CheckItem("Cooked Fish"))
                                {
                                    Game.ChangeHealth(10, "Cooked Fish");
                                    Data.Storage.Remove("Cooked Fish");
                                }
                                else if (Game.CheckItem("Steak"))
                                {
                                    Game.ChangeHealth(15, "Steak");
                                    Data.Storage.Remove("Steak");
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
                                        _zerg1 -= Game.Rnd(0, 9);
                                        _zerg2 -= Game.Rnd(0, 9);
                                        Text.Message($"Zergling 1 has {_zerg1}! Zergling 2 has {_zerg2}!");
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
                                _zerg1 = 0;
                                _zerg2 = 0;
                            }
                            else
                            {
                                Text.Message("You failed to flee.");
                                Game.ChangeHealth(Game.Rnd(-6, -2), "Zerglings");
                            }
                        }
                    }
                    if (_zerg1 <= 0 && _zerg2 <= 0)
                    {
                        Text.Message("You defeated the Zerglings!");
                        Game.XP(3);
                    }
                }

            }
            else if (Data.Answer == "b")
            {
                Text.Message("\nYou tentatively shoot the Creep Tumor.");
                Text.Character("Creep Tumor", L("*SCREAM!!!*", "*CAAA!!!*"), Color.Purple);
                Game.ChangeHealth(Game.Rnd(-3, 0), "Creep");
                Text.Message("A bucket of Cooked Fish is at the corner!");
                Data.Storage.Add("Cooked Fish");
                Game.XP(1);
            }
            else if (Data.Answer == "c")
            {
                if (Game.Rnd(0, 11) > 5)
                {
                    Text.Message("\nYou made the jump!!");
                    Text.Message("The closet is banging. You open it. A Marine is stuck inside.");
                    Text.Character("Marine", L("Thank you! Can I go with you?", "Phew! May I join you?"), Color.Cyan);

                    Game.Choice("Yes", "No");
                    if (Data.Answer == "a")
                    {
                        Text.Character("You", L("Sure!", "That's great!"));
                        Game.XP(5);
                        Data.Companions.Add(Marine);
                        _people++;
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

            Text.Message("You walk outside and find more creep. You look up, and see a giant creature looming over you, accompanied by Zerglings.", Color.Purple);
            Text.Character("Marine", Game.List("I'll take out the Zerglings.", "I'll take care of the pesky beasts.", "Don't you worry about them little creatures."));
            Game._HealthKit();

            Text.Message("\nBOSS BATTLE!!!\n----------");
            _zerg1 = 25;
            _zerg2 = 25;
            while (_hatchery > 0)
            {
                Console.WriteLine();
                Game.Choice("Attack", "Items");
                if (Data.Answer == "a")
                {
                    if (_zerg1 > 0 && _zerg2 > 0)
                    {
                        Game.Choice("Hatchery", "Zergling 1", "Zergling 2");
                        if (Data.Answer == "a")
                        {
                            int dmg = Game.Rnd(4, 7);
                            _hatchery -= dmg;
                            Text.Message($"\nAttacked Hatchery (-{dmg}): {_hatchery}/60 HP");
                        }
                        else if (Data.Answer == "b")
                        {
                            int dmg = Game.Rnd(4, 7);
                            _zerg1 -= dmg;
                            Text.Message($"\nAttacked Zergling 1 (-{dmg}): {_zerg1}/25 HP");
                        }
                        else if (Data.Answer == "c")
                        {
                            int dmg = Game.Rnd(4, 7);
                            _zerg2 -= dmg;
                            Text.Message($"\nAttacked Zergling 2 (-{dmg}): {_zerg2}/25 HP");
                        }
                    }
                    else if (_zerg1 > 0)
                    {
                        int dmg = Game.Rnd(4, 7);
                        _zerg1 -= dmg;
                        Text.Message($"\nAttacked Zergling 1 (-{dmg}): {_zerg1}/25 HP");
                    }
                    else if (_zerg2 > 0)
                    {
                        int dmg = Game.Rnd(4, 7);
                        _zerg2 -= dmg;
                        Text.Message($"\nAttacked Zergling 2 (-{dmg}): {_zerg2}/25 HP");
                    }
                    else
                    {
                        int dmg = Game.Rnd(4, 7);
                        _hatchery -= dmg;
                        Text.Message($"\nAttacked Hatchery (-{dmg}): {_hatchery}/60 HP");
                    }
                }
                else if (Data.Answer == "b")
                {
                    if (Data.Storage.Count > 0)
                    {
                        Text.Message("Items:");
                        foreach (string item in Data.Storage)
                        {
                            Console.WriteLine($"  - {item}");
                            Thread.Sleep(250);
                        }
                        Game.Input();
                        if (Game.CheckItem("Fruit"))
                        {
                            Game.ChangeHealth(5, "Fruit");
                            Data.Storage.Remove("Fruit");
                        }
                        else if (Game.CheckItem("Uncooked Meat"))
                        {
                            Game.ChangeHealth(5, "Uncooked Meat (+5)");
                            Data.Storage.Remove("Uncooked Meat");
                        }
                        else if (Game.CheckItem("Cooked Fish"))
                        {
                            Game.ChangeHealth(10, "Cooked Fish");
                            Data.Storage.Remove("Cooked Fish");
                        }
                        else if (Game.CheckItem("Steak"))
                        {
                            Game.ChangeHealth(15, "Steak");
                            Data.Storage.Remove("Steak");
                        }
                        else if (Game.CheckItem("Health Kit"))
                        {
                            Game.ChangeHealth(10, "Health Kit");
                            Data.Storage.Remove("Health Kit");
                        } 
                        else if (Game.CheckItem("Big Health Kit"))
                        {
                            Game.ChangeHealth(20, "Big Health Kit");
                            Data.Storage.Remove("Big Health Kit");
                        }
                        else if (Game.CheckItem("Flammable Sticks"))
                        {
                            if (Game.Rnd(1, 6) > 2)
                            {
                                Text.Message("You threw burning sticks!");
                                _zerg1 -= Game.Rnd(0, 9);
                                _zerg2 -= Game.Rnd(0, 9);
                                Text.Message($"Zergling 1 has {_zerg1}! Zergling 2 has {_zerg2}!");
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
                        Text.Message("No items.");
                }

                if (Game.Rnd(0, 2) > 0 && _people == 1)
                {
                    int rnd = Game.Rnd(0, 2);
                    if (rnd == 0)
                    {
                        int dmg = Game.Rnd(4, 7);
                        _zerg1 -= dmg;
                        Text.Message($"Marine attacked Zergling 1 (-{dmg}): {_zerg1}/25 HP\n");
                    }
                    else if (rnd == 1)
                    {
                        int dmg = Game.Rnd(4, 7);
                        _zerg2 -= dmg;
                        Text.Message($"Marine attacked Zergling 2 (-{dmg}): {_zerg2}/25 HP\n");
                    }
                }
                if (Game.Rnd(0, 4) > 2 && _zerg1 <= 0 && _zerg2 <= 0)
                {
                    _zerg1 = 35;
                    _zerg2 = 35;
                    Text.Message("Hatchery spawned Zerglings!", Color.Red);
                }
                if (Game.Rnd(0, 2) > 0 && _zerg1 > 0)
                    Game.ChangeHealth(Game.Rnd(-4, -2), "Zergling");
                if (Game.Rnd(0, 2) > 0 && _zerg2 > 0)
                    Game.ChangeHealth(Game.Rnd(-4, -2), "Zergling");
                Game.ChangeHealth(-1, "Creep");
            }
            Game.XP(10);
            Text.Message("The Hatchery slowly disintegrates, spitting out a new Gauss Rifle and clearing the creep.");
            ActII.Play();
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