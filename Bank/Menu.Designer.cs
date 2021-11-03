
namespace BankTeacher.Bank
{
    partial class Menu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Menu));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.Home_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pay = new System.Windows.Forms.ToolStripMenuItem();
            this.Loan = new System.Windows.Forms.ToolStripMenuItem();
            this.สมครกToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.จายกToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ดขอมลกToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Cancel = new System.Windows.Forms.ToolStripMenuItem();
            this.AmountOff = new System.Windows.Forms.ToolStripMenuItem();
            this.Member_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SentingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Home_ToolStripMenuItem,
            this.pay,
            this.Loan,
            this.AmountOff,
            this.Member_ToolStripMenuItem,
            this.SentingToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(882, 38);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemAdded += new System.Windows.Forms.ToolStripItemEventHandler(this.menuStrip1_ItemAdded);
            // 
            // Home_ToolStripMenuItem
            // 
            this.Home_ToolStripMenuItem.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Home_ToolStripMenuItem.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Home_ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("Home_ToolStripMenuItem.Image")));
            this.Home_ToolStripMenuItem.Name = "Home_ToolStripMenuItem";
            this.Home_ToolStripMenuItem.Size = new System.Drawing.Size(99, 34);
            this.Home_ToolStripMenuItem.Text = "หน้าเเรก";
            this.Home_ToolStripMenuItem.Click += new System.EventHandler(this.หนาเเรกToolStripMenuItem_Click);
            // 
            // pay
            // 
            this.pay.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pay.Image = ((System.Drawing.Image)(resources.GetObject("pay.Image")));
            this.pay.Name = "pay";
            this.pay.Size = new System.Drawing.Size(124, 34);
            this.pay.Text = "จ่าย สะสม/กู้";
            this.pay.Click += new System.EventHandler(this.pay_Click);
            // 
            // Loan
            // 
            this.Loan.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.สมครกToolStripMenuItem,
            this.จายกToolStripMenuItem,
            this.ดขอมลกToolStripMenuItem,
            this.Cancel});
            this.Loan.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Loan.Image = ((System.Drawing.Image)(resources.GetObject("Loan.Image")));
            this.Loan.Name = "Loan";
            this.Loan.Size = new System.Drawing.Size(54, 34);
            this.Loan.Text = "กู้";
            // 
            // สมครกToolStripMenuItem
            // 
            this.สมครกToolStripMenuItem.Name = "สมครกToolStripMenuItem";
            this.สมครกToolStripMenuItem.Size = new System.Drawing.Size(140, 34);
            this.สมครกToolStripMenuItem.Text = "สมัครกู้";
            this.สมครกToolStripMenuItem.Click += new System.EventHandler(this.สมครกToolStripMenuItem_Click);
            // 
            // จายกToolStripMenuItem
            // 
            this.จายกToolStripMenuItem.Name = "จายกToolStripMenuItem";
            this.จายกToolStripMenuItem.Size = new System.Drawing.Size(140, 34);
            this.จายกToolStripMenuItem.Text = "จ่ายกู้";
            this.จายกToolStripMenuItem.Click += new System.EventHandler(this.จายกToolStripMenuItem_Click);
            // 
            // ดขอมลกToolStripMenuItem
            // 
            this.ดขอมลกToolStripMenuItem.Name = "ดขอมลกToolStripMenuItem";
            this.ดขอมลกToolStripMenuItem.Size = new System.Drawing.Size(140, 34);
            this.ดขอมลกToolStripMenuItem.Text = "ดูข้อมุลกู้";
            this.ดขอมลกToolStripMenuItem.Click += new System.EventHandler(this.ดขอมลกToolStripMenuItem_Click);
            // 
            // Cancel
            // 
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(140, 34);
            this.Cancel.Text = "ยกเลิกกู้";
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // AmountOff
            // 
            this.AmountOff.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AmountOff.Image = ((System.Drawing.Image)(resources.GetObject("AmountOff.Image")));
            this.AmountOff.Name = "AmountOff";
            this.AmountOff.Size = new System.Drawing.Size(90, 34);
            this.AmountOff.Text = "ปิดยอด";
            this.AmountOff.Click += new System.EventHandler(this.AmountOff_Click);
            // 
            // Member_ToolStripMenuItem
            // 
            this.Member_ToolStripMenuItem.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Member_ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("Member_ToolStripMenuItem.Image")));
            this.Member_ToolStripMenuItem.Name = "Member_ToolStripMenuItem";
            this.Member_ToolStripMenuItem.Size = new System.Drawing.Size(89, 34);
            this.Member_ToolStripMenuItem.Text = "สมาชิก";
            this.Member_ToolStripMenuItem.Click += new System.EventHandler(this.สมครสมาชกToolStripMenuItem_Click);
            // 
            // SentingToolStripMenuItem
            // 
            this.SentingToolStripMenuItem.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SentingToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("SentingToolStripMenuItem.Image")));
            this.SentingToolStripMenuItem.Name = "SentingToolStripMenuItem";
            this.SentingToolStripMenuItem.Size = new System.Drawing.Size(78, 34);
            this.SentingToolStripMenuItem.Text = "ตั้งค่า";
            this.SentingToolStripMenuItem.Click += new System.EventHandler(this.SettingToolStripMenuItem_Click);
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(882, 643);
            this.Controls.Add(this.menuStrip1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Menu";
            this.Text = "หน้าเเรก";
            this.Load += new System.EventHandler(this.Menu_Load_1);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem Home_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pay;
        private System.Windows.Forms.ToolStripMenuItem Loan;
        private System.Windows.Forms.ToolStripMenuItem AmountOff;
        private System.Windows.Forms.ToolStripMenuItem Member_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SentingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem จายกToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ดขอมลกToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Cancel;
        private System.Windows.Forms.ToolStripMenuItem สมครกToolStripMenuItem;
    }
}