using System;
using System.Collections.Generic;
using Torpedo.AIModule;
using Torpedo.Interfaces;

namespace Torpedo.Models
{
    /// <summary>
    /// This class is define the grids of the battlefield.
    /// </summary>
    public class Coordinate
    {
        /// <summary>
        /// Represents X coordinate(Horizontal)
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Represents Y coordinate(Vertical)
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Constructor of Coordinate
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        public Coordinate(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Coordinate);
        }

        public bool Equals(Coordinate other)
        {
            return !(other is null)
                && this.X == other.X
                && this.Y == other.Y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.X, this.Y);
        }

        public static bool operator ==(Coordinate left, Coordinate right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Coordinate left, Coordinate right)
        {
            return !(left == right);
        }
        public override string ToString()
        {
            return "X" + this.X + "Y" + this.Y;
        }

        public Coordinate Shift(Directions direction)
        {
            switch (direction)
            {
                case Directions.Left:
                    {
                        return new Coordinate(X - 1, Y);
                    }
                case Directions.Top:
                    {
                        return new Coordinate(X, Y - 1);
                    }
                case Directions.Bottom:
                    {
                        return new Coordinate(X, Y + 1);
                    }
                case Directions.Right:
                    {
                        return new Coordinate(X + 1, Y);
                    }
            }
            return this;
        }
    }
}