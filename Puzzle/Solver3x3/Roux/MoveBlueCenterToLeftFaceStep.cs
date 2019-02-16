using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3.Roux
{
    public class MoveBlueCenterToLeftFaceStep : IStep
    {
        private static readonly ImmutableArray<Algorithm> allAlgorithms = BuildAllAlgorithms();

        private MoveBlueCenterToLeftFaceStep()
        {
        }

        public static MoveBlueCenterToLeftFaceStep Instance { get; } = new MoveBlueCenterToLeftFaceStep();

        public IEnumerable<Algorithm> GetPossibleAlgorithms(Puzzle puzzle)
            => from stepInfo in allAlgorithms
               where stepInfo.InitialPosition(puzzle)
               select stepInfo;

        private static ImmutableArray<Algorithm> BuildAllAlgorithms()
            => ImmutableArray.Create(
                MoveFaceFromBack(),
                MoveFaceFromFront(),
                MoveFaceFromRight(),
                MoveFaceFromUp(),
                MoveFaceFromDown());

        private static Algorithm MoveFaceFromBack()
            => new Algorithm(
                "Move blue face from back to left.",
                puzzle => Checker.SingleColor(puzzle, Location.Back, PuzzleColor.Blue),
                NotationMoveType.AllFrontRight);

        private static Algorithm MoveFaceFromFront()
            => new Algorithm(
                "Move blue face from front to left.",
                puzzle => Checker.SingleColor(puzzle, Location.Front, PuzzleColor.Blue),
                NotationMoveType.AllFrontLeft);

        private static Algorithm MoveFaceFromRight()
            => new Algorithm(
                "Move blue face from right to left.",
                puzzle => Checker.SingleColor(puzzle, Location.Right, PuzzleColor.Blue),
                NotationMoveType.AllFrontLeftDouble);

        private static Algorithm MoveFaceFromUp()
            => new Algorithm(
                "Move blue face from top to left.",
                puzzle => Checker.SingleColor(puzzle, Location.Up, PuzzleColor.Blue),
                NotationMoveType.AllFrontCounterClockwise);

        private static Algorithm MoveFaceFromDown()
            => new Algorithm(
                "Move blue face from down to left.",
                puzzle => Checker.SingleColor(puzzle, Location.Down, PuzzleColor.Blue),
                NotationMoveType.AllFrontClockwise);

        public bool ShouldUse(Puzzle puzzle)
            => !Checker.SingleColor(puzzle, Location.Left, PuzzleColor.Blue);
    }
}