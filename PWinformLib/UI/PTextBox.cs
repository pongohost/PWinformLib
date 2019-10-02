using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using PWinformLib.UI;

namespace PWinformLib
{
    [ToolboxItem(true)]
    public partial class PTextBox : Control
    {
        int _radius = 15;
        public TextBox box = new TextBox();
        GraphicsPath shape;
        GraphicsPath innerRect;
        Color _bgColor;
        Color _borderColor;
        string _watermark;
        Font _wfont;
        private string _text;
        private ContentAlignment _textAlign;
        private ContentAlignment _wmTextAlign;
        private ContentAlignment _tempTextAlign;
        private string[] _autoComplete;
        private AutoCompleteMode _autoMode;
        private char passChar = ' ';

        public PTextBox()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.ResizeRedraw, true);

            box.Parent = this;
            Controls.Add(box);
            

            box.BorderStyle = BorderStyle.None;
            box.TextAlign = HorizontalAlignment.Left;
            box.Font = Font;

            _borderColor = Color.Crimson;
            BackColor = Color.Transparent;
            ForeColor = Color.DimGray;
            _bgColor = Color.White;
            SetStyle(box, ControlStyles.SupportsTransparentBackColor, true);
            box.BackColor = _bgColor;
            _textAlign = ContentAlignment.TopLeft;
            _wmTextAlign = ContentAlignment.TopLeft;

