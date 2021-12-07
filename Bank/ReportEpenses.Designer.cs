
namespace BankTeacher.Bank
{
    partial class ReportEpenses
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.BSearchTeacher = new System.Windows.Forms.Button();
            this.TBTeacherName = new System.Windows.Forms.TextBox();
            this.TBTeacherNo = new System.Windows.Forms.TextBox();
            this.LB2Ne = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.LB1Id = new System.Windows.Forms.Label();
            this.TBAmountWithDraw = new System.Windows.Forms.TextBox();
            this.TBAmountLoan = new System.Windows.Forms.TextBox();
            this.TBAmount = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.DGV = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DTP = new System.Windows.Forms.DateTimePicker();
            this.BExitForm = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.BExitForm);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.DGV);
            this.panel1.Controls.Add(this.DTP);
            this.panel1.Location = new System.Drawing.Point(15, 16);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(877, 606);
            this.panel1.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.BSearchTeacher);
            this.panel2.Controls.Add(this.TBTeacherName);
            this.panel2.Controls.Add(this.TBTeacherNo);
            this.panel2.Controls.Add(this.LB2Ne);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.LB1Id);
            this.panel2.Controls.Add(this.TBAmountWithDraw);
            this.panel2.Controls.Add(this.TBAmountLoan);
            this.panel2.Controls.Add(this.TBAmount);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Location = new System.Drawing.Point(23, 69);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(816, 133);
            this.panel2.TabIndex = 92;
            // 
            // BSearchTeacher
            // 
            this.BSearchTeacher.BackColor = System.Drawing.Color.White;
            this.BSearchTeacher.BackgroundImage = global::BankTeacher.Properties.Resources._64x64_magnifying_glass;
            this.BSearchTeacher.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.BSearchTeacher.Font = new System.Drawing.Font("TH Sarabun New", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BSearchTeacher.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.BSearchTeacher.ImageKey = "(none)";
            this.BSearchTeacher.Location = new System.Drawing.Point(313, 19);
            this.BSearchTeacher.Margin = new System.Windows.Forms.Padding(2, 6, 2, 6);
            this.BSearchTeacher.Name = "BSearchTeacher";
            this.BSearchTeacher.Size = new System.Drawing.Size(36, 35);
            this.BSearchTeacher.TabIndex = 91;
            this.BSearchTeacher.UseVisualStyleBackColor = false;
            this.BSearchTeacher.Click += new System.EventHandler(this.BSearchTeacher_Click);
            // 
            // TBTeacherName
            // 
            this.TBTeacherName.Enabled = false;
            this.TBTeacherName.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TBTeacherName.Location = new System.Drawing.Point(435, 17);
            this.TBTeacherName.Margin = new System.Windows.Forms.Padding(2);
            this.TBTeacherName.Name = "TBTeacherName";
            this.TBTeacherName.Size = new System.Drawing.Size(289, 36);
            this.TBTeacherName.TabIndex = 90;
            // 
            // TBTeacherNo
            // 
            this.TBTeacherNo.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TBTeacherNo.Location = new System.Drawing.Point(134, 18);
            this.TBTeacherNo.Margin = new System.Windows.Forms.Padding(2);
            this.TBTeacherNo.MaxLength = 6;
            this.TBTeacherNo.Name = "TBTeacherNo";
            this.TBTeacherNo.Size = new System.Drawing.Size(174, 36);
            this.TBTeacherNo.TabIndex = 89;
            this.TBTeacherNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TBTeacherNo_KeyDown);
            // 
            // LB2Ne
            // 
            this.LB2Ne.AutoSize = true;
            this.LB2Ne.BackColor = System.Drawing.Color.White;
            this.LB2Ne.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LB2Ne.ForeColor = System.Drawing.Color.Black;
            this.LB2Ne.Location = new System.Drawing.Point(373, 19);
            this.LB2Ne.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LB2Ne.Name = "LB2Ne";
            this.LB2Ne.Size = new System.Drawing.Size(63, 30);
            this.LB2Ne.TabIndex = 87;
            this.LB2Ne.Text = "ชื่อ-สกุล";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(187, 18);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(0, 26);
            this.label10.TabIndex = 88;
            // 
            // LB1Id
            // 
            this.LB1Id.AutoSize = true;
            this.LB1Id.BackColor = System.Drawing.Color.White;
            this.LB1Id.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LB1Id.ForeColor = System.Drawing.Color.Black;
            this.LB1Id.Location = new System.Drawing.Point(17, 18);
            this.LB1Id.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LB1Id.Name = "LB1Id";
            this.LB1Id.Size = new System.Drawing.Size(113, 30);
            this.LB1Id.TabIndex = 86;
            this.LB1Id.Text = "รหัสผู้ทำรายการ";
            // 
            // TBAmountWithDraw
            // 
            this.TBAmountWithDraw.Enabled = false;
            this.TBAmountWithDraw.Location = new System.Drawing.Point(671, 72);
            this.TBAmountWithDraw.Name = "TBAmountWithDraw";
            this.TBAmountWithDraw.Size = new System.Drawing.Size(133, 35);
            this.TBAmountWithDraw.TabIndex = 12;
            this.TBAmountWithDraw.Text = "0";
            // 
            // TBAmountLoan
            // 
            this.TBAmountLoan.Enabled = false;
            this.TBAmountLoan.Location = new System.Drawing.Point(380, 72);
            this.TBAmountLoan.Name = "TBAmountLoan";
            this.TBAmountLoan.Size = new System.Drawing.Size(133, 35);
            this.TBAmountLoan.TabIndex = 13;
            this.TBAmountLoan.Text = "0";
            // 
            // TBAmount
            // 
            this.TBAmount.Enabled = false;
            this.TBAmount.Location = new System.Drawing.Point(136, 72);
            this.TBAmount.Name = "TBAmount";
            this.TBAmount.Size = new System.Drawing.Size(133, 35);
            this.TBAmount.TabIndex = 14;
            this.TBAmount.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(519, 75);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(146, 28);
            this.label7.TabIndex = 8;
            this.label7.Text = "จำนวนเงินถอนหุ้นสะสม";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(275, 75);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(102, 28);
            this.label8.TabIndex = 9;
            this.label8.Text = "จำนวนเงินจ่ายกู้";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(19, 75);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(111, 28);
            this.label9.TabIndex = 10;
            this.label9.Text = "จำนวนเงินทั้งหมด";
            // 
            // DGV
            // 
            this.DGV.AllowUserToAddRows = false;
            this.DGV.AllowUserToDeleteRows = false;
            this.DGV.AllowUserToResizeRows = false;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle7.Font = new System.Drawing.Font("TH Sarabun New", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.DGV.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle7;
            this.DGV.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("TH Sarabun New", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGV.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.DGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn2,
            this.Column4,
            this.Column1,
            this.Column7});
            this.DGV.GridColor = System.Drawing.SystemColors.Control;
            this.DGV.Location = new System.Drawing.Point(23, 208);
            this.DGV.Name = "DGV";
            this.DGV.ReadOnly = true;
            this.DGV.RowHeadersVisible = false;
            this.DGV.RowHeadersWidth = 51;
            this.DGV.RowTemplate.Height = 24;
            this.DGV.Size = new System.Drawing.Size(816, 307);
            this.DGV.TabIndex = 6;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewCellStyle9.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle9;
            this.dataGridViewTextBoxColumn2.HeaderText = "ชื่อ - นามสกุล";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 225;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "รายการ";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 220;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "รูปแบบ";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 170;
            // 
            // Column7
            // 
            this.Column7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column7.HeaderText = "จำนวนเงิน";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            // 
            // DTP
            // 
            this.DTP.Location = new System.Drawing.Point(40, 18);
            this.DTP.Name = "DTP";
            this.DTP.Size = new System.Drawing.Size(200, 35);
            this.DTP.TabIndex = 7;
            this.DTP.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // BExitForm
            // 
            this.BExitForm.BackgroundImage = global::BankTeacher.Properties.Resources.logout;
            this.BExitForm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.BExitForm.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.BExitForm.Location = new System.Drawing.Point(766, 524);
            this.BExitForm.Name = "BExitForm";
            this.BExitForm.Size = new System.Drawing.Size(73, 66);
            this.BExitForm.TabIndex = 116;
            this.BExitForm.UseVisualStyleBackColor = true;
            this.BExitForm.Click += new System.EventHandler(this.BExitForm_Click);
            // 
            // ReportEpenses
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 28F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(907, 630);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("TH Sarabun New", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.Name = "ReportEpenses";
            this.Text = "ReportEpenses";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ReportEpenses_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DateTimePicker DTP;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button BSearchTeacher;
        private System.Windows.Forms.TextBox TBTeacherName;
        public System.Windows.Forms.TextBox TBTeacherNo;
        private System.Windows.Forms.Label LB2Ne;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label LB1Id;
        private System.Windows.Forms.TextBox TBAmountWithDraw;
        private System.Windows.Forms.TextBox TBAmountLoan;
        private System.Windows.Forms.TextBox TBAmount;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DataGridView DGV;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.Button BExitForm;
    }
}