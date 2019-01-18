namespace RubiksCubeTrainer.WinFormsUI
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtScramble = new System.Windows.Forms.TextBox();
            this.picPuzzle = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblScramble = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picPuzzle)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtScramble
            // 
            this.txtScramble.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtScramble.Location = new System.Drawing.Point(63, 3);
            this.txtScramble.Name = "txtScramble";
            this.txtScramble.Size = new System.Drawing.Size(734, 20);
            this.txtScramble.TabIndex = 0;
            this.txtScramble.Text = "F\' U B\' L\' F U\' D2 F R\' L\' B\' R D\' L2 B D R\' U2 D\' F2";
            // 
            // picPuzzle
            // 
            this.picPuzzle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picPuzzle.ErrorImage = null;
            this.picPuzzle.InitialImage = null;
            this.picPuzzle.Location = new System.Drawing.Point(63, 29);
            this.picPuzzle.Name = "picPuzzle";
            this.picPuzzle.Size = new System.Drawing.Size(734, 418);
            this.picPuzzle.TabIndex = 1;
            this.picPuzzle.TabStop = false;
            this.picPuzzle.Paint += new System.Windows.Forms.PaintEventHandler(this.picPuzzle_Paint);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.lblScramble, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtScramble, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.picPuzzle, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 450);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // lblScramble
            // 
            this.lblScramble.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblScramble.AutoSize = true;
            this.lblScramble.Location = new System.Drawing.Point(3, 0);
            this.lblScramble.Name = "lblScramble";
            this.lblScramble.Size = new System.Drawing.Size(54, 26);
            this.lblScramble.TabIndex = 0;
            this.lblScramble.Text = "Scramble:";
            this.lblScramble.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "MainForm";
            this.Text = "Rubik\'s ";
            ((System.ComponentModel.ISupportInitialize)(this.picPuzzle)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtScramble;
        private System.Windows.Forms.PictureBox picPuzzle;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblScramble;
    }
}