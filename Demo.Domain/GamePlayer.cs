using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Domain
{
    public class GamePlayer
    {
        public int PlayerId { get; set; }
        public int GameId { get; set; }

        //加Game & Player 我的理解是为了生成后续的外键 FK_GameId & FK_PlayerId
        public Game Game { get; set; }
        public Player Player { get; set; }
    }
}
