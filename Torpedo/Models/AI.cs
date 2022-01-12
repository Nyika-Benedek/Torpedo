using System.Collections.Generic;
using System.Linq;
using Torpedo.AIModule;
using Torpedo.Interfaces;

namespace Torpedo.Models
{
    /// <summary>
    /// This class provides an agent to act like a player
    /// </summary>
    public class AI : Player
    {
        /// <summary>
        /// There are 3 different behivaur the AI could act
        /// </summary>
        private enum PlayStyle { Random, Found, Sink }
        public List<(Coordinate, bool)> ShotHistory { get; } = new List<(Coordinate, bool)>(MainWindow.BattlefieldWidth * MainWindow.BattlefieldHeight);
        public List<IShips> Ships { get; private set; } = new List<IShips>(4);
        private PlayStyle _playStyle = PlayStyle.Random;
        private static readonly string _aiName = "AI";

        /// <summary>
        /// Constructor of <see cref="AI"/>, its sets the first behivour of the agent
        /// </summary>
        public AI() : base(_aiName)
        {
        }

        /// <summary>
        /// Calls an AI behivour based on the current agent playstyle, and the agent send an advised position to shoot at
        /// </summary>
        public void Act()
        {
            AILogic logic;
            switch (_playStyle)
            {
                case PlayStyle.Found:
                    {
                        logic = new FoundAILogic(EnemyBattlefield, ShotHistory.Last().Item1);
                        break;
                    }
                case PlayStyle.Random:
                    {
                        logic = new RandomAILogic(EnemyBattlefield);
                        break;
                    }
                default:
                    {
                        logic = new RandomAILogic(EnemyBattlefield);
                        break;
                    }
            }

            Coordinate advised;
            try
            {
                 advised = logic.Act();
            }
            catch (NowhereToShootException)
            {
                _playStyle = PlayStyle.Random;
                logic = new RandomAILogic(EnemyBattlefield);
                advised = logic.Act();
            }

            bool isHit = EnemyBattlefield.Shoot(advised);
            ShotHistory.Add((advised, isHit));

            if (isHit)
            {
                _playStyle = PlayStyle.Found;
            }
        }

        public Ship GenerateRandomShip(int size) => new Ship(AIUtils.RandomCoordinate(), new MyVector(AIUtils.Random.Next(0, 1) == 0 ? IShips.Direction.Horizontal : IShips.Direction.Vertical, size));

        public void GenerateShips()
        {
            // TODO: Get the AI to generate ships
            for (int i = 2; i <= 5; i++)
            {
                while (!BattlefieldBuilder.TryToAddShip(GenerateRandomShip(i))) ;
            }
            Ships.AddRange(BattlefieldBuilder.Ships);
        }
    }
}
