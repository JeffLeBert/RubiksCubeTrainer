using System;
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
            this.EndGoal = BuildEndGoal();
        }

        public Func<Puzzle, bool> EndGoal { get; }

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
                Checker.Edge(Location.DownLeftEdge, PuzzleColor.Blue, PuzzleColor.White),
                NotationMoveType.DownClockwise,
                NotationMoveType.FrontClockwise,
                NotationMoveType.LeftClockwise);

        private static Algorithm BackFaceLeftSideNoFlip()
            => new Algorithm(
                "Move edge from back left.",
                Checker.Edge(Location.BackLeftEdge, PuzzleColor.White, PuzzleColor.Blue),
                NotationMoveType.LeftCounterClockwise);

        private static Algorithm BackFaceLeftSideFlip()
            => new Algorithm(
                "Move edit from back left and flip.",
                Checker.Edge(Location.BackLeftEdge, PuzzleColor.Blue, PuzzleColor.White),
                NotationMoveType.BackClockwise,
                NotationMoveType.DownClockwise);

        private static Algorithm TopFaceLeftSideNoFlip()
            => new Algorithm(
                "Move edge from top left.",
                Checker.Edge(Location.LeftUpEdge, PuzzleColor.Blue, PuzzleColor.White),
                NotationMoveType.LeftDouble);

        private static Algorithm TopFaceLeftSideFlip()
            => new Algorithm(
                "Move edge from top left and flip.",
                Checker.Edge(Location.LeftUpEdge, PuzzleColor.White, PuzzleColor.Blue),
                NotationMoveType.LeftCounterClockwise,
                NotationMoveType.BackClockwise,
                NotationMoveType.DownClockwise);

        private static Algorithm FrontFaceLeftSideNoFlip()
            => new Algorithm(
                "Move edge from front left.",
                Checker.Edge(Location.FrontLeftEdge, PuzzleColor.White, PuzzleColor.Blue),
                NotationMoveType.LeftClockwise);

        private static Algorithm FrontFaceLeftSideFlip()
            => new Algorithm(
                "Move edge from front left and flip.",
                Checker.Edge(Location.FrontLeftEdge, PuzzleColor.Blue, PuzzleColor.White),
                NotationMoveType.FrontCounterClockwise,
                NotationMoveType.DownCounterClockwise);

        private static Algorithm BackFaceDownSideNoFlip()
            => new Algorithm(
                "Move edge from back bottom.",
                Checker.Edge(Location.BackDownEdge, PuzzleColor.Blue, PuzzleColor.White),
                NotationMoveType.DownClockwise);

        private static Algorithm BackFaceDownSideFlip()
            => new Algorithm(
                "Move edge from back bottom and flip.",
                Checker.Edge(Location.BackDownEdge, PuzzleColor.White, PuzzleColor.Blue),
                NotationMoveType.BackCounterClockwise,
                NotationMoveType.LeftCounterClockwise);

        private static Algorithm BackFaceUpSideNoFlip()
            => new Algorithm(
                "Move edge from back top.",
                Checker.Edge(Location.BackUpEdge, PuzzleColor.Blue, PuzzleColor.White),
                NotationMoveType.UpCounterClockwise,
                NotationMoveType.LeftDouble);

        private static Algorithm BackFaceUpSideFlip()
            => new Algorithm(
                "Move edge from back top and flip.",
                Checker.Edge(Location.BackUpEdge, PuzzleColor.White, PuzzleColor.Blue),
                NotationMoveType.BackClockwise,
                NotationMoveType.LeftCounterClockwise);

        private static Algorithm FrontFaceDownSideNoFlip()
            => new Algorithm(
                "Move edge from front bottom.",
                Checker.Edge(Location.FrontDownEdge, PuzzleColor.Blue, PuzzleColor.White),
                NotationMoveType.DownCounterClockwise);

        private static Algorithm FrontFaceDownSideFlip()
            => new Algorithm(
                "Move edge from front bottom and flip.",
                Checker.Edge(Location.FrontDownEdge, PuzzleColor.White, PuzzleColor.Blue),
                NotationMoveType.FrontClockwise,
                NotationMoveType.LeftClockwise);

        private static Algorithm FrontFaceUpSideNoFlip()
            => new Algorithm(
                "Move edge from front up.",
                Checker.Edge(Location.FrontUpEdge, PuzzleColor.Blue, PuzzleColor.White),
                NotationMoveType.FrontDouble,
                NotationMoveType.DownCounterClockwise);

        private static Algorithm FrontFaceUpSideFlip()
            => new Algorithm(
                "Move edge from front up and flip.",
                Checker.Edge(Location.FrontUpEdge, PuzzleColor.White, PuzzleColor.Blue),
                NotationMoveType.FrontCounterClockwise, NotationMoveType.LeftClockwise);

        private static Algorithm RightFaceDownSideNoFlip()
            => new Algorithm(
                "Move edge from right down.",
                Checker.Edge(Location.RightDownEdge, PuzzleColor.Blue, PuzzleColor.White),
                NotationMoveType.DownDouble);

        private static Algorithm RightFaceDownSideFlip()
            => new Algorithm(
                "Move edge from right down and flip.",
                Checker.Edge(Location.RightDownEdge, PuzzleColor.White, PuzzleColor.Blue),
                NotationMoveType.RightClockwise,
                NotationMoveType.FrontClockwise,
                NotationMoveType.DownCounterClockwise);

        private static Algorithm RightFaceBackSideNoFlip()
            => new Algorithm(
                "Move edge from right back.",
                Checker.Edge(Location.RightBackEdge, PuzzleColor.White, PuzzleColor.Blue),
                NotationMoveType.BackCounterClockwise,
                NotationMoveType.DownClockwise);

        private static Algorithm RightFaceBackSideFlip()
            => new Algorithm(
                "Move edge from right back and flip.",
                Checker.Edge(Location.RightBackEdge, PuzzleColor.Blue, PuzzleColor.White),
                NotationMoveType.RightClockwise,
                NotationMoveType.DownDouble);

        private static Algorithm RightFaceFrontSideNoFlip()
            => new Algorithm(
                "Move edge from right front.",
                Checker.Edge(Location.RightFrontEdge, PuzzleColor.Blue, PuzzleColor.White),
                NotationMoveType.RightCounterClockwise,
                NotationMoveType.DownDouble);

        private static Algorithm RightFaceFrontSideFlip()
            => new Algorithm(
                "Move edge from right front and flip.",
                Checker.Edge(Location.RightFrontEdge, PuzzleColor.White, PuzzleColor.Blue),
                NotationMoveType.FrontClockwise,
                NotationMoveType.DownCounterClockwise);

        private static Algorithm RightFaceFrontUpNoFlip()
            => new Algorithm(
                "Move edge from right up.",
                Checker.Edge(Location.RightUpEdge, PuzzleColor.Blue, PuzzleColor.White),
                NotationMoveType.UpDouble,
                NotationMoveType.LeftDouble);

        private static Algorithm RightFaceFrontUpFlip()
            => new Algorithm(
                "Move edge from right up.",
                Checker.Edge(Location.RightUpEdge, PuzzleColor.White, PuzzleColor.Blue),
                NotationMoveType.UpClockwise,
                NotationMoveType.FrontCounterClockwise,
                NotationMoveType.LeftClockwise);

        private static Func<Puzzle, bool> BuildEndGoal()
            => puzzle => Checker.Edge(Location.LeftDownEdge, PuzzleColor.Blue, PuzzleColor.White)(puzzle)
                && MoveBlueCenterToLeftFaceStep.Instance.EndGoal(puzzle);
    }
}