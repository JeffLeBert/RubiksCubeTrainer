using System.Collections.Generic;
using System.Linq;
using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3
{
    public class OrChecker : IChecker
    {
        public OrChecker(params IChecker[] checkers)
        {
            this.Checkers = checkers;
        }

        public IEnumerable<IChecker> Checkers { get; }

        public bool Check(Puzzle puzzle)
            => this.Checkers.Any(checker => checker.Check(puzzle));
    }
}