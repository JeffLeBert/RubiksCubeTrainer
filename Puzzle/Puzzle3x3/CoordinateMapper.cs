using System;
using System.Collections.Immutable;

namespace RubiksCubeTrainer.Puzzle3x3
{
    /// <summary>
    /// Maps the 3D co-ordinates of the puzzle into the 2D co-ordinates of a face.
    /// </summary>
    public static class CoordinateMapper
    {
        private static readonly ImmutableDictionary<Location, Location> adjacentEdge = BuildAdjacentEdges();

        public static Location GetAdjacentEdge(Location location)
            => adjacentEdge[location];

        public static Point2D Map(Location location)
        {
            var point3D = location.Point3D;
            switch (location.FaceName)
            {
                case FaceName.Up:
                case FaceName.Down:
                    return new Point2D(point3D.X, point3D.Y);

                case FaceName.Front:
                case FaceName.Back:
                    return new Point2D(point3D.X, point3D.Z);

                case FaceName.Left:
                case FaceName.Right:
                    return new Point2D(point3D.Y, point3D.Z);

                default:
                    throw new InvalidOperationException();
            }
        }

        private static ImmutableDictionary<Location, Location> BuildAdjacentEdges()
        {
            var builder = ImmutableDictionary.CreateBuilder<Location, Location>();

            // Up face.
            builder.Add(Location.UpBack, Location.BackUp);
            builder.Add(Location.UpLeft, Location.LeftUp);
            builder.Add(Location.UpRight, Location.RightUp);
            builder.Add(Location.UpFront, Location.FrontUp);

            // Front face.
            builder.Add(Location.FrontUp, Location.UpFront);
            builder.Add(Location.FrontLeft, Location.LeftFront);
            builder.Add(Location.FrontRight, Location.RightFront);
            builder.Add(Location.FrontDown, Location.DownFront);

            // Down face.
            builder.Add(Location.DownFront, Location.FrontDown);
            builder.Add(Location.DownLeft, Location.LeftDown);
            builder.Add(Location.DownRight, Location.RightDown);
            builder.Add(Location.DownBack, Location.BackDown);

            // Back face.
            builder.Add(Location.BackUp, Location.UpBack);
            builder.Add(Location.BackLeft, Location.LeftBack);
            builder.Add(Location.BackRight, Location.RightBack);
            builder.Add(Location.BackDown, Location.DownBack);

            // Left face.
            builder.Add(Location.LeftUp, Location.UpLeft);
            builder.Add(Location.LeftBack, Location.BackLeft);
            builder.Add(Location.LeftFront, Location.FrontLeft);
            builder.Add(Location.LeftDown, Location.DownLeft);

            // Right face.
            builder.Add(Location.RightUp, Location.UpRight);
            builder.Add(Location.RightBack, Location.BackRight);
            builder.Add(Location.RightFront, Location.FrontRight);
            builder.Add(Location.RightDown, Location.DownRight);

            return builder.ToImmutable();
        }
    }
}