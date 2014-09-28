using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace AutoFollow
{
    public partial class BaseForm : Form
    {
        private readonly ResizeForm _searchwindow;
        private const string Toptext = "Top: ", Righttext = "Right: ", Bottomtext = "Bottom: ", Lefttext = "Left: ";
        private const string Dimensionseparator = " x ";

        public string Clicktype = "left";
        public int Delaystart = 0;
        public bool Mouseclick = false;
        public int Nclicks = 1;
        public int Nstep = 1;
        public bool Pickingcolor = false;
        public int Randomx = 0;
        public int Randomy = 0;
        public int Searchcolor = 0;
        public int Shadevariation = 0;

        public BaseForm()
        {
            InitializeComponent();
            _searchwindow = new ResizeForm(this);
            _searchwindow.Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label8.Text = Toptext + _searchwindow.Location.Y;
            label9.Text = Lefttext + _searchwindow.Location.X;
            label10.Text = Righttext + (_searchwindow.Location.X + Width);
            label11.Text = Bottomtext + (_searchwindow.Location.Y + Height);
            label12.Text = (_searchwindow.Location.X + Width - _searchwindow.Location.X) + Dimensionseparator +
                           (_searchwindow.Location.Y + Height - _searchwindow.Location.Y);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _searchwindow.Show();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            pictureBox1.BackColor = Color.FromArgb((int) numericUpDown1.Value, (int) numericUpDown2.Value,
                (int) numericUpDown3.Value);

            string hex = pictureBox1.BackColor.R.ToString("X2") + pictureBox1.BackColor.G.ToString("X2") +
                         pictureBox1.BackColor.B.ToString("X2");
            Searchcolor = Int32.Parse(hex, NumberStyles.AllowHexSpecifier);
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            pictureBox1.BackColor = Color.FromArgb((int) numericUpDown1.Value, (int) numericUpDown2.Value,
                (int) numericUpDown3.Value);

            string hex = pictureBox1.BackColor.R.ToString("X2") + pictureBox1.BackColor.G.ToString("X2") +
                         pictureBox1.BackColor.B.ToString("X2");
            Searchcolor = Int32.Parse(hex, NumberStyles.AllowHexSpecifier);
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            pictureBox1.BackColor = Color.FromArgb((int) numericUpDown1.Value, (int) numericUpDown2.Value,
                (int) numericUpDown3.Value);

            string hex = pictureBox1.BackColor.R.ToString("X2") + pictureBox1.BackColor.G.ToString("X2") +
                         pictureBox1.BackColor.B.ToString("X2");
            Searchcolor = Int32.Parse(hex, NumberStyles.AllowHexSpecifier);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            groupBox1.Enabled = checkBox1.Checked;
            Mouseclick = checkBox1.Checked;
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            Shadevariation = (int) numericUpDown4.Value;
        }

        private void numericUpDown6_ValueChanged(object sender, EventArgs e)
        {
            Nstep = (int) numericUpDown6.Value;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
                Clicktype = "left";
            if (radioButton2.Checked)
                Clicktype = "middle";
            if (radioButton3.Checked)
                Clicktype = "right";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
                Clicktype = "left";
            if (radioButton2.Checked)
                Clicktype = "middle";
            if (radioButton3.Checked)
                Clicktype = "right";
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
                Clicktype = "left";
            if (radioButton2.Checked)
                Clicktype = "middle";
            if (radioButton3.Checked)
                Clicktype = "right";
        }

        private void numericUpDown5_ValueChanged(object sender, EventArgs e)
        {
            Nclicks = (int) numericUpDown5.Value;
        }

        private void numericUpDown9_ValueChanged(object sender, EventArgs e)
        {
            Delaystart = (int) numericUpDown9.Value;
        }

        private void numericUpDown7_ValueChanged(object sender, EventArgs e)
        {
            Randomx = (int) numericUpDown7.Value;
        }

        private void numericUpDown8_ValueChanged(object sender, EventArgs e)
        {
            Randomy = (int) numericUpDown8.Value;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            TopMost = checkBox2.Checked;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Pickingcolor = true;
            _searchwindow.StartTimer1();
            Enabled = false;
        }

        public void Setcolor(int color)
        {
            string hexString = color.ToString("X2");
            while (hexString.Length < 6) hexString = "0" + hexString;
            char[] hex = hexString.ToCharArray();

            string r = "" + hex[0] + hex[1];
            string g = "" + hex[2] + hex[3];
            string b = "" + hex[4] + hex[5];

            numericUpDown1.Value = int.Parse(r, NumberStyles.HexNumber);
            numericUpDown2.Value = int.Parse(g, NumberStyles.HexNumber);
            numericUpDown3.Value = int.Parse(b, NumberStyles.HexNumber);

            pictureBox1.BackColor = Color.FromArgb((int) numericUpDown1.Value, (int) numericUpDown2.Value,
                (int) numericUpDown3.Value);
        }
    }
}