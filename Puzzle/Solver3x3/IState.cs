using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3
{
    public interface IState
    {
        bool Matches(Puzzle puzzle);

        string ToString();

        IState WithColors(PuzzleColor[] colors);

        IState Negate();
    }
}