namespace Game
{
    class People
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int MinDamage { get; set; }
        public int MaxDamage { get; set; }
        public int TimesToAttack { get; set; }
        public bool Ordered = false;

        public int MinDmg()
        {
            return Data.Companions.Find(x => x == this).MinDamage;
        }
        public int MaxDmg()
        {
            return Data.Companions.Find(x => x == this).MaxDamage;
        }
        public int Attacks()
        {
            return Data.Companions.Find(x => x == this).TimesToAttack;
        }
        public bool Order()
        {
            return Data.Companions.Find(x => x == this).Ordered;
        }

        public void ChangeMinDmg(int newValue)
        {
            Data.Companions.Find(x => x == this).MinDamage = newValue;
        }
        public void ChangeMaxDmg(int newValue)
        {
            Data.Companions.Find(x => x == this).MaxDamage = newValue;
        }
        public void ChangeAttacks(int newValue)
        {
            Data.Companions.Find(x => x == this).TimesToAttack = newValue;
        }
        public void Ordering()
        {
            if (Ordered)
                Ordered = false;
            if (!Ordered)
                Ordered = true;
        }

        public int Dmg()
        {
            return Game.Rnd(MaxDamage, MinDamage);
        }
    }
}