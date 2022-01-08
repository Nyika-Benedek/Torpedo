using System;
using System.Collections.Generic;
using System.Text;

namespace Torpedo.Models
{
    class Battlefield : IBattlefield
    {
        public List<Coordinate> Shoots { get; private set; }

        private List<IShips> _ships;

        public void AddShips(IShips ship)
        {
            _ships.Add(ship);
        }

        public void Shoot(Coordinate coordinate)
        {
            bool isHitAny = false;
            foreach (Ship ship in _ships)
            {
                if (ship.IsHit(coordinate))
                {
                    isHitAny = true;
                }
            }

        }
    }
}
