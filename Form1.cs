using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using X_O_Game.Properties;

namespace X_O_Game
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            GameStatus.Winner = enWinner.eInProgress;
            GameStatus.PlayCounter = 0;
        }

        enum enPlayerTurn
        {
            ePlayer1Turn = 1,
            ePlayer2Turn = 2
        }

        enum enWinner
        {
            ePlayer1,
            ePlayer2,
            eDraw,
            eInProgress
        }

        struct stGameStatus
        {
            public enWinner Winner;
            public short PlayCounter;
        }

        enPlayerTurn PlayerTurn = enPlayerTurn.ePlayer1Turn;
        stGameStatus GameStatus;
        


        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            // GameStatus.PlayCounter = 0;
            Color white = Color.FromArgb(255, 255, 255, 255);
            Pen whitePen = new Pen(white);
            whitePen.Width = 15;
            whitePen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            whitePen.EndCap = System.Drawing.Drawing2D.LineCap.Round;

            e.Graphics.DrawLine(whitePen, 400, 300, 1050, 300);
            e.Graphics.DrawLine(whitePen, 400, 460, 1050, 460);

            e.Graphics.DrawLine(whitePen, 610, 140, 610, 620);
            e.Graphics.DrawLine(whitePen, 840, 140, 840, 620);

        }

        private void _EndGame()
        {

            lblTurn.Text = "No One";

            switch (GameStatus.Winner)
            {

                case enWinner.ePlayer1:

                    lblWinner.Text = "Player1";
                    MessageBox.Show("Player 1  Wins", "The End", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;

                case enWinner.ePlayer2:

                    lblWinner.Text = "Player2";
                    MessageBox.Show("Player 2  Wins", "The End", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;

                default:

                    lblWinner.Text = "Draw";
                    MessageBox.Show("Draw", "The End", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;

            }

        }

        private bool _CheckPlayerValues(PictureBox pb1, PictureBox pb2, PictureBox pb3)
        {

            if (pb1.Tag.ToString() != "?" && pb1.Tag.ToString() == pb2.Tag.ToString() && pb1.Tag.ToString() == pb3.Tag.ToString())
            {

                pb1.BackColor = Color.LightGreen;
                pb2.BackColor = Color.LightGreen;
                pb3.BackColor = Color.LightGreen;

                if (pb1.Tag.ToString() == "X")
                {
                    GameStatus.Winner = enWinner.ePlayer1;
                    _EndGame();
                    return true;
                }
                else
                {
                    GameStatus.Winner = enWinner.ePlayer2;
                    _EndGame();
                    return true;
                }

            }

            return false;

        }

        private void _CheckWinner()
        {

            if (_CheckPlayerValues(pb1, pb2, pb3))
                return;

            if (_CheckPlayerValues(pb4, pb5, pb6))
                return;

            if (_CheckPlayerValues(pb7, pb8, pb9))
                return;

            if (_CheckPlayerValues(pb1, pb4, pb7))
                return;

            if (_CheckPlayerValues(pb2, pb5, pb8))
                return;

            if (_CheckPlayerValues(pb3, pb6, pb9))
                return;

            if (_CheckPlayerValues(pb1, pb5, pb9))
                return;

            if (_CheckPlayerValues(pb3, pb5, pb7))
                return;

        }

        private void _ChangeImages(PictureBox pb)
        {

            if (pb.Tag.ToString() == "?")
            {

                switch (PlayerTurn)
                {
                    case enPlayerTurn.ePlayer1Turn:
                        pb.Image = Resources.X;
                        pb.Tag = "X";
                        PlayerTurn = enPlayerTurn.ePlayer2Turn;
                        lblTurn.Text = "Player 2";
                        _CheckWinner();
                        break;
                    case enPlayerTurn.ePlayer2Turn:
                        pb.Image = Resources.O;
                        pb.Tag = "O";
                        PlayerTurn = enPlayerTurn.ePlayer1Turn;
                        lblTurn.Text = "Player 1";
                        _CheckWinner();
                        break;

                }

                pb.Cursor = default;
                GameStatus.PlayCounter++;

                if (GameStatus.PlayCounter == 9 && GameStatus.Winner == enWinner.eInProgress)
                {
                    GameStatus.Winner = enWinner.eDraw;
                    _EndGame();
                }

            }
            else
            {
                MessageBox.Show("Not Allowed Playing Here", "Worng", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void _ResetImageInfo(PictureBox pb)
        {
            pb.Image = Resources.question_mark_96;
            pb.Tag = "?";
            pb.BackColor = Color.Black;
            pb.Cursor = Cursors.Hand;

        }
        private void _RestartGame()
        {

            _ResetImageInfo(pb1);
            _ResetImageInfo(pb2);
            _ResetImageInfo(pb3);
            _ResetImageInfo(pb4);
            _ResetImageInfo(pb5);
            _ResetImageInfo(pb6);
            _ResetImageInfo(pb7);
            _ResetImageInfo(pb8);
            _ResetImageInfo(pb9);

            PlayerTurn = enPlayerTurn.ePlayer1Turn;
            lblTurn.Text = "Player 1";
            GameStatus.PlayCounter = 0;
            GameStatus.Winner = enWinner.eInProgress;
            lblWinner.Text = "In Progress";

        }

        private void btnResetGame_Click(object sender, EventArgs e)
        {
            _RestartGame();
        }

        // Put All 9 Events Clicks On Images In This Single Event .
        private void PictureBox_Click(object sender, EventArgs e)
        {
            _ChangeImages((PictureBox)sender);
        }

    }
}
