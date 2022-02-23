
namespace BankTeacher.Bank.Pay
{
    partial class Calculator
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
            this.TBAmount = new System.Windows.Forms.TextBox();
            this.TBGetAmount = new System.Windows.Forms.TextBox();
            this.TBTON = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // TBAmount
            // 
            this.TBAmount.BackColor = System.Drawing.Color.White;
            this.TBAmount.Font = new System.Drawing.Font("TH Sarabun New", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.TBAmount.Location = new System.Drawing.Point(115, 94);
            this.TBAmount.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TBAmount.Multiline = true;
            this.TBAmount.Name = "TBAmount";
            this.TBAmount.ReadOnly = true;
            this.TBAmount.Size = new System.Drawing.Size(279, 47);
            this.TBAmount.TabIndex = 1;
            this.TBAmount.Text = "0";
            this.TBAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TBGetAmount
            // 
            this.TBGetAmount.Font = new System.Drawing.Font("TH Sarabun New", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.TBGetAmount.Location = new System.Drawing.Point(115, 14);
            this.TBGetAmount.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TBGetAmount.Multiline = true;
            this.TBGetAmount.Name = "TBGetAmount";
            this.TBGetAmount.Size = new System.Drawing.Size(279, 47);
            this.TBGetAmount.TabIndex = 0;
            this.TBGetAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TBGetAmount.TextChanged += new System.EventHandler(this.TBGetAmount_TextChanged);
            this.TBGetAmount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TBGetAmount_KeyDown);
            this.TBGetAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TBGetAmount_KeyPress);
            // 
            // TBTON
            // 
            this.TBTON.BackColor = System.Drawing.Color.White;
            this.TBTON.Font = new System.Drawing.Font("TH Sarabun New", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.TBTON.Location = new System.Drawing.Point(115, 181);
            this.TBTON.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TBTON.Multiline = true;
            this.TBTON.Name = "TBTON";
            this.TBTON.ReadOnly = true;
            this.TBTON.Size = new System.Drawing.Size(279, 47);
            this.TBTON.TabIndex = 2;
            this.TBTON.Text = "0";
            this.TBTON.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("TH Sarabun New", 17.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(55, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 39);
            this.label1.TabIndex = 1;
            this.label1.Text = "ยอด";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("TH Sarabun New", 17.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.Location = new System.Drawing.Point(17, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 39);
            this.label2.TabIndex = 1;
            this.label2.Text = "รับเงินมา";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("TH Sarabun New", 17.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label3.Location = new System.Drawing.Point(55, 186);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 39);
            this.label3.TabIndex = 1;
            this.label3.Text = "ทอน";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("TH Sarabun New", 17.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label4.Location = new System.Drawing.Point(400, 186);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 39);
            this.label4.TabIndex = 1;
            this.label4.Text = "บาท";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("TH Sarabun New", 17.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label5.Location = new System.Drawing.Point(400, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 39);
            this.label5.TabIndex = 1;
            this.label5.Text = "บาท";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("TH Sarabun New", 17.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label6.Location = new System.Drawing.Point(396, 98);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 39);
            this.label6.TabIndex = 1;
            this.label6.Text = "บาท";
            // 
            // Calculator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(491, 301);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TBTON);
            this.Controls.Add(this.TBGetAmount);
            this.Controls.Add(this.TBAmount);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Calculator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Calculator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox TBAmount;
        public System.Windows.Forms.TextBox TBGetAmount;
        public System.Windows.Forms.TextBox TBTON;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.Label label4;
        public System.Windows.Forms.Label label5;
        public System.Windows.Forms.Label label6;
    }
}