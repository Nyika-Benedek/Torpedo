using System;
using System.Collections.Generic;
using System.Text;

namespace Torpedo.Models
{
    class Game : IGame
    {
        public List<IPlayer> Players { get; set; }
        public IPlayer CurrentPlayer { get; set; }
        public int Turn { get; set; }
        public IPlayer Winner { get; set; }

        public bool IsEnded()
        {
            throw new NotImplementedException();
        }

        public IPlayer NextPlayer()
        {
            throw new NotImplementedException();
        }

        public void Start()
        {
            throw new NotImplementedException();
        }
    }
}
