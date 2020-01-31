using System;
using Mafia.Entities;
using Mafia.Enumerations;

namespace Mafia.Entities.Characters
{
    public class MafiaMember : Player
    {
        public MafiaMember(string name) : base(name, ROLE.Mafia)
        { }


        public static void kill(Player p, Round r)
        {
            r.playersAlive.Remove(p);
        }
    }
}