
namespace BankTeacher.Bank
{
    partial class Home
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Home));
            this.panel7 = new System.Windows.Forms.Panel();
            this.BSearchTeacher = new System.Windows.Forms.Button();
            this.TBTeacherName = new System.Windows.Forms.TextBox();
            this.TBTeacherNo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.P1 = new System.Windows.Forms.Panel();
            this.BTPrint = new System.Windows.Forms.Button();
            this.panel16 = new System.Windows.Forms.Panel();
            this.LB5Ye = new System.Windows.Forms.Label();
            this.CByear = new System.Windows.Forms.ComboBox();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.panel7.SuspendLayout();
            this.P1.SuspendLayout();
            this.panel16.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            this.SuspendLayout();
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.White;
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel7.Controls.Add(this.BSearchTeacher);
            this.panel7.Controls.Add(this.TBTeacherName);
            this.panel7.Controls.Add(this.TBTeacherNo);
            this.panel7.Controls.Add(this.label2);
            this.panel7.Controls.Add(this.label1);
            this.panel7.Controls.Add(this.label12);
            this.panel7.Location = new System.Drawing.Point(6, 8);
            this.panel7.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(850, 59);
            this.panel7.TabIndex = 65;
            // 
            // BSearchTeacher
            // 
            this.BSearchTeacher.BackColor = System.Drawing.Color.White;
            this.BSearchTeacher.BackgroundImage = global::BankTeacher.Properties.Resources._64x64_magnifying_glass;
            this.BSearchTeacher.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.BSearchTeacher.ForeColor = System.Drawing.Color.White;
            this.BSearchTeacher.Location = new System.Drawing.Point(171, 10);
            this.BSearchTeacher.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.BSearchTeacher.Name = "BSearchTeacher";
            this.BSearchTeacher.Size = new System.Drawing.Size(32, 35);
            this.BSearchTeacher.TabIndex = 86;
            this.BSearchTeacher.UseVisualStyleBackColor = false;
            this.BSearchTeacher.Click += new System.EventHandler(this.BSearchTeacher_Click);
            // 
            // TBTeacherName
            // 
            this.TBTeacherName.Enabled = false;
            this.TBTeacherName.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TBTeacherName.Location = new System.Drawing.Point(270, 12);
            this.TBTeacherName.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.TBTeacherName.Name = "TBTeacherName";
            this.TBTeacherName.Size = new System.Drawing.Size(562, 36);
            this.TBTeacherName.TabIndex = 83;
            // 
            // TBTeacherNo
            // 
            this.TBTeacherNo.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TBTeacherNo.Location = new System.Drawing.Point(52, 10);
            this.TBTeacherNo.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.TBTeacherNo.MaxLength = 6;
            this.TBTeacherNo.Name = "TBTeacherNo";
            this.TBTeacherNo.Size = new System.Drawing.Size(116, 36);
            this.TBTeacherNo.TabIndex = 82;
            this.TBTeacherNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TBTeacherNo_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(207, 12);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 30);
            this.label2.TabIndex = 68;
            this.label2.Text = "ชื่อ-สกุล";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(183, 14);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 26);
            this.label1.TabIndex = 68;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.White;
            this.label12.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(10, 10);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(39, 30);
            this.label12.TabIndex = 67;
            this.label12.Text = "รหัส";
            // 
            // P1
            // 
            this.P1.BackColor = System.Drawing.Color.White;
            this.P1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.P1.Controls.Add(this.BTPrint);
            this.P1.Controls.Add(this.panel16);
            this.P1.Controls.Add(this.dataGridView3);
            this.P1.Controls.Add(this.panel7);
            this.P1.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.P1.Location = new System.Drawing.Point(19, 34);
            this.P1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.P1.Name = "P1";
            this.P1.Size = new System.Drawing.Size(868, 589);
            this.P1.TabIndex = 98;
            // 
            // BTPrint
            // 
            this.BTPrint.BackgroundImage = global::BankTeacher.Properties.Resources._10x10_Print;
            this.BTPrint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.BTPrint.CausesValidation = false;
            this.BTPrint.Enabled = false;
            this.BTPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BTPrint.Location = new System.Drawing.Point(760, 78);
            this.BTPrint.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.BTPrint.Name = "BTPrint";
            this.BTPrint.Size = new System.Drawing.Size(95, 47);
            this.BTPrint.TabIndex = 106;
            this.BTPrint.UseVisualStyleBackColor = true;
            this.BTPrint.Click += new System.EventHandler(this.BTPrint_Click);
            // 
            // panel16
            // 
            this.panel16.BackColor = System.Drawing.Color.White;
            this.panel16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel16.Controls.Add(this.LB5Ye);
            this.panel16.Controls.Add(this.CByear);
            this.panel16.Location = new System.Drawing.Point(6, 72);
            this.panel16.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel16.Name = "panel16";
            this.panel16.Size = new System.Drawing.Size(168, 59);
            this.panel16.TabIndex = 99;
            // 
            // LB5Ye
            // 
            this.LB5Ye.AutoSize = true;
            this.LB5Ye.BackColor = System.Drawing.Color.White;
            this.LB5Ye.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LB5Ye.ForeColor = System.Drawing.Color.Black;
            this.LB5Ye.Location = new System.Drawing.Point(7, 14);
            this.LB5Ye.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LB5Ye.Name = "LB5Ye";
            this.LB5Ye.Size = new System.Drawing.Size(23, 30);
            this.LB5Ye.TabIndex = 99;
            this.LB5Ye.Text = "ปี";
            // 
            // CByear
            // 
            this.CByear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CByear.Enabled = false;
            this.CByear.Font = new System.Drawing.Font("TH Sarabun New", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CByear.FormattingEnabled = true;
            this.CByear.Location = new System.Drawing.Point(33, 12);
            this.CByear.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.CByear.Name = "CByear";
            this.CByear.Size = new System.Drawing.Size(108, 33);
            this.CByear.TabIndex = 84;
            this.CByear.SelectedIndexChanged += new System.EventHandler(this.CByear_SelectedIndexChanged);
            this.CByear.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CByear_KeyDown);
            // 
            // dataGridView3
            // 
            this.dataGridView3.AllowUserToAddRows = false;
            this.dataGridView3.AllowUserToDeleteRows = false;
            this.dataGridView3.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dataGridView3.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView3.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView3.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column2,
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.Column1});
            this.dataGridView3.GridColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridView3.Location = new System.Drawing.Point(6, 135);
            this.dataGridView3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView3.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridView3.RowHeadersVisible = false;
            this.dataGridView3.RowHeadersWidth = 51;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridView3.RowsDefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridView3.RowTemplate.Height = 24;
            this.dataGridView3.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView3.Size = new System.Drawing.Size(850, 439);
            this.dataGridView3.TabIndex = 66;
            this.dataGridView3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView3_KeyDown);
            // 
            // Column2
            // 
            this.Column2.HeaderText = "ลำดับที่";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column2.Visible = false;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewCellStyle2.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewTextBoxColumn1.HeaderText = "เดือน / ปี";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewCellStyle3.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewTextBoxColumn3.HeaderText = "รายการ";
            this.dataGridViewTextBoxColumn3.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewCellStyle4.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridViewTextBoxColumn4.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewTextBoxColumn4.HeaderText = "ยอดเงิน";
            this.dataGridViewTextBoxColumn4.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column1
            // 
            dataGridViewCellStyle5.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Column1.DefaultCellStyle = dataGridViewCellStyle5;
            this.Column1.HeaderText = "สถานะ";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
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
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(904, 657);
            this.Controls.Add(this.P1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Home";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Home";
            this.SizeChanged += new System.EventHandler(this.Form2_SizeChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Home_KeyDown);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.P1.ResumeLayout(false);
            this.panel16.ResumeLayout(false);
            this.panel16.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.TextBox TBTeacherName;
        private System.Windows.Forms.TextBox TBTeacherNo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Panel P1;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.Button BSearchTeacher;
        private System.Windows.Forms.Panel panel16;
        private System.Windows.Forms.Label LB5Ye;
        private System.Windows.Forms.ComboBox CByear;
        private System.Windows.Forms.Button BTPrint;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
    }
}