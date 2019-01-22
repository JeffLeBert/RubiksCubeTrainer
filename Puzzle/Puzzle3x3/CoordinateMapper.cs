using System;
using System.Collections.Immutable;

namespace RubiksCubeTrainer.Puzzle3x3
{
    /// <summary>
    /// Maps the 3D co-ordinates of the puzzle into the 2D co-ordinates of a face.
    /// </summary>
    public abstract class CoordinateMapper
    {
        // This must be in the same order as FaceName.
        private static readonly ImmutableArray<CoordinateMapper> coordinateMappers = ImmutableArray.Create<CoordinateMapper>(
            new ZFaceMapper(FaceName.Up, 1),
            new YFaceMapper(FaceName.Front, -1),
            new ZFaceMapper(FaceName.Down, -1),
            new YFaceMapper(FaceName.Back, 1),
            new XFaceMapper(FaceName.Left, -1),
            new XFaceMapper(FaceName.Right, 1));

        private CoordinateMapper(FaceName faceName, int faceSide)
        {
            this.FaceName = faceName;
            this.FaceSide = faceSide;
        }

        public Point3D CenterPoint => this.Map(new Point2D(0, 0));

        public FaceName FaceName { get; }

        public int FaceSide { get; }

        public static FaceName GetFaceFromCenterPoint(Point3D centerPoint)
        {
            if (centerPoint.X == -1)
            {
                return FaceName.Left;
            }

            if (centerPoint.X == 1)
            {
                return FaceName.Right;
            }

            if (centerPoint.Y == -1)
            {
                return FaceName.Front;
            }

            if (centerPoint.Y == 1)
            {
                return FaceName.Back;
            }

            if (centerPoint.Z == -1)
            {
                return FaceName.Down;
            }

            if (centerPoint.Z == 1)
            {
                return FaceName.Up;
            }

            throw new InvalidOperationException();
        }

        public static CoordinateMapper GetMapperForFace(FaceName faceName)
            => coordinateMappers[(int)faceName];

        public abstract FaceName GetAdjacentFaceForEdge(Point2D edge);

        public abstract Point2D Map(Point3D point3D);

        public abstract Point3D Map(Point2D point2D);

        private class XFaceMapper : CoordinateMapper
        {
            public XFaceMapper(FaceName faceName, int faceSide) : base(faceName, faceSide)
            {
            }

            public override FaceName GetAdjacentFaceForEdge(Point2D edge)
                => GetFaceFromCenterPoint(new Point3D(0, edge.X, edge.Y));

            public override Point2D Map(Point3D point3D) => new Point2D(point3D.Y, point3D.Z);

            public override Point3D Map(Point2D point2D) => new Point3D(this.FaceSide, point2D.X, point2D.Y);
        }

        private class YFaceMapper : CoordinateMapper
        {
            public YFaceMapper(FaceName faceName, int faceSide) : base(faceName, faceSide)
            {
            }

            public override FaceName GetAdjacentFaceForEdge(Point2D edge)
                => GetFaceFromCenterPoint(new Point3D(edge.X, 0, edge.Y));

            public override Point2D Map(Point3D point3D) => new Point2D(point3D.X, point3D.Z);

            public override Point3D Map(Point2D point2D) => new Point3D(point2D.X, this.FaceSide, point2D.Y);
        }

        private class ZFaceMapper : CoordinateMapper
        {
            public ZFaceMapper(FaceName faceName, int faceSide) : base(faceName, faceSide)
            {
            }

            public override FaceName GetAdjacentFaceForEdge(Point2D edge)
                => GetFaceFromCenterPoint(new Point3D(edge.X, edge.Y, 0));

            public override Point2D Map(Point3D point3D) => new Point2D(point3D.X, point3D.Y);

            public override Point3D Map(Point2D point2D) => new Point3D(point2D.X, point2D.Y, this.FaceSide);
        }
    }
}