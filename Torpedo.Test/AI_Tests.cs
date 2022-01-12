using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Torpedo.Interfaces;
using Torpedo.Models;

namespace Torpedo.Test
{
    [TestClass]
    public class AI_Tests
    {
        [DataRow(6)]
        [DataRow(7)]
        [DataTestMethod]
        public void IsInField_ReturnTrue(Coordinate x)
        {
            //Arrange + Act + Assert
            Assert.IsTrue(AIUtils.IsInField(x));
        }

        [DataRow(0)]
        [DataRow(10)]
        [DataTestMethod]
        public void IsInField_ReturnFalse(Coordinate x)
        {
            //Arrange + Act + Assert
            Assert.IsFalse(AIUtils.IsInField(x));
        }

        [DataRow(5, 5)]
        [DataRow(4, 4)]
        [DataTestMethod]
        public void IsCellShot_ReturnTrue(IBattlefield x, Coordinate y)
        {
            //Arrange + Act + Assert
            Assert.IsTrue(AIUtils.IsCellShot(x, y));
        }

        [DataRow(4, 6)]
        [DataRow(7, 6)]
        [DataTestMethod]
        public void IsCellShot_ReturnFalse(IBattlefield x, Coordinate y)
        {
            //Arrange + Act + Assert
            Assert.IsFalse(AIUtils.IsCellShot(x, y));
        }


    }
}