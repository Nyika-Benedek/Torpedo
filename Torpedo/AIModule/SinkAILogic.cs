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
        private Directions direction;
        private Coordinate proposed, nonRandomHit, lastRandomHit;

        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="aI">Used <see cref="AI"/> agent</param>
        public SinkAILogic(IBattlefield enemyBattlefield, Coordinate nonRandomHit, Coordinate lastRandomHit) : base(enemyBattlefield) // TODO test if the args match
        {
            this.nonRandomHit = nonRandomHit;
            this.lastRandomHit = lastRandomHit;
            Propose();
        }

        private void Propose()
        {
            direction = AIUtils.GetDirection(origin: lastRandomHit, shifted: nonRandomHit);
            proposed = nonRandomHit.Shift(direction);
        }

        /// <summary>
        /// It shooting radom to try hit something
        /// </summary>
        /// <returns>It returns the recommended <see cref="Coordinate"/></returns>
        public override List<Coordinate> Plan()
        {
            var result = new List<Coordinate>();
            if (!AIUtils.IsCellShot(EnemyBattlefield, proposed) && AIUtils.IsInField(proposed))
            {
                result.Add(proposed);
            }
            return result;
        }
    }
}
