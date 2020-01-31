using System;
using Mafia.Entities;
using Mafia.Enumerations;

namespace Mafia.Entities.Characters
{
    public class MafiaMember : Player
    {
        /// <summary>
        /// Initializes the ROLE from the Player base class.
        /// </summary>
        public MafiaMember(string name) : base(name, ROLE.Mafia)
        { }

        /// <summary>
        /// Provides Mafia their special ability to kill other players at night.
        /// </summary>
        public static void kill(Player p, Round r)
        {
            r.playersAlive.Remove(p);
        }
    }
}