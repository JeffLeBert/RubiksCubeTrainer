using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3.Roux
{
    internal class MoveCornerToTopFrontOrBackStep : IStep
    {
        private readonly ImmutableArray<Algorithm> allAlgorithms;

        public MoveCornerToTopFrontOrBackStep(PuzzleColor color1, PuzzleColor color2)
        {
            this.EndGoal = puzzle => IsEndGoal(puzzle, color1, color2);
        }

        public static MoveCornerToTopFrontOrBackStep InstanceFrontLeft { get; } = new MoveCornerToTopFrontOrBackStep(
            PuzzleColor.Blue,
            PuzzleColor.Red);

        public static MoveCornerToTopFrontOrBackStep InstanceBackLeft { get; } = new MoveCornerToTopFrontOrBackStep(
            PuzzleColor.Blue,
            PuzzleColor.Orange);

        public static MoveCornerToTopFrontOrBackStep InstanceFrontRight { get; } = new MoveCornerToTopFrontOrBackStep(
            PuzzleColor.Green,
            PuzzleColor.Red);

        public static MoveCornerToTopFrontOrBackStep InstanceBackRight { get; } = new MoveCornerToTopFrontOrBackStep(
            PuzzleColor.Green,
            PuzzleColor.Orange);

        public Func<Puzzle, bool> EndGoal { get; }

        public IEnumerable<Algorithm> GetPossibleAlgorithms(Puzzle puzzle)
            => from algorithmInfo in this.allAlgorithms
               where algorithmInfo.InitialPosition(puzzle)
               select algorithmInfo;

        private static bool IsEndGoal(Puzzle puzzle, PuzzleColor color1, PuzzleColor color2)
        {
            // We don't care which way the edge is flipped so always take it that color1 is facing us.
            if (puzzle[Location.FrontDown] == color2)
            {
                return IsEndGoal(puzzle, color2, color1);
            }

            return false;
        }
    }
}