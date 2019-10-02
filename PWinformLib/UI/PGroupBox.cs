using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using ContentAlignment = System.Drawing.ContentAlignment;

namespace PWinformLib.UI
{
    public partial class PGroupBox : UserControl
    {
        private DashStyle _BorderType;
        private int _radius;
        private Color _borderColor;
        private Color _bgColor;
        private string _text;
        private ContentAlignment _textAlignment;
        private Padding _textMargin;
        
        public PGroupBox()
        {
            InitializeComponent();
            _radius = 35;
            _borderColor = Color.Gray;
            _bgColor = Color.Transparent;
            title_lbl.Text = "Title Here";
            _textAlignment = ContentAlignment.TopLeft;
        }
        
        /*public Color BackColor
        {
            get { return panelBox.BackColor; }
            set { panelBox.BackColor = value;Invalidate(); }
        }*/
        
        protected override void OnPaint(PaintEventArgs e)
        {
            int yP = 0, xT = _radius, yT = 0;
            if (_textAlignment.ToString().Contains("Top"))
            {
                yP = title_lbl.Height + _textMargin.Bottom;
            }

            if (_textAlignment.ToString().Contains("Mid"))
            {
                yP = title_lbl.Height/2 + _textMargin.Bottom;
            }

            if (_textAlignment.ToString().Contains("Bottom"))
            {
                yT = 2 + _textMargin.Top;
            }

            if (_textAlignment.ToString().Contains("Cent"))
            {
                xT = (Width / 2) - (title_lbl.Width / 2);
            }

            if (_textAlignment.ToString().Contains("Right"))
            {
                xT = Width - title_lbl.Width - _radius;
            }
            title_lbl.Location = new Point(xT, yT);

            //Helper.DrawBorder(e,(Control)sender,Color.Red,2,ButtonBorderStyle.Dashed);
            GraphicsPath shape = new RoundedBorder(Width, Height, _radius,0,yP).Path;
            GraphicsPath innerRect = new RoundedBorder(Width-0.5f, Height-0.5f, _radius, 0.5f, yP+0.5f).Path;
            //resizeTextBox();

            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            Bitmap bmp = new Bitmap(Width, Height);
            Graphics grp = Graphics.FromImage(bmp);
            float[] dashValues = { 5, 2, 15, 4 };
            Pen pen = new Pen(_borderColor, 3);
            pen.DashStyle = _BorderType;
            //pen.DashPattern = dashValues;
            e.Graphics.DrawPath(pen, shape);
            using (SolidBrush brush = new SolidBrush(_bgColor))
                e.Graphics.FillPath(brush, innerRect);
            //Transparenter.MakeTransparent(panelBox, e.Graphics);

            if (_textAlignment.ToString().Contains("Mid"))
            {
                yP = title_lbl.Height / 2 + _textMargin.Bottom;
                pen = new Pen(_bgColor, 3);
                e.Graphics.DrawLine(pen, title_lbl.Location.X - 3, yP, title_lbl.Location.X + title_lbl.Width + 3, yP);
            }
//            e.Graphics.DrawArc(pen,10,10,150,150,180,90);
            base.OnPaint(e);
        }
        
        public DashStyle PBorderType
        {
            get { return _BorderType; }
            set
            {
                _BorderType = value;
                Invalidate();
            }
        }

        public Color PBorderColor
        {
            get { return _borderColor; }
            set
            {
                _borderColor = value;
                Invalidate();
            }
        }
        
        public Color PBgColor
        {
            get { return _bgColor; }
            set
            {
                _bgColor = value;
                Invalidate();
            }
        }

        public int Pradius
        {
            get { return _radius;}
            set { _radius = value;Invalidate(); }
        }

        public string PText
        {
            get { return title_lbl.Text; }
            set { title_lbl.Text = value; Invalidate(); }
        }

        public ContentAlignment PTitlePosition
        {
            get { return _textAlignment; }
            set { _textAlignment = value; Invalidate(); }
        }

        public Padding PTitleMargin
        {
            get { return _textMargin; }
            set { _textMargin = value; Invalidate(); }
        }

    }
}
