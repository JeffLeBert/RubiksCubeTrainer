using System;
using System.Collections.Generic;
using System.Linq;
using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3.Roux
{
    public class FirstPieceToBottomLeftEdgeStep : StepBase
    {
        private FirstPieceToBottomLeftEdgeStep()
            : base()
        {
        }

        public static FirstPieceToBottomLeftEdgeStep Instance { get; } = new FirstPieceToBottomLeftEdgeStep();

        protected override Func<Puzzle, bool> BuildEndGoal()
        {
            var goal = Checker.Edge(
                new Location(FaceName.Left, -1, 0, -1),
                PuzzleColor.Blue,
                PuzzleColor.White);

            return puzzle => goal(puzzle) && MoveBlueCenterToLeftFaceStep.Instance.EndGoal(puzzle);
        }

        public override IEnumerable<AlgorithmInformation> GetPossibleAlgorithms(Puzzle puzzle)
            => from stepInfo in this.AllAlgorithms
               where stepInfo.Goal(puzzle)
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
                    new NotationMoveType(NotationRotationNames.Down, NotationRotationType.Clockwise),
                    new NotationMoveType(NotationRotationNames.Front, NotationRotationType.Clockwise),
                    new NotationMoveType(NotationRotationNames.Left, NotationRotationType.Clockwise)),
                Checker.Edge(
                    new Location(FaceName.Down, -1, 0, -1),
                    PuzzleColor.Blue,
                    PuzzleColor.White));

        private static AlgorithmInformation BackFaceLeftSideNoFlip()
            => new AlgorithmInformation(
                new Algorithm(
                    "Move edge from back left.",
                    new NotationMoveType(NotationRotationNames.Left, NotationRotationType.CounterClockwise)),
                Checker.Edge(
                    new Location(FaceName.Back, -1, 1, 0),
                    PuzzleColor.White,
                    PuzzleColor.Blue));

        private static AlgorithmInformation BackFaceLeftSideFlip()
            => new AlgorithmInformation(
                new Algorithm(
                    "Move edit from back left and flip.",
                    new NotationMoveType(NotationRotationNames.Back, NotationRotationType.Clockwise),
                    new NotationMoveType(NotationRotationNames.Down, NotationRotationType.Clockwise)),
                Checker.Edge(
                    new Location(FaceName.Back, -1, 1, 0),
                    PuzzleColor.Blue,
                    PuzzleColor.White));

        private static AlgorithmInformation TopFaceLeftSideNoFlip()
            => new AlgorithmInformation(
                new Algorithm(
                    "Move edge from top left.",
                    new NotationMoveType(NotationRotationNames.Left, NotationRotationType.Double)),
                Checker.Edge(
                    new Location(FaceName.Left, -1, 0, 1),
                    PuzzleColor.Blue,
                    PuzzleColor.White));

        private static AlgorithmInformation TopFaceLeftSideFlip()
            => new AlgorithmInformation(
                new Algorithm(
                    "Move edge from top left and flip.",
                    new NotationMoveType(NotationRotationNames.Left, NotationRotationType.CounterClockwise),
                    new NotationMoveType(NotationRotationNames.Back, NotationRotationType.Clockwise),
                    new NotationMoveType(NotationRotationNames.Down, NotationRotationType.Clockwise)),
                Checker.Edge(
                    new Location(FaceName.Left, -1, 0, 1),
                    PuzzleColor.White,
                    PuzzleColor.Blue));

        private static AlgorithmInformation FrontFaceLeftSideNoFlip()
            => new AlgorithmInformation(
                new Algorithm(
                    "Move edge from front left.",
                    new NotationMoveType(NotationRotationNames.Left, NotationRotationType.Clockwise)),
                Checker.Edge(
                    new Location(FaceName.Front, -1, -1, 0),
                    PuzzleColor.White,
                    PuzzleColor.Blue));

        private static AlgorithmInformation FrontFaceLeftSideFlip()
            => new AlgorithmInformation(
                new Algorithm(
                    "Move edge from front left and flip.",
                    new NotationMoveType(NotationRotationNames.Front, NotationRotationType.CounterClockwise),
                    new NotationMoveType(NotationRotationNames.Down, NotationRotationType.CounterClockwise)),
                Checker.Edge(
                    new Location(FaceName.Front, -1, -1, 0),
                    PuzzleColor.Blue,
                    PuzzleColor.White));

        private static AlgorithmInformation BackFaceDownSideNoFlip()
            => new AlgorithmInformation(
                new Algorithm(
                    "Move edge from back bottom.",
                    new NotationMoveType(NotationRotationNames.Down, NotationRotationType.Clockwise)),
                Checker.Edge(
                    new Location(FaceName.Back, 0, 1, -1),
                    PuzzleColor.Blue,
                    PuzzleColor.White));

        private static AlgorithmInformation BackFaceDownSideFlip()
            => new AlgorithmInformation(
                new Algorithm(
                    "Move edge from back bottom and flip.",
                    new NotationMoveType(NotationRotationNames.Back, NotationRotationType.CounterClockwise),
                    new NotationMoveType(NotationRotationNames.Left, NotationRotationType.CounterClockwise)),
                Checker.Edge(
                    new Location(FaceName.Back, 0, 1, -1),
                    PuzzleColor.White,
                    PuzzleColor.Blue));

        private static AlgorithmInformation BackFaceUpSideNoFlip()
            => new AlgorithmInformation(
                new Algorithm(
                    "Move edge from back top.",
                    new NotationMoveType(NotationRotationNames.Up, NotationRotationType.CounterClockwise),
                    new NotationMoveType(NotationRotationNames.Left, NotationRotationType.Double)),
                Checker.Edge(
                    new Location(FaceName.Back, 0, 1, 1),
                    PuzzleColor.Blue,
                    PuzzleColor.White));

        private static AlgorithmInformation BackFaceUpSideFlip()
            => new AlgorithmInformation(
                new Algorithm(
                    "Move edge from back top and flip.",
                    new NotationMoveType(NotationRotationNames.Back, NotationRotationType.Clockwise),
                    new NotationMoveType(NotationRotationNames.Left, NotationRotationType.CounterClockwise)),
                Checker.Edge(
                    new Location(FaceName.Back, 0, 1, 1),
                    PuzzleColor.White,
                    PuzzleColor.Blue));

        private static AlgorithmInformation FrontFaceDownSideNoFlip()
            => new AlgorithmInformation(
                new Algorithm(
                    "Move edge from front bottom.",
                    new NotationMoveType(NotationRotationNames.Down, NotationRotationType.CounterClockwise)),
                Checker.Edge(
                    new Location(FaceName.Front, 0, -1, -1),
                    PuzzleColor.Blue,
                    PuzzleColor.White));

        private static AlgorithmInformation FrontFaceDownSideFlip()
            => new AlgorithmInformation(
                new Algorithm(
                    "Move edge from front bottom and flip.",
                    new NotationMoveType(NotationRotationNames.Front, NotationRotationType.Clockwise),
                    new NotationMoveType(NotationRotationNames.Left, NotationRotationType.Clockwise)),
                Checker.Edge(
                    new Location(FaceName.Front, 0, -1, -1),
                    PuzzleColor.White,
                    PuzzleColor.Blue));

        private static AlgorithmInformation FrontFaceUpSideNoFlip()
            => new AlgorithmInformation(
                new Algorithm(
                    "Move edge from front up.",
                    new NotationMoveType(NotationRotationNames.Front, NotationRotationType.Double),
                    new NotationMoveType(NotationRotationNames.Down, NotationRotationType.CounterClockwise)),
                Checker.Edge(
                    new Location(FaceName.Front, 0, -1, 1),
                    PuzzleColor.Blue,
                    PuzzleColor.White));

        private static AlgorithmInformation FrontFaceUpSideFlip()
            => new AlgorithmInformation(
                new Algorithm(
                    "Move edge from front up and flip.",
                    new NotationMoveType(NotationRotationNames.Front, NotationRotationType.CounterClockwise),
                    new NotationMoveType(NotationRotationNames.Left, NotationRotationType.Clockwise)),
                Checker.Edge(
                    new Location(FaceName.Front, 0, -1, 1),
                    PuzzleColor.White,
                    PuzzleColor.Blue));

        private static AlgorithmInformation RightFaceDownSideNoFlip()
            => new AlgorithmInformation(
                new Algorithm(
                    "Move edge from right down.",
                    new NotationMoveType(NotationRotationNames.Down, NotationRotationType.Double)),
                Checker.Edge(
                    new Location(FaceName.Right, 1, 0, -1),
                    PuzzleColor.Blue,
                    PuzzleColor.White));

        private static AlgorithmInformation RightFaceDownSideFlip()
            => new AlgorithmInformation(
                new Algorithm(
                    "Move edge from right down and flip.",
                    new NotationMoveType(NotationRotationNames.Right, NotationRotationType.Clockwise),
                    new NotationMoveType(NotationRotationNames.Front, NotationRotationType.Clockwise),
                    new NotationMoveType(NotationRotationNames.Down, NotationRotationType.CounterClockwise)),
                Checker.Edge(
                    new Location(FaceName.Right, 1, 0, -1),
                    PuzzleColor.White,
                    PuzzleColor.Blue));

        private static AlgorithmInformation RightFaceBackSideNoFlip()
            => new AlgorithmInformation(
                new Algorithm(
                    "Move edge from right back.",
                    new NotationMoveType(NotationRotationNames.Back, NotationRotationType.CounterClockwise),
                    new NotationMoveType(NotationRotationNames.Down, NotationRotationType.Clockwise)),
                Checker.Edge(
                    new Location(FaceName.Right, 1, 1, 0),
                    PuzzleColor.White,
                    PuzzleColor.Blue));

        private static AlgorithmInformation RightFaceBackSideFlip()
            => new AlgorithmInformation(
                new Algorithm(
                    "Move edge from right back and flip.",
                    new NotationMoveType(NotationRotationNames.Right, NotationRotationType.Clockwise),
                    new NotationMoveType(NotationRotationNames.Down, NotationRotationType.Double)),
                Checker.Edge(
                    new Location(FaceName.Right, 1, 1, 0),
                    PuzzleColor.Blue,
                    PuzzleColor.White));

        private static AlgorithmInformation RightFaceFrontSideNoFlip()
            => new AlgorithmInformation(
                new Algorithm(
                    "Move edge from right front.",
                    new NotationMoveType(NotationRotationNames.Right, NotationRotationType.CounterClockwise),
                    new NotationMoveType(NotationRotationNames.Down, NotationRotationType.Double)),
                Checker.Edge(
                    new Location(FaceName.Right, 1, -1, 0),
                    PuzzleColor.Blue,
                    PuzzleColor.White));

        private static AlgorithmInformation RightFaceFrontSideFlip()
            => new AlgorithmInformation(
                new Algorithm(
                    "Move edge from right front and flip.",
                    new NotationMoveType(NotationRotationNames.Front, NotationRotationType.Clockwise),
                    new NotationMoveType(NotationRotationNames.Down, NotationRotationType.CounterClockwise)),
                Checker.Edge(
                    new Location(FaceName.Right, 1, -1, 0),
                    PuzzleColor.White,
                    PuzzleColor.Blue));

        private static AlgorithmInformation RightFaceFrontUpNoFlip()
            => new AlgorithmInformation(
                new Algorithm(
                    "Move edge from right up.",
                    new NotationMoveType(NotationRotationNames.Up, NotationRotationType.Double),
                    new NotationMoveType(NotationRotationNames.Left, NotationRotationType.Double)),
                Checker.Edge(
                    new Location(FaceName.Right, 1, 0, 1),
                    PuzzleColor.Blue,
                    PuzzleColor.White));

        private static AlgorithmInformation RightFaceFrontUpFlip()
            => new AlgorithmInformation(
                new Algorithm(
                    "Move edge from right up.",
                    new NotationMoveType(NotationRotationNames.Up, NotationRotationType.Clockwise),
                    new NotationMoveType(NotationRotationNames.Front, NotationRotationType.CounterClockwise),
                    new NotationMoveType(NotationRotationNames.Left, NotationRotationType.Clockwise)),
                Checker.Edge(
                    new Location(FaceName.Right, 1, 0, 1),
                    PuzzleColor.White,
                    PuzzleColor.Blue));
    }
}