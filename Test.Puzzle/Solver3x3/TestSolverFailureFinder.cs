//using System.Diagnostics;
//using System.Linq;
//using RubiksCubeTrainer.Puzzle3x3;
//using RubiksCubeTrainer.Solver3x3.Roux;
//using Xunit;
//using Xunit.Abstractions;

//namespace RubiksCubeTrainer.Solver3x3
//{
//    public class FindSolverFailure
//    {
//        private readonly ITestOutputHelper output;

//        public FindSolverFailure(ITestOutputHelper output)
//        {
//            this.output = output;
//        }

//        [Fact]
//        public void Find_first_failure()
//        {
//            var puzzle = Rotator.ApplyMoves(Solver.Solved, Scrambler.EnumerateMoves());
//            var failInfo = SolverFailureFinder.FindFailure(new Solver(), puzzle);
//        }

//        //[Fact]
//        //public void TimeSome()
//        //{
//        //    const int Iterations = 474552; // 4 random moves.
//        //    var stopwatch = Stopwatch.StartNew();

//        //    var puzzle = Solver.Solved;
//        //    foreach (var move in Scrambler.EnumerateMoves().Take(Iterations))
//        //    {
//        //        puzzle = Rotator.ApplyMove(puzzle, move);
//        //    }

//        //    stopwatch.Stop();
//        //    output.WriteLine($"{Iterations} took {stopwatch.ElapsedMilliseconds} milliseconds.");
//        //}
//    }
//}