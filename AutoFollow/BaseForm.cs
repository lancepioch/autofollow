using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace AutoFollow
{
    public partial class BaseForm : Form
    {
        ResizeForm searchwindow;

        public int searchcolor = 0;
        public int shadevariation = 0;
        public bool mouseclick = false;
        public string clicktype = "left";
        public int nclicks = 1;
        public int nstep = 1;
        public int delaystart = 0;
        public int randomx = 0;
        public int randomy = 0;

        public bool pickingcolor = false;

        public BaseForm()
        {
            InitializeComponent();
            searchwindow = new ResizeForm(this);
            searchwindow.Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label8.Text = "Top: " + searchwindow.Location.Y.ToString();
            label9.Text = "Left: " + searchwindow.Location.X.ToString();
            label10.Text = "Right: " + (searchwindow.Location.X + this.Width).ToString();
            label11.Text = "Bottom: " + (searchwindow.Location.Y + this.Height).ToString();
            label12.Text = (searchwindow.Location.X + this.Width - searchwindow.Location.X).ToString() + " x " + (searchwindow.Location.Y + this.Height - searchwindow.Location.Y).ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            searchwindow.Show();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            pictureBox1.BackColor = Color.FromArgb((int)numericUpDown1.Value, (int)numericUpDown2.Value, (int)numericUpDown3.Value);

            string hex = pictureBox1.BackColor.R.ToString("X2") + pictureBox1.BackColor.G.ToString("X2") + pictureBox1.BackColor.B.ToString("X2");
            searchcolor = Int32.Parse(hex, System.Globalization.NumberStyles.AllowHexSpecifier);
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            pictureBox1.BackColor = Color.FromArgb((int)numericUpDown1.Value, (int)numericUpDown2.Value, (int)numericUpDown3.Value);

            string hex = pictureBox1.BackColor.R.ToString("X2") + pictureBox1.BackColor.G.ToString("X2") + pictureBox1.BackColor.B.ToString("X2");
            searchcolor = Int32.Parse(hex, System.Globalization.NumberStyles.AllowHexSpecifier);
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            pictureBox1.BackColor = Color.FromArgb((int)numericUpDown1.Value, (int)numericUpDown2.Value, (int)numericUpDown3.Value);

            string hex = pictureBox1.BackColor.R.ToString("X2") + pictureBox1.BackColor.G.ToString("X2") + pictureBox1.BackColor.B.ToString("X2");
            searchcolor = Int32.Parse(hex, System.Globalization.NumberStyles.AllowHexSpecifier);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            groupBox1.Enabled = checkBox1.Checked;
            mouseclick = checkBox1.Checked;
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            shadevariation = (int)numericUpDown4.Value;
        }

        private void numericUpDown6_ValueChanged(object sender, EventArgs e)
        {
            nstep = (int)numericUpDown6.Value;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
                clicktype = "left";
            if (radioButton2.Checked == true)
                clicktype = "middle";
            if (radioButton3.Checked == true)
                clicktype = "right";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
                clicktype = "left";
            if (radioButton2.Checked == true)
                clicktype = "middle";
            if (radioButton3.Checked == true)
                clicktype = "right";
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
                clicktype = "left";
            if (radioButton2.Checked == true)
                clicktype = "middle";
            if (radioButton3.Checked == true)
                clicktype = "right";
        }

        private void numericUpDown5_ValueChanged(object sender, EventArgs e)
        {
            nclicks = (int)numericUpDown5.Value;
        }

        private void numericUpDown9_ValueChanged(object sender, EventArgs e)
        {
            delaystart = (int)numericUpDown9.Value;
        }

        private void numericUpDown7_ValueChanged(object sender, EventArgs e)
        {
            randomx = (int)numericUpDown7.Value;
        }

        private void numericUpDown8_ValueChanged(object sender, EventArgs e)
        {
            randomy = (int)numericUpDown8.Value;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            this.TopMost = checkBox2.Checked;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pickingcolor = true;
            searchwindow.startTimer1();
            this.Enabled = false;
        }

        public void setcolor(int color)
        {
            try
            {
                char[] hex = color.ToString("X2").ToCharArray();

                string r = hex[0].ToString() + hex[1].ToString();
                string g = hex[2].ToString() + hex[3].ToString();
                string b = hex[4].ToString() + hex[5].ToString();

                numericUpDown1.Value = (decimal)int.Parse(r, System.Globalization.NumberStyles.HexNumber);
                numericUpDown2.Value = (decimal)int.Parse(g, System.Globalization.NumberStyles.HexNumber);
                numericUpDown3.Value = (decimal)int.Parse(b, System.Globalization.NumberStyles.HexNumber);

                // */

                pictureBox1.BackColor = Color.FromArgb((int)numericUpDown1.Value, (int)numericUpDown2.Value, (int)numericUpDown3.Value);
            }
            catch (Exception e)
            {
                // Ignore the nigger
            }
            // MessageBox.Show(new string(hex));
            // textBox1.Text = tester;
        }
    }
}
