using System;
using System.Collections.Generic;
using System.Text;

namespace Torpedo.Models
{
    public class Battlefield : IBattlefield
    {

        public List<(Coordinate, bool)> Shots { get; private set; }
        private List<IShips> _ships;

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
            Shots.Add((coordinate, isHitAny));

        }
        public Battlefield(List<IShips> ships)
        {
            _ships = ships;
        }
    }
}
