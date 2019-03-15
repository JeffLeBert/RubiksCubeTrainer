using System.Collections.Immutable;
using System.Linq;
using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3
{
    public class OrChecker : IChecker
    {
        public OrChecker(ImmutableArray<IChecker> checkers)
        {
            this.Checkers = checkers;
        }

        public ImmutableArray<IChecker> Checkers { get; }

        public bool Matches(Puzzle puzzle)
        {
            for (int i = 0; i < this.Checkers.Length; i++)
            {
                if (this.Checkers[i].Matches(puzzle))
                {
                    return true;
                }
            }

            return false;
        }

        public override string ToString()
            => "("
            + string.Join(" || ", from checker in this.Checkers select checker.ToString())
            + ")";

        public IChecker WithColors(PuzzleColor[] colors)
            => new OrChecker(
                ImmutableArray.CreateRange(
                    from checker in this.Checkers
                    select checker.WithColors(colors)));

        public IChecker Negate()
            => new AndChecker(
                ImmutableArray.CreateRange(
                    from checker in this.Checkers
                    select checker.Negate()));
    }
}