using System.Collections.Generic;
using System.Linq;
using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3.Roux
{
    public class Solver : SolverBase
    {
        public Solver()
            : base()
        {
        }

        public static Puzzle Solved { get; } = BuildSolvedPuzzle();

        public override IEnumerable<StepBase> NextSteps(Puzzle puzzle)
        {
            if (!MoveBlueCenterToLeftFaceStep.Instance.EndGoal.Check(puzzle))
            {
                return new StepBase[] { MoveBlueCenterToLeftFaceStep.Instance };
            }

            if (!FirstPieceToBottomLeftEdgeStep.Instance.EndGoal.Check(puzzle))
            {
                return new StepBase[] { FirstPieceToBottomLeftEdgeStep.Instance };
            }

            return Enumerable.Empty<StepBase>();
        }

        private static Puzzle BuildSolvedPuzzle()
            => new Puzzle(
                new Face(FaceName.Up, PuzzleColor.Yellow),
                new Face(FaceName.Front, PuzzleColor.Red),
                new Face(FaceName.Down, PuzzleColor.White),
                new Face(FaceName.Back, PuzzleColor.Orange),
                new Face(FaceName.Left, PuzzleColor.Blue),
                new Face(FaceName.Right, PuzzleColor.Green));
    }
}