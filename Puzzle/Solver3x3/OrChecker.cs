using System.Collections.Immutable;
using System.Linq;
using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3
{
    public class OrChecker : IChecker
    {
        public OrChecker(ImmutableArray<IChecker> checkers, bool isNot)
        {
            this.Checkers = checkers;
            this.IsNot = isNot;
        }

        public ImmutableArray<IChecker> Checkers { get; }

        public bool IsNot { get; }

        public bool Matches(Puzzle puzzle)
        {
            for (int i = 0; i < this.Checkers.Length; i++)
            {
                if (this.Checkers[i].Matches(puzzle))
                {
                    return !this.IsNot;
                }
            }

            return this.IsNot;
        }

        public override string ToString()
            => (this.IsNot ? "!" : string.Empty)
            + "("
            + string.Join(" || ", from checker in this.Checkers select checker.ToString())
            + ")";

        public IChecker WithColors(PuzzleColor[] colors)
            => new OrChecker(
                ImmutableArray.CreateRange(
                    from checker in this.Checkers
                    select checker.WithColors(colors)),
                this.IsNot);

        public IChecker WithNot()
            => new OrChecker(this.Checkers, !this.IsNot);
    }
}