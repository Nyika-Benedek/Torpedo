﻿using Torpedo.Models;

namespace Torpedo.Interfaces
{
    public interface IPlayer
    {
        public string Name { get; }
        public IBattlefield Battlefield { get; }
        public BattlefieldBuilder BattlefieldBuilder { get; }

        public IBattlefield EnemyBattlefield { get; }

        public int Points { get; }
        public abstract void AddPoint();
        public abstract void BuildBattlefield();
        public abstract void SetEnemyBattlefield(IBattlefield enemyBattlefield);
    }
}
