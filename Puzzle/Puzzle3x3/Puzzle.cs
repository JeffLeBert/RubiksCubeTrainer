using System.Collections.Immutable;

namespace RubiksCubeTrainer.Puzzle3x3
{
    public class Puzzle
    {
        private readonly ImmutableArray<Face> faces;

        public Puzzle(ImmutableArray<Face> faces)
        {
            this.faces = faces;
        }

        public static Puzzle Solved { get; } = BuildSolvedPuzzle();

        public Face this[FaceName faceName] => this.faces[(int)faceName];

        public Puzzle With(Face face)
            => new Puzzle(this.faces.SetItem((int)face.FaceName, face));

        private static Puzzle BuildSolvedPuzzle()
            => new Puzzle(
                ImmutableArray<Face>.Empty.AddRange(
                    new Face[]
                    {
                        new Face(FaceName.Up, PuzzleColor.White),
                        new Face(FaceName.Front, PuzzleColor.Orange),
                        new Face(FaceName.Down, PuzzleColor.Yellow),
                        new Face(FaceName.Back, PuzzleColor.Red),
                        new Face(FaceName.Left, PuzzleColor.Blue),
                        new Face(FaceName.Right, PuzzleColor.Green)
                    }));
    }
}