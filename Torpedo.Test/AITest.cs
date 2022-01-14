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
        /// Tests analyze gets a random last shot, will return the <see cref="PlayStyle.Random"/>.
        /// </summary>
        [TestMethod]
        public void Analyze_FalseEmptyPlanned_ReturnsRandom()
        {
            AI ai = new AI();
            ai.SetEnemyBattlefield(new Battlefield(new List<IShip>()));
            ai.ShotHistory.AddLast((new Coordinate(5, 5), false, PlayStyle.Random));
            ai.Analyze();
            Assert.AreEqual(ai.PlayStyle, PlayStyle.Random);
        }

        /// <summary>
        /// Tests analyze gets a random last shot, will return the <see cref="PlayStyle.FollowPlan"/>.
        /// </summary>
        [TestMethod]
        public void Analyze_FalseWidthPlanned_ReturnsRandom()
        {
            AI ai = new AI();
            ai.SetEnemyBattlefield(new Battlefield(new List<IShip>()));
            ai.ShotHistory.AddLast((new Coordinate(5, 5), false, PlayStyle.Random));
            ai.Planned.Push((new Coordinate(5, 5), PlayStyle.Random));
            ai.Analyze();
            Assert.AreEqual(ai.PlayStyle, PlayStyle.FollowPlan);
        }

        /// <summary>
        /// Tests analyze gets a random last shot, will return the <see cref="PlayStyle.Found"/>.
        /// </summary>
        [TestMethod]
        public void Analyze_TrueWidthRandom_ReturnsFound()
        {
            AI ai = new AI();
            ai.SetEnemyBattlefield(new Battlefield(new List<IShip>()));
            ai.ShotHistory.AddLast((new Coordinate(5, 5), true, PlayStyle.Random));
            ai.Planned.Push((new Coordinate(5, 5), PlayStyle.Random));
            ai.Analyze();
            Assert.AreEqual(ai.PlayStyle, PlayStyle.Found);
        }

        /// <summary>
        /// Tests analyze gets a random last shot, will return the <see cref="PlayStyle.Random"/>.
        /// </summary>
        [TestMethod]
        public void Analyze_TrueWidthRandom_ReturnsRandom()
        {
            AI ai = new AI();
            ai.SetEnemyBattlefield(new Battlefield(new List<IShip>()));
            ai.ShotHistory.AddLast((new Coordinate(-2, -2), true, PlayStyle.Random));
            //ai.Planned.Enqueue((new Coordinate(-1, -1), PlayStyle.Random));
            ai.Analyze();
            Assert.AreEqual(ai.PlayStyle, PlayStyle.Random);
        }

        /// <summary>
        /// Tests analyze gets a random last shot, will return the <see cref="PlayStyle.Sink"/>.
        /// </summary>
        [TestMethod]
        public void Analyze_TrueWidthFoundCoordinateMatched_ReturnsSink()
        {
            AI ai = new AI();
            ai.SetEnemyBattlefield(new Battlefield(new List<IShip>()));
            ai.ShotHistory.AddLast((new Coordinate(5, 5), true, PlayStyle.Found));
            ai.Planned.Push((new Coordinate(5, 5), PlayStyle.Random));
            ai.Analyze();
            Assert.AreEqual(ai.PlayStyle, PlayStyle.Sink);
        }

        /// <summary>
        /// Tests analyze gets a random last shot, will return the <see cref="PlayStyle.Sink"/>.
        /// </summary>
        [TestMethod]
        public void Analyze_TrueWidthSinkCoordinateMatched_ReturnsSink()
        {
            AI ai = new AI();
            ai.SetEnemyBattlefield(new Battlefield(new List<IShip>()));
            ai.ShotHistory.AddLast((new Coordinate(5, 5), true, PlayStyle.Sink));
            ai.Planned.Push((new Coordinate(5, 5), PlayStyle.Random));
            ai.Analyze();
            Assert.AreEqual(ai.PlayStyle, PlayStyle.Sink);
        }

        /// <summary>
        /// Tests analyze gets a random last shot, will return the <see cref="PlayStyle.Sink"/>.
        /// </summary>
        [TestMethod]
        public void Analyze_TrueWidthFoundDiagonal_ReturnsSink()
        {
            AI ai = new AI();
            ai.SetEnemyBattlefield(new Battlefield(new List<IShip>()));
            ai.ShotHistory.AddLast((new Coordinate(5, 5), true, PlayStyle.Found));
            ai.Planned.Push((new Coordinate(5, 5), PlayStyle.Random));
            ai.Analyze();
            Assert.AreEqual(ai.PlayStyle, PlayStyle.Sink);
        }

        /// <summary>
        /// Tests analyze gets a random last shot, will return the <see cref="PlayStyle.Sink"/>.
        /// </summary>
        [TestMethod]
        public void Analyze_TrueWidthSinkDiagonal_ReturnsSink()
        {
            AI ai = new AI();
            ai.SetEnemyBattlefield(new Battlefield(new List<IShip>()));
            ai.ShotHistory.AddLast((new Coordinate(5, 5), true, PlayStyle.Sink));
            ai.Planned.Push((new Coordinate(5, 5), PlayStyle.Random));
            ai.Analyze();
            Assert.AreEqual(ai.PlayStyle, PlayStyle.Sink);
        }
    }
}
