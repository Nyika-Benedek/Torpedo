using System;
using System.Collections.Generic;
using System.Text;

namespace Torpedo.Models
{
    public class BattlefieldBuilder
    {

        public List<IShips> Ships { get; private set; }

        public BattlefieldBuilder()
        {
            Ships = new List<IShips>(4);
        }

        public Battlefield Build()
        {
            return new Battlefield(Ships);
        }

        public BattlefieldBuilder AddShip(IShips ship)
        {
            Ships.Add(ship);
            return this;
        }
    }
}
