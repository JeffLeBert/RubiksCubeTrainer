using System.Collections.Immutable;
using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3
{
    public class AndChecker : CheckerBase
    {
        public AndChecker(ImmutableArray<IChecker> checkers)
        {
            this.Checkers = checkers;
        }

        public ImmutableArray<IChecker> Checkers { get; }

        public override bool Matches(Puzzle puzzle)
        {
            for (int i = 0; i < this.Checkers.Length; i++)
            {
                if (!this.Checkers[i].Matches(puzzle))
                {
                    return false;
                }
            }

            return true;
        }
    }
}