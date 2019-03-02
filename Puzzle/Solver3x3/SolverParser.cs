using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace RubiksCubeTrainer.Solver3x3
{
    public static class SolverParser
    {
        public static Solver ParseFromEmbeddedResource(string name)
            => AddAllSteps(Solver.Empty, GetSolverDocument(name));

        private static Solver AddAllSteps(Solver initialSolver, XDocument document)
            => document.Root.Elements(nameof(Step)).Aggregate(
                initialSolver,
                (solver, stepElement) => solver.WithStep(StepParser.Parse(solver, stepElement)));

        private static XDocument GetSolverDocument(string name)
            => XDocument.Load(GetSolverStream(name));

        private static Stream GetSolverStream(string name)
            => typeof(Solver).Assembly.GetManifestResourceStream(typeof(Solver), name + ".xml");
    }
}