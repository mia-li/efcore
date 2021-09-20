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

        //[Player_EnsureBirthDate] //自定义验证，看接收到的数据是否符合我们的需求
        public DateTime DateofBirth { get; set; }
        public List<GamePlayer> GamePlayers { get; set; }//一个球员会参加多场比赛
        public int ResumeId { get; set; }
        public Resume Resume { get; set; }
    }
}
