using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankTeacher.Bank
{
    public partial class Menu : Form
    {
        //============================== Time Servers SQL ========================================
        public static DataTable Date_Time_SQL_Now = Class.SQLConnection.InputSQLMSSQL("SELECT FORMAT (getdate(), 'dd-MM-yyyy') as date,YEAR(GETDATE()) as Year,MONTH(GETDATE()) as Month, CAST(GETDATE() as time) as Time;");
        public static int MenuEnabled;
        public static int startAmountMin;
        public static int startAmountMax;
        public static int perShare;
        public static int DateAmountChange;
        public static string FontSize;
        public static int MinLoan;
        public static Form Parent;
        public static String[] Date;
        public static String Monthname;
        public static int StatusActivateButtonExceltoSQL = 0;
        public static String[] Month = { "มกราคม", "กุมภาพันธ์", "มีนาคม", "เมษายน", "พฤษภาคม", "มิถุนายน", "กรกฎาคม", "สิงหาคม", "กันยายน", "ตุลาคม", "พฤศจิกายน", "ธันวาคม" };
        DataTable dt;
        /// <summary> 
        /// SQLDafault 
        /// <para>[0]Check Setting INPUT: - </para> 
        /// <para>[1]Select Date Input - </para>
        /// </summary> 
        private String[] SQLDefault = new String[]
        { 
             //[0]Check Setting INPUT: - 
             "SELECT DateAmountChange , StartAmountMin , StartAmountMax,MinLoan , PerShare ,StatusUploadExceltoSQL\r\n" +
             "FROM EmployeeBank.dbo.tblSettingAmount;"
          ,
             //[1]Select Date Input :  -
             "SELECT CAST(CURRENT_TIMESTAMP as DATE);"
          ,

        };
        Thread ReloadDB;
        public Menu()
        {
            InitializeComponent();
            Class.UserInfo.SetTeacherInformation("T43005", "ภาณุวัฒน์ ชุมฉิม", "1");
            ReloadDB = new Thread(Tricker);
            ReloadDB.Start();
        }
        private void Tricker()
        {
            Stopwatch time = new Stopwatch();
            time.Start();
            ThreadReloadData();
            //Relaod ข้อมูลทุก 1 นาที
            while (true)
            {
                if (time.ElapsedMilliseconds >= 60000)
                {
                    ThreadReloadData();
                    time.Reset();
                }
            }
        }
        void ThreadReloadData()
        {
            try
            {
                Date = Class.SQLConnection.InputSQLMSSQLV2ForThread(SQLDefault[1]).Rows[0][0].ToString().Split('-');
                BankTeacher.Bank.Menu.Monthname = Month[Convert.ToInt32(BankTeacher.Bank.Menu.Date[1]) - 1];
                dt = Class.SQLConnection.InputSQLMSSQLV2ForThread(SQLDefault[0]);
                DateAmountChange = Convert.ToInt32(dt.Rows[0][0]);
                startAmountMin = Convert.ToInt32(dt.Rows[0][1]);
                startAmountMax = Convert.ToInt32(dt.Rows[0][2]);
                MinLoan = Convert.ToInt32(dt.Rows[0][3]);
                perShare = Convert.ToInt32(dt.Rows[0][4]);
                StatusActivateButtonExceltoSQL = Convert.ToInt32(dt.Rows[0][5]);
            }
            catch
            {
                MessageBox.Show("การเชื่อมต่อมีปัญหา lost connection DB_KCC_Error", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Application.Exit();
            }
        }
        public void CloseFrom(Form F)
        {
            foreach (Form Menu in this.MdiChildren)
            {
                if (Menu.Name != F.Name)
                    Menu.Close();
                else
                    return;
            }
            F.MdiParent = this;
            Parent = this;
            F.WindowState = FormWindowState.Maximized;
            F.Show();
            if(this.AccessibilityObject.Name != "หน้าเเรก - [Home]")
            {
                if(this.AccessibilityObject.Name == "หน้าเเรก")
                {
                    BankTeacher.Bank.Home f = new BankTeacher.Bank.Home();
                    f.MdiParent = BankTeacher.Bank.Menu.Parent;
                    f.WindowState = FormWindowState.Maximized;
                    f.Show();

                }
                else
                {
                    this.menuStrip1.Visible = false;
                }
            }
          
        }
        public void menuStrip1_ItemAdded(object sender, ToolStripItemEventArgs e)
        {
            e.Item.Visible = false;
        }
        public void Menu_Home_Click(object sender, EventArgs e)
        {
            BankTeacher.Bank.Home Home = new BankTeacher.Bank.Home();
            CloseFrom(Home);
            menuStrip1.Visible = true;
        }
        private void Menu_Load_1(object sender, EventArgs e)
        {
            //BankTeacher.Bank.Home startHome = new BankTeacher.Bank.Home();
            //CloseFrom(startHome);
            menuStrip1.Visible = true;
        }
        private void Menu_setring_Click(object sender, EventArgs e)
        {
            Bank.Setting St = new Bank.Setting();
            CloseFrom(St);
        }
        private void Cancel_Click(object sender, EventArgs e)
        {
            BankTeacher.Bank.Loan.CancelLoan CancelLoan = new Bank.Loan.CancelLoan();
            CloseFrom(CancelLoan);
        }
        private void Menubar_Click(object sender, EventArgs e)
        {
            Bank.Loan.PayLoan PayLoan = new Bank.Loan.PayLoan();
            CloseFrom(PayLoan);
        }
        private void Menubar_MerberLoan_Click(object sender, EventArgs e)
        {
            Bank.Loan.loan loan = new Bank.Loan.loan();
            CloseFrom(loan);
        }
        private void Menubar_infoLoan_Click(object sender, EventArgs e)
        {
            Bank.Loan.InfoLoan InfoLoan = new Bank.Loan.InfoLoan();
            CloseFrom(InfoLoan);
        }
        private void AmountOff_Click(object sender, EventArgs e)
        {
            Bank.AmountOff AmountOff = new Bank.AmountOff();
            CloseFrom(AmountOff);
        }

        private void aaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ไม่ได้ใช้ ณ ตอนนี้
            Bank.Report report = new Bank.Report();
            CloseFrom(report);
        }

        private void Manu_pay_Click(object sender, EventArgs e)
        {
            Bank.Pay.pay pay = new Pay.pay();
            CloseFrom(pay);
        }

        private void สมครสมาชกToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bank.Add_Member.Member Merber = new Bank.Add_Member.Member();
            CloseFrom(Merber);
        }

        private void ยกเลกสมาชกToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bank.Add_Member.CancelMember cancelMembers = new Bank.Add_Member.CancelMember();
            CloseFrom(cancelMembers);
        }

        private void Menu_CanCelBill_Click(object sender, EventArgs e)
        {
            Bank.Pay.CancelBill cancelBill = new Pay.CancelBill();
            CloseFrom(cancelBill);
        }

        private void MenuOneIncome_Click(object sender, EventArgs e)
        {
            Bank.ReportIncome reportIncome = new Bank.ReportIncome();
            CloseFrom(reportIncome);
        }

        private void MenuAllIncome_Click(object sender, EventArgs e)
        {
            Bank.ReportIncomeAll reportIncomeall = new Bank.ReportIncomeAll();
            CloseFrom(reportIncomeall);
        }

        private void MenuOneEpenses_Click(object sender, EventArgs e)
        {
            Bank.ReportEpenses reportEpenses = new Bank.ReportEpenses();
            CloseFrom(reportEpenses);
        }

        private void MenuAllEpenses_Click(object sender, EventArgs e)
        {
            Bank.ReportEpensesAll reportEpensesall = new Bank.ReportEpensesAll();
            CloseFrom(reportEpensesall);
        }


        private void ประวตการยกเลกบลลToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bank.Pay.BillHistory Billcancelhistory = new Bank.Pay.BillHistory();
            CloseFrom(Billcancelhistory);
        }

        private void memberInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bank.Add_Member.infoMeber infoMeber = new Bank.Add_Member.infoMeber();
            CloseFrom(infoMeber);
        }

        private void ReprotDividend_Click(object sender, EventArgs e)
        {
            Bank.ReportDividend Reportdividend = new Bank.ReportDividend();
            CloseFrom(Reportdividend);
        }

        private void DevidendYear_Click(object sender, EventArgs e)
        {
            Bank.Dividend Dividend = new Bank.Dividend();
            CloseFrom(Dividend);
        }

        private void CancelDevind_Click(object sender, EventArgs e)
        {
            Bank.CancelDividend CancelDividend = new Bank.CancelDividend();
            CloseFrom(CancelDividend);
        }

        private void menuStrip1_VisibleChanged(object sender, EventArgs e)
        {
            if (menuStrip1.Visible == true)
            {
                BankTeacher.Bank.Home F = new BankTeacher.Bank.Home();
                CloseFrom(F);
            }
        }

        private void Menu_GuarantorPayLoan_Click(object sender, EventArgs e)
        {
            BankTeacher.Bank.Pay.GuarantorPayLoan GPL = new BankTeacher.Bank.Pay.GuarantorPayLoan();
            CloseFrom(GPL);
        }

        private void Menu_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
            Application.Exit();
        }
    }
}