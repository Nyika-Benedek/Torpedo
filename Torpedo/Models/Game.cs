using System;
using System.Collections.Generic;
using Torpedo.Interfaces;

namespace Torpedo.Models
{
    /// <summary>
    /// Represents the states of the game
    /// </summary>
    public enum GameState { NotStarted, ShipPlacement, Battle, Finished }

    /// <summary>
    /// Represents the game as an object
    /// </summary>
    public class Game : IGame
    {
        public GameState State { get; set; } = GameState.NotStarted;
        public List<IPlayer> Players { get; private set; } = new List<IPlayer>(2);
        public IPlayer CurrentPlayer { get; private set; }
        public int Turn { get; private set; } = 0;
        public IPlayer Winner { get; private set; } = null;

        private int _playerIndex = -1;
        private const int _maxPoints = 14;

        /// <summary>
        /// Is the game are in win condition
        /// </summary>
        /// <returns>bool: Yes, if its in win condition, no otherwise</returns>
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

        /// <summary>
        /// Set the current player into the next player
        /// </summary>
        /// <returns>Returns the next <see cref="IPlayer"/></returns>
        public IPlayer NextPlayer()
        {
            _playerIndex++;
            return CurrentPlayer = Players.ToArray()[_playerIndex % 2];
        }

        /// <summary>
        /// Set the necessary values, then change into battle state
        /// </summary>
        public void Start()
        {
            IPlayer playerA = NextPlayer();
            IPlayer playerB = NextPlayer();

            playerA.BuildBattlefield();
            playerB.BuildBattlefield();

            playerA.SetEnemyBattlefield(playerB.Battlefield);
            playerB.SetEnemyBattlefield(playerA.Battlefield);
            State = GameState.Battle;
        }

        /// <summary>
        /// Randomize next player, used in the start
        /// </summary>
        public void RandomizeStartingPlayer()
        {
            _ = NextPlayer();
            if (new Random().Next(2) == 1)
            {
                _ = NextPlayer();
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public Game() => State = GameState.ShipPlacement;

        /// <summary>
        /// Add player to the game
        /// </summary>
        /// <param name="player"><see cref="IPlayer"/>: player</param>
        public void AddPlayer(IPlayer player)
        {
            if (Players.Count >= 2)
            {
                throw new OverflowException("trying to add too many players to the game");
            }
            Players.Add(player);
        }

        /// <summary>
        /// Increase turn counter
        /// </summary>
        public void AddTurn()
        {
            Turn++;
        }
    }
}