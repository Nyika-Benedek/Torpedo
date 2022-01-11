﻿using System.Collections.Generic;
using Torpedo.Interfaces;

namespace Torpedo.Models
{
    /// <summary>
    /// Stores data of one players's battle
    /// </summary>
    public class Battlefield : IBattlefield
    {

        public List<(Coordinate, bool)> Shots { get; private set; }
        private List<IShips> _ships;

        /// <summary>
        /// Shoot at a coordinate and checks if it was a hit
        /// </summary>
        /// <param name="coordinate">A <see cref="Coordinate"/> to shoot at</param>
        /// <returns>bool: Yes if it hits a ship, no otherwise</returns>
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
        /// Constructor
        /// </summary>
        /// <param name="ships">List of <see cref="IShips"/> to place it on the battlefield</param>
        public Battlefield(List<IShips> ships)
        {
            Shots = new List<(Coordinate, bool)>();
            _ships = ships;
        }
    }
}
