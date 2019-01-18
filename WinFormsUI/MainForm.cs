using System.Windows.Forms;
using RubiksCubeTrainer.Puzzle3x3;

namespace RubiksCubeTrainer.WinFormsUI
{
    public partial class MainForm : Form
    {
        private Puzzle puzzle = Puzzle.Solved.ClockwiseRotateLayer(LayerName.Up);

        public MainForm()
        {
            this.InitializeComponent();
        }

        private void picPuzzle_Paint(object sender, PaintEventArgs e)
        {
            this.picPuzzle.Invalidate();

            PuzzleRenderer3x3.Draw(e.Graphics, this.puzzle, this.picPuzzle.Size);
        }
    }
}