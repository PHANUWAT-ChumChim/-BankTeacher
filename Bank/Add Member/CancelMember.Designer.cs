﻿
namespace BankTeacher.Bank.Add_Member
{
    partial class CancelMember
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CancelMember));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle33 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle34 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle35 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle36 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.LStatusFile = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.TBNote = new System.Windows.Forms.TextBox();
            this.BDeleteFile = new System.Windows.Forms.Button();
            this.BPrint = new System.Windows.Forms.Button();
            this.BOpenFile = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.TBTeacherNo = new System.Windows.Forms.TextBox();
            this.BSearch = new System.Windows.Forms.Button();
            this.BSave = new System.Windows.Forms.Button();
            this.TBTeacherName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel16 = new System.Windows.Forms.Panel();
            this.LB5Ye = new System.Windows.Forms.Label();
            this.CBYear_HistoryCancel = new System.Windows.Forms.ComboBox();
            this.DGV_HistoryCancel = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BOpenFile_Cancel = new System.Windows.Forms.Button();
            this.BPrint_Cancel = new System.Windows.Forms.Button();
            this.BDeleteFile_Cancel = new System.Windows.Forms.Button();
            this.TBNote_Cancel = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.LScan_Cancel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TBTeacherName_Cancel = new System.Windows.Forms.TextBox();
            this.BSave_Cancel = new System.Windows.Forms.Button();
            this.TBTeacherNO_Cancel = new System.Windows.Forms.TextBox();
            this.BSearch_Cancel = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel16.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_HistoryCancel)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(7, 7);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1043, 606);
            this.tabControl1.TabIndex = 61;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.White;
            this.tabPage1.Controls.Add(this.groupBox4);
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Location = new System.Drawing.Point(4, 40);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1035, 562);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "ยกเลิกสมาชิก";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.LStatusFile);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.TBNote);
            this.groupBox4.Controls.Add(this.BDeleteFile);
            this.groupBox4.Controls.Add(this.BPrint);
            this.groupBox4.Controls.Add(this.BOpenFile);
            this.groupBox4.Location = new System.Drawing.Point(500, 20);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(509, 523);
            this.groupBox4.TabIndex = 59;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "ส่งเอกสาร";
            // 
            // LStatusFile
            // 
            this.LStatusFile.AutoSize = true;
            this.LStatusFile.Location = new System.Drawing.Point(26, 391);
            this.LStatusFile.Name = "LStatusFile";
            this.LStatusFile.Size = new System.Drawing.Size(137, 31);
            this.LStatusFile.TabIndex = 23;
            this.LStatusFile.Text = "ยังไม่ได้อัพโหลดไฟล์";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(33, 32);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(75, 30);
            this.label7.TabIndex = 17;
            this.label7.Text = "*หมายเหตุ";
            // 
            // TBNote
            // 
            this.TBNote.Location = new System.Drawing.Point(32, 65);
            this.TBNote.Multiline = true;
            this.TBNote.Name = "TBNote";
            this.TBNote.Size = new System.Drawing.Size(442, 231);
            this.TBNote.TabIndex = 18;
            // 
            // BDeleteFile
            // 
            this.BDeleteFile.BackColor = System.Drawing.Color.White;
            this.BDeleteFile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.BDeleteFile.Cursor = System.Windows.Forms.Cursors.Default;
            this.BDeleteFile.Enabled = false;
            this.BDeleteFile.Location = new System.Drawing.Point(292, 444);
            this.BDeleteFile.Name = "BDeleteFile";
            this.BDeleteFile.Size = new System.Drawing.Size(182, 46);
            this.BDeleteFile.TabIndex = 22;
            this.BDeleteFile.Text = "ลบไฟล์";
            this.BDeleteFile.UseVisualStyleBackColor = false;
            this.BDeleteFile.Click += new System.EventHandler(this.BDeleteFile_Click);
            // 
            // BPrint
            // 
            this.BPrint.BackColor = System.Drawing.Color.White;
            this.BPrint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.BPrint.Cursor = System.Windows.Forms.Cursors.Default;
            this.BPrint.Location = new System.Drawing.Point(292, 302);
            this.BPrint.Name = "BPrint";
            this.BPrint.Size = new System.Drawing.Size(182, 83);
            this.BPrint.TabIndex = 21;
            this.BPrint.Text = "ปริ้นเอกสารยกเลิกสมาชิก";
            this.BPrint.UseVisualStyleBackColor = false;
            // 
            // BOpenFile
            // 
            this.BOpenFile.BackColor = System.Drawing.Color.White;
            this.BOpenFile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.BOpenFile.Cursor = System.Windows.Forms.Cursors.Default;
            this.BOpenFile.Location = new System.Drawing.Point(292, 391);
            this.BOpenFile.Name = "BOpenFile";
            this.BOpenFile.Size = new System.Drawing.Size(182, 47);
            this.BOpenFile.TabIndex = 22;
            this.BOpenFile.Text = "เปิดไฟล์";
            this.BOpenFile.UseVisualStyleBackColor = false;
            this.BOpenFile.Click += new System.EventHandler(this.BOpenFile_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.TBTeacherNo);
            this.groupBox3.Controls.Add(this.BSearch);
            this.groupBox3.Controls.Add(this.BSave);
            this.groupBox3.Controls.Add(this.TBTeacherName);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Location = new System.Drawing.Point(25, 20);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(450, 282);
            this.groupBox3.TabIndex = 58;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "ยกเลิกสมาชิก";
            // 
            // TBTeacherNo
            // 
            this.TBTeacherNo.Location = new System.Drawing.Point(131, 41);
            this.TBTeacherNo.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            this.TBTeacherNo.Name = "TBTeacherNo";
            this.TBTeacherNo.Size = new System.Drawing.Size(211, 38);
            this.TBTeacherNo.TabIndex = 18;
            this.TBTeacherNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TBTeacherNo_KeyDown);
            // 
            // BSearch
            // 
            this.BSearch.BackColor = System.Drawing.Color.White;
            this.BSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BSearch.BackgroundImage")));
            this.BSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.BSearch.ForeColor = System.Drawing.Color.White;
            this.BSearch.ImageKey = "(none)";
            this.BSearch.Location = new System.Drawing.Point(350, 36);
            this.BSearch.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            this.BSearch.Name = "BSearch";
            this.BSearch.Size = new System.Drawing.Size(43, 46);
            this.BSearch.TabIndex = 47;
            this.BSearch.UseVisualStyleBackColor = false;
            this.BSearch.Click += new System.EventHandler(this.BSearch_Click);
            // 
            // BSave
            // 
            this.BSave.BackColor = System.Drawing.Color.White;
            this.BSave.Font = new System.Drawing.Font("TH Sarabun New", 19.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BSave.Location = new System.Drawing.Point(132, 169);
            this.BSave.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            this.BSave.Name = "BSave";
            this.BSave.Size = new System.Drawing.Size(227, 63);
            this.BSave.TabIndex = 56;
            this.BSave.Text = "บันทึก";
            this.BSave.UseVisualStyleBackColor = false;
            this.BSave.Click += new System.EventHandler(this.BSave_Click);
            // 
            // TBTeacherName
            // 
            this.TBTeacherName.Enabled = false;
            this.TBTeacherName.Location = new System.Drawing.Point(131, 98);
            this.TBTeacherName.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            this.TBTeacherName.Name = "TBTeacherName";
            this.TBTeacherName.Size = new System.Drawing.Size(262, 38);
            this.TBTeacherName.TabIndex = 34;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("TH Sarabun New", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(71, 41);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 32);
            this.label4.TabIndex = 8;
            this.label4.Text = "รหัส";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("TH Sarabun New", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(40, 98);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 32);
            this.label5.TabIndex = 7;
            this.label5.Text = "ชื่อ-สกุล";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panel16);
            this.tabPage2.Controls.Add(this.DGV_HistoryCancel);
            this.tabPage2.Location = new System.Drawing.Point(4, 40);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1035, 562);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "ประวัติการยกเลิกสมาชิก";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // panel16
            // 
            this.panel16.BackColor = System.Drawing.Color.White;
            this.panel16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel16.Controls.Add(this.LB5Ye);
            this.panel16.Controls.Add(this.CBYear_HistoryCancel);
            this.panel16.Location = new System.Drawing.Point(17, 15);
            this.panel16.Margin = new System.Windows.Forms.Padding(2);
            this.panel16.Name = "panel16";
            this.panel16.Size = new System.Drawing.Size(195, 63);
            this.panel16.TabIndex = 100;
            // 
            // LB5Ye
            // 
            this.LB5Ye.AutoSize = true;
            this.LB5Ye.BackColor = System.Drawing.Color.White;
            this.LB5Ye.Font = new System.Drawing.Font("TH Sarabun New", 17.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LB5Ye.ForeColor = System.Drawing.Color.Black;
            this.LB5Ye.Location = new System.Drawing.Point(11, 13);
            this.LB5Ye.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LB5Ye.Name = "LB5Ye";
            this.LB5Ye.Size = new System.Drawing.Size(24, 31);
            this.LB5Ye.TabIndex = 99;
            this.LB5Ye.Text = "ปี";
            // 
            // CBYear_HistoryCancel
            // 
            this.CBYear_HistoryCancel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBYear_HistoryCancel.Font = new System.Drawing.Font("TH Sarabun New", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CBYear_HistoryCancel.FormattingEnabled = true;
            this.CBYear_HistoryCancel.Location = new System.Drawing.Point(38, 10);
            this.CBYear_HistoryCancel.Margin = new System.Windows.Forms.Padding(2);
            this.CBYear_HistoryCancel.Name = "CBYear_HistoryCancel";
            this.CBYear_HistoryCancel.Size = new System.Drawing.Size(143, 33);
            this.CBYear_HistoryCancel.TabIndex = 84;
            this.CBYear_HistoryCancel.SelectedIndexChanged += new System.EventHandler(this.CBYear_HistoryCancel_SelectedIndexChanged);
            // 
            // DGV_HistoryCancel
            // 
            this.DGV_HistoryCancel.AllowUserToAddRows = false;
            this.DGV_HistoryCancel.AllowUserToDeleteRows = false;
            dataGridViewCellStyle33.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DGV_HistoryCancel.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle33;
            this.DGV_HistoryCancel.BackgroundColor = System.Drawing.Color.White;
            this.DGV_HistoryCancel.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV_HistoryCancel.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column4,
            this.Column2,
            this.Column3});
            this.DGV_HistoryCancel.Location = new System.Drawing.Point(17, 82);
            this.DGV_HistoryCancel.Margin = new System.Windows.Forms.Padding(2);
            this.DGV_HistoryCancel.Name = "DGV_HistoryCancel";
            this.DGV_HistoryCancel.ReadOnly = true;
            this.DGV_HistoryCancel.RowHeadersVisible = false;
            this.DGV_HistoryCancel.RowHeadersWidth = 51;
            this.DGV_HistoryCancel.RowTemplate.Height = 24;
            this.DGV_HistoryCancel.Size = new System.Drawing.Size(1001, 466);
            this.DGV_HistoryCancel.TabIndex = 99;
            // 
            // Column1
            // 
            dataGridViewCellStyle34.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Column1.DefaultCellStyle = dataGridViewCellStyle34;
            this.Column1.HeaderText = "วัน/เดือน//ปี";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 150;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "รหัส";
            this.Column4.MinimumWidth = 6;
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 130;
            // 
            // Column2
            // 
            dataGridViewCellStyle35.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Column2.DefaultCellStyle = dataGridViewCellStyle35;
            this.Column2.HeaderText = "ชื่อ";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 250;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle36.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Column3.DefaultCellStyle = dataGridViewCellStyle36;
            this.Column3.HeaderText = "สาเหตุ";
            this.Column3.MinimumWidth = 6;
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // BOpenFile_Cancel
            // 
            this.BOpenFile_Cancel.BackColor = System.Drawing.Color.White;
            this.BOpenFile_Cancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.BOpenFile_Cancel.Cursor = System.Windows.Forms.Cursors.Default;
            this.BOpenFile_Cancel.Location = new System.Drawing.Point(348, 358);
            this.BOpenFile_Cancel.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            this.BOpenFile_Cancel.Name = "BOpenFile_Cancel";
            this.BOpenFile_Cancel.Size = new System.Drawing.Size(154, 73);
            this.BOpenFile_Cancel.TabIndex = 22;
            this.BOpenFile_Cancel.Text = "เปิดไฟล์";
            this.BOpenFile_Cancel.UseVisualStyleBackColor = false;
            // 
            // BPrint_Cancel
            // 
            this.BPrint_Cancel.BackColor = System.Drawing.Color.White;
            this.BPrint_Cancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.BPrint_Cancel.Cursor = System.Windows.Forms.Cursors.Default;
            this.BPrint_Cancel.Location = new System.Drawing.Point(267, 282);
            this.BPrint_Cancel.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            this.BPrint_Cancel.Name = "BPrint_Cancel";
            this.BPrint_Cancel.Size = new System.Drawing.Size(235, 62);
            this.BPrint_Cancel.TabIndex = 21;
            this.BPrint_Cancel.Text = "ปริ้นเอกสารยกเลิกสมาชิก";
            this.BPrint_Cancel.UseVisualStyleBackColor = false;
            // 
            // BDeleteFile_Cancel
            // 
            this.BDeleteFile_Cancel.BackColor = System.Drawing.Color.White;
            this.BDeleteFile_Cancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.BDeleteFile_Cancel.Cursor = System.Windows.Forms.Cursors.Default;
            this.BDeleteFile_Cancel.Enabled = false;
            this.BDeleteFile_Cancel.Location = new System.Drawing.Point(348, 445);
            this.BDeleteFile_Cancel.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            this.BDeleteFile_Cancel.Name = "BDeleteFile_Cancel";
            this.BDeleteFile_Cancel.Size = new System.Drawing.Size(154, 73);
            this.BDeleteFile_Cancel.TabIndex = 22;
            this.BDeleteFile_Cancel.Text = "ลบไฟล์";
            this.BDeleteFile_Cancel.UseVisualStyleBackColor = false;
            // 
            // TBNote_Cancel
            // 
            this.TBNote_Cancel.Location = new System.Drawing.Point(43, 75);
            this.TBNote_Cancel.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            this.TBNote_Cancel.Multiline = true;
            this.TBNote_Cancel.Name = "TBNote_Cancel";
            this.TBNote_Cancel.Size = new System.Drawing.Size(459, 193);
            this.TBNote_Cancel.TabIndex = 18;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(38, 38);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 30);
            this.label3.TabIndex = 17;
            // 
            // LScan_Cancel
            // 
            this.LScan_Cancel.AutoSize = true;
            this.LScan_Cancel.Location = new System.Drawing.Point(37, 358);
            this.LScan_Cancel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LScan_Cancel.Name = "LScan_Cancel";
            this.LScan_Cancel.Size = new System.Drawing.Size(137, 31);
            this.LScan_Cancel.TabIndex = 23;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("TH Sarabun New", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(57, 105);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 32);
            this.label1.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("TH Sarabun New", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(72, 57);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 32);
            this.label2.TabIndex = 8;
            // 
            // TBTeacherName_Cancel
            // 
            this.TBTeacherName_Cancel.Enabled = false;
            this.TBTeacherName_Cancel.Location = new System.Drawing.Point(124, 105);
            this.TBTeacherName_Cancel.Margin = new System.Windows.Forms.Padding(5, 17, 5, 17);
            this.TBTeacherName_Cancel.Name = "TBTeacherName_Cancel";
            this.TBTeacherName_Cancel.Size = new System.Drawing.Size(216, 20);
            this.TBTeacherName_Cancel.TabIndex = 34;
            // 
            // BSave_Cancel
            // 
            this.BSave_Cancel.BackColor = System.Drawing.Color.White;
            this.BSave_Cancel.Font = new System.Drawing.Font("TH Sarabun New", 19.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BSave_Cancel.Location = new System.Drawing.Point(124, 154);
            this.BSave_Cancel.Margin = new System.Windows.Forms.Padding(5, 17, 5, 17);
            this.BSave_Cancel.Name = "BSave_Cancel";
            this.BSave_Cancel.Size = new System.Drawing.Size(216, 83);
            this.BSave_Cancel.TabIndex = 56;
            this.BSave_Cancel.Text = "บันทึก";
            this.BSave_Cancel.UseVisualStyleBackColor = false;
            // 
            // TBTeacherNO_Cancel
            // 
            this.TBTeacherNO_Cancel.Location = new System.Drawing.Point(124, 55);
            this.TBTeacherNO_Cancel.Margin = new System.Windows.Forms.Padding(5, 17, 5, 17);
            this.TBTeacherNO_Cancel.Name = "TBTeacherNO_Cancel";
            this.TBTeacherNO_Cancel.Size = new System.Drawing.Size(167, 20);
            this.TBTeacherNO_Cancel.TabIndex = 18;
            // 
            // BSearch_Cancel
            // 
            this.BSearch_Cancel.BackColor = System.Drawing.Color.White;
            this.BSearch_Cancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BSearch_Cancel.BackgroundImage")));
            this.BSearch_Cancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.BSearch_Cancel.ForeColor = System.Drawing.Color.White;
            this.BSearch_Cancel.ImageKey = "(none)";
            this.BSearch_Cancel.Location = new System.Drawing.Point(301, 44);
            this.BSearch_Cancel.Margin = new System.Windows.Forms.Padding(5, 17, 5, 17);
            this.BSearch_Cancel.Name = "BSearch_Cancel";
            this.BSearch_Cancel.Size = new System.Drawing.Size(48, 49);
            this.BSearch_Cancel.TabIndex = 47;
            this.BSearch_Cancel.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Location = new System.Drawing.Point(5, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1058, 622);
            this.panel1.TabIndex = 62;
            // 
            // CancelMember
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1067, 630);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("TH Sarabun New", 17.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            this.Name = "CancelMember";
            this.Text = "CancelMember";
            this.SizeChanged += new System.EventHandler(this.CancelMember_SizeChanged);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.panel16.ResumeLayout(false);
            this.panel16.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_HistoryCancel)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button BOpenFile_Cancel;
        private System.Windows.Forms.Button BPrint_Cancel;
        private System.Windows.Forms.Button BDeleteFile_Cancel;
        private System.Windows.Forms.TextBox TBNote_Cancel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label LScan_Cancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TBTeacherName_Cancel;
        private System.Windows.Forms.Button BSave_Cancel;
        private System.Windows.Forms.Button BSearch_Cancel;
        private System.Windows.Forms.TextBox TBTeacherNO_Cancel;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox TBTeacherNo;
        private System.Windows.Forms.Button BSearch;
        private System.Windows.Forms.Button BSave;
        private System.Windows.Forms.TextBox TBTeacherName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label LStatusFile;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox TBNote;
        private System.Windows.Forms.Button BDeleteFile;
        private System.Windows.Forms.Button BPrint;
        private System.Windows.Forms.Button BOpenFile;
        private System.Windows.Forms.Panel panel16;
        private System.Windows.Forms.Label LB5Ye;
        private System.Windows.Forms.ComboBox CBYear_HistoryCancel;
        private System.Windows.Forms.DataGridView DGV_HistoryCancel;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.Panel panel1;
    }
}