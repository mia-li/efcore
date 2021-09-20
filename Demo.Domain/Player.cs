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

        [Player_EnsureBirthDate] //自定义验证，看接收到的数据是否符合我们的需求
        public DateTime DateofBirth { get; set; }
        public List<GamePlayer> GamePlayers { get; set; }//一个球员会参加多场比赛
        public int ResumeId { get; set; }
        public Resume Resume { get; set; }

        ///<summary>
        ///when creating a player,some cases you must validate 
        ///validation 直接和类属性写在一起
        ///</summary>
        public bool ValidateDateOfBirth()
        {
            //some validation logic you're gona to desgin.. 
            if (DateofBirth > DateTime.Now) return false;
            return true;
        }
    }
}
