using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Linq;
using Font = System.Drawing.Font;
using Point = System.Drawing.Point;
using Rectangle = System.Drawing.Rectangle;

namespace PWinformLib.UI
{
    [ToolboxItem(true)]
    public partial class PFlatButton : Control, IButtonControl
    {
        int radius;
        MouseState state;
        RoundedBorder roundedRect;
        private PictureBox pbBox;
        Color inactive1, inactive2, active1, active2;
        private Position _iconPosition;
        private Image _Picon;
        private bool _multiLine;
        private bool _autoSizeFont;
        private ContentAlignment _textAlign;
        private float gradientAngle;
        private Color strokeColor;
        private bool stroke;
        private EStyleButton _StyleButton;

        public bool Stroke
        {
            get { return stroke; }
            set
            {
                stroke = value;
                Invalidate();
            }
        }

        public Color StrokeColor
        {
            get { return strokeColor; }
            set
            {
                strokeColor = value;
                Invalidate();
            }
        }

        public PFlatButton()
        {
            Width = 65;
            Height = 30;
            stroke = false;
            strokeColor = Color.Gray;
            inactive1 = Color.FromArgb(44, 188, 210);
            inactive2 = Color.FromArgb(33, 167, 188);
            active1 = Color.FromArgb(64, 168, 183);
            active2 = Color.FromArgb(36, 164, 183);
            gradientAngle = 180f;
            _autoSizeFont = true;
            _multiLine = false;
            _StyleButton = EStyleButton.Custom;
            _iconPosition = Position.Left;
            _textAlign = ContentAlignment.MiddleCenter;

            pbBox = new PictureBox();
            pbBox.MouseEnter += MouseEnterAct;
            pbBox.MouseLeave += MouseLeaveAct;
            pbBox.MouseDown += MouseDownAct;
            pbBox.MouseUp += MouseUpAct;
            pbBox.MouseClick += MouseClickAct;
            radius = 5;
            roundedRect = new RoundedBorder(Width, Height, radius);

            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor |
                     ControlStyles.UserPaint, true);
            BackColor = Color.Transparent;
            ForeColor = Color.Black;
            Font = new Font("Verdana", 10, FontStyle.Bold);
            state = MouseState.Leave;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            roundedRect = new RoundedBorder(Width, Height, radius);
            e.Graphics.FillRectangle(Brushes.Transparent, ClientRectangle);

            int R1 = (active1.R + inactive1.R) / 2;
            int G1 = (active1.G + inactive1.G) / 2;
            int B1 = (active1.B + inactive1.B) / 2;

            int R2 = (active2.R + inactive2.R) / 2;
            int G2 = (active2.G + inactive2.G) / 2;
            int B2 = (active2.B + inactive2.B) / 2;

            Rectangle rect = new Rectangle(0, 0, Width, Height);

            if (this.Enabled)
            {
                if (state == MouseState.Leave)
                    using (LinearGradientBrush inactiveGB = new LinearGradientBrush(rect, inactive1, inactive2, gradientAngle))
                        e.Graphics.FillPath(inactiveGB, roundedRect.Path);
                else if (state == MouseState.Enter)
                    using (LinearGradientBrush activeGB = new LinearGradientBrush(rect, active1, active2, gradientAngle))
                        e.Graphics.FillPath(activeGB, roundedRect.Path);
                else if (state == MouseState.Down)
                    using (LinearGradientBrush downGB = new LinearGradientBrush(rect, Color.FromArgb(R1, G1, B1), Color.FromArgb(R2, G2, B2), gradientAngle))
                        e.Graphics.FillPath(downGB, roundedRect.Path);
                if (stroke)
                    using (Pen pen = new Pen(strokeColor, 1))
                    using (GraphicsPath path = new RoundedBorder(Width - (radius > 0 ? 0 : 1), Height - (radius > 0 ? 0 : 1), radius).Path)
                        e.Graphics.DrawPath(pen, path);
            }
            else
            {
                Color linear1 = Color.FromArgb(190, 190, 190);
                Color linear2 = Color.FromArgb(210, 210, 210);
                using (LinearGradientBrush inactiveGB = new LinearGradientBrush(rect, linear1, linear2, 90f))
                {
                    e.Graphics.FillPath(inactiveGB, roundedRect.Path);
                    e.Graphics.DrawPath(new Pen(inactiveGB), roundedRect.Path);
                }
            }
            
