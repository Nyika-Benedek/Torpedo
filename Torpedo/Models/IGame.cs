using System;
using System.Collections.Generic;
using System.Text;

namespace Torpedo.logic
{
    interface IGame
    {
        public List<IPlayer> Players { get; set; }
        public IPlayer CurrentPlayer { get; set; }

        public int Turn { get; set; }
        public IPlayer Winner { get; set; }

    }
}
