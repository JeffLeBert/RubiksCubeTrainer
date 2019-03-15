using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace RubiksCubeTrainer.Solver3x3
{
    public static class SolverParser
    {
        public static Solver ParseFromEmbeddedResource(string name)
        {
            var document = GetSolverDocument(name);

            var solver = SolverWithPuzzleStates(Solver.Empty, document);
            solver = SolverWithAlgorithmCollections(solver, document);
            return SolverWithSteps(solver, document);
        }

        private static Solver SolverWithPuzzleStates(Solver initialSolver, XDocument document)
            => (from statesElement in document.Root.Elements("States")
               from stateElement in statesElement.Elements("State")
               select stateElement)
            .Aggregate(
                initialSolver,
                (solver, element) =>
                {
                    var (name, checker) = PuzzleStateParser.Parse(element, solver);
                    return solver.With(name, checker);
                });

        private static Solver SolverWithAlgorithmCollections(Solver initialSolver, XDocument document)
            => (from algorithmCollectionsElement in document.Root.Elements("AlgorithmCollections")
               from algorithmCollectionElement in algorithmCollectionsElement.Elements("AlgorithmCollection")
               select algorithmCollectionElement)
            .Aggregate(
                initialSolver,
                (solver, element) => solver.With(AlgorithmCollectionParser.Parse(element, solver)));

        private static Solver SolverWithSteps(Solver initialSolver, XDocument document)
            => document.Root.Elements(nameof(Step))
            .Aggregate(
                initialSolver,
                (solver, element) => solver.With(StepParser.Parse(element, solver)));

        private static XDocument GetSolverDocument(string name)
            => XDocument.Load(GetSolverStream(name));

        private static Stream GetSolverStream(string name)
            => typeof(Solver).Assembly.GetManifestResourceStream(typeof(Solver), name + ".xml");
    }
}