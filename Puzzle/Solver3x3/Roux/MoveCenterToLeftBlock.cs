using System.Collections.Generic;
using System.Linq;
using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3.Roux
{
    public class MoveCenterToLeftBlock : StepBase
    {
        public MoveCenterToLeftBlock(Puzzle startPuzzle)
            : base(startPuzzle, new Goal(null))
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
                    "Move blue face from back to left.",
                    new NotationMoveType(NotationRotationNames.AllClockwise, NotationRotationType.CounterClockwise)),
                this.NextStep,
                new SingleColorChecker(new Location(FaceName.Back, 0, 1, 0), PuzzleColor.Blue));

            // On front face.
            yield return new StepInformation(
                new Algorithm(
                    "Move blue face from front to left.",
                    new NotationMoveType(NotationRotationNames.AllClockwise, NotationRotationType.Clockwise)),
                this.NextStep,
                new SingleColorChecker(new Location(FaceName.Front, 0, -1, 0), PuzzleColor.Blue));

            // On right face.
            yield return new StepInformation(
                new Algorithm(
                    "Move blue face from right to left.",
                    new NotationMoveType(NotationRotationNames.AllClockwise, NotationRotationType.Double)),
                this.NextStep,
                new SingleColorChecker(new Location(FaceName.Right, 1, 0, 0), PuzzleColor.Blue));

            // On top face.
            yield return new StepInformation(
                new Algorithm(
                    "Move blue face from top to left.",
                    new NotationMoveType(NotationRotationNames.AllRight, NotationRotationType.CounterClockwise)),
                this.NextStep,
                new SingleColorChecker(new Location(FaceName.Up, 0, 0, 1), PuzzleColor.Blue));

            // On down face.
            yield return new StepInformation(
                new Algorithm(
                    "Move blue face from down to left.",
                    new NotationMoveType(NotationRotationNames.AllRight, NotationRotationType.Clockwise)),
                this.NextStep,
                new SingleColorChecker(new Location(FaceName.Down, 0, 0, -1), PuzzleColor.Blue));
        }

        private StepBase NextStep(Puzzle puzzle)
            => new FirstBlockBottomEdge(puzzle, this.BuildEndGoal());

        private Goal BuildEndGoal()
        {
            var goal = new Goal(this.StartGoal);
            goal.Checkers.Add(
                new SingleColorChecker(
                    new Location(FaceName.Left, -1, 0, 0),
                    PuzzleColor.Blue));

            return goal;
        }
    }
}