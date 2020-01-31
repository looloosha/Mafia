using System;
using Mafia.Entities;
using Mafia.Enumerations;

namespace Mafia.Entities.Characters
{
    public class Sheriff : Player
    {
        public Sheriff(string name) : base(name, ROLE.Sheriff)
        { }
    }
}
