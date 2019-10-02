namespace PWinformLib.UI.modal
{
    partial class Warn
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbl_title = new System.Windows.Forms.Label();
            this.btn_tidak = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btn_ya = new System.Windows.Forms.Button();
            this.lbl_msg = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_title
            // 
            this.lbl_title.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(53)))), ((int)(((byte)(71)))));
            this.lbl_title.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_title.Font = new System.Drawing.Font("Verdana", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_title.ForeColor = System.Drawing.Color.White;
            this.lbl_title.Location = new System.Drawing.Point(0, 0);
            this.lbl_title.Name = "lbl_title";
            this.lbl_title.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.lbl_title.Size = new System.Drawing.Size(500, 74);
            this.lbl_title.TabIndex = 0;
            this.lbl_title.Text = "Peringatan ";
            this.lbl_title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbl_title.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DraggControl);
            // 
            // btn_tidak
            // 
            this.btn_tidak.BackColor = System.Drawing.Color.White;
            this.btn_tidak.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_tidak.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_tidak.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_tidak.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(53)))), ((int)(((byte)(71)))));
            this.btn_tidak.Location = new System.Drawing.Point(261, 193);
            this.btn_tidak.Name = "btn_tidak";
            this.btn_tidak.Size = new System.Drawing.Size(140, 45);
            this.btn_tidak.TabIndex = 2;
            this.btn_tidak.Text = "Tidak";
            this.btn_tidak.UseVisualStyleBackColor = false;
            this.btn_tidak.Click += new System.EventHandler(this.btn_tidak_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Gainsboro;
            this.panel1.Location = new System.Drawing.Point(0, 180);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(500, 2);
            this.panel1.TabIndex = 3;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::PWinformLib.Properties.Resources.error_color;
            this.pictureBox1.Location = new System.Drawing.Point(12, 93);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(64, 64);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DraggControl);
            // 
            // btn_ya
            // 
            this.btn_ya.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(53)))), ((int)(((byte)(71)))));
            this.btn_ya.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_ya.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_ya.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_ya.ForeColor = System.Drawing.Color.White;
            this.btn_ya.Location = new System.Drawing.Point(104, 193);
            this.btn_ya.Margin = new System.Windows.Forms.Padding(20, 3, 3, 3);
            this.btn_ya.Name = "btn_ya";
            this.btn_ya.Size = new System.Drawing.Size(140, 45);
            this.btn_ya.TabIndex = 4;
            this.btn_ya.Text = "Ya";
            this.btn_ya.UseVisualStyleBackColor = false;
            this.btn_ya.Click += new System.EventHandler(this.btn_ya_Click);
            // 
            // lbl_msg
            // 
            this.lbl_msg.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_msg.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(97)))), ((int)(((byte)(97)))), ((int)(((byte)(97)))));
            this.lbl_msg.Location = new System.Drawing.Point(83, 82);
            this.lbl_msg.Name = "lbl_msg";
            this.lbl_msg.Padding = new System.Windows.Forms.Padding(5);
            this.lbl_msg.Size = new System.Drawing.Size(405, 90);
            this.lbl_msg.TabIndex = 5;
            this.lbl_msg.Text = "lofdsfsd dfsdfsdf sdfsdfsdf dsfdsfsdf fdsfsdfsd dsfsdfsdfs ";
            this.lbl_msg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbl_msg.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DraggControl);
            // 
            // Warn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(500, 250);
            this.Controls.Add(this.btn_tidak);
            this.Controls.Add(this.btn_ya);
            this.Controls.Add(this.lbl_msg);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lbl_title);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Warn";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Warn";
            this.Shown += new System.EventHandler(this.Warn_Shown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DraggControl);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbl_title;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btn_tidak;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_ya;
        private System.Windows.Forms.Label lbl_msg;
    }
}