using System;
using System.Collections.Generic;
using System.Text;
using Torpedo.Models;
using Torpedo.Interfaces;

namespace Torpedo.AIModule
{
    public abstract class AILogic
    {
        public AILogic(AI aI)
        {
            AI = aI;
        }
        public AI AI { get; private set; }
        public abstract Coordinate Act();
    }
}
