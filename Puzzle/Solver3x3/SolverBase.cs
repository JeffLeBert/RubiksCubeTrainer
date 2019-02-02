using System.Collections.Generic;

namespace RubiksCubeTrainer.Solver3x3
{
    public abstract class SolverBase
    {
        protected SolverBase(SolverBase parentSolver, StepBase step)
        {
            this.ParentSolver = parentSolver;
            this.CurrentStep = step;
        }

        public IEnumerable<SolverBase> AncestorSolversAndSelf
        {
            get
            {
                var solver = this;
                do
                {
                    yield return solver;
                    solver = solver.ParentSolver;
                }
                while (solver != null);
            }
        }

        public StepBase CurrentStep { get; }

        public SolverBase ParentSolver { get; }

        public abstract SolverBase NextSolver(StepInformation stepInformation);
    }
}