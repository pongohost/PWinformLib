using System;
using System.Drawing;
using System.Windows.Forms;

namespace PWinformLib.UI
{
    partial class PDialogUI : Form
    {
        private string kode;
        public PDialogUI(string jenis,string pesan,string kodeCaptcha="")
        {
            InitializeComponent();
            if (jenis.ToLower().Equals("error"))
            {
                BackColor = Color.FromArgb(255, 192, 192);
            }

            if (jenis.ToLower().Equals("warn"))
            {
                BackColor = Color.FromArgb(255, 255, 128);
            }

            if (jenis.ToLower().Equals("ok"))
            {
                BackColor = Color.FromArgb(192, 255, 192);
            }

            if (kodeCaptcha.Length > 0)
            {
                txt_input.Visible = true;
                kode = kodeCaptcha;
                txt_input.Watermark = $"Ketik \"{kodeCaptcha}\" untuk menyetujui";
            }
            lbl_pesan.Text = pesan;
            lbl_imbang.Size = new Size(440 - lbl_pesan.Width,1);
            Size = new Size(450,flowLayoutPanel1.Height+5);
            Opacity = 0.9D;
        }

        private void pFlatButton2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            if (txt_input.Text.Equals(kode) || kode == null)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                notification.Error("Kode Salah","Kode yang anda inputkan tidak sama");
            }
        }

        private void flowLayoutPanel1_Click(object sender, EventArgs e)
        {
            lbl_pesan.Focus();
        }
    }
}
