using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PWinformLib.UI
{
    public partial class PDragVertical : UserControl
    {
        private bool dragable = false;
        private Size mouseOffset;
        Control ThisControl;
        public string PBottomControl { get; set; }
        public string PTopControl { get; set; }

        public PDragVertical()
        {
            InitializeComponent();
            var itemCtrl = Helper.GetAllControl(this).ToList();
            foreach (Control ctrl in itemCtrl)
            {
                ctrl.MouseDown += new MouseEventHandler(control_MouseDown);
                ctrl.MouseMove += new MouseEventHandler(control_MouseMove);
                ctrl.MouseUp += new MouseEventHandler(control_MouseUp);
            }
            Cursor = Cursors.Hand;
            ThisControl = this;
        }
        
        private Control getControl(string ctrlName)
        {
            foreach (Control ctrl in Parent.Controls)
            {
                if (ctrl.Name.Equals(ctrlName))
                    return ctrl;
            }
            return null;
        }

        //============================== Part Event =====================================
        protected override void OnResize(EventArgs e)
        {
            int widthMeasurement = this.Width / 2 - 15;
            pb_leftBar.Width = widthMeasurement;
            pb_rightBar.Width = widthMeasurement;
            pb_rightBar.Location = new Point(widthMeasurement + 32, pb_rightBar.Location.Y);
            Height = 20;
            base.OnResize(e);
        }

        void control_MouseDown(object sender, MouseEventArgs e)
        {
            mouseOffset = new Size(e.Location);
            dragable = true;
        }

        void control_MouseUp(object sender, MouseEventArgs e)
        {
            dragable = false;
        }

        void control_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragable)
            {
                Point newLocationOffset = e.Location - mouseOffset;
                ThisControl.Top += newLocationOffset.Y;
                //ThisControl.Left += newLocationOffset.X;
                if (PBottomControl != null)
                {
                    Control bCtrl = getControl(PBottomControl);
                    if(bCtrl!=null)
                    {
                        bCtrl.Top += newLocationOffset.Y;
                        bCtrl.Height -= newLocationOffset.Y;
                        bCtrl.Invalidate();
                    }
                }
                if (PTopControl != null)
                {
                    Control tCtrl = getControl(PTopControl);
                    if(tCtrl!=null)
                    {
                        tCtrl.Height += newLocationOffset.Y;
                        tCtrl.Invalidate();
                    }
                }
            }
        }
    }

}
