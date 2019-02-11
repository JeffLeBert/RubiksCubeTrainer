using System;
using System.Collections.Generic;
using System.Linq;
using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3.Roux
{
    public class FirstPieceToBottomLeftEdgeStep : StepBase
    {
        private FirstPieceToBottomLeftEdgeStep()
            : base(BuildEndGoal())
        {
        }

        public static FirstPieceToBottomLeftEdgeStep Instance { get; } = new FirstPieceToBottomLeftEdgeStep();

        public override IEnumerable<AlgorithmInformation> GetPossibleAlgorithms(Puzzle puzzle)
            => from stepInfo in this.AllAlgorithms
               where stepInfo.InitialPosition(puzzle)
               select stepInfo;

        protected override AlgorithmInformation[] BuildAllAlgorithms()
            => new AlgorithmInformation[]
            {
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
                RightFaceFrontUpFlip()
            };

        private static AlgorithmInformation DownFaceLeftSideFlip()
            => new AlgorithmInformation(
                new Algorithm(
                    "Flip down left edge.",
                    NotationMoveType.DownClockwise,
                    NotationMoveType.FrontClockwise,
                    NotationMoveType.LeftClockwise),
                Checker.Edge(Location.DownLeftEdge, PuzzleColor.Blue, PuzzleColor.White));

        private static AlgorithmInformation BackFaceLeftSideNoFlip()
            => new AlgorithmInformation(
                new Algorithm("Move edge from back left.", NotationMoveType.LeftCounterClockwise),
                Checker.Edge(Location.BackLeftEdge, PuzzleColor.White, PuzzleColor.Blue));

        private static AlgorithmInformation BackFaceLeftSideFlip()
            => new AlgorithmInformation(
                new Algorithm(
                    "Move edit from back left and flip.",
                    NotationMoveType.BackClockwise,
                    NotationMoveType.DownClockwise),
                Checker.Edge(Location.BackLeftEdge, PuzzleColor.Blue, PuzzleColor.White));

        private static AlgorithmInformation TopFaceLeftSideNoFlip()
            => new AlgorithmInformation(
                new Algorithm("Move edge from top left.", NotationMoveType.LeftDouble),
                Checker.Edge(Location.LeftUpEdge, PuzzleColor.Blue, PuzzleColor.White));

        private static AlgorithmInformation TopFaceLeftSideFlip()
            => new AlgorithmInformation(
                new Algorithm(
                    "Move edge from top left and flip.",
                    NotationMoveType.LeftCounterClockwise,
                    NotationMoveType.BackClockwise,
                    NotationMoveType.DownClockwise),
                Checker.Edge(Location.LeftUpEdge, PuzzleColor.White, PuzzleColor.Blue));

        private static AlgorithmInformation FrontFaceLeftSideNoFlip()
            => new AlgorithmInformation(
                new Algorithm("Move edge from front left.", NotationMoveType.LeftClockwise),
                Checker.Edge(Location.FrontLeftEdge, PuzzleColor.White, PuzzleColor.Blue));

        private static AlgorithmInformation FrontFaceLeftSideFlip()
            => new AlgorithmInformation(
                new Algorithm(
                    "Move edge from front left and flip.",
                    NotationMoveType.FrontCounterClockwise,
                    NotationMoveType.DownCounterClockwise),
                Checker.Edge(Location.FrontLeftEdge, PuzzleColor.Blue, PuzzleColor.White));

        private static AlgorithmInformation BackFaceDownSideNoFlip()
            => new AlgorithmInformation(
                new Algorithm("Move edge from back bottom.", NotationMoveType.DownClockwise),
                Checker.Edge(Location.BackDownEdge, PuzzleColor.Blue, PuzzleColor.White));

        private static AlgorithmInformation BackFaceDownSideFlip()
            => new AlgorithmInformation(
                new Algorithm(
                    "Move edge from back bottom and flip.",
                    NotationMoveType.BackCounterClockwise,
                    NotationMoveType.LeftCounterClockwise),
                Checker.Edge(Location.BackDownEdge, PuzzleColor.White, PuzzleColor.Blue));

        private static AlgorithmInformation BackFaceUpSideNoFlip()
            => new AlgorithmInformation(
                new Algorithm(
                    "Move edge from back top.",
                    NotationMoveType.UpCounterClockwise,
                    NotationMoveType.LeftDouble),
                Checker.Edge(Location.BackUpEdge, PuzzleColor.Blue, PuzzleColor.White));

        private static AlgorithmInformation BackFaceUpSideFlip()
            => new AlgorithmInformation(
                new Algorithm(
                    "Move edge from back top and flip.",
                    NotationMoveType.BackClockwise,
                    NotationMoveType.LeftCounterClockwise),
                Checker.Edge(Location.BackUpEdge, PuzzleColor.White, PuzzleColor.Blue));

        private static AlgorithmInformation FrontFaceDownSideNoFlip()
            => new AlgorithmInformation(
                new Algorithm("Move edge from front bottom.", NotationMoveType.DownCounterClockwise),
                Checker.Edge(Location.FrontDownEdge, PuzzleColor.Blue, PuzzleColor.White));

        private static AlgorithmInformation FrontFaceDownSideFlip()
            => new AlgorithmInformation(
                new Algorithm(
                    "Move edge from front bottom and flip.",
                    NotationMoveType.FrontClockwise,
                    NotationMoveType.LeftClockwise),
                Checker.Edge(Location.FrontDownEdge, PuzzleColor.White, PuzzleColor.Blue));

        private static AlgorithmInformation FrontFaceUpSideNoFlip()
            => new AlgorithmInformation(
                new Algorithm(
                    "Move edge from front up.",
                    NotationMoveType.FrontDouble,
                    NotationMoveType.DownCounterClockwise),
                Checker.Edge(Location.FrontUpEdge, PuzzleColor.Blue, PuzzleColor.White));

        private static AlgorithmInformation FrontFaceUpSideFlip()
            => new AlgorithmInformation(
                new Algorithm(
                    "Move edge from front up and flip.",
                    NotationMoveType.FrontCounterClockwise,
                    NotationMoveType.LeftClockwise),
                Checker.Edge(Location.FrontUpEdge, PuzzleColor.White, PuzzleColor.Blue));

        private static AlgorithmInformation RightFaceDownSideNoFlip()
            => new AlgorithmInformation(
                new Algorithm("Move edge from right down.", NotationMoveType.DownDouble),
                Checker.Edge(Location.RightDownEdge, PuzzleColor.Blue, PuzzleColor.White));

        private static AlgorithmInformation RightFaceDownSideFlip()
            => new AlgorithmInformation(
                new Algorithm(
                    "Move edge from right down and flip.",
                    NotationMoveType.RightClockwise,
                    NotationMoveType.FrontClockwise,
                    NotationMoveType.DownCounterClockwise),
                Checker.Edge(Location.RightDownEdge, PuzzleColor.White, PuzzleColor.Blue));

        private static AlgorithmInformation RightFaceBackSideNoFlip()
            => new AlgorithmInformation(
                new Algorithm(
                    "Move edge from right back.",
                    NotationMoveType.BackCounterClockwise,
                    NotationMoveType.DownClockwise),
                Checker.Edge(Location.RightBackEdge, PuzzleColor.White, PuzzleColor.Blue));

        private static AlgorithmInformation RightFaceBackSideFlip()
            => new AlgorithmInformation(
                new Algorithm(
                    "Move edge from right back and flip.",
                    NotationMoveType.RightClockwise,
                    NotationMoveType.DownDouble),
                Checker.Edge(Location.RightBackEdge, PuzzleColor.Blue, PuzzleColor.White));

        private static AlgorithmInformation RightFaceFrontSideNoFlip()
            => new AlgorithmInformation(
                new Algorithm(
                    "Move edge from right front.",
                    NotationMoveType.RightCounterClockwise,
                    NotationMoveType.DownDouble),
                Checker.Edge(Location.RightFrontEdge, PuzzleColor.Blue, PuzzleColor.White));

        private static AlgorithmInformation RightFaceFrontSideFlip()
            => new AlgorithmInformation(
                new Algorithm(
                    "Move edge from right front and flip.",
                    NotationMoveType.FrontClockwise,
                    NotationMoveType.DownCounterClockwise),
                Checker.Edge(Location.RightFrontEdge, PuzzleColor.White, PuzzleColor.Blue));

        private static AlgorithmInformation RightFaceFrontUpNoFlip()
            => new AlgorithmInformation(
                new Algorithm(
                    "Move edge from right up.",
                    NotationMoveType.UpDouble,
                    NotationMoveType.LeftDouble),
                Checker.Edge(Location.RightUpEdge, PuzzleColor.Blue, PuzzleColor.White));

        private static AlgorithmInformation RightFaceFrontUpFlip()
            => new AlgorithmInformation(
                new Algorithm(
                    "Move edge from right up.",
                    NotationMoveType.UpClockwise,
                    NotationMoveType.FrontCounterClockwise,
                    NotationMoveType.LeftClockwise),
                Checker.Edge(Location.RightUpEdge, PuzzleColor.White, PuzzleColor.Blue));

        private static Func<Puzzle, bool> BuildEndGoal()
            => puzzle => Checker.Edge(Location.LeftDownEdge, PuzzleColor.Blue, PuzzleColor.White)(puzzle)
                && MoveBlueCenterToLeftFaceStep.Instance.EndGoal(puzzle);
    }
}