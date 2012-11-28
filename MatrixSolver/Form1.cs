using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MatrixSolver
{
    public partial class Form1 : Form
    {

        private TextBox[,] _MatrixText;
        private Label[] _KoeffLabel;

        public Form1()
        {
            InitializeComponent();

            RenderTextBox();

            /*
             * Erstmal im Konstruktor testen 
             * bevor ne richtige GUI gebaut wird 
             * :D
             */
            /*
            Matrix matrix = new Matrix(3,3,true);

            matrix._matrix[0, 0] = -2; matrix._matrix[0, 1] =  3; matrix._matrix[0, 2] =  2; matrix._matrix[0, 3] = -1;
            matrix._matrix[1, 0] =  4; matrix._matrix[1, 1] = -4; matrix._matrix[1, 2] =  8; matrix._matrix[1, 3] = -4;
            matrix._matrix[2, 0] =  2; matrix._matrix[2, 1] = -3; matrix._matrix[2, 2] = -3; matrix._matrix[2, 3] =  6;

            MessageBox.Show("" + matrix._matrix[0, 0].ToString() + ";" + matrix._matrix[0, 1].ToString() + ";" + matrix._matrix[0, 2].ToString() + "|" + matrix._matrix[0, 3].ToString() + Environment.NewLine
                                + matrix._matrix[1, 0].ToString() + ";" + matrix._matrix[1, 1].ToString() + ";" + matrix._matrix[1, 2].ToString() + "|" + matrix._matrix[1, 3].ToString() + Environment.NewLine
                                + matrix._matrix[2, 0].ToString() + ";" + matrix._matrix[2, 1].ToString() + ";" + matrix._matrix[2, 2].ToString() + "|" + matrix._matrix[2, 3].ToString());

            matrix.ToNZSF();

            MessageBox.Show("" + matrix._matrix[0, 0].ToString() + ";" + matrix._matrix[0, 1].ToString() + ";" + matrix._matrix[0, 2].ToString() + "|" + matrix._matrix[0, 3].ToString() + Environment.NewLine
                                + matrix._matrix[1, 0].ToString() + ";" + matrix._matrix[1, 1].ToString() + ";" + matrix._matrix[1, 2].ToString() + "|" + matrix._matrix[1, 3].ToString() + Environment.NewLine
                                + matrix._matrix[2, 0].ToString() + ";" + matrix._matrix[2, 1].ToString() + ";" + matrix._matrix[2, 2].ToString() + "|" + matrix._matrix[2, 3].ToString());
             */
        }
        
        private void RenderTextBox()
        {
            int m = Convert.ToInt32(numericUpDown1.Value);
            int n = Convert.ToInt32(numericUpDown2.Value);

            bool isKoeff = checkBox1.Checked;
            if (isKoeff) n++;

            if(_MatrixText != null)foreach (var box in _MatrixText)
            {
                box.Parent = null;
            }
            if (_KoeffLabel != null)foreach (var lab in _KoeffLabel)
            {
                lab.Parent = null;
            }

            _MatrixText = new TextBox[m,n];

            _KoeffLabel = isKoeff ? new Label[m] : null;

            int wHeader = 50 + 45 + 5 + 14 + 5 + 45 + 5 + 35 + 20 + 75 + 50;
            int wBox = 2 * 50 + n * 45 + (n - 1) * 5;

            if(isKoeff) wBox += 5 + 14 + 5;

            bool hBigger = wHeader > wBox;

            int wReal = Math.Max(wHeader, wBox) + 20;

            int height = 39 + 17 + 30 + m*20 + m*5 + 30 + 20;

            for (int x = 0; x < n; x++)
            {
                for (int y = 0; y < m; y++)
                {
                    TextBox txtBox =_MatrixText[y, x] = new TextBox();
                    //TextBox txtBox = 

                    txtBox.Parent = this;

                    txtBox.Size = new Size(45, 20);
                    txtBox.Location = new Point(50 + x * 45 + /*Math.Max(0, x - 1)*/x * 5, 39 + 17 + 30 + y * 20 + /*Math.Max(0, y - 1)*/y * 5);

                    txtBox.Text = "0.0";

                    //_MatrixText[y, x] = txtBox;
                }
            }

            if (isKoeff)
            {
                for (int y = 0; y < m; y++)
                {
                    TextBox txtBox = _MatrixText[y, n-1];

                    Label la = (_KoeffLabel[y]) = new Label();
                    la.Parent = this;
                    la.Text = "|";
                    la.Size = new Size(17,20);
                    la.Location = new Point(txtBox.Location.X + 5, txtBox.Location.Y);

                    txtBox.Location = new Point(txtBox.Location.X + 27, txtBox.Location.Y);
                }
            }

            this.Size = new Size(wReal, height);

        }

        private void buttonGO_Click(object sender, EventArgs e)
        {
            RenderTextBox();
        }

    }
}
