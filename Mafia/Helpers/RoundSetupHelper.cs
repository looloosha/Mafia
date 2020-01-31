using System;
using System.Collections.Generic;
using Mafia.Entities;
using Mafia.Entities.Characters;
using Mafia.Enumerations;

namespace Mafia.Helpers
{
    /// <summary>
    /// A supporting class to the Round class. Provids functionality for logistics of initial game setup
    /// </summary>
    public class RoundSetupHelper
    {
        /// <summary>
        /// A list of the remaining available ROLEs. ROLEs are limited in a game.
        /// </summary>
        private List<ROLE> availableRoles = new List<ROLE>();

        /// <summary>
        /// Keeps tack of the quantity of ROLEs as they are randomly given out to players.
        /// </summary>
        private int currCivilian, currMafia, currDoctor, currSherrif = 0;

        /// <summary>
        /// The Round instance that this instance is supporting.
        /// </summary>
        public Round round;

        /// <summary>
        /// Populates availableRoles with all ROLEs in the ROLEs enum.
        /// </summary>
        public RoundSetupHelper(Round round)
        {
            availableRoles.Add(ROLE.Civilian);
            availableRoles.Add(ROLE.Mafia);
            availableRoles.Add(ROLE.Doctor);
            availableRoles.Add(ROLE.Sheriff);

            this.round = round;
        }

        /// <summary>
        /// Returns a list of Player objects after asking the user for their name and assignming them a ROLE.
        /// </summary>
        public List<Player> getPlayerNamesAndAssignRandomRoles(int totalPlayers)
        {
            List<Player> registeredPlayers = new List<Player>();

            Random roleRandomGenerator = new Random();

            int count = 1;
            while (count <= totalPlayers)
            {

                Console.WriteLine("Name for Player " + count + ":");

                ROLE currentRole = getRandomAvailableRole(roleRandomGenerator);
                Player p = null;

                if (currentRole == ROLE.Civilian)
                {
                    p = new Player(Console.ReadLine(), ROLE.Civilian);
                }
                else if (currentRole == ROLE.Doctor)
                {
                    p = new Doctor(Console.ReadLine());
                }
                else if (currentRole == ROLE.Mafia)
                {
                    p = new MafiaMember(Console.ReadLine());
                }
                else if (currentRole == ROLE.Sheriff)
                {
                    p = new Sheriff(Console.ReadLine());
                }

                registeredPlayers.Add(p);
                count++;

                Prompt.postRegistrationInformation(p.name, currentRole.ToString());
            }

            return registeredPlayers;

        }

        /// <summary>
        /// Uses a random generator to randomly distribute ROLEs still avaialbe to assign to players.
        /// </summary>
        public ROLE getRandomAvailableRole(Random roleRandomGenerator)
        {
            int r = roleRandomGenerator.Next(availableRoles.Count);
            ROLE randomRole = availableRoles[r];

            if (randomRole == ROLE.Civilian)
            {
                currCivilian++;
                if (currCivilian == round.numberOfCivilian)
                {
                    availableRoles.Remove(ROLE.Civilian);
                }
            }

            if (randomRole == ROLE.Mafia)
            {
                currMafia++;
                if (currMafia == round.numberOfMafia)
                {
                    availableRoles.Remove(ROLE.Mafia);
                }
            }

            if (randomRole == ROLE.Doctor)
            {
                currDoctor++;
                if (currDoctor == round.numberOfDoctor)
                {
                    availableRoles.Remove(ROLE.Doctor);
                }
            }

            if (randomRole == ROLE.Sheriff)
            {
                currSherrif++;
                if (currSherrif == round.numberOfSheriff)
                {
                    availableRoles.Remove(ROLE.Sheriff);
                }
            }

            return randomRole;

        }
    }
}
