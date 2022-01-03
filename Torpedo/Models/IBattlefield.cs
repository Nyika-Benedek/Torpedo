using System;
using System.Collections.Generic;
using System.Text;

namespace Torpedo.Models
{
    internal interface IBattlefield
    {
        List<Coordinate> Shoots { get; set; }
        List<IShips> Ships { get; set; }

        public void Shoot(Coordinate coordinate);

        public List<Coordinate> Hits();
    }
}
