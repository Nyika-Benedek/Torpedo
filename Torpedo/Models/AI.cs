using System;
using System.Collections.Generic;
using System.Text;

namespace Torpedo.Models
{
    public class AI : Player
    {
        private static readonly string _aiName = "AI";
        public AI() : base(_aiName)
        {
        }
        public Coordinate Act(IBattlefield enemyBattlefield)
        {
            return new Coordinate(0, 0); // TODO create an agent that decides where to shoot
        }
    }
}
