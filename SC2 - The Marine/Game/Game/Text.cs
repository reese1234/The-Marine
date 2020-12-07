using System;
using System.Threading;

namespace Game
{
    class Text
    {
        public static void Message(string say, ConsoleColor color = ConsoleColor.White)
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

        public static void Character(string name, string[] possibleMessages, ConsoleColor color = ConsoleColor.Cyan)
        {
            Random r = new Random();
            string say = possibleMessages[r.Next(0, possibleMessages.Length)];
            Color.Text(color);
            Console.Write(name + ": ");
            Color.Text(ConsoleColor.Gray);
            Message(say);
        }
    }
}
