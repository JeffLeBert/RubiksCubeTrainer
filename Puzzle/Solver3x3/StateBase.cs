using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.Solver3x3
{
    public abstract class StateBase : IState
    {
        protected StateBase(Location location, bool isNot, bool isRotated)
        {
            this.Location = location;
            this.IsNot = isNot;
            this.IsRotated = isRotated;
        }

        public bool IsNot { get; }

        public bool IsRotated { get; }

        public Location Location { get; }

        public static PuzzleColor UpdateColorIfTemplated(PuzzleColor currentColor, PuzzleColor[] templateColors)
            => currentColor > PuzzleColor.TemplateColors
            ? templateColors[currentColor - PuzzleColor.TemplateColors - 1]
            : currentColor;

        public abstract bool Matches(Puzzle puzzle);

        public abstract IState WithColors(PuzzleColor[] colors);

        public abstract IState Negate();

        protected string FormattedLocation
            => (this.IsNot ? "!" : "")
            + this.Location.ToString()
            + (this.IsRotated ? "*" : "");

        internal static (bool IsRotated, bool IsNot, Location Location) GetLocationInformation(string locationPart)
        {
            var isNot = locationPart.StartsWith("!");
            if (isNot)
            {
                locationPart = locationPart.Substring(1);
            }

            var isRotated = locationPart.EndsWith("*");
            if (isRotated)
            {
                locationPart = locationPart.Substring(0, locationPart.Length - 1);
            }

            return (isRotated, isNot, LocationParser.Parse(locationPart));
        }
    }
}