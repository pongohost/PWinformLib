namespace PWinformLib.UI
{
    partial class PDragHorizontal
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
            this.pb_topBar = new System.Windows.Forms.PictureBox();
            this.pb_bottomBar = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_topBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_bottomBar)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = global::PWinformLib.Properties.Resources.barHorizontal;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox1.Location = new System.Drawing.Point(0, 10);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(20, 25);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // pb_topBar
            // 
            this.pb_topBar.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pb_topBar.BackgroundImage = global::PWinformLib.Properties.Resources.Grip;
            this.pb_topBar.Location = new System.Drawing.Point(5, 0);
            this.pb_topBar.Name = "pb_topBar";
            this.pb_topBar.Size = new System.Drawing.Size(10, 5);
            this.pb_topBar.TabIndex = 1;
            this.pb_topBar.TabStop = false;
            // 
            // pb_bottomBar
            // 
            this.pb_bottomBar.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.pb_bottomBar.BackgroundImage = global::PWinformLib.Properties.Resources.Grip;
            this.pb_bottomBar.Location = new System.Drawing.Point(5, 40);
            this.pb_bottomBar.Name = "pb_bottomBar";
            this.pb_bottomBar.Size = new System.Drawing.Size(10, 5);
            this.pb_bottomBar.TabIndex = 2;
            this.pb_bottomBar.TabStop = false;
            // 
            // PDragHorizontal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.pb_bottomBar);
            this.Controls.Add(this.pb_topBar);
            this.Controls.Add(this.pictureBox1);
            this.Name = "PDragHorizontal";
            this.Size = new System.Drawing.Size(20, 45);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_topBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_bottomBar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pb_topBar;
        private System.Windows.Forms.PictureBox pb_bottomBar;
    }
}
