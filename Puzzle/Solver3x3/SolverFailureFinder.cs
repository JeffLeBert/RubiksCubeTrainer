using System;
using System.Collections.Immutable;
using System.Linq;
using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3
{
    public static class SolverFailureFinder
    {
        private static IChecker solvedChecker = BuildSolvedChecker();

        public static SolverFailureInformation FindFailure(Solver solver, Puzzle puzzle)
        {
            var failInfo = FindFailure(solver, puzzle, SolverFailureInformation.Empty);

            if (!failInfo.Solved)
            {
                return failInfo;
            }

            var solvedPuzzle = Rotator.ApplyMoves(puzzle, failInfo.Algorithms);
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
            AlgorithmCollection[] possibleAlgorithmCollections)
        {
            if (failureInfo.Algorithms.Length > 50)
            {
                return failureInfo.WithFailed("Cycle found.");
            }

            foreach (var algorithmCollection in possibleAlgorithmCollections)
            {
                foreach (var algorithm in algorithmCollection.Algorithms)
                {
                    var newPuzzle = Rotator.ApplyMoves(puzzle, algorithm);
                    if (!step.FinishedState.Matches(newPuzzle))
                    {
                        return failureInfo.WithFailed(
                            $"{algorithmCollection.Description}\r\nAlgorithm: {NotationParser.FormatMoves(algorithm)}\r\nStep initial state: {step.InitialState.ToString()}\r\nAlgorithm initial state: {algorithmCollection.InitialState.ToString()}\r\nFinished state: {step.FinishedState.ToString()}");
                    }

                    var algorithmFailureInfo = FindFailure(
                        solver,
                        newPuzzle,
                        failureInfo.WithAlgorithm(algorithm));
                    if (algorithmFailureInfo.AtEnd)
                    {
                        return algorithmFailureInfo;
                    }
                }
            }

            return failureInfo;
        }

        private static IChecker BuildSolvedChecker()
            => new AndChecker(
                ImmutableArray.Create<IChecker>(
                    SingleColorChecker.Create("Left", "Blue"),
                    EdgeChecker.Create("LeftDown", "Blue", "White"),
                    EdgeChecker.Create("LeftFront", "Blue", "Red"),
                    EdgeChecker.Create("LeftBack", "Blue", "Orange"),
                    CornerChecker.Create("LeftFrontDown", "Blue", "White", "Red"),
                    CornerChecker.Create("LeftBackDown", "Blue", "Orange", "White")));
    }
}