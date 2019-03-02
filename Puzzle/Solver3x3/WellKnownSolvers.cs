namespace RubiksCubeTrainer.Solver3x3
{
    public static class WellKnownSolvers
    {
        public static Solver Roux { get; } = SolverParser.ParseFromEmbeddedResource(nameof(Roux));
    }
}