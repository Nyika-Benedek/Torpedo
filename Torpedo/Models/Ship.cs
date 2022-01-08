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
            Parts = new List<Coordinate>(vector.Size);
            for (int i = 0; i < vector.Size; i++)
            {
                Parts.Add(new Coordinate(coordinate.X, coordinate.Y));
                if (vector.Way == IShips.Direction.Horizontal)
                {
                    coordinate.X++;
                }
                else
                {
                    coordinate.Y++;
                }
            }
        }

        public override string ToString()
        {
            string result = "";
            foreach (Coordinate coordinate in Parts)
            {
                result = result + coordinate.ToString() + ' ';
            }
            return result;
        }
    }
}
