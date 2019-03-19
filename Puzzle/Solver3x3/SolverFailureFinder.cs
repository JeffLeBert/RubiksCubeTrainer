using System.Linq;
using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3
{
    public static class SolverFailureFinder
    {
        public static SolverFailureInformation FindFailure(Solver solver, Puzzle puzzle)
        {
            var failInfo = FindFailure(solver, puzzle, SolverFailureInformation.Empty);

            if (!failInfo.Solved)
            {
                return failInfo;
            }

            var solvedPuzzle = Rotator.ApplyMoves(puzzle, failInfo.Algorithms);
            var solvedChecker = solver.States["Solved"];
            return solvedChecker.Matches(solvedPuzzle)
                ? failInfo
                : failInfo.WithFailed("Solution not found.");
        }

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
            Step[] possibleSteps)
        {
            foreach (var possibleStep in possibleSteps)
            {
                var possibleAlgorithms = possibleStep.GetPossibleAlgorithms(puzzle).ToArray();
                if (!possibleAlgorithms.Any())
                {
                    return failureInfo.WithFailed("No more algorithms found.");
                }

                var stepsFailureInfo = FindFailure(solver, puzzle, failureInfo, possibleStep, possibleAlgorithms);
                if (stepsFailureInfo.AtEnd)
                {
                    return stepsFailureInfo;
                }
            }

            return failureInfo.WithSolved();
        }

        private static SolverFailureInformation FindFailure(
            Solver solver,
            Puzzle puzzle,
            SolverFailureInformation failureInfo,
            Step step,
            Algorithm[] possibleAlgorithms)
        {
            if (failureInfo.Algorithms.Length > 50)
            {
                return failureInfo.WithFailed("Cycle found.");
            }

            foreach (var algorithm in possibleAlgorithms)
            {
                foreach (var moves in algorithm.Moves)
                {
                    var newPuzzle = Rotator.ApplyMoves(puzzle, moves);
                    if (!step.FinishedState.Matches(newPuzzle))
                    {
                        return failureInfo.WithFailed(
                            $"Step name: {step.Name}\r\nAlgorithm name: {algorithm.Name}\r\nDescription: {algorithm.Description}\r\nMoves: {NotationParser.FormatMoves(moves)}\r\nStep initial state: {step.InitialState.ToString()}\r\nAlgorithm initial state: {algorithm.InitialState.ToString()}\r\nFinished state: {step.FinishedState.ToString()}");
                    }

                    var algorithmFailureInfo = FindFailure(
                        solver,
                        newPuzzle,
                        failureInfo.WithMoves(moves));
                    if (algorithmFailureInfo.AtEnd)
                    {
                        return algorithmFailureInfo;
                    }
                }
            }

            return failureInfo;
        }
    }
}