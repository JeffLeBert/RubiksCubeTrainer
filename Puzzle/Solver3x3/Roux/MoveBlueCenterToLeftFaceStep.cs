using System;
using System.Collections.Generic;
using System.Linq;
using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3.Roux
{
    public class MoveBlueCenterToLeftFaceStep : StepBase
    {
        private MoveBlueCenterToLeftFaceStep()
            : base()
        {
        }

        public static MoveBlueCenterToLeftFaceStep Instance { get; } = new MoveBlueCenterToLeftFaceStep();

        public override IEnumerable<AlgorithmInformation> GetPossibleAlgorithms(Puzzle puzzle)
            => from stepInfo in this.AllAlgorithms
               where stepInfo.Goal(puzzle)
               select stepInfo;

        protected override AlgorithmInformation[] BuildAllAlgorithms()
            => new AlgorithmInformation[]
            {
                MoveFaceFromBack(),
                MoveFaceFromFront(),
                MoveFaceFromRight(),
                MoveFaceFromUp(),
                MoveFaceFromDown()
            };

        private static AlgorithmInformation MoveFaceFromBack()
            => new AlgorithmInformation(
                new Algorithm(
                    "Move blue face from back to left.",
                    new NotationMoveType(NotationRotationNames.AllClockwise, NotationRotationType.CounterClockwise)),
                Checker.SingleColor(new Location(FaceName.Back, 0, 1, 0), PuzzleColor.Blue));

        private static AlgorithmInformation MoveFaceFromFront()
            => new AlgorithmInformation(
                new Algorithm(
                    "Move blue face from front to left.",
                    new NotationMoveType(NotationRotationNames.AllClockwise, NotationRotationType.Clockwise)),
                Checker.SingleColor(new Location(FaceName.Front, 0, -1, 0), PuzzleColor.Blue));

        private static AlgorithmInformation MoveFaceFromRight()
            => new AlgorithmInformation(
                new Algorithm(
                    "Move blue face from right to left.",
                    new NotationMoveType(NotationRotationNames.AllClockwise, NotationRotationType.Double)),
                Checker.SingleColor(new Location(FaceName.Right, 1, 0, 0), PuzzleColor.Blue));

        private static AlgorithmInformation MoveFaceFromUp()
            => new AlgorithmInformation(
                new Algorithm(
                    "Move blue face from top to left.",
                    new NotationMoveType(NotationRotationNames.AllRight, NotationRotationType.CounterClockwise)),
                Checker.SingleColor(new Location(FaceName.Up, 0, 0, 1), PuzzleColor.Blue));

        private static AlgorithmInformation MoveFaceFromDown()
            => new AlgorithmInformation(
                new Algorithm(
                    "Move blue face from down to left.",
                    new NotationMoveType(NotationRotationNames.AllRight, NotationRotationType.Clockwise)),
                Checker.SingleColor(new Location(FaceName.Down, 0, 0, -1), PuzzleColor.Blue));

        protected override Func<Puzzle, bool> BuildEndGoal()
            => Checker.SingleColor(new Location(FaceName.Left, -1, 0, 0), PuzzleColor.Blue);
    }
}