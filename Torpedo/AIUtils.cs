using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Torpedo.Interfaces;
using Torpedo.Models;

namespace Torpedo
{
    public static class AIUtils
    {
        Coordinate _lastShoot;
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
            _lastShoot = coordinate;
            return coordinate;
        }
        /// <summary>
        /// The directions enum is made to define the four directions we can shoot at after a successful hit.
        /// </summary>
        enum directions { left, right, top, bottom };
        directions direction;
        int _counter = 2;
        /// <summary>
        /// It gets a coordinate and tries to hit the four possible neighbours.
        /// </summary>
        /// <param name="battlefield">It defines the whole playground.</param>
        /// <returns>It returns the coordinate in a form of (x,y) values.</returns>
        public static Coordinate Found(IBattlefield battlefield)
        {
            Coordinate coordinate;
            
            if(AIUtils.IsCellShooted(battlefield, _lastShoot.X - 1) == false && _lastShoot.X - 1 >= 0)
            { 
                coordinate = _lastShoot.X - 1;
                direction = directions.left;
            }
            else if (AIUtils.IsCellShooted(battlefield, _lastShoot.Y - 1) == false && _lastShoot.Y - 1 >= 0)
            {
                coordinate = _lastShoot.Y - 1;
                direction = directions.top;
            }
            else if (AIUtils.IsCellShooted(battlefield, _lastShoot.X + 1) == false && _lastShoot.X + 1 <= 9)
            {
                coordinate = _lastShoot.X + 1;
                direction = directions.right;
            }
            else if (AIUtils.IsCellShooted(battlefield, _lastShoot.Y + 1) == false && _lastShoot.Y + 1 <= 9)
            {
                coordinate = _lastShoot.Y + 1;
                direction = directions.bottom;
            }
            return coordinate;
        }

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
        }
    }
}