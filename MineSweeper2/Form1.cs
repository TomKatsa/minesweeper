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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            /*
            Board hi = new Board(30, 30, 200);
            hi.Text = "Minesweeper";
            hi.Show();
            */
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                int w = int.Parse(widthBox.Text);
                int h = int.Parse(heightBox.Text);
                int m = int.Parse(minesBox.Text);
                Board hi = new Board(w, h, m);
                hi.Show();
            }
            catch
            {
                MessageBox.Show("Please enter valid information!");
            }
        }
    }
}
