using System;
using System.Collections.Generic;
using Torpedo.AIModule;
using Torpedo.Interfaces;

namespace Torpedo.Models
{
    /// <summary>
    /// This class helps the front-end to add <see cref="Ship"/>s to each <see cref="Player"/>s <see cref="Battlefield"/>.
    /// <para>The ships need to be displayed while the players are placing them.</para>
    /// </summary>
    public class BattlefieldBuilder
    {
        public IList<IShips> Ships { get; } = new List<IShips>(4);

        /// <summary>
        /// Finalizes the Battlefiled, where ship positions are inaccessable.
        /// </summary>
        /// <returns>new <see cref="Battlefield"/> filled with ships.</returns>
        public Battlefield Build() => new Battlefield(Ships);

        /// <summary>
        /// Add a ship to the builder object.
        /// </summary>
        /// <param name="newShip"><see cref="IShips"/> ship to add.</param>
        /// <returns><see cref="bool"/> True if successful, false if it would violate rules.</returns>
        public bool TryToAddShip(IShips newShip)
        {
            try
            {
                CheckForCollision(newShip);
                IsShipWithinBattlefield(newShip);
            }
            catch (InvalidPlacementException)
            {
                return false;
            }
            Ships.Add(newShip);
            return true;
        }

        /// <summary>
        /// Checks if the ship would be colliding upon placement.
        /// </summary>
        /// <param name="newShip"><see cref="IShips"/> ship to check.</param>
        private void CheckForCollision(IShips newShip)
        {
            var shipPositions = new List<Coordinate>(14);

            // list all the occupied places
            foreach (var ship in Ships)
            {
                shipPositions.AddRange(ship.Parts);
            }

            // Checkin if there is a collision with other already placed ships
            foreach (var shipPart in shipPositions)
            {
                foreach (var newPart in newShip.Parts)
                {
                    if (shipPart.Equals(newPart))
                    {
                        throw new InvalidPlacementException();
                    }
                }
            }
        }

        /// <summary>
        /// Checks if the given ship is in the battlefield.
        /// </summary>
        /// <param name="ship"><see cref="IShip"/>New ship.</param>
        private static void IsShipWithinBattlefield(IShips ship)
        {
            foreach (Coordinate part in ship.Parts)
            {
                if (!AIUtils.IsInField(part))
                {
                    throw new InvalidPlacementException();
                }
            }
        }
    }

    /// <summary>
    /// Own exception to indicate an invalid ship placement.
    /// <para>The ship hangs off the Battlefield, or collides with another.</para>
    /// </summary>
    [Serializable]
    public class InvalidPlacementException : Exception
    {
        public InvalidPlacementException() : base() { }
        public InvalidPlacementException(string message) : base(message) { }
        public InvalidPlacementException(string message, Exception inner) : base(message, inner) { }
        protected InvalidPlacementException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context)
        { }
    }
}