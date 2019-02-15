using System;

namespace RubiksCubeTrainer.Puzzle3x3
{
    [System.Diagnostics.DebuggerDisplay("{FaceName} ({Point3D.X}, {Point3D.Y}, {Point3D.Z})")]
    public struct Location : IEquatable<Location>
    {
        public Location(FaceName faceName, int x, int y, int z)
            : this(faceName, new Point3D(x, y, z))
        {
        }

        public Location(FaceName faceName, Point3D point)
        {
            this.FaceName = faceName;
            this.Point3D = point;
        }

        public static Location Back { get; } = new Location(FaceName.Back, 0, 1, 0);
        public static Location BackLeft { get; } = new Location(FaceName.Back, -1, 1, 0);
        public static Location BackLeftUp { get; } = new Location(FaceName.Back, -1, 1, 1);
        public static Location BackLeftDown { get; } = new Location(FaceName.Back, -1, 1, -1);
        public static Location BackDown { get; } = new Location(FaceName.Back, 0, 1, -1);
        public static Location BackUp { get; } = new Location(FaceName.Back, 0, 1, 1);
        public static Location BackRight { get; } = new Location(FaceName.Back, 1, 1, 0);
        public static Location BackRightUp { get; } = new Location(FaceName.Back, 1, 1, 1);
        public static Location BackRightDown { get; } = new Location(FaceName.Back, 1, 1, -1);

        public static Location Down { get; } = new Location(FaceName.Down, 0, 0, -1);
        public static Location DownFront { get; } = new Location(FaceName.Down, 0, -1, -1);
        public static Location DownFrontLeft { get; } = new Location(FaceName.Down, -1, -1, -1);
        public static Location DownFrontRight { get; } = new Location(FaceName.Down, 1, -1, -1);
        public static Location DownLeft { get; } = new Location(FaceName.Down, -1, 0, -1);
        public static Location DownRight { get; } = new Location(FaceName.Down, 1, 0, -1);
        public static Location DownBack { get; } = new Location(FaceName.Down, 0, 1, -1);
        public static Location DownBackLeft { get; } = new Location(FaceName.Down, -1, 1, -1);
        public static Location DownBackRight { get; } = new Location(FaceName.Down, 1, 1, -1);

        public static Location Front { get; } = new Location(FaceName.Front, 0, -1, 0);
        public static Location FrontLeft { get; } = new Location(FaceName.Front, -1, -1, 0);
        public static Location FrontLeftUp { get; } = new Location(FaceName.Front, -1, -1, 1);
        public static Location FrontLeftDown { get; } = new Location(FaceName.Front, -1, -1, -1);
        public static Location FrontRight { get; } = new Location(FaceName.Front, 1, -1, 0);
        public static Location FrontRightUp { get; } = new Location(FaceName.Front, 1, -1, 1);
        public static Location FrontRightDown { get; } = new Location(FaceName.Front, 1, -1, -1);
        public static Location FrontUp { get; } = new Location(FaceName.Front, 0, -1, 1);
        public static Location FrontDown { get; } = new Location(FaceName.Front, 0, -1, -1);

        public static Location Left { get; } = new Location(FaceName.Left, -1, 0, 0);
        public static Location LeftBack { get; } = new Location(FaceName.Left, -1, 1, 0);
        public static Location LeftBackUp { get; } = new Location(FaceName.Left, -1, 1, 1);
        public static Location LeftBackDown { get; } = new Location(FaceName.Left, -1, 1, -1);
        public static Location LeftFront { get; } = new Location(FaceName.Left, -1, -1, 0);
        public static Location LeftFrontUp { get; } = new Location(FaceName.Left, -1, -1, 1);
        public static Location LeftFrontDown { get; } = new Location(FaceName.Left, -1, -1, -1);
        public static Location LeftUp { get; } = new Location(FaceName.Left, -1, 0, 1);
        public static Location LeftDown { get; } = new Location(FaceName.Left, -1, 0, -1);

        public static Location Right { get; } = new Location(FaceName.Right, 1, 0, 0);
        public static Location RightBack { get; } = new Location(FaceName.Right, 1, 1, 0);
        public static Location RightBackUp { get; } = new Location(FaceName.Right, 1, 1, 1);
        public static Location RightBackDown { get; } = new Location(FaceName.Right, 1, 1, -1);
        public static Location RightFront { get; } = new Location(FaceName.Right, 1, -1, 0);
        public static Location RightFrontUp { get; } = new Location(FaceName.Right, 1, -1, 1);
        public static Location RightFrontDown { get; } = new Location(FaceName.Right, 1, -1, -1);
        public static Location RightUp { get; } = new Location(FaceName.Right, 1, 0, 1);
        public static Location RightDown { get; } = new Location(FaceName.Right, 1, 0, -1);

        public static Location Up { get; } = new Location(FaceName.Up, 0, 0, 1);
        public static Location UpBack { get; } = new Location(FaceName.Up, 0, 1, 1);
        public static Location UpBackLeft { get; } = new Location(FaceName.Up, -1, 1, 1);
        public static Location UpBackRight { get; } = new Location(FaceName.Up, 1, 1, 1);
        public static Location UpFront { get; } = new Location(FaceName.Up, 0, -1, 1);
        public static Location UpFrontLeft { get; } = new Location(FaceName.Up, -1, -1, 1);
        public static Location UpFrontRight { get; } = new Location(FaceName.Up, 1, -1, 1);
        public static Location UpLeft { get; } = new Location(FaceName.Up, -1, 0, 1);
        public static Location UpRight { get; } = new Location(FaceName.Up, 1, 0, 1);

        public FaceName FaceName { get; }

        public Point3D Point3D { get; }

        public override bool Equals(object obj)
            => obj is Location location && this.Equals(location);

        public bool Equals(Location other)
            => this.FaceName == other.FaceName && this.Point3D == other.Point3D;

        public override int GetHashCode()
            => 955250357
            * (-1521134295 + this.FaceName.GetHashCode())
            * (-1521134295 + this.Point3D.GetHashCode());

        public static bool operator ==(Location left, Location right)
            => left.Equals(right);

        public static bool operator !=(Location left, Location right)
            => !(left == right);
    }
}