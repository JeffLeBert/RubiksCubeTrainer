using System.Collections.Generic;
using System.Linq;
using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3
{
    public class AlgorithmInformation
    {
        public AlgorithmInformation(Algorithm algorithm, params IChecker[] checkers)
            : this(algorithm, (IEnumerable<IChecker>)checkers)
        {
        }

        public AlgorithmInformation(Algorithm algorithm, IEnumerable<IChecker> checkers)
        {
            this.Checkers = checkers;
            this.Algorithm = algorithm;
        }

        public Algorithm Algorithm { get; }

        public IEnumerable<IChecker> Checkers { get; }

        public bool PassesAllChecks(Puzzle puzzle)
            => this.Checkers.All(checker => checker.Check(puzzle));
    }
}