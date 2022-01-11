using System.Collections.Generic;
using Torpedo.AIModule;
using Torpedo.Interfaces;
using System.Linq;

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
        private enum Playstyle { Random, Found, Sink}
        public List<(Coordinate, bool)> ShotHistory { get; private set; }

        private Playstyle _playStyle;
        private static readonly string _aiName = "AI";

        /// <summary>
        /// Constructor of <see cref="AI"/>, its sets the first behivour of the agent
        /// </summary>
        public AI() : base(_aiName)
        {
            _playStyle = Playstyle.Random;
            ShotHistory = new List<(Coordinate, bool)>(MainWindow.BattlefieldWidth * MainWindow.BattlefieldHeight);
        }

        /// <summary>
        /// Calls an AI behivour based on the current agent playstyle, and the agent send an advised position to shoot at
        /// </summary>
        public void Act()
        {
            AILogic logic;
            switch (_playStyle)
            {
                case Playstyle.Found:
                    {
                        logic = new FoundAILogic(this, ShotHistory.Last().Item1);
                        break;
                    }
                case Playstyle.Random:
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
            catch (NowhereToShootException ex)
            {
                _playStyle = Playstyle.Random;
                logic = new RandomAILogic(this);
                advised = logic.Act();
            }

            bool isHit = EnemyBattlefield.Shoot(advised);
            ShotHistory.Add((advised, isHit));

            if (isHit)
            {
                _playStyle = Playstyle.Found;
            }
        }
    }
}