            Size newSize = Size;
            //newSize.Width = newSize.Width - Padding.Left - Padding.Right;
            float[] getSize = NewFontSize(e.Graphics, newSize, Font, Text);
            float fontSize = getSize[0];
            Font newFont = Font;
            if (_autoSizeFont)
            {
                newFont = new Font(Font.FontFamily, fontSize, Font.Style);
                getSize[1] = measureSizeF(e.Graphics, newFont, Text).Width;
                getSize[2] = measureSizeF(e.Graphics, newFont, Text).Height;
            }

            Rectangle rrRectangle = ClientRectangle;
            StringAlignment horAlgnmt = StringAlignment.Center;
            StringAlignment verAlgnmt = StringAlignment.Center;
            int selisihH = (int)((ClientRectangle.Width - getSize[1]) / 2);
            int selisihV = (int)((ClientRectangle.Height - getSize[2]) / 2);
            if (_textAlign.ToString().Contains("Left"))
            {
                horAlgnmt = StringAlignment.Near;
            }

            if (_textAlign.ToString().Contains("Right"))
            {
                horAlgnmt = StringAlignment.Far;
            }
            if (_textAlign.ToString().Contains("Top"))
            {
                verAlgnmt = StringAlignment.Near;
            }

            if (_textAlign.ToString().Contains("Bottom"))
            {
                verAlgnmt = StringAlignment.Far;
            }
            if (_Picon != null)
            {
                int locX = ClientRectangle.Width / 2 - _Picon.Width / 2;
                int locY = ClientRectangle.Height / 2 - _Picon.Height / 2;
                if (_iconPosition == Position.FollowLeft && _textAlign.ToString().Contains("Center") )
                {
                    locX = (int)(ClientRectangle.Width - getSize[1]) / 2 - _Picon.Width;
                    locX = locX > Padding.Left + 5 ? locX : Padding.Left + 5;
                }
                else if (_iconPosition == Position.FollowLeft && _textAlign.ToString().Contains("Right"))
                {
                    locX = (int)(ClientRectangle.Width - getSize[1] - _Picon.Width);
                    locX = locX > Padding.Left + 5 ? locX : Padding.Left + 5;
                }
                else if (_iconPosition == Position.FollowRight && _textAlign.ToString().Contains("Center"))
                {
                    locX = (int)((ClientRectangle.Width + getSize[1]) / 2);
                    int tempX = ClientRectangle.Width - _Picon.Width - 5 - Padding.Right;
                    locX = locX >  tempX ? tempX:locX;
                }
                else if (_iconPosition == Position.FollowRight && _textAlign.ToString().Contains("Left"))
                {
                    locX = (int)(getSize[1] + 5);
                    int tempX = ClientRectangle.Width - _Picon.Width - 5 - Padding.Right;
                    locX = locX > tempX ? tempX : locX;
                }
                else if (_iconPosition == Position.FollowTop && _textAlign.ToString().Contains("Middle"))
                {
                    locY = (int)((ClientRectangle.Height - getSize[2]) / 2) - _Picon.Height;
                    locY = locY > Padding.Top + 5 ? locY : Padding.Top + 5;
                }
                else if (_iconPosition == Position.FollowTop && _textAlign.ToString().Contains("Bottom"))
                {
                    locY = (int)(ClientRectangle.Height - getSize[2] - _Picon.Height);
                    int tempY = ClientRectangle.Width - _Picon.Width - 5 - Padding.Right;
                    locY = locY > Padding.Top + 5 ? locY : Padding.Top + 5;
                }
                else if (_iconPosition == Position.FollowBottom && _textAlign.ToString().Contains("Middle"))
                {
                    locY = (int)((ClientRectangle.Height + getSize[2]) / 2);
                    int tempY = ClientRectangle.Height - _Picon.Width - 5 - Padding.Right;
                    locY = locY > tempY ? tempY:locY;
                }
                else if (_iconPosition == Position.FollowBottom && _textAlign.ToString().Contains("Top"))
                {
                    locY = (int)(getSize[2] + 5);
                    int tempY = ClientRectangle.Height - _Picon.Width - 5 - Padding.Right;
                    locY = locY > tempY ? tempY : locY;
                }
                else if (_iconPosition.ToString().Contains("Left"))
                {
                    locX = 5+Padding.Left;
                    if((ClientRectangle.Width-getSize[1])/2<_Picon.Width+5+Padding.Left)
                        rrRectangle.X = _Picon.Width+ Padding.Left +5-selisihH;
                    if(_textAlign.ToString().Contains("Left"))
                        rrRectangle.X = _Picon.Width + Padding.Left + 5;

                }
                else if (_iconPosition.ToString().Contains("Right"))
                {
                    locX = ClientRectangle.Width - _Picon.Width-5-Padding.Right;
                    if ((ClientRectangle.Width - getSize[1]) / 2 < _Picon.Width + 5 + Padding.Right || 
                        _textAlign.ToString().Contains("Right"))
                        rrRectangle.X = -_Picon.Width - Padding.Right + 5;
                }
                else if (_iconPosition.ToString().Contains("Top"))
                {
                    locY = 5 + Padding.Top;
                    if (selisihV < _Picon.Height + 5 + Padding.Top)
                        rrRectangle.Y = _Picon.Height + Padding.Top -selisihV;
                    if (_textAlign.ToString().Contains("Top"))
                        rrRectangle.Y = _Picon.Height + 5 + Padding.Top;
                }
                else if (_iconPosition.ToString().Contains("Bottom"))
                {
                    locY = ClientRectangle.Height - _Picon.Width - 5 - Padding.Bottom;
                    if (selisihV < _Picon.Width + 5 + Padding.Right ||
                        _textAlign.ToString().Contains("Bottom"))
                        rrRectangle.Y = -_Picon.Width - Padding.Right + 5;
                }

                pbBox.Location = new Point(locX, locY);
                pbBox.Size = new Size(_Picon.Width,_Picon.Height);
                pbBox.BackgroundImage = _Picon;
                pbBox.BackgroundImageLayout = ImageLayout.Stretch;
                Controls.Add(pbBox);
                pbBox.BringToFront();
            }

