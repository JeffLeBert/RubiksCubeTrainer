using System.Collections.Generic;
using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3.Roux
{
    public class MoveCenterToLeftBlock : StepBase
    {
        private static readonly Goal endGoal = BuildGoal();

        public MoveCenterToLeftBlock(Puzzle puzzle)
            : base(puzzle)
        {
        }

        private static Goal BuildGoal()
        {
            var goal = new Goal(null);
            goal.AddColor(new Location(FaceName.Left, -1, 0, 0), PuzzleColor.Blue);

            return goal;
        }

        public override IEnumerable<Algorithm> GetPossibleAlgorithms()
        {
            // No algorithms to run if already at goal.
            if (endGoal.Check(this.Puzzle))
            {
                return new Algorithm[] { };
            }

            return this.CheckAlgorithms(
                new Algorithm(
                    "Rotate puzzle counter clockwise.",
                    new NotationMoveType(NotationRotationNames.AllClockwise, NotationRotationType.CounterClockwise)),
                new Algorithm(
                    "Rotate puzzle clockwise.",
                    new NotationMoveType(NotationRotationNames.AllClockwise, NotationRotationType.Clockwise)),
                new Algorithm(
                    "Rotate puzzle clockwise twice.",
                    new NotationMoveType(NotationRotationNames.AllClockwise, NotationRotationType.Double)),
                new Algorithm(
                    "Rotate bottom of puzzle to left side.",
                    new NotationMoveType(NotationRotationNames.AllRight, NotationRotationType.Clockwise)),
                new Algorithm(
                    "Rotate top of puzzle to left side.",
                    new NotationMoveType(NotationRotationNames.AllRight, NotationRotationType.CounterClockwise)));
        }

        private IEnumerable<Algorithm> CheckAlgorithms(params Algorithm[] algorithms)
        {
            foreach (var algorithm in algorithms)
            {
                var newPuzzle = Rotator.ApplyMoves(this.Puzzle, algorithm.Moves);
                if (endGoal.Check(newPuzzle))
                {
                    yield return algorithm;
                }
            }
        }
    }
}