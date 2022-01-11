using System.Collections.Generic;
using Torpedo.Models;

namespace Torpedo.Interfaces
{
    public interface IBattlefield
    {
        List<(Coordinate, bool)> Shots { get; }
        abstract bool Shoot(Coordinate coordinate);
        abstract List<int> RemainingShips(IBattlefield battlefield);
    }
}
