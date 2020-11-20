using System;

namespace Game
{
    class Program
    {
        static void Main()
        {
            try
            {
                Play();
            }
            catch (Exception e)
            {
                Text.Message("Hey! You made an Exception.");
                Text.Message("Hmm... Maybe that was me.");
                Text.Message("I am printing the Exception now.");
                Text.Message(Convert.ToString(e));
            }
            finally
            {
                Text.Message("Now, leave this game!");
            }
        }
        static void Play()
        {
            Text.Message("Set a typing speed: 50 for default");
            Console.Write("> ");
            Console.ForegroundColor = ConsoleColor.Green;
            Game.GameSpeed = Convert.ToInt32(Console.ReadLine());
            Console.ForegroundColor = ConsoleColor.White;

            Text.Message("Type in code (nothing to start introduction)");
            Console.Write("> ");
            if (Console.ReadLine() == "JTVH8M37")
            {
                ActI.Play();
            }
            else
            {
                Intro();
                ActI.Play();
            }
        }

        public static void Intro()
        {
            Text.Message("Welcome to The Marine!");
            Text.Message("This is a game based on Starcraft.");
            Text.Message("Would you like to play?\n");
            Console.ReadKey();
            Console.WriteLine("\n\n");

            Text.Message("You are a Marine, left out in the forest.");
            Text.Message("You were out on a special mission, led by Emperor Mengsk.");
            Text.Message("Then, it didn't go as planned.");
            Text.Message("The Zerg attacked, and overwhelmed your team.");
            Text.Message("The others were able to escape, but they left you here.");
            Text.Message("Now you need to get out of this forest.");
            Text.Message("You will require much skill. You think you're up for it?\n");
            Console.ReadKey();
        }
    }
}
