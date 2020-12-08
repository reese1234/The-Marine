using System;
using System.Collections.Generic;
using System.Text;

namespace Game
{
    class People
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int MinDamage { get; set; }
        public int MaxDamage { get; set; }
        public int TimesToAttack { get; set; }

        public int GetInt(string toGet)
        {
            toGet.ToLower();
            if (toGet == "mindmg")
                return Data.Companions.Find(x => x == this).MinDamage;
            if (toGet == "maxdmg")
                return Data.Companions.Find(x => x == this).MaxDamage;
            if (toGet == "attacks")
                return Data.Companions.Find(x => x == this).TimesToAttack;
            else
                return 0;
        }
    }
}