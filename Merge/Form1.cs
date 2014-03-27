using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Merge
{
    public partial class Form1 : Form
    {
        private Button[,] _gridButtons;
        private int[,] _mergeGrid;
        private int _score;
        private MergeSettings _settings;

        private int widthPadding = 3;
        private int heightPadding = 3;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _settings = new MergeSettings();
            PlaceControls();
            NewGame();
        }

        private void NewGame()
        {
            if (_score > _settings.HighScore)
            {
                _settings.HighScore = _score;
                _settings.Save();
            }
            _mergeGrid = Merge.CreateGrid(_settings.GridWidth);
            _score = 0;
            AddIfPossible();
        }

        private void PlaceControls()
        {
            var leftPadding = 13;
            var topPadding = lblBest.Bottom + heightPadding + heightPadding;
            _gridButtons = new Button[_settings.GridWidth, _settings.GridWidth];
            for (int y = 0; y < _settings.GridWidth; y++)
            {
                for (int x = 0; x < _settings.GridWidth; x++)
                {
                    _gridButtons[x, y] = new Button
                    {
                        FlatStyle = FlatStyle.Flat,
                        TabStop = false,
                        Enabled = false,
                        Width = _settings.TileWidth,
                        Height = _settings.TileWidth,
                        Left = leftPadding + x * (_settings.TileWidth + widthPadding),
                        Top = topPadding + y * (_settings.TileWidth + heightPadding),
                        Font = new Font(FontFamily.GenericSansSerif, 18)
                    };

                    this.Controls.Add(_gridButtons[x, y]);
                }
            }

            lblBest.Left = _gridButtons[_settings.GridWidth - 1, 0].Right - lblBest.Width;
            lblBestLabel.Left = lblBest.Left;
            lblBest.BackColor = MergeTileDisplayDetail.Style2048[0].Background;
            lblBestLabel.BackColor = lblBest.BackColor;

            lblScore.Left = (lblBest.Left) - lblScore.Width;
            lblScoreLabel.Left = lblScore.Left;
            lblScore.BackColor = MergeTileDisplayDetail.Style2048[0].Background;
            lblScoreLabel.BackColor = lblScore.BackColor;

            this.ClientSize = new Size(
                lblBest.Right + leftPadding,
                _gridButtons[0, _settings.GridWidth - 1].Bottom + leftPadding);

            this.BackColor = Color.FromArgb(
                (MergeTileDisplayDetail.Style2048[0].Background.R + 255) / 2,
                (MergeTileDisplayDetail.Style2048[0].Background.G + 255) / 2,
                (MergeTileDisplayDetail.Style2048[0].Background.B + 255) / 2);
        }

        private void UpdateControls()
        {
            for (int y = 0; y < _settings.GridWidth; y++)
            {
                for (int x = 0; x < _settings.GridWidth; x++)
                {
                    _gridButtons[x, y].Text = MergeTileDisplayDetail.Style2048[_mergeGrid[x, y]].Display;
                    _gridButtons[x, y].BackColor = MergeTileDisplayDetail.Style2048[_mergeGrid[x, y]].Background;
                    _gridButtons[x, y].ForeColor = MergeTileDisplayDetail.Style2048[_mergeGrid[x, y]].Foreground;
                }
            }

            lblScore.Text = _score.ToString();
            lblBest.Text = (_score > _settings.HighScore)? _score.ToString(): _settings.HighScore.ToString();
        }

        private void AddIfPossible()
        {
            if (Merge.AvailableSpaces(_mergeGrid).Any())
            {
                Merge.AddTile(ref _mergeGrid);
                UpdateControls();
            }
            else
            {
                MessageBox.Show("No spaces left");
                if (!Merge.AvailableMoves(_mergeGrid))
                {
                    MessageBox.Show("Game Over.");
                    MessageBox.Show("Press OK to restart.");
                    NewGame();
                }
            }
        }

        protected override bool IsInputKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Right:
                case Keys.Left:
                case Keys.Up:
                case Keys.Down:
                    return true;
            }
            return base.IsInputKey(keyData);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);

            var moveResult = -1;

            if (e.KeyCode == Keys.Up)
                moveResult = Merge.Up(ref _mergeGrid);

            if (e.KeyCode == Keys.Down)
                moveResult = Merge.Down(ref _mergeGrid);

            if (e.KeyCode == Keys.Left)
                moveResult = Merge.Left(ref _mergeGrid);

            if (e.KeyCode == Keys.Right)
                moveResult = Merge.Right(ref _mergeGrid);

            if (moveResult != -1)
            {
                _score += moveResult;
                AddIfPossible();
                CheckForGameOver();
            }
        }

        private void CheckForGameOver()
        {
            if (!Merge.AvailableSpaces(_mergeGrid).Any() && !Merge.AvailableMoves(_mergeGrid))
            {
                MessageBox.Show("Game Over.");
                MessageBox.Show("Press OK to restart.");
                NewGame();
            }
        }

        private void DrawRoundedRectange(Graphics gfx, Rectangle bounds, int cornerRadius, Pen drawPen, Color fillColor)
        {
            int strokeOffset = Convert.ToInt32(Math.Ceiling(drawPen.Width));
            bounds = Rectangle.Inflate(bounds, -strokeOffset, -strokeOffset);

            drawPen.EndCap = drawPen.StartCap = System.Drawing.Drawing2D.LineCap.Round;

            GraphicsPath gfxPath = new GraphicsPath();
            gfxPath.AddArc(bounds.X, bounds.Y, cornerRadius, cornerRadius, 180, 90);
            gfxPath.AddArc(bounds.X + bounds.Width - cornerRadius, bounds.Y, cornerRadius, cornerRadius, 270, 90);
            gfxPath.AddArc(bounds.X + bounds.Width - cornerRadius, bounds.Y + bounds.Height - cornerRadius, cornerRadius, cornerRadius, 0, 90);
            gfxPath.AddArc(bounds.X, bounds.Y + bounds.Height - cornerRadius, cornerRadius, cornerRadius, 90, 90);
            gfxPath.CloseAllFigures();

            gfx.FillPath(new SolidBrush(fillColor), gfxPath);
            gfx.DrawPath(drawPen, gfxPath);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            var rectLoc = new Rectangle(
                lblScore.Left - widthPadding,
                lblScoreLabel.Top - heightPadding,
                (widthPadding + lblScore.Width + lblBest.Width + widthPadding),
                (heightPadding + lblScore.Height + lblScoreLabel.Height + heightPadding));

            DrawRoundedRectange(e.Graphics, rectLoc, 10, new Pen(MergeTileDisplayDetail.Style2048[0].Background), MergeTileDisplayDetail.Style2048[0].Background);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _settings.HighScore = (_score > _settings.HighScore)? _score: _settings.HighScore;
            _settings.Save();
        }
    }
}
