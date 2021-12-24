
namespace BankTeacher.Bank.log
{
    partial class AddMember_Logg
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.DGV = new System.Windows.Forms.DataGridView();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RBday = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.LB1Id = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.LB2Ne = new System.Windows.Forms.Label();
            this.TBTeacherNo = new System.Windows.Forms.TextBox();
            this.TBTeacherName = new System.Windows.Forms.TextBox();
            this.RBSelectTeacherAdd = new System.Windows.Forms.RadioButton();
            this.DTP = new System.Windows.Forms.DateTimePicker();
            this.panel3 = new System.Windows.Forms.Panel();
            this.BSearchTeacher = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.DGV);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.DTP);
            this.panel1.Location = new System.Drawing.Point(12, 11);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(899, 569);
            this.panel1.TabIndex = 1;
            // 
            // DGV
            // 
            this.DGV.AllowUserToAddRows = false;
            this.DGV.AllowUserToDeleteRows = false;
            this.DGV.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.DGV.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("TH Sarabun New", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGV.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.DGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column6,
            this.Column2,
            this.Column5,
            this.Column1,
            this.Column4,
            this.Column3});
            this.DGV.Location = new System.Drawing.Point(22, 133);
            this.DGV.Name = "DGV";
            this.DGV.ReadOnly = true;
            this.DGV.RowHeadersVisible = false;
            this.DGV.Size = new System.Drawing.Size(851, 415);
            this.DGV.TabIndex = 94;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "ชื่อ - สกุล ผู้ทำรายการ";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Width = 165;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "รหัส";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 130;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "ชื่อ - สกุล";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 175;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "วันที่";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "จำนวนเงินเริ่มต้น";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 130;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "สถานะ";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 110;
            // 
            // RBday
            // 
            this.RBday.AutoSize = true;
            this.RBday.Location = new System.Drawing.Point(18, 1);
            this.RBday.Name = "RBday";
            this.RBday.Size = new System.Drawing.Size(108, 32);
            this.RBday.TabIndex = 92;
            this.RBday.Text = "ดูข้อมูลรายวัน";
            this.RBday.UseVisualStyleBackColor = true;
            this.RBday.CheckedChanged += new System.EventHandler(this.RBday_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.LB1Id);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.LB2Ne);
            this.panel2.Controls.Add(this.BSearchTeacher);
            this.panel2.Controls.Add(this.TBTeacherNo);
            this.panel2.Controls.Add(this.TBTeacherName);
            this.panel2.Location = new System.Drawing.Point(22, 55);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(551, 60);
            this.panel2.TabIndex = 93;
            // 
            // LB1Id
            // 
            this.LB1Id.AutoSize = true;
            this.LB1Id.BackColor = System.Drawing.Color.White;
            this.LB1Id.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LB1Id.ForeColor = System.Drawing.Color.Black;
            this.LB1Id.Location = new System.Drawing.Point(17, 14);
            this.LB1Id.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LB1Id.Name = "LB1Id";
            this.LB1Id.Size = new System.Drawing.Size(39, 30);
            this.LB1Id.TabIndex = 86;
            this.LB1Id.Text = "รหัส";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(187, 14);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 26);
            this.label1.TabIndex = 88;
            // 
            // LB2Ne
            // 
            this.LB2Ne.AutoSize = true;
            this.LB2Ne.BackColor = System.Drawing.Color.White;
            this.LB2Ne.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LB2Ne.ForeColor = System.Drawing.Color.Black;
            this.LB2Ne.Location = new System.Drawing.Point(223, 14);
            this.LB2Ne.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LB2Ne.Name = "LB2Ne";
            this.LB2Ne.Size = new System.Drawing.Size(63, 30);
            this.LB2Ne.TabIndex = 87;
            this.LB2Ne.Text = "ชื่อ-สกุล";
            // 
            // TBTeacherNo
            // 
            this.TBTeacherNo.Enabled = false;
            this.TBTeacherNo.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TBTeacherNo.Location = new System.Drawing.Point(57, 11);
            this.TBTeacherNo.Margin = new System.Windows.Forms.Padding(2);
            this.TBTeacherNo.MaxLength = 6;
            this.TBTeacherNo.Name = "TBTeacherNo";
            this.TBTeacherNo.Size = new System.Drawing.Size(116, 36);
            this.TBTeacherNo.TabIndex = 89;
            this.TBTeacherNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TBTeacherNo_KeyDown);
            // 
            // TBTeacherName
            // 
            this.TBTeacherName.Enabled = false;
            this.TBTeacherName.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TBTeacherName.Location = new System.Drawing.Point(290, 10);
            this.TBTeacherName.Margin = new System.Windows.Forms.Padding(2);
            this.TBTeacherName.Name = "TBTeacherName";
            this.TBTeacherName.Size = new System.Drawing.Size(242, 36);
            this.TBTeacherName.TabIndex = 90;
            // 
            // RBSelectTeacherAdd
            // 
            this.RBSelectTeacherAdd.AutoSize = true;
            this.RBSelectTeacherAdd.Location = new System.Drawing.Point(143, 1);
            this.RBSelectTeacherAdd.Name = "RBSelectTeacherAdd";
            this.RBSelectTeacherAdd.Size = new System.Drawing.Size(109, 32);
            this.RBSelectTeacherAdd.TabIndex = 92;
            this.RBSelectTeacherAdd.Text = "ดูข้อมูลรายคน";
            this.RBSelectTeacherAdd.UseVisualStyleBackColor = true;
            this.RBSelectTeacherAdd.CheckedChanged += new System.EventHandler(this.RBSelectTeacherAdd_CheckedChanged);
            // 
            // DTP
            // 
            this.DTP.Location = new System.Drawing.Point(22, 15);
            this.DTP.Name = "DTP";
            this.DTP.Size = new System.Drawing.Size(200, 35);
            this.DTP.TabIndex = 0;
            this.DTP.ValueChanged += new System.EventHandler(this.DTP_ValueChanged);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.RBSelectTeacherAdd);
            this.panel3.Controls.Add(this.RBday);
            this.panel3.Location = new System.Drawing.Point(235, 15);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(265, 35);
            this.panel3.TabIndex = 95;
            // 
            // BSearchTeacher
            // 
            this.BSearchTeacher.BackColor = System.Drawing.Color.White;
            this.BSearchTeacher.BackgroundImage = global::BankTeacher.Properties.Resources._64x64_magnifying_glass;
            this.BSearchTeacher.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.BSearchTeacher.Enabled = false;
            this.BSearchTeacher.Font = new System.Drawing.Font("TH Sarabun New", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BSearchTeacher.ImageKey = "(none)";
            this.BSearchTeacher.Location = new System.Drawing.Point(180, 13);
            this.BSearchTeacher.Margin = new System.Windows.Forms.Padding(2, 5, 2, 5);
            this.BSearchTeacher.Name = "BSearchTeacher";
            this.BSearchTeacher.Size = new System.Drawing.Size(32, 32);
            this.BSearchTeacher.TabIndex = 91;
            this.BSearchTeacher.UseVisualStyleBackColor = false;
            this.BSearchTeacher.Click += new System.EventHandler(this.BSearchTeacher_Click);
            // 
            // AddMember_Logg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 28F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(923, 591);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("TH Sarabun New", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.Name = "AddMember_Logg";
            this.Text = "AddMember_Log";
            this.Load += new System.EventHandler(this.AddMember_Log_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AddMember_Logg_KeyDown);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGV)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView DGV;
        private System.Windows.Forms.RadioButton RBday;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label LB1Id;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label LB2Ne;
        private System.Windows.Forms.Button BSearchTeacher;
        private System.Windows.Forms.TextBox TBTeacherNo;
        private System.Windows.Forms.TextBox TBTeacherName;
        private System.Windows.Forms.RadioButton RBSelectTeacherAdd;
        private System.Windows.Forms.DateTimePicker DTP;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.Panel panel3;
    }
}