using System;
using System.Windows.Forms;

namespace PWinformLib.UI
{
    public class PDialog
    {
        public static DialogResult Ok(String Message,string Captcha="")
        {
            DialogResult result = DialogResult.No;
            using (var form = new PDialogUI("ok", Message, Captcha))
            {
                result = form.ShowDialog();
            }
            return result;
        }

        public static DialogResult Warn(String Message, string Captcha="")
        {
            DialogResult result = DialogResult.No;
            using (var form = new PDialogUI("warn", Message, Captcha))
            {
                result = form.ShowDialog();
            }
            return result;
        }

        public static DialogResult Error(String Message, string Captcha="")
        {
            DialogResult result = DialogResult.No;
            using (var form = new PDialogUI("error", Message, Captcha))
            {
                result = form.ShowDialog();
            }
            return result;
        }
    }
}
