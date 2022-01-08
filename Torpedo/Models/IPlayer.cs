using System;
using System.Collections.Generic;
using System.Text;

namespace Torpedo.Models
{
    public interface IPlayer
    {
        public string Name { get; }
        public IBattlefield Battlefield { get; }
        public BattlefieldBuilder BattlefieldBuilder { get; }

        public int Points { get; }
        public abstract void AddPoint();
        public abstract void BuildBattlefield();
    }
}
