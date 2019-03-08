using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3
{
    public interface IChecker
    {
        bool Matches(Puzzle puzzle);
    }
}