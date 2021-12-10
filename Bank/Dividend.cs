using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankTeacher.Bank
{
    public partial class Dividend : Form
    {
        public Dividend()
        {
            InitializeComponent();
        }

        /// <summary> 
        /// SQLDefault 
        /// <para>[0] Insert Dividend INPUT: {Year} {TeacherAddbyNo} </para> 
        /// <para>[1] Table[1]Select StartYear and Table[2]Select EndYear INPUT: </para>
        /// </summary> 
        private String[] SQLDefault = new String[]
         { 
           //[0] Insert Dividend INPUT: {Year}
           "DECLARE @Interest int \r\n " +
          "DECLARE @AmountShare float \r\n " +
          "DECLARE @AVGDivident float \r\n " +
          "DECLARE @PerShare int \r\n " +
          "DECLARE @DividendNo int \r\n " +
          " \r\n " +
          "--PerShare \r\n " +
          "SELECT @PerShare = a.PerShare \r\n " +
          "FROM EmployeeBank.dbo.tblSettingAmount as a ; \r\n " +
          " \r\n " +
          "--AmountShare \r\n " +
          "SELECT @AmountShare = (SUM(b.SavingAmount ) / @PerShare ) \r\n " +
          "FROM EmployeeBank.dbo.tblMember as a \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblShare as b on a.TeacherNo = b.TeacherNo; \r\n " +
          " \r\n " +
          "--Interest \r\n " +
          "SELECT @Interest = SUM(ISNULL(CASE \r\n " +
          "    WHEN a.YearPay = {Year} - 1 and a.MonthPay + a.PayNo - 1 > 12  THEN (ROUND((CAST(a.InterestRate as float) / 100) * a.LoanAmount , 0) / a.PayNo  * (a.PayNo - (12 - a.MonthPay + 1))) \r\n " +
          "    WHEN a.YearPay = {Year} THEN (ROUND((CAST(a.InterestRate as float) / 100) * a.LoanAmount , 0) / a.PayNo  * (12 - a.MonthPay + 1)) \r\n " +
          "END , 0)) \r\n " +
          "FROM EmployeeBank.dbo.tblLoan as a \r\n " +
          "WHERE a.LoanStatusNo = 2 and a.YearPay = {Year} or a.YearPay = {Year} - 1; \r\n " +
          " \r\n " +
          "SELECT @Interest = @Interest + RemainInterestLastYear \r\n " +
          "FROM EmployeeBank.dbo.tblDividend \r\n " +
          "WHERE Cancel = 1 and Year = {Year} - 1 \r\n " +
          " \r\n " +
          "SET @AVGDivident = @Interest/@AmountShare; \r\n " +
          " \r\n " +
          "INSERT EmployeeBank.dbo.tblDividend (Interest,AverageDividend,Year,Cancel) \r\n " +
          "VALUES (@Interest, ROUND(@AVGDivident,2,1),{Year},1); \r\n " +
          " \r\n " +
          "SET @DividendNo = SCOPE_IDENTITY(); \r\n " +
          " \r\n " +
          "INSERT INTO EmployeeBank.dbo.tblDividendDetail (DividendNo , TeacherNo , SavingAmount , DividendAmount , TeacherAddby) \r\n " +
          "SELECT @DividendNo,a.TeacherNo  , b.SavingAmount , ROUND(ROUND((b.SavingAmount/@PerShare), 2 ,1) * @AVGDivident ,2 , 1) as Dividend , {TeacherAddbyNo} \r\n " +
          "FROM EmployeeBank.dbo.tblMember as a  \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblShare as b on a.TeacherNo = b.TeacherNo \r\n " +
          "WHERE a.MemberStatusNo = 1; \r\n " +
          " \r\n " +
          "SELECT @Interest = @Interest - SUM(a.DividendAmount) \r\n " +
          "FROM EmployeeBank.dbo.tblDividendDetail as a \r\n " +
          "WHERE a.DividendNo = @DividendNo \r\n " +
          " \r\n " +
          "UPDATE EmployeeBank.dbo.tblDividend  \r\n " +
          "SET RemainInterestLastYear = @Interest \r\n " +
          "WHERE DividendNo = @DividendNo; \r\n " +
          " \r\n " +
          "UPDATE EmployeeBank.dbo.tblShare \r\n " +
          "SET SavingAmount = SavingAmount + (SELECT b.DividendAmount \r\n " +
          "	FROM EmployeeBank.dbo.tblDividend as a \r\n " +
          "	LEFT JOIN EmployeeBank.dbo.tblDividendDetail as b on a.DividendNo = b.DividendNo \r\n " +
          "	WHERE a.DividendNo = @DividendNo and a.Cancel = 1 and EmployeeBank.dbo.tblShare.TeacherNo = b.TeacherNo) \r\n " +
          "WHERE EmployeeBank.dbo.tblShare.TeacherNo IN (SELECT b.TeacherNo \r\n " +
          "	FROM EmployeeBank.dbo.tblDividend as a \r\n " +
          "	LEFT JOIN EmployeeBank.dbo.tblDividendDetail as b on a.DividendNo = b.DividendNo \r\n " +
          "	WHERE a.DividendNo = @DividendNo and a.Cancel = 1) \r\n " +
          ""
           ,


           //[1] Table[1]Select StartYear and Table[2]Select EndYear INPUT:
           "DECLARE @Getnull int; \r\n " +
          " \r\n " +
          "SELECT TOP 1 @Getnull = ISNULL(MAX(a.Year) + 1 , 0) \r\n " +
          "FROM EmployeeBank.dbo.tblDividend as a  \r\n " +
          "WHERE a.Cancel = 1 ; \r\n " +
          " \r\n " +
          "IF (@Getnull = 0) \r\n " +
          "BEGIN \r\n " +
          "	SELECT TOP 1 MIN(b.Year)  \r\n " +
          "	FROM EmployeeBank.dbo.tblBill as a \r\n " +
          "	LEFT JOIN EmployeeBank.dbo.tblBillDetail as b on a.BillNo = b.BillNo \r\n " +
          "	WHERE a.Cancel = 1 and b.TypeNo = 2; \r\n " +
          "END \r\n " +
          "ELSE \r\n " +
          "BEGIN \r\n " +
          "	SELECT TOP 1 @Getnull = ISNULL(MAX(a.Year) + 1 , 0) \r\n " +
          "	FROM EmployeeBank.dbo.tblDividend as a  \r\n " +
          "	WHERE a.Cancel = 1 ; \r\n " +
          "END \r\n " +
          " \r\n " +
          "SELECT TOP 1 MAX(b.Year)  \r\n " +
          "FROM EmployeeBank.dbo.tblBill as a \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblBillDetail as b on a.BillNo = b.BillNo \r\n " +
          "WHERE a.Cancel = 1 and b.TypeNo = 2;"
           ,



         };

        private void Dividend_Load(object sender, EventArgs e)
        {
            DataSet dsStartYear = Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[1]);
            for (int x = Convert.ToInt32(dsStartYear.Tables[0].Rows[0][0].ToString()); x <= Convert.ToInt32(dsStartYear.Tables[1].Rows[0][0].ToString()); x++)
            {
                CBYearDividend.Items.Add(x);
            }
        }

        private void BSaveDividend_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("ยืนยันที่จะบันทึกหรือไม่", "แจ้งเตือน", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes && CBYearDividend.SelectedIndex != -1)
            {
                try
                {
                    Class.SQLConnection.InputSQLMSSQL(SQLDefault[0]
                    .Replace("{Year}", CBYearDividend.Items.ToString())
                    .Replace("{TeacherAddbyNo}",Class.UserInfo.TeacherNo));
                    MessageBox.Show("บันทึกสำเร็จ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    CBYearDividend.Items.RemoveAt(CBYearDividend.SelectedIndex);
                    CBYearDividend.SelectedIndex = -1;
                    BSaveDividend.Enabled = false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"------------------------{ex}-------------------------");
                    MessageBox.Show("บันทึกล้มเหลว", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            else if (CBYearDividend.SelectedIndex == -1)
                MessageBox.Show("โปรดทำการเลือกปีที่จะปันผลก่อน", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void CBYearDividend_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CBYearDividend.SelectedIndex != -1)
                BSaveDividend.Enabled = true;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void BExitForm_Click(object sender, EventArgs e)
        {
            BankTeacher.Class.FromSettingMedtod.ReturntoHome(this);
        }

        private void Dividend_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                 BExitForm_Click(new object(), new EventArgs());
            }
        }
    }
}
