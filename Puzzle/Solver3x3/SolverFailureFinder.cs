using System.Collections.Generic;
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
            var solvedState = solver.States["Solved"];
            return solvedState.Matches(solvedPuzzle)
                ? failInfo
                : failInfo.WithFailed("Solution not found.");
        }

        private static SolverFailureInformation FindFailure(Solver solver, Puzzle puzzle, SolverFailureInformation failureInfo)
            => FindFailure(solver, puzzle, failureInfo, solver.PossibleAlgorithms(puzzle));

        private static SolverFailureInformation FindFailure(
            Solver solver,
            Puzzle puzzle,
            SolverFailureInformation failureInfo,
            IEnumerable<Algorithm> possibleAlgorithms)
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
                    if (!algorithm.FinishedState.Matches(newPuzzle))
                    {
                        return failureInfo.WithFailed(
                            $"Algorithm name: {algorithm.Name}\r\nDescription: {algorithm.Description}\r\nMoves: {NotationParser.FormatMoves(moves)}\r\nInitial state: {algorithm.InitialState.ToString()}\r\nFinished state: {algorithm.FinishedState.ToString()}");
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