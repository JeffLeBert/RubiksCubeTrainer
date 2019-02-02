using System.Collections.Generic;
using System.Linq;
using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3.Roux
{
    public class FirstBlockBottomEdge : StepBase
    {
        public FirstBlockBottomEdge(Puzzle startPuzzle, Goal startGoal)
            : base(startPuzzle, startGoal)
        {
        }

        public override IEnumerable<StepInformation> GetPossibleSteps()
            => from stepInfo in GetAllAlgorithms()
               where stepInfo.PassesAllChecks(this.StartPuzzle)
               select stepInfo;

        private IEnumerable<StepInformation> GetAllAlgorithms()
        {
            // On back face.
            yield return new StepInformation(
                new Algorithm(
                    "Move cubie from back.",
                    new NotationMoveType(NotationRotationNames.Left, NotationRotationType.CounterClockwise)),
                NextStep,
                new EdgeCheckerer(
                    new Location(FaceName.Back, -1, 1, 0),
                    PuzzleColor.White,
                    PuzzleColor.Blue));

            //// On front face.
            //yield return new StepInformation(
            //    new Algorithm(
            //        "Move blue face from front to left.",
            //        new NotationMoveType(NotationRotationNames.AllClockwise, NotationRotationType.Clockwise)),
            //    NextStep,
            //    new SingleColorCheck(new Location(FaceName.Front, 0, -1, 0), PuzzleColor.Blue));

            //// On right face.
            //yield return new StepInformation(
            //    new Algorithm(
            //        "Move blue face from right to left.",
            //        new NotationMoveType(NotationRotationNames.AllClockwise, NotationRotationType.Double)),
            //    NextStep,
            //    new SingleColorCheck(new Location(FaceName.Right, 1, 0, 0), PuzzleColor.Blue));

            //// On top face.
            //yield return new StepInformation(
            //    new Algorithm(
            //        "Move blue face from top to left.",
            //        new NotationMoveType(NotationRotationNames.AllRight, NotationRotationType.CounterClockwise)),
            //    NextStep,
            //    new SingleColorCheck(new Location(FaceName.Up, 0, 0, 1), PuzzleColor.Blue));

            //// On down face.
            //yield return new StepInformation(
            //    new Algorithm(
            //        "Move blue face from down to left.",
            //        new NotationMoveType(NotationRotationNames.AllRight, NotationRotationType.Clockwise)),
            //    NextStep,
            //    new SingleColorCheck(new Location(FaceName.Down, 0, 0, -1), PuzzleColor.Blue));
        }

        private static StepBase NextStep(Puzzle puzzle)
            => null;

        private Goal BuildEndGoal()
        {
            var goal = new Goal(this.StartGoal);
            goal.Checkers.Add(
                new EdgeCheckerer(
                    new Location(FaceName.Left, -1, 0, -1),
                    PuzzleColor.Blue,
                    PuzzleColor.White));

            return goal;
        }
    }
}