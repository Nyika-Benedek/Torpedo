using System;
using System.Collections.Generic;
using System.Text;
using Torpedo.Interfaces;
using Torpedo.Models;

namespace Torpedo.AIModule
{
    /// <summary>
    /// This class describe the default behivaour of the AI.
    /// </summary>
    public class RandomAILogic : AILogic
    {
        /// <summary>
        /// Contructor.
        /// </summary>
        /// <param name="enemyBattlefield">The enemy's <see cref="Battlefield"/> containing previous shots.</param>
        public RandomAILogic(IBattlefield enemyBattlefield) : base(enemyBattlefield)
        {
        }

        /// <summary>
        /// Generate a random <see cref="Coordinate"/> to shoot at.
        /// </summary>
        /// <returns>List of <see cref="Coordinate"/>, containing a single coordinate.</returns>
        public override ICollection<Coordinate> Plan()
        {
            var result = new List<Coordinate>();
            result.Add(AIUtils.GenerateRandomShoot(EnemyBattlefield));
            return result;
        }
    }
}