            Size = new Size(135, 33);
            DoubleBuffered = true;
            box.KeyDown += box_KeyDown;
            box.KeyUp += (sender, args) =>{base.OnKeyUp(args);} ;
            box.KeyPress += (sender, args) => { base.OnKeyPress(args); };
            box.TextChanged += box_TextChanged;
            box.MouseDoubleClick += box_MouseDoubleClick;
            box.Enter += text_Enter;
            box.Leave += text_Leave;
            box.VisibleChanged += text_Leave;
        }

        private static bool SetStyle(Control c, ControlStyles Style, bool value)
        {
            bool retval = false;
            Type typeTB = typeof(Control);
            System.Reflection.MethodInfo misSetStyle = typeTB.GetMethod("SetStyle", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (misSetStyle != null && c != null) { misSetStyle.Invoke(c, new object[] { Style, value }); retval = true; }
            return retval;
        }

        private void box_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            box.SelectAll();
        }

        private void text_Leave(object sender, EventArgs e)
        {
            if (box.Text.Length == 0)
            {
                box.Text = _watermark;
                box.Font = _wfont;
                box.PasswordChar = '\0';
                _tempTextAlign = PTextAlign;
                PTextAlign = _wmTextAlign;
            }
        }

        private void text_Enter(object sender, EventArgs e)
        {
            if (box.Text.Equals(_watermark))
            {
                box.Text = "";
                box.Font = Font;
                box.PasswordChar = passChar;
                PTextAlign = _tempTextAlign;
            }
        }

        public event EventHandler TextChanged;
        private void box_TextChanged(object sender, EventArgs e)
        {
            resizeTextBox();
            if (this.TextChanged != null)
                this.TextChanged(this, e);
        }

        private void resizeTextBox()
        {
            //box.BackColor = Color.Aqua;
            const int padding = 5;
            int numLines = box.GetLineFromCharIndex(box.TextLength) + 1;
            int border = box.Height - box.ClientSize.Height;
            int heightBox = box.Font.Height * numLines + padding + border;
            if (heightBox + _radius > Height)
            {
                box.Height = Height - _radius - 2;
                box.ScrollBars = ScrollBars.Vertical;
            }
            else
            {
                box.Height = heightBox;
                box.ScrollBars = ScrollBars.None;
            }
            box.Width = Width - (int)(_radius * 2);
            int y = 0;
            if (_textAlign.ToString().Contains("Top"))
                y = 2;
            if (_textAlign.ToString().Contains("Mid"))
                y = (Height - box.Height) / 2;
            if (_textAlign.ToString().Contains("Bottom"))
                y = (Height - box.Height) - 2 /*- _radius / 2+2*/;
            if (_textAlign.ToString().Contains("Left"))
                box.TextAlign = HorizontalAlignment.Left;
            if (_textAlign.ToString().Contains("Center"))
                box.TextAlign = HorizontalAlignment.Center;
            if (_textAlign.ToString().Contains("Right"))
                box.TextAlign = HorizontalAlignment.Right;
            box.Location = new Point(_radius, y);
        }
        
        void box_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                box.SelectionStart = 0;
                box.SelectionLength = Text.Length;
            }
            base.OnKeyDown(e);
        }
        
        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            box.Font = Font;
            Invalidate();
        }

        protected override void OnClick(EventArgs e)
        {
            box.Focus();
        }

        protected override void OnGotFocus(EventArgs e)
        {
            box.Focus();
        }

        protected override void OnForeColorChanged(EventArgs e)
        {
            base.OnForeColorChanged(e);
            box.ForeColor = ForeColor;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (_autoComplete != null && _autoMode != null)
            {
                AutoCompleteStringCollection acsc = new AutoCompleteStringCollection();
                acsc.AddRange(_autoComplete);
                box.AutoCompleteCustomSource = acsc;
                box.AutoCompleteMode = _autoMode;
                box.AutoCompleteSource = AutoCompleteSource.CustomSource;
            }
            shape = new RoundedBorder(Width-1, Height-1, _radius).Path;
            innerRect = new RoundedBorder(Width - 1.5f, Height - 1.5f, _radius, 0.5f, 0.5f).Path;
            resizeTextBox();
            
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            Bitmap bmp = new Bitmap(Width, Height);
            Graphics grp = Graphics.FromImage(bmp);
            e.Graphics.DrawPath(new Pen(_borderColor,1), shape);
            using (SolidBrush brush = new SolidBrush(_bgColor))
                e.Graphics.FillPath(brush, innerRect);
            Transparenter.MakeTransparent(this, e.Graphics);

            base.OnPaint(e);
        }

        [EditorAttribute(
            "System.ComponentModel.Design.MultilineStringEditor, System.Design",
            "System.Drawing.Design.UITypeEditor")]
        public override string Text
        {
            get
            {
                if (!box.Text.Equals(_watermark))
                    return box.Text;
                return "";
            }
            set
            {
                box.Text = value;
                Invalidate();
            }
        }

        //[Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(System.Drawing.Design.UITypeEditor))]
        public string[] AutoCompleteList
        {
            get { return _autoComplete; }
            set {_autoComplete = value;Invalidate();}
        }
        
        public AutoCompleteMode AutoCompleteMode
        {
            get { return _autoMode; }
            set { _autoMode = value;Invalidate(); }
        }
        public Char PasswordChar
        {
            get { return passChar; }
            set
            {
                box.PasswordChar = value;
                passChar = value;

                Console.Out.WriteLine(passChar + " =====" + PasswordChar + " ====="+value);
                Invalidate(); }
        }
        
        public ContentAlignment PTextAlign
        {
            get { return _textAlign; }
            set { _textAlign = value; Invalidate(); }
        }
        
        public bool PMultiLine
        {
            get { return box.Multiline; }
            set { box.Multiline = value; Invalidate(); }
        }

        /// <summary>
        /// Gets or sets the cue banner text FOnt.
        /// </summary>
        public Font PWatermarkFont
        {
            get
            {
                return _wfont;
            }

            set
            {
                _wfont = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets Watermark text.
        /// </summary>
        public string PWatermark
        {
            get
            {
                return _watermark;
            }

            set
            {
                _watermark = value;
                Invalidate();
            }
        }
        
        public ContentAlignment PWatermarkTextAlign
        {
            get { return _wmTextAlign; }
            set { _wmTextAlign = value; Invalidate(); }
        }

        public Color PBgColor
        {
            get
            {
                return _bgColor;
            }
            set
            {
                _bgColor = value;
                //if (br != Color.Transparent)
                    box.BackColor = _bgColor;
                Invalidate();
            }
        }

        public override Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                base.BackColor = Color.Transparent;
            }
        }

        public int PRadius
        {
            get
            {
                return _radius;
            }
            set
            {
                _radius = value;
                Invalidate();
            }
        }

        public Color PBorderColor
        {
            get
            {
                return _borderColor;
            }
            set
            {
                _borderColor = value;
                Invalidate();
            }
        }
    }
}
