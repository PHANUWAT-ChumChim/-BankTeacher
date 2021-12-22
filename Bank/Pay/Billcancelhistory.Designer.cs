
namespace BankTeacher.Bank.Pay
{
    partial class Billcancelhistory
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
            this.PL_Form = new System.Windows.Forms.Panel();
            this.BExitForm = new System.Windows.Forms.Button();
            this.TB_2 = new System.Windows.Forms.TabControl();
            this.TB5 = new System.Windows.Forms.TabPage();
            this.panel16 = new System.Windows.Forms.Panel();
            this.BListAdd_Pay = new System.Windows.Forms.Button();
            this.CB_Typebill = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.CBMonthSelection_Bill = new System.Windows.Forms.ComboBox();
            this.LB5Mo = new System.Windows.Forms.Label();
            this.LB5Ye = new System.Windows.Forms.Label();
            this.CBYearSelection_Bill = new System.Windows.Forms.ComboBox();
            this.DGV_Bill = new System.Windows.Forms.DataGridView();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PL_Form.SuspendLayout();
            this.TB_2.SuspendLayout();
            this.TB5.SuspendLayout();
            this.panel16.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_Bill)).BeginInit();
            this.SuspendLayout();
            // 
            // PL_Form
            // 
            this.PL_Form.BackColor = System.Drawing.Color.White;
            this.PL_Form.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PL_Form.Controls.Add(this.BExitForm);
            this.PL_Form.Controls.Add(this.TB_2);
            this.PL_Form.Location = new System.Drawing.Point(16, 16);
            this.PL_Form.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PL_Form.Name = "PL_Form";
            this.PL_Form.Size = new System.Drawing.Size(1168, 711);
            this.PL_Form.TabIndex = 100;
            // 
            // BExitForm
            // 
            this.BExitForm.BackgroundImage = global::BankTeacher.Properties.Resources.logout;
            this.BExitForm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.BExitForm.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.BExitForm.Location = new System.Drawing.Point(1040, 604);
            this.BExitForm.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.BExitForm.Name = "BExitForm";
            this.BExitForm.Size = new System.Drawing.Size(97, 81);
            this.BExitForm.TabIndex = 112;
            this.BExitForm.UseVisualStyleBackColor = true;
            this.BExitForm.Click += new System.EventHandler(this.BExitForm_Click);
            // 
            // TB_2
            // 
            this.TB_2.Controls.Add(this.TB5);
            this.TB_2.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TB_2.Location = new System.Drawing.Point(8, 2);
            this.TB_2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TB_2.Name = "TB_2";
            this.TB_2.SelectedIndex = 0;
            this.TB_2.Size = new System.Drawing.Size(1139, 596);
            this.TB_2.TabIndex = 97;
            // 
            // TB5
            // 
            this.TB5.Controls.Add(this.panel16);
            this.TB5.Controls.Add(this.DGV_Bill);
            this.TB5.Location = new System.Drawing.Point(4, 45);
            this.TB5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TB5.Name = "TB5";
            this.TB5.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TB5.Size = new System.Drawing.Size(1131, 547);
            this.TB5.TabIndex = 5;
            this.TB5.Text = "ประวัติการยกเลิก";
            this.TB5.UseVisualStyleBackColor = true;
            // 
            // panel16
            // 
            this.panel16.BackColor = System.Drawing.Color.White;
            this.panel16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel16.Controls.Add(this.BListAdd_Pay);
            this.panel16.Controls.Add(this.CB_Typebill);
            this.panel16.Controls.Add(this.label3);
            this.panel16.Controls.Add(this.CBMonthSelection_Bill);
            this.panel16.Controls.Add(this.LB5Mo);
            this.panel16.Controls.Add(this.LB5Ye);
            this.panel16.Controls.Add(this.CBYearSelection_Bill);
            this.panel16.Location = new System.Drawing.Point(5, 5);
            this.panel16.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel16.Name = "panel16";
            this.panel16.Size = new System.Drawing.Size(939, 72);
            this.panel16.TabIndex = 99;
            // 
            // BListAdd_Pay
            // 
            this.BListAdd_Pay.BackColor = System.Drawing.Color.White;
            this.BListAdd_Pay.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BListAdd_Pay.Location = new System.Drawing.Point(716, 11);
            this.BListAdd_Pay.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BListAdd_Pay.Name = "BListAdd_Pay";
            this.BListAdd_Pay.Size = new System.Drawing.Size(191, 50);
            this.BListAdd_Pay.TabIndex = 100;
            this.BListAdd_Pay.Text = "ค้นหา";
            this.BListAdd_Pay.UseVisualStyleBackColor = false;
            this.BListAdd_Pay.Click += new System.EventHandler(this.BListAdd_Pay_Click_1);
            // 
            // CB_Typebill
            // 
            this.CB_Typebill.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_Typebill.Font = new System.Drawing.Font("TH Sarabun New", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CB_Typebill.FormattingEnabled = true;
            this.CB_Typebill.Items.AddRange(new object[] {
            "ทั้งหมด",
            "บิลล์ยกเลิก",
            "บิลล์"});
            this.CB_Typebill.Location = new System.Drawing.Point(543, 17);
            this.CB_Typebill.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.CB_Typebill.Name = "CB_Typebill";
            this.CB_Typebill.Size = new System.Drawing.Size(129, 39);
            this.CB_Typebill.TabIndex = 102;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(459, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 37);
            this.label3.TabIndex = 100;
            this.label3.Text = "ประเภท";
            // 
            // CBMonthSelection_Bill
            // 
            this.CBMonthSelection_Bill.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBMonthSelection_Bill.Font = new System.Drawing.Font("TH Sarabun New", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CBMonthSelection_Bill.FormattingEnabled = true;
            this.CBMonthSelection_Bill.Location = new System.Drawing.Point(295, 17);
            this.CBMonthSelection_Bill.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.CBMonthSelection_Bill.Name = "CBMonthSelection_Bill";
            this.CBMonthSelection_Bill.Size = new System.Drawing.Size(129, 39);
            this.CBMonthSelection_Bill.TabIndex = 101;
            // 
            // LB5Mo
            // 
            this.LB5Mo.AutoSize = true;
            this.LB5Mo.BackColor = System.Drawing.Color.White;
            this.LB5Mo.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LB5Mo.ForeColor = System.Drawing.Color.Black;
            this.LB5Mo.Location = new System.Drawing.Point(216, 18);
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
            this.LB5Ye.Location = new System.Drawing.Point(19, 18);
            this.LB5Ye.Name = "LB5Ye";
            this.LB5Ye.Size = new System.Drawing.Size(29, 37);
            this.LB5Ye.TabIndex = 99;
            this.LB5Ye.Text = "ปี";
            // 
            // CBYearSelection_Bill
            // 
            this.CBYearSelection_Bill.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBYearSelection_Bill.Font = new System.Drawing.Font("TH Sarabun New", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CBYearSelection_Bill.FormattingEnabled = true;
            this.CBYearSelection_Bill.Location = new System.Drawing.Point(71, 17);
            this.CBYearSelection_Bill.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.CBYearSelection_Bill.Name = "CBYearSelection_Bill";
            this.CBYearSelection_Bill.Size = new System.Drawing.Size(103, 39);
            this.CBYearSelection_Bill.TabIndex = 84;
            this.CBYearSelection_Bill.SelectedIndexChanged += new System.EventHandler(this.CBYearSelection_Bill_SelectedIndexChanged_1);
            // 
            // DGV_Bill
            // 
            this.DGV_Bill.AllowUserToAddRows = false;
            this.DGV_Bill.AllowUserToDeleteRows = false;
            this.DGV_Bill.AllowUserToResizeRows = false;
            this.DGV_Bill.BackgroundColor = System.Drawing.Color.White;
            this.DGV_Bill.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV_Bill.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column3,
            this.Column1,
            this.Column2,
            this.Column4,
            this.Column6,
            this.Column5});
            this.DGV_Bill.Location = new System.Drawing.Point(4, 82);
            this.DGV_Bill.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.DGV_Bill.Name = "DGV_Bill";
            this.DGV_Bill.ReadOnly = true;
            this.DGV_Bill.RowHeadersVisible = false;
            this.DGV_Bill.RowHeadersWidth = 51;
            this.DGV_Bill.Size = new System.Drawing.Size(1120, 454);
            this.DGV_Bill.TabIndex = 1;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "ผู้ทำรายการ";
            this.Column3.MinimumWidth = 6;
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 200;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "บิลล์";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 130;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "วันที่";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 125;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "รายการ";
            this.Column4.MinimumWidth = 6;
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 150;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "ประเภท";
            this.Column6.MinimumWidth = 6;
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Width = 125;
            // 
            // Column5
            // 
            this.Column5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column5.HeaderText = "เงิน";
            this.Column5.MinimumWidth = 6;
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // Billcancelhistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1201, 743);
            this.Controls.Add(this.PL_Form);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Billcancelhistory";
            this.Text = "Billcancelhistory";
            this.Load += new System.EventHandler(this.Billcancelhistory_Load);
            this.SizeChanged += new System.EventHandler(this.Billcancelhistory_SizeChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Billcancelhistory_KeyDown);
            this.PL_Form.ResumeLayout(false);
            this.TB_2.ResumeLayout(false);
            this.TB5.ResumeLayout(false);
            this.panel16.ResumeLayout(false);
            this.panel16.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_Bill)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PL_Form;
        public System.Windows.Forms.TabControl TB_2;
        private System.Windows.Forms.TabPage TB5;
        private System.Windows.Forms.Panel panel16;
        private System.Windows.Forms.Button BListAdd_Pay;
        private System.Windows.Forms.ComboBox CB_Typebill;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox CBMonthSelection_Bill;
        private System.Windows.Forms.Label LB5Mo;
        private System.Windows.Forms.Label LB5Ye;
        private System.Windows.Forms.ComboBox CBYearSelection_Bill;
        private System.Windows.Forms.DataGridView DGV_Bill;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.Button BExitForm;
    }
}