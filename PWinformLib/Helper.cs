using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Data;
using Newtonsoft.Json;
using System.Net;
using System.Net.Sockets;
using System.Drawing.Text;
using System.Linq;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using PWinformLib.Preloader;
using PWinformLib.UI;

namespace PWinformLib
{
    public class Helper
    {
        /// <summary>
        /// Get amount overlap duration
        /// </summary>
        /// <param name="firstStartTime">First Start Time.</param>
        /// <param name="firstEndTime">First End Time.</param>
        /// <param name="secondStarTime">Second Start Time.</param>
        /// <param name="secondEndTime">Second End Time.</param>
        /// <param name="unit">Return unit (D,h,m,s,n).</param>
        /// <returns>Returns overlap value in unit.</returns>
        public static Double GetOverlapDuration(DateTime firstStartTime, DateTime firstEndTime, 
            DateTime secondStarTime, DateTime secondEndTime, String unit)
        {
            Double result = 0;
            DateTime start, end;
            if (firstStartTime <= secondEndTime && firstEndTime >= secondStarTime)
            {
                if (firstStartTime >= secondStarTime) start = firstStartTime;
                else start = secondStarTime;
                if (firstEndTime >= secondEndTime) end = secondEndTime;
                else end = firstEndTime;
                TimeSpan ts = end.Subtract(start);
                if (unit.Equals("D")) result = ts.TotalDays;
                if (unit.Equals("h")) result = ts.TotalHours;
                if (unit.Equals("m")) result = ts.TotalMinutes;
                if (unit.Equals("s")) result = ts.TotalSeconds;
                if (unit.Equals("n")) result = ts.TotalMilliseconds;
            }
            return result;
        }


        /// <summary>
        /// Convert Object To DataTable
        /// </summary>
        /// <param name="obj">Object want to convert.</param>
        /// <returns>Returns DataTable.</returns>
        public static DataTable ToDataTable<T>(IEnumerable<T> obj)
        {
            var properties = typeof(T).GetProperties();

            var dataTable = new DataTable();
            foreach (var info in properties)
                dataTable.Columns.Add(info.Name, Nullable.GetUnderlyingType(info.PropertyType)
                                                 ?? info.PropertyType);

            foreach (var entity in obj)
                dataTable.Rows.Add(properties.Select(p => p.GetValue(entity)).ToArray());

            return dataTable;
        }

        /// <summary>
        /// Get String Height
        /// </summary>
        /// <param name="Text">Checked Text Value.</param>
        /// <param name="Width">Width of control</param>
        /// <returns>Returns Text Height.</returns>
        public static float getTextHeight(String Text, int Width,Font font)
        {
            /*SizeF stringSize = new SizeF();
            PictureBox pictureBox1 = new PictureBox();
            pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(pictureBox1_Paint);
            void pictureBox1_Paint(object sender, PaintEventArgs e)
            {
                stringSize = e.Graphics.MeasureString(Text, font,Width);
            }
            pictureBox1.DrawToBitmap();
            return stringSize.Height;*/
            Bitmap b = new Bitmap(1000, 1000);
            Graphics g = Graphics.FromImage(b);
            SizeF sizeOfString = new SizeF();
            sizeOfString = g.MeasureString(Text, font,Width);
            return sizeOfString.Height;
        }

        /// <summary>
        /// Check if time is in time range
        /// </summary>
        /// <param name="checkedTime">Checked Time Value.</param>
        /// <param name="startTime">Start Time Range.</param>
        /// <param name="endTime">End Time Range.</param>
        /// <returns>Returns true chekedTime is in Range.</returns>
        public static bool isInPeriode(DateTime checkedTime,DateTime startTime,DateTime endTime)
        {
            if (checkedTime >= startTime && checkedTime <= endTime) return true;
            return false;
        }

