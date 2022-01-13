namespace TestProject3
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;
    using Torpedo;
    using Torpedo.Interfaces;
    using Torpedo.Models;

#pragma warning disable NI1007 // Test classes must ultimately inherit from 'AutoTest'
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void IsInField_ReturnTrue()
        {
            Coordinate x = new Coordinate(4, 8);
            // Arrange + Act + Assert
            Assert.IsTrue(AIUtils.IsInField(x));
        }

        [TestMethod]
        public void IsInField_y_ReturnFalse()
        {
            Coordinate x = new Coordinate(0, 12);
            // Arrange + Act + Assert
            Assert.IsFalse(AIUtils.IsInField(x));
        }

        [TestMethod]
        public void IsInField_x_ReturnFalse()
        {
            Coordinate x = new Coordinate(12, 0);
            // Arrange + Act + Assert
            Assert.IsFalse(AIUtils.IsInField(x));
        }

        [TestMethod]
        public void IsInField_xy_ReturnFalse()
        {
            Coordinate x = new Coordinate(11, 12);
            // Arrange + Act + Assert
            Assert.IsFalse(AIUtils.IsInField(x));
        }

        [TestMethod]
        public void IsInField_edgecases_ReturnFalse()
        {
            // Arrange + Act + Assert
            Assert.IsFalse(AIUtils.IsInField(new Coordinate(int.MinValue, int.MinValue)));
            Assert.IsFalse(AIUtils.IsInField(new Coordinate(int.MaxValue, int.MaxValue)));
            Assert.IsFalse(AIUtils.IsInField(new Coordinate(int.MinValue, 2)));
            Assert.IsFalse(AIUtils.IsInField(new Coordinate(4, int.MinValue)));
            Assert.IsFalse(AIUtils.IsInField(new Coordinate(int.MaxValue, 7)));
            Assert.IsFalse(AIUtils.IsInField(new Coordinate(0, int.MaxValue)));
            Assert.IsFalse(AIUtils.IsInField(new Coordinate(int.MinValue, int.MaxValue)));
            Assert.IsFalse(AIUtils.IsInField(new Coordinate(int.MaxValue, int.MinValue)));
        }

        [TestMethod]
        public void IsCellShot_ReturnTrue()
        {
            MyVector vector = new MyVector(IShips.Direction.Horizontal, 5);
            Ship ship = new Ship(new Coordinate(0, 0), vector);
            List<IShips> list = new List<IShips>();
            list.Add(ship);
            Battlefield battlefield = new Battlefield(list);
            Coordinate x = new Coordinate(0, 0);
            battlefield.Shoot(x);
            // Arrange + Act + Assert
            Assert.IsTrue(AIUtils.IsCellShot(battlefield, x));
        }

        [TestMethod]
        public void IsCellShot_MissShip_ReturnTrue()
        {
            MyVector vector = new MyVector(IShips.Direction.Horizontal, 5);
            Ship ship = new Ship(new Coordinate(0, 0), vector);
            List<IShips> list = new List<IShips>();
            list.Add(ship);
            Battlefield battlefield = new Battlefield(list);
            Coordinate x = new Coordinate(0, 4);
            battlefield.Shoot(x);
            // Arrange + Act + Assert
            Assert.IsTrue(AIUtils.IsCellShot(battlefield, x));
        }

        [TestMethod]
        public void IsCellShot_WithoutShip_ReturnFalse()
        {
            List<IShips> list = new List<IShips>();
            Battlefield battlefield = new Battlefield(list);
            Coordinate x = new Coordinate(0, 0);
            battlefield.Shoot(x);
            // Arrange + Act + Assert
            Coordinate y = new Coordinate(1, 0);
            Assert.IsFalse(AIUtils.IsCellShot(battlefield, y));
        }
    }
#pragma warning restore NI1007 // Test classes must ultimately inherit from 'AutoTest'
}
