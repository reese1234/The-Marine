using System.Collections.Generic;

namespace Game
{
    class Data
    {
        public static int People = 0;
        public static string Answer;

        public static int Health;
        public static int MaxHealth;

        public static int Level = 1;
        public static int XP;
        public static int XPNeeded = 5;

        public static List<string> Storage = new List<string>();
        public static List<Enemy> units = new List<Enemy>
        {
            new Enemy{ Name = "Zergling", Health = 35, Damage = Game.Rnd(1, 3) },
            new Enemy{ Name = "Roach", Health = 130, Damage = Game.Rnd(7, 11) },
            new Enemy{ Name = "Hydralisk", Health = 90, Damage = Game.Rnd(5, 9) }
        };
    }
}
