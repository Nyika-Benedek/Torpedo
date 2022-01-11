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
        public List<IShips> Ships { get => BattlefieldBuilder.Ships; }
        private PlayStyle _playStyle = PlayStyle.Random;
        private static readonly string _aiName = "AI";

        /// <summary>
        /// Constructor of <see cref="AI"/>, its sets the first behivour of the agent
        /// </summary>
        public AI() : base(_aiName)
        {
        }

        /// <summary>
        /// Build the <see cref="Battlefield"/> of the player
        /// </summary>
        /// BattlefieldBuilder is kept.
        public new void BuildBattlefield() => Battlefield = BattlefieldBuilder.Build();

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
                        logic = new FoundAILogic(this, ShotHistory.Last().Item1);
                        break;
                    }
                case PlayStyle.Random:
                    {
                        logic = new RandomAILogic(this);
                        break;
                    }
                default:
                    {
                        logic = new RandomAILogic(this);
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
                logic = new RandomAILogic(this);
                advised = logic.Act();
            }

            bool isHit = EnemyBattlefield.Shoot(advised);
            ShotHistory.Add((advised, isHit));

            if (isHit)
            {
                _playStyle = PlayStyle.Found;
            }
        }
    }
}
