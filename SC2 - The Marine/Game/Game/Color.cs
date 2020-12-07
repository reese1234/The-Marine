using System;

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
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static ConsoleColor Red = ConsoleColor.Red;
        public static ConsoleColor Green = ConsoleColor.Green;
        public static ConsoleColor Cyan = ConsoleColor.Cyan;
        public static ConsoleColor Yellow = ConsoleColor.Yellow;
        public static ConsoleColor Purple = ConsoleColor.Magenta;
    }
}
