
namespace BankTeacher.Bank
{
    partial class ReportDividend
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportDividend));
            this.panel1 = new System.Windows.Forms.Panel();
            this.BExitForm = new System.Windows.Forms.Button();
            this.CBYear = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.DGVReportDividend = new System.Windows.Forms.DataGridView();
            this.C1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.BTPrint = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.TB_DividendPerShare = new System.Windows.Forms.TextBox();
            this.TB_DividendAmount = new System.Windows.Forms.TextBox();
            this.TB_SavingAmount = new System.Windows.Forms.TextBox();
            this.TB_InterestNextYear = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TB_InterestAmount = new System.Windows.Forms.TextBox();
            this.TB_RemainInterest = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGVReportDividend)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.BExitForm);
            this.panel1.Controls.Add(this.CBYear);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.DGVReportDividend);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Location = new System.Drawing.Point(42, 14);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(879, 636);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // BExitForm
            // 
            this.BExitForm.BackgroundImage = global::BankTeacher.Properties.Resources.logout;
            this.BExitForm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.BExitForm.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.BExitForm.Location = new System.Drawing.Point(798, 558);
            this.BExitForm.Name = "BExitForm";
            this.BExitForm.Size = new System.Drawing.Size(73, 66);
            this.BExitForm.TabIndex = 114;
            this.BExitForm.UseVisualStyleBackColor = true;
            this.BExitForm.Click += new System.EventHandler(this.BExitForm_Click);
            // 
            // CBYear
            // 
            this.CBYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBYear.FormattingEnabled = true;
            this.CBYear.Location = new System.Drawing.Point(102, 10);
            this.CBYear.Name = "CBYear";
            this.CBYear.Size = new System.Drawing.Size(203, 42);
            this.CBYear.TabIndex = 102;
            this.CBYear.SelectedIndexChanged += new System.EventHandler(this.CBYear_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("TH Sarabun New", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label11.Location = new System.Drawing.Point(24, 10);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(72, 40);
            this.label11.TabIndex = 101;
            this.label11.Text = "เลือกปี";
            // 
            // DGVReportDividend
            // 
            this.DGVReportDividend.AllowUserToAddRows = false;
            this.DGVReportDividend.AllowUserToDeleteRows = false;
            this.DGVReportDividend.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
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
            this.C1,
            this.dataGridViewTextBoxColumn2,
            this.Column2,
            this.dataGridViewTextBoxColumn4,
            this.Column1});
            this.DGVReportDividend.GridColor = System.Drawing.SystemColors.Control;
            this.DGVReportDividend.Location = new System.Drawing.Point(4, 273);
            this.DGVReportDividend.Name = "DGVReportDividend";
            this.DGVReportDividend.RowHeadersVisible = false;
            this.DGVReportDividend.RowHeadersWidth = 51;
            this.DGVReportDividend.RowTemplate.Height = 24;
            this.DGVReportDividend.Size = new System.Drawing.Size(871, 279);
            this.DGVReportDividend.TabIndex = 97;
            // 
            // C1
            // 
            this.C1.HeaderText = "ลำดับที่";
            this.C1.MinimumWidth = 6;
            this.C1.Name = "C1";
            this.C1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.C1.Width = 125;
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
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "เงินหุ้นสะสม";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column2.Width = 200;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "จำนวนเงินปันผล";
            this.dataGridViewTextBoxColumn4.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn4.Width = 200;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.HeaderText = "ลายมือผู้รับเงิน";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column1.Visible = false;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.BTPrint);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.TB_DividendPerShare);
            this.panel2.Controls.Add(this.TB_DividendAmount);
            this.panel2.Controls.Add(this.TB_SavingAmount);
            this.panel2.Controls.Add(this.TB_InterestNextYear);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.TB_InterestAmount);
            this.panel2.Controls.Add(this.TB_RemainInterest);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Location = new System.Drawing.Point(4, 73);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(870, 194);
            this.panel2.TabIndex = 121;
            // 
            // BTPrint
            // 
            this.BTPrint.BackColor = System.Drawing.Color.White;
            this.BTPrint.BackgroundImage = global::BankTeacher.Properties.Resources._10x10_Print;
            this.BTPrint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.BTPrint.CausesValidation = false;
            this.BTPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTPrint.Location = new System.Drawing.Point(769, 132);
            this.BTPrint.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.BTPrint.Name = "BTPrint";
            this.BTPrint.Size = new System.Drawing.Size(95, 54);
            this.BTPrint.TabIndex = 128;
            this.BTPrint.UseVisualStyleBackColor = false;
            this.BTPrint.Click += new System.EventHandler(this.BTPrint_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(701, 142);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 36);
            this.label1.TabIndex = 129;
            this.label1.Text = "Print :";
            // 
            // TB_DividendPerShare
            // 
            this.TB_DividendPerShare.Enabled = false;
            this.TB_DividendPerShare.Location = new System.Drawing.Point(543, 56);
            this.TB_DividendPerShare.Name = "TB_DividendPerShare";
            this.TB_DividendPerShare.Size = new System.Drawing.Size(124, 42);
            this.TB_DividendPerShare.TabIndex = 120;
            // 
            // TB_DividendAmount
            // 
            this.TB_DividendAmount.Enabled = false;
            this.TB_DividendAmount.Location = new System.Drawing.Point(543, 144);
            this.TB_DividendAmount.Name = "TB_DividendAmount";
            this.TB_DividendAmount.Size = new System.Drawing.Size(124, 42);
            this.TB_DividendAmount.TabIndex = 119;
            // 
            // TB_SavingAmount
            // 
            this.TB_SavingAmount.Enabled = false;
            this.TB_SavingAmount.Location = new System.Drawing.Point(316, 56);
            this.TB_SavingAmount.Name = "TB_SavingAmount";
            this.TB_SavingAmount.Size = new System.Drawing.Size(124, 42);
            this.TB_SavingAmount.TabIndex = 118;
            // 
            // TB_InterestNextYear
            // 
            this.TB_InterestNextYear.Enabled = false;
            this.TB_InterestNextYear.Location = new System.Drawing.Point(316, 144);
            this.TB_InterestNextYear.Name = "TB_InterestNextYear";
            this.TB_InterestNextYear.Size = new System.Drawing.Size(124, 42);
            this.TB_InterestNextYear.TabIndex = 117;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("TH Sarabun New", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.Location = new System.Drawing.Point(3, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(267, 40);
            this.label2.TabIndex = 101;
            this.label2.Text = "ดอกเบี้ยคงเหลือจากปี่ที่ผ่านมา ";
            // 
            // TB_InterestAmount
            // 
            this.TB_InterestAmount.Enabled = false;
            this.TB_InterestAmount.Location = new System.Drawing.Point(50, 144);
            this.TB_InterestAmount.Name = "TB_InterestAmount";
            this.TB_InterestAmount.Size = new System.Drawing.Size(124, 42);
            this.TB_InterestAmount.TabIndex = 116;
            // 
            // TB_RemainInterest
            // 
            this.TB_RemainInterest.Enabled = false;
            this.TB_RemainInterest.Location = new System.Drawing.Point(50, 56);
            this.TB_RemainInterest.Name = "TB_RemainInterest";
            this.TB_RemainInterest.Size = new System.Drawing.Size(124, 42);
            this.TB_RemainInterest.TabIndex = 115;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("TH Sarabun New", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label5.Location = new System.Drawing.Point(527, 101);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(158, 40);
            this.label5.TabIndex = 98;
            this.label5.Text = "จำนวนเงินปันผล ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("TH Sarabun New", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label7.Location = new System.Drawing.Point(525, 13);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(158, 40);
            this.label7.TabIndex = 98;
            this.label7.Text = "ปันผลเฉลี่ยต่อหุ้น";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("TH Sarabun New", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label6.Location = new System.Drawing.Point(293, 101);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(186, 40);
            this.label6.TabIndex = 99;
            this.label6.Text = "ดอกเบี้ยไว้จ่ายปีหน้า ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("TH Sarabun New", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label4.Location = new System.Drawing.Point(316, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(124, 40);
            this.label4.TabIndex = 100;
            this.label4.Text = "เงินหุ้นสะสม ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("TH Sarabun New", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label3.Location = new System.Drawing.Point(43, 101);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(146, 40);
            this.label3.TabIndex = 101;
            this.label3.Text = "จำนวนดอกเบี้ย ";
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Document = this.printDocument1;
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            // 
            // ReportDividend
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 34F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(962, 665);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("TH Sarabun New", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.Name = "ReportDividend";
            this.Text = "ReportDividend";
            this.Load += new System.EventHandler(this.ReportDividend_Load);
            this.SizeChanged += new System.EventHandler(this.ReportDividend_SizeChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ReportDividend_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGVReportDividend)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView DGVReportDividend;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox CBYear;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button BExitForm;
        private System.Windows.Forms.TextBox TB_RemainInterest;
        private System.Windows.Forms.TextBox TB_DividendPerShare;
        private System.Windows.Forms.TextBox TB_DividendAmount;
        private System.Windows.Forms.TextBox TB_SavingAmount;
        private System.Windows.Forms.TextBox TB_InterestNextYear;
        private System.Windows.Forms.TextBox TB_InterestAmount;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button BTPrint;
        private System.Windows.Forms.Label label1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Windows.Forms.DataGridViewTextBoxColumn C1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
    }
}