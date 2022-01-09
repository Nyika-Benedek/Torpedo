using System.Collections.Generic;

namespace Torpedo.Models
{
    public interface IShips
    {
        public enum Direction { Horizontal, Vertical }
        List<Coordinate> Parts { get; set; }
        int Size { get => Parts.Count; }
        int Hits { get; }
        abstract bool IsHit(Coordinate coordinate);
        bool Sunk { get => Parts.Count.Equals(Hits); }
    }
}
