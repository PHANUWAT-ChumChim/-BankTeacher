using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace example.GOODS
{
    public partial class Menu : Form
    {
        public static int startAmountMin;
        public static int startAmountMax;
        public static int DateAmountChange;
        public static string FontSize;
        public static int MinLoan;
        public static String[] Date;
        public static String Monthname;
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
             "SELECT DateAmountChange , StartAmountMin , StartAmountMax,MinLoan \r\n" +
             "FROM EmployeeBank.dbo.tblSettingAmount;"
          ,
             //[1]Select Date InWput :  -
             "SELECT CAST(CURRENT_TIMESTAMP as DATE);"
             ,
             
         };
        public Menu()
        {
            InitializeComponent();
            Date = Class.SQLConnection.InputSQLMSSQL(SQLDefault[1]).Rows[0][0].ToString().Split('-');
            example.GOODS.Menu.Monthname = Month[Convert.ToInt32(example.GOODS.Menu.Date[1]) - 1];

            Class.UserInfo.SetTeacherInformation("T43005", "Manit Hodkuntod", "1");

            dt = Class.SQLConnection.InputSQLMSSQL(SQLDefault[0]);
            DateAmountChange = Convert.ToInt32(dt.Rows[0][0]);
            startAmountMin = Convert.ToInt32(dt.Rows[0][1]);
            startAmountMax = Convert.ToInt32(dt.Rows[0][2]);
            MinLoan = Convert.ToInt32(dt.Rows[0][3]);
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
            F.WindowState = FormWindowState.Maximized;
            F.Show();
        }
        public void shareinformation(object sender, EventArgs e)
        {
            pay Mn = new pay(1);
            CloseFrom(Mn);
        }

        private void จายยอดToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pay Mn = new pay(0);
            CloseFrom(Mn);
        }

        private void menuStrip1_ItemAdded(object sender, ToolStripItemEventArgs e)
        {
            e.Item.Visible = false;
        }

        private void หนาเเรกToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Home Hm = new Home();
            CloseFrom(Hm);
        }
        private void loaninformation(object sender, EventArgs e)
        {
            pay Mn = new pay(2);
            CloseFrom(Mn);
        }

        private void member(object sender, EventArgs e)
        {
            pay Mn = new pay(3);
            CloseFrom(Mn);
        }

        private void สมครสมาชกToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bank.MemberShip Ms = new Bank.MemberShip();
            CloseFrom(Ms);
        }
        private void Menu_Load_1(object sender, EventArgs e)
        {
            Home Hm = new Home();
            CloseFrom(Hm);
        }

        private void SettingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bank.Setting St = new Bank.Setting();
            St.Show();
        }
        private void TMLCancelMembers_Click(object sender, EventArgs e)
        {
            Bank.CancelMembership Cm = new Bank.CancelMembership();
            CloseFrom(Cm);
        }

        private void ReportCancelMember_Click(object sender, EventArgs e)
        {
            Bank.ReportCancelMember Rc = new Bank.ReportCancelMember();
            CloseFrom(Rc);
        }

        private void pay_Click(object sender, EventArgs e)
        {

        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            example.Bank.Loan.CancelLoan Cl = new Bank.Loan.CancelLoan();
            CloseFrom(Cl);
        }

        private void จายกToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bank.Loan.PayLoan Pl = new Bank.Loan.PayLoan();
            CloseFrom(Pl);
        }

        private void สมครกToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bank.Loan.loan Ln = new Bank.Loan.loan();
            CloseFrom(Ln);
        }

        private void ดขอมลกToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bank.Loan.InfoLoan If = new Bank.Loan.InfoLoan();
            CloseFrom(If);
        }

        private void AmountOff_Click(object sender, EventArgs e)
        {
            Bank.AmountOff Af = new Bank.AmountOff();
            CloseFrom(Af);  
        }
    }
}
