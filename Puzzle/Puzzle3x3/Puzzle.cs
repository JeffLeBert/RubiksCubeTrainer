namespace RubiksCubeTrainer.Puzzle3x3
{
    public class Puzzle
    {
        private readonly Face[] faces;

        public Puzzle(params Face[] faces)
        {
            this.faces = faces;
        }

        public static Puzzle Solved { get; } = BuildSolvedPuzzle();

        public Face this[FaceName faceName]
            => this.faces[(int)faceName];

        public PuzzleColor this[Location location]
        {
            get { return this[location.FaceName][location.Point3D]; }

            internal set { this[location.FaceName][location.Point3D] = value; }
        }

        public Puzzle Clone()
            => new Puzzle(
                new Face[]
                {
                    this.faces[0].Clone(),
                    this.faces[1].Clone(),
                    this.faces[2].Clone(),
                    this.faces[3].Clone(),
                    this.faces[4].Clone(),
                    this.faces[5].Clone()
                });

        public void With(Face face)
            => this.faces[(int)face.FaceName] = face;

        private static Puzzle BuildSolvedPuzzle()
            => new Puzzle(
                new Face(FaceName.Up, PuzzleColor.White),
                new Face(FaceName.Front, PuzzleColor.Orange),
                new Face(FaceName.Down, PuzzleColor.Yellow),
                new Face(FaceName.Back, PuzzleColor.Red),
                new Face(FaceName.Left, PuzzleColor.Blue),
                new Face(FaceName.Right, PuzzleColor.Green));
    }
}