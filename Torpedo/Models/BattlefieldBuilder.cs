using System.Collections.Generic;
using Torpedo.Interfaces;

namespace Torpedo.Models
{
    /// <summary>
    /// This class helps the front-end to reach the battlefield and build it
    /// </summary>
    public class BattlefieldBuilder
    {

        public List<IShips> Ships { get; private set; }

        public BattlefieldBuilder()
        {
            Ships = new List<IShips>(4);
        }

        /// <summary>
        /// Finaliez, then build the Battlefield to the player
        /// </summary>
        /// <returns>new <see cref="Battlefield"/> filled with ships</returns>
        public Battlefield Build()
        {
            return new Battlefield(Ships);
        }

        /// <summary>
        /// Add another ship to store int the builder
        /// </summary>
        /// <param name="ship"><see cref="IShips"/> ship to add</param>
        /// <returns><see cref="BattlefieldBuilder"/>Itself</returns>
        public BattlefieldBuilder AddShip(IShips ship)
        {
            Ships.Add(ship);
            return this;
        }
    }
}
