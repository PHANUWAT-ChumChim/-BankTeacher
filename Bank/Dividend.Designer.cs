
namespace BankTeacher.Bank
{
    partial class Dividend
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.DGV = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label12 = new System.Windows.Forms.Label();
            this.TBInterestNextYear = new System.Windows.Forms.TextBox();
            this.TBRemainInterest = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.TBSavingAmount = new System.Windows.Forms.TextBox();
            this.TBDividendPerShare = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.TBDividendAmount = new System.Windows.Forms.TextBox();
            this.TBInterestAmount = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.CBYearDividend = new System.Windows.Forms.ComboBox();
            this.BExitForm = new System.Windows.Forms.Button();
            this.BSaveDividend = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.BExitForm);
            this.panel1.Controls.Add(this.BSaveDividend);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(15, 16);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(877, 622);
            this.panel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.DGV);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.TBInterestNextYear);
            this.groupBox1.Controls.Add(this.TBRemainInterest);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.TBSavingAmount);
            this.groupBox1.Controls.Add(this.TBDividendPerShare);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.TBDividendAmount);
            this.groupBox1.Controls.Add(this.TBInterestAmount);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(11, 69);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(846, 394);
            this.groupBox1.TabIndex = 124;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ตัวอย่างปันผล";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("TH Sarabun New", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label11.Location = new System.Drawing.Point(489, 31);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(103, 32);
            this.label11.TabIndex = 119;
            this.label11.Text = "เงินหุ้นสะสม :";
            // 
            // DGV
            // 
            this.DGV.AllowUserToAddRows = false;
            this.DGV.AllowUserToDeleteRows = false;
            this.DGV.AllowUserToResizeColumns = false;
            this.DGV.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.DGV.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.DGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
            this.DGV.Location = new System.Drawing.Point(25, 160);
            this.DGV.Name = "DGV";
            this.DGV.ReadOnly = true;
            this.DGV.RowHeadersVisible = false;
            this.DGV.Size = new System.Drawing.Size(797, 219);
            this.DGV.TabIndex = 123;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "รหัส";
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
            this.Column3.HeaderText = "จำนวนเงินในหุ้นสะสม";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 175;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "จำนวนเงินปันผล";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 175;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("TH Sarabun New", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label12.Location = new System.Drawing.Point(362, 28);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(43, 32);
            this.label12.TabIndex = 118;
            this.label12.Text = "บาท";
            // 
            // TBInterestNextYear
            // 
            this.TBInterestNextYear.Enabled = false;
            this.TBInterestNextYear.Location = new System.Drawing.Point(223, 114);
            this.TBInterestNextYear.Name = "TBInterestNextYear";
            this.TBInterestNextYear.Size = new System.Drawing.Size(133, 35);
            this.TBInterestNextYear.TabIndex = 117;
            this.TBInterestNextYear.Text = "0";
            this.TBInterestNextYear.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // TBRemainInterest
            // 
            this.TBRemainInterest.Enabled = false;
            this.TBRemainInterest.Location = new System.Drawing.Point(223, 70);
            this.TBRemainInterest.Name = "TBRemainInterest";
            this.TBRemainInterest.Size = new System.Drawing.Size(133, 35);
            this.TBRemainInterest.TabIndex = 117;
            this.TBRemainInterest.Text = "0";
            this.TBRemainInterest.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("TH Sarabun New", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label7.Location = new System.Drawing.Point(460, 117);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(132, 32);
            this.label7.TabIndex = 121;
            this.label7.Text = "ปันผลเฉลี่ยต่อหุ้น :";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("TH Sarabun New", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label14.Location = new System.Drawing.Point(727, 78);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(43, 32);
            this.label14.TabIndex = 118;
            this.label14.Text = "บาท";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(79, 114);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(134, 28);
            this.label10.TabIndex = 116;
            this.label10.Text = "ดอกเบี้ยไว้จ่ายปีหน้า :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(29, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(188, 28);
            this.label3.TabIndex = 116;
            this.label3.Text = "ดอกเบี้ยคงเหลือจากปี่ที่ผ่านมา :";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("TH Sarabun New", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label13.Location = new System.Drawing.Point(725, 117);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(43, 32);
            this.label13.TabIndex = 118;
            this.label13.Text = "บาท";
            // 
            // TBSavingAmount
            // 
            this.TBSavingAmount.Enabled = false;
            this.TBSavingAmount.Location = new System.Drawing.Point(598, 28);
            this.TBSavingAmount.Name = "TBSavingAmount";
            this.TBSavingAmount.Size = new System.Drawing.Size(123, 35);
            this.TBSavingAmount.TabIndex = 117;
            this.TBSavingAmount.Text = "0";
            this.TBSavingAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // TBDividendPerShare
            // 
            this.TBDividendPerShare.Enabled = false;
            this.TBDividendPerShare.Location = new System.Drawing.Point(598, 117);
            this.TBDividendPerShare.Name = "TBDividendPerShare";
            this.TBDividendPerShare.Size = new System.Drawing.Size(121, 35);
            this.TBDividendPerShare.TabIndex = 117;
            this.TBDividendPerShare.Text = "0";
            this.TBDividendPerShare.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("TH Sarabun New", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label15.Location = new System.Drawing.Point(727, 28);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(43, 32);
            this.label15.TabIndex = 118;
            this.label15.Text = "บาท";
            // 
            // TBDividendAmount
            // 
            this.TBDividendAmount.Enabled = false;
            this.TBDividendAmount.Location = new System.Drawing.Point(598, 73);
            this.TBDividendAmount.Name = "TBDividendAmount";
            this.TBDividendAmount.Size = new System.Drawing.Size(121, 35);
            this.TBDividendAmount.TabIndex = 117;
            this.TBDividendAmount.Text = "0";
            this.TBDividendAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // TBInterestAmount
            // 
            this.TBInterestAmount.Enabled = false;
            this.TBInterestAmount.Location = new System.Drawing.Point(223, 28);
            this.TBInterestAmount.Name = "TBInterestAmount";
            this.TBInterestAmount.Size = new System.Drawing.Size(133, 35);
            this.TBInterestAmount.TabIndex = 117;
            this.TBInterestAmount.Text = "0";
            this.TBInterestAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("TH Sarabun New", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label9.Location = new System.Drawing.Point(362, 114);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(43, 32);
            this.label9.TabIndex = 118;
            this.label9.Text = "บาท";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("TH Sarabun New", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label6.Location = new System.Drawing.Point(463, 76);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(129, 32);
            this.label6.TabIndex = 120;
            this.label6.Text = "จำนวนเงินปันผล :";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("TH Sarabun New", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label8.Location = new System.Drawing.Point(362, 70);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(43, 32);
            this.label8.TabIndex = 118;
            this.label8.Text = "บาท";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(65, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(152, 28);
            this.label4.TabIndex = 116;
            this.label4.Text = "จำนวนดอกเบี้ยภายในปี :";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.CBYearDividend);
            this.panel2.Location = new System.Drawing.Point(12, 9);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(212, 54);
            this.panel2.TabIndex = 115;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(21, 28);
            this.label5.TabIndex = 8;
            this.label5.Text = "ปี";
            // 
            // CBYearDividend
            // 
            this.CBYearDividend.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBYearDividend.FormattingEnabled = true;
            this.CBYearDividend.Location = new System.Drawing.Point(45, 6);
            this.CBYearDividend.Name = "CBYearDividend";
            this.CBYearDividend.Size = new System.Drawing.Size(121, 36);
            this.CBYearDividend.TabIndex = 7;
            this.CBYearDividend.SelectedIndexChanged += new System.EventHandler(this.CBYearDividend_SelectedIndexChanged);
            // 
            // BExitForm
            // 
            this.BExitForm.BackgroundImage = global::BankTeacher.Properties.Resources.logout;
            this.BExitForm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.BExitForm.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.BExitForm.Location = new System.Drawing.Point(786, 548);
            this.BExitForm.Name = "BExitForm";
            this.BExitForm.Size = new System.Drawing.Size(73, 66);
            this.BExitForm.TabIndex = 114;
            this.BExitForm.UseVisualStyleBackColor = true;
            this.BExitForm.Click += new System.EventHandler(this.BExitForm_Click);
            // 
            // BSaveDividend
            // 
            this.BSaveDividend.Enabled = false;
            this.BSaveDividend.Location = new System.Drawing.Point(691, 466);
            this.BSaveDividend.Name = "BSaveDividend";
            this.BSaveDividend.Size = new System.Drawing.Size(167, 82);
            this.BSaveDividend.TabIndex = 6;
            this.BSaveDividend.Text = "ยืนยันการปันผล";
            this.BSaveDividend.UseVisualStyleBackColor = true;
            this.BSaveDividend.Click += new System.EventHandler(this.BSaveDividend_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(181, 389);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 28);
            this.label2.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(352, 466);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(333, 84);
            this.label1.TabIndex = 5;
            this.label1.Text = "      **เมื่อปันผลแล้วจะไม่สามารถจ่ายเงินกู้ภายในปีนั้นได้\r\nเมื่อมีผู้ที่ต้องการก" +
    "ู้ต้องเริ่มชำระภายในปีหน้า\r\nและไม่สามารถยกเลิกบิลล์ที่ชำระมาก่อนก่อนปันผลได้\r\n";
            // 
            // Dividend
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 28F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(905, 650);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("TH Sarabun New", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.Name = "Dividend";
            this.Text = "0";
            this.Load += new System.EventHandler(this.Dividend_Load);
            this.SizeChanged += new System.EventHandler(this.Dividend_SizeChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Dividend_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox CBYearDividend;
        private System.Windows.Forms.Button BSaveDividend;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BExitForm;
        private System.Windows.Forms.TextBox TBDividendAmount;
        private System.Windows.Forms.TextBox TBSavingAmount;
        private System.Windows.Forms.TextBox TBDividendPerShare;
        private System.Windows.Forms.TextBox TBInterestAmount;
        private System.Windows.Forms.TextBox TBRemainInterest;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView DGV;
        private System.Windows.Forms.TextBox TBInterestNextYear;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
    }
}