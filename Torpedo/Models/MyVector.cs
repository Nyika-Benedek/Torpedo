using System;
using System.Collections.Generic;
using System.Text;

namespace Torpedo.Models
{
    // The name Vector will mess up the inner functioning of the WPF

    public enum Direction { Horizontal, Vertical };
    class MyVector
    {
        public Direction Way { get; private set; }
        public int Size { get; private set; }

        public MyVector(Direction way, int size)
        {
            Way = way;
            Size = size;
        }
    }
}
