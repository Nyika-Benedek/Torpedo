using System;
using System.Collections.Generic;
using System.Text;
using Torpedo.Interfaces;
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
        /// <param name="enemyBattlefield"><see cref="IBattlefield"/> enemy's battlefield</param>
        public RandomAILogic(IBattlefield enemyBattlefield) : base(enemyBattlefield)
        {
        }

        /// <summary>
        /// The conclusion of where could be the enemy's hips
        /// </summary>
        /// <returns>List of <see cref="Coordinate"/>, where the enemy ships could be located</returns>
        public override List<Coordinate> Plan()
        {
            var result = new List<Coordinate>();
            result.Add(AIUtils.GenerateRandomShoot(EnemyBattlefield));
            return result;
        }
    }
}
