using Torpedo.Interfaces;

namespace Torpedo.Models
{
    // The name Vector will mess up the inner functioning of the WPF

    /// <summary>
    /// This class defines ship size and direction.
    /// </summary>
    public class MyVector
    {
        /// <summary>
        /// This is the direction of the vector.
        /// </summary>
        public IShip.Direction Way { get; private set; }

        /// <summary>
        /// This is the size of the vector.
        /// </summary>
        public int Size { get; private set; }

        /// <summary>
        /// Constructor of MyVector.
        /// </summary>
        /// <param name="way"><see cref="IShip.Direction"/>: Direction of the ship.</param>
        /// <param name="size">int: Size of the ship.</param>
        public MyVector(IShip.Direction way, int size)
        {
            Way = way;
            Size = size;
        }
    }
}
