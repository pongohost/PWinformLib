using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System;
using PWinformLib.Properties;
using PWinformLib.UI.Preloader;

namespace PWinformLib.Preloader
{
    public class Preloaderani
    {
        public static void addSpinnLoad(Control pnl, String text = "", string tipe= "BoxSwap",int width=77,int height=77,Image imageRes=null)
        {
            if(tipe.Equals("SpinCircle"))
            {
                SpinningCircles spinningCircles = new SpinningCircles();
                spinningCircles.randColor = true;
                spinningCircles.Location = new Point(pnl.Width / 2 - 50, pnl.Height / 2 - 50);
                spinningCircles.Visible = true;
                pnl.Controls.Add(spinningCircles);
                spinningCircles.BringToFront();
            }
            /*else if(tipe.Contains("full-"))
            {
                PreloaderHolder itm = new PreloaderHolder();
                if (tipe.Contains("wave"))
                {
                    //                    itm.Image = Helper.ChangeOpacity(Resources.full_wave,0.5f);
                    itm.Image = Resources.full_wave;
                }
                itm.SizeMode = PictureBoxSizeMode.StretchImage;
                itm.Size = new Size(pnl.Width, pnl.Height);
                itm.Location = new Point(0, 0);
                itm.Visible = true;
                itm.BackColor = Color.Transparent;
                pnl.Controls.Add(itm);
                itm.BringToFront();
            }*/
            else 
            {
                PictureBox itm = new PictureBox();
                if(tipe.Equals("BoxSwap"))
                    itm.Image = Resources.Preloader_Ani;
                itm.SizeMode = PictureBoxSizeMode.StretchImage;
                itm.Size = new Size(width,height);
                itm.Location = new Point(pnl.Width / 2 - width/2, pnl.Height / 2 - height/2);
                itm.Visible = true;
                itm.BackColor = Color.Transparent;
                pnl.Controls.Add(itm);
                itm.BringToFront();
            }
            if (text.Length > 0)
            {
                Graphics graphics = pnl.CreateGraphics();
                Label txt = new Label
                {
                    AutoSize = true,
                    Name = "preloader_lbl",
                    BackColor = Color.FromArgb(158,210,102),
                    ForeColor = Color.Black,
                    Font = new Font("Verdana", 11.25F, (FontStyle.Regular | FontStyle.Italic), GraphicsUnit.Point, 0),
                    Padding = new Padding(5),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Text = text
                };
                Size size = TextRenderer.MeasureText(txt.Text, txt.Font);
                Console.Out.WriteLine($"{(size.Width / graphics.DpiX) * 72} - {txt.Width} | {size.Height} - {txt.Height}");
                int lebar = Convert.ToInt32((size.Width /graphics.DpiX) * 72);
                txt.Location = new Point(pnl.Width / 2 - size.Width / 2-5, pnl.Height / 2 + width/2);
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

                if (control is PictureBox)
                {
                    PictureBox itm = (PictureBox) control;
                    pnl.Controls.Remove(itm);
                    itm.Dispose();
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
