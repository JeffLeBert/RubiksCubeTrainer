using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3
{
    public abstract class CheckerBase
    {
        protected CheckerBase()
        {
        }

        public abstract bool Check(Puzzle puzzle);
    }
}