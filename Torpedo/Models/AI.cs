using System;
using System.Collections.Generic;
using System.Linq;
using Torpedo.AIModule;
using Torpedo.Interfaces;

namespace Torpedo.Models
{
    /// <summary>
    /// Represents the a playstyle of the AI.
    /// </summary>
    public enum PlayStyle { Random, Found, Sink, FollowPlan }
    /// <summary>
    /// This class provides an agent to act like a player.
    /// </summary>
    public class AI : Player
    {
        /// <summary>
        /// There are 3 different behivaur the AI could act.
        /// </summary>
        public LinkedList<(Coordinate, bool, PlayStyle)> ShotHistory { get; } = new LinkedList<(Coordinate, bool, PlayStyle)>();
        public List<IShips> Ships { get; private set; } = new List<IShips>(4);
        public PlayStyle PlayStyle = PlayStyle.Random;
        private static readonly string _aiName = "AI";
        public Stack<(Coordinate, PlayStyle)> Planned { get; } = new Stack<(Coordinate, PlayStyle)>();

        /// <summary>
        /// Constructor of <see cref="AI"/>, its sets the name of the agent.
        /// </summary>
        public AI() : base(_aiName)
        {
        }

        /// <summary>
        /// The player agent makes its move.
        /// </summary>
        public bool Act()
        {
            AILogic logic = new RandomAILogic(EnemyBattlefield);

            if (ShotHistory.Count != 0)
            {
                logic = Analyze();
                StorePlan(logic.Plan(), PlayStyle);
            }

            if (Planned.Count == 0) // after all these thinking theres still no plan.. shoot randomly
            {
                logic = new RandomAILogic(EnemyBattlefield); // comes up with random coordinates
                StorePlan(logic.Plan(), PlayStyle.Random);
            }

            return ExecutePlan();

        }

        /// <summary>
        /// Generating a randomly lacated <see cref="Ship"/>.
        /// </summary>
        private Ship GenerateRandomShip(int size)
        {
            return new Ship(
                AIUtils.RandomCoordinate(),
                new MyVector(AIUtils.Random.Next(0, 2) == 0 ? IShips.Direction.Horizontal : IShips.Direction.Vertical, size));
        }

        /// <summary>
        /// Generating the AI's <see cref="Ship"/>, then adding to it's <see cref="Battlefield"/>.
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

        /// <summary>
        /// Gets the last <see cref="Coordinate"/> planned then shoots at it.
        /// <para>Stores the shot for later reference.</para>
        /// </summary>
        /// <returns><see cref="bool"/>true if the shot hit, false otherwise.</returns>
        private bool ExecutePlan()
        {
            Coordinate advised;
            PlayStyle reason;
            (advised, reason) = Planned.Pop();
            bool isHit = EnemyBattlefield.Shoot(advised);
            ShotHistory.AddLast(new LinkedListNode<(Coordinate, bool, PlayStyle)>((advised, isHit, reason)));
            return isHit;
        }

        /// <summary>
        /// Adds the advised shooting location to the Planned <see cref="Stack{T}"/> (LIFO).
        /// </summary>
        /// <param name="coordinates">Collection of <see cref="Coordinate"/> advised.</param>
        /// <param name="reason"> <see cref="PlayStyle"/> for later reference to aid decison.</param>
        private void StorePlan(ICollection<Coordinate> coordinates, PlayStyle reason)
        {
            foreach (Coordinate coordinate in coordinates)
            {
                Planned.Push((coordinate, reason));
            }
        }
        /// <summary>
        /// Decides which <see cref="AILogic"/> should the agent follow.
        /// </summary>
        /// <returns><see cref="AILogic"/> to follow.</returns>
        public AILogic Analyze()
        {
            AILogic logic = new RandomAILogic(EnemyBattlefield);
            LinkedListNode<(Coordinate, bool, PlayStyle)> last = ShotHistory.Last;

            if (last.Value.Item2) // Last shot hit
            {
                if (last.Value.Item3 == PlayStyle.Random) // Last shot was random
                {
                    PlayStyle = PlayStyle.Found;
                    try
                    {
                        logic = new FoundAILogic(EnemyBattlefield, last.Value.Item1); // comes up with at most four possible ship part location
                    }
                    catch (ArgumentException)
                    {
                        PlayStyle = PlayStyle.Random;
                        logic = new RandomAILogic(EnemyBattlefield);
                    }
                }
                else
                {
                    PlayStyle = PlayStyle.Sink;

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
                        PlayStyle = PlayStyle.Random;
                        logic = new RandomAILogic(EnemyBattlefield);
                    }
                }
            }
            else // The last shot missed
            {
                if (Planned.Count != 0) // Any options?
                {
                    PlayStyle = PlayStyle.FollowPlan;
                    logic = new PlannedAILogic(EnemyBattlefield); // does not advise at all, follows the plan
                }
                else // No plan, default back to shooting randomly
                {
                    PlayStyle = PlayStyle.Random;
                    logic = new RandomAILogic(EnemyBattlefield); // comes up with random coordinates
                }
            }

            return logic;
        }
    }
}
