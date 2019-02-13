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
            this.EndGoal = BuildEndGoal();
        }

        public static MoveBlueCenterToLeftFaceStep Instance { get; } = new MoveBlueCenterToLeftFaceStep();

        public Func<Puzzle, bool> EndGoal { get; }

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
                Checker.SingleColor(Location.Back, PuzzleColor.Blue),
                NotationMoveType.AllFrontRight);

        private static Algorithm MoveFaceFromFront()
            => new Algorithm(
                "Move blue face from front to left.",
                Checker.SingleColor(Location.Front, PuzzleColor.Blue),
                NotationMoveType.AllFrontLeft);

        private static Algorithm MoveFaceFromRight()
            => new Algorithm(
                "Move blue face from right to left.",
                Checker.SingleColor(Location.Right, PuzzleColor.Blue),
                NotationMoveType.AllFrontLeftDouble);

        private static Algorithm MoveFaceFromUp()
            => new Algorithm(
                "Move blue face from top to left.",
                Checker.SingleColor(Location.Up, PuzzleColor.Blue),
                NotationMoveType.AllFrontCounterClockwise);

        private static Algorithm MoveFaceFromDown()
            => new Algorithm(
                "Move blue face from down to left.",
                Checker.SingleColor(Location.Down, PuzzleColor.Blue),
                NotationMoveType.AllFrontClockwise);

        private static Func<Puzzle, bool> BuildEndGoal()
            => Checker.SingleColor(Location.Left, PuzzleColor.Blue);
    }
}