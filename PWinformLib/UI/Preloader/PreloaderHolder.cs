using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using PWinformLib.Properties;

namespace PWinformLib.UI.Preloader
{
    partial class PreloaderHolder : PictureBox
    {
        //Create a Bitmpap Object.
        Bitmap animatedImage = Resources.full_wave;
        bool currentlyAnimating = false;

        //This method begins the animation.
        public void AnimateImage()
        {
            if (!currentlyAnimating)
            {

                //Begin the animation only once.
                ImageAnimator.Animate(animatedImage, new EventHandler(this.OnFrameChanged));
                currentlyAnimating = true;
            }
        }

        private void OnFrameChanged(object o, EventArgs e)
        {

            //Force a call to the Paint event handler.
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {

            //Begin the animation.
            AnimateImage();

            //Get the next frame ready for rendering.
            ImageAnimator.UpdateFrames();

            //Draw the next frame in the animation.
            e.Graphics.DrawImage(Helper.ChangeOpacity(this.animatedImage, 0.1f), new Point(0, 0));
        }

        public PreloaderHolder()
        {
            InitializeComponent();
        }

        public PreloaderHolder(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

    }
}
