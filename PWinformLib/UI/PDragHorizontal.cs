using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PWinformLib.UI
{
    public partial class PDragHorizontal : UserControl
    {
        private bool dragable = false;
        private Size mouseOffset;
        Control ThisControl;
        public string PRightControl { get; set; }
        public string PLeftControl { get; set; }

        public PDragHorizontal()
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
            int heightMeasurement = this.Height / 2 - 15;
            pb_topBar.Height = heightMeasurement;
            pb_bottomBar.Height = heightMeasurement;
            pb_bottomBar.Location = new Point(pb_bottomBar.Location.X,heightMeasurement +31);
            Width = 20;
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
                // ThisControl.Top += newLocationOffset.Y;
                ThisControl.Left += newLocationOffset.X;
                if (PRightControl != null)
                {
                    Control rCtrl = getControl(PRightControl);
                    if(rCtrl!=null)
                    {
                        rCtrl.Left += newLocationOffset.X;
                        rCtrl.Width -= newLocationOffset.X;
                        rCtrl.Invalidate();
                    }
                }
                if (PLeftControl != null)
                {
                    Control lCtrl = getControl(PLeftControl);
                    if(lCtrl!=null)
                    {
                        lCtrl.Width += newLocationOffset.X;
                        lCtrl.Invalidate();
                    }
                }
            }
        }
    }

}
