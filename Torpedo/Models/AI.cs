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
        public void Act(IBattlefield enemyBattlefield)
        {
            // TODO create an agent that decides where to shoot
        }
    }
}
