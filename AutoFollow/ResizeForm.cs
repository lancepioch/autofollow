using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AutoItX3Lib;
using System.Threading;

namespace AutoFollow
{
    public partial class ResizeForm : Form
    {
        BaseForm root;
        AutoItX3Class auto;
        public bool searching;
        Thread mthread;

        public ResizeForm(BaseForm rootform)
        {
            InitializeComponent();
            root = rootform;
            auto = new AutoItX3Class();
            searching = false;
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            mthread = new Thread(startmacro);
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mthread.Start();
        }

        public void startmacro()
        {
            root.WindowState = FormWindowState.Minimized;
            auto.Sleep(root.delaystart * 1000);

            int left = this.Location.X;
            int top = this.Location.Y;
            int bottom = this.Location.Y + this.Height;
            int right = this.Location.X + this.Width;

            searching = true;

            this.Hide();

            while (true == true)
            {
                object locate = auto.PixelSearch(left, top, right, bottom, root.searchcolor, root.shadevariation, root.nstep);

                if (locate is object[])
                {
                    Random newrandom = new Random();
                    Object[] coord = (object[])locate;
                    if (root.mouseclick == true)
                    {
                        auto.MouseClick(root.clicktype, (int)coord[0] + newrandom.Next((-1 * root.randomx), root.randomx), (int)coord[1] + newrandom.Next((-1 * root.randomy), root.randomy), root.nclicks, 0);
                    }
                    else
                    {
                        auto.MouseMove((int)coord[0] + newrandom.Next((-1 * root.randomx), root.randomx), (int)coord[1] + newrandom.Next((-1 * root.randomy), root.randomy), 0);
                    }
                }
            }
        }

        private void systemHotkey1_Pressed(object sender, EventArgs e)
        {
            mthread.Abort();
            mthread = new Thread(startmacro);
        }

        private void systemHotkey2_Pressed(object sender, EventArgs e)
        {
            if (root.pickingcolor == true)
            {
                timer1.Stop();
                root.pickingcolor = false;
                root.setcolor(auto.PixelGetColor(auto.MouseGetPosX(), auto.MouseGetPosY()));
                root.Enabled = true;
            }
        }

        public void startTimer1()
        {
            timer1.Start();
        }

        private void Form2_Resize(object sender, EventArgs e)
        {
            if (root != null)
            {
                root.label8.Text = "Top: " + this.Location.Y.ToString();
                root.label9.Text = "Left: " + this.Location.X.ToString();
                root.label10.Text = "Right: " + (this.Location.X + this.Width).ToString();
                root.label11.Text = "Bottom: " + (this.Location.Y + this.Height).ToString();
                root.label12.Text = (this.Location.X + this.Width - this.Location.X).ToString() + " x " + (this.Location.Y + this.Height - this.Location.Y).ToString();
            }
        }

        private void Form2_LocationChanged(object sender, EventArgs e)
        {
            root.label8.Text = "Top: " + this.Location.Y.ToString();
            root.label9.Text = "Left: " + this.Location.X.ToString();
            root.label10.Text = "Right: " + (this.Location.X + this.Width).ToString();
            root.label11.Text = "Bottom: " + (this.Location.Y + this.Height).ToString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            root.setcolor(auto.PixelGetColor(auto.MouseGetPosX(), auto.MouseGetPosY()));
        }
    }
}
