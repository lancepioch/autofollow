using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Forms;

namespace AutoFollow
{
    /// <summary>
    ///     Handles a System Hotkey
    /// </summary>
    public class SystemHotkey : Component, IDisposable
    {
        private Container _components;
        private bool _isRegistered;
        protected Shortcut MHotKey = Shortcut.None;
        protected DummyWindowWithEvent MWindow = new DummyWindowWithEvent(); //window for WM_Hotkey Messages

        public SystemHotkey(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
            MWindow.ProcessMessage += MessageEvent;
            _isRegistered = false;
        }

        public SystemHotkey()
        {
            InitializeComponent();
            if (!DesignMode)
            {
                MWindow.ProcessMessage += MessageEvent;
            }
        }

        public bool IsRegistered
        {
            get { return _isRegistered; }
        }


        [DefaultValue(Shortcut.None)]
        public Shortcut Shortcut
        {
            get { return MHotKey; }
            set
            {
                if (DesignMode)
                {
                    MHotKey = value;
                    return;
                } //Don't register in Designmode
                if ((_isRegistered) && (MHotKey != value)) //Unregister previous registered Hotkey
                {
                    if (UnregisterHotkey())
                    {
                        Debug.WriteLine("Unreg: OK");
                        _isRegistered = false;
                    }
                    else
                    {
                        if (Error != null) Error(this, EventArgs.Empty);
                        Debug.WriteLine("Unreg: ERR");
                    }
                }
                if (value == Shortcut.None)
                {
                    MHotKey = value;
                    return;
                }
                if (RegisterHotkey(value)) //Register new Hotkey
                {
                    Debug.WriteLine("Reg: OK");
                    _isRegistered = true;
                }
                else
                {
                    if (Error != null) Error(this, EventArgs.Empty);
                    Debug.WriteLine("Reg: ERR");
                }
                MHotKey = value;
            }
        }

        public Container Components
        {
            get { return _components; }
            set { _components = value; }
        }

        public new void Dispose()
        {
            if (_isRegistered)
            {
                if (UnregisterHotkey())
                    Debug.WriteLine("Unreg: OK");
            }
            Debug.WriteLine("Disposed");
        }

        #region Component Designer generated code

        /// <summary>
        ///     Required method for Designer support - do not modify
        ///     the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Components = new System.ComponentModel.Container();
        }

        #endregion

        public event EventHandler Pressed;
        public event EventHandler Error;

        protected void MessageEvent(object sender, ref Message m, ref bool handled)
        {
            //Handle WM_Hotkey event
            if ((m.Msg == (int) Msgs.WM_HOTKEY) && (m.WParam == (IntPtr) GetType().GetHashCode()))
            {
                handled = true;
                Debug.WriteLine("HOTKEY pressed!");
                if (Pressed != null) Pressed(this, EventArgs.Empty);
            }
        }

        protected bool UnregisterHotkey()
        {
            //unregister hotkey
            return User32.UnregisterHotKey(MWindow.Handle, GetType().GetHashCode());
        }

        protected bool RegisterHotkey(Shortcut key)
        {
            //register hotkey
            int mod = 0;
            var k2 = Keys.None;
            if (((int) key & (int) Keys.Alt) == (int) Keys.Alt)
            {
                mod += (int) Modifiers.MOD_ALT;
                k2 = Keys.Alt;
            }
            if (((int) key & (int) Keys.Shift) == (int) Keys.Shift)
            {
                mod += (int) Modifiers.MOD_SHIFT;
                k2 = Keys.Shift;
            }
            if (((int) key & (int) Keys.Control) == (int) Keys.Control)
            {
                mod += (int) Modifiers.MOD_CONTROL;
                k2 = Keys.Control;
            }

            Debug.Write(mod + " ");
            Debug.WriteLine((((int) key) - ((int) k2)).ToString(CultureInfo.InvariantCulture));

            return User32.RegisterHotKey(MWindow.Handle, GetType().GetHashCode(), mod, ((int) key) - ((int) k2));
        }
    }
}