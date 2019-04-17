namespace Demo
{
    partial class Form1
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
            this.pGroupBox1 = new PWinformLib.UI.PGroupBox();
            this.pFlatButton1 = new PWinformLib.UI.PFlatButton();
            this.pTextBox2 = new PWinformLib.PTextBox();
            this.SuspendLayout();
            // 
            // pGroupBox1
            // 
            this.pGroupBox1.Location = new System.Drawing.Point(176, 108);
            this.pGroupBox1.Name = "pGroupBox1";
            this.pGroupBox1.PBgColor = System.Drawing.Color.Transparent;
            this.pGroupBox1.PBorderColor = System.Drawing.Color.Gray;
            this.pGroupBox1.PBorderType = System.Drawing.Drawing2D.DashStyle.Solid;
            this.pGroupBox1.Pradius = 35;
            this.pGroupBox1.PText = "Title Here";
            this.pGroupBox1.PTitleMargin = new System.Windows.Forms.Padding(0);
            this.pGroupBox1.PTitlePosition = System.Drawing.ContentAlignment.MiddleCenter;
            this.pGroupBox1.Size = new System.Drawing.Size(244, 241);
            this.pGroupBox1.TabIndex = 3;
            // 
            // pFlatButton1
            // 
            this.pFlatButton1.Active1 = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(168)))), ((int)(((byte)(183)))));
            this.pFlatButton1.Active2 = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(164)))), ((int)(((byte)(183)))));
            this.pFlatButton1.AutoResizeText = true;
            this.pFlatButton1.BackColor = System.Drawing.Color.Transparent;
            this.pFlatButton1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.pFlatButton1.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold);
            this.pFlatButton1.ForeColor = System.Drawing.Color.Black;
            this.pFlatButton1.GradientAngle = 180F;
            this.pFlatButton1.Inactive1 = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(188)))), ((int)(((byte)(210)))));
            this.pFlatButton1.Inactive2 = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(167)))), ((int)(((byte)(188)))));
            this.pFlatButton1.Location = new System.Drawing.Point(546, 153);
            this.pFlatButton1.MultiLineText = false;
            this.pFlatButton1.Name = "pFlatButton1";
            this.pFlatButton1.Radius = 10;
            this.pFlatButton1.Size = new System.Drawing.Size(65, 30);
            this.pFlatButton1.Stroke = false;
            this.pFlatButton1.StrokeColor = System.Drawing.Color.Gray;
            this.pFlatButton1.TabIndex = 2;
            this.pFlatButton1.Text = "pFlatButton1";
            this.pFlatButton1.Click += new System.EventHandler(this.pFlatButton1_Click);
            // 
            // pTextBox2
            // 
            this.pTextBox2.BackColor = System.Drawing.Color.Transparent;
            this.pTextBox2.BgColor = System.Drawing.Color.White;
            this.pTextBox2.BorderColor = System.Drawing.Color.Crimson;
            this.pTextBox2.ForeColor = System.Drawing.Color.DimGray;
            this.pTextBox2.Location = new System.Drawing.Point(460, 59);
            this.pTextBox2.MultiLine = false;
            this.pTextBox2.Name = "pTextBox2";
            this.pTextBox2.PasswordChar = '\0';
            this.pTextBox2.Radius = 15;
            this.pTextBox2.Size = new System.Drawing.Size(135, 33);
            this.pTextBox2.TabIndex = 1;
            this.pTextBox2.Text = "pTextBox2";
            this.pTextBox2.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.pTextBox2.Watermark = null;
            this.pTextBox2.WatermarkFont = null;
            this.pTextBox2.WatermarkTextAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pGroupBox1);
            this.Controls.Add(this.pFlatButton1);
            this.Controls.Add(this.pTextBox2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private PWinformLib.PTextBox pTextBox2;
        private PWinformLib.UI.PFlatButton pFlatButton1;
        private PWinformLib.UI.PGroupBox pGroupBox1;
    }
}

