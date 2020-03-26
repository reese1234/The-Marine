using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    static class Color
    {
        public static void Text(ConsoleColor color)
        {
            Console.ForegroundColor = color;
        }

        public static void Reset()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public static ConsoleColor Red = ConsoleColor.Red;
        public static ConsoleColor Green = ConsoleColor.Green;
        public static ConsoleColor Cyan = ConsoleColor.Cyan;
        public static ConsoleColor White = ConsoleColor.White;
    }
}
