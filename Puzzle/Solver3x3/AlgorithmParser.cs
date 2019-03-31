using System;
using System.Collections.Immutable;
using System.Linq;
using System.Xml.Linq;
using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3
{
    public static class AlgorithmParser
    {
        public static Algorithm Parse(
            string baseName,
            IState initialState,
            IState finalState,
            XElement element,
            Solver solver)
        {
            var (algorithm, colors) = GetBaseAlgorithm(element, solver);

            algorithm = algorithm
                .WithName(BuildName(baseName, element.Attribute(nameof(Algorithm.Name))?.Value))
                .WithDescription(element.Element(nameof(Algorithm.Description))?.Value);

            var movesText = element.Element(nameof(Algorithm.Moves))?.Value;
            if (!string.IsNullOrWhiteSpace(movesText))
            {
                algorithm = algorithm.WithMoves(ParseMoves(movesText));
            }

            var myInitialState = GetState(initialState, element.Element(nameof(Algorithm.InitialState)), solver);
            var myFinishedState = GetState(finalState, element.Element(nameof(Algorithm.FinishedState)), solver);

            return algorithm
                .WithInitialState(AndState.Combine(algorithm.InitialState, myInitialState))
                .WithFinishedState(AndState.Combine(algorithm.FinishedState, myFinishedState))
                .WithColors(colors);
        }

        private static string BuildName(string baseName, string name)
            => baseName == null
            ? name
            : baseName + "." + name;

        private static (Algorithm Algorithm, PuzzleColor[] Colors) GetBaseAlgorithm(XElement element, Solver solver)
        {
            var attribute = element.Attribute("Base");
            if (attribute == null)
            {
                return (Algorithm.Empty, null);
            }

            var (name, colors) = ExpressionParser.ParseInner(attribute.Value);
            Algorithm algorithm;
            if (!solver.AlgorithmTemplates.TryGetValue(name, out algorithm))
            {
                if (!solver.Algorithms.TryGetValue(name, out algorithm))
                {
                    throw new InvalidOperationException($"The '{name}' algorithm or algorithm template could not be found.");
                }
            }

            return (algorithm, colors);
        }

        private static ImmutableArray<ImmutableArray<NotationMoveType>> ParseMoves(string value)
            => string.IsNullOrWhiteSpace(value)
            ? ImmutableArray<ImmutableArray<NotationMoveType>>.Empty
            : ImmutableArray.CreateRange(
                from algorithmText in value.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                select ImmutableArray.CreateRange(NotationParser.EnumerateMoves(algorithmText)));

        private static IState GetState(IState parentState, XElement stateElement, Solver solver)
            => stateElement == null
                ? parentState
                : StateParser.Parse(null, parentState, stateElement, solver).State;
    }
}