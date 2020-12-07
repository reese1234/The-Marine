using System;
using System.Collections.Generic;
using System.Text;

namespace Game
{
    class ActII
    {
        static Enemy Zergling = new Enemy
        {
            Name =          "Zergling",
            Description =   "A small Zerg creature with sharp claws and short legs.\n" +
                            "Can overwhelm enemies in big swarms.",
            MaxHealth =     35,
            MinDamage =     0,
            MaxDamage =     -3
        };

        static Enemy Hydralisk = new Enemy
        {
            Name =          "Hydralisk",
            Description =   "A Zerg that has Needle Spines and slithers." +
                            "Attacks quickly and can hit both air and ground.",
            MaxHealth =     40,
            MinDamage =     -2,
            MaxDamage =     -5
        };

        static Enemy Roach = new Enemy
        {
            Name =          "Roach",
            Description =   "A Zerg burrowing assault unit. Has Acid Saliva to melt enemies." +
                            "Used for surprise attacks and can regenerate health quickly.",
            MaxHealth =     50,
            MinDamage =     -3,
            MaxDamage =     -7
        };

        static Enemy Mutalisk = new Enemy
        {
            Name = "Mutalisk",
            Description = "",
            MaxHealth = 40,
            MinDamage = -0,
            MaxDamage = -3
        };

        public static People Blacksmith = new People
        {
            Name = "Blacksmith",
            Description = "A blacksmith who can easily fix broken weapons. " +
                            "He is also trained to use a Firebat suit, making him a useful fighter.",
            MinDamage = -4,
            MaxDamage = -7
        };

        public static People BlacksmithFriend = new People
        {
            Name = "Blacksmith",
            Description = "A blacksmith who can easily fix broken weapons. " +
                            "He is friends with the Marine, and is good in a Firebat suit.",
            MinDamage = -6,
            MaxDamage = -8
        };

        public static void Play()
        {
            Console.WriteLine("\n");
            Text.Message("ACT II");
            Text.Message("Never on the Surface", Color.Purple);

            Text.Message("\nYou keep walking on your path, carrying the new Gauss Rifle.");
            Text.Message("It was badly damaged, as it had been digested a Hatchery.\n");
            Data.Companions.Add(ActI.Marine);
            if (Data.Companions.Contains(ActI.Marine))
            {
                Text.Character("Marine", Game.List("Hey, I know someone who could fix that.",
                                                    "I have a friend who is a master at fixing things.",
                                                    "There is a guy that could take a look at that."));
                Game.Choice("'Contact him!'", "'Where is he now?'");
                if (Data.Answer == "a")
                {
                    Text.Character("\nYou", Game.List("Give him a call!", "Contact him!"), Color.Green);
                    Text.Message("The Marine reached into their pocket, and pulled out a strange device.");
                    Text.Character("Marine", Game.List("He said he would try to come to come here.", "He'll come to us, but we need to stay here."));
                    Text.Message("Marine's Friend will now come sooner!", Color.Yellow);
                }
                else if (Data.Answer == "b")
                {
                    Text.Character("\nYou", Game.List("Do you know what he's doing?", "Where is he now?"), Color.Green);
                    Text.Character("Marine", Game.List("I think he's trying to make a new weapon now.", 
                                                        "I'm pretty sure he's testing out a new weapon for the Dominion."));
                }
            }


        }
    }
}