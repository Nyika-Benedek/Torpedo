using System;
using System.Collections.Generic;
using System.Text;
using Torpedo.Models;

namespace Torpedo.AIModule
{
    public class FoundAILogic : AILogic
    {
        public FoundAILogic(AI aI, Coordinate focus) : base(aI)
        {
            Focus = focus;
        }
        public Coordinate Focus { get; private set; }
        /// <summary>
        /// It gets a coordinate and tries to hit the four possible neighbours.
        /// </summary>
        /// <param name="battlefield">It defines the whole playground.</param>
        /// <returns>It returns the coordinate in a form of (x,y) values.</returns>
        public override Coordinate Act()
        {
            Coordinate proposed;

            foreach (Directions direction in Enum.GetValues(typeof(Directions)))
            {
                proposed = Focus.Shift(direction);
                if (!AIUtils.IsCellShooted(AI.EnemyBattlefield, proposed) && AIUtils.IsInField(proposed))
                {
                    return proposed;
                }
            }
            throw new NowhereToShootException();
        }
    }

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