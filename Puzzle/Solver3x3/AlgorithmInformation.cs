using System;
using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3
{
    public class AlgorithmInformation
    {
        public AlgorithmInformation(Algorithm algorithm, Func<Puzzle, bool> initialPosition)
        {
            this.Algorithm = algorithm;
            this.InitialPosition = initialPosition;
        }

        public Algorithm Algorithm { get; }

        public Func<Puzzle, bool> InitialPosition { get; }
    }
}