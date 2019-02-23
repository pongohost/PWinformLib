using System;
using System.Drawing;
using System.Windows.Forms;

namespace PWinformLib
{
    public partial class PTextBox : TextBox
    {
        /// <summary>
        /// The cue banner text.
        /// </summary>
        private string _cueText;
        private Font _wfont;
        private TextFormatFlags _txtF;
        private AlignWm _alignWm;

        public PTextBox()
        {
            InitializeComponent();
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        }
        public enum AlignWm
        {
            Kiri,Kanan,Tengah
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
        /// Gets or sets the cue banner text.
        /// </summary>
        public string Watermark
        {
            get
            {
                return _cueText;
            }

            set
            {
                _cueText = value;
                Invalidate();
            }
        }
        /// <summary>
        /// Gets or sets the cue banner text.
        /// </summary>
        public AlignWm WatermarkTextAlign
        {
            get
            {
                return _alignWm;
            }

            set
            {
                _alignWm = value;
                Invalidate();
            }
        }

        private TextFormatFlags textFormat()
        {
            TextFormatFlags tf = TextFormatFlags.Default;
            switch (_alignWm)
            {
                case AlignWm.Kiri:
                    tf = TextFormatFlags.Left | TextFormatFlags.VerticalCenter;
                    break;
                case AlignWm.Kanan:
                    tf = TextFormatFlags.Right | TextFormatFlags.VerticalCenter;
                    break;
                case AlignWm.Tengah:
                    tf = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter;
                    break;
            }
            return tf;
        }

        /// <summary>
        /// Processes Windows messages.
        /// </summary>
        /// <param name="m">A Windows Message object.</param>
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            const int WM_PAINT = 0xF;
            if (m.Msg == WM_PAINT)
            {
                if (!Focused && String.IsNullOrEmpty(Text) && !String.IsNullOrEmpty(Watermark))
                {
                    using (var graphics = CreateGraphics())
                    {
                        TextRenderer.DrawText(
                            dc: graphics,
                            text: Watermark,
                            font: WatermarkFont,
                            bounds: ClientRectangle,
                            foreColor: Color.DarkGray,
                            backColor: Enabled ? BackColor : Color.Transparent,
                            //backColor: this.BackColor,
                            flags: textFormat());
                    }
                }
            }
        }
    }
}
