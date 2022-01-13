using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Torpedo;
using Torpedo.AIModule;
using Torpedo.Interfaces;
using Torpedo.Models;
using System.Linq;

namespace Torpedo.Test
{
#pragma warning disable NI1007 // Test classes must ultimately inherit from 'AutoTest'
    [TestClass]
    public class SinkAILogicTest
    {
        [TestMethod]
        public void HappyPath_Plan()
        {
            List<IShips> ships = new List<IShips>();
            Battlefield battlefield = new Battlefield(ships);
            Coordinate x = new Coordinate(5, 7);
            Coordinate y = new Coordinate(5, 8);
            SinkAILogic logic = new SinkAILogic(battlefield, x, y);
            logic.Proposed = new Coordinate(0, 0);
            List<Coordinate> planned = logic.Plan();
            Assert.AreEqual(planned.Count, 1);
            Assert.AreEqual(planned.Last(), logic.Proposed);
        }

        [TestMethod]
        public void SadPath_Plan()
        {
            List<IShips> ships = new List<IShips>();
            Battlefield battlefield = new Battlefield(ships);
            Coordinate x = new Coordinate(5, 7);
            Coordinate y = new Coordinate(5, 8);
            SinkAILogic logic = new SinkAILogic(battlefield, x, y);
            logic.Proposed = new Coordinate(-1, 0);
            List<Coordinate> planned = logic.Plan();
            Assert.AreEqual(planned.Count, 0);
        }

        [TestMethod]
        public void SadPath_AlreadyShot_Plan()
        {
            List<IShips> ships = new List<IShips>();
            Battlefield battlefield = new Battlefield(ships);
            battlefield.Shoot(new Coordinate(0, 0));
            Coordinate x = new Coordinate(5, 7);
            Coordinate y = new Coordinate(5, 8);
            SinkAILogic logic = new SinkAILogic(battlefield, x, y);
            logic.Proposed = new Coordinate(0, 0);
            List<Coordinate> planned = logic.Plan();
            Assert.AreEqual(planned.Count, 0);
        }

    }
#pragma warning restore NI1007 // Test classes must ultimately inherit from 'AutoTest'
}
