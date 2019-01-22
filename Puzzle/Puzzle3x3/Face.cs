using System.Collections.Immutable;

namespace RubiksCubeTrainer.Puzzle3x3
{
    [System.Diagnostics.DebuggerDisplay("{FaceName}: {colors[0]} {colors[1]} {colors[2]} {colors[3]} {colors[4]} {colors[5]} {colors[6]} {colors[7]} {colors[8]}")]
    public class Face
    {
        // These are the indexes of the squares on this face. Remember, the face can be oriented
        // in any fashion.
        // 0 1 2
        // 3 4 5
        // 6 7 8

        private readonly ImmutableArray<PuzzleColor> colors;

        public const int Size = 3;

        public Face(FaceName faceName, PuzzleColor color)
        {
            this.FaceName = faceName;
            this.colors = ImmutableArray.Create(color, color, color, color, color, color, color, color, color);
        }

        public Face(FaceName faceName, ImmutableArray<PuzzleColor> colors)
        {
            this.FaceName = faceName;
            this.colors = colors;
        }

        public PuzzleColor this[int x, int y] => this.colors[GetIndex(new Point2D(x, y))];

        public PuzzleColor this[Point2D point2D] => this.colors[GetIndex(point2D)];

        public PuzzleColor this[Point3D point3D] => this[this.Mapper.Map(point3D)];

        public FaceName FaceName { get; }

        public CoordinateMapper Mapper => CoordinateMapper.GetMapperForFace(this.FaceName);

        public Face With(Point2D point, PuzzleColor color)
            => new Face(
                this.FaceName,
                this.colors.SetItem(GetIndex(point), color));

        public Face With(Point3D point, PuzzleColor color)
            => this.With(this.Mapper.Map(point), color);

        internal Face RotateForward()
            => new Face(
                this.FaceName,
                ImmutableArray.Create(
                    this.colors[6],
                    this.colors[3],
                    this.colors[0],
                    this.colors[7],
                    this.colors[4],
                    this.colors[1],
                    this.colors[8],
                    this.colors[5],
                    this.colors[2]));

        internal Face RotateBackward()
            => new Face(
                this.FaceName,
                ImmutableArray.Create(
                    this.colors[2],
                    this.colors[5],
                    this.colors[8],
                    this.colors[1],
                    this.colors[4],
                    this.colors[7],
                    this.colors[0],
                    this.colors[3],
                    this.colors[6]));

        internal Face Rotate2()
            => new Face(
                this.FaceName,
                ImmutableArray.Create(
                    this.colors[8],
                    this.colors[7],
                    this.colors[6],
                    this.colors[5],
                    this.colors[4],
                    this.colors[3],
                    this.colors[2],
                    this.colors[1],
                    this.colors[0]));

        private static int GetIndex(Point2D point) => (point.X + 1) + (point.Y + 1) * Size;
    }
}