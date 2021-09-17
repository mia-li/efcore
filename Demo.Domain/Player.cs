using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Domain
{
    public class Player
    {
        public Player()
        {
            GamePlayers = new List<GamePlayer>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateofBirth { get; set; }
        public List<GamePlayer> GamePlayers { get; set; }//一个球员会参加多场比赛
        public int ResumeId { get; set; }
        public Resume Resume { get; set; }
    }
}
