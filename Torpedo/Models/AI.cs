using Torpedo.Interfaces;

namespace Torpedo.Models
{
    public class AI : Player
    {
        private enum Playstyle { Random, Found, Sink}
        private Playstyle _playstyle;
        private static readonly string _aiName = "AI";
        public AI() : base(_aiName)
        {
            _playstyle = Playstyle.Random;
        }
        public void Act()
        {
            switch (_playstyle)
            {
                case Playstyle.Random:
                {
                    EnemyBattlefield.Shoot(AIUtils.GenerateRandomShoot(EnemyBattlefield));
                    break;
                }
                default:
                {
                    break;
                }
            }
            // TODO create an agent that decides where (and how playstyles change) to shoot
        }
    }
}
