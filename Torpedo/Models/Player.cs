using System;
using Torpedo.Interfaces;

namespace Torpedo.Models
{
    /// <summary>
    /// Represents a player.
    /// </summary>
    public class Player : IPlayer
    {
        public string Name { get; private set; }
        public IBattlefield Battlefield { get; protected set; }
        public IBattlefield EnemyBattlefield { get; private set; }

        public BattlefieldBuilder BattlefieldBuilder { get; private set; } = new BattlefieldBuilder();
        public int Points { get; set; } = 0;

        /// <summary>
        /// Increase current player's point.
        /// </summary>
        public void AddPoint() { Points++; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name">Name of the player.</param>
        public Player(string name) => Name = name;

        /// <summary>
        /// Build the <see cref="Battlefield"/> of the player.
        /// </summary>
        public void BuildBattlefield()
        {
            Battlefield = BattlefieldBuilder.Build();
            BattlefieldBuilder = null;
        }

        /// <summary>
        /// Set the enemy battlefield to be able to shoot at.
        /// </summary>
        /// <param name="enemyBattlefield"><see cref="IBattlefield"/> enemy's battlefield.</param>
        public void SetEnemyBattlefield(IBattlefield enemyBattlefield)
        {
            if (EnemyBattlefield == null)
            {
                EnemyBattlefield = enemyBattlefield;
            }
            else
            {
                throw new InvalidOperationException("Already assigned");
            }
        }
    }
}
