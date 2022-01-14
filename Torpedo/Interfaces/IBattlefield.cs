using System.Collections.Generic;
using Torpedo.Models;

namespace Torpedo.Interfaces
{
    public interface IBattlefield
    {
        IList<(Coordinate, bool)> Shots { get; }
        abstract bool Shoot(Coordinate coordinate);
        abstract IList<int> RemainingShips();
    }
}
