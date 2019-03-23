using System.Collections.Immutable;
using System.Linq;
using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3
{
    public class OrState : IState
    {
        public OrState(ImmutableArray<IState> states)
        {
            this.States = states;
        }

        public ImmutableArray<IState> States { get; }

        public bool Matches(Puzzle puzzle)
        {
            for (int i = 0; i < this.States.Length; i++)
            {
                if (this.States[i].Matches(puzzle))
                {
                    return true;
                }
            }

            return false;
        }

        public override string ToString()
            => "("
            + string.Join(" || ", from state in this.States select state.ToString())
            + ")";

        public IState WithColors(PuzzleColor[] colors)
            => new OrState(
                ImmutableArray.CreateRange(
                    from state in this.States
                    select state.WithColors(colors)));

        public IState Negate()
            => new AndState(
                ImmutableArray.CreateRange(
                    from state in this.States
                    select state.Negate()));
    }
}