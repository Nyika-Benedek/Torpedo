using System.Collections.Generic;
using Torpedo.Models;

namespace Torpedo.Interfaces
{
    public interface IShip
    {
        public enum Direction { Horizontal, Vertical }
        IList<Coordinate> Parts { get; set; }
        int Size { get => Parts.Count; }
        int Hits { get; }
        abstract bool IsHit(Coordinate coordinate);
        bool Sunk { get => Parts.Count.Equals(Hits); }
    }
}
