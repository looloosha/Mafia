using System;
using Mafia.Enumerations;
using Mafia.Interfaces;

namespace Mafia.Entities.Characters
{
    /// <summary>
    /// The base class representing each player in the game.
    /// </summary>
    public class Player : IPrintable
    {
        /// <summary>
        /// Used an an identifier for each player.
        /// </summary>
        public string name;

        /// <summary>
        /// Tracks the role associated to the player in the context of the game.
        /// </summary>
        public ROLE role;

        public Player(string name, ROLE role)
        {

            this.name = name;
            this.role = role;

        }

        /// <summary>
        /// Prints out a summary of the Player object including their name and ROLE.
        /// </summary>
        public void print()
        {
            Console.WriteLine(name + "  |  " + role.ToString());
        }

    }
}
