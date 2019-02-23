using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System;

namespace PWinformLib.Preloader
{
    public class Preloaderani
    {
        public static void addSpinnLoad(Control pnl,String text="")
        {
            SpinningCircles spinningCircles = new SpinningCircles();
            spinningCircles.randColor = true;
            spinningCircles.Location = new Point(pnl.Width / 2 - 50, pnl.Height / 2 - 50);
            spinningCircles.Visible = true;
            pnl.Controls.Add(spinningCircles);
            spinningCircles.BringToFront();
            if (text.Length > 0)
            {
                Graphics graphics = pnl.CreateGraphics();
                Label txt = new Label
                {
                    AutoSize = true,
                    Name = "preloader_lbl",
                    BackColor = Color.FromArgb(158,210,102),
                    ForeColor = Color.Black,
                    //Font = new Font("Verdana", 11.25F, (FontStyle.Bold | FontStyle.Italic), GraphicsUnit.Point, 0),
                    Padding = new Padding(5),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Text = text
                };
                Size size = TextRenderer.MeasureText(txt.Text, txt.Font);
                int lebar = Convert.ToInt32((size.Width /graphics.DpiX) * 72);
                txt.Location = new Point(pnl.Width / 2 - lebar, pnl.Height / 2 + 60);
                pnl.Controls.Add(txt);
            }
        }

        public static void updateText(Control pnl,String text)
        {
            var controlLst = Helper.GetAllControl(pnl).ToList();
            foreach (var control in controlLst)
            {
                if (control is Label)
                {
                    Label tdr = (Label)control;
                    tdr.Text = text;
                }
            }
        }

        public static void remSpinnLoad(Control pnl)
        {
            var controlLst = Helper.GetAllControl(pnl).ToList();
            foreach (var control in controlLst)
            {
                if (control is SpinningCircles)
                {
                    SpinningCircles tdr = ((SpinningCircles)control);
                    pnl.Controls.Remove(tdr);
                    tdr.Dispose();
                }
                if (control is Label)
                {
                    Label tdr = (Label)control;
                    if (tdr.Name.Equals("preloader_lbl"))
                    {
                        pnl.Controls.Remove(tdr);
                        tdr.Dispose();
                    }
                }
            }
        }
    }
}
