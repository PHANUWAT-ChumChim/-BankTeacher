
namespace BankTeacher.Bank.Loan
{
    partial class CancelLoan
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
            this.CBlist = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.BSearchTeacher = new System.Windows.Forms.Button();
            this.TBTeacherNo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.DGV = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel7 = new System.Windows.Forms.Panel();
            this.TBTeacherName = new System.Windows.Forms.TextBox();
            this.LB2Ne = new System.Windows.Forms.Label();
            this.BCancelSave = new System.Windows.Forms.Button();
            this.BExit = new System.Windows.Forms.Button();
            this.CBStatus = new System.Windows.Forms.ComboBox();
            this.BSave = new System.Windows.Forms.Button();
            this.panel16 = new System.Windows.Forms.Panel();
            this.BExitForm = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV)).BeginInit();
            this.panel7.SuspendLayout();
            this.panel16.SuspendLayout();
            this.SuspendLayout();
            // 
            // CBlist
            // 
            this.CBlist.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBlist.Enabled = false;
            this.CBlist.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CBlist.FormattingEnabled = true;
            this.CBlist.Location = new System.Drawing.Point(81, 12);
            this.CBlist.Margin = new System.Windows.Forms.Padding(2);
            this.CBlist.Name = "CBlist";
            this.CBlist.Size = new System.Drawing.Size(189, 38);
            this.CBlist.TabIndex = 62;
            this.CBlist.SelectedIndexChanged += new System.EventHandler(this.CBList_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("TH Sarabun New", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(16, 12);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(42, 32);
            this.label9.TabIndex = 56;
            this.label9.Text = "รหัส";
            // 
            // BSearchTeacher
            // 
            this.BSearchTeacher.BackColor = System.Drawing.Color.White;
            this.BSearchTeacher.BackgroundImage = global::BankTeacher.Properties.Resources._64x64_magnifying_glass;
            this.BSearchTeacher.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.BSearchTeacher.ForeColor = System.Drawing.Color.White;
            this.BSearchTeacher.ImageKey = "(none)";
            this.BSearchTeacher.Location = new System.Drawing.Point(257, 11);
            this.BSearchTeacher.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.BSearchTeacher.Name = "BSearchTeacher";
            this.BSearchTeacher.Size = new System.Drawing.Size(32, 35);
            this.BSearchTeacher.TabIndex = 58;
            this.BSearchTeacher.UseVisualStyleBackColor = false;
            this.BSearchTeacher.Click += new System.EventHandler(this.BSearchTeacher_Click_1);
            // 
            // TBTeacherNo
            // 
            this.TBTeacherNo.Location = new System.Drawing.Point(62, 12);
            this.TBTeacherNo.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.TBTeacherNo.Name = "TBTeacherNo";
            this.TBTeacherNo.Size = new System.Drawing.Size(188, 36);
            this.TBTeacherNo.TabIndex = 57;
            this.TBTeacherNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TBTeacherNo_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("TH Sarabun New", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 32);
            this.label1.TabIndex = 61;
            this.label1.Text = "รายการกู้";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.DGV);
            this.panel1.Controls.Add(this.panel7);
            this.panel1.Controls.Add(this.BCancelSave);
            this.panel1.Controls.Add(this.BExit);
            this.panel1.Controls.Add(this.CBStatus);
            this.panel1.Controls.Add(this.BSave);
            this.panel1.Controls.Add(this.panel16);
            this.panel1.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(16, 39);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 13, 4, 13);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(874, 533);
            this.panel1.TabIndex = 20;
            // 
            // DGV
            // 
            this.DGV.AllowUserToAddRows = false;
            this.DGV.AllowUserToDeleteRows = false;
            this.DGV.BackgroundColor = System.Drawing.Color.White;
            this.DGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
            this.DGV.Location = new System.Drawing.Point(23, 138);
            this.DGV.Name = "DGV";
            this.DGV.ReadOnly = true;
            this.DGV.RowHeadersVisible = false;
            this.DGV.Size = new System.Drawing.Size(824, 313);
            this.DGV.TabIndex = 100;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "วันที่สมัครกู้";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 150;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column2.HeaderText = "ชื่อ - สกุล";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "ยอดเงินค้ำ";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 200;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.White;
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel7.Controls.Add(this.TBTeacherName);
            this.panel7.Controls.Add(this.LB2Ne);
            this.panel7.Controls.Add(this.label9);
            this.panel7.Controls.Add(this.BSearchTeacher);
            this.panel7.Controls.Add(this.TBTeacherNo);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Margin = new System.Windows.Forms.Padding(2);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(872, 62);
            this.panel7.TabIndex = 67;
            // 
            // TBTeacherName
            // 
            this.TBTeacherName.Enabled = false;
            this.TBTeacherName.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TBTeacherName.Location = new System.Drawing.Point(357, 8);
            this.TBTeacherName.Margin = new System.Windows.Forms.Padding(2);
            this.TBTeacherName.Name = "TBTeacherName";
            this.TBTeacherName.Size = new System.Drawing.Size(306, 36);
            this.TBTeacherName.TabIndex = 85;
            // 
            // LB2Ne
            // 
            this.LB2Ne.AutoSize = true;
            this.LB2Ne.BackColor = System.Drawing.Color.White;
            this.LB2Ne.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LB2Ne.ForeColor = System.Drawing.Color.Black;
            this.LB2Ne.Location = new System.Drawing.Point(294, 11);
            this.LB2Ne.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LB2Ne.Name = "LB2Ne";
            this.LB2Ne.Size = new System.Drawing.Size(59, 30);
            this.LB2Ne.TabIndex = 84;
            this.LB2Ne.Text = "ชื่อ-สกุล";
            // 
            // BCancelSave
            // 
            this.BCancelSave.BackColor = System.Drawing.Color.White;
            this.BCancelSave.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BCancelSave.Location = new System.Drawing.Point(614, 456);
            this.BCancelSave.Margin = new System.Windows.Forms.Padding(2);
            this.BCancelSave.Name = "BCancelSave";
            this.BCancelSave.Size = new System.Drawing.Size(256, 64);
            this.BCancelSave.TabIndex = 55;
            this.BCancelSave.Text = "บันทึก";
            this.BCancelSave.UseVisualStyleBackColor = false;
            this.BCancelSave.Click += new System.EventHandler(this.BCancelSave_Click);
            // 
            // BExit
            // 
            this.BExit.BackColor = System.Drawing.Color.White;
            this.BExit.Font = new System.Drawing.Font("TH Sarabun New", 19.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BExit.Location = new System.Drawing.Point(743, 861);
            this.BExit.Margin = new System.Windows.Forms.Padding(4, 13, 4, 13);
            this.BExit.Name = "BExit";
            this.BExit.Size = new System.Drawing.Size(129, 111);
            this.BExit.TabIndex = 53;
            this.BExit.Text = "ออก";
            this.BExit.UseVisualStyleBackColor = false;
            // 
            // CBStatus
            // 
            this.CBStatus.FormattingEnabled = true;
            this.CBStatus.Location = new System.Drawing.Point(904, 657);
            this.CBStatus.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.CBStatus.Name = "CBStatus";
            this.CBStatus.Size = new System.Drawing.Size(199, 38);
            this.CBStatus.TabIndex = 51;
            // 
            // BSave
            // 
            this.BSave.BackColor = System.Drawing.Color.White;
            this.BSave.Font = new System.Drawing.Font("TH Sarabun New", 19.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BSave.Location = new System.Drawing.Point(267, 861);
            this.BSave.Margin = new System.Windows.Forms.Padding(4, 13, 4, 13);
            this.BSave.Name = "BSave";
            this.BSave.Size = new System.Drawing.Size(410, 111);
            this.BSave.TabIndex = 28;
            this.BSave.Text = "บันทึก";
            this.BSave.UseVisualStyleBackColor = false;
            // 
            // panel16
            // 
            this.panel16.BackColor = System.Drawing.Color.White;
            this.panel16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel16.Controls.Add(this.label1);
            this.panel16.Controls.Add(this.CBlist);
            this.panel16.Location = new System.Drawing.Point(0, 62);
            this.panel16.Margin = new System.Windows.Forms.Padding(2);
            this.panel16.Name = "panel16";
            this.panel16.Size = new System.Drawing.Size(290, 59);
            this.panel16.TabIndex = 99;
            // 
            // BExitForm
            // 
            this.BExitForm.BackgroundImage = global::BankTeacher.Properties.Resources.logout;
            this.BExitForm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.BExitForm.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.BExitForm.Location = new System.Drawing.Point(816, 579);
            this.BExitForm.Name = "BExitForm";
            this.BExitForm.Size = new System.Drawing.Size(73, 66);
            this.BExitForm.TabIndex = 82;
            this.BExitForm.UseVisualStyleBackColor = true;
            this.BExitForm.Click += new System.EventHandler(this.BExitForm_Click);
            // 
            // CancelLoan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(904, 657);
            this.Controls.Add(this.BExitForm);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "CancelLoan";
            this.Text = "CancelLoan";
            this.SizeChanged += new System.EventHandler(this.CancelLoan_SizeChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CancelLoan_KeyDown);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGV)).EndInit();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel16.ResumeLayout(false);
            this.panel16.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button BExit;
        private System.Windows.Forms.ComboBox CBStatus;
        private System.Windows.Forms.Button BSave;
        private System.Windows.Forms.ComboBox CBlist;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button BSearchTeacher;
        private System.Windows.Forms.TextBox TBTeacherNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BCancelSave;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel16;
        private System.Windows.Forms.TextBox TBTeacherName;
        private System.Windows.Forms.Label LB2Ne;
        private System.Windows.Forms.Button BExitForm;
        private System.Windows.Forms.DataGridView DGV;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
    }
}