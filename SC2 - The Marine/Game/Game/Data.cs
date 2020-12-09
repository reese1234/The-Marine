using System.Collections.Generic;

namespace Game
{
    class Data
    {
        public static string Answer;

        public static int Health;
        public static int MaxHealth;
        public static int AcidEffect;

        public static int Level = 1;
        public static int XP;
        public static int XPNeeded = 5;

        public static List<string> Storage = new List<string>();
        public static List<Enemy> Foes = new List<Enemy>();
        public static List<People> Companions = new List<People>();
    }
}