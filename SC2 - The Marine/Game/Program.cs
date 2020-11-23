using System;
using System.Threading;

namespace Game
{
    class Program
    {
        static void Main()
        {
            while (true)
            {
                try
                {
                    Play();
                }
                catch (Exception e)
                {
                    Text.Message("Oh no! An error occurred!", Color.Red);
                    Console.WriteLine(e);
                }
                finally
                {
                    Text.Message("Restarting...", Color.Green);
                    Thread.Sleep(2000);
                }
            }
        }
        static void Play()
        {
            Text.Message("Set Game Speed (0 - 100)");
            Console.Write("> ");
            Color.Text(Color.Green);
            Game.GameSpeed = 100 - Convert.ToInt32(Console.ReadLine());
            Color.Reset();

            Text.Message("\nSelect Act (0 - 1):");
            Console.WriteLine("  - Act 0 (Introduction)");
            Thread.Sleep(250);
            Console.WriteLine("  - Act 1 (Zergling Infestation)");
            Console.Write("> ");
            Color.Text(Color.Green);
            if (Console.ReadLine() == "1")
            {
                Color.Reset();
                ActI.Play();
            }
            else
            {
                Color.Reset();
                Intro();
                ActI.Play();
            }
        }

        public static void Intro()
        {
            Text.Message("\nWelcome to The Marine!");
            Text.Message("This is a game based on Starcraft.");
            Console.WriteLine("\n");

            Text.Message("You are a Marine, left out in the forest.");
            Text.Message("You were out on a special mission, led by Emperor Mengsk.");
            Text.Message("Then, it didn't go as planned.");
            Text.Message("The Zerg attacked, and overwhelmed your team.");
            Text.Message("Most of the others were able to escape, but they left you here.");
            Text.Message("Now you need to get out of this forest.");
            Text.Message("You will require much skill. You think you're up for it?");
            Text.Message("(Type anything to start)", Color.Cyan);
            Console.ReadKey();
        }
    }
}
