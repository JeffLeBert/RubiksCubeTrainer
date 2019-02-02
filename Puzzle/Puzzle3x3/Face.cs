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

        private readonly PuzzleColor[] colors;

        public const int Size = 3;

        public Face(FaceName faceName, PuzzleColor color)
            : this(faceName, new[] { color, color, color, color, color, color, color, color, color })
        {
        }

        public Face(FaceName faceName, params PuzzleColor[] colors)
        {
            this.FaceName = faceName;
            this.colors = colors;
        }

        public PuzzleColor this[int x, int y]
            => this.colors[GetIndex(new Point2D(x, y))];

        private PuzzleColor this[Point2D point2D]
        {
            get { return this.colors[GetIndex(point2D)]; }

            set { this.colors[GetIndex(point2D)] = value; }
        }

        public PuzzleColor this[Point3D point3D]
        {
            get { return this[this.Mapper.Map(point3D)]; }

            internal set { this[this.Mapper.Map(point3D)] = value; }
        }

        public FaceName FaceName { get; }

        public CoordinateMapper Mapper => CoordinateMapper.GetMapperForFace(this.FaceName);

        public Face Clone()
        {
            var newColors = new PuzzleColor[Size * Size];
            this.colors.CopyTo(newColors, 0);

            return new Face(this.FaceName, newColors);
        }

        internal Face RotateForward()
            => new Face(
                this.FaceName,
                this.colors[6],
                this.colors[3],
                this.colors[0],
                this.colors[7],
                this.colors[4],
                this.colors[1],
                this.colors[8],
                this.colors[5],
                this.colors[2]);

        internal Face RotateBackward()
            => new Face(
                this.FaceName,
                this.colors[2],
                this.colors[5],
                this.colors[8],
                this.colors[1],
                this.colors[4],
                this.colors[7],
                this.colors[0],
                this.colors[3],
                this.colors[6]);

        internal Face Rotate2()
            => new Face(
                this.FaceName,
                this.colors[8],
                this.colors[7],
                this.colors[6],
                this.colors[5],
                this.colors[4],
                this.colors[3],
                this.colors[2],
                this.colors[1],
                this.colors[0]);

        private static int GetIndex(Point2D point) => (point.X + 1) + (point.Y + 1) * Size;
    }
}