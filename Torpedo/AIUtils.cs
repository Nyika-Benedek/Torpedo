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
        public static bool isCellShooted(IBattlefield battlefield, Coordinate coordinate)
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
            while (AIUtils.isCellShooted(battlefield, coordinate) == true);
            _lastShoot = coordinate;
            return coordinate;
        } 

        public static Coordinate found(IBattlefield battlefield)
        {
            Coordinate coordinate;
            if(AIUtils.isCellShooted(battlefield, _lastShoot.X - 1) == false && _lastShoot.X - 1 >= 0)
            { 
                coordinate = _lastShoot.X - 1; 
            }
            else if (AIUtils.isCellShooted(battlefield, _lastShoot.Y - 1) == false && _lastShoot.Y - 1 >= 0)
            {
                coordinate = _lastShoot.Y - 1;
            }
            else if (AIUtils.isCellShooted(battlefield, _lastShoot.X + 1) == false && _lastShoot.X + 1 <= 9)
            {
                coordinate = _lastShoot.X + 1;
            }
            else if (AIUtils.isCellShooted(battlefield, _lastShoot.Y + 1) == false && _lastShoot.Y + 1 <= 9)
            {
                coordinate = _lastShoot.Y + 1;
            }
            return coordinate;
        }
    }
}