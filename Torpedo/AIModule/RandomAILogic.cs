using System;
using System.Collections.Generic;
using System.Text;
using Torpedo.Models;

namespace Torpedo.AIModule
{
    public class RandomAILogic : AILogic
    {
        public RandomAILogic(AI aI) : base(aI)
        {
        }

        public override Coordinate Act()
        {
            return AIUtils.GenerateRandomShoot(AI.EnemyBattlefield);
        }
    }
}
