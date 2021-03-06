﻿using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace RubiksCubeTrainer.Solver3x3
{
    public static class SolverParser
    {
        public static Solver ParseFromEmbeddedResource(string name)
            => ParseFromEmbeddedResource(XDocument.Load(GetSolverStream(name)));

        private static Solver ParseFromEmbeddedResource(XDocument document)
            => Solver.Empty
                .SolverWithPuzzleStates(document)
                .SolverWithAlgorithmTemplates(document)
                .SolverWithAlgorithms(document);

        private static Solver SolverWithPuzzleStates(this Solver initialSolver, XDocument document)
            => (from statesElement in document.Root.Elements("States")
                let baseName = statesElement.Attribute("Name")?.Value
                from stateElement in statesElement.Elements("State")
                select (stateElement, baseName))
            .Aggregate(
                initialSolver,
                (solver, info) =>
                {
                    var (name, state) = StateParser.Parse(info.baseName, null, info.stateElement, solver);
                    return solver.WithState(name, state);
                });

        private static Solver SolverWithAlgorithmTemplates(this Solver initialSolver, XDocument document)
            => (from algorithmsElement in document.Root.Elements("AlgorithmTemplates")
                let baseName = algorithmsElement.Attribute("Name")?.Value
                let initialState = GetChildState(algorithmsElement, nameof(Algorithm.InitialState), initialSolver)
                let finishedState = GetChildState(algorithmsElement, nameof(Algorithm.FinishedState), initialSolver)
                from algorithmElement in algorithmsElement.Elements("Algorithm")
                select (algorithmElement, baseName, initialState, finishedState))
            .Aggregate(
                initialSolver,
                (solver, info) => solver.WithAlgorithmTemplate(AlgorithmParser.Parse(
                    info.baseName,
                    info.initialState,
                    info.finishedState,
                    info.algorithmElement,
                    solver)));

        private static Solver SolverWithAlgorithms(this Solver initialSolver, XDocument document)
            => (from algorithmsElement in document.Root.Elements("Algorithms")
                let baseName = algorithmsElement.Attribute("Name")?.Value
                let initialState = GetChildState(algorithmsElement, nameof(Algorithm.InitialState), initialSolver)
                let finishedState = GetChildState(algorithmsElement, nameof(Algorithm.FinishedState), initialSolver)
                from algorithmElement in algorithmsElement.Elements("Algorithm")
                select (algorithmElement, baseName, initialState, finishedState))
            .Aggregate(
                initialSolver,
                (solver, info) => solver.WithAlgorithm(AlgorithmParser.Parse(
                    info.baseName,
                    info.initialState,
                    info.finishedState,
                    info.algorithmElement,
                    solver)));

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