using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PWinformLib.UI.modal
{
    public partial class Warn : Form
    {
        public bool Draggable = false;
        public string Title = "";
        public string Message="";
        public string OkText = "Ya";
        public string CancelText = "Tidak";
        public string Result = "";
        public Color BackgroundColor = Color.White;

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;
        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        private static extern bool ReleaseCapture();

        public Warn()
        {
            InitializeComponent();
        }

        private void DraggControl(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && Draggable)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void Warn_Shown(object sender, EventArgs e)
        {
            lbl_title.Text = Title;
            lbl_msg.Text = Message;
            btn_ya.Text = OkText;
            btn_tidak.Text = CancelText;
            BackColor = BackgroundColor;
        }

        private void btn_ya_Click(object sender, EventArgs e)
        {
            Result = btn_ya.Text;
            DialogResult = DialogResult.Yes;
            Close();
        }

        private void btn_tidak_Click(object sender, EventArgs e)
        {
            Result = btn_tidak.Text;
            DialogResult = DialogResult.No;
            Close();
        }
    }
}
