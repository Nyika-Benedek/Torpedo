using System;
using System.Collections.Generic;
using System.Text;

namespace Torpedo.Models
{
    public interface IPlayer
    {
        public string Name { get; }
        public IBattlefield Battlefield { get; }

        public int Points { get; }

    }
}
