using System;
using System.Collections.Generic;
using System.Text;

namespace Torpedo.Models
{
    public interface IBattlefield
    {
        List<(Coordinate, bool)> Shots { get; }
        abstract void Shoot(Coordinate coordinate);
    }
}
