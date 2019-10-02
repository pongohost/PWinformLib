using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PWinformLib.intpkg;

namespace PWinformLib
{
    class FormValidation : ifacepkg
    {
        private Control _ctrl;
        private ToolTip _toolTip;
        FormValidation(Control ctrl,ToolTip toolTip)
        {
            _ctrl = ctrl;
            _toolTip = toolTip;
            _toolTip.ToolTipTitle = "";
        }

        public bool isEmpty()
        {
            return MinLength(1);
        }

        public bool MinLength(int length)
        {
            bool result = true;
            if (_ctrl is TextBox)
            {

            }
            return result;
        }
    }
}
