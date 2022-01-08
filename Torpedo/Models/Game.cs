﻿using System;
using System.Collections.Generic;

namespace Torpedo.Models
{
    public class Game : IGame
    {
        public List<IPlayer> Players { get; private set; }
        public IPlayer CurrentPlayer { get; private set; }
        public int Turn { get; private set; }
        public IPlayer Winner { get; private set; }

        private int _playerIndex = 0;
        private const int _maxPoints = 14;

        public bool IsEnded()
        {
            foreach (IPlayer player in Players)
            {
                if (player.Points == _maxPoints)
                {
                    Winner = player;
                    return true;
                }
            }
            return false;
        }

        public IPlayer NextPlayer()
        {
            _playerIndex++;
            return Players.ToArray()[_playerIndex % 2];
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

        public void AddPlayer(IPlayer player)
        {
            if (Players.Count >= 2)
            {
                throw new OverflowException("trying to add too many players to the game");
            }
            Players.Add(player);
        }

    }
}