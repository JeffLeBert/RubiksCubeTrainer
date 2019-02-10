using System;
using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3
{
    public class AlgorithmInformation
    {
        public AlgorithmInformation(Algorithm algorithm, Func<Puzzle, bool> goal)
        {
            this.Algorithm = algorithm;
            this.Goal = goal;
        }

        public Algorithm Algorithm { get; }

        public Func<Puzzle, bool> Goal { get; }
    }
}