
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
            this.Menu_Home = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_pay = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_Loan = new System.Windows.Forms.ToolStripMenuItem();
            this.Menubar_MerberLoan = new System.Windows.Forms.ToolStripMenuItem();
            this.Menubar_payLoan = new System.Windows.Forms.ToolStripMenuItem();
            this.Menubar_infoLoan = new System.Windows.Forms.ToolStripMenuItem();
            this.Cancel = new System.Windows.Forms.ToolStripMenuItem();
            this.AmountOff = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_Membership = new System.Windows.Forms.ToolStripMenuItem();
            this.TMLCancelMembers = new System.Windows.Forms.ToolStripMenuItem();
            this.ReportCancelMember = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_setring = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Menu_Home,
            this.Menu_pay,
            this.Menu_Loan,
            this.AmountOff,
            this.Menu_Membership,
            this.Menu_setring});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(882, 38);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemAdded += new System.Windows.Forms.ToolStripItemEventHandler(this.menuStrip1_ItemAdded);
            // 
            // Menu_Home
            // 
            this.Menu_Home.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Menu_Home.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Menu_Home.Image = ((System.Drawing.Image)(resources.GetObject("Menu_Home.Image")));
            this.Menu_Home.Name = "Menu_Home";
            this.Menu_Home.Size = new System.Drawing.Size(117, 41);
            this.Menu_Home.Text = "หน้าเเรก";
            this.Menu_Home.Click += new System.EventHandler(this.Menu_Home_Click);
            // 
            // Menu_pay
            // 
            this.Menu_pay.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Menu_pay.Image = ((System.Drawing.Image)(resources.GetObject("Menu_pay.Image")));
            this.Menu_pay.Name = "Menu_pay";
            this.Menu_pay.Size = new System.Drawing.Size(152, 41);
            this.Menu_pay.Text = "จ่าย สะสม/กู้";
            this.Menu_pay.Click += new System.EventHandler(this.Menu_pay_Click);
            // 
            // Menu_Loan
            // 
            this.Menu_Loan.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Menubar_MerberLoan,
            this.Menubar_payLoan,
            this.Menubar_infoLoan,
            this.Cancel});
            this.Menu_Loan.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Menu_Loan.Image = ((System.Drawing.Image)(resources.GetObject("Menu_Loan.Image")));
            this.Menu_Loan.Name = "Menu_Loan";
            this.Menu_Loan.Size = new System.Drawing.Size(62, 41);
            this.Menu_Loan.Text = "กู้";
            // 
            // Menubar_MerberLoan
            // 
            this.Menubar_MerberLoan.Name = "Menubar_MerberLoan";
            this.Menubar_MerberLoan.Size = new System.Drawing.Size(176, 42);
            this.Menubar_MerberLoan.Text = "สมัครกู้";
            this.Menubar_MerberLoan.Click += new System.EventHandler(this.Menubar_MerberLoan_Click);
            // 
            // Menubar_payLoan
            // 
            this.Menubar_payLoan.Name = "Menubar_payLoan";
            this.Menubar_payLoan.Size = new System.Drawing.Size(176, 42);
            this.Menubar_payLoan.Text = "จ่ายกู้";
            this.Menubar_payLoan.Click += new System.EventHandler(this.Menubar_Click);
            // 
            // Menubar_infoLoan
            // 
            this.Menubar_infoLoan.Name = "Menubar_infoLoan";
            this.Menubar_infoLoan.Size = new System.Drawing.Size(176, 42);
            this.Menubar_infoLoan.Text = "ดูข้อมุลกู้";
            this.Menubar_infoLoan.Click += new System.EventHandler(this.Menubar_infoLoan_Click);
            // 
            // Cancel
            // 
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(176, 42);
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
            // Menu_Membership
            // 
            this.Menu_Membership.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TMLCancelMembers});
            this.Menu_Membership.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Menu_Membership.Image = ((System.Drawing.Image)(resources.GetObject("Menu_Membership.Image")));
            this.Menu_Membership.Name = "Menu_Membership";
            this.Menu_Membership.Size = new System.Drawing.Size(149, 41);
            this.Menu_Membership.Text = "สมัครสมาชิก";
            this.Menu_Membership.Click += new System.EventHandler(this.Menu_Membership_Click);
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
            // Menu_setring
            // 
            this.Menu_setring.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Menu_setring.Image = ((System.Drawing.Image)(resources.GetObject("Menu_setring.Image")));
            this.Menu_setring.Name = "Menu_setring";
            this.Menu_setring.Size = new System.Drawing.Size(92, 41);
            this.Menu_setring.Text = "ตั้งค่า";
            this.Menu_setring.Click += new System.EventHandler(this.Menu_setring_Click);
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
        private System.Windows.Forms.ToolStripMenuItem Menu_Home;
        private System.Windows.Forms.ToolStripMenuItem Menu_pay;
        private System.Windows.Forms.ToolStripMenuItem Menu_Loan;
        private System.Windows.Forms.ToolStripMenuItem AmountOff;
        private System.Windows.Forms.ToolStripMenuItem Menu_Membership;
        private System.Windows.Forms.ToolStripMenuItem Menu_setring;
        private System.Windows.Forms.ToolStripMenuItem TMLCancelMembers;
        private System.Windows.Forms.ToolStripMenuItem ReportCancelMember;
        private System.Windows.Forms.ToolStripMenuItem Menubar_payLoan;
        private System.Windows.Forms.ToolStripMenuItem Menubar_infoLoan;
        private System.Windows.Forms.ToolStripMenuItem Cancel;
        private System.Windows.Forms.ToolStripMenuItem Menubar_MerberLoan;
    }
}