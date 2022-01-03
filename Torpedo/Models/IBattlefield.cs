using System;
using System.Collections.Generic;
using System.Text;
using Torpedo.Models;

namespace Torpedo.logic
{
    interface IBattlefield
    {
        List<Coordinate> Shoots { get; set; }
        List<Ships> Ships { get; set; }
    }
}
