using System;
using System.Collections.Generic;
using System.Text;
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
        /// <param name="aI">The used <see cref="AI"/> agent</param>
        /// <param name="focus">Coordinate where the part of a ship was hit successfully.</param>
        public FoundAILogic(AI aI, Coordinate focus) : base(aI) => Focus = focus;
        public Coordinate Focus { get; private set; }
        /// <summary>
        /// Tries to hit the four possible neighbours of the hit coordinate.
        /// </summary>
        /// <param name="battlefield">The used <see cref="Battlefield"/></param>
        /// <returns>It returns the recommended <see cref="Coordinate"/>.</returns>
        public override Coordinate Act()
        {
            Coordinate proposed;

            foreach (Directions direction in Enum.GetValues(typeof(Directions)))
            {
                proposed = Focus.Shift(direction);
                if (!AIUtils.IsCellShot(AI.EnemyBattlefield, proposed) && AIUtils.IsInField(proposed))
                {
                    return proposed;
                }
            }
            throw new NowhereToShootException();
        }
    }

    /// <summary>
    /// Own exception the indeicate nowhere left to shoot exception
    /// <para>Throw this exception, when the whole battlefield is full and nowhere left to shoot</para>
    /// </summary>
    [Serializable]
    public class NowhereToShootException : Exception
    {
        public NowhereToShootException() : base() { }
        public NowhereToShootException(string message) : base(message) { }
        public NowhereToShootException(string message, Exception inner) : base(message, inner) { }
        protected NowhereToShootException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context)
        { }
    }
}