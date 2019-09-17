namespace CeadeCEtabs
{
    partial class CeadeCEtabsMainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CeadeCEtabsMainForm));
            this.button1 = new System.Windows.Forms.Button();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.listBox3 = new System.Windows.Forms.ListBox();
            this.listBox4 = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.P = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.M2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.M3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.V2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.V3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.T = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label4 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(20, 14);
            this.button1.Margin = new System.Windows.Forms.Padding(5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(1235, 83);
            this.button1.TabIndex = 0;
            this.button1.Text = "Get Analysis Results From Etabs";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.ItemHeight = 20;
            this.listBox2.Location = new System.Drawing.Point(20, 146);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(212, 424);
            this.listBox2.TabIndex = 2;
            this.listBox2.SelectedIndexChanged += new System.EventHandler(this.ListBox2_SelectedIndexChanged);
            // 
            // listBox3
            // 
            this.listBox3.FormattingEnabled = true;
            this.listBox3.ItemHeight = 20;
            this.listBox3.Location = new System.Drawing.Point(254, 146);
            this.listBox3.Name = "listBox3";
            this.listBox3.Size = new System.Drawing.Size(250, 184);
            this.listBox3.TabIndex = 3;
            this.listBox3.SelectedIndexChanged += new System.EventHandler(this.ListBox3_SelectedIndexChanged);
            // 
            // listBox4
            // 
            this.listBox4.FormattingEnabled = true;
            this.listBox4.ItemHeight = 20;
            this.listBox4.Location = new System.Drawing.Point(254, 366);
            this.listBox4.Name = "listBox4";
            this.listBox4.Size = new System.Drawing.Size(250, 204);
            this.listBox4.TabIndex = 4;
            this.listBox4.SelectedIndexChanged += new System.EventHandler(this.ListBox4_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label1.Location = new System.Drawing.Point(16, 123);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(145, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "Selected Frames";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label2.Location = new System.Drawing.Point(250, 123);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "Load Cases";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label3.Location = new System.Drawing.Point(254, 343);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(163, 20);
            this.label3.TabIndex = 7;
            this.label3.Text = "Load Combinations";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(20, 593);
            this.button2.Margin = new System.Windows.Forms.Padding(5);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(1235, 83);
            this.button2.TabIndex = 8;
            this.button2.Text = "Export To CeadeC";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.P,
            this.M2,
            this.M3,
            this.V2,
            this.V3,
            this.T});
            this.dataGridView1.Location = new System.Drawing.Point(525, 366);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(730, 204);
            this.dataGridView1.TabIndex = 9;
            // 
            // P
            // 
            this.P.HeaderText = "P (ton)";
            this.P.Name = "P";
            this.P.ReadOnly = true;
            // 
            // M2
            // 
            this.M2.HeaderText = "M2 (ton.m)";
            this.M2.Name = "M2";
            this.M2.ReadOnly = true;
            // 
            // M3
            // 
            this.M3.HeaderText = "M3 (ton.m)";
            this.M3.Name = "M3";
            this.M3.ReadOnly = true;
            // 
            // V2
            // 
            this.V2.HeaderText = "V2 (ton)";
            this.V2.Name = "V2";
            this.V2.ReadOnly = true;
            // 
            // V3
            // 
            this.V3.HeaderText = "V3 (ton)";
            this.V3.Name = "V3";
            this.V3.ReadOnly = true;
            // 
            // T
            // 
            this.T.HeaderText = "T (ton.m)";
            this.T.Name = "T";
            this.T.ReadOnly = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label4.Location = new System.Drawing.Point(521, 123);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 20);
            this.label4.TabIndex = 11;
            this.label4.Text = "Stations";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 20;
            this.listBox1.Location = new System.Drawing.Point(525, 146);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(250, 184);
            this.listBox1.TabIndex = 10;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.ListBox1_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label5.Location = new System.Drawing.Point(521, 343);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(146, 20);
            this.label5.TabIndex = 12;
            this.label5.Text = "Straining Actions";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(920, 146);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(177, 214);
            this.pictureBox1.TabIndex = 13;
            this.pictureBox1.TabStop = false;
            // 
            // CeadeCEtabsMainForm
            // 
            this.AcceptButton = this.button2;
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Maroon;
            this.ClientSize = new System.Drawing.Size(1269, 690);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBox4);
            this.Controls.Add(this.listBox3);
            this.Controls.Add(this.listBox2);
            this.Controls.Add(this.button1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "CeadeCEtabsMainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CeadeCEtabs";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CeadeCEtabsMainForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.ListBox listBox3;
        private System.Windows.Forms.ListBox listBox4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridViewTextBoxColumn P;
        private System.Windows.Forms.DataGridViewTextBoxColumn M2;
        private System.Windows.Forms.DataGridViewTextBoxColumn M3;
        private System.Windows.Forms.DataGridViewTextBoxColumn V2;
        private System.Windows.Forms.DataGridViewTextBoxColumn V3;
        private System.Windows.Forms.DataGridViewTextBoxColumn T;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

