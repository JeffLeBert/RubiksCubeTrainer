namespace RubiksCubeTrainer.Puzzle3x3
{
    [System.Diagnostics.DebuggerDisplay("{FaceName}: {colors[0, 0]} {colors[1, 0]} {colors[2, 0]} {colors[1, 0]} {colors[1, 1]} {colors[1, 2]} {colors[2, 0]} {colors[2, 1]} {colors[2, 2]}")]
    public class Face
    {
        private readonly PuzzleColor[,] colors;

        public const int Size = 3;

        public Face(FaceName faceName, PuzzleColor color)
            : this(
                  faceName,
                  new PuzzleColor[Size, Size] 
                  {
                      { color, color, color }, 
                      { color, color, color }, 
                      { color, color, color }
                  })
        {
        }

        public Face(FaceName faceName, PuzzleColor[,] colors)
        {
            this.FaceName = faceName;
            this.colors = colors;
        }

        public PuzzleColor this[int x, int y]
            => this.colors[x + 1, y + 1];

        public PuzzleColor this[Point2D point2D]
        {
            get { return this.colors[point2D.X + 1, point2D.Y + 1]; }

            set { this.colors[point2D.X + 1, point2D.Y + 1] = value; }
        }

        public FaceName FaceName { get; }

        public Face Clone()
        {
            var colors = this.colors;
            var newColors = new PuzzleColor[Size, Size]
            {
                { colors[0, 0], colors[0, 1], colors[0, 2] },
                { colors[1, 0], colors[1, 1], colors[1, 2] },
                { colors[2, 0], colors[2, 1], colors[2, 2] }
            };

            return new Face(this.FaceName, newColors);
        }
    }
}