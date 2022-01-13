using System.Collections.Generic;
using Torpedo.Models;

namespace Torpedo.Interfaces
{
    public interface IShips
    {
        public enum Direction { Horizontal, Vertical }

        // TODO: NI:  replace parts with method
        List<Coordinate> Parts { get; set; }
        int Size { get => Parts.Count; }
        int Hits { get; }
        abstract bool IsHit(Coordinate coordinate);
        bool Sunk { get => Parts.Count.Equals(Hits); }
    }
}
