namespace Torpedo.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Torpedo.AIModule;
    using Torpedo.Interfaces;
    using Torpedo.Models;

    /// <summary>
    /// Tests the <see cref="AI"/>.
    /// </summary>
    [TestClass]
    public class AITest
    {
        /// <summary>
        /// Tests analyze after a missed random shot, with no plans.
        /// </summary>
        [TestMethod]
        public void Analyze_RandomMissedNotPlanned_Random()
        {
            // Arrange
            AI ai = new AI();
            ai.SetEnemyBattlefield(new Battlefield(new List<IShip>()));
            ai.ShotHistory.AddLast((new Coordinate(5, 5), false, PlayStyle.Random));

            // Act
            ai.Analyze();

            // Assert
            Assert.AreEqual(ai.PlayStyle, PlayStyle.Random);
        }

        /// <summary>
        /// Tests analyze after a missed random shot, but does have plan. This should not happen.
        /// </summary>
        [TestMethod]
        public void Analyze_RandomMissedWithPlanned_FollowPlan()
        {

            // Arrange
            AI ai = new AI();
            ai.SetEnemyBattlefield(new Battlefield(new List<IShip>()));
            ai.ShotHistory.AddLast((new Coordinate(5, 5), false, PlayStyle.Random));
            ai.Planned.Push((new Coordinate(5, 5), PlayStyle.Random));

            // Act
            ai.Analyze();

            // Assert
            Assert.AreEqual(ai.PlayStyle, PlayStyle.FollowPlan);
        }

        /// <summary>
        /// Tests analyze after a missed Found shot, with no plans.
        /// </summary>
        [TestMethod]
        public void Analyze_FoundMissedNotPlanned_Random()
        {
            // Arrange
            AI ai = new AI();
            ai.SetEnemyBattlefield(new Battlefield(new List<IShip>()));
            ai.ShotHistory.AddLast((new Coordinate(5, 5), false, PlayStyle.Found));

            // Act
            ai.Analyze();

            // Assert
            Assert.AreEqual(ai.PlayStyle, PlayStyle.Random);
        }

        /// <summary>
        /// Tests analyze after a missed Found shot, but does have plan.
        /// </summary>
        [TestMethod]
        public void Analyze_FoundMissedWithPlanned_FollowPlan()
        {
            // Arrange
            AI ai = new AI();
            ai.SetEnemyBattlefield(new Battlefield(new List<IShip>()));
            ai.ShotHistory.AddLast((new Coordinate(5, 5), false, PlayStyle.Found));
            ai.Planned.Push((new Coordinate(5, 5), PlayStyle.Found));

            // Act
            ai.Analyze();

            // Assert
            Assert.AreEqual(ai.PlayStyle, PlayStyle.FollowPlan);
        }

        /// <summary>
        /// Tests analyze after a missed Sink shot, with no plans.
        /// </summary>
        [TestMethod]
        public void Analyze_SinkMissedNotPlanned_Random()
        {
            // Arrange
            AI ai = new AI();
            ai.SetEnemyBattlefield(new Battlefield(new List<IShip>()));
            ai.ShotHistory.AddLast((new Coordinate(5, 5), false, PlayStyle.Sink));

            // Act
            ai.Analyze();

            // Assert
            Assert.AreEqual(ai.PlayStyle, PlayStyle.Random);
        }

        /// <summary>
        /// Tests analyze after a missed Sink shot, but does have plan.
        /// </summary>
        [TestMethod]
        public void Analyze_SinkMissedWithPlanned_FollowPlan()
        {
            // Arrange
            AI ai = new AI();
            ai.SetEnemyBattlefield(new Battlefield(new List<IShip>()));
            ai.ShotHistory.AddLast((new Coordinate(5, 5), false, PlayStyle.Sink));
            ai.Planned.Push((new Coordinate(5, 5), PlayStyle.Found));

            // Act
            ai.Analyze();

            // Assert
            Assert.AreEqual(ai.PlayStyle, PlayStyle.FollowPlan);
        }

        /// <summary>
        /// Tests analyze after a hit Random shot, has no plan.
        /// </summary>
        [TestMethod]
        public void Analyze_HitWithRandom_Found()
        {
            // Arrange
            AI ai = new AI();
            ai.SetEnemyBattlefield(new Battlefield(new List<IShip>()));
            ai.ShotHistory.AddLast((new Coordinate(5, 5), true, PlayStyle.Random));

            // Act
            ai.Analyze();

            // Assert
            Assert.AreEqual(ai.PlayStyle, PlayStyle.Found);
        }

        /// <summary>
        /// Tests analyze after a hit Random shot, has plan. This should not happen.
        /// </summary>
        [TestMethod]
        public void Analyze_HitWithRandomWithPlanned_Found()
        {
            // Arrange
            AI ai = new AI();
            ai.SetEnemyBattlefield(new Battlefield(new List<IShip>()));
            ai.ShotHistory.AddLast((new Coordinate(5, 5), true, PlayStyle.Random));
            ai.Planned.Push((new Coordinate(5, 5), PlayStyle.Random));

            // Act
            ai.Analyze();

            // Assert
            Assert.AreEqual(ai.PlayStyle, PlayStyle.Found);
        }

        /// <summary>
        /// Tests analyze after a hit Random shot, but it has bad coordinates.
        /// </summary>
        [TestMethod]
        public void Analyze_HitWithBadRandom_Random()
        {
            // Arrange
            AI ai = new AI();
            ai.SetEnemyBattlefield(new Battlefield(new List<IShip>()));
            ai.ShotHistory.AddLast((new Coordinate(-2, 1), true, PlayStyle.Random));

            // Act
            ai.Analyze();

            // Assert
            Assert.AreEqual(ai.PlayStyle, PlayStyle.Random);
        }

        /// <summary>
        /// Tests analyze after a hit a Random then a Found shot.
        /// </summary>
        [TestMethod]
        public void Analyze_HitWithFound_Sink()
        {

            // Arrange
            AI ai = new AI();
            ai.SetEnemyBattlefield(new Battlefield(new List<IShip>()));
            ai.ShotHistory.AddLast((new Coordinate(5, 4), true, PlayStyle.Random));
            ai.ShotHistory.AddLast((new Coordinate(5, 5), true, PlayStyle.Found));

            // Act
            ai.Analyze();

            // Assert
            Assert.AreEqual(ai.PlayStyle, PlayStyle.Sink);
        }

        /// <summary>
        /// Tests analyze after a hit a Random then a Found shot.
        /// </summary>
        [TestMethod]
        public void Analyze_HitWithFoundAfterMiss_Sink()
        {
            // Arrange
            AI ai = new AI();
            ai.SetEnemyBattlefield(new Battlefield(new List<IShip>()));
            ai.ShotHistory.AddLast((new Coordinate(5, 4), true, PlayStyle.Random));
            ai.ShotHistory.AddLast((new Coordinate(0, 0), true, PlayStyle.Found));
            ai.ShotHistory.AddLast((new Coordinate(5, 5), true, PlayStyle.Found));

            // Act
            ai.Analyze();

            // Assert
            Assert.AreEqual(ai.PlayStyle, PlayStyle.Sink);
        }

        /// <summary>
        /// Tests analyze after a hit a Random then a Sink shot.
        /// </summary>
        [TestMethod]
        public void Analyze_HitWithSink_Sink()
        {
            // Arrange
            AI ai = new AI();
            ai.SetEnemyBattlefield(new Battlefield(new List<IShip>()));
            ai.ShotHistory.AddLast((new Coordinate(5, 6), true, PlayStyle.Random));
            ai.ShotHistory.AddLast((new Coordinate(5, 5), true, PlayStyle.Sink));

            // Act
            ai.Analyze();

            // Assert
            Assert.AreEqual(ai.PlayStyle, PlayStyle.Sink);
        }

        /// <summary>
        /// Tests analyze after a hit a Random then a Sink shot.
        /// </summary>
        [TestMethod]
        public void Analyze_HitWithSinkAfterMiss_Sink()
        {
            // Arrange
            AI ai = new AI();
            ai.SetEnemyBattlefield(new Battlefield(new List<IShip>()));
            ai.ShotHistory.AddLast((new Coordinate(5, 6), true, PlayStyle.Random));
            ai.ShotHistory.AddLast((new Coordinate(4, 6), true, PlayStyle.Found));
            ai.ShotHistory.AddLast((new Coordinate(5, 5), true, PlayStyle.Sink));

            // Act
            ai.Analyze();

            // Assert
            Assert.AreEqual(ai.PlayStyle, PlayStyle.Sink);
        }

        /// <summary>
        /// Tests analyze after a hit Found shot, but the coordinate matches with the last random hit.
        /// </summary>
        [TestMethod]
        public void Analyze_HitWithFoundCoordinateMatched_Random()
        {
            // Arrange
            AI ai = new AI();
            ai.SetEnemyBattlefield(new Battlefield(new List<IShip>()));
            ai.ShotHistory.AddLast((new Coordinate(5, 5), true, PlayStyle.Random));
            ai.ShotHistory.AddLast((new Coordinate(5, 5), true, PlayStyle.Found));

            // Act
            ai.Analyze();

            // Assert
            Assert.AreEqual(ai.PlayStyle, PlayStyle.Random);
        }

        /// <summary>
        /// Tests analyze after a hit Found shot, but the coordinate matches with the last random hit.
        /// </summary>
        [TestMethod]
        public void Analyze_HitWithFoundCoordinateMatchedExtraMissed_Random()
        {
            // Arrange
            AI ai = new AI();
            ai.SetEnemyBattlefield(new Battlefield(new List<IShip>()));
            ai.ShotHistory.AddLast((new Coordinate(5, 5), true, PlayStyle.Random));
            ai.ShotHistory.AddLast((new Coordinate(5, 4), false, PlayStyle.Found));
            ai.ShotHistory.AddLast((new Coordinate(5, 5), true, PlayStyle.Found));

            // Act
            ai.Analyze();

            // Assert
            Assert.AreEqual(ai.PlayStyle, PlayStyle.Random);
        }

        /// <summary>
        /// Tests analyze after a hit Found shot, but the coordinate matches with the last random hit.
        /// </summary>
        [TestMethod]
        public void Analyze_HitWithFoundCoordinateMatchedExtraHit_Random()
        {
            // Arrange
            AI ai = new AI();
            ai.SetEnemyBattlefield(new Battlefield(new List<IShip>()));
            ai.ShotHistory.AddLast((new Coordinate(5, 5), true, PlayStyle.Random));
            ai.ShotHistory.AddLast((new Coordinate(5, 4), true, PlayStyle.Found));
            ai.ShotHistory.AddLast((new Coordinate(5, 5), true, PlayStyle.Found));

            // Act
            ai.Analyze();

            // Assert
            Assert.AreEqual(ai.PlayStyle, PlayStyle.Random);
        }

        /// <summary>
        /// Tests analyze after a hit Sink shot, but the coordinate matches with the last random hit.
        /// </summary>
        [TestMethod]
        public void Analyze_HitWithSinkCoordinateMatched_Random()
        {
            // Arrange
            AI ai = new AI();
            ai.SetEnemyBattlefield(new Battlefield(new List<IShip>()));
            ai.ShotHistory.AddLast((new Coordinate(5, 5), true, PlayStyle.Random));
            ai.ShotHistory.AddLast((new Coordinate(5, 5), true, PlayStyle.Sink));

            // Act
            ai.Analyze();

            // Assert
            Assert.AreEqual(ai.PlayStyle, PlayStyle.Random);
        }

        /// <summary>
        /// Tests analyze after a hit Sink shot, but the coordinate matches with the last random hit.
        /// </summary>
        [TestMethod]
        public void Analyze_HitWithSinkCoordinateMatchedWithExtraMissed_Random()
        {
            // Arrange
            AI ai = new AI();
            ai.SetEnemyBattlefield(new Battlefield(new List<IShip>()));
            ai.ShotHistory.AddLast((new Coordinate(5, 5), true, PlayStyle.Random));
            ai.ShotHistory.AddLast((new Coordinate(6, 5), false, PlayStyle.Found));
            ai.ShotHistory.AddLast((new Coordinate(5, 5), true, PlayStyle.Sink));

            // Act
            ai.Analyze();

            // Assert
            Assert.AreEqual(ai.PlayStyle, PlayStyle.Random);
        }

        /// <summary>
        /// Tests analyze after a hit Sink shot, but the coordinate matches with the last random hit.
        /// </summary>
        [TestMethod]
        public void Analyze_HitWithSinkCoordinateMatchedWithExtraHit_Random()
        {
            // Arrange
            AI ai = new AI();
            ai.SetEnemyBattlefield(new Battlefield(new List<IShip>()));
            ai.ShotHistory.AddLast((new Coordinate(5, 5), true, PlayStyle.Random));
            ai.ShotHistory.AddLast((new Coordinate(6, 5), true, PlayStyle.Found));
            ai.ShotHistory.AddLast((new Coordinate(5, 5), true, PlayStyle.Sink));

            // Act
            ai.Analyze();

            // Assert
            Assert.AreEqual(ai.PlayStyle, PlayStyle.Random);
        }

        /// <summary>
        /// Tests analyze after a hit Found shot, but the previous Random has bad Coordinate.
        /// </summary>
        [TestMethod]
        public void Analyze_HitWithFoundDiagonal_Random()
        {
            // Arrange
            AI ai = new AI();
            ai.SetEnemyBattlefield(new Battlefield(new List<IShip>()));
            ai.ShotHistory.AddLast((new Coordinate(4, 6), true, PlayStyle.Random));
            ai.ShotHistory.AddLast((new Coordinate(5, 5), true, PlayStyle.Found));

            // Act
            ai.Analyze();

            // Assert
            Assert.AreEqual(ai.PlayStyle, PlayStyle.Random);
        }

        /// <summary>
        /// Tests analyze after a hit Found shot, but the previous Random has bad Coordinate.
        /// </summary>
        [TestMethod]
        public void Analyze_HitWithFoundDiagonalExtraHit_Random()
        {
            // Arrange
            AI ai = new AI();
            ai.SetEnemyBattlefield(new Battlefield(new List<IShip>()));
            ai.ShotHistory.AddLast((new Coordinate(4, 6), true, PlayStyle.Random));
            ai.ShotHistory.AddLast((new Coordinate(4, 0), true, PlayStyle.Found));
            ai.ShotHistory.AddLast((new Coordinate(5, 5), true, PlayStyle.Found));

            // Act
            ai.Analyze();

            // Arrange
            Assert.AreEqual(ai.PlayStyle, PlayStyle.Random);
        }

        /// <summary>
        /// Tests analyze after a hit Found shot, but the previous Random has bad Coordinate.
        /// </summary>
        [TestMethod]
        public void Analyze_HitWithFoundDiagonalExtraMiss_Random()
        {
            // Arrange
            AI ai = new AI();
            ai.SetEnemyBattlefield(new Battlefield(new List<IShip>()));
            ai.ShotHistory.AddLast((new Coordinate(4, 6), true, PlayStyle.Random));
            ai.ShotHistory.AddLast((new Coordinate(4, 5), false, PlayStyle.Sink));
            ai.ShotHistory.AddLast((new Coordinate(5, 5), true, PlayStyle.Found));

            // Act
            ai.Analyze();

            // Assert
            Assert.AreEqual(ai.PlayStyle, PlayStyle.Random);
        }

        /// <summary>
        /// Tests analyze after a hit Sink shot, but the previous Random has bad Coordinate.
        /// </summary>
        [TestMethod]
        public void Analyze_HitWithSinkDiagonal_Random()
        {
            // Arrange
            AI ai = new AI();
            ai.SetEnemyBattlefield(new Battlefield(new List<IShip>()));
            ai.ShotHistory.AddLast((new Coordinate(6, 4), true, PlayStyle.Random));
            ai.ShotHistory.AddLast((new Coordinate(5, 5), true, PlayStyle.Sink));

            // Act
            ai.Analyze();

            // Assert
            Assert.AreEqual(ai.PlayStyle, PlayStyle.Random);
        }

        /// <summary>
        /// Tests analyze after a hit Sink shot, but the previous Random has bad Coordinate.
        /// </summary>
        [TestMethod]
        public void Analyze_HitWithSinkDiagonalExtraHit_Random()
        {
            // Arrange
            AI ai = new AI();
            ai.SetEnemyBattlefield(new Battlefield(new List<IShip>()));
            ai.ShotHistory.AddLast((new Coordinate(3, 6), true, PlayStyle.Random));
            ai.ShotHistory.AddLast((new Coordinate(5, 7), true, PlayStyle.Found));
            ai.ShotHistory.AddLast((new Coordinate(5, 5), true, PlayStyle.Sink));

            // Act
            ai.Analyze();

            // Assert
            Assert.AreEqual(ai.PlayStyle, PlayStyle.Random);
        }

        /// <summary>
        /// Tests analyze after a hit Sink shot, but the previous Random has bad Coordinate.
        /// </summary>
        [TestMethod]
        public void Analyze_HitWithSinkDiagonalExtraMiss_Random()
        {
            // Arrange
            AI ai = new AI();
            ai.SetEnemyBattlefield(new Battlefield(new List<IShip>()));
            ai.ShotHistory.AddLast((new Coordinate(5, 6), true, PlayStyle.Random));
            ai.ShotHistory.AddLast((new Coordinate(4, 5), false, PlayStyle.Sink));
            ai.ShotHistory.AddLast((new Coordinate(6, 5), true, PlayStyle.Sink));

            // Act
            ai.Analyze();

            // Assert
            Assert.AreEqual(ai.PlayStyle, PlayStyle.Random);
        }
    }
}
