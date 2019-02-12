using System;
using System.Collections.Generic;
using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3
{
    public interface IStep
    {
        Func<Puzzle, bool> EndGoal { get; }

        IEnumerable<Algorithm> GetPossibleAlgorithms(Puzzle puzzle);
    }
}