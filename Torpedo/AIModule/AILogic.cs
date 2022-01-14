using System;
using System.Collections.Generic;
using System.Text;
using Torpedo.Interfaces;
using Torpedo.Models;

namespace Torpedo.AIModule
{
    /// <summary>
    /// This class gives class abstraction for the AI's logics.
    /// </summary>
    public abstract class AILogic
    {
        /// <summary>
        /// Stores the enemy <see cref="Battlefield"/> to check the known coordinates.
        /// </summary>
        public IBattlefield EnemyBattlefield { get; private set; }

        /// <summary>
        /// Constructor of AILogic.
        /// </summary>
        /// <param name="enemyBattlefield">The enemy's <see cref="Battlefield"/> containing previous shots.</param>
        protected AILogic(IBattlefield enemyBattlefield) => EnemyBattlefield = enemyBattlefield ?? throw new ArgumentNullException(nameof(enemyBattlefield));

        /// <summary>
        /// The logic how the agent should make decision.
        /// </summary>
        /// <returns>A list of <see cref="Coordinate"/>.</returns>
        public abstract ICollection<Coordinate> Plan();
    }
}
