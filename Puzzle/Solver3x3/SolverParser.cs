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

            var solver = SolverWithAlgorithmCollectionTemplate(Solver.Empty, document);
            return SolverWithSteps(solver, document);
        }

        private static Solver SolverWithAlgorithmCollectionTemplate(Solver initialSolver, XDocument document)
            => document.Root.Elements(nameof(AlgorithmCollection))
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