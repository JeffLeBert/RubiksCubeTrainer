using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3.Roux
{
    internal class MoveCornerToTopStep : IStep
    {
        private readonly ImmutableArray<Algorithm> allAlgorithms;

        public MoveCornerToTopStep(PuzzleColor color1, PuzzleColor color2)
        {
            this.Color1 = color1;
            this.Color2 = color2;
            this.allAlgorithms = this.BuildAllAlgorithms();
        }

        public static MoveCornerToTopStep InstanceLeftFrontDown { get; } = new MoveCornerToTopStep(
            PuzzleColor.Red,
            PuzzleColor.Blue);

        public static MoveCornerToTopStep InstanceLeftBackDown { get; } = new MoveCornerToTopStep(
            PuzzleColor.Blue,
            PuzzleColor.Orange);

        public static MoveCornerToTopStep InstanceRightFrontDown { get; } = new MoveCornerToTopStep(
            PuzzleColor.Green,
            PuzzleColor.Red);

        public static MoveCornerToTopStep InstanceRightBackDown { get; } = new MoveCornerToTopStep(
            PuzzleColor.Orange,
            PuzzleColor.Green);

        public PuzzleColor Color1 { get; }

        public PuzzleColor Color2 { get; }

        public IEnumerable<Algorithm> GetPossibleAlgorithms(Puzzle puzzle)
            => from algorithmInfo in this.allAlgorithms
               where algorithmInfo.InitialPosition(puzzle)
               select algorithmInfo;

        private ImmutableArray<Algorithm> BuildAllAlgorithms()
            => ImmutableArray.Create(
                this.MoveCornerFromLeftFrontDown(),
                this.MoveCornerFromRightFrontDown(),
                this.MoveCornerFromLeftBackDown(),
                this.MoveCornerFromRightBackDown());

        private Algorithm MoveCornerFromLeftFrontDown()
            => new Algorithm(
                "Move corner from left front down to up.",
                puzzle => Checker.CornerOrRotated(puzzle, Location.LeftFrontDown, this.Color1, this.Color2, PuzzleColor.White),
                NotationMoveType.FrontClockwise,
                NotationMoveType.UpClockwise,
                NotationMoveType.FrontCounterClockwise);

        private Algorithm MoveCornerFromRightFrontDown()
            => new Algorithm(
                "Move corner from right front down to up.",
                puzzle => Checker.CornerOrRotated(puzzle, Location.RightFrontDown, this.Color1, this.Color2, PuzzleColor.White),
                NotationMoveType.FrontCounterClockwise,
                NotationMoveType.UpCounterClockwise,
                NotationMoveType.FrontClockwise);

        private Algorithm MoveCornerFromLeftBackDown()
            => new Algorithm(
                "Move corner from left back down to up.",
                puzzle => Checker.CornerOrRotated(puzzle, Location.LeftBackDown, this.Color1, this.Color2, PuzzleColor.White),
                NotationMoveType.BackCounterClockwise,
                NotationMoveType.UpCounterClockwise,
                NotationMoveType.BackClockwise);

        private Algorithm MoveCornerFromRightBackDown()
            => new Algorithm(
                "Move corner from right back down to up.",
                puzzle => Checker.CornerOrRotated(puzzle, Location.RightBackDown, this.Color1, this.Color2, PuzzleColor.White),
                NotationMoveType.BackClockwise,
                NotationMoveType.UpClockwise,
                NotationMoveType.BackCounterClockwise);

        public bool ShouldUse(Puzzle puzzle)
            => !Checker.CornerOrRotated(puzzle, Location.UpFrontLeft, this.Color1, this.Color2, PuzzleColor.White)
            && !Checker.CornerOrRotated(puzzle, Location.UpFrontRight, this.Color1, this.Color2, PuzzleColor.White)
            && !Checker.CornerOrRotated(puzzle, Location.UpBackLeft, this.Color1, this.Color2, PuzzleColor.White)
            && !Checker.CornerOrRotated(puzzle, Location.UpBackRight, this.Color1, this.Color2, PuzzleColor.White);
    }
}