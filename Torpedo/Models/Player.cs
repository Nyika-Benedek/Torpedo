using System;
using System.Collections.Generic;
using System.Text;

namespace Torpedo.Models
{
    public class Player : IPlayer
    {
        public string Name { get; private set; }
        public IBattlefield Battlefield { get; private set; }
        public int Points { get; set; }

        public void AddPoint() { Points++; }

        public Player(string name)
        {
            Name = name;
            Battlefield = new Battlefield();
            Points = 0;
        }
    }
}
