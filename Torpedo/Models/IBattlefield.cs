using System;
using System.Collections.Generic;
using System.Text;

namespace Torpedo.Models
{
    public interface IBattlefield
    {
        List<Coordinate> Shoots { get; }
        abstract void Shoot(Coordinate coordinate);
    }
}
