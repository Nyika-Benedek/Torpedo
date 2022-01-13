using System;
using System.Collections.Generic;
using System.Linq;
using Torpedo.AIModule;
using Torpedo.Interfaces;

namespace Torpedo.Models
{
    /// <summary>
    /// Represents the a playstyle of the AI
    /// </summary>
    public enum PlayStyle { Random, Found, Sink, FollowPlan }
    /// <summary>
    /// This class provides an agent to act like a player
    /// </summary>
    public class AI : Player
    {
        /// <summary>
        /// There are 3 different behivaur the AI could act
        /// </summary>
        public LinkedList<(Coordinate, bool, PlayStyle)> ShotHistory { get; } = new LinkedList<(Coordinate, bool, PlayStyle)>();
        // TODO: NI: replace Ships with method
        public List<IShips> Ships { get; private set; } = new List<IShips>(4);
        private PlayStyle _playStyle = PlayStyle.Random;
        private static readonly string _aiName = "AI";

        // TODO: NI: convert (Coordinate, Playstyle) with struct
        public Queue<(Coordinate, PlayStyle)> Planned { get; } = new Queue<(Coordinate, PlayStyle)>();

        /// <summary>
        /// Constructor of <see cref="AI"/>, its sets the first behivour of the agent
        /// </summary>
        public AI() : base(_aiName)
        {
        }

        /// <summary>
        /// Calls an AI behivour based on the current agent playstyle, and the agent send an advised position to shoot at
        /// </summary>
        public bool Act()
        {
            AILogic logic = new RandomAILogic(EnemyBattlefield);

            if (ShotHistory.Count != 0)
            {
                logic = Analyze();
                StorePlan(logic.Plan(), _playStyle);
            }

            if (Planned.Count == 0) // after all these thinking theres still no plan.. shoot randomly
            {
                logic = new RandomAILogic(EnemyBattlefield); // comes up with random coordinates
                StorePlan(logic.Plan(), PlayStyle.Random);
            }

            return ExecutePlan();

        }

        private Ship GenerateRandomShip(int size)
        {
            return new Ship(
                AIUtils.RandomCoordinate(),
                new MyVector(AIUtils.Random.Next(0, 2) == 0 ? IShips.Direction.Horizontal : IShips.Direction.Vertical, size));
        }

        /// <summary>
        /// Generating the AI's ships, then adding to it's battlefield
        /// </summary>
        public void GenerateShips()
        {
            for (int i = 2; i <= 5; i++)
            {
                Ship ship;
                do
                {
                    ship = GenerateRandomShip(i);
                }
                while (!BattlefieldBuilder.TryToAddShip(ship));
            }
            Ships.AddRange(BattlefieldBuilder.Ships);
        }

        private bool ExecutePlan()
        {
            Coordinate advised;
            PlayStyle reason;
            (advised, reason) = Planned.Dequeue();
            bool isHit = EnemyBattlefield.Shoot(advised);
            ShotHistory.AddLast(new LinkedListNode<(Coordinate, bool, PlayStyle)>((advised, isHit, reason)));
            return isHit;
        }

        private void StorePlan(List<Coordinate> coordinates, PlayStyle reason)
        {
            foreach (Coordinate coordinate in coordinates)
            {
                Planned.Enqueue((coordinate, reason));
            }
        }

        private AILogic Analyze()
        {
            AILogic logic = new RandomAILogic(EnemyBattlefield);
            LinkedListNode<(Coordinate, bool, PlayStyle)> last = ShotHistory.Last;

            if (last.Value.Item2) // Last shot hit
            {
                if (last.Value.Item3 == PlayStyle.Random) // Last shot was random
                {
                    _playStyle = PlayStyle.Found;
                    try
                    {
                        logic = new FoundAILogic(EnemyBattlefield, last.Value.Item1); // comes up with at most four possible ship part location
                    }
                    catch (ArgumentException)
                    {
                        logic = new RandomAILogic(EnemyBattlefield);
                    }
                }
                else
                {
                    _playStyle = PlayStyle.Sink;

                    LinkedListNode<(Coordinate, bool, PlayStyle)> lastRandomHit = last; // the last definitely wasn't random
                    while (lastRandomHit.Value.Item3 != PlayStyle.Random)
                    {
                        lastRandomHit = lastRandomHit.Previous;
                    }

                    try
                    {
                        logic = new SinkAILogic(EnemyBattlefield, last.Value.Item1, lastRandomHit.Value.Item1);
                    }
                    catch (ArgumentException)
                    {
                        logic = new RandomAILogic(EnemyBattlefield);
                    }
                }
            }
            else // The last shot missed
            {
                if (Planned.Count != 0) // Any options?
                {
                    _playStyle = PlayStyle.FollowPlan;
                    logic = new PlannedAILogic(EnemyBattlefield); // does not advise at all, follows the plan
                }
                else // No plan, default back to shooting randomly
                {
                    _playStyle = PlayStyle.Random;
                    logic = new RandomAILogic(EnemyBattlefield); // comes up with random coordinates
                }
            }

            return logic;
        }
    }
}
