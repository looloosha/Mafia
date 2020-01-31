using System;
using Mafia.Entities;
using Mafia.Enumerations;

namespace Mafia.Entities.Characters
{
    public class Sheriff : Player
    {
        /// <summary>
        /// Initializes the ROLE from the Player base class.
        /// </summary>
        public Sheriff(string name) : base(name, ROLE.Sheriff)
        { }
    }
}
