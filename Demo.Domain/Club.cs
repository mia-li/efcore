using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Demo.Domain
{
    public class Club
    {
        public Club()
        {
            Players = new List<Player>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        
        [Column(TypeName="date")]
        public DateTime DateofEstablishment { get; set; }

        public string History { get; set; }

        public League League { get; set; } 
        //一个联赛对应多个足球队，代码体现即不同足球队可能对应同一个联赛ID
        public List<Player> Players { get; set; } //一个足球队对应多个球员
        //用list时注意要加构造函数进行初始化，防止抛出空指针异常
    }
    /*
     联赛--(1：n)--足球队--(1:n)--队员--(m:n)--比赛 
     （队员会参加很多比赛[1:m]，一场比赛有很多队员会参加[n:1]，多对多关系[m:n]）
     多对多关系ef core不能直接实现，故借助中间表 gameplayer
     联赛--(1：n)--足球队--(1:n)--队员--(1:m)--GamePlayer--(n:1)--比赛
     */
}
