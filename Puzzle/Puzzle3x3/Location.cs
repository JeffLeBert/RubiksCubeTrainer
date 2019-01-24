using System;

namespace RubiksCubeTrainer.Puzzle3x3
{
    [System.Diagnostics.DebuggerDisplay("{FaceName} ({Point3D.X}, {Point3D.Y}, {Point3D.Z})")]
    public class Location
    {
        private readonly Lazy<Point2D> lazyPoint2D;

        public Location(FaceName faceName, int x, int y, int z)
            : this(faceName, new Point3D(x, y, z))
        {
        }

        public Location(FaceName faceName, Point3D point)
        {
            this.FaceName = faceName;
            this.Point3D = point;

            this.lazyPoint2D = new Lazy<Point2D>(() => CoordinateMapper.GetMapperForFace(faceName).Map(point));
        }

        public FaceName FaceName { get; }

        public bool IsCenter
        {
            get
            {
                var point = this.Point2D;
                return point.X == 0 && point.Y == 0;
            }
        }

        public bool IsEdge
        {
            get
            {
                var point = this.Point2D;
                return (point.X == 0 && point.Y != 0)
                    || (point.X != 0 && point.Y == 0);
            }
        }

        public bool IsCorner
        {
            get
            {
                return this.Point3D.X != 0 && this.Point3D.Y != 0 && this.Point3D.Z != 0;
            }
        }

        public Point2D Point2D
            => this.lazyPoint2D.Value;

        public Point3D Point3D { get; }
    }
}