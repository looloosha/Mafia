﻿using System;
using Mafia.Entities;
using Mafia.Helpers;

namespace Mafia
{
    class Program
    {
        static void Main(string[] args)
        {
            Prompt.welcome();

            bool hasQuit = false;

            while (!hasQuit)
            {
                Prompt.howManyPlayers(out int numberOfPlayers);

                Round round = new Round(numberOfPlayers);

                round.runGameSequence();

                Prompt.quit(out hasQuit);
            }
        }
    }
}
