namespace Merge
{
    partial class Form1
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
            this.lblScoreLabel = new System.Windows.Forms.Label();
            this.lblBestLabel = new System.Windows.Forms.Label();
            this.lblScore = new System.Windows.Forms.Label();
            this.lblBest = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblScoreLabel
            // 
            this.lblScoreLabel.Location = new System.Drawing.Point(97, 9);
            this.lblScoreLabel.Name = "lblScoreLabel";
            this.lblScoreLabel.Size = new System.Drawing.Size(100, 23);
            this.lblScoreLabel.TabIndex = 1;
            this.lblScoreLabel.Text = "Score:";
            this.lblScoreLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblBestLabel
            // 
            this.lblBestLabel.Location = new System.Drawing.Point(203, 9);
            this.lblBestLabel.Name = "lblBestLabel";
            this.lblBestLabel.Size = new System.Drawing.Size(100, 23);
            this.lblBestLabel.TabIndex = 2;
            this.lblBestLabel.Text = "Best:";
            this.lblBestLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblScore
            // 
            this.lblScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblScore.Location = new System.Drawing.Point(97, 32);
            this.lblScore.Name = "lblScore";
            this.lblScore.Size = new System.Drawing.Size(100, 23);
            this.lblScore.TabIndex = 3;
            this.lblScore.Text = "00000";
            this.lblScore.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblBest
            // 
            this.lblBest.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBest.Location = new System.Drawing.Point(203, 32);
            this.lblBest.Name = "lblBest";
            this.lblBest.Size = new System.Drawing.Size(100, 23);
            this.lblBest.TabIndex = 4;
            this.lblBest.Text = "00000";
            this.lblBest.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(355, 298);
            this.Controls.Add(this.lblBest);
            this.Controls.Add(this.lblScore);
            this.Controls.Add(this.lblBestLabel);
            this.Controls.Add(this.lblScoreLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = "Merge";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblScoreLabel;
        private System.Windows.Forms.Label lblBestLabel;
        private System.Windows.Forms.Label lblScore;
        private System.Windows.Forms.Label lblBest;


    }
}

