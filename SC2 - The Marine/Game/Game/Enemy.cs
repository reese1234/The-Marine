using System;
using System.Threading;

namespace Game
{
    public class Enemy
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int MinDamage { get; set; }
        public int MaxDamage { get; set; }

        public void Add()
        {
            Health = MaxHealth;
            Data.Foes.Add(this);
        }
        public void Announce()
        {
                Color.Text(Color.Yellow);
                Console.WriteLine($"\n{Name}");
                Color.Reset();
                Console.Write("Description: ");
                Text.Message(Description, ConsoleColor.Gray);
                Console.Write("Health: ");
                Thread.Sleep(250);
                Color.Text(Color.Red);
                Console.Write($"     {Health}/{MaxHealth}");
        }

        public int HP()
        {
            return Data.Foes.Find(x => x == this).Health;
        }
        public int MaxHP()
        {
            return Data.Foes.Find(x => x == this).MaxHealth;
        }
        public int MinDmg()
        {
            return Data.Foes.Find(x => x == this).MinDamage;
        }
        public int MaxDmg()
        {
            return Data.Foes.Find(x => x == this).MaxDamage;
        }

        public int Dmg()
        {
            return Game.Rnd(MaxDamage, MinDamage);
        }

        public void ChangeHP(string reason, int change)
        {
            Data.Foes.Find(x => x == this).Health += change;
            if (Health <= 0)
            {
                Text.Message($"\nDefeated {Name} (-{change})");
            }
            else
                Text.Message($"\n{reason} ({change}): {Health}/{MaxHealth}");
        }
    }
}