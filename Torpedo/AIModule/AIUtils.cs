using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Torpedo.Interfaces;
using Torpedo.Models;

namespace Torpedo
{
    /// <summary>
    /// The directions enum is made to define the four directions we can shoot at after a successful hit.
    /// </summary>
    public enum Directions { Left, Right, Top, Bottom }

    /// <summary>
    /// This class contains all static method wich helps the AI's decision
    /// </summary>
    // TODO: NI: Move to namespace
    public static class AIUtils
    {
        public static Random Random { get; } = new Random();
        /// <summary>
        /// This method decides that if a unit is shooted already or not.
        /// </summary>
        /// <param name="battlefield">It defines the whole playground.</param>
        /// <param name="coordinate">Defines the exact position of the unit which we would like to shoot at in a form of (x,y)</param>
        /// <returns>Returns a boolean, true if a unit is already shooted and false if it is not.</returns>
        public static bool IsCellShot(IBattlefield battlefield, Coordinate coordinate)
        {
            bool result = false;
            foreach ((Coordinate, bool) shot in battlefield.Shots)
            {
                if (coordinate.Equals(shot.Item1))
                {
                    result = true;
                }
            }
            return result;
        }

        /// <summary>
        /// Checks if a given Coordinate is within the battlefield
        /// </summary>
        /// <param name="coordinate">The <see cref="Coordinate"/> to check</param>
        /// <returns>bool</returns>
        public static bool IsInField(Coordinate coordinate) => coordinate.X >= 0 && coordinate.X < MainWindow.BattlefieldWidth && coordinate.Y >= 0 && coordinate.Y < MainWindow.BattlefieldHeight;
        /// <summary>
        /// This method generates a random (x,y) coordinate pair, so the AI will select a position randomly to shoot at.
        /// </summary>
        /// <param name="battlefield">The used<see cref="Battlefield"/></param>
        /// <returns>It returns the coordinate in a form of (x,y) values.</returns>
        public static Coordinate GenerateRandomShoot(IBattlefield battlefield)
        {
            Coordinate coordinate;
            do
            {
                coordinate = RandomCoordinate();
            }
            while (AIUtils.IsCellShot(battlefield, coordinate) == true);
            return coordinate;
        }

        public static Coordinate RandomCoordinate()
        {
            int x;
            int y;
            y = Random.Next(0, MainWindow.BattlefieldHeight);
            x = Random.Next(0, MainWindow.BattlefieldWidth);
            return new Coordinate(x, y);
        }

        public static Directions GetDirection(Coordinate origin, Coordinate shifted)
        {
            Coordinate delta = new Coordinate(x: shifted.X, y: shifted.Y);
            delta.X -= origin.X;
            delta.Y -= origin.Y;

            if (delta.X != 0 && delta.Y != 0)
            {
                throw new ArgumentException("shots are not aligned");
            }

            if (delta.X < 0)
            {
                return Directions.Left;
            }
            if (delta.X > 0)
            {
                return Directions.Right;
            }
            if (delta.Y < 0)
            {
                return Directions.Top;
            }
            if (delta.Y > 0)
            {
                return Directions.Bottom;
            }

            throw new ArgumentException("shots are on the same spot");

        }
    }
}