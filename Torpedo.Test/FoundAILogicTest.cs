using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        [TestMethod]
        public void Plan_EmptyMiddle_AllCorrect()
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
        public void Plan_OneSwot_MissingShotAndFocus()
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
        public void Plan_WithShot_MissingCoordinates()
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
        public void Ctor_OutOfBattleField_ThrowsError()
        {
            List<IShips> ships = new List<IShips>();
            Battlefield battlefield = new Battlefield(ships);
            Coordinate focus = new Coordinate(-1, 0);
            Assert.ThrowsException<ArgumentException>(() => new FoundAILogic(battlefield, focus));
        }
    }
#pragma warning restore NI1007 // Test classes must ultimately inherit from 'AutoTest'
}
