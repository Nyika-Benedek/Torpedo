using System.Collections.Generic;
using Torpedo.Models;

namespace Torpedo.Interfaces
{
    public interface IBattlefield
    {
        List<(Coordinate, bool)> Shots { get; }
        abstract void Shoot(Coordinate coordinate);
    }
}
