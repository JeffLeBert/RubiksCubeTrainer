using System;
using System.Diagnostics;

namespace RubiksCubeTrainer.Puzzle3x3
{
    [DebuggerDisplay("({X}, {Y}, {Z})")]
    public struct Point3D : IEquatable<Point3D>
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

        public override bool Equals(object obj)
            => obj is Point3D point && this.Equals(point);

        public bool Equals(Point3D other)
            => this.X == other.X
            && this.Y == other.Y
            && this.Z == other.Z;

        public override int GetHashCode()
            => -307843816
            * (-1521134295 + this.X.GetHashCode())
            * (-1521134295 + this.Y.GetHashCode())
            * (-1521134295 + this.Z.GetHashCode());

        public static bool operator ==(Point3D left, Point3D right)
            => left.Equals(right);

        public static bool operator !=(Point3D left, Point3D right)
            => !(left == right);
    }
}