using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Game
{
    class Program
    {
        private static bool done;

        public static int Zerg1;
        public static int Zerg2;

        private static string A;
        private static string B;
        private static string C;
        private static string answer;

        public static int health = 15;
        public static int maxHealth = 35;

        public static int level = 1;
        public static int xp;
        public static int xpNeed = 5;

        public static List<string> storage = new List<string>();
        public static List<Enemy> units = new List<Enemy>
        {
            new Enemy{ name = "Zergling", health = 35, damage = Rnd(1, 3) },
            new Enemy{ name = "Roach", health = 130, damage = Rnd(7, 11) },
            new Enemy{ name = "Hydralisk", health = 90, damage = Rnd(5, 9) }
        };

        static void Main(string[] args)
        {
            try
            {
                Play();
            }
            catch (Exception e)
            {
                Game.Message("Hey! You made an Exception.");
                Game.Message("Hmm... Maybe that was me.");
                Game.Message("I am printing the Exception now.");
                Game.Message(Convert.ToString(e));
            }
            finally
            {
                Game.Message("Now, leave this game!");
            }
        }
        static void Play()
        {
            Game.Message("CODE: Nothing for beginning");
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
            Game.Message("Welcome to The Marine!");
            Game.Message("This is a game based on Starcraft.");
            Game.Message("Would you like to play?\n");
            Console.ReadKey();
            Console.WriteLine("\n\n");

            Game.Message("You are a Marine, left out in the forest.");
            Game.Message("You were out on a special mission, led by Emperor Mengsk.");
            Game.Message("Then, it didn't go as planned.");
            Game.Message("The Zerg attacked, and overwhelmed your team.");
            Game.Message("The others were able to escape, but they left you here.");
            Game.Message("Now you need to get out of this forest.");
            Game.Message("You will require much skill. You think you're up for it?\n");
            Console.ReadKey();
            Console.WriteLine("\n\n");
            Game.Message("ACT I");
            Game.Message("Checkpoint! Code: 'JTVH8M37'");
            ActI();
        }

        public static void ActI()
        {
            Game.Message("First, you will need to get some food. Your health is at 15, so get it before the Zerg realise you're still here.");
            Game.Message("These are your choices:");
            A = "Hunt for animals";
            B = "Look for fruit";
            C = "Run around for help";
            Question(3);
            
            if (answer == "a")
            {
                Game.Message("You chose to hunt for animals.");
                Game.Message("You found a wolf and killed it. But you didn't have anything to cook it with.");
                Health("+", Rnd(5, 8), "Uncooked Meat");
            }
            else if (answer == "b")
            {
                Game.Message("You chose to get fruit from the trees around you.");
                Game.Message("Good move! You climbed the trees and easily found food.");
                Health("+", Rnd(8, 13), "Fruit");
                XP(1);
            }
            else if (answer == "c")
            {
                Game.Message("You chose to look for help.");
                Game.Message("You didn't find anyone.");
                Health("-", Rnd(0, 4), "Tired/Hungry");
            }

            Game.Message("You have encountered a Zergling! It is going to fight you!");
            Game.Message("\nBATTLE!!!\n1 Zergling\n----------");
            A = "Attack";
            B = "Move back";
            C = "Hold ground";
            Zerg1 = 20;
            while (Zerg1 > 0)
            {
                Question(3);
                if (answer == "a")
                {
                    Game.Message("You chose to attack.");
                    Zerg1 -= 5;
                    Game.Message($"You attacked once, but got hit. The Zergling has {Zerg1} health!");
                    Health("-", Rnd(1, 3), "Zergling");
                }
                else if (answer == "b")
                {
                    Game.Message("You chose to move back. You got a little tired.");
                    Health("-", Rnd(0, 2), "Tired");
                    Game.Message("You did a good move! The Zergling got tired.");
                    Zerg1 -= 4;
                    Game.Message($"Zergling's health dropped by 4. The Zergling has {Zerg1} health!");
                }
                else if (answer == "c")
                {
                    Game.Message("You chose to hold your ground.");
                    Zerg1 -= 10;
                    Game.Message($"You were able to attack 2 times but you were hit lots! The Zergling has {Zerg1} health!");
                    Health("-", Rnd(1, 3) * Rnd(2, 4), "Zergling");
                }
            }
            XP(2);
            if (health < 10)
            {
                Game.Message("A supply box dropped from the sky! It contained a Health Kit.");
                
                while (done == false)
                {
                    done = false;
                    Game.Message("Would you like to use the Health Kit now? Yes/No");
                    var a = Console.ReadLine().ToLower();
                    if (a == "yes")
                    {
                        Game.Message("You used the Health Kit.");
                        Health("+", 10, "Health Kit");
                        break;
                    }
                    else if (a == "no")
                    {
                        Game.Message("You put the Health Kit in your storage.");
                        storage.Add("Health Kit");
                        break;
                    }
                }

            }
            Game.Message("You come across an old hut. It looks like no one's used it for years.");
            A = "Knock";
            B = "Run";
            C = "Examine";
            Question(3);
            if (answer == "c")
            {
                Game.Message("You found a pair of Flammable Sticks!");
                storage.Add("Flammable Sticks");
                XP(1);
            } else if (answer == "b")
            {
                Game.Message("You made a run for it. You got tired.");
                Health("-", Rnd(0, 3), "Tired");
                Game.Message("You found Fruit!");
                storage.Add("Fruit");
                XP(1);
                Game.Message("You found Zerglings!");
                Game.Message("BATTLE!!\n2 Zerglings\n----------");
                Zerg1 = 35;
                int Zerg2 = 35;
                while (Zerg1 > 0 && Zerg2 > 0)
                {
                    A = "Attack";
                    B = "Items";
                    C = "Flee";
                    Question(3);
                    if (answer == "a")
                    {
                        Game.Message("Attacked");
                        Zerg1 -= Rnd(4, 7);
                        Zerg2 -= Rnd(4, 7);
                        Game.Message($"Zergling 1 has {Zerg1}! Zergling 2 has {Zerg2}!");
                        Health("-", Rnd(2, 4) * Rnd(1, 4), "Zerglings");
                    } else if (answer == "b")
                    {
                        if (storage.Count > 0)
                        {
                            Game.Message("Items:");
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
                                    Game.Message("You threw burning sticks!");
                                    Zerg1 -= Rnd(0, 9);
                                    Zerg2 -= Rnd(0, 9);
                                    Game.Message($"Zergling 1 has {Zerg1}! Zergling 2 has {Zerg2}!");
                                    Health("-", Rnd(0, 5), "Sticks");
                                } else
                                {
                                    Game.Message("Burning sticks failed.");
                                }
                                storage.Remove("Flammable Sticks");
                            }
                        } else
                        {
                            Game.Message("No items.");
                        }
                    } else if (answer == "c")
                    {

                    }
                }
                if (Zerg1 == 0 || Zerg2 == 0)
                {
                    Game.Message("You defeated the Zerglings!");
                    XP(3);
                }
            }
            if (answer == "a")
            {
                Game.Message("You knocked on the door of the hut. It opened, and you went in.");
            } else
            {
                Game.Message("You went back to the hut and knocked. The door opened, and you went in.");
            }

            Game.Message("You see Zerg Creep crawling on the floor. You notice a Creep Tumor in front of you.\n" +
                "Other than those, it's empty except for a closet at the other side.");
            Game.Character("Creep Tumor", "*scowl*");
            A = "Walk around";
            B = "Shoot Creep Tumor";
            C = "Jump to closet";
            Question(3);
            if (answer == "a")
            {
                Game.Message("You carefully walk around. ZERGLINGS APPEAR!!");
                A = "Fight";
                B = "Lure away";
                Question(2);

                bool fight = false;
                if (answer == "a")
                {
                    fight = true;
                } else if (answer == "b")
                {
                    if (storage.Contains("Fruit"))
                    {
                        Game.Message("You used some Fruit to lure the Zerglings away.");
                        storage.Remove("Fruit");
                        fight = false;
                    } else
                    {
                        Game.Message("You don't have anything to lure them away with.");
                        fight = true;
                    }
                }
                if (fight == true)
                {
                    Zerg1 = Enemy("Zergling");
                    Zerg2 = Enemy("Zergling");
                }
                
            }

            Console.ReadKey();
        }


        public static void Question(int choices)
        {
            Console.WriteLine($" A. {A}");
            Thread.Sleep(350);
            Console.WriteLine($" B. {B}");
            Thread.Sleep(350);
            if (choices == 3)
            {
                Console.WriteLine($" C. {C}");
                            Thread.Sleep(350);
            }
            
            while (done == false)
            {
                done = false;

                Console.Write("> ");
                answer = Console.ReadLine().ToLower();
                if (answer.Contains("a") || (answer.Contains("b") || (answer.Contains("c"))))
                {
                    break;
                }
                else
                {
                    continue;
                }
            }
        }

       
        public static void Health(string pm, int change, string reason)
        {
            if (!(change == 0))
            {
                if (pm == "+")
                {
                    health += change;
                    if (health > maxHealth) { health = maxHealth; }
                    Game.Message($"+{change} health ({reason}): {health}!");
                }
                else if (pm == "-")
                {
                    health -= change;
                    if (health < 0)
                    {
                        Game.Message("Oh no! You died! You were left in the forest, never to be seen again.");
                        throw new Exception("DEAD");
                    }
                    Game.Message($"-{change} health ({reason}): {health}!");
                }
            } 
        }
        public static void XP(int change)
        {
            xp += change;
            
            if(xp > xpNeed || xp == xpNeed)
            {
                level++;
                maxHealth += 5;
                health = maxHealth;
                Game.Message($"Level Up: {level}! Max Health now {maxHealth}!");
                xpNeed += 2;
            } else
            {
                Game.Message($"+{change} XP: {xp}/{xpNeed}.");
            }
        }

        public static bool Item(string item)
        {
            bool isTrue = false;
            string choice = Console.ReadLine().ToLower();
            if (choice == item.ToLower() && storage.Contains(item))
            {
                isTrue = true;
            }
            return isTrue;
        }
        public static int Rnd(int min, int max)
        {
            Random rnd = new Random();
            int num = rnd.Next(min, max);
            return num;
        }
        public static int Enemy(string Name)
        {
            int enemy = units.SingleOrDefault(e => e.name == Name).health;
            return enemy;
        }
    }
}
