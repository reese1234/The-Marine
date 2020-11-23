using System;
using System.Threading;

namespace Game
{
    class Game
    {
        public static int GameSpeed = 50;

        public static string Input()
        {
            Console.Write("> ");
            Color.Text(Color.Green);
            Data.Answer = Console.ReadLine().ToLower();
            Color.Reset();
            return null;
        }

        public static void ChangeHealth(int change, string reason)
        {
            if (change != 0)
            {
                Data.Health += change;
                if (Data.Health > Data.MaxHealth)
                    Data.Health = Data.MaxHealth;
                if (change.ToString().Contains("-"))
                    Text.Message($"{change} health ({reason}): {Data.Health}/{Data.MaxHealth}!");
                else
                    Text.Message($"+{change} health ({reason}): {Data.Health}/{Data.MaxHealth}!");

                if (Data.Health <= 0)
                {
                    Text.Message("Oh no! You died! You were left in the forest, never to be seen again.");
                    throw new Exception("DEAD");
                }
            }
        }
        public static void XP(int change)
        {
            Data.XP += change;

            if (Data.XP > Data.XPNeeded || Data.XP == Data.XPNeeded)
            {
                Data.Level++;
                Data.XP = 0;
                Data.MaxHealth += 5;
                Data.Health = Data.MaxHealth;
                Text.Message($"Level Up: {Data.Level}! Max Health now {Data.MaxHealth}!");
                Data.XPNeeded += 2;
            }
            else
                Text.Message($"+{change} XP: {Data.XP}/{Data.XPNeeded}.");
        }

        public static int Rnd(int min, int max)
        {
            Random rnd = new Random();
            return rnd.Next(min, max);
        }
        public static bool CheckItem(string item)
        {
            if (Data.Answer == item.ToLower() && Data.Storage.Contains(item))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public static void Choice(string A, string B)
        {
            Data.Answer = "";
            Color.Text(Color.Cyan);
            Console.WriteLine($"  A: {A}");
            Thread.Sleep(350);
            Console.WriteLine($"  B: {B}");
            Thread.Sleep(350);

            Color.Reset();

            for (; ; )
            {
                Input();

                if (Data.Answer.Contains("a") || Data.Answer.Contains("b"))
                {
                    break;
                }
                else
                {
                    continue;
                }
            }
        }
        public static void Choice(string A, string B, string C)
        {
            Data.Answer = "";
            Color.Text(Color.Cyan);
            Console.WriteLine($"  A: {A}");
            Thread.Sleep(350);
            Console.WriteLine($"  B: {B}");
            Thread.Sleep(350);
            Console.WriteLine($"  C: {C}");
            Thread.Sleep(350);


            Color.Reset();

            for (; ; )
            {
                Input();

                if (Data.Answer.Contains("a") || Data.Answer.Contains("b") || Data.Answer.Contains("c"))
                {
                    break;
                }
                else
                {
                    continue;
                }
            }
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
