using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ntp_bomb
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Bombs
        /// </summary>
        bool[,] hasBomb = new bool[4, 4];
        bool[,] isManual = new bool[4, 4];

        PictureBox[,] pictureBoxes1 = new PictureBox[4, 4];
        Random prng = new Random();
        private void MainForm_Load(object sender, EventArgs e)
        {
            var pictureBoxes = (from Control ctl in this.Controls where ctl is PictureBox _ select ctl).ToList();

            pictureBoxes1 = new PictureBox[,]
            {
                {pictureBox1, pictureBox2, pictureBox3, pictureBox4 },
                {pictureBox5, pictureBox6, pictureBox7, pictureBox8 },
                {pictureBox9, pictureBox10, pictureBox11, pictureBox12 },
                {pictureBox13, pictureBox14, pictureBox15, pictureBox16 }
            };

            for (int i = 0; i < pictureBoxes.Count(); i++)
            {
                pictureBoxes[i].BackColor = Color.Crimson;
            }
        }

        /// <summary>
        /// Draws the bomb array on the screen.
        /// </summary>
        protected void RenderBombArray()
        {
            var pictureBoxes = (from Control ctl in this.Controls where ctl is PictureBox _ select ctl).ToList();

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (hasBomb[i, j])
                        pictureBoxes1[i,j].BackColor = Color.DarkOrchid;
                    else
                        pictureBoxes1[i,j].BackColor = Color.Crimson;
                }
            }
        }

        private void btnAutoBomb_Click(object sender, EventArgs e)
        {
            int falseCount = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (!hasBomb[i, j]) falseCount++;
                }
            }
            if (falseCount < 3)
            {
                // Select all unused boxes
                var ctl = (from Control c in this.Controls where c is PictureBox _ && c.BackColor == Color.Crimson select (Control)c).SingleOrDefault();
                if(ctl == null)
                    return;

                ctl.BackColor = Color.DarkOrchid;
                return;
            }
            (int x, int y) = (prng.Next(0, 4), prng.Next(0, 4));
            int iter = 0;
            for (int _i = 0; _i < 3; _i++)
            {
                while (hasBomb[x, y])
                {
                    iter++;
                    (x, y) = (prng.Next(0, 4), prng.Next(0, 4));
                }

                hasBomb[x, y] = true;
            }

            RenderBombArray();
        }

        private void btnManualBomb_Click(object sender, EventArgs e)
        {
            (int x, int y) = (Decimal.ToInt32(nuCoordX.Value) - 1, Decimal.ToInt32(nuCoordY.Value) - 1);
            hasBomb[x, y] = true;
            RenderBombArray();
        }

        [Obsolete("This function was a mistake", true)]
        private void nuCoordY_ValueChanged(object sender, EventArgs e)
        {

        }

        private void tbAutoBom_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tbAutoBom.SelectedIndex == 2)
            {
                hasBomb = new bool[4, 4];
                RenderBombArray();
                tbAutoBom.SelectedIndex = 0;
            }
        }
    }
}
