using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Torpedo;
using Torpedo.AIModule;
using Torpedo.Interfaces;
using Torpedo.Models;

namespace Torpedo.Test
{
#pragma warning disable NI1007 // Test classes must ultimately inherit from 'AutoTest'
    [TestClass]
    public class FoundAILogicTest
    {
        //[TestMethod]
        /*public void Plan_IsEmpty()
        {
            List<IShips> ships = new List<IShips>();
            ships.Add(new Ship(new List<Coordinate>() { new Coordinate(0, 0) }));
            ships.Add(new Ship(new List<Coordinate>() { new Coordinate(-1, -1) }));
            ships.Add(new Ship(new List<Coordinate>() { new Coordinate(-2, 0) }));
            ships.Add(new Ship(new List<Coordinate>() { new Coordinate(-1, 1) }));
            Battlefield battlefield = new Battlefield(ships);
            Coordinate focus = new Coordinate(-1, 0);
            FoundAILogic logic = new FoundAILogic(battlefield, focus);
            List<Coordinate> logicList = logic.Plan();
            Assert.AreEqual(new List<Coordinate>(), logicList);
        }*/

        [TestMethod]
        public void Plan_AllCorrect()
        {
            List<IShips> ships = new List<IShips>();
            Battlefield battlefield = new Battlefield(ships);
            Coordinate focus = new Coordinate(1, 1);
            FoundAILogic logic = new FoundAILogic(battlefield, focus);
            List<Coordinate> actual = logic.Plan();
            List<Coordinate> expected = new List<Coordinate>() { new Coordinate(0, 1), new Coordinate(2, 1), new Coordinate(1, 0), new Coordinate(1, 2) };
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Plan_OneNotCorrect()
        {
            List<IShips> ships = new List<IShips>();
            Battlefield battlefield = new Battlefield(ships);
            Coordinate x = new Coordinate(0, 1);
            battlefield.Shoot(x);
            Coordinate focus = new Coordinate(1, 1);
            FoundAILogic logic = new FoundAILogic(battlefield, focus);
            List<Coordinate> actual = logic.Plan();
            Assert.IsFalse(actual.Contains(x));
            Assert.IsFalse(actual.Contains(focus));
        }

        [TestMethod]
        public void Plan_TwoNotCorrect()
        {
            List<IShips> ships = new List<IShips>();
            Battlefield battlefield = new Battlefield(ships);
            Coordinate x = new Coordinate(1, 1);
            Coordinate y = new Coordinate(-1, 1);
            battlefield.Shoot(x);
            Coordinate focus = new Coordinate(0, 1);
            FoundAILogic logic = new FoundAILogic(battlefield, focus);
            List<Coordinate> actual = logic.Plan();
            Assert.IsFalse(actual.Contains(x));
            Assert.IsFalse(actual.Contains(y));
            Assert.IsFalse(actual.Contains(focus));
        }

        [TestMethod]
        public void Plan_AllNotCorrect()
        {
            List<IShips> ships = new List<IShips>();
            Battlefield battlefield = new Battlefield(ships);
            Coordinate focus = new Coordinate(-2, 0);
            FoundAILogic logic = new FoundAILogic(battlefield, focus);
            List<Coordinate> actual = logic.Plan();
            Assert.IsTrue(actual.Count == 0);
        }
    }
#pragma warning restore NI1007 // Test classes must ultimately inherit from 'AutoTest'
}
