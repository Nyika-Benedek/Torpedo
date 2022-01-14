using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Torpedo.Interfaces;
using Torpedo.Models;

namespace Torpedo.AIModule
{
    /// <summary>
    /// The directions enum is made to define the four directions we can shoot at after a successful hit.
    /// </summary>
    public enum Directions { Left, Right, Top, Bottom }

    /// <summary>
    /// This class contains all static method wich helps the AI's decision.
    /// </summary>
    public static class AIUtils
    {
        public static Random Random { get; } = new Random();

        /// <summary>
        /// This method decides whether a <see cref="Coordinate"/> is shot already.
        /// </summary>
        /// <param name="battlefield"> defines the playground.</param>
        /// <param name="coordinate"> defines the <see cref="Coordinate"/> which we would like to shoot at.</param>
        /// <returns> <see cref="bool"/> true if a  <see cref="Coordinate"/> is already shot or otherwise false.</returns>
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
        /// Checks if a given <see cref="Coordinate"/> is within the <see cref="Battlefield"/>.
        /// </summary>
        /// <param name="coordinate">The <see cref="Coordinate"/> to check.</param>
        /// <returns><see cref="bool"/> true if a <see cref="Coordinate"/> is within the <see cref="Battlefield"/>.</returns>
        public static bool IsInField(Coordinate coordinate) => coordinate.X >= 0 && coordinate.X < MainWindow.BattlefieldWidth && coordinate.Y >= 0 && coordinate.Y < MainWindow.BattlefieldHeight;

        /// <summary>
        /// This method generates a random <see cref="Coordinate"/>, that wasn't shot before.
        /// </summary>
        /// <param name="battlefield">The used <see cref="Battlefield"/>.</param>
        /// <returns>It returns the <see cref="Coordinate"/>.</returns>
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

        /// <summary>
        /// Generate a random <see cref="Coordinate"/>.
        /// </summary>
        /// <returns>A random <see cref="Coordinate"/> coordinate.</returns>
        public static Coordinate RandomCoordinate()
        {
            int x;
            int y;
            y = Random.Next(0, MainWindow.BattlefieldHeight);
            x = Random.Next(0, MainWindow.BattlefieldWidth);
            return new Coordinate(x, y);
        }

        /// <summary>
        /// Describes the <see cref="Directions"/> of shifted <see cref="Coordinate"/> from the origin.
        /// <para>Inverse of  <see cref="Coordinate.Shift(Directions)"/>.</para>
        /// </summary>
        /// <param name="origin"><see cref="Coordinate"/></param>
        /// <param name="shifted"><see cref="Coordinate"/></param>
        /// <returns><see cref="Directions"/></returns>
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