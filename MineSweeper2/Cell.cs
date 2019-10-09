using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;


namespace MineSweeper2
{
    class Cell : Button
    {

        // Variables
        int x;
        int y;
        bool mine;
        bool flag;
        bool revealed = false;

        // Cell Constructor and X, Y coordinates
        public Cell(int x, int y)
        {
            this.x = x;
            this.y = y;
            Width = 30;
            Height = 30;
            Font = new Font(Font.Name, Font.Size, FontStyle.Bold);
        }

        public bool IsRevealed
        {
            get
            {
                return revealed;
            }
            set
            {
                revealed = value;
            }
        }

        public void SetNum(int mines)
        {
            if (mine == true)
            {
                return;
            }
            FlatStyle = FlatStyle.Flat;
            Text = mines.ToString();
            switch(mines)
            {
                case 0:
                    Text = "";
                    Enabled = false;
                    break;
                case 1:
                    ForeColor = Color.Blue;
                    break;
                case 2:
                    ForeColor = Color.Green;
                    break;
                case 3:
                    ForeColor = Color.Red;
                    break;
                case 4:
                    ForeColor = Color.Yellow;
                    break;

            }
            BackColor = Color.LightGray;
        }

        public int GetX()
        { 
            return x;
        }

        public int GetY()
        {
            return y;
        }

        public bool IsMine
        {
            get
            {
                return mine;
            }
            set
            {
                mine = value;
            }
        }

        public bool GoodFlag()
        {
            if (mine==flag)
            {
                return true;
            }
            return false;
        }

        public bool IsFlagged
        {

            get
            {
                return flag;
            }
            set
            {
                if(IsRevealed)
                {
                    return;
                }
                if (value)
                {
                    Text = "⚑";
                    ForeColor = System.Drawing.Color.Red;
                }
                else {
                    Text = "";
                }
                flag = value;
            }
        }
    }
}
