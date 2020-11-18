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
                Thread.Sleep(50);
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
                Thread.Sleep(50);
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

        public static void Choice(string A, string B)
        {
            Program.answer = "";
            Color.Text(Color.Cyan);
            Console.WriteLine($"\tA: {A}");
            Thread.Sleep(350);
            Console.WriteLine($"\tB: {B}");
            Thread.Sleep(350);

            Color.Reset();

            for (; ; )
            {
                Console.Write("> ");
                Program.answer = Console.ReadLine().ToLower();

                if (Program.answer.Contains("a") || Program.answer.Contains("b"))
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
            Program.answer = "";
            Color.Text(Color.Cyan);
            Console.WriteLine($"\tA: {A}");
            Thread.Sleep(350);
            Console.WriteLine($"\tB: {B}");
            Thread.Sleep(350);
            Console.WriteLine($"\tC: {C}");
            Thread.Sleep(350);


            Color.Reset();

            for (; ; )
            {
                Console.Write("> ");
                Program.answer = Console.ReadLine().ToLower();

                if (Program.answer.Contains("a") || Program.answer.Contains("b") || Program.answer.Contains("c"))
                {
                    break;
                }
                else
                {
                    continue;
                }
            }
        }
    }
}
