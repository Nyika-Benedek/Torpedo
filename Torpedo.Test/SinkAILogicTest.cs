using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Torpedo;
using Torpedo.AIModule;
using Torpedo.Interfaces;
using Torpedo.Models;
using System.Linq;

namespace Torpedo.Test
{
    /// <summary>
    /// Tests the <see cref="SinkAILogic"/>.
    /// </summary>
    [TestClass]
    public class SinkAILogicTest
    {
        /// <summary>
        /// Tests Plan with custom proposed <see cref="Coordinate"/>.
        /// </summary>
        [TestMethod]
        public void Plan_WithProposed_Accepted()
        {
            // Arrange
            List<IShip> ships = new List<IShip>();
            Battlefield battlefield = new Battlefield(ships);
            Coordinate x = new Coordinate(5, 7);
            Coordinate y = new Coordinate(5, 8);
            SinkAILogic logic = new SinkAILogic(battlefield, x, y);
            logic.Proposed = new Coordinate(0, 0);

            // Act
            List<Coordinate> planned = logic.Plan();

            // Assert
            Assert.AreEqual(planned.Count, 1);
            Assert.AreEqual(planned.Last(), logic.Proposed);
        }

        /// <summary>
        /// Tests Plan with custom proposed <see cref="Coordinate"/>, outside of the <see cref="Battlefield"/>.
        /// </summary>
        [TestMethod]
        public void Plan_WithProposedOutside_Rejected()
        {
            // Arrange
            List<IShip> ships = new List<IShip>();
            Battlefield battlefield = new Battlefield(ships);
            Coordinate x = new Coordinate(5, 7);
            Coordinate y = new Coordinate(5, 8);
            SinkAILogic logic = new SinkAILogic(battlefield, x, y);
            logic.Proposed = new Coordinate(-1, 0);

            // Act
            List<Coordinate> planned = logic.Plan();

            // Assert
            Assert.AreEqual(planned.Count, 0);
        }

        /// <summary>
        /// Tests Plan with custom, already shot <see cref="Coordinate"/>.
        /// </summary>
        [TestMethod]
        public void Plan_ProposedAlreadyShot_Rejected()
        {
            // Arrange
            List<IShip> ships = new List<IShip>();
            Battlefield battlefield = new Battlefield(ships);
            battlefield.Shoot(new Coordinate(0, 0));
            Coordinate x = new Coordinate(5, 7);
            Coordinate y = new Coordinate(5, 8);
            SinkAILogic logic = new SinkAILogic(battlefield, x, y);
            logic.Proposed = new Coordinate(0, 0);

            // Act
            List<Coordinate> planned = logic.Plan();

            // Assert
            Assert.AreEqual(planned.Count, 0);
        }

    }
}
