﻿using System;
using System.Collections.Generic;
using System.Text;
using Torpedo.Models;
using Torpedo.Interfaces;

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
        public RandomAILogic(IBattlefield enemyBattlefield) : base(enemyBattlefield)
        {
        }

        /// <summary>
        /// It shooting radom to try hit something
        /// </summary>
        /// <returns>It returns the recommended <see cref="Coordinate"/></returns>
        public override List<Coordinate> Plan()
        {
            var result = new List<Coordinate>();
            result.Add(AIUtils.GenerateRandomShoot(EnemyBattlefield));
            return result;
        }
    }
}
