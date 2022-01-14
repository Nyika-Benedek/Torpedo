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
    /// This class does not make decision.
    /// </summary>
    public class PlannedAILogic : AILogic
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="enemyBattlefield">The enemy's <see cref="Battlefield"/> containing previous shots.</param>
        public PlannedAILogic(IBattlefield enemyBattlefield) : base(enemyBattlefield)
        {
        }

        /// <summary>
        /// No operation.
        /// </summary>
        /// <returns> empty List of <see cref="Coordinate"/>.</returns>
        public override ICollection<Coordinate> Plan()
        {
            return new List<Coordinate>();
        }
    }
}
