
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.Menu_AddMembers = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_CancelMembers = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_Home = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_Pay = new System.Windows.Forms.ToolStripMenuItem();
            this.Manu_pay = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_CanCelBill = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_Loan = new System.Windows.Forms.ToolStripMenuItem();
            this.Menubar_MerberLoan = new System.Windows.Forms.ToolStripMenuItem();
            this.Menubar_payLoan = new System.Windows.Forms.ToolStripMenuItem();
            this.Menubar_infoLoan = new System.Windows.Forms.ToolStripMenuItem();
            this.Cancel = new System.Windows.Forms.ToolStripMenuItem();
            this.AmountOff = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_Membership = new System.Windows.Forms.ToolStripMenuItem();
            this.สมครสมาชกToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ยกเลกสมาชกToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.memberInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu_RePornt = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuReportExpenses = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuOneIncome = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuAllIncome = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuReportIncome = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuOneEpenses = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuAllEpenses = new System.Windows.Forms.ToolStripMenuItem();
            this.ประวตการยกเลกบลลToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ReprotDividend = new System.Windows.Forms.ToolStripMenuItem();
            this.Dividend = new System.Windows.Forms.ToolStripMenuItem();
            this.DevidendYear = new System.Windows.Forms.ToolStripMenuItem();
            this.CancelDevind = new System.Windows.Forms.ToolStripMenuItem();
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
            this.Menu_Pay,
            this.Menu_Loan,
            this.AmountOff,
            this.Menu_Membership,
            this.Menu_RePornt,
            this.Dividend,
            this.Menu_setring});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(882, 39);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemAdded += new System.Windows.Forms.ToolStripItemEventHandler(this.menuStrip1_ItemAdded);
            // 
            // Menu_AddMembers
            // 
            this.Menu_AddMembers.Name = "Menu_AddMembers";
            this.Menu_AddMembers.Size = new System.Drawing.Size(246, 42);
            this.Menu_AddMembers.Text = "5.1 สมัครสมาชิก";
            // 
            // Menu_CancelMembers
            // 
            this.Menu_CancelMembers.Name = "Menu_CancelMembers";
            this.Menu_CancelMembers.Size = new System.Drawing.Size(246, 42);
            this.Menu_CancelMembers.Text = "5.2 ยกเลิกสมาชิก";
            // 
            // Menu_Home
            // 
            this.Menu_Home.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Menu_Home.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Menu_Home.Image = global::BankTeacher.Properties.Resources._64x64_home;
            this.Menu_Home.Name = "Menu_Home";
            this.Menu_Home.Size = new System.Drawing.Size(112, 35);
            this.Menu_Home.Text = "1 หน้าเเรก";
            this.Menu_Home.Click += new System.EventHandler(this.Menu_Home_Click);
            // 
            // Menu_Pay
            // 
            this.Menu_Pay.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Manu_pay,
            this.Menu_CanCelBill});
            this.Menu_Pay.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Menu_Pay.Image = global::BankTeacher.Properties.Resources._64x64_wallet;
            this.Menu_Pay.Name = "Menu_Pay";
            this.Menu_Pay.Size = new System.Drawing.Size(83, 35);
            this.Menu_Pay.Text = "2 จ่าย";
            // 
            // Manu_pay
            // 
            this.Manu_pay.Name = "Manu_pay";
            this.Manu_pay.Size = new System.Drawing.Size(190, 34);
            this.Manu_pay.Text = "2.1 จ่าย สะสม/กู้";
            this.Manu_pay.Click += new System.EventHandler(this.Manu_pay_Click);
            // 
            // Menu_CanCelBill
            // 
            this.Menu_CanCelBill.Name = "Menu_CanCelBill";
            this.Menu_CanCelBill.Size = new System.Drawing.Size(190, 34);
            this.Menu_CanCelBill.Text = "2.2 ยกเลิกบิลล์";
            this.Menu_CanCelBill.Click += new System.EventHandler(this.Menu_CanCelBill_Click);
            // 
            // Menu_Loan
            // 
            this.Menu_Loan.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Menubar_MerberLoan,
            this.Menubar_payLoan,
            this.Menubar_infoLoan,
            this.Cancel});
            this.Menu_Loan.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Menu_Loan.Image = global::BankTeacher.Properties.Resources._64x64_money_bag;
            this.Menu_Loan.Name = "Menu_Loan";
            this.Menu_Loan.Size = new System.Drawing.Size(67, 35);
            this.Menu_Loan.Text = "3 กู้";
            // 
            // Menubar_MerberLoan
            // 
            this.Menubar_MerberLoan.Name = "Menubar_MerberLoan";
            this.Menubar_MerberLoan.Size = new System.Drawing.Size(165, 34);
            this.Menubar_MerberLoan.Text = "3. 1สมัครกู้";
            this.Menubar_MerberLoan.Click += new System.EventHandler(this.Menubar_MerberLoan_Click);
            // 
            // Menubar_payLoan
            // 
            this.Menubar_payLoan.Name = "Menubar_payLoan";
            this.Menubar_payLoan.Size = new System.Drawing.Size(165, 34);
            this.Menubar_payLoan.Text = "3.2 จ่ายกู้";
            this.Menubar_payLoan.Click += new System.EventHandler(this.Menubar_Click);
            // 
            // Menubar_infoLoan
            // 
            this.Menubar_infoLoan.Name = "Menubar_infoLoan";
            this.Menubar_infoLoan.Size = new System.Drawing.Size(165, 34);
            this.Menubar_infoLoan.Text = "3.3 ดูข้อมุลกู้";
            this.Menubar_infoLoan.Click += new System.EventHandler(this.Menubar_infoLoan_Click);
            // 
            // Cancel
            // 
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(165, 34);
            this.Cancel.Text = "3.4 ยกเลิกกู้";
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // AmountOff
            // 
            this.AmountOff.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AmountOff.Image = global::BankTeacher.Properties.Resources._64x64_money_bag__1_;
            this.AmountOff.Name = "AmountOff";
            this.AmountOff.Size = new System.Drawing.Size(162, 35);
            this.AmountOff.Text = "4 ถอนเงินหุ้นสะสม";
            this.AmountOff.Click += new System.EventHandler(this.AmountOff_Click);
            // 
            // Menu_Membership
            // 
            this.Menu_Membership.BackgroundImage = global::BankTeacher.Properties.Resources._64x64_user;
            this.Menu_Membership.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.สมครสมาชกToolStripMenuItem,
            this.ยกเลกสมาชกToolStripMenuItem,
            this.memberInfoToolStripMenuItem});
            this.Menu_Membership.Font = new System.Drawing.Font("TH Sarabun New", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Menu_Membership.Image = global::BankTeacher.Properties.Resources._64x64_user;
            this.Menu_Membership.Name = "Menu_Membership";
            this.Menu_Membership.Size = new System.Drawing.Size(102, 35);
            this.Menu_Membership.Tag = "";
            this.Menu_Membership.Text = "5 สมาชิก";
            // 
            // สมครสมาชกToolStripMenuItem
            // 
            this.สมครสมาชกToolStripMenuItem.Name = "สมครสมาชกToolStripMenuItem";
            this.สมครสมาชกToolStripMenuItem.Size = new System.Drawing.Size(195, 34);
            this.สมครสมาชกToolStripMenuItem.Text = "5.1 สมัครสมาชิก";
            this.สมครสมาชกToolStripMenuItem.Click += new System.EventHandler(this.สมครสมาชกToolStripMenuItem_Click);
            // 
            // ยกเลกสมาชกToolStripMenuItem
            // 
            this.ยกเลกสมาชกToolStripMenuItem.Name = "ยกเลกสมาชกToolStripMenuItem";
            this.ยกเลกสมาชกToolStripMenuItem.Size = new System.Drawing.Size(195, 34);
            this.ยกเลกสมาชกToolStripMenuItem.Text = "5.2 ยกเลิกสมาชิก";
            this.ยกเลกสมาชกToolStripMenuItem.Click += new System.EventHandler(this.ยกเลกสมาชกToolStripMenuItem_Click);
            // 
            // memberInfoToolStripMenuItem
            // 
            this.memberInfoToolStripMenuItem.Name = "memberInfoToolStripMenuItem";
            this.memberInfoToolStripMenuItem.Size = new System.Drawing.Size(195, 34);
            this.memberInfoToolStripMenuItem.Text = "5.3 ข้อมูลสมาชิก";
            this.memberInfoToolStripMenuItem.Click += new System.EventHandler(this.memberInfoToolStripMenuItem_Click);
            // 
            // Menu_RePornt
            // 
            this.Menu_RePornt.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuReportExpenses,
            this.MenuReportIncome,
            this.ประวตการยกเลกบลลToolStripMenuItem,
            this.ReprotDividend});
            this.Menu_RePornt.Font = new System.Drawing.Font("TH Sarabun New", 17.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Menu_RePornt.Image = global::BankTeacher.Properties.Resources.Report_2;
            this.Menu_RePornt.Name = "Menu_RePornt";
            this.Menu_RePornt.Size = new System.Drawing.Size(110, 35);
            this.Menu_RePornt.Text = "6 รายงาน";
            // 
            // MenuReportExpenses
            // 
            this.MenuReportExpenses.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuOneIncome,
            this.MenuAllIncome});
            this.MenuReportExpenses.Name = "MenuReportExpenses";
            this.MenuReportExpenses.Size = new System.Drawing.Size(253, 36);
            this.MenuReportExpenses.Text = "6.1 รายงานรายรับ";
            // 
            // MenuOneIncome
            // 
            this.MenuOneIncome.Name = "MenuOneIncome";
            this.MenuOneIncome.Size = new System.Drawing.Size(174, 36);
            this.MenuOneIncome.Text = "6.1.1 รายคน";
            this.MenuOneIncome.Click += new System.EventHandler(this.MenuOneIncome_Click);
            // 
            // MenuAllIncome
            // 
            this.MenuAllIncome.Name = "MenuAllIncome";
            this.MenuAllIncome.Size = new System.Drawing.Size(174, 36);
            this.MenuAllIncome.Text = "6.1.2 ทั้งหมด";
            this.MenuAllIncome.Click += new System.EventHandler(this.MenuAllIncome_Click);
            // 
            // MenuReportIncome
            // 
            this.MenuReportIncome.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuOneEpenses,
            this.MenuAllEpenses});
            this.MenuReportIncome.Name = "MenuReportIncome";
            this.MenuReportIncome.Size = new System.Drawing.Size(253, 36);
            this.MenuReportIncome.Text = "6.2 รายงานรายจ่าย";
            // 
            // MenuOneEpenses
            // 
            this.MenuOneEpenses.Name = "MenuOneEpenses";
            this.MenuOneEpenses.Size = new System.Drawing.Size(174, 36);
            this.MenuOneEpenses.Text = "6.2.1 รายคน";
            this.MenuOneEpenses.Click += new System.EventHandler(this.MenuOneEpenses_Click);
            // 
            // MenuAllEpenses
            // 
            this.MenuAllEpenses.Name = "MenuAllEpenses";
            this.MenuAllEpenses.Size = new System.Drawing.Size(174, 36);
            this.MenuAllEpenses.Text = "6.2.2 ทั้งหมด";
            this.MenuAllEpenses.Click += new System.EventHandler(this.MenuAllEpenses_Click);
            // 
            // ประวตการยกเลกบลลToolStripMenuItem
            // 
            this.ประวตการยกเลกบลลToolStripMenuItem.Name = "ประวตการยกเลกบลลToolStripMenuItem";
            this.ประวตการยกเลกบลลToolStripMenuItem.Size = new System.Drawing.Size(253, 36);
            this.ประวตการยกเลกบลลToolStripMenuItem.Text = "6.3 ประวัติการยกเลิกบิลล์";
            this.ประวตการยกเลกบลลToolStripMenuItem.Click += new System.EventHandler(this.ประวตการยกเลกบลลToolStripMenuItem_Click);
            // 
            // ReprotDividend
            // 
            this.ReprotDividend.Name = "ReprotDividend";
            this.ReprotDividend.Size = new System.Drawing.Size(253, 36);
            this.ReprotDividend.Text = "6.4 รายงานปันผล";
            this.ReprotDividend.Click += new System.EventHandler(this.ReprotDividend_Click);
            // 
            // Dividend
            // 
            this.Dividend.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DevidendYear,
            this.CancelDevind});
            this.Dividend.Font = new System.Drawing.Font("TH Sarabun New", 17.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Dividend.Image = global::BankTeacher.Properties.Resources.dividends;
            this.Dividend.Name = "Dividend";
            this.Dividend.Size = new System.Drawing.Size(97, 35);
            this.Dividend.Text = "7 ปันผล";
            // 
            // DevidendYear
            // 
            this.DevidendYear.Name = "DevidendYear";
            this.DevidendYear.Size = new System.Drawing.Size(191, 36);
            this.DevidendYear.Text = "7.1 ปันผลรายปี";
            this.DevidendYear.Click += new System.EventHandler(this.DevidendYear_Click);
            // 
            // CancelDevind
            // 
            this.CancelDevind.Name = "CancelDevind";
            this.CancelDevind.Size = new System.Drawing.Size(191, 36);
            this.CancelDevind.Text = "7.2 ยกเลิกปันผล";
            this.CancelDevind.Click += new System.EventHandler(this.CancelDevind_Click);
            // 
            // Menu_setring
            // 
            this.Menu_setring.Font = new System.Drawing.Font("TH Sarabun New", 17.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Menu_setring.Image = global::BankTeacher.Properties.Resources._64x64_gear;
            this.Menu_setring.Name = "Menu_setring";
            this.Menu_setring.Size = new System.Drawing.Size(78, 35);
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
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Menu";
            this.Text = "หน้าเเรก";
            this.Load += new System.EventHandler(this.Menu_Load_1);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.ToolStripMenuItem Menu_Home;
        private System.Windows.Forms.ToolStripMenuItem Menu_Loan;
        private System.Windows.Forms.ToolStripMenuItem AmountOff;
        private System.Windows.Forms.ToolStripMenuItem Menu_Membership;
        private System.Windows.Forms.ToolStripMenuItem Menu_setring;
        private System.Windows.Forms.ToolStripMenuItem Menubar_payLoan;
        private System.Windows.Forms.ToolStripMenuItem Menubar_infoLoan;
        private System.Windows.Forms.ToolStripMenuItem Cancel;
        private System.Windows.Forms.ToolStripMenuItem Menubar_MerberLoan;
        private System.Windows.Forms.ToolStripMenuItem Menu_RePornt;
        public System.Windows.Forms.ToolStripMenuItem Menu_Pay;
        private System.Windows.Forms.ToolStripMenuItem Manu_pay;
        private System.Windows.Forms.ToolStripMenuItem Menu_AddMembers;
        private System.Windows.Forms.ToolStripMenuItem Menu_CancelMembers;
        private System.Windows.Forms.ToolStripMenuItem Menu_CanCelBill;
        private System.Windows.Forms.ToolStripMenuItem สมครสมาชกToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ยกเลกสมาชกToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MenuReportExpenses;
        private System.Windows.Forms.ToolStripMenuItem MenuReportIncome;
        private System.Windows.Forms.ToolStripMenuItem MenuOneIncome;
        private System.Windows.Forms.ToolStripMenuItem MenuAllIncome;
        private System.Windows.Forms.ToolStripMenuItem MenuOneEpenses;
        private System.Windows.Forms.ToolStripMenuItem MenuAllEpenses;
        private System.Windows.Forms.ToolStripMenuItem ประวตการยกเลกบลลToolStripMenuItem;
        public System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem memberInfoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ReprotDividend;
        private System.Windows.Forms.ToolStripMenuItem Dividend;
        private System.Windows.Forms.ToolStripMenuItem DevidendYear;
        private System.Windows.Forms.ToolStripMenuItem CancelDevind;
    }
}