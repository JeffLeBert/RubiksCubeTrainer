using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3.Roux
{
    public class FirstPieceToBottomLeftEdgeStep : IStep
    {
        private static readonly ImmutableArray<Algorithm> allAlgorithms = BuildAllAlgorithms();

        private FirstPieceToBottomLeftEdgeStep()
        {
        }

        public static FirstPieceToBottomLeftEdgeStep Instance { get; } = new FirstPieceToBottomLeftEdgeStep();

        public IEnumerable<Algorithm> GetPossibleAlgorithms(Puzzle puzzle)
            => from stepInfo in allAlgorithms
               where stepInfo.InitialPosition(puzzle)
               select stepInfo;

        private static ImmutableArray<Algorithm> BuildAllAlgorithms()
            => ImmutableArray.Create(
                // Left side.
                DownFaceLeftSideFlip(),
                BackFaceLeftSideNoFlip(),
                BackFaceLeftSideFlip(),
                TopFaceLeftSideNoFlip(),
                TopFaceLeftSideFlip(),
                FrontFaceLeftSideNoFlip(),
                FrontFaceLeftSideFlip(),

                // Middle slice.
                BackFaceDownSideNoFlip(),
                BackFaceDownSideFlip(),
                BackFaceUpSideNoFlip(),
                BackFaceUpSideFlip(),
                FrontFaceDownSideNoFlip(),
                FrontFaceDownSideFlip(),
                FrontFaceUpSideNoFlip(),
                FrontFaceUpSideFlip(),

                // Right side.
                RightFaceDownSideNoFlip(),
                RightFaceDownSideFlip(),
                RightFaceBackSideNoFlip(),
                RightFaceBackSideFlip(),
                RightFaceFrontSideNoFlip(),
                RightFaceFrontSideFlip(),
                RightFaceFrontUpNoFlip(),
                RightFaceFrontUpFlip());

        private static Algorithm DownFaceLeftSideFlip()
            => new Algorithm(
                "Flip down left edge.",
                puzzle => Checker.Edge(puzzle, Location.DownLeft, PuzzleColor.Blue, PuzzleColor.White),
                NotationMoveType.DownClockwise,
                NotationMoveType.FrontClockwise,
                NotationMoveType.LeftClockwise);

        private static Algorithm BackFaceLeftSideNoFlip()
            => new Algorithm(
                "Move edge from back left.",
                puzzle => Checker.Edge(puzzle, Location.BackLeft, PuzzleColor.White, PuzzleColor.Blue),
                NotationMoveType.LeftCounterClockwise);

        private static Algorithm BackFaceLeftSideFlip()
            => new Algorithm(
                "Move edit from back left and flip.",
                puzzle => Checker.Edge(puzzle, Location.BackLeft, PuzzleColor.Blue, PuzzleColor.White),
                NotationMoveType.BackClockwise,
                NotationMoveType.DownClockwise);

        private static Algorithm TopFaceLeftSideNoFlip()
            => new Algorithm(
                "Move edge from top left.",
                puzzle => Checker.Edge(puzzle, Location.LeftUp, PuzzleColor.Blue, PuzzleColor.White),
                NotationMoveType.LeftDouble);

        private static Algorithm TopFaceLeftSideFlip()
            => new Algorithm(
                "Move edge from top left and flip.",
                puzzle => Checker.Edge(puzzle, Location.LeftUp, PuzzleColor.White, PuzzleColor.Blue),
                NotationMoveType.LeftCounterClockwise,
                NotationMoveType.BackClockwise,
                NotationMoveType.DownClockwise);

        private static Algorithm FrontFaceLeftSideNoFlip()
            => new Algorithm(
                "Move edge from front left.",
                puzzle => Checker.Edge(puzzle, Location.FrontLeft, PuzzleColor.White, PuzzleColor.Blue),
                NotationMoveType.LeftClockwise);

        private static Algorithm FrontFaceLeftSideFlip()
            => new Algorithm(
                "Move edge from front left and flip.",
                puzzle => Checker.Edge(puzzle, Location.FrontLeft, PuzzleColor.Blue, PuzzleColor.White),
                NotationMoveType.FrontCounterClockwise,
                NotationMoveType.DownCounterClockwise);

        private static Algorithm BackFaceDownSideNoFlip()
            => new Algorithm(
                "Move edge from back bottom.",
                puzzle => Checker.Edge(puzzle, Location.BackDown, PuzzleColor.Blue, PuzzleColor.White),
                NotationMoveType.DownClockwise);

        private static Algorithm BackFaceDownSideFlip()
            => new Algorithm(
                "Move edge from back bottom and flip.",
                puzzle => Checker.Edge(puzzle, Location.BackDown, PuzzleColor.White, PuzzleColor.Blue),
                NotationMoveType.BackCounterClockwise,
                NotationMoveType.LeftCounterClockwise);

        private static Algorithm BackFaceUpSideNoFlip()
            => new Algorithm(
                "Move edge from back top.",
                puzzle => Checker.Edge(puzzle, Location.BackUp, PuzzleColor.Blue, PuzzleColor.White),
                NotationMoveType.UpCounterClockwise,
                NotationMoveType.LeftDouble);

        private static Algorithm BackFaceUpSideFlip()
            => new Algorithm(
                "Move edge from back top and flip.",
                puzzle => Checker.Edge(puzzle, Location.BackUp, PuzzleColor.White, PuzzleColor.Blue),
                NotationMoveType.BackClockwise,
                NotationMoveType.LeftCounterClockwise);

        private static Algorithm FrontFaceDownSideNoFlip()
            => new Algorithm(
                "Move edge from front bottom.",
                puzzle => Checker.Edge(puzzle, Location.FrontDown, PuzzleColor.Blue, PuzzleColor.White),
                NotationMoveType.DownCounterClockwise);

        private static Algorithm FrontFaceDownSideFlip()
            => new Algorithm(
                "Move edge from front bottom and flip.",
                puzzle => Checker.Edge(puzzle, Location.FrontDown, PuzzleColor.White, PuzzleColor.Blue),
                NotationMoveType.FrontClockwise,
                NotationMoveType.LeftClockwise);

        private static Algorithm FrontFaceUpSideNoFlip()
            => new Algorithm(
                "Move edge from front up.",
                puzzle => Checker.Edge(puzzle, Location.FrontUp, PuzzleColor.Blue, PuzzleColor.White),
                NotationMoveType.FrontDouble,
                NotationMoveType.DownCounterClockwise);

        private static Algorithm FrontFaceUpSideFlip()
            => new Algorithm(
                "Move edge from front up and flip.",
                puzzle => Checker.Edge(puzzle, Location.FrontUp, PuzzleColor.White, PuzzleColor.Blue),
                NotationMoveType.FrontCounterClockwise, NotationMoveType.LeftClockwise);

        private static Algorithm RightFaceDownSideNoFlip()
            => new Algorithm(
                "Move edge from right down.",
                puzzle => Checker.Edge(puzzle, Location.RightDown, PuzzleColor.Blue, PuzzleColor.White),
                NotationMoveType.DownDouble);

        private static Algorithm RightFaceDownSideFlip()
            => new Algorithm(
                "Move edge from right down and flip.",
                puzzle => Checker.Edge(puzzle, Location.RightDown, PuzzleColor.White, PuzzleColor.Blue),
                NotationMoveType.RightClockwise,
                NotationMoveType.FrontClockwise,
                NotationMoveType.DownCounterClockwise);

        private static Algorithm RightFaceBackSideNoFlip()
            => new Algorithm(
                "Move edge from right back.",
                puzzle => Checker.Edge(puzzle, Location.RightBack, PuzzleColor.White, PuzzleColor.Blue),
                NotationMoveType.BackCounterClockwise,
                NotationMoveType.DownClockwise);

        private static Algorithm RightFaceBackSideFlip()
            => new Algorithm(
                "Move edge from right back and flip.",
                puzzle => Checker.Edge(puzzle, Location.RightBack, PuzzleColor.Blue, PuzzleColor.White),
                NotationMoveType.RightClockwise,
                NotationMoveType.DownDouble);

        private static Algorithm RightFaceFrontSideNoFlip()
            => new Algorithm(
                "Move edge from right front.",
                puzzle => Checker.Edge(puzzle, Location.RightFront, PuzzleColor.Blue, PuzzleColor.White),
                NotationMoveType.RightCounterClockwise,
                NotationMoveType.DownDouble);

        private static Algorithm RightFaceFrontSideFlip()
            => new Algorithm(
                "Move edge from right front and flip.",
                puzzle => Checker.Edge(puzzle, Location.RightFront, PuzzleColor.White, PuzzleColor.Blue),
                NotationMoveType.FrontClockwise,
                NotationMoveType.DownCounterClockwise);

        private static Algorithm RightFaceFrontUpNoFlip()
            => new Algorithm(
                "Move edge from right up.",
                puzzle => Checker.Edge(puzzle, Location.RightUp, PuzzleColor.Blue, PuzzleColor.White),
                NotationMoveType.UpDouble,
                NotationMoveType.LeftDouble);

        private static Algorithm RightFaceFrontUpFlip()
            => new Algorithm(
                "Move edge from right up.",
                puzzle => Checker.Edge(puzzle, Location.RightUp, PuzzleColor.White, PuzzleColor.Blue),
                NotationMoveType.UpClockwise,
                NotationMoveType.FrontCounterClockwise,
                NotationMoveType.LeftClockwise);

        public bool ShouldUse(Puzzle puzzle)
            => !Checker.Edge(puzzle, Location.LeftDown, PuzzleColor.Blue, PuzzleColor.White);
    }
}