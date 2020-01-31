using System;
using Mafia.Enumerations;
using Mafia.Interfaces;

namespace Mafia.Entities.Characters
{
    public class Player : IPrintable
    {
        public string name;
        public ROLE role;

        public Player(string name, ROLE role)
        {

            this.name = name;
            this.role = role;

        }

        public void print()
        {
            Console.WriteLine(name + "  |  " + role.ToString());
        }

    }
}
