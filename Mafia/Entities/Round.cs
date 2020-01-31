using System;
using System.Collections.Generic;
using Mafia.Entities.Characters;
using Mafia.Enumerations;
using Mafia.Helpers;
using Mafia.Interfaces;

namespace Mafia.Entities
{
    public class Round : IPrintable
    {
        public int totalPlayers;
        public int numberOfMafia;
        public int numberOfDoctor;
        public int numberOfSheriff;
        public int numberOfCivilian;

        public List<Player> playersAlive = new List<Player>();

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

        public void print()
        {

            Console.WriteLine("Players Alive");
            Console.WriteLine("-------------");

            foreach (Player p in playersAlive)
            {
                p.print();
            }
        }


        public void runGameSequence()
        {
            ROLE winner = ROLE.None;
            string playerToMurder = "";
            string playerToHeal = "";
            string playerToAccuse = "";

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

        private Player getAlivePlayerByName(string name)
        {
            foreach (Player p in playersAlive)
            {
                if (p.name == name)
                    return p;
            }

            return null;
        }

        private ROLE roleHasWon()
        {

            if (numberOfMafia == 0)
                return ROLE.Civilian;
            if (numberOfCivilian + numberOfDoctor + numberOfSheriff == 0)
                return ROLE.Mafia;


            return ROLE.None;
        }

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
