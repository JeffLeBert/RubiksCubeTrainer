using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace RubiksCubeTrainer.Solver3x3
{
    internal static class XDocumentExtensions
    {
        public static IEnumerable<string> XTextValues(this XElement element)
            => from textNode in element.Nodes().OfType<XText>()
               select textNode.Value;
    }
}