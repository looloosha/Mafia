using System;
using System.Collections.Generic;
using Mafia.Entities.Characters;
using Mafia.Enumerations;
using Mafia.Helpers;
using Mafia.Interfaces;

namespace Mafia.Entities
{
    /// <summary>
    /// The Round class is in charge of the progression of one round of the game Mafia.
    /// </summary>
    public class Round : IPrintable
    {
        /// <summary>
        /// The total amount of players in a given round.
        /// </summary>
        public int totalPlayers;

        /// <summary>
        /// The amount of Mafia players in the game as specified by the user.
        /// </summary>
        public int numberOfMafia;

        /// <summary>
        /// The amount of Doctor players in the game as specified by the user.
        /// </summary>
        public int numberOfDoctor;

        /// <summary>
        /// The amount of Sheriff players in the game as specified by the user.
        /// </summary>
        public int numberOfSheriff;

        /// <summary>
        /// The amount of Civilian players in the game as calculated by the program after other roles have been specified by user.
        /// </summary>
        public int numberOfCivilian;

        /// <summary>
        /// A list of the players remaining in the game using the top level Player object.
        /// </summary>
        public List<Player> playersAlive = new List<Player>();


        /// <summary>
        /// Uses the RoundSetupHelper to setup and begin a round.
        /// </summary>
        public Round(int totalPlayers)
        {
            this.totalPlayers = totalPlayers;

            Prompt.numberOfMafiaForRound(out numberOfMafia);
            Prompt.numberOfDoctorsForRound(out numberOfDoctor);
            Prompt.numberOfSherrifsForRound(out numberOfSheriff);

            numberOfCivilian = totalPlayers - numberOfDoctor - numberOfSheriff - numberOfMafia;

            Prompt.introducePlayerNaming();

            RoundSetupHelper logisticsHelper = new RoundSetupHelper(this);

            playersAlive = logisticsHelper.getPlayerNamesAndAssignRandomRoles(totalPlayers);

            Prompt.passToModerator();
        }

        /// <summary>
        /// Inherited from the IPrintable interface. This implementation prints out the remaining players in the round.
        /// </summary>
        public void print()
        {
            Console.WriteLine("Players Alive");
            Console.WriteLine("-------------");

            foreach (Player p in playersAlive)
            {
                p.print();
            }
        }

        /// <summary>
        /// Provides the core game loop functionality of the game and changes the state of the game based on input from user.
        /// </summary>
        public void runGameSequence()
        {
            ROLE winner = ROLE.None;
            string playerToMurder = "";
            string playerToHeal = "";
            string playerToAccuse = "";

            //ROLE returned by roleHasWon() will remain ROLE.None until a role has been designated a winner.
            while (roleHasWon() == ROLE.None)
            {
                Prompt.goToSleepAndMafiaChooseVictim(out playerToMurder, this);
                while (!isValidPlayerAlive(playerToMurder))
                {
                    Prompt.invalidPlayerToKill(out playerToMurder);
                }
                Prompt.doctorSelectSomoneToHeal(numberOfDoctor, this, out playerToHeal);
                Prompt.sherrifSelectSomeoneToAccuse(numberOfSheriff, this, out playerToAccuse);

                Prompt.postNightSummary(playerToMurder, playerToHeal, playerToAccuse);

                //Doctor did not heal player that was murdered.
                if (playerToMurder != playerToHeal)
                {
                    Player murdered = getAlivePlayerByName(playerToMurder);
                    MafiaMember.kill(murdered, this);
                    if (murdered.role == ROLE.Mafia)
                    {
                        numberOfMafia--;
                    }
                    else if (murdered.role == ROLE.Doctor)
                    {
                        numberOfDoctor--;
                    }
                    else if (murdered.role == ROLE.Sheriff)
                    {
                        numberOfSheriff--;
                    }
                    else if (murdered.role == ROLE.Civilian)
                    {
                        numberOfCivilian--;
                    }
                }

                string playerToHang = "";

                Prompt.civiliansChooseSomeoneToHang(this, out playerToHang);

                Player hung = getAlivePlayerByName(playerToHang);

                if (hung.role == ROLE.Mafia)
                {
                    numberOfMafia--;
                }
                else if (hung.role == ROLE.Doctor)
                {
                    numberOfDoctor--;
                }
                else if (hung.role == ROLE.Sheriff)
                {
                    numberOfSheriff--;
                }
                else if (hung.role == ROLE.Civilian)
                {
                    numberOfCivilian--;
                }

                playersAlive.Remove(getAlivePlayerByName(playerToHang));

                winner = roleHasWon();

                playerToMurder = "";
                playerToHeal = "";
                playerToAccuse = "";
            }

            Console.WriteLine(winner.ToString() + " wins!");
        }

        /// <summary>
        /// Return the Player object given a name.
        /// </summary>
        private Player getAlivePlayerByName(string name)
        {
            foreach (Player p in playersAlive)
            {
                if (p.name == name)
                    return p;
            }

            return null;
        }

        /// <summary>
        /// Determines if Civilian or Mafia has won based on remaining player numbers.
        /// </summary>
        private ROLE roleHasWon()
        {
            if (numberOfMafia == 0)
                return ROLE.Civilian;
            if (numberOfCivilian + numberOfDoctor + numberOfSheriff == 0)
                return ROLE.Mafia;


            return ROLE.None;
        }

        /// <summary>
        /// Utility function to ensure that the player being queried is a valid player.
        /// </summary>
        public bool isValidPlayerAlive(string name)
        {
            bool result = false;

            foreach (Player p in playersAlive)
            {
                if (p.name == name)
                    result = true;
            }

            return result;
        }

    }
}
