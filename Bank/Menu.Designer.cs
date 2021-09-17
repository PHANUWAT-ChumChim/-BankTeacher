
namespace example.GOODS
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
            this.จายยอดToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ขอมลกToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ขอมลสมาชกToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Loan = new System.Windows.Forms.ToolStripMenuItem();
            this.สมครกToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.จายกToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ดขอมลกToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Cancel = new System.Windows.Forms.ToolStripMenuItem();
            this.ปดยอดToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Member_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TMLCancelMembers = new System.Windows.Forms.ToolStripMenuItem();
            this.ReportCancelMember = new System.Windows.Forms.ToolStripMenuItem();
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
            this.ปดยอดToolStripMenuItem,
            this.Member_ToolStripMenuItem,
            this.SentingToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1176, 45);
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
            this.Home_ToolStripMenuItem.Size = new System.Drawing.Size(117, 41);
            this.Home_ToolStripMenuItem.Text = "หน้าเเรก";
            this.Home_ToolStripMenuItem.Click += new System.EventHandler(this.หนาเเรกToolStripMenuItem_Click);
            // 
            // pay
            // 
            this.pay.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.จายยอดToolStripMenuItem,
            this.ToolStripMenuItem,
            this.ขอมลกToolStripMenuItem,
            this.ขอมลสมาชกToolStripMenuItem});
            this.pay.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pay.Image = ((System.Drawing.Image)(resources.GetObject("pay.Image")));
            this.pay.Name = "pay";
            this.pay.Size = new System.Drawing.Size(152, 41);
            this.pay.Text = "จ่าย สะสม/กู้";
            this.pay.Click += new System.EventHandler(this.pay_Click);
            // 
            // จายยอดToolStripMenuItem
            // 
            this.จายยอดToolStripMenuItem.Name = "จายยอดToolStripMenuItem";
            this.จายยอดToolStripMenuItem.Size = new System.Drawing.Size(222, 42);
            this.จายยอดToolStripMenuItem.Text = "จ่ายยอด";
            this.จายยอดToolStripMenuItem.Click += new System.EventHandler(this.จายยอดToolStripMenuItem_Click);
            // 
            // ToolStripMenuItem
            // 
            this.ToolStripMenuItem.Name = "ToolStripMenuItem";
            this.ToolStripMenuItem.Size = new System.Drawing.Size(222, 42);
            this.ToolStripMenuItem.Text = "ข้อมูลหุ้นสะสม";
            this.ToolStripMenuItem.Click += new System.EventHandler(this.shareinformation);
            // 
            // ขอมลกToolStripMenuItem
            // 
            this.ขอมลกToolStripMenuItem.Name = "ขอมลกToolStripMenuItem";
            this.ขอมลกToolStripMenuItem.Size = new System.Drawing.Size(222, 42);
            this.ขอมลกToolStripMenuItem.Text = "ข้อมูลกู้";
            this.ขอมลกToolStripMenuItem.Click += new System.EventHandler(this.loaninformation);
            // 
            // ขอมลสมาชกToolStripMenuItem
            // 
            this.ขอมลสมาชกToolStripMenuItem.Name = "ขอมลสมาชกToolStripMenuItem";
            this.ขอมลสมาชกToolStripMenuItem.Size = new System.Drawing.Size(222, 42);
            this.ขอมลสมาชกToolStripMenuItem.Text = "ข้อมูลสมาชิก";
            this.ขอมลสมาชกToolStripMenuItem.Click += new System.EventHandler(this.member);
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
            this.Loan.Size = new System.Drawing.Size(62, 41);
            this.Loan.Text = "กู้";
            // 
            // สมครกToolStripMenuItem
            // 
            this.สมครกToolStripMenuItem.Name = "สมครกToolStripMenuItem";
            this.สมครกToolStripMenuItem.Size = new System.Drawing.Size(224, 42);
            this.สมครกToolStripMenuItem.Text = "สมัครกู้";
            this.สมครกToolStripMenuItem.Click += new System.EventHandler(this.สมครกToolStripMenuItem_Click);
            // 
            // จายกToolStripMenuItem
            // 
            this.จายกToolStripMenuItem.Name = "จายกToolStripMenuItem";
            this.จายกToolStripMenuItem.Size = new System.Drawing.Size(224, 42);
            this.จายกToolStripMenuItem.Text = "จ่ายกู้";
            this.จายกToolStripMenuItem.Click += new System.EventHandler(this.จายกToolStripMenuItem_Click);
            // 
            // ดขอมลกToolStripMenuItem
            // 
            this.ดขอมลกToolStripMenuItem.Name = "ดขอมลกToolStripMenuItem";
            this.ดขอมลกToolStripMenuItem.Size = new System.Drawing.Size(224, 42);
            this.ดขอมลกToolStripMenuItem.Text = "ดูข้อมุลกู้";
            this.ดขอมลกToolStripMenuItem.Click += new System.EventHandler(this.ดขอมลกToolStripMenuItem_Click);
            // 
            // Cancel
            // 
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(224, 42);
            this.Cancel.Text = "ยกเลิกกู้";
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // ปดยอดToolStripMenuItem
            // 
            this.ปดยอดToolStripMenuItem.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ปดยอดToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("ปดยอดToolStripMenuItem.Image")));
            this.ปดยอดToolStripMenuItem.Name = "ปดยอดToolStripMenuItem";
            this.ปดยอดToolStripMenuItem.Size = new System.Drawing.Size(107, 41);
            this.ปดยอดToolStripMenuItem.Text = "ปิดยอด";
            // 
            // Member_ToolStripMenuItem
            // 
            this.Member_ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TMLCancelMembers});
            this.Member_ToolStripMenuItem.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Member_ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("Member_ToolStripMenuItem.Image")));
            this.Member_ToolStripMenuItem.Name = "Member_ToolStripMenuItem";
            this.Member_ToolStripMenuItem.Size = new System.Drawing.Size(149, 41);
            this.Member_ToolStripMenuItem.Text = "สมัครสมาชิก";
            this.Member_ToolStripMenuItem.Click += new System.EventHandler(this.สมครสมาชกToolStripMenuItem_Click);
            // 
            // TMLCancelMembers
            // 
            this.TMLCancelMembers.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ReportCancelMember});
            this.TMLCancelMembers.Image = ((System.Drawing.Image)(resources.GetObject("TMLCancelMembers.Image")));
            this.TMLCancelMembers.Name = "TMLCancelMembers";
            this.TMLCancelMembers.Size = new System.Drawing.Size(213, 42);
            this.TMLCancelMembers.Text = "ยกเลิกสมาชิก";
            this.TMLCancelMembers.Click += new System.EventHandler(this.TMLCancelMembers_Click);
            // 
            // ReportCancelMember
            // 
            this.ReportCancelMember.Name = "ReportCancelMember";
            this.ReportCancelMember.Size = new System.Drawing.Size(277, 42);
            this.ReportCancelMember.Text = "รายชื่อผู้ยกเลิกสมาชิก";
            this.ReportCancelMember.Click += new System.EventHandler(this.ReportCancelMember_Click);
            // 
            // SentingToolStripMenuItem
            // 
            this.SentingToolStripMenuItem.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SentingToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("SentingToolStripMenuItem.Image")));
            this.SentingToolStripMenuItem.Name = "SentingToolStripMenuItem";
            this.SentingToolStripMenuItem.Size = new System.Drawing.Size(92, 41);
            this.SentingToolStripMenuItem.Text = "ตั้งค่า";
            this.SentingToolStripMenuItem.Click += new System.EventHandler(this.SettingToolStripMenuItem_Click);
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1176, 791);
            this.Controls.Add(this.menuStrip1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
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
        private System.Windows.Forms.ToolStripMenuItem จายยอดToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Loan;
        private System.Windows.Forms.ToolStripMenuItem ปดยอดToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ขอมลกToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ขอมลสมาชกToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Member_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SentingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TMLCancelMembers;
        private System.Windows.Forms.ToolStripMenuItem ReportCancelMember;
        private System.Windows.Forms.ToolStripMenuItem จายกToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ดขอมลกToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Cancel;
        private System.Windows.Forms.ToolStripMenuItem สมครกToolStripMenuItem;
    }
}