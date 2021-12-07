
namespace BankTeacher.Bank
{
    partial class ReportEpensesAll
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
            this.DGV = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DTP = new System.Windows.Forms.DateTimePicker();
            this.TBAmountWithDraw = new System.Windows.Forms.TextBox();
            this.TBAmountLoan = new System.Windows.Forms.TextBox();
            this.TBAmount = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.BExitForm = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.BExitForm);
            this.panel1.Controls.Add(this.DGV);
            this.panel1.Controls.Add(this.DTP);
            this.panel1.Controls.Add(this.TBAmountWithDraw);
            this.panel1.Controls.Add(this.TBAmountLoan);
            this.panel1.Controls.Add(this.TBAmount);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(15, 16);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(877, 590);
            this.panel1.TabIndex = 2;
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
            this.Column1,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn7});
            this.DGV.GridColor = System.Drawing.SystemColors.Control;
            this.DGV.Location = new System.Drawing.Point(15, 180);
            this.DGV.Name = "DGV";
            this.DGV.ReadOnly = true;
            this.DGV.RowHeadersVisible = false;
            this.DGV.RowHeadersWidth = 51;
            this.DGV.RowTemplate.Height = 24;
            this.DGV.Size = new System.Drawing.Size(841, 313);
            this.DGV.TabIndex = 21;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "ชื่อผู้ทำรายการ";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 200;
            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewCellStyle9.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridViewTextBoxColumn4.DefaultCellStyle = dataGridViewCellStyle9;
            this.dataGridViewTextBoxColumn4.HeaderText = "ชื่อ - นามสกุล";
            this.dataGridViewTextBoxColumn4.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 225;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "รายการ";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 175;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "รูปแบบ";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Width = 125;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn7.HeaderText = "จำนวนเงิน";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            // 
            // DTP
            // 
            this.DTP.Location = new System.Drawing.Point(40, 18);
            this.DTP.Name = "DTP";
            this.DTP.Size = new System.Drawing.Size(200, 35);
            this.DTP.TabIndex = 7;
            this.DTP.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // TBAmountWithDraw
            // 
            this.TBAmountWithDraw.Enabled = false;
            this.TBAmountWithDraw.Location = new System.Drawing.Point(721, 97);
            this.TBAmountWithDraw.Name = "TBAmountWithDraw";
            this.TBAmountWithDraw.Size = new System.Drawing.Size(133, 35);
            this.TBAmountWithDraw.TabIndex = 18;
            this.TBAmountWithDraw.Text = "0";
            // 
            // TBAmountLoan
            // 
            this.TBAmountLoan.Enabled = false;
            this.TBAmountLoan.Location = new System.Drawing.Point(415, 94);
            this.TBAmountLoan.Name = "TBAmountLoan";
            this.TBAmountLoan.Size = new System.Drawing.Size(133, 35);
            this.TBAmountLoan.TabIndex = 19;
            this.TBAmountLoan.Text = "0";
            // 
            // TBAmount
            // 
            this.TBAmount.Enabled = false;
            this.TBAmount.Location = new System.Drawing.Point(168, 94);
            this.TBAmount.Name = "TBAmount";
            this.TBAmount.Size = new System.Drawing.Size(133, 35);
            this.TBAmount.TabIndex = 20;
            this.TBAmount.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 28);
            this.label3.TabIndex = 17;
            this.label3.Text = "จำนวนเงินทั้งหมด";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(554, 100);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(146, 28);
            this.label1.TabIndex = 15;
            this.label1.Text = "จำนวนเงินถอนหุ้นสะสม";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(307, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 28);
            this.label2.TabIndex = 16;
            this.label2.Text = "จำนวนเงินจ่ายกู้";
            // 
            // BExitForm
            // 
            this.BExitForm.BackgroundImage = global::BankTeacher.Properties.Resources.logout;
            this.BExitForm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.BExitForm.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.BExitForm.Location = new System.Drawing.Point(781, 510);
            this.BExitForm.Name = "BExitForm";
            this.BExitForm.Size = new System.Drawing.Size(73, 66);
            this.BExitForm.TabIndex = 117;
            this.BExitForm.UseVisualStyleBackColor = true;
            this.BExitForm.Click += new System.EventHandler(this.BExitForm_Click);
            // 
            // ReportEpensesAll
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 28F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(907, 622);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("TH Sarabun New", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.Name = "ReportEpensesAll";
            this.Text = "ReportEpenses";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ReportEpensesAll_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DateTimePicker DTP;
        private System.Windows.Forms.DataGridView DGV;
        private System.Windows.Forms.TextBox TBAmountWithDraw;
        private System.Windows.Forms.TextBox TBAmountLoan;
        private System.Windows.Forms.TextBox TBAmount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.Button BExitForm;
    }
}