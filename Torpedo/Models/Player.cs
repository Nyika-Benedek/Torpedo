using System;
using Torpedo.Interfaces;

namespace Torpedo.Models
{
    public class Player : IPlayer
    {
        public string Name { get; private set; }
        public IBattlefield Battlefield { get; private set; }
        public IBattlefield EnemyBattlefield { get; private set; }

        public BattlefieldBuilder BattlefieldBuilder { get; private set;}
        public int Points { get; set; }
        public void AddPoint() { Points++; }

        public Player(string name)
        {
            Name = name;
            BattlefieldBuilder = new BattlefieldBuilder();
            Points = 0;
        }

        public void BuildBattlefield()
        {
            Battlefield = BattlefieldBuilder.Build();
            BattlefieldBuilder = null;
        }

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
