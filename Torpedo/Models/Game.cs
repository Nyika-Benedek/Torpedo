using System;
using System.Collections.Generic;

namespace Torpedo.Models
{
    public class Game : IGame
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
            foreach (IPlayer player in Players)
            {
                if (player != CurrentPlayer)
                {
                    CurrentPlayer = player;
                    break;
                }
            }
            return CurrentPlayer;
        }

        public void Start()
        {
            RandomizeStartingPlayer();
            throw new NotImplementedException();
        }

        internal void RandomizeStartingPlayer()
        {
            _ = NextPlayer();
            if (new Random().Next(2) == 1)
            {
                _ = NextPlayer();
            }
        }

        public Game()
        {
            Winner = null;
            Turn = 0;
            CurrentPlayer = null;
            Players = new List<IPlayer>(2);
        }
    }
}