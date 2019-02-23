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

        public override IEnumerable<IStep> NextSteps(Puzzle puzzle)
        {
            if (MoveBlueCenterToLeftFaceStep.Instance.ShouldUse(puzzle))
            {
                return new IStep[] { MoveBlueCenterToLeftFaceStep.Instance };
            }

            if (FirstPieceToBottomLeftEdgeStep.Instance.ShouldUse(puzzle))
            {
                return new IStep[] { FirstPieceToBottomLeftEdgeStep.Instance };
            }

            if (MoveEdgeToFrontBottomStep.InstanceFrontLeft.ShouldUse(puzzle))
            {
                return new IStep[]
                {
                    MoveEdgeToFrontBottomStep.InstanceFrontLeft,
                    MoveCornerToTopStep.InstanceLeftFrontDown
                };
            }

            //if (MoveEdgeToFrontBottomStep.InstanceBackLeft.ShouldUse(puzzle))
            //{
            //    return new IStep[]
            //    {
            //        MoveEdgeToFrontBottomStep.InstanceBackLeft,
            //        MoveCornerToTopStep.InstanceLeftBackDown
            //    };
            //}

            //if (MoveEdgeToFrontBottomStep.InstanceBackRight.ShouldUse(puzzle))
            //{
            //    return new IStep[]
            //    {
            //        MoveEdgeToFrontBottomStep.InstanceBackRight,
            //        MoveCornerToTopStep.InstanceRightBackDown
            //    };
            //}

            //if (MoveEdgeToFrontBottomStep.InstanceFrontRight.ShouldUse(puzzle))
            //{
            //    return new IStep[]
            //    {
            //        MoveEdgeToFrontBottomStep.InstanceFrontRight,
            //        MoveCornerToTopStep.InstanceRightFrontDown
            //    };
            //}

            return Enumerable.Empty<IStep>();
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