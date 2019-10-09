using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MineSweeper2
{
    public partial class Board : Form
    {

        // Variables
        private Cell[,] Grid;
        int w;
        int h;
        int mines;
        int victory = 0;
        int wrong = 0;

        public Board()
        {
            InitializeComponent();
        }

        // Main Constructor
        public Board(int w, int h, int mines)
        {
            this.w = w;
            this.h = h;
            this.mines = mines;
            // Board size
            InitializeComponent();
            Height = 30 * h;
            Width = 30 * w;
            Rectangle rc = ClientRectangle;
            // Filling the grid
            this.Grid = new Cell[w, h];
            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    Grid[i, j] = new Cell(i, j)
                    {
                        Left = i * (rc.Width / w),
                        Top = j * (rc.Height / h)
                    };
                    this.Controls.Add(Grid[i, j]);
                    // Handling right / left click
                    Grid[i, j].MouseDown += new MouseEventHandler(Cell_Click);
                    void Cell_Click(object sender, MouseEventArgs e)
                    {
                        Cell cell = sender as Cell;
                        if (e.Button == MouseButtons.Left)
                        {
                             if (cell.IsMine == true)
                            {
                                RevealAll();
                                MessageBox.Show("Mine!");
                                EndGame();
                            }
                            Reveal(cell);
                        }
                        else if (e.Button == MouseButtons.Right)
                        {
                            if (cell.IsFlagged){
                                if (cell.IsFlagged==cell.IsMine)
                                {
                                    victory--;
                                }
                                else
                                {
                                    wrong--;
                                }
                                cell.IsFlagged = false;
                                return;
                            }
                            cell.IsFlagged = true;
                            if (cell.GoodFlag())
                            {
                                victory++;
                            }
                            else
                            {
                                wrong++;
                            }
                            if (victory==mines && wrong==0)
                            {
                                WinGame();
                            }
                        }
                    }
                }
            }

            // Adding mines
            Random rnd = new Random();
            for (int i=0; i<mines; i++)
            {
                int x = rnd.Next(w);
                int y = rnd.Next(h);
                while (Grid[x,y].IsMine)
                {
                    x = rnd.Next(w);
                    y = rnd.Next(y);
                }
                Grid[x, y].IsMine = true;
            }

        }


        private void Reveal(Cell c)
        {
            if (c.IsRevealed)
            {
                return;
            }
            int m = 0;
            int x = c.GetX();
            int y = c.GetY();
            // Finding all nearby mines
            for (int i=-1; i<=1; i++)
            {
                for (int j=-1; j<=1; j++)
                {
                    int newx = x + i;
                    int newy = y + j;
                    if (newx>=0 && newx<w && newy>=0 && newy<h)
                    {
                        if(Grid[newx,newy].IsMine)
                        {
                            m++;
                        }
                    }
                }
            }

            c.IsRevealed = true;

            // Revealing nearby cells, if the cell is empty
            c.SetNum(m);
            if (m == 0)
            {
                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                    //    if (i==j || i == -1*j)
                      //  {
                      //      continue;
                     //   }
                        int newx = x + i;
                        int newy = y + j;
                        if (newx >= 0 && newx < w && newy >= 0 && newy < h)
                        {
                            Reveal(Grid[newx, newy]);
                        }
                    }
                }
            }

        }

        private void Board_Load(object sender, EventArgs e)
        {

        }

        // For end game
        private void RevealAll()
        {
            for (int i=0; i<w; i++)
            {
                for (int j=0; j<h; j++)
                {
                    Reveal(Grid[i, j]);
                    if (Grid[i, j].IsMine)
                    {
                        Grid[i, j].Text = "💣";
                    }
                }
            }
        }
        private void EndGame()
        {
            Board nboard = new Board(w, h, mines);
            nboard.Show();
            //Close();

        }

        private void WinGame()
        {
            MessageBox.Show("Congratulations, you won!");
        }
    }
}
