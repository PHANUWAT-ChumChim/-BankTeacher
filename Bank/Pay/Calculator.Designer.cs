﻿
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
            this.TBAmount.Location = new System.Drawing.Point(86, 76);
            this.TBAmount.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.TBAmount.Multiline = true;
            this.TBAmount.Name = "TBAmount";
            this.TBAmount.ReadOnly = true;
            this.TBAmount.Size = new System.Drawing.Size(210, 39);
            this.TBAmount.TabIndex = 1;
            this.TBAmount.Text = "0";
            this.TBAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TBGetAmount
            // 
            this.TBGetAmount.Font = new System.Drawing.Font("TH Sarabun New", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.TBGetAmount.Location = new System.Drawing.Point(86, 11);
            this.TBGetAmount.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.TBGetAmount.Multiline = true;
            this.TBGetAmount.Name = "TBGetAmount";
            this.TBGetAmount.Size = new System.Drawing.Size(210, 39);
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
            this.TBTON.Location = new System.Drawing.Point(86, 147);
            this.TBTON.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.TBTON.Multiline = true;
            this.TBTON.Name = "TBTON";
            this.TBTON.ReadOnly = true;
            this.TBTON.Size = new System.Drawing.Size(210, 39);
            this.TBTON.TabIndex = 2;
            this.TBTON.Text = "0";
            this.TBTON.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("TH Sarabun New", 17.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label1.Location = new System.Drawing.Point(41, 80);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 31);
            this.label1.TabIndex = 1;
            this.label1.Text = "ยอด";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("TH Sarabun New", 17.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label2.Location = new System.Drawing.Point(13, 15);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 31);
            this.label2.TabIndex = 1;
            this.label2.Text = "รับเงินมา";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("TH Sarabun New", 17.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label3.Location = new System.Drawing.Point(41, 151);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 31);
            this.label3.TabIndex = 1;
            this.label3.Text = "ทอน";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("TH Sarabun New", 17.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label4.Location = new System.Drawing.Point(300, 151);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 31);
            this.label4.TabIndex = 1;
            this.label4.Text = "บาท";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("TH Sarabun New", 17.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label5.Location = new System.Drawing.Point(300, 15);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 31);
            this.label5.TabIndex = 1;
            this.label5.Text = "บาท";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("TH Sarabun New", 17.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.label6.Location = new System.Drawing.Point(297, 80);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 31);
            this.label6.TabIndex = 1;
            this.label6.Text = "บาท";
            // 
            // Calculator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(368, 206);
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
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Calculator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Calculator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TBAmount;
        private System.Windows.Forms.TextBox TBGetAmount;
        private System.Windows.Forms.TextBox TBTON;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}