            using (StringFormat sf = new StringFormat()
            {
                LineAlignment = verAlgnmt,
                Alignment = horAlgnmt
            })
            using (Brush brush = new SolidBrush(ForeColor))
                e.Graphics.DrawString(Text, newFont, brush, rrRectangle, sf);
            base.OnPaint(e);
        }

        private void ChangeDefaultColor()
        {
            if (_StyleButton == EStyleButton.Save)
            {
                active1 = Color.MidnightBlue;
                active2 = Color.LightBlue;
                inactive1 = Color.Lime;
                inactive2 = Color.DarkGreen;
            }
            if (_StyleButton == EStyleButton.Clear)
            {
                active1 = Color.MidnightBlue;
                active2 = Color.LightBlue;
                inactive1 = Color.Lime;
                inactive2 = Color.DarkGreen;
            }
            if (_StyleButton == EStyleButton.Delete)
            {
                active1 = Color.DarkRed;
                active2 = Color.Red;
                inactive1 = Color.Red;
                inactive2 = Color.DarkRed;
            }
            if (_StyleButton == EStyleButton.Refresh)
            {
                active1 = Color.MidnightBlue;
                active2 = Color.LightBlue;
                inactive1 = Color.LightBlue;
                inactive2 = Color.MidnightBlue;
            }
        }

        private SizeF measureSizeF(Graphics grp, Font font, string str)
        {
            return grp.MeasureString(str, font);
        }

