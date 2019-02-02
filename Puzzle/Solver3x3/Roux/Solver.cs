using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3.Roux
{
    public class Solver : SolverBase
    {
        private Solver(SolverBase parentSolver, StepBase step)
            : base(parentSolver, step)
        {
        }

        public static SolverBase Create(Puzzle puzzle)
            => new Solver(null, new MoveCenterToLeftBlock(puzzle));

        public override SolverBase NextSolver(StepInformation stepInformation)
        {
            var newPuzzle = Rotator.ApplyMoves(this.CurrentStep.StartPuzzle, stepInformation.Algorithm.Moves);
            return new Solver(this, stepInformation.NextStep(newPuzzle));
        }
    }
}