        public static bool addSingleEmptyArray(String[] arr, String nilai, String judul, String pesan)
        {
            int tempint = 999;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] != null)
                {
                    if (arr[i].Equals(nilai))
                    {
                        tempint = 999;
                        break;
                    }
                }
                else
                {
                    if (tempint == 999)
                        tempint = i;
                }
            }
            if (tempint != 999)
            {
                arr[tempint] = nilai;
                return true;
            }
            else
            {
                notification.Warn(judul, pesan);
                return false;
            }
        }

        public static bool delSingleEmptyArray(String[] arr, String nilai, String judul, String pesan)
        {
            int tempint = 999;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] != null)
                {
                    if (arr[i].Equals(nilai))
                    {
                        arr[i] = null;
                        tempint = i;
                        break;
                    }
                }
            }
            if (tempint != 999)
            {
                return true;
            }
            else
            {
                notification.Warn(judul, pesan);
                return false;
            }
        }

        public static void clrSingleEmptyArray(String[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = null;
            }
        }

        public static void addComboItem(ComboBox cb, DataTable dt)
        {
            cb.DataSource = null;
            Dictionary<string, string> row = new Dictionary<string, string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                row.Add(dt.Rows[i][1].ToString(), dt.Rows[i][0].ToString());
            }
            cb.DataSource = new BindingSource(row, null);
            cb.DisplayMember = "Value";
            cb.ValueMember = "Key";
        }

        public static void addComboItemNew(ComboBox cb, DataTable dt)
        {
            cb.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ComboboxItem item = new ComboboxItem();
                item.Text = dt.Rows[i][1].ToString();
                item.Value = dt.Rows[i][0].ToString();
                cb.Items.Add(item);
            }
        }

        // Format Kolom 1 = value,kolom 2 display
        public static void addComboItem(ComboBox cb, DataSet ds)
        {
            cb.DataSource = null;
            String[] namakolom = new String[2];
            DataTable tbl = ds.Tables[0];
            int i = 0;
            foreach (DataColumn kolom in tbl.Columns)
            {
                namakolom[i] = kolom.ColumnName;
                i++;
            }
            cb.DataSource = tbl;
            cb.ValueMember = namakolom[0];
            cb.DisplayMember = namakolom[1];

        }

        public static void setDatePickerMaxMinValue(DateTimePicker dp,DateTime min,DateTime max)
        {
            dp.MaxDate = new DateTime(9997, 1, 1);
            dp.MinDate = min;
            dp.MaxDate = max;
        }

        /// <summary>
        /// Membandingkan waktu antara datepicker.
        /// </summary>
        /// <param name="bawah">Waktu paling kecil.</param>
        /// <param name="atas">Waktu paling besar.</param>
        /// <returns>Returns true jika atas lebih besar dari bawah.</returns>
        public static bool compareDatePicker(DateTimePicker bawah,DateTimePicker atas)
        {
            if (DateTime.Parse(bawah.Value.ToString()) < DateTime.Parse(atas.Value.ToString()))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Mendapatkan list control pada layout.
        /// </summary>
        /// <param name="root">Nama panel yang d attach.</param>
        /// <returns>Returns IEnumerable.</returns>
        public static IEnumerable<Control> GetAllControl(Control root)
        {
            var queue = new Queue<Control>();
            queue.Enqueue(root);
            do
            {
                var control = queue.Dequeue();
                yield return control;
                foreach (var child in control.Controls.OfType<Control>())
                    queue.Enqueue(child);
            } while (queue.Count > 0);
        }

        /// <summary>
        /// Mendapatkan current working dir.
        /// </summary>
        /// <returns>Returns String.</returns>
        public static String Cwd()
        {
            string executable = System.Reflection.Assembly.GetExecutingAssembly().Location;
            return System.IO.Path.GetDirectoryName(executable);
        }

        /// <summary>
        /// Mendapatkan IP address Lokal.
        /// </summary>
        public static String[] GetLocalIPAddress()
        {
            String[] alamat = new string[2];
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    alamat[0] = ip.ToString();
                    alamat[1] = host.HostName;
                    return alamat;
                    //return ip.ToString()+"|"+host.HostName;
                }
            }
            throw new Exception("Local IP Address Not Found!");
        }

        /// <summary>
        /// Menghapus semua Control yang ada dalam control.
        /// </summary>
        /// <param name="layout">Nama layout yang akan di bersihkan.</param>
        public static void clearLayout(Control layout)
        {
            List<Control> listControls = new List<Control>();
            foreach (Control control in layout.Controls)
            {
                listControls.Add(control);
            }

            foreach (Control control in listControls)
            {
                layout.Controls.Remove(control);
                control.Dispose();
            }
            layout.Controls.Clear();
        }

        /// <summary>
        /// Menambah AddSlash pada String.
        /// </summary>
        /// <param name="Input">Nama string yang di addslash.</param>
        public static String AddSlash(String Input)
        {
            String hasil = "";
            try
            {
                hasil = System.Text.RegularExpressions.Regex.Replace(Input, @"[\000\010\011\012\015\032\042\134\140]", "\\$0");
                hasil = System.Text.RegularExpressions.Regex.Replace(hasil, @"[\047]", "'$0");
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
            }
            return hasil;
        }

        /// <summary>
        /// Stripslash String.
        /// </summary>
        /// <param name="Input">Nama String yang di stripslash.</param>
        public static String StripSlash(String Input)
        {
            String hasil = "";
            try
            {
                hasil = System.Text.RegularExpressions.Regex.Replace(Input, @"(\\)([\000\010\011\012\015\032\042\047\134\140])", "$2");
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
            }
            return hasil;
        }

        /// <summary>
        /// Make Image Transparent.
        /// </summary>
        /// <param name="img">Image Source.</param>
        /// <param name="opacityvalue">Opacity Value.</param>
        public static Bitmap ChangeOpacity(Image img, float opacityvalue)
        {
            Bitmap bmp = new Bitmap(img.Width, img.Height); // Determining Width and Height of Source Image
            Graphics graphics = Graphics.FromImage(bmp);
            ColorMatrix colormatrix = new ColorMatrix();
            colormatrix.Matrix33 = opacityvalue;
            ImageAttributes imgAttribute = new ImageAttributes();
            imgAttribute.SetColorMatrix(colormatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            graphics.DrawImage(img, new Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, imgAttribute);
            graphics.Dispose();   // Releasing all resource used by graphics 
            return bmp;
        }

        /// <summary>
        /// Mencek nama kolom error pada dataset.
        /// </summary>
        /// <param name="ds">Nama dataset yang di attach.</param>
        public static Boolean cekError(DataSet ds)
        {
            if (ds.Tables[0].Columns[0].ColumnName.Equals("ERROR"))
            {
                notification.Error("Oops!There was a problem..", ds.Tables[0].Rows[0][0].ToString());
                return false;
            } else
            {
                return true;
            }
        }

        /// <summary>
        /// Menambah Preloader yang di pasang pada komponen.
        /// </summary>
        /// <param name="pnl">Nama panel yang d attach.</param>
        public static void addSpinnLoad(Control pnl)
        {
            SpinningCircles spinningCircles = new SpinningCircles();
            spinningCircles.randColor = true;
            spinningCircles.Location = new Point(pnl.Width/2-50, pnl.Height/2-50);
            spinningCircles.Visible = true;
            pnl.Controls.Add(spinningCircles);
            spinningCircles.BringToFront();
        }

        /// <summary>
        /// Menghapus Preloader yang di pasang pada komponen.
        /// </summary>
        /// <param name="pnl">Nama panel yang d attach.</param>
        public static void remSpinnLoad(Control pnl)
        {
            var controlLst = GetAllControl(pnl).ToList();
            foreach (var control in controlLst)
            {
                if (control is SpinningCircles)
                {
                    SpinningCircles tdr = ((SpinningCircles)control);
                    pnl.Controls.Remove(tdr);
                    tdr.Dispose();
                }
            }
        }

        /// <summary>
        /// Mengcopy file image ke clipboard.
        /// </summary>
        /// <param name="path">Nama panel yang d attach.</param>
        public static void ImgToClipBoard(String path)
        {
            Bitmap img = new Bitmap(path);
            Clipboard.SetImage(img);
        }

        //e.Control.KeyPress -= new KeyPressEventHandler(text_OnlyNumberEvent);
        public static void text_OnlyNumberEvent(object sender, KeyPressEventArgs e)
        {            
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && !e.KeyChar.ToString().Equals("."))
            {
                e.Handled = true;
            }
        }
        
        public static void text_OnlySelectedChar(object sender, KeyPressEventArgs e, string[] daftar)
        {
            Boolean ok = true;
            foreach(String a in daftar)
            {
                if (e.KeyChar.ToString().Equals(a))
                {
                    ok = false;
                    break;
                }
            }
            if (ok)
                e.Handled = true;
        }

        //private async void txt_StdProyLoader_TextChanged(object sender, EventArgs e)
        //{
        //    TextBox txt = (TextBox)sender;
        //    if (await bantu.UserKeepsTyping(txt,1000)) aksi;
        //}
        public async static Task<bool> UserKeepsTyping(TextBox txtBox,int durasi =500)
        {
            string txt = txtBox.Text;   // remember text
            await Task.Delay(durasi);        // wait some
            return txt != txtBox.Text;  // return that text chaged or not
        }
    }

    class ComboboxItem
    {
        public string Text { get; set; }
        public string Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}
