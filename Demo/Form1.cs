using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Threading;
using PWinformLib.UI;
using System.Windows.Forms;
using PWinformLib;
using PWinformLib.Preloader;
using PWinformLib.Properties;
using PWinformLib.UI.modal;

namespace Demo
{
    public partial class Form1 : Form
    {

        DataTable dt;
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
        
        private void Form1_Shown(object sender, EventArgs e)
        {
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            Console.Out.WriteLine("DSdsfsdf");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pTextBoxAni1_Click(object sender, EventArgs e)
        {

        }

        private void pTextBoxAni21_Click(object sender, EventArgs e)
        {
            MessageBox.Show("mmmmmmmmmmmmmm");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Warn aaWarn = new Warn();
            aaWarn.Draggable = true;
            aaWarn.Title = "Errorrrrrr!!!!!!";
            aaWarn.Message = "Pesan sfsdf sdfdsfs sgdfdsfs gdsgsdfsdg fdsgfd sdgfhthd sdgh fdgsfa dh";
            aaWarn.OkText = "yahaa";
            aaWarn.BackgroundColor = Color.Aqua;
            aaWarn.StartPosition = FormStartPosition.CenterParent;
            aaWarn.ShowDialog();
            MessageBox.Show(aaWarn.Result + " - " + aaWarn.DialogResult);
        }

        private void pFlatButton1_Click(object sender, EventArgs e)
        {
            /*DataTable dt = ExcelHelper.GetDataTableFromExcel(@"C:\Users\bss\Desktop\a.xlsx");
            DataSet ds = ExcelHelper.GetDataSetFromExcel(@"C:\Users\bss\Desktop\a.xlsx");
            DataTable dt2 = ExcelHelper.GetDataTableFromExcel(@"C:\Users\bss\Desktop\a.xlsx",4,3);*/
            notification.Error("judul","pesan0",true);
        }

    }
}
