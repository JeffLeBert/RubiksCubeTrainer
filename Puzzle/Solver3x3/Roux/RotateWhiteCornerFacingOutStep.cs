using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3.Roux
{
    public class RotateWhiteCornerFacingOutStep : IStep
    {
        private readonly ImmutableArray<AlgorithmCollection> allAlgorithms;

        public RotateWhiteCornerFacingOutStep(PuzzleColor color1, PuzzleColor color2)
        {
            this.Color1 = color1;
            this.Color2 = color2;

            this.allAlgorithms = this.BuildAllAlgorithms();
        }

        public IEnumerable<AlgorithmCollection> GetPossibleAlgorithms(Puzzle puzzle)
            => from algorithmInfo in this.allAlgorithms
               where algorithmInfo.InitialState.Matches(puzzle)
               select algorithmInfo;

        private ImmutableArray<AlgorithmCollection> BuildAllAlgorithms()
            => ImmutableArray<AlgorithmCollection>.Empty;

        public static RotateWhiteCornerFacingOutStep InstanceDownFrontLeft { get; } = new RotateWhiteCornerFacingOutStep(
            PuzzleColor.Red,
            PuzzleColor.Blue);

        public static RotateWhiteCornerFacingOutStep InstanceDownBackLeft { get; } = new RotateWhiteCornerFacingOutStep(
            PuzzleColor.Blue,
            PuzzleColor.Orange);

        public static RotateWhiteCornerFacingOutStep InstanceDownFrontRight { get; } = new RotateWhiteCornerFacingOutStep(
            PuzzleColor.Green,
            PuzzleColor.Red);

        public static RotateWhiteCornerFacingOutStep InstanceDownBackRight { get; } = new RotateWhiteCornerFacingOutStep(
            PuzzleColor.Orange,
            PuzzleColor.Green);

        public PuzzleColor Color1 { get; }

        public PuzzleColor Color2 { get; }

        public string Name => nameof(RotateWhiteCornerFacingOutStep);

        public bool ShouldUse(Puzzle puzzle)
            => this.ShouldUseForUpFrontLeft(puzzle)
            || this.ShouldUseForUpFrontRight(puzzle)
            || this.ShouldUseForUpBackLeft(puzzle)
            || this.ShouldUseForUpBackRight(puzzle);

        private bool ShouldUseForUpFrontLeft(Puzzle puzzle)
            => Checker.CornerUpFrontLeft(puzzle, this.Color1, this.Color2, PuzzleColor.White)
            && Checker.SingleColor(puzzle, Location.UpFrontLeft, PuzzleColor.White);

        private bool ShouldUseForUpFrontRight(Puzzle puzzle)
            => Checker.CornerUpFrontRight(puzzle, this.Color1, this.Color2, PuzzleColor.White)
            && Checker.SingleColor(puzzle, Location.UpFrontRight, PuzzleColor.White);

        private bool ShouldUseForUpBackLeft(Puzzle puzzle)
            => Checker.CornerUpBackLeft(puzzle, this.Color1, this.Color2, PuzzleColor.White)
            && Checker.SingleColor(puzzle, Location.UpBackLeft, PuzzleColor.White);

        private bool ShouldUseForUpBackRight(Puzzle puzzle)
            => Checker.CornerUpBackRight(puzzle, this.Color1, this.Color2, PuzzleColor.White)
            && Checker.SingleColor(puzzle, Location.UpBackRight, PuzzleColor.White);
    }
}