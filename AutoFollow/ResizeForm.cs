using System;
using System.Threading;
using System.Windows.Forms;
using AutoItX3Lib;

namespace AutoFollow
{
    public partial class ResizeForm : Form
    {
        private readonly BaseForm _root;
        private readonly AutoItX3Class _auto;
        public bool Searching;
        private Thread _mthread;
        private const string Toptext = "Top: ", Righttext = "Right: ", Bottomtext = "Bottom: ", Lefttext = "Left: ";
        private const string Dimensionseparator = " x ";

        public ResizeForm(BaseForm rootform)
        {
            InitializeComponent();
            _root = rootform;
            _auto = new AutoItX3Class();
            Searching = false;
            CheckForIllegalCrossThreadCalls = false;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            _mthread = new Thread(Startmacro);
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _mthread.Start();
        }

        public void Startmacro()
        {
            _root.WindowState = FormWindowState.Minimized;
            _auto.Sleep(_root.Delaystart*1000);

            int left = Location.X;
            int top = Location.Y;
            int bottom = Location.Y + Height;
            int right = Location.X + Width;

            Searching = true;

            Hide();

            while (Searching)
            {
                object locate = _auto.PixelSearch(left, top, right, bottom, _root.Searchcolor, _root.Shadevariation,
                    _root.Nstep);

                if (locate is object[])
                {
                    var newrandom = new Random();
                    var coord = (object[]) locate;
                    if (_root.Mouseclick)
                    {
                        _auto.MouseClick(_root.Clicktype,
                            (int) coord[0] + newrandom.Next((-1*_root.Randomx), _root.Randomx),
                            (int) coord[1] + newrandom.Next((-1*_root.Randomy), _root.Randomy), _root.Nclicks, 0);
                    }
                    else
                    {
                        _auto.MouseMove((int) coord[0] + newrandom.Next((-1*_root.Randomx), _root.Randomx),
                            (int) coord[1] + newrandom.Next((-1*_root.Randomy), _root.Randomy), 0);
                    }
                }
            }
        }

        private void systemHotkey1_Pressed(object sender, EventArgs e)
        {
            _mthread.Abort();
            _mthread = new Thread(Startmacro);
        }

        private void systemHotkey2_Pressed(object sender, EventArgs e)
        {
            if (_root.Pickingcolor)
            {
                timer1.Stop();
                _root.Pickingcolor = false;
                _root.Setcolor(_auto.PixelGetColor(_auto.MouseGetPosX(), _auto.MouseGetPosY()));
                _root.Enabled = true;
            }
        }

        public void StartTimer1()
        {
            timer1.Start();
        }

        private void Form2_Resize(object sender, EventArgs e)
        {
            if (_root != null)
            {
                _root.label8.Text = Toptext + Location.Y;
                _root.label9.Text = Lefttext + Location.X;
                _root.label10.Text = Righttext + (Location.X + Width);
                _root.label11.Text = Bottomtext + (Location.Y + Height);
                _root.label12.Text = (Location.X + Width - Location.X) + Dimensionseparator + (Location.Y + Height - Location.Y);
            }
        }

        private void Form2_LocationChanged(object sender, EventArgs e)
        {
            _root.label8.Text = Toptext + Location.Y;
            _root.label9.Text = Lefttext + Location.X;
            _root.label10.Text = Righttext + (Location.X + Width);
            _root.label11.Text = Bottomtext + (Location.Y + Height);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            _root.Setcolor(_auto.PixelGetColor(_auto.MouseGetPosX(), _auto.MouseGetPosY()));
        }
    }
}