using System;
using System.Collections.Immutable;

namespace RubiksCubeTrainer.Puzzle3x3
{
    [System.Diagnostics.DebuggerDisplay("{FaceName} ({Point3D.X}, {Point3D.Y}, {Point3D.Z})")]
    public struct Location : IEquatable<Location>
    {
        private static readonly ImmutableDictionary<Location, Location> adjacentCorner;
        private static readonly ImmutableDictionary<Location, Location> adjacentEdge;

        static Location()
        {
            // Initialize here so other static initialization is done first.
            adjacentCorner = BuildAdjacentCorners();
            adjacentEdge = BuildAdjacentEdges();
        }

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

        public Location AdjacentCorner => adjacentCorner[this];

        public Location AdjacentEdge => adjacentEdge[this];

        public FaceName FaceName { get; }

        public Point3D Point3D { get; }

        public override string ToString()
            => $"{this.FaceName} ({this.Point3D.X}, {this.Point3D.Y}, {this.Point3D.Z})";

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

        private static ImmutableDictionary<Location, Location> BuildAdjacentEdges()
        {
            var builder = ImmutableDictionary.CreateBuilder<Location, Location>();

            // Top.
            BuildLocations(UpBack, BackUp);
            BuildLocations(UpLeft, LeftUp);
            BuildLocations(UpFront, FrontUp);
            BuildLocations(UpRight, RightUp);

            // Middle.
            BuildLocations(LeftBack, BackLeft);
            BuildLocations(LeftFront, FrontLeft);
            BuildLocations(RightBack, BackRight);
            BuildLocations(RightFront, FrontRight);

            // Down.
            BuildLocations(FrontDown, DownFront);
            BuildLocations(DownBack, BackDown);
            BuildLocations(LeftDown, DownLeft);
            BuildLocations(RightDown, DownRight);

            return builder.ToImmutable();

            void BuildLocations(Location location1, Location location2)
            {
                builder.Add(location1, location2);
                builder.Add(location2, location1);
            }
        }

        private static ImmutableDictionary<Location, Location> BuildAdjacentCorners()
        {
            var builder = ImmutableDictionary.CreateBuilder<Location, Location>();

            // Up corners.
            BuildLocations(UpFrontLeft, LeftFrontUp, FrontLeftUp);
            BuildLocations(UpFrontRight, FrontRightUp, RightFrontUp);
            BuildLocations(UpBackRight, RightBackUp, BackRightUp);
            BuildLocations(UpBackLeft, BackLeftUp, LeftBackUp);

            // Down corners.
            BuildLocations(FrontLeftDown, LeftFrontDown, DownFrontLeft);
            BuildLocations(FrontRightDown, DownFrontRight, RightFrontDown);
            BuildLocations(BackLeftDown, DownBackLeft, LeftBackDown);
            BuildLocations(BackRightDown, RightBackDown, DownBackRight);

            return builder.ToImmutable();

            void BuildLocations(Location location1, Location location2, Location location3)
            {
                builder.Add(location1, location2);
                builder.Add(location2, location3);
                builder.Add(location3, location1);
            }
        }
    }
}