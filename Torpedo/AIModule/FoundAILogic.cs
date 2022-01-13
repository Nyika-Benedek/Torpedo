using System;
using System.Collections.Generic;
using System.Text;
using Torpedo.Interfaces;
using Torpedo.Models;

namespace Torpedo.AIModule
{
    /// <summary>
    /// This class describe the behivaour of the AI, when its hits something on random
    /// </summary>
    public class FoundAILogic : AILogic
    {
        /// <summary>
        /// Constructor of FoundAILogic
        /// </summary>
        /// <param name="enemyBattlefield">The enemy's <see cref="Battlefield"/> containing previous shots</param>
        /// <param name="focus">Coordinate where the part of a ship was hit successfully.</param>
        public FoundAILogic(IBattlefield enemyBattlefield, Coordinate focus) : base(enemyBattlefield)
        {
            if (!AIUtils.IsInField(focus))
            {
                throw new ArgumentException("no shooting outside the battlefield");
            }
            
            Focus = focus;
        }

        /// <summary>
        /// The <see cref="Coordinate"/> which the AI will shoot around
        /// <para>This is a position, where the AI randomly hit a ship.<para>
        /// </summary>
        public Coordinate Focus { get; private set; }

        /// <summary>
        /// Tries to hit the four possible neighbours of the hit coordinate.
        /// </summary>
        /// <param name="battlefield">The used <see cref="Battlefield"/></param>
        /// <returns>It returns the recommended <see cref="Coordinate"/>.</returns>
        public override List<Coordinate> Plan()
        {
            List<Coordinate> result = new List<Coordinate>();

            foreach (Directions direction in Enum.GetValues(typeof(Directions)))
            {
                Coordinate proposed = Focus.Shift(direction);
                if (!AIUtils.IsCellShot(EnemyBattlefield, proposed) && AIUtils.IsInField(proposed))
                {
                    result.Add(proposed);
                }
            }
            return result;
        }
    }
}