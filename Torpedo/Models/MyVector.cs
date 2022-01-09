using System;
using System.Collections.Generic;
using System.Text;

namespace Torpedo.Models
{
    // The name Vector will mess up the inner functioning of the WPF
    public class MyVector
    {
        public IShips.Direction Way { get; private set; }
        public int Size { get; private set; }

        public MyVector(IShips.Direction way, int size)
        {
            Way = way;
            Size = size;
        }
    }
}
