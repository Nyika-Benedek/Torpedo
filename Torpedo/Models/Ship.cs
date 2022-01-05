using System.Collections.Generic;

namespace Torpedo.Models
{
    public class Ship : IShips
    {
        public Ship(List<Coordinate> parts)
        {
            Parts = parts;
            Hits = 0;
        }

        public bool IsHit(Coordinate position)
        {
            foreach (Coordinate part in Parts)
            {
                if (part == position)
                {
                    Hits++;
                    return true;
                }
            }
            return false;
        }

        public List<Coordinate> Parts { get; set; }

        public int Hits { get; private set; }
    }
}
