using System;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace RubiksCubeTrainer.Solver3x3
{
    public static class SolverParser
    {
        public static Solver ParseFromEmbeddedResource(string name)
            => new Solver(AddAllSteps(GetSolverDocument(name)));

        private static ImmutableDictionary<string, Step> AddAllSteps(XDocument document)
            => document.Root.Elements(nameof(Step)).Aggregate(
                ImmutableDictionary.CreateBuilder<string, Step>(StringComparer.OrdinalIgnoreCase),
                (steps, stepElement) =>
                {
                    var step = StepParser.Parse(stepElement, name => steps[name]);
                    steps.Add(step.Name, step);
                    return steps;
                })
                .ToImmutable();

        private static XDocument GetSolverDocument(string name)
            => XDocument.Load(GetSolverStream(name));

        private static Stream GetSolverStream(string name)
            => typeof(Solver).Assembly.GetManifestResourceStream(typeof(Solver), name + ".xml");
    }
}