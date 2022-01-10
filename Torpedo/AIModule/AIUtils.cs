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
    public enum Directions { Left, Right, Top, Bottom };
    
    public static class AIUtils
    {
        /// <summary>
        /// This method decides that if a unit is shooted already or not.
        /// </summary>
        /// <param name="battlefield">It defines the whole playground.</param>
        /// <param name="coordinate">Defines the exact position of the unit which we would like to shoot at in a form of (x,y)</param>
        /// <returns>Returns a boolean, true if a unit is already shooted and false if it is not.</returns>
        public static bool IsCellShooted(IBattlefield battlefield, Coordinate coordinate)
        {
            bool result = false;
            foreach (var shot in battlefield.Shots)
            {
                if(coordinate.Equals(shot))
                {
                    result = true;
                }
            }
            return result;
        }
        public static bool IsInField(Coordinate coordinate)
        {
            return coordinate.X >= 0 && coordinate.X < MainWindow.BattlefieldWidth && coordinate.Y >= 0 && coordinate.Y < MainWindow.BattlefieldHeight;
        }
        /// <summary>
        /// This method generates a random (x,y) coordinate pair, so the AI will select a position randomly to shoot at.
        /// </summary>
        /// <param name="battlefield">It defines the whole playground.</param>
        /// <returns>It returns the coordinate in a form of (x,y) values.</returns>
        public static Coordinate GenerateRandomShoot(IBattlefield battlefield)
        {
            Coordinate coordinate;
            int x;
            int y;
            Random random = new Random();
            do
            {
                y = random.Next(0, 9 + 1);
                x = random.Next(0, 9 + 1);
                coordinate = new Coordinate(x, y);
            }
            while (AIUtils.IsCellShooted(battlefield, coordinate) == true);
            return coordinate;
        }
        /*
        /// <summary>
        /// This method shoots at a direction.
        /// </summary>
        /// <param name="battlefield">It defines the whole playground.</param>
        /// <returns>It returns the coordinate in a form of (x,y) values.</returns>

        public static Coordinate Sink(IBattlefield battlefield)
        {
            Coordinate coordinate;

            switch (direction)
            {
                case directions.left:
                    coordinate = _lastShoot.X - _counter;
                    _counter++;
                    break;
                case directions.right:
                    coordinate = _lastShoot.X + _counter;
                    _counter++;
                    break;
                case directions.top:
                    coordinate = _lastShoot.Y - _counter;
                    _counter++;
                    break;
                case directions.bottom:
                    coordinate = _lastShoot.Y + _counter;
                    _counter++;
                    break;
                default:
                    break;
            }
            //TODO Hajók lekérdezése, a lövés ne menjen ki a pályáról.
            //TODO Ami még nem tetszik
            return coordinate;
        }*/
    }
}