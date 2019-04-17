namespace PWinformLib.UI
{
    partial class PDragVertical
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pb_leftBar = new System.Windows.Forms.PictureBox();
            this.pb_rightBar = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_leftBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_rightBar)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = global::PWinformLib.Properties.Resources.barVertical;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox1.Location = new System.Drawing.Point(12, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(25, 20);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // pb_leftBar
            // 
            this.pb_leftBar.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pb_leftBar.BackgroundImage = global::PWinformLib.Properties.Resources.Grip;
            this.pb_leftBar.Location = new System.Drawing.Point(0, 5);
            this.pb_leftBar.Name = "pb_leftBar";
            this.pb_leftBar.Size = new System.Drawing.Size(5, 10);
            this.pb_leftBar.TabIndex = 1;
            this.pb_leftBar.TabStop = false;
            // 
            // pb_rightBar
            // 
            this.pb_rightBar.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.pb_rightBar.BackgroundImage = global::PWinformLib.Properties.Resources.Grip;
            this.pb_rightBar.Location = new System.Drawing.Point(42, 5);
            this.pb_rightBar.Name = "pb_rightBar";
            this.pb_rightBar.Size = new System.Drawing.Size(5, 10);
            this.pb_rightBar.TabIndex = 2;
            this.pb_rightBar.TabStop = false;
            // 
            // PDragVertical
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.pb_rightBar);
            this.Controls.Add(this.pb_leftBar);
            this.Controls.Add(this.pictureBox1);
            this.Name = "PDragVertical";
            this.Size = new System.Drawing.Size(45, 25);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_leftBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_rightBar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pb_leftBar;
        private System.Windows.Forms.PictureBox pb_rightBar;
    }
}
