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
            => new Algorithm(
                BuildName(baseName, element.Attribute(nameof(Algorithm.Name))?.Value),
                element.Element(nameof(Algorithm.Description)).Value,
                GetState(initialState, element.Element(nameof(Algorithm.InitialState)), solver),
                GetState(finalState, element.Element(nameof(Algorithm.FinishedState)), solver),
                ParseMoves(element.Element(nameof(Algorithm.Moves)).Value));

        private static string BuildName(string baseName, string name)
            => baseName == null
            ? name
            : baseName + "." + name;

        private static ImmutableArray<ImmutableArray<NotationMoveType>> ParseMoves(string value)
            => ImmutableArray.CreateRange(
                from algorithmText in value.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                select ImmutableArray.CreateRange(NotationParser.EnumerateMoves(algorithmText)));

        private static IState GetState(IState parentState, XElement stateElement, Solver solver)
            => stateElement == null
                ? parentState
                : StateParser.Parse(null, parentState, stateElement, solver).State;
    }
}