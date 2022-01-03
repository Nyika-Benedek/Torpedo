using System;
using System.Collections.Generic;
using System.Text;

namespace Torpedo.Models
{
    public interface IShips
    {
        List<Coordinate> Parts { get; set; }
        int Size { get => Parts.Count; }
        bool Sunk { get; set; }
    }
}
