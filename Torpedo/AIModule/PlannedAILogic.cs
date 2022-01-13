using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Torpedo.Interfaces;
using Torpedo.Models;

namespace Torpedo.AIModule
{
    /// <summary>
    /// This class helps to store previously planned decision
    /// </summary>
    public class PlannedAILogic : AILogic
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="enemyBattlefield"><see cref="IBattlefield"/> enemy's battlefield</param>
        public PlannedAILogic(IBattlefield enemyBattlefield) : base(enemyBattlefield)
        {
        }

        /// <summary>
        /// The conclusion of where could be the enemy's hips
        /// </summary>
        /// <returns>List of <see cref="Coordinate"/>, where the enemy ships could be located</returns>
        public override List<Coordinate> Plan()
        {
            return new List<Coordinate>();
        }
    }
}
