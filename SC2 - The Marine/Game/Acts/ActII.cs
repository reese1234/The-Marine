using System;
using System.Threading;

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
            Description =   "A Zerg that has Needle Spines and slithers.\n" +
                            "Attacks quickly and can hit both air and ground.",
            MaxHealth =     40,
            MinDamage =     -2,
            MaxDamage =     -5
        };

        static Enemy Roach = new Enemy
        {
            Name =          "Roach",
            Description =   "A Zerg burrowing assault unit. Has Acid Saliva to melt enemies.\n" +
                            "             Used for surprise attacks and can regenerate health quickly.",
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
            Description = "A blacksmith who can easily fix broken weapons.\n" +
                            "He is also trained to use a Firebat suit, making him a useful fighter.",
            MinDamage = -4,
            MaxDamage = -7
        };

        public static People BlacksmithFriend = new People
        {
            Name = "Blacksmith",
            Description =   "A blacksmith who can easily fix broken weapons.\n" +
                            "He is also trained to use a Firebat suit, making him a useful fighter.\n" +
                            "He is friends with the Marine, who gives him extra training and practice.",
            MinDamage = -6,
            MaxDamage = -8
        };

        public static void Play()
        {
            if (Data.MaxHealth == 0)
                Data.MaxHealth = 35;
            Data.Health = Data.MaxHealth;

            Console.WriteLine("\n");
            Text.Message("ACT II");
            Text.Message("Never on the Surface\n", Color.Purple);

            Game.HealthKit();
            Text.Message("\nYou keep walking on your path, carrying the new Gauss Rifle.");
            Text.Message("It was badly damaged, as it had been digested a Hatchery.");

            Data.Companions.Add(ActI.Marine);
            if (Data.Companions.Contains(ActI.Marine))
            {
                Text.Character("\nMarine", Game.List("Hey, I know someone who could fix that.",
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

            Text.Message("\nImmediately after that, the ground starts rumbling.");
            if (Data.Companions.Contains(ActI.Marine))
                Text.Character("Marine", Game.List("Is this an earthquake?", "Look out, this looks bad!"));
            Text.Message("Out of the ground comes a monstrosity, with green saliva dripping from its tusks, melting the ground beneath it.", Color.Red);
            Text.Message("On its head are a few health items, which would be useful if you defeat it.", Color.Red);

            Roach.Add();
            Roach.Announce();

            Console.WriteLine("\n");

            while (Roach.HP() > 0)
            {
                Game.Choice("Attack", "Items", "Order");
                if (Data.Answer == "a")
                    Roach.ChangeHP("Attacked Roach", Game.Rnd(-7, -4));
                else if (Data.Answer == "b")
                    Game.ChooseItem();
                else if (Data.Answer == "c")
                {
                    Text.Message("\nCompanions:");
                    foreach (People person in Data.Companions)
                    {
                        Color.Text(Color.Yellow);
                        Console.WriteLine($"\n{person.Name}");
                        Color.Reset();
                        Console.Write("Description: ");
                        Text.Message(person.Description, ConsoleColor.Gray);
                    }
                    Console.WriteLine();
                    Game.Input();

                    if (Data.Answer == "marine" && Data.Companions.Contains(ActI.Marine))
                    {
                        ActI.Marine.Ordering();
                        Game.Choice("Attack", "Stimpack");
                        if (Data.Answer == "a")
                        {
                            Console.WriteLine();
                            for (int i = 0; i < ActI.Marine.Attacks(); i++)
                            {
                                Text.Message("Enemies:");
                                foreach (Enemy enemy in Data.Foes)
                                {
                                    Console.WriteLine($"  - {enemy.Name} ({enemy.Health}/{enemy.MaxHealth})");
                                    Thread.Sleep(250);
                                }
                                Game.Input();

                                if (Data.Answer == "roach" && Data.Foes.Contains(Roach))
                                {
                                    Roach.ChangeHP("Marine attacked Roach", ActI.Marine.Dmg());
                                }
                            }
                        }
                        else if (Data.Answer == "b")
                        {
                            ActI.Marine.ChangeMinDmg(-2);
                            ActI.Marine.ChangeMaxDmg(-4);
                            ActI.Marine.ChangeAttacks(2);
                            Text.Message("\nDamage was halved and now can attack twice.\n", Color.Yellow);
                        }
                    }
                }

                if (Data.Companions.Contains(ActI.Marine))
                {
                    if (Game.Rnd(0, 2) == 0 && !ActI.Marine.Order())
                        Roach.ChangeHP("Marine attacked Roach", ActI.Marine.Dmg());
                    ActI.Marine.Ordering();
                }

                if (Game.Rnd(0, 2) == 0)
                {
                    Game.ChangeHealth(Roach.Dmg(), "Roach");
                    if (Game.Rnd(0, 3) == 0 && Data.AcidEffect == 0)
                    {
                        Text.Message("You've been acidified!", Color.Yellow);
                        Data.AcidEffect = 1;
                    }
                }
                if (Data.AcidEffect > 0)
                    Game.ChangeHealth(-Data.AcidEffect, "Acid");
            }
            if (Data.Companions.Contains(ActI.Marine))
            {
                ActI.Marine.ChangeMinDmg(-4);
                ActI.Marine.ChangeMaxDmg(-7);
                ActI.Marine.ChangeAttacks(1);
                Text.Message("Marine's damage and attacks have reset.\n", Color.Yellow);
            }
        }
    }
}