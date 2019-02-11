using System;
using System.Collections.Generic;
using System.Linq;
using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3.Roux
{
    public class MoveBlueCenterToLeftFaceStep : StepBase
    {
        private MoveBlueCenterToLeftFaceStep()
            : base(BuildEndGoal())
        {
        }

        public static MoveBlueCenterToLeftFaceStep Instance { get; } = new MoveBlueCenterToLeftFaceStep();

        public override IEnumerable<AlgorithmInformation> GetPossibleAlgorithms(Puzzle puzzle)
            => from stepInfo in this.AllAlgorithms
               where stepInfo.InitialPosition(puzzle)
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
                new Algorithm("Move blue face from back to left.", NotationMoveType.AllFrontRight),
                Checker.SingleColor(Location.BackCenter, PuzzleColor.Blue));

        private static AlgorithmInformation MoveFaceFromFront()
            => new AlgorithmInformation(
                new Algorithm("Move blue face from front to left.", NotationMoveType.AllFrontLeft),
                Checker.SingleColor(Location.FrontCenter, PuzzleColor.Blue));

        private static AlgorithmInformation MoveFaceFromRight()
            => new AlgorithmInformation(
                new Algorithm("Move blue face from right to left.", NotationMoveType.AllFrontLeftDouble),
                Checker.SingleColor(Location.RightCenter, PuzzleColor.Blue));

        private static AlgorithmInformation MoveFaceFromUp()
            => new AlgorithmInformation(
                new Algorithm("Move blue face from top to left.", NotationMoveType.AllFrontCounterClockwise),
                Checker.SingleColor(Location.UpCenter, PuzzleColor.Blue));

        private static AlgorithmInformation MoveFaceFromDown()
            => new AlgorithmInformation(
                new Algorithm("Move blue face from down to left.", NotationMoveType.AllFrontClockwise),
                Checker.SingleColor(Location.DownCenter, PuzzleColor.Blue));

        private static Func<Puzzle, bool> BuildEndGoal()
            => Checker.SingleColor(Location.LeftCenter, PuzzleColor.Blue);
    }
}