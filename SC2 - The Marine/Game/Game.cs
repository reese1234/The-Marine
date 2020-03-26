using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Game
{
    class Game
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
        public static void Character(string name, string say)
        {
            Color.Text(Color.Cyan);
            Console.Write(name + ": ");
            Color.Reset();
            Message(say);
        }
    }
}
