using System;
using Mafia.Entities;
using Mafia.Interfaces;

namespace Mafia.Helpers
{
    public static class Prompt
    {
        public static void welcome()
        {
            Console.WriteLine("Welcome to MafiaPlus! Press any key to begin...");
            Console.ReadKey();
            Console.Clear();
        }

        public static void howManyPlayers(out int numberOfPlayers)
        {
            Console.WriteLine("How many people are playing in this round?");


            while (!Int32.TryParse(Console.ReadLine(), out numberOfPlayers))
            {
                Console.WriteLine("Invalid input. Please enter a valid number of players!");
            }
            Console.Clear();
        }

        public static void quit(out bool hasQuit)
        {
            hasQuit = false;

            Console.WriteLine("Press Q to quit or any other key to get another round going!");
            string quitInput = Console.ReadLine();

            if (quitInput == "q" || quitInput == "Q")
            {
                hasQuit = true;
            }

            Console.Clear();
        }

        public static void numberOfMafiaForRound(out int numberOfMafia)
        {
            Console.WriteLine("Thanks! Let's get started. A few more questions and we'll start playing a game of Mafia...");

            Console.WriteLine("Amount of Mafia(s)?");
            while (!Int32.TryParse(Console.ReadLine(), out numberOfMafia))
            {
                Console.WriteLine("Invalid input. Please enter a valid number of mafia!");
            }
        }

        public static void numberOfDoctorsForRound(out int numberOfDoctor)
        {
            Console.WriteLine("Amount of Doctor(s)?");
            while (!Int32.TryParse(Console.ReadLine(), out numberOfDoctor))
            {
                Console.WriteLine("Invalid input. Please enter a valid number of Doctors!");
            }
        }

        public static void numberOfSherrifsForRound(out int numberOfSheriff)
        {
            Console.WriteLine("Amount of Sheriff(s)?");
            while (!Int32.TryParse(Console.ReadLine(), out numberOfSheriff))
            {
                Console.WriteLine("Invalid input. Please enter a valid number of Sherrifs!");
            }
        }

        public static void introducePlayerNaming()
        {
            Console.Clear();
            Console.WriteLine("Next we'll need the name of each player:");
        }

        public static void postRegistrationInformation(string name, string roleName)
        {
            Console.WriteLine("Thanks " + name + "! You're role is " + roleName +
                  ". When you're ready to pass the device to the next person, press any key to clear the screen so that they don't see your role!");

            Console.ReadKey();
            Console.Clear();
        }

        public static void passToModerator()
        {
            Console.WriteLine("Thanks! We're all set and ready to start a game of Mafia!. Give the device back to the MODERATOR");
            Console.WriteLine("Moderator, press any key to commence the game...");

            Console.ReadKey();
            Console.Clear();
        }

        public static void goToSleepAndMafiaChooseVictim(out string playerToMurder, IPrintable round)
        {
            Console.WriteLine("Announce for the town to go to sleep. Then tell Mafia to awake and choose player to murder.");

            round.print();

            Console.WriteLine("Type the name of the person that the mafia murdered:");
            playerToMurder = Console.ReadLine();
        }

        public static void invalidPlayerToKill(out string playerToMurder)
        {
            Console.WriteLine("That is not a valid player to kill. Retry!");

            playerToMurder = Console.ReadLine();
        }

        public static void doctorSelectSomoneToHeal(int numberOfDoctor, Round round, out string playerToHeal)
        {
            Console.Clear();
            playerToHeal = "";

            if (numberOfDoctor > 0)
            {
                Console.WriteLine("Announce for the mafia to go back to sleep and the doctor to awake and to choose a player to heal");

                round.print();

                Console.WriteLine("Type the name of the person that the doctor healed:");

                playerToHeal = Console.ReadLine();

                while (!round.isValidPlayerAlive(playerToHeal))
                {
                    Console.WriteLine("That is not a valid player to heal. Retry!");

                    playerToHeal = Console.ReadLine();
                }

                Console.Clear();
            }
        }

        public static void sherrifSelectSomeoneToAccuse(int numberOfSheriff, Round round, out string playerToAccuse)
        {
            playerToAccuse = "";

            if (numberOfSheriff > 0)
            {

                Console.WriteLine("Announce for the doctor to go back to sleep and the sheriff to awake and to choose a player to accuse");

                round.print();

                Console.WriteLine("Type the name of the person that the sheriff accused:");

                playerToAccuse = Console.ReadLine();

                while (!round.isValidPlayerAlive(playerToAccuse))
                {
                    Console.WriteLine("That is not a valid player to accuse. Retry!");

                    playerToAccuse = Console.ReadLine();
                }

                Console.Clear();
            }
        }

        public static void postNightSummary(string playerToMurder, string playerToHeal, string playerToAccuse)
        {
            Console.WriteLine("The mafia murdered " + playerToMurder);
            Console.WriteLine("The doctor healed " + playerToHeal);
            Console.WriteLine("The sheriff accused " + playerToAccuse);
        }

        public static void civiliansChooseSomeoneToHang(Round round, out string playerToHang)
        {
            Console.WriteLine("Towns-people must now choose someone to hang");

            round.print();

            Console.WriteLine("Type the name of the player that the towns-people want to hang:");

            playerToHang = Console.ReadLine();

            while (!round.isValidPlayerAlive(playerToHang))
            {
                Console.WriteLine("That is not a valid player to hang. Retry!");

                playerToHang = Console.ReadLine();
            }
        }
    }
}
