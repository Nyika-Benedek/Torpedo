using System.Collections.Generic;
using Torpedo.AIModule;
using Torpedo.Interfaces;
using System.Linq;

namespace Torpedo.Models
{
    public class AI : Player
    {
        private enum Playstyle { Random, Found, Sink}
        public List<(Coordinate, bool)> ShotHistory { get; private set; }

        private Playstyle _playStyle;
        private static readonly string _aiName = "AI";
        public AI() : base(_aiName)
        {
            _playStyle = Playstyle.Random;
            ShotHistory = new List<(Coordinate, bool)>(MainWindow.BattlefieldWidth * MainWindow.BattlefieldHeight);
        }
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
