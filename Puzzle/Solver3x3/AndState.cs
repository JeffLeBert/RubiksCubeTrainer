using System.Collections.Immutable;
using System.Linq;
using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3
{
    public class AndState : IState
    {
        public AndState(ImmutableArray<IState> states)
        {
            this.States = states;
        }

        public ImmutableArray<IState> States { get; }

        public static IState Combine(IState state1, IState state2)
        {
            // If only one exists, just use that.
            if (state1 == null)
            {
                return state2;
            }
            if (state2 == null)
            {
                return state1;
            }

            var andState2 = state2 as AndState;
            if (state1 is AndState andState1)
            {
                if (andState2 != null)
                {
                    return new AndState(andState1.States.AddRange(andState2.States));
                }
                else
                {
                    return new AndState(andState1.States.Add(state2));
                }
            }

            if (andState2 != null)
            {
                return new AndState(ImmutableArray.Create(state1).AddRange(andState2.States));
            }

            return new AndState(ImmutableArray.Create(state1, state2));
        }

        public bool Matches(Puzzle puzzle)
        {
            for (int i = 0; i < this.States.Length; i++)
            {
                if (!this.States[i].Matches(puzzle))
                {
                    return false;
                }
            }

            return true;
        }

        public override string ToString()
            => "("
            + string.Join(" && ", from state in this.States select state.ToString())
            + ")";

        public IState WithColors(PuzzleColor[] colors)
            => new AndState(
                ImmutableArray.CreateRange(
                    from state in this.States
                    select state.WithColors(colors)));

        public IState Negate()
            => new OrState(
                ImmutableArray.CreateRange(
                    from state in this.States
                    select state.Negate()));
    }
}