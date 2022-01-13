using System.Collections.Generic;
using Torpedo.Models;

namespace Torpedo.Interfaces
{
    public interface IGame
    {
        public GameState State { get; set; }

        // TODO: NI: replace Player with methods
        public List<IPlayer> Players { get; }
        public IPlayer CurrentPlayer { get; }

        public int Turn { get; }
        public IPlayer Winner { get; }

        abstract void Start();

        abstract bool IsEnded();

        abstract IPlayer NextPlayer();
        public abstract void AddPlayer(IPlayer player);
        void RandomizeStartingPlayer();
        void AddTurn();
    }
}
