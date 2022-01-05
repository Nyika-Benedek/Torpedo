using System.Collections.Generic;

namespace Torpedo.Models
{
    public interface IShips
    {
        List<Coordinate> Parts { get; set; }
        int Size { get => Parts.Count; }
        int Hits { get; }
        abstract bool IsHit(Coordinate position);
        bool Sunk { get => Parts.Count.Equals(Hits); }
    }
}
