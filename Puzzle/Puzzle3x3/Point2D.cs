using System.Diagnostics;

namespace RubiksCubeTrainer.Puzzle3x3
{
    [DebuggerDisplay("({X}, {Y})")]
    public struct Point2D
    {
        public Point2D(int x, int y)
        {
            Debug.Assert(
                x >= -1 && x <= 1 && y >= -1 && y <= 1,
                "X and Y co-ordinates must be between -1 and 1.");

            this.X = x;
            this.Y = y;
        }

        public int X { get; }

        public int Y { get; }
    }
}