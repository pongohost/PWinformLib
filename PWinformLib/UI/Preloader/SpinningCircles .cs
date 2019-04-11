﻿using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace PWinformLib.Preloader
{
    class SpinningCircles : Control
    {
        int increment = 1;
        int radius = 4;
        int n = 8;
        int next = 0;
        Timer timer;
        public bool randColor = false;
        public SpinningCircles()
        {
            timer = new Timer();
            this.Size = new Size(110, 110);
            timer.Tick += (s, e) => this.Invalidate();
            if (!DesignMode)
                timer.Enabled = true;
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw | ControlStyles.UserPaint |
                     ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            if (Parent != null && this.BackColor == Color.Transparent)
            {
                using (var bmp = new Bitmap(Parent.Width, Parent.Height))
                {
                    Parent.Controls.Cast<Control>()
                          .Where(c => Parent.Controls.GetChildIndex(c) > Parent.Controls.GetChildIndex(this))
                          .Where(c => c.Bounds.IntersectsWith(this.Bounds))
                          .OrderByDescending(c => Parent.Controls.GetChildIndex(c))
                          .ToList()
                          .ForEach(c => c.DrawToBitmap(bmp, c.Bounds));

                    e.Graphics.DrawImage(bmp, -Left, -Top);
                }
            }
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            int length = Math.Min(Width, Height);
            PointF center = new PointF(length / 2, length / 2);
            int bigRadius = length / 2 - radius - (n - 1) * increment;
            float unitAngle = 360 / n;
            if (!DesignMode)
                next++;
            next = next >= n ? 0 : next;
            int a = 0;
            for (int i = next; i < next + n; i++)
            {
                int factor = i % n;
                float c1X = center.X + (float)(bigRadius * Math.Cos(unitAngle * factor * Math.PI / 180));
                float c1Y = center.Y + (float)(bigRadius * Math.Sin(unitAngle * factor * Math.PI / 180));
                int currRad = radius + a * increment;
                PointF c1 = new PointF(c1X - currRad, c1Y - currRad);
                if(randColor)
                    e.Graphics.FillEllipse(PickBrush(), c1.X, c1.Y, 2 * currRad, 2 * currRad);
                else
                    e.Graphics.FillEllipse(Brushes.Black, c1.X, c1.Y, 2 * currRad, 2 * currRad);
                using (Pen pen = new Pen(Color.White, 2))
                    e.Graphics.DrawEllipse(pen, c1.X, c1.Y, 2 * currRad, 2 * currRad);
                a++;
            }
        }
        protected override void OnVisibleChanged(EventArgs e)
        {
            timer.Enabled = Visible;
            base.OnVisibleChanged(e);
        }

        private Brush PickBrush()
        {
            Brush result = Brushes.Transparent;

            Random rnd = new Random();

            Type brushesType = typeof(Brushes);

            PropertyInfo[] properties = brushesType.GetProperties();

            int random = rnd.Next(properties.Length);
            result = (Brush)properties[random].GetValue(null, null);

            return result;
        }
    }
}
