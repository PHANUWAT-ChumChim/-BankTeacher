
namespace example.Bank
{
    partial class Setting
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
            this.P1 = new System.Windows.Forms.Panel();
            this.LBsetting = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.B_Cancel = new System.Windows.Forms.Button();
            this.B_Save = new System.Windows.Forms.Button();
            this.TB_Max = new System.Windows.Forms.TextBox();
            this.TB_Min = new System.Windows.Forms.TextBox();
            this.ตั้งการเเก้ไขเวลา = new System.Windows.Forms.TabPage();
            this.CHB_edittime = new System.Windows.Forms.CheckBox();
            this.P1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.ตั้งการเเก้ไขเวลา.SuspendLayout();
            this.SuspendLayout();
            // 
            // P1
            // 
            this.P1.BackColor = System.Drawing.Color.White;
            this.P1.Controls.Add(this.LBsetting);
            this.P1.Controls.Add(this.tabControl1);
            this.P1.Location = new System.Drawing.Point(18, 42);
            this.P1.Name = "P1";
            this.P1.Size = new System.Drawing.Size(732, 657);
            this.P1.TabIndex = 99;
            // 
            // LBsetting
            // 
            this.LBsetting.AutoSize = true;
            this.LBsetting.Font = new System.Drawing.Font("TH Sarabun New", 30F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBsetting.Location = new System.Drawing.Point(290, 14);
            this.LBsetting.Name = "LBsetting";
            this.LBsetting.Size = new System.Drawing.Size(178, 66);
            this.LBsetting.TabIndex = 99;
            this.LBsetting.Text = "ตั้งค่าระบบ";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.ตั้งการเเก้ไขเวลา);
            this.tabControl1.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(32, 92);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(678, 535);
            this.tabControl1.TabIndex = 98;
            //this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.White;
            this.tabPage1.Controls.Add(this.comboBox2);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.comboBox1);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage1.Location = new System.Drawing.Point(4, 45);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(670, 486);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "กำหนดค่าเริ่มต้น";
            // 
            // comboBox2
            // 
            this.comboBox2.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "16",
            "18"});
            this.comboBox2.Location = new System.Drawing.Point(13, 182);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(224, 44);
            this.comboBox2.TabIndex = 15;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(6, 142);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 37);
            this.label4.TabIndex = 14;
            this.label4.Text = "ขนาด";
            // 
            // comboBox1
            // 
            this.comboBox1.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(13, 77);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(224, 44);
            this.comboBox1.TabIndex = 13;
            this.comboBox1.Text = "TH Sarabun New";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 37);
            this.label3.TabIndex = 12;
            this.label3.Text = "เลือกฟอนต์";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.B_Cancel);
            this.tabPage2.Controls.Add(this.B_Save);
            this.tabPage2.Controls.Add(this.TB_Max);
            this.tabPage2.Controls.Add(this.TB_Min);
            this.tabPage2.Location = new System.Drawing.Point(4, 45);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(670, 486);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "หุ้น";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(45, 233);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(150, 37);
            this.label2.TabIndex = 14;
            this.label2.Text = "จำนวนที่มากที่สุด";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(45, 169);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(139, 37);
            this.label1.TabIndex = 15;
            this.label1.Text = "จำนวนน้อยที่สุด";
            // 
            // B_Cancel
            // 
            this.B_Cancel.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.B_Cancel.Location = new System.Drawing.Point(231, 414);
            this.B_Cancel.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.B_Cancel.Name = "B_Cancel";
            this.B_Cancel.Size = new System.Drawing.Size(97, 45);
            this.B_Cancel.TabIndex = 17;
            this.B_Cancel.Text = "ยกเลิก";
            this.B_Cancel.UseVisualStyleBackColor = true;
            this.B_Cancel.Click += new System.EventHandler(this.B_Cancel_Click);
            // 
            // B_Save
            // 
            this.B_Save.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.B_Save.Location = new System.Drawing.Point(354, 367);
            this.B_Save.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.B_Save.Name = "B_Save";
            this.B_Save.Size = new System.Drawing.Size(298, 92);
            this.B_Save.TabIndex = 16;
            this.B_Save.Text = "บันทึก";
            this.B_Save.UseVisualStyleBackColor = true;
            this.B_Save.Click += new System.EventHandler(this.B_Save_Click);
            // 
            // TB_Max
            // 
            this.TB_Max.Location = new System.Drawing.Point(251, 233);
            this.TB_Max.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.TB_Max.MaxLength = 10;
            this.TB_Max.Name = "TB_Max";
            this.TB_Max.Size = new System.Drawing.Size(238, 43);
            this.TB_Max.TabIndex = 12;
            this.TB_Max.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TB_Max_KeyPress);
            // 
            // TB_Min
            // 
            this.TB_Min.Location = new System.Drawing.Point(251, 169);
            this.TB_Min.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.TB_Min.MaxLength = 10;
            this.TB_Min.Name = "TB_Min";
            this.TB_Min.Size = new System.Drawing.Size(238, 43);
            this.TB_Min.TabIndex = 13;
            this.TB_Min.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TB_Min_KeyPress);
            // 
            // ตั้งการเเก้ไขเวลา
            // 
            this.ตั้งการเเก้ไขเวลา.Controls.Add(this.CHB_edittime);
            this.ตั้งการเเก้ไขเวลา.Location = new System.Drawing.Point(4, 45);
            this.ตั้งการเเก้ไขเวลา.Name = "ตั้งการเเก้ไขเวลา";
            this.ตั้งการเเก้ไขเวลา.Padding = new System.Windows.Forms.Padding(3);
            this.ตั้งการเเก้ไขเวลา.Size = new System.Drawing.Size(670, 486);
            this.ตั้งการเเก้ไขเวลา.TabIndex = 2;
            this.ตั้งการเเก้ไขเวลา.Text = "ตั้งการเเก้ไขเวลา";
            this.ตั้งการเเก้ไขเวลา.UseVisualStyleBackColor = true;
            // 
            // CHB_edittime
            // 
            this.CHB_edittime.AutoSize = true;
            this.CHB_edittime.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CHB_edittime.Location = new System.Drawing.Point(437, 24);
            this.CHB_edittime.Name = "CHB_edittime";
            this.CHB_edittime.Size = new System.Drawing.Size(196, 41);
            this.CHB_edittime.TabIndex = 8;
            this.CHB_edittime.Text = "อณุญาตให้แก้ไขเวลา";
            this.CHB_edittime.UseVisualStyleBackColor = true;
            // 
            // Setting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(769, 712);
            this.Controls.Add(this.P1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Setting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Setting";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Setting_FormClosed);
            this.Load += new System.EventHandler(this.B_Save_Click_1);
            this.P1.ResumeLayout(false);
            this.P1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ตั้งการเเก้ไขเวลา.ResumeLayout(false);
            this.ตั้งการเเก้ไขเวลา.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel P1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label LBsetting;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button B_Cancel;
        private System.Windows.Forms.Button B_Save;
        private System.Windows.Forms.TextBox TB_Max;
        private System.Windows.Forms.TextBox TB_Min;
        private System.Windows.Forms.TabPage ตั้งการเเก้ไขเวลา;
        private System.Windows.Forms.CheckBox CHB_edittime;
    }
}