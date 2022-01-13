using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Torpedo.Interfaces;
using Torpedo.Models;

namespace Torpedo.AIModule
{
    public class PlannedAILogic : AILogic
    {
        public PlannedAILogic(IBattlefield enemyBattlefield) : base(enemyBattlefield)
        {
        }

        public override List<Coordinate> Plan()
        {
            return new List<Coordinate>();
        }
    }
}
