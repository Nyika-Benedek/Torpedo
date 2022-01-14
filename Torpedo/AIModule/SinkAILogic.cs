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
    /// This logic tries to sink a ship witch position has been determined.
    /// </summary>
    public class SinkAILogic : AILogic
    {
        private Directions _direction;
        public Coordinate Proposed { get; set; }
        private Coordinate _nonRandomHit, _lastRandomHit;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="enemyBattlefield">The enemy's <see cref="Battlefield"/> containing previous shots.</param>
        /// <param name="nonRandomHit"><see cref="Coordinate"/> a non random hit.</param>
        /// <param name="lastRandomHit"><see cref="Coordinate"/> last random hit</param>
        public SinkAILogic(IBattlefield enemyBattlefield, Coordinate nonRandomHit, Coordinate lastRandomHit) : base(enemyBattlefield)
        {
            this._nonRandomHit = nonRandomHit;
            this._lastRandomHit = lastRandomHit;
            Propose();
        }

        /// <summary>
        /// Store a possible <see cref="Coordinate"/>, where the <see cref="Ship"/>'s remaining part could be.
        /// </summary>
        private void Propose()
        {
            _direction = AIUtils.GetDirection(origin: _lastRandomHit, shifted: _nonRandomHit);
            Proposed = _nonRandomHit.Shift(_direction);
        }

        /// <summary>
        /// Tries to hit in one row were a <see cref="Ship"/> could be.
        /// </summary>
        /// <returns>List of <see cref="Coordinate"/>, containing the proposed target.</returns>
        public override List<Coordinate> Plan()
        {
            var result = new List<Coordinate>();
            if (!AIUtils.IsCellShot(EnemyBattlefield, Proposed) && AIUtils.IsInField(Proposed))
            {
                result.Add(Proposed);
            }
            return result;
        }
    }
}
