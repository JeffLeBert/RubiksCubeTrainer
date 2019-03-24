using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3
{
    public class AlgorithmUsageFinder : SolveWalker
    {
        private readonly Algorithm algorithmToFind;
        private Puzzle foundPuzzle;

        public AlgorithmUsageFinder(Solver solver, Algorithm algorithmToFind, int maximumDepth = 50)
            : base(solver, maximumDepth)
        {
            this.algorithmToFind = algorithmToFind;
        }

        public Puzzle FindUsage(Puzzle puzzle)
        {
            this.Visit(puzzle);

            return this.foundPuzzle;
        }

        protected override bool Visit(Puzzle puzzle, Algorithm algorithm)
        {
            if (this.algorithmToFind.Name == algorithm.Name)
            {
                this.foundPuzzle = puzzle;
                return false;
            }

            return base.Visit(puzzle, algorithm);
        }
    }
}