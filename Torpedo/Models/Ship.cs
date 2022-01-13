using System.Collections.Generic;
using Torpedo.Interfaces;

namespace Torpedo.Models
{
    /// <summary>
    /// Represents a Ship as on object
    /// </summary>
    public class Ship : IShips
    {
        public List<Coordinate> Parts { get; set; }
        public int Hits { get; private set; }

        // TODO: NI: ?!
        public Ship(List<Coordinate> parts)
        {
            Parts = parts;
            Hits = 0;
        }

        /// <summary>
        /// Check if yous shoot at a <see cref="Coordinate"/>, there will hit a ship's part
        /// </summary>
        /// <param name="coordinate"><see cref="Coordinate"/> to shoot at</param>
        /// <returns>bool: True, if it hits a ship, false otherwise</returns>
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

        /// <summary>
        /// Constructor, descibe a ship with a coordinate and a vector, to creat a ship
        /// </summary>
        /// <param name="coordinate"><see cref="Coordinate"/>: starting coordinate</param>
        /// <param name="vector">MyVector: <see cref="IShips.Direction"/>{horizontal: to the right from the given coordinate || Vertical: to the bottom of the given coordinate}, int: size of the vector</param>
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

        /// <summary>
        /// Gives the string of every ship parts
        /// </summary>
        /// <returns>string: line of ship parts</returns>
        public override string ToString()
        {
            string result = string.Empty;
            foreach (Coordinate coordinate in Parts)
            {
                result = result + coordinate.ToString() + ' ';
            }
            return result;
        }
    }
}
