
namespace BankTeacher.Bank.Pay
{
    partial class CancelBill
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
            this.PL_Form = new System.Windows.Forms.Panel();
            this.BExitForm = new System.Windows.Forms.Button();
            this.panel12 = new System.Windows.Forms.Panel();
            this.DTPDate = new System.Windows.Forms.DateTimePicker();
            this.BSearchTeacher = new System.Windows.Forms.Button();
            this.TBTeacherName_Cancelbill = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.TBBillNo_Cancelbill = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label14 = new System.Windows.Forms.Label();
            this.TBNote = new System.Windows.Forms.TextBox();
            this.TBteacharnoby_billcancel = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TBBIllDate_Cancelbill = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel18 = new System.Windows.Forms.Panel();
            this.label27 = new System.Windows.Forms.Label();
            this.LSumAmount_CancelBill = new System.Windows.Forms.Label();
            this.DGV_Cancelbill = new System.Windows.Forms.DataGridView();
            this.Column14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column21 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BSave_Cancelbill = new System.Windows.Forms.Button();
            this.panel17 = new System.Windows.Forms.Panel();
            this.label25 = new System.Windows.Forms.Label();
            this.PL_Form.SuspendLayout();
            this.panel12.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel18.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_Cancelbill)).BeginInit();
            this.panel17.SuspendLayout();
            this.SuspendLayout();
            // 
            // PL_Form
            // 
            this.PL_Form.BackColor = System.Drawing.Color.White;
            this.PL_Form.Controls.Add(this.BExitForm);
            this.PL_Form.Controls.Add(this.panel12);
            this.PL_Form.Controls.Add(this.panel1);
            this.PL_Form.Controls.Add(this.panel18);
            this.PL_Form.Controls.Add(this.DGV_Cancelbill);
            this.PL_Form.Controls.Add(this.BSave_Cancelbill);
            this.PL_Form.Controls.Add(this.panel17);
            this.PL_Form.Font = new System.Drawing.Font("TH Sarabun New", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PL_Form.Location = new System.Drawing.Point(16, 16);
            this.PL_Form.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PL_Form.Name = "PL_Form";
            this.PL_Form.Size = new System.Drawing.Size(1137, 737);
            this.PL_Form.TabIndex = 99;
            // 
            // BExitForm
            // 
            this.BExitForm.BackgroundImage = global::BankTeacher.Properties.Resources.logout;
            this.BExitForm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.BExitForm.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.BExitForm.Location = new System.Drawing.Point(1019, 638);
            this.BExitForm.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.BExitForm.Name = "BExitForm";
            this.BExitForm.Size = new System.Drawing.Size(97, 81);
            this.BExitForm.TabIndex = 112;
            this.BExitForm.UseVisualStyleBackColor = true;
            this.BExitForm.Click += new System.EventHandler(this.BExitForm_Click);
            // 
            // panel12
            // 
            this.panel12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel12.Controls.Add(this.DTPDate);
            this.panel12.Controls.Add(this.BSearchTeacher);
            this.panel12.Controls.Add(this.TBTeacherName_Cancelbill);
            this.panel12.Controls.Add(this.label5);
            this.panel12.Controls.Add(this.label7);
            this.panel12.Controls.Add(this.TBBillNo_Cancelbill);
            this.panel12.Location = new System.Drawing.Point(21, 22);
            this.panel12.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(1093, 73);
            this.panel12.TabIndex = 4;
            // 
            // DTPDate
            // 
            this.DTPDate.CalendarFont = new System.Drawing.Font("TH Sarabun New", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTPDate.Enabled = false;
            this.DTPDate.Location = new System.Drawing.Point(797, 14);
            this.DTPDate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.DTPDate.Name = "DTPDate";
            this.DTPDate.Size = new System.Drawing.Size(265, 42);
            this.DTPDate.TabIndex = 113;
            // 
            // BSearchTeacher
            // 
            this.BSearchTeacher.BackColor = System.Drawing.Color.White;
            this.BSearchTeacher.BackgroundImage = global::BankTeacher.Properties.Resources._64x64_magnifying_glass;
            this.BSearchTeacher.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.BSearchTeacher.Font = new System.Drawing.Font("TH Sarabun New", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BSearchTeacher.ImageKey = "(none)";
            this.BSearchTeacher.Location = new System.Drawing.Point(253, 12);
            this.BSearchTeacher.Margin = new System.Windows.Forms.Padding(3, 7, 3, 7);
            this.BSearchTeacher.Name = "BSearchTeacher";
            this.BSearchTeacher.Size = new System.Drawing.Size(52, 49);
            this.BSearchTeacher.TabIndex = 86;
            this.BSearchTeacher.UseVisualStyleBackColor = false;
            this.BSearchTeacher.Click += new System.EventHandler(this.BSearchTeacher_Click);
            // 
            // TBTeacherName_Cancelbill
            // 
            this.TBTeacherName_Cancelbill.Enabled = false;
            this.TBTeacherName_Cancelbill.Font = new System.Drawing.Font("TH Sarabun New", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TBTeacherName_Cancelbill.Location = new System.Drawing.Point(427, 12);
            this.TBTeacherName_Cancelbill.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TBTeacherName_Cancelbill.Name = "TBTeacherName_Cancelbill";
            this.TBTeacherName_Cancelbill.Size = new System.Drawing.Size(344, 47);
            this.TBTeacherName_Cancelbill.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("TH Sarabun New", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(7, 15);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 40);
            this.label5.TabIndex = 3;
            this.label5.Text = "เลขบิล";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("TH Sarabun New", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(332, 16);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(83, 40);
            this.label7.TabIndex = 3;
            this.label7.Text = "ชื่อ-สกุล";
            // 
            // TBBillNo_Cancelbill
            // 
            this.TBBillNo_Cancelbill.Font = new System.Drawing.Font("TH Sarabun New", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TBBillNo_Cancelbill.Location = new System.Drawing.Point(99, 12);
            this.TBBillNo_Cancelbill.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TBBillNo_Cancelbill.Name = "TBBillNo_Cancelbill";
            this.TBBillNo_Cancelbill.Size = new System.Drawing.Size(133, 47);
            this.TBBillNo_Cancelbill.TabIndex = 1;
            this.TBBillNo_Cancelbill.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TBBillNo_Cancelbill_KeyDown);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.TBNote);
            this.panel1.Controls.Add(this.TBteacharnoby_billcancel);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.TBBIllDate_Cancelbill);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(21, 103);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1094, 136);
            this.panel1.TabIndex = 70;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("TH Sarabun New", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(48, 18);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(78, 40);
            this.label14.TabIndex = 3;
            this.label14.Text = "บิลวันที่";
            // 
            // TBNote
            // 
            this.TBNote.Enabled = false;
            this.TBNote.Font = new System.Drawing.Font("TH Sarabun New", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TBNote.Location = new System.Drawing.Point(151, 70);
            this.TBNote.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TBNote.Name = "TBNote";
            this.TBNote.Size = new System.Drawing.Size(873, 47);
            this.TBNote.TabIndex = 69;
            // 
            // TBteacharnoby_billcancel
            // 
            this.TBteacharnoby_billcancel.Enabled = false;
            this.TBteacharnoby_billcancel.Font = new System.Drawing.Font("TH Sarabun New", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TBteacharnoby_billcancel.Location = new System.Drawing.Point(612, 15);
            this.TBteacharnoby_billcancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TBteacharnoby_billcancel.Name = "TBteacharnoby_billcancel";
            this.TBteacharnoby_billcancel.Size = new System.Drawing.Size(412, 47);
            this.TBteacharnoby_billcancel.TabIndex = 69;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("TH Sarabun New", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(41, 74);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 40);
            this.label1.TabIndex = 4;
            this.label1.Text = "หมายเหตุ";
            // 
            // TBBIllDate_Cancelbill
            // 
            this.TBBIllDate_Cancelbill.Enabled = false;
            this.TBBIllDate_Cancelbill.Font = new System.Drawing.Font("TH Sarabun New", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TBBIllDate_Cancelbill.Location = new System.Drawing.Point(151, 15);
            this.TBBIllDate_Cancelbill.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TBBIllDate_Cancelbill.Name = "TBBIllDate_Cancelbill";
            this.TBBIllDate_Cancelbill.Size = new System.Drawing.Size(220, 47);
            this.TBBIllDate_Cancelbill.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("TH Sarabun New", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(479, 17);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 40);
            this.label2.TabIndex = 4;
            this.label2.Text = "ผู้ทำรายการ";
            // 
            // panel18
            // 
            this.panel18.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel18.Controls.Add(this.label27);
            this.panel18.Controls.Add(this.LSumAmount_CancelBill);
            this.panel18.Location = new System.Drawing.Point(775, 507);
            this.panel18.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel18.Name = "panel18";
            this.panel18.Size = new System.Drawing.Size(339, 36);
            this.panel18.TabIndex = 7;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(279, -2);
            this.label27.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(48, 36);
            this.label27.TabIndex = 5;
            this.label27.Text = "บาท";
            // 
            // LSumAmount_CancelBill
            // 
            this.LSumAmount_CancelBill.AutoSize = true;
            this.LSumAmount_CancelBill.Location = new System.Drawing.Point(4, 0);
            this.LSumAmount_CancelBill.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LSumAmount_CancelBill.Name = "LSumAmount_CancelBill";
            this.LSumAmount_CancelBill.Size = new System.Drawing.Size(25, 36);
            this.LSumAmount_CancelBill.TabIndex = 5;
            this.LSumAmount_CancelBill.Text = "0";
            // 
            // DGV_Cancelbill
            // 
            this.DGV_Cancelbill.AllowUserToAddRows = false;
            this.DGV_Cancelbill.AllowUserToDeleteRows = false;
            this.DGV_Cancelbill.AllowUserToResizeRows = false;
            this.DGV_Cancelbill.BackgroundColor = System.Drawing.Color.White;
            this.DGV_Cancelbill.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV_Cancelbill.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column14,
            this.Column12,
            this.Column13,
            this.Column15,
            this.Column16,
            this.Column21});
            this.DGV_Cancelbill.Location = new System.Drawing.Point(20, 246);
            this.DGV_Cancelbill.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.DGV_Cancelbill.Name = "DGV_Cancelbill";
            this.DGV_Cancelbill.ReadOnly = true;
            this.DGV_Cancelbill.RowHeadersVisible = false;
            this.DGV_Cancelbill.RowHeadersWidth = 51;
            this.DGV_Cancelbill.Size = new System.Drawing.Size(1095, 263);
            this.DGV_Cancelbill.TabIndex = 0;
            // 
            // Column14
            // 
            dataGridViewCellStyle1.Font = new System.Drawing.Font("TH Sarabun New", 17.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Column14.DefaultCellStyle = dataGridViewCellStyle1;
            this.Column14.HeaderText = "ปี / เดือน";
            this.Column14.MinimumWidth = 6;
            this.Column14.Name = "Column14";
            this.Column14.ReadOnly = true;
            this.Column14.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column14.Width = 180;
            // 
            // Column12
            // 
            this.Column12.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("TH Sarabun New", 17.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Column12.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column12.HeaderText = "รายการ";
            this.Column12.MinimumWidth = 6;
            this.Column12.Name = "Column12";
            this.Column12.ReadOnly = true;
            this.Column12.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column13
            // 
            dataGridViewCellStyle3.Font = new System.Drawing.Font("TH Sarabun New", 17.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Column13.DefaultCellStyle = dataGridViewCellStyle3;
            this.Column13.HeaderText = "จำนวนเงิน";
            this.Column13.MinimumWidth = 6;
            this.Column13.Name = "Column13";
            this.Column13.ReadOnly = true;
            this.Column13.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column13.Width = 250;
            // 
            // Column15
            // 
            this.Column15.HeaderText = "LoanNO";
            this.Column15.MinimumWidth = 6;
            this.Column15.Name = "Column15";
            this.Column15.ReadOnly = true;
            this.Column15.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column15.Visible = false;
            this.Column15.Width = 125;
            // 
            // Column16
            // 
            this.Column16.HeaderText = "Month";
            this.Column16.MinimumWidth = 6;
            this.Column16.Name = "Column16";
            this.Column16.ReadOnly = true;
            this.Column16.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column16.Visible = false;
            this.Column16.Width = 125;
            // 
            // Column21
            // 
            this.Column21.HeaderText = "Year";
            this.Column21.MinimumWidth = 6;
            this.Column21.Name = "Column21";
            this.Column21.ReadOnly = true;
            this.Column21.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column21.Visible = false;
            this.Column21.Width = 125;
            // 
            // BSave_Cancelbill
            // 
            this.BSave_Cancelbill.BackColor = System.Drawing.Color.White;
            this.BSave_Cancelbill.Enabled = false;
            this.BSave_Cancelbill.Location = new System.Drawing.Point(775, 551);
            this.BSave_Cancelbill.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.BSave_Cancelbill.Name = "BSave_Cancelbill";
            this.BSave_Cancelbill.Size = new System.Drawing.Size(341, 79);
            this.BSave_Cancelbill.TabIndex = 2;
            this.BSave_Cancelbill.Text = "บันทึก";
            this.BSave_Cancelbill.UseVisualStyleBackColor = false;
            this.BSave_Cancelbill.Click += new System.EventHandler(this.BSave_Cancelbill_Click);
            // 
            // panel17
            // 
            this.panel17.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel17.Controls.Add(this.label25);
            this.panel17.Location = new System.Drawing.Point(20, 506);
            this.panel17.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel17.Name = "panel17";
            this.panel17.Size = new System.Drawing.Size(1094, 38);
            this.panel17.TabIndex = 6;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(7, -1);
            this.label25.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(75, 36);
            this.label25.TabIndex = 5;
            this.label25.Text = "ยอดรวม";
            // 
            // CancelBill
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1172, 767);
            this.Controls.Add(this.PL_Form);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "CancelBill";
            this.Text = "CancellBill";
            this.Load += new System.EventHandler(this.CancelBill_Load);
            this.SizeChanged += new System.EventHandler(this.CancelBill_SizeChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CancelBill_KeyDown);
            this.PL_Form.ResumeLayout(false);
            this.panel12.ResumeLayout(false);
            this.panel12.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel18.ResumeLayout(false);
            this.panel18.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_Cancelbill)).EndInit();
            this.panel17.ResumeLayout(false);
            this.panel17.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PL_Form;
        private System.Windows.Forms.Panel panel18;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label LSumAmount_CancelBill;
        private System.Windows.Forms.Panel panel17;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox TBTeacherName_Cancelbill;
        private System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox TBBIllDate_Cancelbill;
        private System.Windows.Forms.TextBox TBBillNo_Cancelbill;
        private System.Windows.Forms.Button BSave_Cancelbill;
        private System.Windows.Forms.DataGridView DGV_Cancelbill;
        private System.Windows.Forms.TextBox TBteacharnoby_billcancel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button BExitForm;
        private System.Windows.Forms.Button BSearchTeacher;
        private System.Windows.Forms.TextBox TBNote;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column14;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column13;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column15;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column16;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column21;
        private System.Windows.Forms.DateTimePicker DTPDate;
    }
}