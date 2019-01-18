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

        public PuzzleColor this[SquareLocation index] => this[index.Face][index.Index];

        public Puzzle ClockwiseRotateLayer(LayerName layerName)
        {
            var rotateInfos = Structure.ClockwiseRotates[layerName];
            var newPuzzle = this;
            foreach (var rotateInfo in rotateInfos)
            {
                newPuzzle = newPuzzle.With(
                    rotateInfo.Value.Face,
                    newPuzzle[rotateInfo.Value.Face].With(rotateInfo.Value.Index, this[rotateInfo.Key]));
            }

            return newPuzzle;
        }

        public Puzzle With(FaceName faceIndex, Face face)
            => new Puzzle(this.faces.SetItem((int)faceIndex, face));

        private static Puzzle BuildSolvedPuzzle()
            => new Puzzle(
                ImmutableArray<Face>.Empty.AddRange(
                    new Face[]
                    {
                        new Face(PuzzleColor.White),
                        new Face(PuzzleColor.Blue),
                        new Face(PuzzleColor.Yellow),
                        new Face(PuzzleColor.Green),
                        new Face(PuzzleColor.Red),
                        new Face(PuzzleColor.Orange)
                    }));
    }
}