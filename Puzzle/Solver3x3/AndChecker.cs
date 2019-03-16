using System.Collections.Immutable;
using System.Linq;
using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3
{
    public class AndChecker : IChecker
    {
        public AndChecker(ImmutableArray<IChecker> checkers)
        {
            this.Checkers = checkers;
        }

        public ImmutableArray<IChecker> Checkers { get; }

        public static IChecker Combine(IChecker checker1, IChecker checker2)
        {
            // If only one exists, just use that.
            if (checker1 == null)
            {
                return checker2;
            }
            if (checker2 == null)
            {
                return checker1;
            }

            var andChecker1 = checker1 as AndChecker;
            var andChecker2 = checker2 as AndChecker;

            if (andChecker1 != null)
            {
                if (andChecker2 != null)
                {
                    return new AndChecker(andChecker1.Checkers.AddRange(andChecker2.Checkers));
                }
                else
                {
                    return new AndChecker(andChecker1.Checkers.Add(checker2));
                }
            }

            if (andChecker2 != null)
            {
                return new AndChecker(ImmutableArray.Create(checker1).AddRange(andChecker2.Checkers));
            }

            return new AndChecker(ImmutableArray.Create(checker1, checker2));
        }

        public bool Matches(Puzzle puzzle)
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

        public override string ToString()
            => "("
            + string.Join(" && ", from checker in this.Checkers select checker.ToString())
            + ")";

        public IChecker WithColors(PuzzleColor[] colors)
            => new AndChecker(
                ImmutableArray.CreateRange(
                    from checker in this.Checkers
                    select checker.WithColors(colors)));

        public IChecker Negate()
            => new OrChecker(
                ImmutableArray.CreateRange(
                    from checker in this.Checkers
                    select checker.Negate()));
    }
}