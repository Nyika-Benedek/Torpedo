namespace Torpedo.Test
{
    using System;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Torpedo.AIModule;
    using Torpedo.Interfaces;
    using Torpedo.Models;

    /// <summary>
    /// Tests the <see cref="FoundAILogic"/>.
    /// </summary>
    [TestClass]
    public class FoundAILogicTest
    {
        /// <summary>
        /// Tests plan with empty middle.
        /// </summary>
        [TestMethod]
        public void Plan_EmptyMiddle_AllCorrect()
        {
            // Arrange
            List<IShips> ships = new List<IShips>();
            Battlefield battlefield = new Battlefield(ships);
            Coordinate focus = new Coordinate(1, 1);
            FoundAILogic logic = new FoundAILogic(battlefield, focus);

            // Act
            List<Coordinate> actual = logic.Plan();
            List<Coordinate> expected = new List<Coordinate>() { new Coordinate(0, 1), new Coordinate(2, 1), new Coordinate(1, 0), new Coordinate(1, 2) };

            // Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests Plan shoting once, will miss and focus.
        /// </summary>
        [TestMethod]
        public void Plan_OneSot_MissingShotAndFocus()
        {
            // Arrange
            List<IShips> ships = new List<IShips>();
            Battlefield battlefield = new Battlefield(ships);
            Coordinate x = new Coordinate(0, 1);

            // Act
            battlefield.Shoot(x);
            Coordinate focus = new Coordinate(1, 1);
            FoundAILogic logic = new FoundAILogic(battlefield, focus);
            List<Coordinate> actual = logic.Plan();

            // Assert
            Assert.IsFalse(actual.Contains(x));
            Assert.IsFalse(actual.Contains(focus));
        }

        /// <summary>
        /// Tests Plan with shot, will moss coordinate.
        /// </summary>
        [TestMethod]
        public void Plan_WithShot_MissingCoordinates()
        {
            // Arrange
            List<IShips> ships = new List<IShips>();
            Battlefield battlefield = new Battlefield(ships);
            Coordinate x = new Coordinate(1, 1);
            Coordinate y = new Coordinate(-1, 1);

            // Act
            battlefield.Shoot(x);
            Coordinate focus = new Coordinate(0, 1);
            FoundAILogic logic = new FoundAILogic(battlefield, focus);
            List<Coordinate> actual = logic.Plan();

            // Assert
            Assert.IsFalse(actual.Contains(x));
            Assert.IsFalse(actual.Contains(y));
            Assert.IsFalse(actual.Contains(focus));
        }

        /// <summary>
        /// Tests constructor of battlefield with illegal argument.
        /// </summary>
        [TestMethod]
        public void Constructor_OutOfBattleField_ThrowsError()
        {
            // Arrange
            List<IShips> ships = new List<IShips>();
            Battlefield battlefield = new Battlefield(ships);
            Coordinate focus = new Coordinate(-1, 0);

            // Act + Assert
            Assert.ThrowsException<ArgumentException>(() => new FoundAILogic(battlefield, focus));
        }
    }
}
