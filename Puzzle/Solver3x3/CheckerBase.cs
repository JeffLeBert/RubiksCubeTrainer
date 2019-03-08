using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3
{
    public abstract class CheckerBase : IChecker
    {
        public abstract bool Matches(Puzzle puzzle);

        internal static (bool IsAll, bool IsNot, Location Location) GetLocationInformation(string locationPart)
        {
            var isNot = locationPart.StartsWith("!");
            if (isNot)
            {
                locationPart = locationPart.Substring(1);
            }

            var isAll = locationPart.EndsWith("*");
            if (isAll)
            {
                locationPart = locationPart.Substring(0, locationPart.Length - 1);
            }

            return (isAll, isNot, LocationParser.Parse(locationPart));
        }
    }
}