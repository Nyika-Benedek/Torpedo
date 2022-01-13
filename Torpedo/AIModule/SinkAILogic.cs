using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Torpedo.Interfaces;
using Torpedo.Models;

namespace Torpedo.AIModule
{
    public class SinkAILogic : AILogic
    {
        Coordinate nonRandomHit;
        Coordinate lastRandomHit;

        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="aI">Used <see cref="AI"/> agent</param>
        public SinkAILogic(IBattlefield enemyBattlefield, Coordinate nonRandomHit, Coordinate lastRandomHit) : base(enemyBattlefield)
        {
            this.nonRandomHit = nonRandomHit;
            this.lastRandomHit = lastRandomHit;
            if (lastRandomHit.Equals(nonRandomHit))
            {
                throw new InvalidOperationException();
            }
        }

        private Directions GetDirection()
        {
            // TODO SEVERE please provide a direction between the two coordinates. for explanation call from 10am if needed 
            // please implement if possible
            // the direction that if I shift enough time the lastRandomHit coordinate I get the nonRandomHit coordinate
            return Directions.Top;
        }

        /// <summary>
        /// It shooting radom to try hit something
        /// </summary>
        /// <returns>It returns the recommended <see cref="Coordinate"/></returns>
        public override List<Coordinate> Plan()
        {
            var result = new List<Coordinate>();
            result.Add(nonRandomHit.Shift(GetDirection()));
            return result;
        }
    }
}
