namespace PWinformLib.UI
{
    partial class PDialogUI
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.lbl_pesan = new System.Windows.Forms.Label();
            this.lbl_imbang = new System.Windows.Forms.Label();
            this.txt_input = new PWinformLib.PTextBox();
            this.btn_ok = new PWinformLib.UI.PFlatButton();
            this.pFlatButton2 = new PWinformLib.UI.PFlatButton();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.flowLayoutPanel1.Controls.Add(this.lbl_pesan);
            this.flowLayoutPanel1.Controls.Add(this.lbl_imbang);
            this.flowLayoutPanel1.Controls.Add(this.txt_input);
            this.flowLayoutPanel1.Controls.Add(this.btn_ok);
            this.flowLayoutPanel1.Controls.Add(this.pFlatButton2);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(450, 172);
            this.flowLayoutPanel1.TabIndex = 0;
            this.flowLayoutPanel1.Click += new System.EventHandler(this.flowLayoutPanel1_Click);
            // 
            // lbl_pesan
            // 
            this.lbl_pesan.AutoSize = true;
            this.lbl_pesan.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_pesan.Location = new System.Drawing.Point(3, 30);
            this.lbl_pesan.Margin = new System.Windows.Forms.Padding(3, 30, 3, 0);
            this.lbl_pesan.MaximumSize = new System.Drawing.Size(440, 0);
            this.lbl_pesan.Name = "lbl_pesan";
            this.lbl_pesan.Size = new System.Drawing.Size(0, 0);
            this.lbl_pesan.TabIndex = 0;
            // 
            // lbl_imbang
            // 
            this.lbl_imbang.BackColor = System.Drawing.Color.Transparent;
            this.lbl_imbang.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_imbang.Location = new System.Drawing.Point(6, 0);
            this.lbl_imbang.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_imbang.Name = "lbl_imbang";
            this.lbl_imbang.Size = new System.Drawing.Size(270, 10);
            this.lbl_imbang.TabIndex = 4;
            // 
            // txt_input
            // 
            this.txt_input.BackColor = System.Drawing.Color.Transparent;
            this.txt_input.BgColor = System.Drawing.Color.White;
            this.txt_input.BorderColor = System.Drawing.Color.Crimson;
            this.txt_input.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_input.ForeColor = System.Drawing.Color.DimGray;
            this.txt_input.Location = new System.Drawing.Point(10, 63);
            this.txt_input.Margin = new System.Windows.Forms.Padding(10);
            this.txt_input.Name = "txt_input";
            this.txt_input.PasswordChar = '\0';
            this.txt_input.Radius = 15;
            this.txt_input.Size = new System.Drawing.Size(431, 38);
            this.txt_input.TabIndex = 1;
            this.txt_input.Visible = false;
            this.txt_input.Watermark = "";
            this.txt_input.WatermarkFont = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btn_ok
            // 
            this.btn_ok.Active1 = System.Drawing.Color.Lime;
            this.btn_ok.Active2 = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btn_ok.AutoResizeText = true;
            this.btn_ok.BackColor = System.Drawing.Color.Transparent;
            this.btn_ok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_ok.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold);
            this.btn_ok.ForeColor = System.Drawing.Color.Black;
            this.btn_ok.GradientAngle = 180F;
            this.btn_ok.Inactive1 = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btn_ok.Inactive2 = System.Drawing.Color.Lime;
            this.btn_ok.Location = new System.Drawing.Point(10, 114);
            this.btn_ok.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.btn_ok.MultiLineText = false;
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Radius = 10;
            this.btn_ok.Size = new System.Drawing.Size(210, 55);
            this.btn_ok.Stroke = true;
            this.btn_ok.StrokeColor = System.Drawing.Color.Crimson;
            this.btn_ok.TabIndex = 2;
            this.btn_ok.Text = "Ya";
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // pFlatButton2
            // 
            this.pFlatButton2.Active1 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.pFlatButton2.Active2 = System.Drawing.Color.Red;
            this.pFlatButton2.AutoResizeText = true;
            this.pFlatButton2.BackColor = System.Drawing.Color.Transparent;
            this.pFlatButton2.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.pFlatButton2.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold);
            this.pFlatButton2.ForeColor = System.Drawing.Color.Black;
            this.pFlatButton2.GradientAngle = 180F;
            this.pFlatButton2.Inactive1 = System.Drawing.Color.Red;
            this.pFlatButton2.Inactive2 = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.pFlatButton2.Location = new System.Drawing.Point(226, 114);
            this.pFlatButton2.MultiLineText = false;
            this.pFlatButton2.Name = "pFlatButton2";
            this.pFlatButton2.Radius = 10;
            this.pFlatButton2.Size = new System.Drawing.Size(210, 55);
            this.pFlatButton2.Stroke = true;
            this.pFlatButton2.StrokeColor = System.Drawing.Color.Crimson;
            this.pFlatButton2.TabIndex = 3;
            this.pFlatButton2.Text = "Tidak";
            this.pFlatButton2.Click += new System.EventHandler(this.pFlatButton2_Click);
            // 
            // PDialogUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(450, 250);
            this.Controls.Add(this.flowLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PDialogUI";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PDialog";
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label lbl_pesan;
        private PTextBox txt_input;
        private PFlatButton btn_ok;
        private PFlatButton pFlatButton2;
        private System.Windows.Forms.Label lbl_imbang;
    }
}