using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using PWinformLib.UI;
using System.Windows.Forms;
using PWinformLib;

namespace Demo
{
    public partial class Form1 : Form
    {
        private static List<PopupNotifier> aaList = new List<PopupNotifier>();
        private int i = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            notification.Error("JUDUL1 hahaha","Tesss kkdskfsd sfksdksf");
            //label2.Font = Helper.CustomFont("Lato-Light", 16);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            notification.Ok("JUDUL2 hahaha", "Tesss kkds\r\nkfsd sfksdksf sdfsdfsdf dsf sd fsd f sdf sdfdsfdsfsdf dsfdsfsdfsdfsdf dsfdsfdsfdsfdsf sdfdfsdfsfsf sdfsdfsfsfsd sdfdsfsdfsf");
            /*Form2 xx = new Form2();
            xx.Show();*/

        }
    }
}
