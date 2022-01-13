using System.Collections.Generic;
using Torpedo.Models;

namespace Torpedo.Interfaces
{
    public interface IBattlefield
    {
        // TODO: NI: replace Shots with GetShots
        List<(Coordinate, bool)> Shots { get; }
        abstract bool Shoot(Coordinate coordinate);

        // TODO: NI: replace method with property
        abstract List<int> RemainingShips();
    }
}
