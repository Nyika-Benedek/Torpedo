using System;
using System.Collections.Generic;
using System.Text;
using Torpedo.Models;

namespace Torpedo.AIModule
{
    /// <summary>
    /// This class describe the behivaour of the AI, when there is no sign where could be ships
    /// </summary>
    public class RandomAILogic : AILogic
    {
        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="aI">Used <see cref="AI"/> agent</param>
        public RandomAILogic(AI aI) : base(aI)
        {
        }

        /// <summary>
        /// It shooting radom to try hit something
        /// </summary>
        /// <returns>It returns the recommended <see cref="Coordinate"/></returns>
        public override Coordinate Act()
        {
            return AIUtils.GenerateRandomShoot(AI.EnemyBattlefield);
        }
    }
}
