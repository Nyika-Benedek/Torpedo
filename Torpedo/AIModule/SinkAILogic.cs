﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Torpedo.Interfaces;
using Torpedo.Models;

namespace Torpedo.AIModule
{
    /// <summary>
    /// This logic tries to sink a ship witch position (exactly or partly) has been determined
    /// </summary>
    public class SinkAILogic : AILogic
    {
        private Directions direction;
        public Coordinate proposed { get; set; }
        private Coordinate nonRandomHit, lastRandomHit;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="enemyBattlefield"><see cref="IBattlefield"/> enemy's battlefield</param>
        /// <param name="nonRandomHit"><see cref="Coordinate"/> a non random hit</param>
        /// <param name="lastRandomHit"><see cref="Coordinate"/> last random hit</param>
        public SinkAILogic(IBattlefield enemyBattlefield, Coordinate nonRandomHit, Coordinate lastRandomHit) : base(enemyBattlefield) // TODO test if the args match
        {
            this.nonRandomHit = nonRandomHit;
            this.lastRandomHit = lastRandomHit;
            Propose();
        }

        /// <summary>
        /// Propose a possible <see cref="Coordinate"/>, where the ships remaining parts is
        /// </summary>
        private void Propose()
        {
            direction = AIUtils.GetDirection(origin: lastRandomHit, shifted: nonRandomHit);
            proposed = nonRandomHit.Shift(direction);
        }

        /// <summary>
        /// The conclusion of where could be the enemy's hips
        /// </summary>
        /// <returns>List of <see cref="Coordinate"/>, where the enemy ships could be located</returns>
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
