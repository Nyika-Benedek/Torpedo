using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Torpedo;
using Torpedo.AIModule;
using Torpedo.Interfaces;
using Torpedo.Models;

namespace Torpedo.Test
{
    public class SinkAILogicTest
    {
        [TestMethod]
        public void aha()
        {
            List<IShips> ships = new List<IShips>();
            Battlefield battlefield = new Battlefield(ships);
            Coordinate x = new Coordinate(5, 7);
            Coordinate y = new Coordinate(5, 8);
            SinkAILogic logic = new SinkAILogic(battlefield, x, y);
            logic.proposed();
        }

    }
}
