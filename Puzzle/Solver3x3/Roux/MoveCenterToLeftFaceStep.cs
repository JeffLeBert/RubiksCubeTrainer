using System.Collections.Generic;
using System.Linq;
using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3.Roux
{
    public class MoveCenterToLeftFaceStep : StepBase
    {
        private MoveCenterToLeftFaceStep()
            : base()
        {
        }

        public static MoveCenterToLeftFaceStep Instance { get; } = new MoveCenterToLeftFaceStep();

        protected override Goal BuildEndGoal()
        {
            var goal = new Goal(null);
            goal.Checkers.Add(
                new SingleColorChecker(
                    new Location(FaceName.Left, -1, 0, 0),
                    PuzzleColor.Blue));

            return goal;
        }

        public override IEnumerable<AlgorithmInformation> GetPossibleAlgorithms(Puzzle puzzle)
            => from stepInfo in this.AllAlgorithms
               where stepInfo.PassesAllChecks(puzzle)
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
                new SingleColorChecker(new Location(FaceName.Back, 0, 1, 0), PuzzleColor.Blue));

        private static AlgorithmInformation MoveFaceFromFront()
            => new AlgorithmInformation(
                new Algorithm(
                    "Move blue face from front to left.",
                    new NotationMoveType(NotationRotationNames.AllClockwise, NotationRotationType.Clockwise)),
                new SingleColorChecker(new Location(FaceName.Front, 0, -1, 0), PuzzleColor.Blue));

        private static AlgorithmInformation MoveFaceFromRight()
            => new AlgorithmInformation(
                new Algorithm(
                    "Move blue face from right to left.",
                    new NotationMoveType(NotationRotationNames.AllClockwise, NotationRotationType.Double)),
                new SingleColorChecker(new Location(FaceName.Right, 1, 0, 0), PuzzleColor.Blue));

        private static AlgorithmInformation MoveFaceFromUp()
            => new AlgorithmInformation(
                new Algorithm(
                    "Move blue face from top to left.",
                    new NotationMoveType(NotationRotationNames.AllRight, NotationRotationType.CounterClockwise)),
                new SingleColorChecker(new Location(FaceName.Up, 0, 0, 1), PuzzleColor.Blue));

        private static AlgorithmInformation MoveFaceFromDown()
            => new AlgorithmInformation(
                new Algorithm(
                    "Move blue face from down to left.",
                    new NotationMoveType(NotationRotationNames.AllRight, NotationRotationType.Clockwise)),
                new SingleColorChecker(new Location(FaceName.Down, 0, 0, -1), PuzzleColor.Blue));
    }
}