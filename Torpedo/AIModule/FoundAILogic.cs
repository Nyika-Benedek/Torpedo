using System;
using System.Collections.Generic;
using System.Text;
using Torpedo.Interfaces;
using Torpedo.Models;

namespace Torpedo.AIModule
{
    /// <summary>
    /// This class describe the behivaour of the AI, when it tires to hit something at random.
    /// </summary>
    public class FoundAILogic : AILogic
    {
        /// <summary>
        /// Constructor of FoundAILogic.
        /// </summary>
        /// <param name="enemyBattlefield">The enemy's <see cref="Battlefield"/> containing previous shots.</param>
        /// <param name="focus"> <see cref="Coordinate"/> where the part of a <see cref="Ship"/> was hit successfully.</param>
        public FoundAILogic(IBattlefield enemyBattlefield, Coordinate focus) : base(enemyBattlefield)
        {
            if (!AIUtils.IsInField(focus))
            {
                throw new ArgumentException("no shooting outside the battlefield");
            }
            Focus = focus;
        }

        /// <summary>
        /// The <see cref="Coordinate"/> which the AI will shoot around.
        /// <para>This is the position, where the AI randomly hit a <see cref="Ship"/>.<para>
        /// </summary>
        public Coordinate Focus { get; private set; }

        /// <summary>
        /// Tries to hit the four possible neighbours of the hit  <see cref="Coordinate"/>.
        /// </summary>
        /// <returns> the recommended <see cref="Coordinate"/>.</returns>
        public override ICollection<Coordinate> Plan()
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