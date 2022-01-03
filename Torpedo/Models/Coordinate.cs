﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Torpedo.Models
{
    struct Coordinate
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Coordinate(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
