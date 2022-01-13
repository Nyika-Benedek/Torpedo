using System;
using System.Collections.Generic;
using Torpedo.AIModule;
using Torpedo.Interfaces;

namespace Torpedo.Models
{
    /// <summary>
    /// Mutable representation of position on the battlefield
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

        /// <summary>
        /// Equals
        /// </summary>
        /// <param name="obj"><see cref="object"/>obj</param>
        /// <returns><see cref="bool"/> true if equals, false otherwise</returns>
        public override bool Equals(object obj)
        {
            return Equals(obj as Coordinate);
        }

        /// <summary>
        /// Equals
        /// </summary>
        /// <param name="other"><see cref="Coordinate"/> other</param>
        /// <returns><see cref="bool"/> true if equals, false otherwise</returns>
        public bool Equals(Coordinate other)
        {
            return !(other is null)
                && this.X == other.X
                && this.Y == other.Y;
        }

        /// <summary>
        /// Get hash of a coordinate
        /// </summary>
        /// <returns>HashCode.Combine(<see cref="int"/> X, <see cref="int"/> Y</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(this.X, this.Y);
        }


        /// <summary>
        /// Override <see cref="Coordinate"/> == <see cref="Coordinate"/> operation
        /// </summary>
        /// <param name="left"><see cref="Coordinate"/> left</param>
        /// <param name="right"><see cref="Coordinate"/> right</param>
        /// <returns><see cref="bool"/> true, if left.X equals right.X and left.Y equals left.Y, false otherwise</returns>
        public static bool operator ==(Coordinate left, Coordinate right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Override <see cref="Coordinate"/> != <see cref="Coordinate"/> operation
        /// </summary>
        /// <param name="left"><see cref="Coordinate"/> left</param>
        /// <param name="right"><see cref="Coordinate"/> right</param>
        /// <returns>!(<see cref="Coordinate"/> left == <see cref="Coordinate"/> right)</returns>
        public static bool operator !=(Coordinate left, Coordinate right)
        {
            return !(left == right);
        }
        public override string ToString()
        {
            return "X" + this.X + "Y" + this.Y;
        }

        /// <summary>
        /// Shift a <see cref="Coordinate"/> into one <see cref="IShips.Direction"/>
        /// </summary>
        /// <param name="direction"><see cref="IShips.Direction"/> to shift at</param>
        /// <returns>The shifted <see cref="Coordinate"/></returns>
        // TODO: NI: ?!
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