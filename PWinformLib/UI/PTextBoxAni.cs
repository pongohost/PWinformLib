using System;
using System.Drawing;
using System.Windows.Forms;

namespace PWinformLib.UI
{
    public partial class PTextBoxAni : Control
    {
        TextBox box = new TextBox();
        Label boxShow = new Label();
        PictureBox pictBox = new PictureBox();
        Panel pnlActive = new Panel();
        Panel pnlInactive = new Panel();
        private readonly PTimer _timer;
        private Font _font;
        private bool onDesign = true;
        private bool isEnter = false;
        private int target;
        private int _boxSize;
        private int _incWidth;
        private Color _PBorderClrInactive;
        private Color _PBorderClrActive;
        private int _PBorderLine;
        private Image _PIconImage;
        private int hitung=0;

        public PTextBoxAni()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.UserPaint, true);

            PDurationAnimation = 500;
            Font = new Font("Verdana",12);
            PBorderLine = 1;
            PBorderClrInactive = Color.Gray;
            PBorderClrActive = Color.Red;

            pictBox.Parent = this;
            pictBox.BackgroundImageLayout = ImageLayout.Stretch;

            box.Parent = this;
            box.BorderStyle = BorderStyle.None;
            boxShow.Parent = this;
            boxShow.AutoSize = false;
            boxShow.BorderStyle = BorderStyle.None;

            pnlActive.Parent = this;
            pnlInactive.Parent = this;
            
            _timer = new PTimer(_tick,FPSLimiterKnownValues.LimitSixty);
            _timer.Start();

            box.Enter += new EventHandler(box_Enter);
            box.Leave += new EventHandler(box_Leave);
            box.TextChanged += new EventHandler(box_TextChange);
            boxShow.Click += new EventHandler(boxShow_Click);
        }

        private void _tick(ulong millSinceBeginning = 0)
        {
            if (isEnter)
            {
                if (pictBox.Location.Y >= 2)
                {
                    pictBox.InvokeEx(f => pictBox.Location = new Point(2, pictBox.Location.Y - 1));
                    if (pictBox.Width>=_boxSize*2/3)
                        pictBox.BeginInvoke(new MethodInvoker(() => pictBox.Size = new Size(pictBox.Width - 1, pictBox.Height - 1)));
                }
                if(pnlActive.Width<Width)
                    pnlActive.InvokeEx(panel => pnlActive.Width = pnlActive.Width + _incWidth);
                else _timer.Stop();
                
            }
            else if (box.Text.Length < 1)
            {
                if (pictBox.Location.Y <= target)
                {
                    pictBox.InvokeEx(pictureBox => pictBox.Location = new Point(2, pictBox.Location.Y + 1));
                    if (pictBox.Width <= _boxSize)
                        pictBox.InvokeEx(pictureBox => pictBox.Size = new Size(pictBox.Width + 1, pictBox.Height + 1));
                }
                else
                    pictBox.InvokeEx(pictureBox => pictBox.Location = new Point(2, target));
                if (pnlActive.Width > 0)
                    pnlActive.InvokeEx(panel => pnlActive.Width = pnlActive.Width - _incWidth);
                else _timer.Stop();
            }
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            pnlActive.BackColor = _PBorderClrActive;
            pnlInactive.BackColor = _PBorderClrInactive;
            if (onDesign)
            {
                if (_PIconImage != null)
                    pictBox.BackgroundImage = _PIconImage;

                _boxSize = (int)(boxShow.Height * 0.75);
                Size = new Size(Width, (int)_boxSize + boxShow.Height + _PBorderLine);

                box.Location = new Point(0, (int)_boxSize);
                box.Width = 0;
                boxShow.Location = new Point(0, (int)_boxSize);
                boxShow.Width = Width;

                target = box.Location.Y + box.Height / 2 - _boxSize / 2;
                pictBox.Size = new Size(_boxSize, _boxSize);
                pictBox.Location = new Point(2, target);

                pnlInactive.Location = new Point(0, Height - _PBorderLine);
                pnlInactive.Size = new Size(Width, _PBorderLine);

                pnlActive.Location = new Point(0, Height - _PBorderLine);
                pnlActive.Size = new Size(0, _PBorderLine);

                double interVal = Math.Ceiling(PDurationAnimation / 60F);
                _incWidth = (int)Math.Ceiling(Width / interVal);
            }
        }
        
        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            box.Font = Font;
            Invalidate();
        }
        
        private void box_Enter(object sender, EventArgs e)
        {
            isEnter = true;
            hitung = 0;
            _timer.Start();
            onDesign = false;
        }

        private void box_Leave(object sender, EventArgs e)
        {
            isEnter = false;
            hitung = 0;
            _timer.Start();
            onDesign = false;
        }

        private void box_TextChange(object sender, EventArgs e)
        {
            if (PPassChar != '\0')
            {
                string teks = "";
                for (int i = 0; i < box.TextLength; i++)
                {
                    teks += PPassChar;
                }

                boxShow.Text = teks;
            }
            else
                boxShow.Text = box.Text;
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            box.Select();
        }
        private void boxShow_Click(object sender, EventArgs e)
        {
            box.Select();
        }

        public int PBorderLine
        {
            get { return _PBorderLine; }
            set { _PBorderLine=value;Invalidate(); }
        }

        public Image PIconImage
        {
            get { return _PIconImage; }
            set { _PIconImage = value;Invalidate(); }
        }

        public Color PBorderClrInactive
        {
            get { return _PBorderClrInactive; }
            set { _PBorderClrInactive = value;Invalidate(); }
        }
        
        public Color PBorderClrActive
        {
            get { return _PBorderClrActive; }
            set { _PBorderClrActive = value;Invalidate(); }
        }

        public int PDurationAnimation { get; set; }

        public char PPassChar { get; set; }

        public override string Text
        {
            get { return box.Text; }
            set { box.Text =value;Invalidate(); }
        }
    }
}
