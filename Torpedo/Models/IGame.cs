using System;
using System.Collections.Generic;
using System.Text;

namespace Torpedo.Models
{
    public interface IGame
    {
        public List<IPlayer> Players { get; set; }
        public IPlayer CurrentPlayer { get; set; }

        public int Turn { get; set; }
        public IPlayer Winner { get; set; }

        abstract void Start();

        abstract bool IsEnded();

        abstract IPlayer NextPlayer();
    }
}
