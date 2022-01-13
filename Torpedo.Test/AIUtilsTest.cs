namespace TestProject3
{
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Torpedo;
    using Torpedo.Interfaces;
    using Torpedo.Models;

    /// <summary>
    /// Tests the <see cref="AIUtils"/>.
    /// </summary>
    [TestClass]
    public class AIUtilsTest
    {
        /// <summary>
        /// Tests IsInField with <see cref="Coordinate"/> in field.
        /// </summary>
        [TestMethod]
        public void IsInField_InField_True()
        {
            Coordinate x = new Coordinate(4, 8);
            Assert.IsTrue(AIUtils.IsInField(x));
        }

        /// <summary>
        /// Tests IsInField with Y <see cref="Coordinate"/> out of Field.
        /// </summary>
        [TestMethod]
        public void IsInField_YOutside_False()
        {
            Coordinate x = new Coordinate(0, 12);
            Assert.IsFalse(AIUtils.IsInField(x));
        }

        /// <summary>
        /// Tests IsInField with X <see cref="Coordinate"/> out of Field.
        /// </summary>
        [TestMethod]
        public void IsInField_XOutside_False()
        {
            Coordinate x = new Coordinate(12, 0);
            Assert.IsFalse(AIUtils.IsInField(x));
        }

        /// <summary>
        /// Tests IsInField with both x and y <see cref="Coordinate"/> out of Field.
        /// </summary>
        [TestMethod]
        public void IsInField_BothOutside_False()
        {
            Coordinate x = new Coordinate(11, 12);
            Assert.IsFalse(AIUtils.IsInField(x));
        }

        /// <summary>
        /// Tests IsInField with absurd <see cref="Coordinate"/> inputs.
        /// </summary>
        [TestMethod]
        public void IsInField_EdgeCases_False()
        {
            Assert.IsFalse(AIUtils.IsInField(new Coordinate(int.MinValue, int.MinValue)));
            Assert.IsFalse(AIUtils.IsInField(new Coordinate(int.MaxValue, int.MaxValue)));
            Assert.IsFalse(AIUtils.IsInField(new Coordinate(int.MinValue, 2)));
            Assert.IsFalse(AIUtils.IsInField(new Coordinate(4, int.MinValue)));
            Assert.IsFalse(AIUtils.IsInField(new Coordinate(int.MaxValue, 7)));
            Assert.IsFalse(AIUtils.IsInField(new Coordinate(0, int.MaxValue)));
            Assert.IsFalse(AIUtils.IsInField(new Coordinate(int.MinValue, int.MaxValue)));
            Assert.IsFalse(AIUtils.IsInField(new Coordinate(int.MaxValue, int.MinValue)));
        }

        /// <summary>
        /// Tests IsCellShot with an already shot <see cref="Coordinate"/>, hitting a ship.
        /// </summary>
        [TestMethod]
        public void IsCellShot_Hit_True()
        {
            // Arrange
            MyVector vector = new MyVector(IShips.Direction.Horizontal, 5);
            Ship ship = new Ship(new Coordinate(0, 0), vector);
            List<IShips> list = new List<IShips>();
            list.Add(ship);
            Battlefield battlefield = new Battlefield(list);
            Coordinate x = new Coordinate(0, 0);

            // Act
            battlefield.Shoot(x);

            // Assert
            Assert.IsTrue(AIUtils.IsCellShot(battlefield, x));
        }

        /// <summary>
        /// Tests IsCellShot with an already shot <see cref="Coordinate"/>, missing a ship.
        /// </summary>
        [TestMethod]
        public void IsCellShot_Miss_True()
        {
            // Arrange
            MyVector vector = new MyVector(IShips.Direction.Horizontal, 5);
            Ship ship = new Ship(new Coordinate(0, 0), vector);
            List<IShips> list = new List<IShips>();
            list.Add(ship);
            Battlefield battlefield = new Battlefield(list);
            Coordinate x = new Coordinate(0, 4);

            // Act
            battlefield.Shoot(x);

            // Assert
            Assert.IsTrue(AIUtils.IsCellShot(battlefield, x));
        }

        /// <summary>
        /// Tests IsCellShot with an empty <see cref="Battlefield"/>.
        /// </summary>
        [TestMethod]
        public void IsCellShot_EmptyBattlefield_False()
        {
            // Arrange
            List<IShips> list = new List<IShips>();
            Battlefield battlefield = new Battlefield(list);
            Coordinate x = new Coordinate(0, 0);

            // Act
            battlefield.Shoot(x);

            // Assert
            Coordinate y = new Coordinate(1, 0);
            Assert.IsFalse(AIUtils.IsCellShot(battlefield, y));
        }
    }
#pragma warning restore NI1007 // Test classes must ultimately inherit from 'AutoTest'
}
