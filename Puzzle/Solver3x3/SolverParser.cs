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
            return SolverWithAlgorithms(solver, document);
        }

        private static Solver SolverWithPuzzleStates(Solver initialSolver, XDocument document)
            => (from statesElement in document.Root.Elements("States")
                let baseName = statesElement.Attribute("Name")?.Value
                from stateElement in statesElement.Elements("State")
                select (stateElement, baseName))
            .Aggregate(
                initialSolver,
                (solver, info) =>
                {
                    var (name, state) = StateParser.Parse(info.baseName, null, info.stateElement, solver);
                    return solver.With(name, state);
                });

        private static Solver SolverWithAlgorithms(Solver initialSolver, XDocument document)
            => (from algorithmsElement in document.Root.Elements("Algorithms")
                let baseName = algorithmsElement.Attribute("Name")?.Value
                let initialState = GetChildState(algorithmsElement, nameof(Algorithm.InitialState), initialSolver)
                let finishedState = GetChildState(algorithmsElement, nameof(Algorithm.FinishedState), initialSolver)
                from algorithmElement in algorithmsElement.Elements("Algorithm")
                select (algorithmElement, baseName, initialState, finishedState))
            .Aggregate(
                initialSolver,
                (solver, info) => solver.With(AlgorithmParser.Parse(
                    info.baseName,
                    info.initialState,
                    info.finishedState,
                    info.algorithmElement,
                    solver)));

        private static XDocument GetSolverDocument(string name)
            => XDocument.Load(GetSolverStream(name));

        private static Stream GetSolverStream(string name)
            => typeof(Solver).Assembly.GetManifestResourceStream(typeof(Solver), name + ".xml");

        private static IState GetChildState(XElement element, string name, Solver solver)
        {
            var stateElement = element.Element(name);
            return stateElement == null
                ? null
                : StateParser.Parse(null, null, stateElement, solver).State;
        }
    }
}