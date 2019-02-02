using System;
using System.Linq;
using RubiksCubeTrainer.Puzzle3x3;
using RubiksCubeTrainer.Solver3x3.Roux;

namespace RubiksCubeTrainer.Solver3x3
{
    public static class SolverFailureFinder
    {
        public static SolverBase FindFailure(Func<Puzzle, SolverBase> solverCreator)
        {
            var scramble = Scrambler.Scamble(10);
            var scrambledPuzzle = Rotator.ApplyMoves(Puzzle.Solved, NotationParser.EnumerateMoves(scramble));
            var solver = solverCreator(scrambledPuzzle);

            return FindFailure(solver);
        }

        public static SolverBase FindFailure(SolverBase solver)
        {
            var possibleSteps = solver.CurrentStep.GetPossibleSteps().ToArray();

            // If there are no possible moves then this is a failure.
            if (!possibleSteps.Any())
            {
                return solver;
            }

            return FindFailure(solver, possibleSteps);
        }

        private static SolverBase FindFailure(SolverBase solver, StepInformation[] possibleSteps)
        {
            foreach (var possibleStep in possibleSteps)
            {
                var newSolver = solver.NextSolver(possibleStep);
                if (newSolver.CurrentStep != null)
                {
                    return FindFailure(newSolver);
                }
            }

            return null;
        }
    }
}