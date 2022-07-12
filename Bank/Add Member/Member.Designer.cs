
namespace BankTeacher.Bank.Add_Member
{
    partial class Member
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Member));
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.BExitForm = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.BTSave_Reg = new System.Windows.Forms.Button();
            this.TBTeacherName_Reg = new System.Windows.Forms.TextBox();
            this.BTSearchTeacher_Reg = new System.Windows.Forms.Button();
            this.TBTeacherNo_Reg = new System.Windows.Forms.TextBox();
            this.TBStartAmountShare_Reg = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("TH Sarabun New", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(22, 94);
            this.label8.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 32);
            this.label8.TabIndex = 7;
            this.label8.Text = "ชื่อ-สกุล";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("TH Sarabun New", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(34, 36);
            this.label9.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(42, 32);
            this.label9.TabIndex = 8;
            this.label9.Text = "รหัส";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel1.Controls.Add(this.BExitForm);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Location = new System.Drawing.Point(17, 34);
            this.panel1.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(869, 589);
            this.panel1.TabIndex = 18;
            // 
            // BExitForm
            // 
            this.BExitForm.BackgroundImage = global::BankTeacher.Properties.Resources.logout;
            this.BExitForm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.BExitForm.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.BExitForm.Location = new System.Drawing.Point(600, 491);
            this.BExitForm.Name = "BExitForm";
            this.BExitForm.Size = new System.Drawing.Size(73, 66);
            this.BExitForm.TabIndex = 81;
            this.BExitForm.UseVisualStyleBackColor = true;
            this.BExitForm.Click += new System.EventHandler(this.BExitForm_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.BTSave_Reg);
            this.groupBox1.Controls.Add(this.TBTeacherName_Reg);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.BTSearchTeacher_Reg);
            this.groupBox1.Controls.Add(this.TBTeacherNo_Reg);
            this.groupBox1.Controls.Add(this.TBStartAmountShare_Reg);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Font = new System.Drawing.Font("TH Sarabun New", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(196, 20);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.groupBox1.Size = new System.Drawing.Size(477, 466);
            this.groupBox1.TabIndex = 48;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "สมัครสมาชิก";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(11, 418);
            this.label4.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(286, 30);
            this.label4.TabIndex = 17;
            this.label4.Text = "*ชี้แจง หุ้นที่ซื้อ สามารถเปลี่ยนแปลงราคาซื้อได้";
            // 
            // BTSave_Reg
            // 
            this.BTSave_Reg.BackColor = System.Drawing.Color.White;
            this.BTSave_Reg.Enabled = false;
            this.BTSave_Reg.Font = new System.Drawing.Font("TH Sarabun New", 19.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTSave_Reg.Location = new System.Drawing.Point(275, 230);
            this.BTSave_Reg.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
            this.BTSave_Reg.Name = "BTSave_Reg";
            this.BTSave_Reg.Size = new System.Drawing.Size(168, 60);
            this.BTSave_Reg.TabIndex = 28;
            this.BTSave_Reg.Text = "บันทึก";
            this.BTSave_Reg.UseVisualStyleBackColor = false;
            this.BTSave_Reg.Click += new System.EventHandler(this.BSave_Click_1);
            // 
            // TBTeacherName_Reg
            // 
            this.TBTeacherName_Reg.Enabled = false;
            this.TBTeacherName_Reg.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TBTeacherName_Reg.Location = new System.Drawing.Point(118, 93);
            this.TBTeacherName_Reg.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
            this.TBTeacherName_Reg.Name = "TBTeacherName_Reg";
            this.TBTeacherName_Reg.Size = new System.Drawing.Size(253, 36);
            this.TBTeacherName_Reg.TabIndex = 34;
            // 
            // BTSearchTeacher_Reg
            // 
            this.BTSearchTeacher_Reg.BackColor = System.Drawing.Color.White;
            this.BTSearchTeacher_Reg.BackgroundImage = global::BankTeacher.Properties.Resources._64x64_magnifying_glass;
            this.BTSearchTeacher_Reg.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.BTSearchTeacher_Reg.ForeColor = System.Drawing.Color.White;
            this.BTSearchTeacher_Reg.ImageKey = "(none)";
            this.BTSearchTeacher_Reg.Location = new System.Drawing.Point(381, 32);
            this.BTSearchTeacher_Reg.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
            this.BTSearchTeacher_Reg.Name = "BTSearchTeacher_Reg";
            this.BTSearchTeacher_Reg.Size = new System.Drawing.Size(42, 42);
            this.BTSearchTeacher_Reg.TabIndex = 47;
            this.BTSearchTeacher_Reg.UseVisualStyleBackColor = false;
            this.BTSearchTeacher_Reg.Click += new System.EventHandler(this.BTSearchTeacher_Click);
            // 
            // TBTeacherNo_Reg
            // 
            this.TBTeacherNo_Reg.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TBTeacherNo_Reg.Location = new System.Drawing.Point(118, 33);
            this.TBTeacherNo_Reg.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
            this.TBTeacherNo_Reg.Name = "TBTeacherNo_Reg";
            this.TBTeacherNo_Reg.Size = new System.Drawing.Size(253, 36);
            this.TBTeacherNo_Reg.TabIndex = 18;
            this.TBTeacherNo_Reg.EnabledChanged += new System.EventHandler(this.TBTeacherNo_Reg_EnabledChanged);
            this.TBTeacherNo_Reg.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TBTeacherNo_KeyDown);
            // 
            // TBStartAmountShare_Reg
            // 
            this.TBStartAmountShare_Reg.Enabled = false;
            this.TBStartAmountShare_Reg.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TBStartAmountShare_Reg.Location = new System.Drawing.Point(211, 153);
            this.TBStartAmountShare_Reg.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.TBStartAmountShare_Reg.MaxLength = 10;
            this.TBStartAmountShare_Reg.Name = "TBStartAmountShare_Reg";
            this.TBStartAmountShare_Reg.Size = new System.Drawing.Size(160, 36);
            this.TBStartAmountShare_Reg.TabIndex = 16;
            this.TBStartAmountShare_Reg.Text = "0";
            this.TBStartAmountShare_Reg.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TBStartAmountShare_Reg.TextChanged += new System.EventHandler(this.TBStartAmountShare_Reg_TextChanged);
            this.TBStartAmountShare_Reg.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TBStartAmountShare_KeyPress);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(393, 159);
            this.label10.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(38, 30);
            this.label10.TabIndex = 15;
            this.label10.Text = "บาท";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(22, 156);
            this.label16.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(142, 30);
            this.label16.TabIndex = 15;
            this.label16.Text = "เลือกจำนวนเงินเริ่มต้น";
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
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // Member
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(903, 657);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
            this.Name = "Member";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "membership";
            this.SizeChanged += new System.EventHandler(this.membership_SizeChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Member_KeyDown);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox TBTeacherNo_Reg;
        private System.Windows.Forms.Button BTSave_Reg;
        private System.Windows.Forms.TextBox TBTeacherName_Reg;
        private System.Windows.Forms.Button BTSearchTeacher_Reg;
        private System.Windows.Forms.Label label4;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Windows.Forms.TextBox TBStartAmountShare_Reg;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button BExitForm;
        private System.Windows.Forms.PrintDialog printDialog1;
    }
}