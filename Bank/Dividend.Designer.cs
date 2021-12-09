
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.BExitForm = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.CBYearDividend = new System.Windows.Forms.ComboBox();
            this.BSaveDividend = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.BExitForm);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.CBYearDividend);
            this.panel1.Controls.Add(this.BSaveDividend);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(15, 16);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(877, 622);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // BExitForm
            // 
            this.BExitForm.BackgroundImage = global::BankTeacher.Properties.Resources.logout;
            this.BExitForm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.BExitForm.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.BExitForm.Location = new System.Drawing.Point(786, 535);
            this.BExitForm.Name = "BExitForm";
            this.BExitForm.Size = new System.Drawing.Size(73, 66);
            this.BExitForm.TabIndex = 114;
            this.BExitForm.UseVisualStyleBackColor = true;
            this.BExitForm.Click += new System.EventHandler(this.BExitForm_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(156, 185);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(21, 28);
            this.label5.TabIndex = 8;
            this.label5.Text = "ปี";
            // 
            // CBYearDividend
            // 
            this.CBYearDividend.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBYearDividend.FormattingEnabled = true;
            this.CBYearDividend.Location = new System.Drawing.Point(183, 182);
            this.CBYearDividend.Name = "CBYearDividend";
            this.CBYearDividend.Size = new System.Drawing.Size(121, 36);
            this.CBYearDividend.TabIndex = 7;
            // 
            // BSaveDividend
            // 
            this.BSaveDividend.Enabled = false;
            this.BSaveDividend.Location = new System.Drawing.Point(507, 236);
            this.BSaveDividend.Name = "BSaveDividend";
            this.BSaveDividend.Size = new System.Drawing.Size(214, 173);
            this.BSaveDividend.TabIndex = 6;
            this.BSaveDividend.Text = "บันทึกปันผล";
            this.BSaveDividend.UseVisualStyleBackColor = true;
            this.BSaveDividend.Click += new System.EventHandler(this.BSaveDividend_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(225, 323);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(276, 28);
            this.label2.TabIndex = 4;
            this.label2.Text = "และเมื่อมีผู้ที่ต้องการกู้ต้องเริ่มชำระภายในปีหน้า";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(198, 295);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(303, 28);
            this.label1.TabIndex = 5;
            this.label1.Text = "**เมื่อปันผลแล้วจะไม่สามารถจ่ายเงินกู้ภายในปีนั้นได้";
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
            this.Text = "Dividend";
            this.Load += new System.EventHandler(this.Dividend_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Dividend_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
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
    }
}