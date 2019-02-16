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
            this.Color1 = color1;
            this.Color2 = color2;
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

        public PuzzleColor Color1 { get; }

        public PuzzleColor Color2 { get; }

        public IEnumerable<Algorithm> GetPossibleAlgorithms(Puzzle puzzle)
            => from algorithmInfo in this.allAlgorithms
               where algorithmInfo.InitialPosition(puzzle)
               select algorithmInfo;

        public bool ShouldUse(Puzzle puzzle)
        {
            // TODO: Fix this...
            return false;
        }
    }
}