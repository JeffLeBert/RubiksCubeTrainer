using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3
{
    public class Solver
    {
        private readonly ImmutableArray<Step> allSteps;

        public Solver(string name)
            : this(GetSolverDocument(name))
        {
        }

        private Solver(XDocument solverDocument)
        {
            this.allSteps = ImmutableArray.CreateRange(EnumerateSteps(solverDocument.Root));
        }

        public static Solver Roux { get; } = new Solver("Roux");

        public static Puzzle Solved { get; } = BuildSolvedPuzzle();

        public Step GetStep(string name)
            => (from step in this.allSteps
                where step.Name == name
                select step)
                .FirstOrDefault();

        public IEnumerable<IStep> NextSteps(Puzzle puzzle)
            => from step in this.allSteps
               where step.InitialState.Matches(puzzle)
               select step;

        private static Puzzle BuildSolvedPuzzle()
            => new Puzzle(
                new Face(FaceName.Up, PuzzleColor.Yellow),
                new Face(FaceName.Front, PuzzleColor.Red),
                new Face(FaceName.Down, PuzzleColor.White),
                new Face(FaceName.Back, PuzzleColor.Orange),
                new Face(FaceName.Left, PuzzleColor.Blue),
                new Face(FaceName.Right, PuzzleColor.Green));

        private static IEnumerable<Step> EnumerateSteps(XElement root)
            => from stepElement in root.Elements(nameof(Step))
               select new Step(stepElement);

        private static XDocument GetSolverDocument(string name)
            => XDocument.Load(GetSolverStream(name));

        private static Stream GetSolverStream(string name)
            => typeof(Solver).Assembly.GetManifestResourceStream(typeof(Solver), name + ".xml");
    }
}