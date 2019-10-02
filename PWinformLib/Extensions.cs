using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PWinformLib
{
    public static class Extensions
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int PostMessage(IntPtr hwnd, Int32 wMsg, Int32 wParam, Int32 lParam);

        public static void Open(this Control obj)
        {
            const int WM_LBUTTONDOWN = 0x0201;
            int width = obj.Width - 10;
            int height = obj.Height / 2;
            int lParam = width + height * 0x00010000; // VooDoo to shift height
            PostMessage(obj.Handle, WM_LBUTTONDOWN, 1, lParam);
        }

        /*public static void Open(this DataGridViewCell obj)
        {
            const int WM_LBUTTONDOWN = 0x0201;
            Rectangle rec = obj.GetContentBounds(obj.RowIndex);
            int width = rec.Width - 10;
            int height = rec.Height / 2;
            int lParam = width + height * 0x00010000; // VooDoo to shift height
            PostMessage(obj.DataGridView.Handle, WM_LBUTTONDOWN, 1, lParam);
        }*/

        public static void InvokeEx<T>(this T @this, Action<T> action) where T : ISynchronizeInvoke
        {
            if (@this.InvokeRequired)
            {
                @this.Invoke(action, new object[] { @this });
            }
            else
            {
                action(@this);
            }
        }
    }
}
