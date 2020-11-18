using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Game
{
    class Program
    {
        private static bool done;
        public static int people = 1;

        public static int Zerg1;
        public static int Zerg2;
        public static int Zerg3;

        public static string answer;

        public static int health;
        public static int maxHealth;

        public static int level = 1;
        public static int xp;
        public static int xpNeed = 5;

        public static List<string> storage = new List<string>();
        public static List<Enemy> units = new List<Enemy>
        {
            new Enemy{ Name = "Zergling", Health = 35, Damage = Rnd(1, 3) },
            new Enemy{ Name = "Roach", Health = 130, Damage = Rnd(7, 11) },
            new Enemy{ Name = "Hydralisk", Health = 90, Damage = Rnd(5, 9) }
        };

        static void Main(string[] args)
        {
            try
            {
                Play();
            }
            catch (Exception e)
            {
                Text.Message("Hey! You made an Exception.");
                Text.Message("Hmm... Maybe that was me.");
                Text.Message("I am printing the Exception now.");
                Text.Message(Convert.ToString(e));
            }
            finally
            {
                Text.Message("Now, leave this game!");
            }
        }
        static void Play()
        {
            Text.Message("CODE: Nothing for beginning");
            Console.Write("> ");
            if (Console.ReadLine() == "JTVH8M37")
            {
                ActI();
            }
            else
            {
                Intro();
            }


            
        }

        public static void Intro()
        {
            Text.Message("Welcome to The Marine!");
            Text.Message("This is a game based on Starcraft.");
            Text.Message("Would you like to play?\n");
            Console.ReadKey();
            Console.WriteLine("\n\n");

            Text.Message("You are a Marine, left out in the forest.");
            Text.Message("You were out on a special mission, led by Emperor Mengsk.");
            Text.Message("Then, it didn't go as planned.");
            Text.Message("The Zerg attacked, and overwhelmed your team.");
            Text.Message("The others were able to escape, but they left you here.");
            Text.Message("Now you need to get out of this forest.");
            Text.Message("You will require much skill. You think you're up for it?\n");
            Console.ReadKey();
            Console.WriteLine("\n\n");
            Text.Message("ACT I");
            Text.Message("Checkpoint! Code: 'JTVH8M37'");
            ActI();
        }

        public static void ActI()
        {
            health = 15;
            maxHealth = 35;

            Text.Message("First, you will need to get some food. Your health is at 15, so get it before the Zerg realise you're still here.");
            Text.Message("These are your choices:");
            
            Text.Choice("Hunt for animals", "Look for fruit", "Call for help");
            if (answer == "a")
            {
                Text.Message("You chose to hunt for animals.");
                Text.Message("You found a wolf and killed it. But you didn't have anything to cook it with.");
                ChangeHealth(Rnd(5, 8), "Uncooked Meat");
            }
            else if (answer == "b")
            {
                Text.Message("You chose to get fruit from the trees around you.");
                Text.Message("Good move! You climbed the trees and easily found food.");
                ChangeHealth(Rnd(8, 13), "Fruit");
                XP(1);
            }
            else if (answer == "c")
            {
                Text.Message("You chose to look for help.");
                Text.Message("You didn't find anyone.");
                Health("-", Rnd(0, 4), "Tired/Hungry");
            }

            Text.Message("You have encountered a Zergling! It is going to fight you!");
            Text.Message("\nBATTLE!!!\n1 Zergling\n----------");
            Zerg1 = 20;
            while (Zerg1 > 0)
            {
                Text.Choice("Attack", "Move back", "Hold ground");
                if (answer == "a")
                {
                    Text.Message("You chose to attack.");
                    Zerg1 -= 5;
                    Text.Message($"You attacked once, but got hit. The Zergling has {Zerg1} health!");
                    Health("-", Rnd(1, 3), "Zergling");
                }
                else if (answer == "b")
                {
                    Text.Message("You chose to move back. You got a little tired.");
                    Health("-", Rnd(0, 2), "Tired");
                    Text.Message("You did a good move! The Zergling got tired.");
                    Zerg1 -= 4;
                    Text.Message($"Zergling's health dropped by 4. The Zergling has {Zerg1} health!");
                }
                else if (answer == "c")
                {
                    Text.Message("You chose to hold your ground.");
                    Zerg1 -= 10;
                    Text.Message($"You were able to attack 2 times but you were hit lots! The Zergling has {Zerg1} health!");
                    Health("-", Rnd(1, 3) * Rnd(2, 4), "Zergling");
                }
            }
            XP(2);
            if (health < 10)
            {
                Text.Message("A supply box dropped from the sky! It contained a Health Kit.");
                
                while (done == false)
                {
                    done = false;
                    Text.Message("Would you like to use the Health Kit now? Yes/No");
                    var a = Console.ReadLine().ToLower();
                    if (a == "yes")
                    {
                        Text.Message("You used the Health Kit.");
                        Health("+", 10, "Health Kit");
                        break;
                    }
                    else if (a == "no")
                    {
                        Text.Message("You put the Health Kit in your storage.");
                        storage.Add("Health Kit");
                        break;
                    }
                }

            }

            Text.Message("You come across an old hut. It looks like no one's used it for years.");
            Text.Choice("Knock", "Run", "Examine");

            if (answer == "c")
            {
                Text.Message("You found a pair of Flammable Sticks!");
                storage.Add("Flammable Sticks");
                XP(1);
            } else if (answer == "b")
            {
                Text.Message("You made a run for it. You got tired.");
                Health("-", Rnd(0, 3), "Tired");
                Text.Message("You found Fruit!");
                storage.Add("Fruit");
                XP(1);
                Text.Message("You found Zerglings!");
                Text.Message("BATTLE!!\n2 Zerglings\n----------");
                Zerg1 = 35;
                int Zerg2 = 35;
                while (Zerg1 > 0 && Zerg2 > 0)
                {
                    Text.Choice("Attack", "Items", "Flee");
                    if (answer == "a")
                    {
                        Text.Message("Attacked");
                        Zerg1 -= Rnd(4, 7);
                        Zerg2 -= Rnd(4, 7);
                        Text.Message($"Zergling 1 has {Zerg1}! Zergling 2 has {Zerg2}!");
                        Health("-", Rnd(2, 4) * Rnd(1, 4), "Zerglings");
                    } else if (answer == "b")
                    {
                        if (storage.Count > 0)
                        {
                            Text.Message("Items:");
                            foreach (string item in storage)
                            {
                                Console.WriteLine(item + "\n");
                                Thread.Sleep(250);
                            }
                            Console.Write("> ");
                            if (Item("Fruit"))
                            {
                                Health("+", 5, "Fruit");
                                storage.Remove("Fruit");
                            } else if (Item("Health Kit"))
                            {
                                Health("+", 10, "Health Kit");
                                storage.Remove("Health Kit");
                            } else if (Item("Flammable Sticks"))
                            {
                                if (Rnd(1, 6) > 2)
                                {
                                    Text.Message("You threw burning sticks!");
                                    Zerg1 -= Rnd(0, 9);
                                    Zerg2 -= Rnd(0, 9);
                                    Text.Message($"Zergling 1 has {Zerg1}! Zergling 2 has {Zerg2}!");
                                    Health("-", Rnd(0, 5), "Sticks");
                                } else
                                {
                                    Text.Message("Burning sticks failed.");
                                }
                                storage.Remove("Flammable Sticks");
                            }
                        } else
                        {
                            Text.Message("No items.");
                        }
                    } else if (answer == "c")
                    {

                    }
                }
                if (Zerg1 == 0 || Zerg2 == 0)
                {
                    Text.Message("You defeated the Zerglings!");
                    XP(3);
                }
            }
            if (answer == "a")
            {
                Text.Message("You knocked on the door of the hut. It opened, and you went in.");
            } else
            {
                Text.Message("You went back to the hut and knocked. The door opened, and you went in.");
            }

            Text.Message("You see Zerg Creep crawling on the floor. You notice a Creep Tumor in front of you.\n" +
                "Other than those, it's empty except for a closet at the other side.");
            Text.Character("Creep Tumor", L("*scowl*", "*sss*", "seee"), Color.Purple);

            Text.Choice("Walk around", "Shoot Creep Tumor", "Jump to closet");
            if (answer == "a")
            {
                Text.Message("You carefully walk around. ZERGLINGS APPEAR!!");
                Text.Choice("Fight", "Lure away");

                bool fight = false;
                if (answer == "a")
                {
                    fight = true;
                } else if (answer == "b")
                {
                    if (storage.Contains("Fruit"))
                    {
                        Text.Message("You used some Fruit to lure the Zerglings away.");
                        storage.Remove("Fruit");
                        fight = false;
                    } else
                    {
                        Text.Message("You don't have anything to lure them away with.");
                        fight = true;
                    }
                }
                if (fight == true)
                {
                    Zerg1 = e("Zergling", "h");
                    Zerg2 = e("Zergling", "h");
                }
                
            } else if (answer == "b")
            {
                Text.Message("You tentatively shoot the Creep Tumor.");
                Text.Character("Creep Tumor", L("*SCREAM!!!*", "*CAAA!!!*"), Color.Purple);
                Health("-", Rnd(0, 3), "Creep");
                Text.Message("A bucket of Cooked Fish is at the corner!");
                storage.Add("Cooked Fish");
                XP(1);
            } else if (answer == "c")
            {
                if (Rnd(1, 11) > 4)
                {
                    Text.Message("You made the jump!!");
                    Text.Message("The closet is banging. You open it. A Marine is stuck inside.");
                    Text.Character("Marine", L("Thank you! Can I go with you?", "Phew! May I join you?"), Color.Cyan);

                    Text.Choice("Yes", "No");
                    if (answer == "a")
                    {
                        Text.Character("You", L("Sure!", "That's great!"));
                        XP(5);
                        people++;
                    } 
                    else if (answer == "b")
                    {
                        Text.Character("You", L("Sorry, but no.", "I deny!"));
                        Text.Message("The marine walks away.");
                    }
                    
                    Text.Message("Behind him, there's a box with Steak and Fruit.");
                    storage.Add("Fruit");
                    storage.Add("Fruit");
                    storage.Add("Steak");
                    Text.Message("");

                } else
                {

                }
            }

            Exit();
        }
       
        public static void Health(string isUp, int change, string reason)
        {
            if (!(change == 0))
            {
                if (isUp == "+")
                {
                    health += change;
                    if (health > maxHealth) { health = maxHealth; }
                    Text.Message($"+{change} health ({reason}): {health}!");
                }
                else if (isUp == "-")
                {
                    health -= change;
                    if (health < 0)
                    {
                        Text.Message("Oh no! You died! You were left in the forest, never to be seen again.");
                        throw new Exception("DEAD");
                    }
                    Text.Message($"-{change} health ({reason}): {health}!");
                }
            } 
        }

        public static void ChangeHealth(int change, string reason)
        {
            if (change != 0)
            {
                health += change;
                if (health > maxHealth) 
                    health = maxHealth;
                if (change.ToString().Contains("-"))
                    Text.Message($"-{change} health ({reason}): {health}!");
                else
                    Text.Message($"+{change} health ({reason}): {health}!");

                if (health <= 0)
                {
                    Text.Message("Oh no! You died! You were left in the forest, never to be seen again.");
                    throw new Exception("DEAD");
                }
            }
        }
        public static void XP(int change)
        {
            xp += change;
            
            if (xp > xpNeed || xp == xpNeed)
            {
                level++;
                xp = 0;
                maxHealth += 5;
                health = maxHealth;
                Text.Message($"Level Up: {level}! Max Health now {maxHealth}!");
                xpNeed += 2;
            } 
            else
                Text.Message($"+{change} XP: {xp}/{xpNeed}.");
        }

        public static int Rnd(int min, int max)
        {
            Random rnd = new Random();
            return rnd.Next(min, max);
        }
        public static bool Item(string item)
        {
            string choice = Console.ReadLine().ToLower();
            if (choice == item.ToLower() && storage.Contains(item))
            {
                return true;
            } else
            {
                return false;
            }
            
        }
        public static bool choice(string isAnswer)
        {
            isAnswer = isAnswer.ToLower();
            if (isAnswer == answer.ToLower())
            {
                return true;
            } else
            {
                return false;
            }
        }
        public static int e(string name, string data)
        {
            if (data == "h")
            {
                return units.SingleOrDefault(e => e.Name == name).Health;
            } else if (data == "dmg")
            {
                return units.SingleOrDefault(e => e.Name == name).Damage;
            } else
            {
                return units.SingleOrDefault(e => e.Name == name).Health;
            }
            
        }

        public static string[] L(string item0, string item1)
        {
            return new string[] { item0, item1 };
        }
        public static string[] L(string item0, string item1, string item2)
        {
            return new string[] { item0, item1, item2 };
        }

        public static void Exit()
        {
            Thread.Sleep(3000);
            Color.Text(Color.Green);
            Console.WriteLine("Exiting in 5...");
            Thread.Sleep(1000);
            Console.WriteLine("           4...");
            Thread.Sleep(1000);
            Console.WriteLine("           3...");
            Thread.Sleep(1000);
            Console.WriteLine("           2...");
            Thread.Sleep(1000);
            Console.WriteLine("           1...");
            Thread.Sleep(1000);
            Environment.Exit(0);
        }
    }
}
