
namespace BankTeacher.Bank
{
    partial class CancelDividend
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.BExitForm = new System.Windows.Forms.Button();
            this.BSaveCancelDividend = new System.Windows.Forms.Button();
            this.CBYear = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.LRemainInterest = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.LDividendAmount = new System.Windows.Forms.Label();
            this.LInterestNextYear = new System.Windows.Forms.Label();
            this.LDividendPerShare = new System.Windows.Forms.Label();
            this.LSavingAmount = new System.Windows.Forms.Label();
            this.LInterestAmount = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.DGVReportDividend = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGVReportDividend)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.BExitForm);
            this.panel1.Controls.Add(this.BSaveCancelDividend);
            this.panel1.Controls.Add(this.CBYear);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.LRemainInterest);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.LDividendAmount);
            this.panel1.Controls.Add(this.LInterestNextYear);
            this.panel1.Controls.Add(this.LDividendPerShare);
            this.panel1.Controls.Add(this.LSavingAmount);
            this.panel1.Controls.Add(this.LInterestAmount);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.DGVReportDividend);
            this.panel1.Location = new System.Drawing.Point(15, 16);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(877, 590);
            this.panel1.TabIndex = 1;
            // 
            // BExitForm
            // 
            this.BExitForm.BackgroundImage = global::BankTeacher.Properties.Resources.logout;
            this.BExitForm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.BExitForm.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.BExitForm.Location = new System.Drawing.Point(759, 521);
            this.BExitForm.Name = "BExitForm";
            this.BExitForm.Size = new System.Drawing.Size(73, 66);
            this.BExitForm.TabIndex = 114;
            this.BExitForm.UseVisualStyleBackColor = true;
            this.BExitForm.Click += new System.EventHandler(this.BExitForm_Click);
            // 
            // BSaveCancelDividend
            // 
            this.BSaveCancelDividend.Enabled = false;
            this.BSaveCancelDividend.Location = new System.Drawing.Point(644, 444);
            this.BSaveCancelDividend.Name = "BSaveCancelDividend";
            this.BSaveCancelDividend.Size = new System.Drawing.Size(188, 73);
            this.BSaveCancelDividend.TabIndex = 103;
            this.BSaveCancelDividend.Text = "บันทึกยกเลิกการปันผล";
            this.BSaveCancelDividend.UseVisualStyleBackColor = true;
            this.BSaveCancelDividend.Click += new System.EventHandler(this.BSaveCancelDividend_Click);
            // 
            // CBYear
            // 
            this.CBYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBYear.FormattingEnabled = true;
            this.CBYear.Location = new System.Drawing.Point(87, 24);
            this.CBYear.Name = "CBYear";
            this.CBYear.Size = new System.Drawing.Size(140, 36);
            this.CBYear.TabIndex = 102;
            this.CBYear.SelectedIndexChanged += new System.EventHandler(this.CBYear_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("TH Sarabun New", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label5.Location = new System.Drawing.Point(630, 115);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(129, 32);
            this.label5.TabIndex = 98;
            this.label5.Text = "จำนวนเงินปันผล :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("TH Sarabun New", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label7.Location = new System.Drawing.Point(630, 83);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(132, 32);
            this.label7.TabIndex = 98;
            this.label7.Text = "ปันผลเฉลี่ยต่อหุ้น :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("TH Sarabun New", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label6.Location = new System.Drawing.Point(368, 115);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(153, 32);
            this.label6.TabIndex = 99;
            this.label6.Text = "ดอกเบี้ยไว้จ่ายปีหน้า :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("TH Sarabun New", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label4.Location = new System.Drawing.Point(369, 78);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 32);
            this.label4.TabIndex = 100;
            this.label4.Text = "เงินหุ้นสะสม :";
            // 
            // LRemainInterest
            // 
            this.LRemainInterest.AutoSize = true;
            this.LRemainInterest.Font = new System.Drawing.Font("TH Sarabun New", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.LRemainInterest.ForeColor = System.Drawing.Color.Black;
            this.LRemainInterest.Location = new System.Drawing.Point(259, 78);
            this.LRemainInterest.Name = "LRemainInterest";
            this.LRemainInterest.Size = new System.Drawing.Size(24, 32);
            this.LRemainInterest.TabIndex = 101;
            this.LRemainInterest.Text = "0";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("TH Sarabun New", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label11.Location = new System.Drawing.Point(24, 24);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(57, 32);
            this.label11.TabIndex = 101;
            this.label11.Text = "เลือกปี";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("TH Sarabun New", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.Location = new System.Drawing.Point(47, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(215, 32);
            this.label2.TabIndex = 101;
            this.label2.Text = "ดอกเบี้ยคงเหลือจากปี่ที่ผ่านมา :";
            // 
            // LDividendAmount
            // 
            this.LDividendAmount.AutoSize = true;
            this.LDividendAmount.Font = new System.Drawing.Font("TH Sarabun New", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.LDividendAmount.ForeColor = System.Drawing.Color.Black;
            this.LDividendAmount.Location = new System.Drawing.Point(758, 115);
            this.LDividendAmount.Name = "LDividendAmount";
            this.LDividendAmount.Size = new System.Drawing.Size(24, 32);
            this.LDividendAmount.TabIndex = 101;
            this.LDividendAmount.Text = "0";
            // 
            // LInterestNextYear
            // 
            this.LInterestNextYear.AutoSize = true;
            this.LInterestNextYear.Font = new System.Drawing.Font("TH Sarabun New", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.LInterestNextYear.ForeColor = System.Drawing.Color.Black;
            this.LInterestNextYear.Location = new System.Drawing.Point(521, 115);
            this.LInterestNextYear.Name = "LInterestNextYear";
            this.LInterestNextYear.Size = new System.Drawing.Size(24, 32);
            this.LInterestNextYear.TabIndex = 101;
            this.LInterestNextYear.Text = "0";
            // 
            // LDividendPerShare
            // 
            this.LDividendPerShare.AutoSize = true;
            this.LDividendPerShare.Font = new System.Drawing.Font("TH Sarabun New", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.LDividendPerShare.ForeColor = System.Drawing.Color.Black;
            this.LDividendPerShare.Location = new System.Drawing.Point(758, 83);
            this.LDividendPerShare.Name = "LDividendPerShare";
            this.LDividendPerShare.Size = new System.Drawing.Size(24, 32);
            this.LDividendPerShare.TabIndex = 101;
            this.LDividendPerShare.Text = "0";
            // 
            // LSavingAmount
            // 
            this.LSavingAmount.AutoSize = true;
            this.LSavingAmount.Font = new System.Drawing.Font("TH Sarabun New", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.LSavingAmount.ForeColor = System.Drawing.Color.Black;
            this.LSavingAmount.Location = new System.Drawing.Point(468, 78);
            this.LSavingAmount.Name = "LSavingAmount";
            this.LSavingAmount.Size = new System.Drawing.Size(24, 32);
            this.LSavingAmount.TabIndex = 101;
            this.LSavingAmount.Text = "0";
            // 
            // LInterestAmount
            // 
            this.LInterestAmount.AutoSize = true;
            this.LInterestAmount.Font = new System.Drawing.Font("TH Sarabun New", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.LInterestAmount.ForeColor = System.Drawing.Color.Black;
            this.LInterestAmount.Location = new System.Drawing.Point(163, 115);
            this.LInterestAmount.Name = "LInterestAmount";
            this.LInterestAmount.Size = new System.Drawing.Size(24, 32);
            this.LInterestAmount.TabIndex = 101;
            this.LInterestAmount.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("TH Sarabun New", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label3.Location = new System.Drawing.Point(47, 115);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 32);
            this.label3.TabIndex = 101;
            this.label3.Text = "จำนวนดอกเบี้ย :";
            // 
            // DGVReportDividend
            // 
            this.DGVReportDividend.AllowUserToAddRows = false;
            this.DGVReportDividend.AllowUserToDeleteRows = false;
            this.DGVReportDividend.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("TH Sarabun New", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.DGVReportDividend.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.DGVReportDividend.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("TH Sarabun New", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGVReportDividend.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.DGVReportDividend.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVReportDividend.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn2,
            this.Column2,
            this.dataGridViewTextBoxColumn4,
            this.Column1});
            this.DGVReportDividend.GridColor = System.Drawing.SystemColors.Control;
            this.DGVReportDividend.Location = new System.Drawing.Point(53, 163);
            this.DGVReportDividend.Name = "DGVReportDividend";
            this.DGVReportDividend.RowHeadersVisible = false;
            this.DGVReportDividend.RowHeadersWidth = 51;
            this.DGVReportDividend.RowTemplate.Height = 24;
            this.DGVReportDividend.Size = new System.Drawing.Size(779, 263);
            this.DGVReportDividend.TabIndex = 97;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewTextBoxColumn2.HeaderText = "ชื่อ - นามสกุล";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "เงินหุ้นสะสม";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 200;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "จำนวนเงินปันผล";
            this.dataGridViewTextBoxColumn4.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 200;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.HeaderText = "ลายมือผู้รับเงิน";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(275, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(145, 28);
            this.label1.TabIndex = 115;
            this.label1.Text = "ไม่มีปีที่สามารถยกเลิกได้";
            this.label1.Visible = false;
            // 
            // CancelDividend
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 28F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(907, 622);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("TH Sarabun New", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.Name = "CancelDividend";
            this.Text = "CancelDividend";
            this.Load += new System.EventHandler(this.CancelDividend_Load);
            this.SizeChanged += new System.EventHandler(this.CancelDividend_SizeChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CancelDividend_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGVReportDividend)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button BSaveCancelDividend;
        private System.Windows.Forms.ComboBox CBYear;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label LRemainInterest;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label LDividendAmount;
        private System.Windows.Forms.Label LInterestNextYear;
        private System.Windows.Forms.Label LDividendPerShare;
        private System.Windows.Forms.Label LSavingAmount;
        private System.Windows.Forms.Label LInterestAmount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView DGVReportDividend;
        private System.Windows.Forms.Button BExitForm;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.Label label1;
    }
}