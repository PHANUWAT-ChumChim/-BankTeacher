
namespace example.Bank
{
    partial class ReportCancelMember
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportCancelMember));
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel16 = new System.Windows.Forms.Panel();
            this.CBMonth = new System.Windows.Forms.ComboBox();
            this.LB5Mo = new System.Windows.Forms.Label();
            this.LB5Ye = new System.Windows.Forms.Label();
            this.CByeartap1 = new System.Windows.Forms.ComboBox();
            this.button4 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel7 = new System.Windows.Forms.Panel();
            this.BSearchTeacher = new System.Windows.Forms.Button();
            this.TBTeacherBill = new System.Windows.Forms.TextBox();
            this.TBTeacherName = new System.Windows.Forms.TextBox();
            this.TBTeacherNo = new System.Windows.Forms.TextBox();
            this.LB3Bi = new System.Windows.Forms.Label();
            this.LB2Ne = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.LB1Id = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel16.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel7.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Controls.Add(this.panel7);
            this.panel1.Location = new System.Drawing.Point(16, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1156, 712);
            this.panel1.TabIndex = 99;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(8, 108);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1138, 595);
            this.tabControl1.TabIndex = 97;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.White;
            this.tabPage1.Controls.Add(this.panel16);
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage1.Location = new System.Drawing.Point(4, 45);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1130, 546);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "รายชื่อผู้ยกเลิก";
            // 
            // panel16
            // 
            this.panel16.BackColor = System.Drawing.Color.White;
            this.panel16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel16.Controls.Add(this.CBMonth);
            this.panel16.Controls.Add(this.LB5Mo);
            this.panel16.Controls.Add(this.LB5Ye);
            this.panel16.Controls.Add(this.CByeartap1);
            this.panel16.Controls.Add(this.button4);
            this.panel16.Location = new System.Drawing.Point(0, 0);
            this.panel16.Name = "panel16";
            this.panel16.Size = new System.Drawing.Size(1124, 72);
            this.panel16.TabIndex = 98;
            // 
            // CBMonth
            // 
            this.CBMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBMonth.Enabled = false;
            this.CBMonth.Font = new System.Drawing.Font("TH Sarabun New", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CBMonth.FormattingEnabled = true;
            this.CBMonth.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12"});
            this.CBMonth.Location = new System.Drawing.Point(295, 15);
            this.CBMonth.Name = "CBMonth";
            this.CBMonth.Size = new System.Drawing.Size(130, 39);
            this.CBMonth.TabIndex = 101;
            // 
            // LB5Mo
            // 
            this.LB5Mo.AutoSize = true;
            this.LB5Mo.BackColor = System.Drawing.Color.White;
            this.LB5Mo.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LB5Mo.ForeColor = System.Drawing.Color.Black;
            this.LB5Mo.Location = new System.Drawing.Point(215, 17);
            this.LB5Mo.Name = "LB5Mo";
            this.LB5Mo.Size = new System.Drawing.Size(57, 37);
            this.LB5Mo.TabIndex = 100;
            this.LB5Mo.Text = "เดือน";
            // 
            // LB5Ye
            // 
            this.LB5Ye.AutoSize = true;
            this.LB5Ye.BackColor = System.Drawing.Color.White;
            this.LB5Ye.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LB5Ye.ForeColor = System.Drawing.Color.Black;
            this.LB5Ye.Location = new System.Drawing.Point(17, 18);
            this.LB5Ye.Name = "LB5Ye";
            this.LB5Ye.Size = new System.Drawing.Size(29, 37);
            this.LB5Ye.TabIndex = 99;
            this.LB5Ye.Text = "ปี";
            // 
            // CByeartap1
            // 
            this.CByeartap1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CByeartap1.Font = new System.Drawing.Font("TH Sarabun New", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CByeartap1.FormattingEnabled = true;
            this.CByeartap1.Items.AddRange(new object[] {
            "2021",
            "2020",
            "2019",
            "2018",
            "2017",
            "2015"});
            this.CByeartap1.Location = new System.Drawing.Point(75, 16);
            this.CByeartap1.Name = "CByeartap1";
            this.CByeartap1.Size = new System.Drawing.Size(103, 39);
            this.CByeartap1.TabIndex = 84;
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.White;
            this.button4.Enabled = false;
            this.button4.Font = new System.Drawing.Font("TH Sarabun New", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.Location = new System.Drawing.Point(481, 8);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(622, 54);
            this.button4.TabIndex = 86;
            this.button4.Text = "อัตโนมัติ";
            this.button4.UseVisualStyleBackColor = false;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
            this.dataGridView1.Location = new System.Drawing.Point(3, 78);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(1124, 425);
            this.dataGridView1.TabIndex = 93;
            // 
            // Column1
            // 
            dataGridViewCellStyle2.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Column1.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column1.HeaderText = "วัน/เดือน//ปี";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 150;
            // 
            // Column2
            // 
            dataGridViewCellStyle3.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Column2.DefaultCellStyle = dataGridViewCellStyle3;
            this.Column2.HeaderText = "ชื่อ";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 350;
            // 
            // Column3
            // 
            dataGridViewCellStyle4.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Column3.DefaultCellStyle = dataGridViewCellStyle4;
            this.Column3.HeaderText = "สาเหตุ";
            this.Column3.MinimumWidth = 6;
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 340;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.White;
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel7.Controls.Add(this.BSearchTeacher);
            this.panel7.Controls.Add(this.TBTeacherBill);
            this.panel7.Controls.Add(this.TBTeacherName);
            this.panel7.Controls.Add(this.TBTeacherNo);
            this.panel7.Controls.Add(this.LB3Bi);
            this.panel7.Controls.Add(this.LB2Ne);
            this.panel7.Controls.Add(this.label1);
            this.panel7.Controls.Add(this.LB1Id);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(1156, 72);
            this.panel7.TabIndex = 63;
            this.panel7.Paint += new System.Windows.Forms.PaintEventHandler(this.panel7_Paint);
            // 
            // BSearchTeacher
            // 
            this.BSearchTeacher.BackColor = System.Drawing.Color.White;
            this.BSearchTeacher.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BSearchTeacher.BackgroundImage")));
            this.BSearchTeacher.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.BSearchTeacher.ImageKey = "(none)";
            this.BSearchTeacher.Location = new System.Drawing.Point(241, 14);
            this.BSearchTeacher.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            this.BSearchTeacher.Name = "BSearchTeacher";
            this.BSearchTeacher.Size = new System.Drawing.Size(43, 43);
            this.BSearchTeacher.TabIndex = 85;
            this.BSearchTeacher.UseVisualStyleBackColor = false;
            this.BSearchTeacher.Click += new System.EventHandler(this.BSearchTeacher_Click);
            // 
            // TBTeacherBill
            // 
            this.TBTeacherBill.Enabled = false;
            this.TBTeacherBill.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TBTeacherBill.Location = new System.Drawing.Point(776, 14);
            this.TBTeacherBill.Name = "TBTeacherBill";
            this.TBTeacherBill.Size = new System.Drawing.Size(366, 43);
            this.TBTeacherBill.TabIndex = 84;
            // 
            // TBTeacherName
            // 
            this.TBTeacherName.Enabled = false;
            this.TBTeacherName.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TBTeacherName.Location = new System.Drawing.Point(375, 14);
            this.TBTeacherName.Name = "TBTeacherName";
            this.TBTeacherName.Size = new System.Drawing.Size(320, 43);
            this.TBTeacherName.TabIndex = 83;
            // 
            // TBTeacherNo
            // 
            this.TBTeacherNo.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TBTeacherNo.Location = new System.Drawing.Point(71, 14);
            this.TBTeacherNo.Name = "TBTeacherNo";
            this.TBTeacherNo.Size = new System.Drawing.Size(154, 43);
            this.TBTeacherNo.TabIndex = 82;
            this.TBTeacherNo.TextChanged += new System.EventHandler(this.TBTeacherNo_TextChanged);
            // 
            // LB3Bi
            // 
            this.LB3Bi.AutoSize = true;
            this.LB3Bi.BackColor = System.Drawing.Color.White;
            this.LB3Bi.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LB3Bi.ForeColor = System.Drawing.Color.Black;
            this.LB3Bi.Location = new System.Drawing.Point(701, 17);
            this.LB3Bi.Name = "LB3Bi";
            this.LB3Bi.Size = new System.Drawing.Size(69, 37);
            this.LB3Bi.TabIndex = 69;
            this.LB3Bi.Text = "เลขบิล";
            // 
            // LB2Ne
            // 
            this.LB2Ne.AutoSize = true;
            this.LB2Ne.BackColor = System.Drawing.Color.White;
            this.LB2Ne.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LB2Ne.ForeColor = System.Drawing.Color.Black;
            this.LB2Ne.Location = new System.Drawing.Point(291, 17);
            this.LB2Ne.Name = "LB2Ne";
            this.LB2Ne.Size = new System.Drawing.Size(78, 37);
            this.LB2Ne.TabIndex = 68;
            this.LB2Ne.Text = "ชื่อ-สกุล";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(244, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 32);
            this.label1.TabIndex = 68;
            // 
            // LB1Id
            // 
            this.LB1Id.AutoSize = true;
            this.LB1Id.BackColor = System.Drawing.Color.White;
            this.LB1Id.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LB1Id.ForeColor = System.Drawing.Color.Black;
            this.LB1Id.Location = new System.Drawing.Point(16, 17);
            this.LB1Id.Name = "LB1Id";
            this.LB1Id.Size = new System.Drawing.Size(49, 37);
            this.LB1Id.TabIndex = 67;
            this.LB1Id.Text = "รหัส";
            // 
            // ReportCancelMember
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1188, 762);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ReportCancelMember";
            this.Text = "ReportCancelMember";
            this.SizeChanged += new System.EventHandler(this.ReportCancelMember_SizeChanged);
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel16.ResumeLayout(false);
            this.panel16.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Panel panel16;
        private System.Windows.Forms.ComboBox CBMonth;
        private System.Windows.Forms.Label LB5Mo;
        private System.Windows.Forms.Label LB5Ye;
        private System.Windows.Forms.ComboBox CByeartap1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Button BSearchTeacher;
        private System.Windows.Forms.TextBox TBTeacherBill;
        private System.Windows.Forms.TextBox TBTeacherName;
        private System.Windows.Forms.TextBox TBTeacherNo;
        private System.Windows.Forms.Label LB3Bi;
        private System.Windows.Forms.Label LB2Ne;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label LB1Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
    }
}