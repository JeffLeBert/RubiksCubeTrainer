using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3.Roux
{
    public class MoveCornerToTopStep : IStep
    {
        private readonly ImmutableArray<AlgorithmCollection> allAlgorithms;

        public MoveCornerToTopStep(string color1, string color2)
        {
            this.Color1 = color1;
            this.Color2 = color2;

            this.Color1X = PuzzleColorParser.Parse(color1);
            this.Color2X = PuzzleColorParser.Parse(color2);

            this.allAlgorithms = this.BuildAllAlgorithms();
        }

        public static MoveCornerToTopStep InstanceDownFrontLeft { get; } = new MoveCornerToTopStep(
            nameof(PuzzleColor.Red),
            nameof(PuzzleColor.Blue));

        public static MoveCornerToTopStep InstanceDownBackLeft { get; } = new MoveCornerToTopStep(
            nameof(PuzzleColor.Blue),
            nameof(PuzzleColor.Orange));

        public static MoveCornerToTopStep InstanceDownFrontRight { get; } = new MoveCornerToTopStep(
            nameof(PuzzleColor.Green),
            nameof(PuzzleColor.Red));

        public static MoveCornerToTopStep InstanceDownBackRight { get; } = new MoveCornerToTopStep(
            nameof(PuzzleColor.Orange),
            nameof(PuzzleColor.Green));

        public string Color1 { get; }

        public string Color2 { get; }

        public PuzzleColor Color1X { get; }

        public PuzzleColor Color2X { get; }

        public string Name => nameof(MoveCornerToTopStep);

        public IEnumerable<AlgorithmCollection> GetPossibleAlgorithms(Puzzle puzzle)
            => from algorithmInfo in this.allAlgorithms
               where algorithmInfo.InitialState.Matches(puzzle)
               select algorithmInfo;

        private ImmutableArray<AlgorithmCollection> BuildAllAlgorithms()
            => ImmutableArray.Create(
                this.MoveCornerFromLeftFrontDown(),
                this.MoveCornerFromRightFrontDown(),
                this.MoveCornerFromLeftBackDown(),
                this.MoveCornerFromRightBackDown());

        private AlgorithmCollection MoveCornerFromLeftFrontDown()
            => new AlgorithmCollection(
                "Move corner from left front down to up.",
                new PuzzleState($"DownFrontLeft* {this.Color1} {this.Color2} White"),
                NotationMoveType.FrontClockwise,
                NotationMoveType.UpClockwise,
                NotationMoveType.FrontCounterClockwise);

        private AlgorithmCollection MoveCornerFromRightFrontDown()
            => new AlgorithmCollection(
                "Move corner from right front down to up.",
                new PuzzleState($"DownFrontRight* {this.Color1} {this.Color2} White"),
                NotationMoveType.FrontCounterClockwise,
                NotationMoveType.UpCounterClockwise,
                NotationMoveType.FrontClockwise);

        private AlgorithmCollection MoveCornerFromLeftBackDown()
            => new AlgorithmCollection(
                "Move corner from left back down to up.",
                new PuzzleState($"DownBackLeft* {this.Color1} {this.Color2} White"),
                NotationMoveType.BackCounterClockwise,
                NotationMoveType.UpCounterClockwise,
                NotationMoveType.BackClockwise);

        private AlgorithmCollection MoveCornerFromRightBackDown()
            => new AlgorithmCollection(
                "Move corner from right back down to up.",
                new PuzzleState($"DownBackRight* {this.Color1} {this.Color2} White"),
                NotationMoveType.BackClockwise,
                NotationMoveType.UpClockwise,
                NotationMoveType.BackCounterClockwise);

        public bool ShouldUse(Puzzle puzzle)
            => !Checker.CornerUpFrontLeft(puzzle, this.Color1X, this.Color2X, PuzzleColor.White)
            && !Checker.CornerUpFrontRight(puzzle, this.Color1X, this.Color2X, PuzzleColor.White)
            && !Checker.CornerUpBackLeft(puzzle, this.Color1X, this.Color2X, PuzzleColor.White)
            && !Checker.CornerUpBackRight(puzzle, this.Color1X, this.Color2X, PuzzleColor.White);
    }
}