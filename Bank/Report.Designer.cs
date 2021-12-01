
namespace BankTeacher.Bank
{
    partial class Report
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Report));
            this.panel1 = new System.Windows.Forms.Panel();
            this.BTPrint = new System.Windows.Forms.Button();
            this.TP_LUP = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.DGVReportIncome = new System.Windows.Forms.DataGridView();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.TBSumIncome = new System.Windows.Forms.TextBox();
            this.TBInterest = new System.Windows.Forms.TextBox();
            this.TBLoanAmount = new System.Windows.Forms.TextBox();
            this.TBSavingAmount = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.DGVExpensesReport = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.TBSumExpenses = new System.Windows.Forms.TextBox();
            this.TBPayLoan_Expenses = new System.Windows.Forms.TextBox();
            this.TBDividend_Expenses = new System.Windows.Forms.TextBox();
            this.TBAmountOff_Expenses = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.CBMonth = new System.Windows.Forms.ComboBox();
            this.CBYear = new System.Windows.Forms.ComboBox();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.panel1.SuspendLayout();
            this.TP_LUP.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGVReportIncome)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGVExpensesReport)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.BTPrint);
            this.panel1.Controls.Add(this.TP_LUP);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.CBMonth);
            this.panel1.Controls.Add(this.CBYear);
            this.panel1.Location = new System.Drawing.Point(13, 16);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(877, 590);
            this.panel1.TabIndex = 0;
            // 
            // BTPrint
            // 
            this.BTPrint.BackgroundImage = global::BankTeacher.Properties.Resources._10x10_Print;
            this.BTPrint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.BTPrint.CausesValidation = false;
            this.BTPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTPrint.Location = new System.Drawing.Point(729, 38);
            this.BTPrint.Name = "BTPrint";
            this.BTPrint.Size = new System.Drawing.Size(127, 58);
            this.BTPrint.TabIndex = 106;
            this.BTPrint.UseVisualStyleBackColor = true;
            this.BTPrint.Visible = false;
            this.BTPrint.Click += new System.EventHandler(this.BTPrint_Click);
            // 
            // TP_LUP
            // 
            this.TP_LUP.Controls.Add(this.tabPage1);
            this.TP_LUP.Controls.Add(this.tabPage2);
            this.TP_LUP.Location = new System.Drawing.Point(16, 72);
            this.TP_LUP.Name = "TP_LUP";
            this.TP_LUP.SelectedIndex = 0;
            this.TP_LUP.Size = new System.Drawing.Size(844, 511);
            this.TP_LUP.TabIndex = 6;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.DGVReportIncome);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.TBSumIncome);
            this.tabPage1.Controls.Add(this.TBInterest);
            this.tabPage1.Controls.Add(this.TBLoanAmount);
            this.tabPage1.Controls.Add(this.TBSavingAmount);
            this.tabPage1.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.tabPage1.Location = new System.Drawing.Point(4, 45);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(836, 462);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "รายรับ";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // DGVReportIncome
            // 
            this.DGVReportIncome.AllowUserToAddRows = false;
            this.DGVReportIncome.AllowUserToDeleteRows = false;
            this.DGVReportIncome.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("TH Sarabun New", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.DGVReportIncome.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.DGVReportIncome.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGVReportIncome.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.DGVReportIncome.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVReportIncome.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column8,
            this.Column9,
            this.Column6,
            this.Column1,
            this.Column2,
            this.Column3});
            this.DGVReportIncome.GridColor = System.Drawing.SystemColors.Control;
            this.DGVReportIncome.Location = new System.Drawing.Point(20, 6);
            this.DGVReportIncome.Name = "DGVReportIncome";
            this.DGVReportIncome.RowHeadersVisible = false;
            this.DGVReportIncome.RowHeadersWidth = 51;
            this.DGVReportIncome.RowTemplate.Height = 24;
            this.DGVReportIncome.Size = new System.Drawing.Size(816, 387);
            this.DGVReportIncome.TabIndex = 3;
            // 
            // Column8
            // 
            dataGridViewCellStyle3.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Column8.DefaultCellStyle = dataGridViewCellStyle3;
            this.Column8.HeaderText = "รหัส";
            this.Column8.MinimumWidth = 6;
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.Width = 125;
            // 
            // Column9
            // 
            dataGridViewCellStyle4.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Column9.DefaultCellStyle = dataGridViewCellStyle4;
            this.Column9.HeaderText = "ชื่อ - นามสกุล";
            this.Column9.MinimumWidth = 6;
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            this.Column9.Width = 250;
            // 
            // Column6
            // 
            dataGridViewCellStyle5.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Column6.DefaultCellStyle = dataGridViewCellStyle5;
            this.Column6.HeaderText = "หุ้นสะสม";
            this.Column6.MinimumWidth = 6;
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Width = 115;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "เงินต้นกู้";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.Width = 115;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "ดอกเบี้ย";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            this.Column2.Width = 125;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column3.HeaderText = "รวม";
            this.Column3.MinimumWidth = 6;
            this.Column3.Name = "Column3";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(857, 27);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 37);
            this.label6.TabIndex = 1;
            this.label6.Text = "เงินรวม";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(513, 421);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(116, 37);
            this.label5.TabIndex = 1;
            this.label5.Text = "เงินดอกเบี้ย";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(276, 420);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 37);
            this.label4.TabIndex = 1;
            this.label4.Text = "เงินกู้";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 418);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(121, 37);
            this.label3.TabIndex = 1;
            this.label3.Text = "เงินหุ้นสะสม";
            // 
            // TBSumIncome
            // 
            this.TBSumIncome.Location = new System.Drawing.Point(942, 23);
            this.TBSumIncome.Name = "TBSumIncome";
            this.TBSumIncome.Size = new System.Drawing.Size(139, 43);
            this.TBSumIncome.TabIndex = 0;
            // 
            // TBInterest
            // 
            this.TBInterest.Location = new System.Drawing.Point(608, 418);
            this.TBInterest.Name = "TBInterest";
            this.TBInterest.Size = new System.Drawing.Size(151, 43);
            this.TBInterest.TabIndex = 0;
            // 
            // TBLoanAmount
            // 
            this.TBLoanAmount.Location = new System.Drawing.Point(327, 418);
            this.TBLoanAmount.Name = "TBLoanAmount";
            this.TBLoanAmount.Size = new System.Drawing.Size(159, 43);
            this.TBLoanAmount.TabIndex = 0;
            // 
            // TBSavingAmount
            // 
            this.TBSavingAmount.Location = new System.Drawing.Point(110, 418);
            this.TBSavingAmount.Name = "TBSavingAmount";
            this.TBSavingAmount.Size = new System.Drawing.Size(147, 43);
            this.TBSavingAmount.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.DGVExpensesReport);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Controls.Add(this.label10);
            this.tabPage2.Controls.Add(this.TBSumExpenses);
            this.tabPage2.Controls.Add(this.TBPayLoan_Expenses);
            this.tabPage2.Controls.Add(this.TBDividend_Expenses);
            this.tabPage2.Controls.Add(this.TBAmountOff_Expenses);
            this.tabPage2.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.tabPage2.Location = new System.Drawing.Point(4, 45);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(836, 462);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "รายจ่าย";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // DGVExpensesReport
            // 
            this.DGVExpensesReport.AllowUserToAddRows = false;
            this.DGVExpensesReport.AllowUserToResizeRows = false;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.LightSkyBlue;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("TH Sarabun New", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.DGVExpensesReport.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle6;
            this.DGVExpensesReport.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DGVExpensesReport.BackgroundColor = System.Drawing.Color.White;
            this.DGVExpensesReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVExpensesReport.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn6,
            this.Column4,
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8,
            this.dataGridViewTextBoxColumn9});
            this.DGVExpensesReport.GridColor = System.Drawing.SystemColors.ControlLightLight;
            this.DGVExpensesReport.Location = new System.Drawing.Point(23, 11);
            this.DGVExpensesReport.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.DGVExpensesReport.Name = "DGVExpensesReport";
            this.DGVExpensesReport.RowHeadersVisible = false;
            this.DGVExpensesReport.RowHeadersWidth = 51;
            this.DGVExpensesReport.RowTemplate.Height = 24;
            this.DGVExpensesReport.Size = new System.Drawing.Size(798, 398);
            this.DGVExpensesReport.TabIndex = 76;
            // 
            // dataGridViewTextBoxColumn6
            // 
            dataGridViewCellStyle7.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridViewTextBoxColumn6.DefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridViewTextBoxColumn6.FillWeight = 68.52793F;
            this.dataGridViewTextBoxColumn6.HeaderText = "รหัส";
            this.dataGridViewTextBoxColumn6.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            // 
            // Column4
            // 
            this.Column4.FillWeight = 107.868F;
            this.Column4.HeaderText = "ชื่อ - นามสกุล";
            this.Column4.MinimumWidth = 6;
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn7
            // 
            dataGridViewCellStyle8.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridViewTextBoxColumn7.DefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridViewTextBoxColumn7.FillWeight = 107.868F;
            this.dataGridViewTextBoxColumn7.HeaderText = "คืนหุ้นสะสม";
            this.dataGridViewTextBoxColumn7.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            // 
            // dataGridViewTextBoxColumn8
            // 
            dataGridViewCellStyle9.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridViewTextBoxColumn8.DefaultCellStyle = dataGridViewCellStyle9;
            this.dataGridViewTextBoxColumn8.FillWeight = 107.868F;
            this.dataGridViewTextBoxColumn8.HeaderText = "ปันผลประจำปี";
            this.dataGridViewTextBoxColumn8.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.FillWeight = 107.868F;
            this.dataGridViewTextBoxColumn9.HeaderText = "จ่ายเงินกู้";
            this.dataGridViewTextBoxColumn9.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(845, 30);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(118, 37);
            this.label7.TabIndex = 72;
            this.label7.Text = "รายจ่ายรวม";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(562, 422);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(92, 37);
            this.label8.TabIndex = 73;
            this.label8.Text = "จ่ายเงินกู้";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(331, 422);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(70, 37);
            this.label9.TabIndex = 74;
            this.label9.Text = "ปันผล";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(60, 422);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(118, 37);
            this.label10.TabIndex = 75;
            this.label10.Text = "คืนหุ้นสะสม";
            // 
            // TBSumExpenses
            // 
            this.TBSumExpenses.Location = new System.Drawing.Point(943, 27);
            this.TBSumExpenses.Name = "TBSumExpenses";
            this.TBSumExpenses.Size = new System.Drawing.Size(139, 43);
            this.TBSumExpenses.TabIndex = 68;
            // 
            // TBPayLoan_Expenses
            // 
            this.TBPayLoan_Expenses.Location = new System.Drawing.Point(639, 419);
            this.TBPayLoan_Expenses.Name = "TBPayLoan_Expenses";
            this.TBPayLoan_Expenses.Size = new System.Drawing.Size(151, 43);
            this.TBPayLoan_Expenses.TabIndex = 69;
            // 
            // TBDividend_Expenses
            // 
            this.TBDividend_Expenses.Location = new System.Drawing.Point(386, 419);
            this.TBDividend_Expenses.Name = "TBDividend_Expenses";
            this.TBDividend_Expenses.Size = new System.Drawing.Size(159, 43);
            this.TBDividend_Expenses.TabIndex = 70;
            // 
            // TBAmountOff_Expenses
            // 
            this.TBAmountOff_Expenses.Location = new System.Drawing.Point(159, 419);
            this.TBAmountOff_Expenses.Name = "TBAmountOff_Expenses";
            this.TBAmountOff_Expenses.Size = new System.Drawing.Size(147, 43);
            this.TBAmountOff_Expenses.TabIndex = 71;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.Location = new System.Drawing.Point(203, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 37);
            this.label2.TabIndex = 4;
            this.label2.Text = "เดือน";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(29, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 37);
            this.label1.TabIndex = 5;
            this.label1.Text = "ปี";
            // 
            // CBMonth
            // 
            this.CBMonth.Enabled = false;
            this.CBMonth.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.CBMonth.FormattingEnabled = true;
            this.CBMonth.Location = new System.Drawing.Point(252, 22);
            this.CBMonth.Name = "CBMonth";
            this.CBMonth.Size = new System.Drawing.Size(112, 44);
            this.CBMonth.TabIndex = 2;
            this.CBMonth.SelectedIndexChanged += new System.EventHandler(this.CBMonth_SelectedIndexChanged);
            // 
            // CBYear
            // 
            this.CBYear.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.CBYear.FormattingEnabled = true;
            this.CBYear.Location = new System.Drawing.Point(62, 22);
            this.CBYear.Name = "CBYear";
            this.CBYear.Size = new System.Drawing.Size(112, 44);
            this.CBYear.TabIndex = 3;
            this.CBYear.SelectedIndexChanged += new System.EventHandler(this.CBYear_SelectedIndexChanged);
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
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // Report
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 36F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(907, 622);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            this.Name = "Report";
            this.Text = "Report";
            this.Load += new System.EventHandler(this.Report_Load);
            this.SizeChanged += new System.EventHandler(this.Report_SizeChanged);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.TP_LUP.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGVReportIncome)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGVExpensesReport)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox CBMonth;
        private System.Windows.Forms.ComboBox CBYear;
        private System.Windows.Forms.TabControl TP_LUP;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TBSumIncome;
        private System.Windows.Forms.TextBox TBInterest;
        private System.Windows.Forms.TextBox TBLoanAmount;
        private System.Windows.Forms.TextBox TBSavingAmount;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView DGVExpensesReport;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox TBSumExpenses;
        private System.Windows.Forms.TextBox TBPayLoan_Expenses;
        private System.Windows.Forms.TextBox TBDividend_Expenses;
        private System.Windows.Forms.TextBox TBAmountOff_Expenses;
        private System.Windows.Forms.DataGridView DGVReportIncome;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.Button BTPrint;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Drawing.Printing.PrintDocument printDocument1;
    }
}