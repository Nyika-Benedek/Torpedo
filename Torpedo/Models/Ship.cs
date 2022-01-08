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

        public bool IsHit(Coordinate coordinate)
        {
            foreach (Coordinate part in Parts)
            {
                if (part == coordinate)
                {
                    Hits++;
                    return true;
                }
            }
            return false;
        }

        public List<Coordinate> Parts { get; set; }

        public int Hits { get; private set; }
        
        public Ship(Coordinate coordinate, MyVector vector)
        {

        }
    }
}
