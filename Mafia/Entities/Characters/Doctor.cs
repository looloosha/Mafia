using System;
using Mafia.Enumerations;

namespace Mafia.Entities.Characters
{
        
        public class Doctor : Player
        {

        /// <summary>
        /// Initializes the ROLE from the Player base class.
        /// </summary>
        public Doctor(string name) : base(name, ROLE.Doctor) { }
        }
}
