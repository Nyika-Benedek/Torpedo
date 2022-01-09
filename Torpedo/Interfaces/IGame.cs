using System;
using System.Collections.Generic;
using System.Text;

namespace Torpedo.Models
{
    public interface IGame
    {
        public GameState State { get; set; }
        public List<IPlayer> Players { get; }
        public IPlayer CurrentPlayer { get; }

        public int Turn { get; }
        public IPlayer Winner { get; }

        abstract void Start();

        abstract bool IsEnded();

        abstract IPlayer NextPlayer();
        public abstract void AddPlayer(IPlayer player);
    }
}
