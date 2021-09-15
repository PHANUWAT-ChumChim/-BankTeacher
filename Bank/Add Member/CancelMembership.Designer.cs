
namespace example.Bank
{
    partial class CancelMembership
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CancelMembership));
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.TBIDNo = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.TBTeacherNo = new System.Windows.Forms.TextBox();
            this.BSearchTeacher = new System.Windows.Forms.Button();
            this.TBTeacherName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.BExit = new System.Windows.Forms.Button();
            this.CBStatus = new System.Windows.Forms.ComboBox();
            this.DTPStartDate = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.BSave = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Controls.Add(this.BExit);
            this.panel1.Controls.Add(this.CBStatus);
            this.panel1.Controls.Add(this.DTPStartDate);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.BSave);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(27, 25);
            this.panel1.Margin = new System.Windows.Forms.Padding(6, 16, 6, 16);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(900, 541);
            this.panel1.TabIndex = 19;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.Font = new System.Drawing.Font("TH Sarabun New", 19.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(250, 457);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(398, 61);
            this.button1.TabIndex = 55;
            this.button1.Text = "บันทึก";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(56, 135);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(787, 265);
            this.tabControl1.TabIndex = 54;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.TBIDNo);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.TBTeacherNo);
            this.tabPage1.Controls.Add(this.BSearchTeacher);
            this.tabPage1.Controls.Add(this.TBTeacherName);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Location = new System.Drawing.Point(4, 45);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(779, 216);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "ข้อมูลส่วนตัว";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // TBIDNo
            // 
            this.TBIDNo.Enabled = false;
            this.TBIDNo.Location = new System.Drawing.Point(263, 144);
            this.TBIDNo.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            this.TBIDNo.Name = "TBIDNo";
            this.TBIDNo.Size = new System.Drawing.Size(310, 43);
            this.TBIDNo.TabIndex = 20;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("TH Sarabun New", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(172, 87);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(83, 40);
            this.label8.TabIndex = 7;
            this.label8.Text = "ชื่อ-สกุล";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("TH Sarabun New", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(203, 30);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(52, 40);
            this.label9.TabIndex = 8;
            this.label9.Text = "รหัส";
            // 
            // TBTeacherNo
            // 
            this.TBTeacherNo.Location = new System.Drawing.Point(263, 30);
            this.TBTeacherNo.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            this.TBTeacherNo.Name = "TBTeacherNo";
            this.TBTeacherNo.Size = new System.Drawing.Size(310, 43);
            this.TBTeacherNo.TabIndex = 18;
            this.TBTeacherNo.TextChanged += new System.EventHandler(this.TBTeacherNo_TextChanged);
            // 
            // BSearchTeacher
            // 
            this.BSearchTeacher.BackColor = System.Drawing.Color.White;
            this.BSearchTeacher.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("BSearchTeacher.BackgroundImage")));
            this.BSearchTeacher.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.BSearchTeacher.ForeColor = System.Drawing.Color.White;
            this.BSearchTeacher.ImageKey = "(none)";
            this.BSearchTeacher.Location = new System.Drawing.Point(581, 30);
            this.BSearchTeacher.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            this.BSearchTeacher.Name = "BSearchTeacher";
            this.BSearchTeacher.Size = new System.Drawing.Size(43, 43);
            this.BSearchTeacher.TabIndex = 47;
            this.BSearchTeacher.UseVisualStyleBackColor = false;
            this.BSearchTeacher.Click += new System.EventHandler(this.BSearchTeacher_Click);
            // 
            // TBTeacherName
            // 
            this.TBTeacherName.Enabled = false;
            this.TBTeacherName.Location = new System.Drawing.Point(263, 87);
            this.TBTeacherName.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            this.TBTeacherName.Name = "TBTeacherName";
            this.TBTeacherName.Size = new System.Drawing.Size(310, 43);
            this.TBTeacherName.TabIndex = 34;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("TH Sarabun New", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(48, 144);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(207, 40);
            this.label3.TabIndex = 36;
            this.label3.Text = "หมายเลขบัตรประชาชน";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.button3);
            this.tabPage2.Controls.Add(this.button2);
            this.tabPage2.Controls.Add(this.textBox1);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Location = new System.Drawing.Point(4, 45);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(779, 216);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "เอกสาร";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.White;
            this.button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button3.Cursor = System.Windows.Forms.Cursors.Default;
            this.button3.Location = new System.Drawing.Point(23, 106);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(167, 47);
            this.button3.TabIndex = 22;
            this.button3.Text = "ส่งเอกสาร";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.White;
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button2.Cursor = System.Windows.Forms.Cursors.Default;
            this.button2.Location = new System.Drawing.Point(23, 53);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(167, 47);
            this.button2.TabIndex = 21;
            this.button2.Text = "ปริ้นเอกสารยกเลิกสมาชิก";
            this.button2.UseVisualStyleBackColor = false;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(196, 23);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(547, 163);
            this.textBox1.TabIndex = 18;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(24, 13);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(116, 37);
            this.label4.TabIndex = 17;
            this.label4.Text = "*ระบบสาเหตุ";
            // 
            // BExit
            // 
            this.BExit.BackColor = System.Drawing.Color.White;
            this.BExit.Font = new System.Drawing.Font("TH Sarabun New", 19.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BExit.Location = new System.Drawing.Point(991, 1060);
            this.BExit.Margin = new System.Windows.Forms.Padding(6, 16, 6, 16);
            this.BExit.Name = "BExit";
            this.BExit.Size = new System.Drawing.Size(172, 137);
            this.BExit.TabIndex = 53;
            this.BExit.Text = "ออก";
            this.BExit.UseVisualStyleBackColor = false;
            // 
            // CBStatus
            // 
            this.CBStatus.FormattingEnabled = true;
            this.CBStatus.Location = new System.Drawing.Point(934, 227);
            this.CBStatus.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            this.CBStatus.Name = "CBStatus";
            this.CBStatus.Size = new System.Drawing.Size(264, 44);
            this.CBStatus.TabIndex = 51;
            // 
            // DTPStartDate
            // 
            this.DTPStartDate.Enabled = false;
            this.DTPStartDate.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DTPStartDate.Location = new System.Drawing.Point(161, 73);
            this.DTPStartDate.Margin = new System.Windows.Forms.Padding(6, 16, 6, 16);
            this.DTPStartDate.Name = "DTPStartDate";
            this.DTPStartDate.Size = new System.Drawing.Size(272, 43);
            this.DTPStartDate.TabIndex = 48;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("TH Sarabun New", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(40, 75);
            this.label7.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(99, 40);
            this.label7.TabIndex = 39;
            this.label7.Text = "วันที่สมัคร";
            // 
            // BSave
            // 
            this.BSave.BackColor = System.Drawing.Color.White;
            this.BSave.Font = new System.Drawing.Font("TH Sarabun New", 19.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BSave.Location = new System.Drawing.Point(356, 1060);
            this.BSave.Margin = new System.Windows.Forms.Padding(6, 16, 6, 16);
            this.BSave.Name = "BSave";
            this.BSave.Size = new System.Drawing.Size(547, 137);
            this.BSave.TabIndex = 28;
            this.BSave.Text = "บันทึก";
            this.BSave.UseVisualStyleBackColor = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("TH Sarabun New", 24F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(306, 11);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(286, 53);
            this.label2.TabIndex = 1;
            this.label2.Text = "ยกเลิกสมาชิกสหกรณ์ครู";
            // 
            // CancelMembership
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 36F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(955, 590);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            this.Name = "CancelMembership";
            this.Text = "CancelMembership";
            this.Load += new System.EventHandler(this.CancelMembership_Load);
            this.SizeChanged += new System.EventHandler(this.CancelMembership_SizeChanged);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button BExit;
        private System.Windows.Forms.ComboBox CBStatus;
        private System.Windows.Forms.DateTimePicker DTPStartDate;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button BSave;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox TBIDNo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox TBTeacherNo;
        private System.Windows.Forms.Button BSearchTeacher;
        private System.Windows.Forms.TextBox TBTeacherName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
    }
}