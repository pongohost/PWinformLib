﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PWinformLib.UI
{
    public partial class TransparentTextBox : TextBox
    {
        public TransparentTextBox()
        {
            InitializeComponent();
            SetStyle(ControlStyles.SupportsTransparentBackColor |
                     ControlStyles.DoubleBuffer |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.UserPaint, true);
        }

        private AutoScaleMode _autoscaleMode = AutoScaleMode.None;

        public AutoScaleMode AutoScaleMode

        {

            get

            {

                return _autoscaleMode;

            }

            set

            {

                _autoscaleMode = value;

            }

        }
    }
}
