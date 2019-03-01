using System.Linq;
using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3
{
    public static class SolverFailureFinder
    {
        public static SolverFailureInformation FindFailure(Solver solver, Puzzle puzzle)
            => FindFailure(solver, puzzle, SolverFailureInformation.Empty);

        private static SolverFailureInformation FindFailure(
            Solver solver,
            Puzzle puzzle,
            SolverFailureInformation failureInfo)
        {
            var nextSteps = solver.NextSteps(puzzle).ToArray();
            return FindFailure(solver, puzzle, failureInfo, nextSteps);
        }

        private static SolverFailureInformation FindFailure(
            Solver solver,
            Puzzle puzzle,
            SolverFailureInformation failureInfo,
            IStep[] possibleSteps)
        {
            foreach (var possibleStep in possibleSteps)
            {
                var possibleAlgorithms = possibleStep.GetPossibleAlgorithms(puzzle).ToArray();
                if (!possibleAlgorithms.Any())
                {
                    return failureInfo.WithNoMoreAlgorithms();
                }

                var stepsFailureInfo = FindFailure(solver, puzzle, failureInfo, possibleAlgorithms);
                if (stepsFailureInfo.AtEnd)
                {
                    return stepsFailureInfo;
                }
            }

            return failureInfo.WithNoMoreSteps();
        }

        private static SolverFailureInformation FindFailure(
            Solver solver,
            Puzzle puzzle,
            SolverFailureInformation failureInfo,
            AlgorithmCollection[] possibleAlgorithms)
        {
            foreach (var possibleAlgorithm in possibleAlgorithms)
            {
                var newPuzzle = Rotator.ApplyMoves(puzzle, possibleAlgorithm.Algorithms[0]);
                var algorithmFailureInfo = FindFailure(
                    solver,
                    newPuzzle,
                    failureInfo.WithMoves(possibleAlgorithm.Algorithms[0]));
                if (algorithmFailureInfo.AtEnd)
                {
                    return algorithmFailureInfo;
                }
            }

            return failureInfo;
        }
    }
}