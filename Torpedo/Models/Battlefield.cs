using System.Collections.Generic;
using Torpedo.Interfaces;

namespace Torpedo.Models
{
    /// <summary>
    /// Stores data of one <see cref="Player"/>'s battle.
    /// </summary>
    public class Battlefield : IBattlefield
    {
        public IList<(Coordinate, bool)> Shots { get; } = new List<(Coordinate, bool)>();
        private readonly IList<IShips> _ships;

        /// <summary>
        /// Shoot at a <see cref="Coordinate"/> and checks if it was a hit.
        /// </summary>
        /// <param name="coordinate">A <see cref="Coordinate"/> to shoot at.</param>
        /// <returns>bool: True if it hits a <see cref="Ship"/>, false otherwise.</returns>
        public bool Shoot(Coordinate coordinate)
        {
            bool isHitAny = false;
            foreach (Ship ship in _ships)
            {
                if (ship.IsHit(coordinate))
                {
                    isHitAny = true;
                }
            }
            Shots.Add((coordinate, isHitAny));
            return isHitAny;
        }

        /// <summary>
        /// Gives the list of the size of the remaing <see cref="Ship"/>s.
        /// </summary>
        /// <returns><see cref="List{int}"/> of the remaining ships.</returns>
        public IList<int> RemainingShips()
        {
            List<int> result = new List<int>(_ships.Count);
            foreach (IShips ship in _ships)
            {
                if (!ship.Sunk)
                {
                    result.Add(ship.Size);
                }
            }
            return result;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="ships">List of <see cref="IShips"/> to place it on the battlefield.</param>
        public Battlefield(IList<IShips> ships) => _ships = ships;
    }
}