        private float[] NewFontSize(Graphics graphics, Size size, Font font, string str)
        {
            float[] hasil = {1,1,1};
            if (!_multiLine)
            {
                SizeF stringSize = graphics.MeasureString(str, font);
                float curWidth = stringSize.Width;
                float curHeight = stringSize.Height;
                if (_Picon != null)
                {
                    //if (_iconPosition == Position.FollowLeft || _iconPosition == Position.FollowRight)
                    if (_iconPosition.ToString().Contains("Left") || _iconPosition.ToString().Contains("Right"))
                    {
                        curWidth = stringSize.Width + _Picon.Width;
                        curHeight = stringSize.Height > _Picon.Height ? stringSize.Height : _Picon.Height;
                    }
                    if (_iconPosition == Position.FollowTop || _iconPosition == Position.FollowBottom)
                    {
                        curWidth = stringSize.Width > _Picon.Width ? stringSize.Width : _Picon.Width;
                        curHeight = stringSize.Height + _Picon.Height;
                    }
                }
                float wRatio = size.Width / (curWidth+Padding.Left+Padding.Right);
                float hRatio = size.Height / (curHeight+Padding.Top+Padding.Bottom);
                float ratio = Math.Min(hRatio, wRatio);
                hasil[0] = font.Size * ratio;
                hasil[1] = stringSize.Width;
                hasil[2] = stringSize.Height;
            }
            else
                for (int j = 10; j < 100; j++)
                {
                    Font newFont = new Font(font.FontFamily, j, font.Style);
                    SizeF stringSize = graphics.MeasureString(str, newFont,size.Width);
                    if (size.Height < stringSize.Height)
                        break;
                    hasil[0] = j-1;
                    hasil[1] = stringSize.Width;
                    hasil[2] = stringSize.Height;
                }
            return hasil;
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);
            base.OnClick(e);
        }
        
        protected override void OnEnabledChanged(EventArgs e)
        {
            Invalidate();
            base.OnEnabledChanged(e);
        }
        protected override void OnResize(EventArgs e)
        {
            Invalidate();
            base.OnResize(e);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            MouseEnterAct(this,e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            MouseLeaveAct(this,e);
        }
        
        protected override void OnMouseDown(MouseEventArgs e)
        {
            MouseDownAct(this,e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            MouseUpAct(this,e);
        }

        private void MouseClickAct(object sender, MouseEventArgs e)
        {
            base.OnClick(e);
        }

        private void MouseDownAct(object sender, MouseEventArgs e)
        {
            Capture = false;
            state = MouseState.Down;
            base.OnMouseDown(e);
            Invalidate();
        }

        private void MouseUpAct(object sender, MouseEventArgs e)
        {
            if (state != MouseState.Leave)
                state = MouseState.Enter;
            base.OnMouseUp(e);
            Invalidate();
        }

        private void MouseEnterAct(object sender, EventArgs e)
        {
            state = MouseState.Enter;
            base.OnMouseEnter(e);
            Invalidate();
        }

        private void MouseLeaveAct(object sender, EventArgs e)
        {
            state = MouseState.Leave;
            base.OnMouseLeave(e);
            Invalidate();
        }

        public enum EStyleButton
        {
            Save, Delete, Clear,Refresh,Custom
        }

        public EStyleButton PStyleButton
        {
            get { return _StyleButton; }
            set
            {
                _StyleButton = value;
                ChangeDefaultColor();
                Invalidate();
            }
        }

        public int PRadius
        {
            get
            {
                return radius;
            }
            set
            {
                radius = value;
                Invalidate();
            }
        }
        public Color PInactive1
        {
            get
            {
                return inactive1;
            }
            set
            {
                inactive1 = value;
                Invalidate();
            }
        }
        public Color PInactive2
        {
            get
            {
                return inactive2;
            }
            set
            {
                inactive2 = value;
                Invalidate();
            }
        }
        public Color PActive1
        {
            get
            {
                return active1;
            }
            set
            {
                active1 = value;
                Invalidate();
            }
        }
        public Color PActive2
        {
            get
            {
                return active2;
            }
            set
            {
                active2 = value;
                Invalidate();
            }
        }

        public float PGradientAngle
        {
            get
            {
                return gradientAngle;
            }
            set
            {
                gradientAngle = value;
                Invalidate();
            }
        }

        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
                Invalidate();
            }
        }

        public Image PIcon
        {
            get { return _Picon;} set { _Picon = value;Invalidate(); }
        }

        public bool PAutoResizeText
        {
            get
            {
                return _autoSizeFont;
            }
            set
            {
                _autoSizeFont = value;
                Invalidate();
            }
        }

        public bool PMultiLineText
        {
            get
            {
                return _multiLine;
            }
            set
            {
                _multiLine = value;
                Invalidate();
            }
        }

        public Position PIconPosition
        {
            get { return _iconPosition; }
            set { _iconPosition = value; Invalidate(); }
        }

        public ContentAlignment PTextAlignment
        {
            get { return _textAlign; }
            set { _textAlign = value;Invalidate();}
        }
        
        public override Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }
            set
            {
                base.ForeColor = value;
                Invalidate();
            }
        }

        public DialogResult DialogResult
        {
            get
            {
                return System.Windows.Forms.DialogResult.OK;
            }
            set
            {
            }
        }

        public void NotifyDefault(bool value)
        {
        }

        public void PerformClick()
        {
            OnClick(EventArgs.Empty);
        }
    }

    public enum Position
    {
        Top,
        Left,
        Right,
        Bottom,
        FollowTop,
        FollowLeft,
        FollowRight,
        FollowBottom,
    }
    public enum MouseState
    {
        Enter,
        Leave,
        Down,
        Up,
    }
}
