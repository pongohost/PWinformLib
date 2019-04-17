using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using PWinformLib.UI;
using System.Windows.Forms;
using PWinformLib;
using PWinformLib.Preloader;
using PWinformLib.Properties;

namespace Demo
{
    public partial class Form1 : Form
    {
        
        //===============================================================================
        private int i = 0;
        public Form1()
        {
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.DoubleBuffer, true);
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Preloaderani.addSpinnLoad(dataGridView1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //notification.Ok("JUDUL2 hahaha", "Tesss kkds\r\nkfsd sfksdksf sdfsdfsdf dsf sd fsd f sdf sdfdsfdsfsdf dsfdsfsdfsdfsdf dsfdsfdsfdsfdsf sdfdfsdfsfsf sdfsdfsfsfsd sdfdsfsdfsf");
            /*Form2 xx = new Form2();
            xx.Show();*/

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           /*HelperDGDTDS.AddDatePickerDGV(sender,e,1);
           SendKeys.SendWait("%{DOWN}");*/
        }

        private void pTextBox1_ButtonClick(object sender, EventArgs e)
        {
            MessageBox.Show("klik1");
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            //splitContainer1.BackgroundImage = Resources.Grip;

        }
        
        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            Helper.DrawBorder(e,(Control)sender,Color.Aqua,5,ButtonBorderStyle.Dashed);
        }

        private void pTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void pFlatButton1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(pTextBox1.Text);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Helper.DrawBorder(e, (Control)sender, Color.Red, 2, ButtonBorderStyle.Dashed);
        }
    }
}
