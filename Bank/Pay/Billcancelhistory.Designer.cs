
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.PL_Form = new System.Windows.Forms.Panel();
            this.BExitForm = new System.Windows.Forms.Button();
            this.TB_2 = new System.Windows.Forms.TabControl();
            this.TB5 = new System.Windows.Forms.TabPage();
            this.panel16 = new System.Windows.Forms.Panel();
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
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.PL_Form.Location = new System.Drawing.Point(12, 13);
            this.PL_Form.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.PL_Form.Name = "PL_Form";
            this.PL_Form.Size = new System.Drawing.Size(876, 578);
            this.PL_Form.TabIndex = 100;
            // 
            // BExitForm
            // 
            this.BExitForm.BackgroundImage = global::BankTeacher.Properties.Resources.logout;
            this.BExitForm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.BExitForm.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.BExitForm.Location = new System.Drawing.Point(780, 491);
            this.BExitForm.Name = "BExitForm";
            this.BExitForm.Size = new System.Drawing.Size(73, 66);
            this.BExitForm.TabIndex = 112;
            this.BExitForm.UseVisualStyleBackColor = true;
            this.BExitForm.Click += new System.EventHandler(this.BExitForm_Click);
            // 
            // TB_2
            // 
            this.TB_2.Controls.Add(this.TB5);
            this.TB_2.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TB_2.Location = new System.Drawing.Point(6, 2);
            this.TB_2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.TB_2.Name = "TB_2";
            this.TB_2.SelectedIndex = 0;
            this.TB_2.Size = new System.Drawing.Size(854, 484);
            this.TB_2.TabIndex = 97;
            // 
            // TB5
            // 
            this.TB5.Controls.Add(this.DGV_Bill);
            this.TB5.Controls.Add(this.panel16);
            this.TB5.Location = new System.Drawing.Point(4, 39);
            this.TB5.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.TB5.Name = "TB5";
            this.TB5.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.TB5.Size = new System.Drawing.Size(846, 441);
            this.TB5.TabIndex = 5;
            this.TB5.Text = "ประวัติการยกเลิก";
            this.TB5.UseVisualStyleBackColor = true;
            // 
            // panel16
            // 
            this.panel16.BackColor = System.Drawing.Color.White;
            this.panel16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel16.Controls.Add(this.CB_Typebill);
            this.panel16.Controls.Add(this.label3);
            this.panel16.Controls.Add(this.CBMonthSelection_Bill);
            this.panel16.Controls.Add(this.LB5Mo);
            this.panel16.Controls.Add(this.LB5Ye);
            this.panel16.Controls.Add(this.CBYearSelection_Bill);
            this.panel16.Location = new System.Drawing.Point(4, 4);
            this.panel16.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel16.Name = "panel16";
            this.panel16.Size = new System.Drawing.Size(521, 59);
            this.panel16.TabIndex = 99;
            // 
            // CB_Typebill
            // 
            this.CB_Typebill.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_Typebill.Enabled = false;
            this.CB_Typebill.Font = new System.Drawing.Font("TH Sarabun New", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CB_Typebill.FormattingEnabled = true;
            this.CB_Typebill.Items.AddRange(new object[] {
            "ทั้งหมด",
            "ใช้งาน",
            "ยกเลิก"});
            this.CB_Typebill.Location = new System.Drawing.Point(407, 14);
            this.CB_Typebill.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.CB_Typebill.Name = "CB_Typebill";
            this.CB_Typebill.Size = new System.Drawing.Size(98, 33);
            this.CB_Typebill.TabIndex = 102;
            this.CB_Typebill.SelectedIndexChanged += new System.EventHandler(this.CB_Typebill_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(344, 14);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 30);
            this.label3.TabIndex = 100;
            this.label3.Text = "สถาณะ";
            // 
            // CBMonthSelection_Bill
            // 
            this.CBMonthSelection_Bill.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBMonthSelection_Bill.Enabled = false;
            this.CBMonthSelection_Bill.Font = new System.Drawing.Font("TH Sarabun New", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CBMonthSelection_Bill.FormattingEnabled = true;
            this.CBMonthSelection_Bill.Location = new System.Drawing.Point(221, 14);
            this.CBMonthSelection_Bill.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.CBMonthSelection_Bill.Name = "CBMonthSelection_Bill";
            this.CBMonthSelection_Bill.Size = new System.Drawing.Size(98, 33);
            this.CBMonthSelection_Bill.TabIndex = 101;
            this.CBMonthSelection_Bill.SelectedIndexChanged += new System.EventHandler(this.CBMonthSelection_Bill_SelectedIndexChanged);
            // 
            // LB5Mo
            // 
            this.LB5Mo.AutoSize = true;
            this.LB5Mo.BackColor = System.Drawing.Color.White;
            this.LB5Mo.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LB5Mo.ForeColor = System.Drawing.Color.Black;
            this.LB5Mo.Location = new System.Drawing.Point(162, 15);
            this.LB5Mo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LB5Mo.Name = "LB5Mo";
            this.LB5Mo.Size = new System.Drawing.Size(46, 30);
            this.LB5Mo.TabIndex = 100;
            this.LB5Mo.Text = "เดือน";
            // 
            // LB5Ye
            // 
            this.LB5Ye.AutoSize = true;
            this.LB5Ye.BackColor = System.Drawing.Color.White;
            this.LB5Ye.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LB5Ye.ForeColor = System.Drawing.Color.Black;
            this.LB5Ye.Location = new System.Drawing.Point(14, 15);
            this.LB5Ye.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LB5Ye.Name = "LB5Ye";
            this.LB5Ye.Size = new System.Drawing.Size(23, 30);
            this.LB5Ye.TabIndex = 99;
            this.LB5Ye.Text = "ปี";
            // 
            // CBYearSelection_Bill
            // 
            this.CBYearSelection_Bill.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBYearSelection_Bill.Font = new System.Drawing.Font("TH Sarabun New", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CBYearSelection_Bill.FormattingEnabled = true;
            this.CBYearSelection_Bill.Location = new System.Drawing.Point(53, 14);
            this.CBYearSelection_Bill.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.CBYearSelection_Bill.Name = "CBYearSelection_Bill";
            this.CBYearSelection_Bill.Size = new System.Drawing.Size(78, 33);
            this.CBYearSelection_Bill.TabIndex = 84;
            this.CBYearSelection_Bill.SelectedIndexChanged += new System.EventHandler(this.CBYearSelection_Bill_SelectedIndexChanged_1);
            // 
            // DGV_Bill
            // 
            this.DGV_Bill.AllowUserToAddRows = false;
            this.DGV_Bill.AllowUserToDeleteRows = false;
            this.DGV_Bill.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.DGV_Bill.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.DGV_Bill.BackgroundColor = System.Drawing.Color.White;
            this.DGV_Bill.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV_Bill.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column3,
            this.Column1,
            this.Column2,
            this.Column4,
            this.Column5,
            this.Column6});
            this.DGV_Bill.Location = new System.Drawing.Point(3, 67);
            this.DGV_Bill.Name = "DGV_Bill";
            this.DGV_Bill.ReadOnly = true;
            this.DGV_Bill.RowHeadersVisible = false;
            this.DGV_Bill.RowHeadersWidth = 51;
            this.DGV_Bill.Size = new System.Drawing.Size(840, 369);
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
            // Column5
            // 
            this.Column5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column5.HeaderText = "เงิน";
            this.Column5.MinimumWidth = 6;
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "สถาณะ";
            this.Column6.MinimumWidth = 6;
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Width = 125;
            // 
            // Billcancelhistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(901, 604);
            this.Controls.Add(this.PL_Form);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
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
        private System.Windows.Forms.ComboBox CB_Typebill;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox CBMonthSelection_Bill;
        private System.Windows.Forms.Label LB5Mo;
        private System.Windows.Forms.Label LB5Ye;
        private System.Windows.Forms.ComboBox CBYearSelection_Bill;
        private System.Windows.Forms.DataGridView DGV_Bill;
        private System.Windows.Forms.Button BExitForm;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
    }
}