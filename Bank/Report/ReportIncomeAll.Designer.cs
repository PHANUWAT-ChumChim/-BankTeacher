
namespace BankTeacher.Bank
{
    partial class ReportIncomeAll
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportIncomeAll));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.BExitForm = new System.Windows.Forms.Button();
            this.DGV_All = new System.Windows.Forms.DataGridView();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DTP = new System.Windows.Forms.DateTimePicker();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.TBAmount_All = new System.Windows.Forms.TextBox();
            this.BTPrint = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.TBAmountCradit_All = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TBAmountCash_All = new System.Windows.Forms.TextBox();
            this.TBAmountTranfer_All = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_All)).BeginInit();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
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
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.BExitForm);
            this.panel1.Controls.Add(this.DGV_All);
            this.panel1.Controls.Add(this.DTP);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(13, 13);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(897, 665);
            this.panel1.TabIndex = 2;
            // 
            // BExitForm
            // 
            this.BExitForm.BackgroundImage = global::BankTeacher.Properties.Resources.logout;
            this.BExitForm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.BExitForm.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.BExitForm.Location = new System.Drawing.Point(766, 585);
            this.BExitForm.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.BExitForm.Name = "BExitForm";
            this.BExitForm.Size = new System.Drawing.Size(115, 62);
            this.BExitForm.TabIndex = 118;
            this.BExitForm.UseVisualStyleBackColor = true;
            this.BExitForm.Click += new System.EventHandler(this.BExitForm_Click_1);
            // 
            // DGV_All
            // 
            this.DGV_All.AllowUserToAddRows = false;
            this.DGV_All.AllowUserToDeleteRows = false;
            this.DGV_All.AllowUserToResizeRows = false;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.DGV_All.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle7;
            this.DGV_All.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGV_All.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.DGV_All.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV_All.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column2,
            this.dataGridViewTextBoxColumn5,
            this.Column1,
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8,
            this.dataGridViewTextBoxColumn9,
            this.dataGridViewTextBoxColumn10});
            this.DGV_All.GridColor = System.Drawing.SystemColors.Control;
            this.DGV_All.Location = new System.Drawing.Point(20, 219);
            this.DGV_All.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.DGV_All.Name = "DGV_All";
            this.DGV_All.ReadOnly = true;
            this.DGV_All.RowHeadersVisible = false;
            this.DGV_All.RowHeadersWidth = 51;
            this.DGV_All.RowTemplate.Height = 24;
            this.DGV_All.Size = new System.Drawing.Size(861, 354);
            this.DGV_All.TabIndex = 8;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "ลำดับที่";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column2.Visible = false;
            this.Column2.Width = 50;
            // 
            // dataGridViewTextBoxColumn5
            // 
            dataGridViewCellStyle9.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridViewTextBoxColumn5.DefaultCellStyle = dataGridViewCellStyle9;
            this.dataGridViewTextBoxColumn5.HeaderText = "เลขบิล";
            this.dataGridViewTextBoxColumn5.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn5.Width = 125;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "ชื่อผู้ทำรายการ";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column1.Width = 150;
            // 
            // dataGridViewTextBoxColumn7
            // 
            dataGridViewCellStyle10.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridViewTextBoxColumn7.DefaultCellStyle = dataGridViewCellStyle10;
            this.dataGridViewTextBoxColumn7.HeaderText = "ชื่อ - นามสกุล";
            this.dataGridViewTextBoxColumn7.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn7.Width = 200;
            // 
            // dataGridViewTextBoxColumn8
            // 
            dataGridViewCellStyle11.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.dataGridViewTextBoxColumn8.DefaultCellStyle = dataGridViewCellStyle11;
            this.dataGridViewTextBoxColumn8.HeaderText = "รายการ";
            this.dataGridViewTextBoxColumn8.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            this.dataGridViewTextBoxColumn8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn8.Width = 150;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.HeaderText = "รูปแบบ";
            this.dataGridViewTextBoxColumn9.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.ReadOnly = true;
            this.dataGridViewTextBoxColumn9.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn9.Width = 105;
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn10.DefaultCellStyle = dataGridViewCellStyle12;
            this.dataGridViewTextBoxColumn10.HeaderText = "จำนวนเงิน";
            this.dataGridViewTextBoxColumn10.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.ReadOnly = true;
            this.dataGridViewTextBoxColumn10.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // DTP
            // 
            this.DTP.CalendarFont = new System.Drawing.Font("TH Sarabun New", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTP.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTP.Location = new System.Drawing.Point(20, 13);
            this.DTP.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.DTP.Name = "DTP";
            this.DTP.Size = new System.Drawing.Size(279, 36);
            this.DTP.TabIndex = 7;
            this.DTP.ValueChanged += new System.EventHandler(this.DTP_ValueChanged);
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel4.Controls.Add(this.label1);
            this.panel4.Controls.Add(this.TBAmount_All);
            this.panel4.Controls.Add(this.BTPrint);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.TBAmountCradit_All);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Controls.Add(this.TBAmountCash_All);
            this.panel4.Controls.Add(this.TBAmountTranfer_All);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Location = new System.Drawing.Point(20, 71);
            this.panel4.Margin = new System.Windows.Forms.Padding(4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(861, 142);
            this.panel4.TabIndex = 122;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(530, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 30);
            this.label1.TabIndex = 4;
            this.label1.Text = "จำนวนเงินทั้งหมด";
            // 
            // TBAmount_All
            // 
            this.TBAmount_All.Enabled = false;
            this.TBAmount_All.Location = new System.Drawing.Point(528, 70);
            this.TBAmount_All.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.TBAmount_All.Name = "TBAmount_All";
            this.TBAmount_All.Size = new System.Drawing.Size(189, 36);
            this.TBAmount_All.TabIndex = 5;
            this.TBAmount_All.Text = "0";
            // 
            // BTPrint
            // 
            this.BTPrint.BackColor = System.Drawing.Color.White;
            this.BTPrint.BackgroundImage = global::BankTeacher.Properties.Resources._10x10_Print;
            this.BTPrint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.BTPrint.CausesValidation = false;
            this.BTPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTPrint.Location = new System.Drawing.Point(756, 70);
            this.BTPrint.Margin = new System.Windows.Forms.Padding(5, 2, 5, 2);
            this.BTPrint.Name = "BTPrint";
            this.BTPrint.Size = new System.Drawing.Size(93, 53);
            this.BTPrint.TabIndex = 120;
            this.BTPrint.UseVisualStyleBackColor = false;
            this.BTPrint.Click += new System.EventHandler(this.BTPrint_Click_1);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(333, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(139, 30);
            this.label4.TabIndex = 4;
            this.label4.Text = "จำนวนเงินบัตรเครดิต";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(769, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 30);
            this.label5.TabIndex = 123;
            this.label5.Text = "Print :";
            // 
            // TBAmountCradit_All
            // 
            this.TBAmountCradit_All.Enabled = false;
            this.TBAmountCradit_All.Location = new System.Drawing.Point(340, 70);
            this.TBAmountCradit_All.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.TBAmountCradit_All.Name = "TBAmountCradit_All";
            this.TBAmountCradit_All.Size = new System.Drawing.Size(141, 36);
            this.TBAmountCradit_All.TabIndex = 5;
            this.TBAmountCradit_All.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 30);
            this.label2.TabIndex = 4;
            this.label2.Text = "จำนวนเงินสด";
            // 
            // TBAmountCash_All
            // 
            this.TBAmountCash_All.Enabled = false;
            this.TBAmountCash_All.Location = new System.Drawing.Point(18, 70);
            this.TBAmountCash_All.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.TBAmountCash_All.Name = "TBAmountCash_All";
            this.TBAmountCash_All.Size = new System.Drawing.Size(141, 36);
            this.TBAmountCash_All.TabIndex = 5;
            this.TBAmountCash_All.Text = "0";
            // 
            // TBAmountTranfer_All
            // 
            this.TBAmountTranfer_All.Enabled = false;
            this.TBAmountTranfer_All.Location = new System.Drawing.Point(180, 70);
            this.TBAmountTranfer_All.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.TBAmountTranfer_All.Name = "TBAmountTranfer_All";
            this.TBAmountTranfer_All.Size = new System.Drawing.Size(141, 36);
            this.TBAmountTranfer_All.TabIndex = 5;
            this.TBAmountTranfer_All.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(184, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 30);
            this.label3.TabIndex = 4;
            this.label3.Text = "จำนวนเงินโอน";
            // 
            // ReportIncomeAll
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(987, 691);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(5, 9, 5, 9);
            this.Name = "ReportIncomeAll";
            this.Text = "ReportIncome";
            this.SizeChanged += new System.EventHandler(this.ReportIncomeAll_SizeChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ReportIncomeAll_KeyDown);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGV_All)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button BExitForm;
        private System.Windows.Forms.DataGridView DGV_All;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.DateTimePicker DTP;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TBAmount_All;
        private System.Windows.Forms.Button BTPrint;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox TBAmountCradit_All;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TBAmountCash_All;
        private System.Windows.Forms.TextBox TBAmountTranfer_All;
        private System.Windows.Forms.Label label3;
    }
}