using System;
using System.Threading;

namespace Game
{
    class Text
    {
        public static void Message(string say)
        {
            foreach (char letter in say)
            {
                Console.Write(letter);
                Thread.Sleep(Game.GameSpeed);
            }
            Console.Write("\n");
            Thread.Sleep(500);
        }
        public static void Message(string say, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            foreach (char letter in say)
            {
                Console.Write(letter);
                Thread.Sleep(Game.GameSpeed);
            }
            Console.Write("\n");
            Color.Reset();
            Thread.Sleep(500);
        }
        public static void Message(string[] possibleMessages, ConsoleColor color)
        {
            Random r = new Random();
            Console.ForegroundColor = color;

            string say = possibleMessages[r.Next(0, possibleMessages.Length)];

            foreach (char letter in say)
            {
                Console.Write(letter);
                Thread.Sleep(50);
            }

            Console.WriteLine();
            Color.Reset();
            Thread.Sleep(500);
        }

        public static void Character(string name, string say, ConsoleColor color = ConsoleColor.Cyan)
        {
            Color.Text(color);
            Console.Write(name + ": ");
            Color.Reset();
            Message(say);
        }
        public static void Character(string name, string[] possibleMessages, ConsoleColor color = ConsoleColor.Cyan)
        {
            Random r = new Random();
            string say = possibleMessages[r.Next(0, possibleMessages.Length)];
            Color.Text(color);
            Console.Write(name + ": ");
            Color.Reset();
            Message(say);
        }
    }
}
