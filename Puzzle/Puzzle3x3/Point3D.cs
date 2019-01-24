using System.Diagnostics;

namespace RubiksCubeTrainer.Puzzle3x3
{
    [DebuggerDisplay("({X}, {Y}, {Z})")]
    public struct Point3D
    {
        public Point3D(int x, int y, int z)
        {
            Debug.Assert(
                x >= -1 && x <= 1 && y >= -1 && y <= 1 && z >= -1 && z <= 1,
                "X, Y and Z co-ordinates must be between -1 and 1.");

            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public int X { get; }

        public int Y { get; }

        public int Z { get; }
    }
}