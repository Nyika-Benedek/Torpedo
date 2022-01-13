using System;
using System.Collections.Generic;
using System.Text;
using Torpedo.Interfaces;
using Torpedo.Models;

namespace Torpedo.AIModule
{
    /// <summary>
    /// This class gives interface to access the AI's logics
    /// </summary>
    public abstract class AILogic
    {
        public IBattlefield EnemyBattlefield { get; private set; }

        /// <summary>
        /// Constructor of AILogic
        /// </summary>
        /// <param name="enemyBattlefield"><see cref="IBattlefield"/> enemy's bettlafield</param>
        // TODO: NI: ?!
        protected AILogic(IBattlefield enemyBattlefield) => EnemyBattlefield = enemyBattlefield ?? throw new ArgumentNullException(nameof(enemyBattlefield));

        /// <summary>
        /// The logic how the agent should make decision
        /// </summary>
        /// <returns>A list of <see cref="Coordinate"/></returns>
        // TODO: NI: Replayce plan with property
        public abstract List<Coordinate> Plan();
    }
}
