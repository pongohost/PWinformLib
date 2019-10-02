using System.Windows.Forms;

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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.pTextBox1 = new PWinformLib.PTextBox();
            this.pFlatButton1 = new PWinformLib.UI.PFlatButton();
            this.pTextBoxAni21 = new PWinformLib.UI.PTextBoxAni();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(381, 326);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(606, 179);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(165, 23);
            this.label1.TabIndex = 5;
            this.label1.Text = "label1";
            // 
            // button1
            // 
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.ImageKey = "(none)";
            this.button1.ImageList = this.imageList1;
            this.button1.Location = new System.Drawing.Point(436, 81);
            this.button1.Margin = new System.Windows.Forms.Padding(30, 3, 3, 3);
            this.button1.Name = "button1";
            this.button1.Padding = new System.Windows.Forms.Padding(50, 0, 0, 0);
            this.button1.Size = new System.Drawing.Size(157, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "button1";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "2dotswash.gif");
            // 
            // textBox2
            // 
            this.textBox2.AutoCompleteCustomSource.AddRange(new string[] {
            "afdfdsfd",
            "fds",
            "fds",
            "fds",
            "fd",
            "sf"});
            this.textBox2.Location = new System.Drawing.Point(72, 83);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 9;
            // 
            // pTextBox1
            // 
            this.pTextBox1.AutoCompleteList = new string[] {
        "assaxsxsa",
        "xsa",
        "xsa",
        "xsa",
        "sx",
        "asssssssss"};
            this.pTextBox1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.pTextBox1.BackColor = System.Drawing.Color.Transparent;
            this.pTextBox1.ForeColor = System.Drawing.Color.DimGray;
            this.pTextBox1.Location = new System.Drawing.Point(83, 40);
            this.pTextBox1.Name = "pTextBox1";
            this.pTextBox1.PasswordChar = '&';
            this.pTextBox1.PBgColor = System.Drawing.Color.White;
            this.pTextBox1.PBorderColor = System.Drawing.Color.Crimson;
            this.pTextBox1.PMultiLine = false;
            this.pTextBox1.PRadius = 15;
            this.pTextBox1.PTextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.pTextBox1.PWatermark = "isi";
            this.pTextBox1.PWatermarkFont = null;
            this.pTextBox1.PWatermarkTextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.pTextBox1.Size = new System.Drawing.Size(135, 33);
            this.pTextBox1.TabIndex = 8;
            // 
            // pFlatButton1
            // 
            this.pFlatButton1.BackColor = System.Drawing.Color.Transparent;
            this.pFlatButton1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.pFlatButton1.Font = new System.Drawing.Font("Verdana", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pFlatButton1.ForeColor = System.Drawing.Color.Black;
            this.pFlatButton1.Location = new System.Drawing.Point(83, 225);
            this.pFlatButton1.Name = "pFlatButton1";
            this.pFlatButton1.PActive1 = System.Drawing.Color.MidnightBlue;
            this.pFlatButton1.PActive2 = System.Drawing.Color.LightBlue;
            this.pFlatButton1.PAutoResizeText = true;
            this.pFlatButton1.PGradientAngle = 180F;
            this.pFlatButton1.PIcon = global::Demo.Properties.Resources.barVertical;
            this.pFlatButton1.PIconPosition = PWinformLib.UI.Position.Left;
            this.pFlatButton1.PInactive1 = System.Drawing.Color.Lime;
            this.pFlatButton1.PInactive2 = System.Drawing.Color.DarkGreen;
            this.pFlatButton1.PMultiLineText = false;
            this.pFlatButton1.PRadius = 5;
            this.pFlatButton1.PStyleButton = PWinformLib.UI.PFlatButton.EStyleButton.Clear;
            this.pFlatButton1.PTextAlignment = System.Drawing.ContentAlignment.BottomCenter;
            this.pFlatButton1.Size = new System.Drawing.Size(174, 27);
            this.pFlatButton1.Stroke = false;
            this.pFlatButton1.StrokeColor = System.Drawing.Color.Gray;
            this.pFlatButton1.TabIndex = 7;
            this.pFlatButton1.Text = "pFlatButton1";
            this.pFlatButton1.Click += new System.EventHandler(this.pFlatButton1_Click);
            // 
            // pTextBoxAni21
            // 
            this.pTextBoxAni21.BackColor = System.Drawing.Color.Transparent;
            this.pTextBoxAni21.Font = new System.Drawing.Font("Verdana", 12F);
            this.pTextBoxAni21.Location = new System.Drawing.Point(195, 126);
            this.pTextBoxAni21.Name = "pTextBoxAni21";
            this.pTextBoxAni21.PBorderClrActive = System.Drawing.Color.Red;
            this.pTextBoxAni21.PBorderClrInactive = System.Drawing.Color.Gray;
            this.pTextBoxAni21.PBorderLine = 1;
            this.pTextBoxAni21.PDurationAnimation = 500;
            this.pTextBoxAni21.PIconImage = ((System.Drawing.Image)(resources.GetObject("pTextBoxAni21.PIconImage")));
            this.pTextBoxAni21.PPassChar = '\0';
            this.pTextBoxAni21.Size = new System.Drawing.Size(218, 41);
            this.pTextBoxAni21.TabIndex = 4;
            this.pTextBoxAni21.Click += new System.EventHandler(this.pTextBoxAni21_Click);
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopRight;
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewTextBoxColumn1.HeaderText = "Column1";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Column2";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Column3";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.pTextBox1);
            this.Controls.Add(this.pFlatButton1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pTextBoxAni21);
            this.Controls.Add(this.textBox1);
            this.DoubleBuffered = true;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.TextBox textBox1;
        private PWinformLib.UI.PTextBoxAni pTextBoxAni21;
        private Label label1;
        private Button button1;
        private ImageList imageList1;
        private PWinformLib.UI.PFlatButton pFlatButton1;
        private PWinformLib.PTextBox pTextBox1;
        private TextBox textBox2;
    }
}

