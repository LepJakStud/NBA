using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBA.Models
{
    internal class AnonimPlayer
    {
        public PlayerInTeam Player { get; set; }
        public AnonimPlayer(PlayerInTeam player)
        {
            Player = player;
            Experience = DateTime.Now.Year - player.Player.JoinYear.Year;
        }
        public int Experience { get; set; }
    }
}
