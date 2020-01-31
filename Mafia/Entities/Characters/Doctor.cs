using System;
using Mafia.Enumerations;

namespace Mafia.Entities.Characters
{
        public class Doctor : Player
        {
            public Doctor(string name) : base(name, ROLE.Doctor) { }
        }
}
