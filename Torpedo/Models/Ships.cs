using System;
using System.Collections.Generic;
using System.Text;

namespace Torpedo.Models
{
    interface Ships
    {
        List<Coordinate> Parts { get; set; }
        int size { get => Parts.Count; }
    }
}
