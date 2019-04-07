using System;
using System.Collections.Immutable;

namespace RubiksCubeTrainer.Puzzle3x3
{
    public partial struct Location : IEquatable<Location>
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

        public Location AdjacentCorner => adjacentCorner[this];

        public Location AdjacentEdge => adjacentEdge[this];

        public FaceName FaceName { get; }

        public Point3D Point3D { get; }

        public override string ToString()
            => LocationParser.Format(this);

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
            BuildLocations(UpLeftFront, LeftFrontUp, FrontLeftUp);
            BuildLocations(UpRightFront, FrontRightUp, RightFrontUp);
            BuildLocations(UpRightBack, RightBackUp, BackRightUp);
            BuildLocations(UpLeftBack, BackLeftUp, LeftBackUp);

            // Down corners.
            BuildLocations(FrontLeftDown, LeftFrontDown, DownLeftFront);
            BuildLocations(FrontRightDown, DownRightFront, RightFrontDown);
            BuildLocations(BackLeftDown, DownLeftBack, LeftBackDown);
            BuildLocations(BackRightDown, RightBackDown, DownRightBack);

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