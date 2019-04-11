using System;
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
            box.TextChanged += box_TextChanged;
            box.MouseDoubleClick += box_MouseDoubleClick;
            box.Enter += text_Enter;
            box.Leave += text_Leave;
            box.VisibleChanged += text_Leave;
        }

        public static bool SetStyle(Control c, ControlStyles Style, bool value)
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
                passChar = box.PasswordChar;
                box.PasswordChar = '\0';
                _tempTextAlign = TextAlign;
                TextAlign = _wmTextAlign;
            }
        }

        private void text_Enter(object sender, EventArgs e)
        {
            if (box.Text.Equals(_watermark))
            {
                box.Text = "";
                box.Font = Font;
                box.PasswordChar = passChar;
                TextAlign = _tempTextAlign;
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
            box.Width = Width - (int)(_radius * 1.5)-4;
            int y = 0;
            if (_textAlign.ToString().Contains("Top"))
                y = _radius / 2 + 2;
            if (_textAlign.ToString().Contains("Mid"))
                y = (Height - box.Height) / 2 + 2;
            if (_textAlign.ToString().Contains("Bottom"))
                y = (Height - box.Height) - _radius / 2 + 2;
            if (_textAlign.ToString().Contains("Left"))
                box.TextAlign = HorizontalAlignment.Left;
            if (_textAlign.ToString().Contains("Center"))
                box.TextAlign = HorizontalAlignment.Center;
            if (_textAlign.ToString().Contains("Right"))
                box.TextAlign = HorizontalAlignment.Right;
            box.Location = new Point(_radius / 2 + 2, y);
        }
        
        void box_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                box.SelectionStart = 0;
                box.SelectionLength = Text.Length;
            }
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

        protected override void OnForeColorChanged(EventArgs e)
        {
            base.OnForeColorChanged(e);
            box.ForeColor = ForeColor;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
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
            get { return box.Text;}
            set {
                box.Text = value;
                Invalidate();
            }
        }


        public Char PasswordChar
        {
            get { return box.PasswordChar; }
            set { box.PasswordChar = value; Invalidate(); }
        }
        
        public ContentAlignment TextAlign
        {
            get { return _textAlign; }
            set { _textAlign = value; Invalidate(); }
        }
        
        public bool MultiLine
        {
            get { return box.Multiline; }
            set { box.Multiline = value; Invalidate(); }
        }

        /// <summary>
        /// Gets or sets the cue banner text FOnt.
        /// </summary>
        public Font WatermarkFont
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
        public string Watermark
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
        
        public ContentAlignment WatermarkTextAlign
        {
            get { return _wmTextAlign; }
            set { _wmTextAlign = value; Invalidate(); }
        }

        public Color BgColor
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

        public int Radius
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

        public Color BorderColor
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
