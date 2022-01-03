using System;
using System.Collections.Generic;
using System.Text;

namespace Torpedo.Models
{
    internal interface IPlayer
    {
        public string Name { get; set; }
        public IBattlefield Battlefield { get; set; }

        public int Points { get; set; }

    }
}
