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
        /// <para>[1] Table[0]Select StartYear and Table[1]Select EndYear INPUT: </para>
        /// <para>[2] Table[0]Get Name , a.SavingAmount , a.DividendAmount , a.Interest , a.RemainInterestLastYear , a.AverageDividend Table[1]Get InterestLastYear INPUT: {Year}</para>
        /// <para>[3] AfterDividentInfo INPUT: {Year}</para>
        /// <para>[4] Check Loan Paid are Complete in Year INPUT: {Year} </para>
        /// <para>[5] Get TeacherList Unpaid INPUT: {Year} </para>
        /// </summary> 
        private String[] SQLDefault = new String[]
         { 
           //[0] Insert Dividend INPUT: {Year} {TeacherAddbyNo}
           "DECLARE @Interest float  \r\n " +
          "  DECLARE @AmountShare float  \r\n " +
          "  DECLARE @AVGDivident float  \r\n " +
          "  DECLARE @PerShare int  \r\n " +
          "  DECLARE @DividendNo int  \r\n " +
          "  DECLARE @SumSavingAmount float;  \r\n " +
          "   \r\n " +
          " --SumSavingAmount  \r\n " +
          " SELECT @SumSavingAmount = SUM(SavingAmount)  \r\n " +
          " FROM EmployeeBank.dbo.tblShare  \r\n " +
          "     \r\n " +
          "  --PerShare   \r\n " +
          "  SELECT @PerShare = a.PerShare   \r\n " +
          "  FROM EmployeeBank.dbo.tblSettingAmount as a ;   \r\n " +
          "     \r\n " +
          "  --AmountShare   \r\n " +
          "  SELECT @AmountShare = (SUM(b.SavingAmount ) / @PerShare )   \r\n " +
          "  FROM EmployeeBank.dbo.tblMember as a   \r\n " +
          "  LEFT JOIN EmployeeBank.dbo.tblShare as b on a.TeacherNo = b.TeacherNo;   \r\n " +
          "     \r\n " +
          "  --Interest   \r\n " +
          "  SELECT @Interest =  SUM(ISNULL(CASE     \r\n " +
          "        WHEN a.YearPay = {Year} - 1 and a.MonthPay + a.PayNo - 1 > 12  THEN (ROUND((CAST(a.InterestRate as float) / 100) * a.LoanAmount , 0) / a.PayNo  * (a.PayNo - (12 - a.MonthPay + 1)))     \r\n " +
          "        WHEN a.YearPay = {Year} and a.MonthPay + a.PayNo - 1 > 12 THEN (ROUND((CAST(a.InterestRate as float) / 100) * a.LoanAmount , 0) / a.PayNo  * (12 - a.MonthPay + 1))     \r\n " +
          "  	  WHEN a.YearPay = {Year} and a.MonthPay + a.PayNo - 1 <= 12 THEN ROUND((CAST(a.InterestRate as float) / 100) * a.LoanAmount , 0)    \r\n " +
          "    END , 0))     \r\n " +
          "    FROM EmployeeBank.dbo.tblLoan as a     \r\n " +
          "    WHERE (a.LoanStatusNo = 2 or a.LoanStatusNo = 3) and a.YearPay = {Year} or (a.YearPay = {Year} - 1 and a.MonthPay + a.PayNo - 1 > 12) ;  \r\n " +
          "   \r\n " +
          "  SELECT @Interest = @Interest + RemainInterestLastYear  \r\n " +
          "  FROM EmployeeBank.dbo.tblDividend   \r\n " +
          "  WHERE Cancel = 1 and Year = {Year} - 1   \r\n " +
          "     \r\n " +
          "  SET @AVGDivident = @Interest/@AmountShare;   \r\n " +
          "     \r\n " +
          "  INSERT EmployeeBank.dbo.tblDividend (Interest,AverageDividend,Year,Cancel,CancelBy,TeacherAddby,DateAdd,DateCancel)   \r\n " +
          "  VALUES (@Interest, ROUND(@AVGDivident,2,1),{Year},1,'','{TeacherAddbyNo}',CURRENT_TIMESTAMP,'');   \r\n " +
          "     \r\n " +
          "  SET @DividendNo = SCOPE_IDENTITY();   \r\n " +
          "     \r\n " +
          "  INSERT INTO EmployeeBank.dbo.tblDividendDetail (DividendNo , TeacherNo , SavingAmount , DividendAmount)   \r\n " +
          "  SELECT @DividendNo,a.TeacherNo  , b.SavingAmount , ROUND((SavingAmount/@SumSavingAmount) * @Interest , 0,1) as Dividend   \r\n " +
          "  FROM EmployeeBank.dbo.tblMember as a  \r\n " +
          "  LEFT JOIN EmployeeBank.dbo.tblShare as b on a.TeacherNo = b.TeacherNo  \r\n " +
          "  WHERE a.MemberStatusNo = 1;  \r\n " +
          "     \r\n " +
          "  SELECT @Interest = @Interest - SUM(a.DividendAmount)  \r\n " +
          "  FROM EmployeeBank.dbo.tblDividendDetail as a   \r\n " +
          "  WHERE a.DividendNo = @DividendNo   \r\n " +
          "     \r\n " +
          "  UPDATE EmployeeBank.dbo.tblDividend    \r\n " +
          "  SET RemainInterestLastYear = ROUND(@Interest , 2 , 1) , DateCancel = NULL , CancelBy = NULL  \r\n " +
          "  WHERE DividendNo = @DividendNo;  \r\n " +
          " \r\n " +
          " UPDATE EmployeeBank.dbo.tblShare   \r\n " +
          "  SET SavingAmount = SavingAmount + (SELECT b.DividendAmount   \r\n " +
          "  	FROM EmployeeBank.dbo.tblDividend as a   \r\n " +
          "  	LEFT JOIN EmployeeBank.dbo.tblDividendDetail as b on a.DividendNo = b.DividendNo   \r\n " +
          "  	WHERE a.DividendNo = @DividendNo and a.Cancel = 1 and EmployeeBank.dbo.tblShare.TeacherNo = b.TeacherNo)   \r\n " +
          "  WHERE EmployeeBank.dbo.tblShare.TeacherNo IN (SELECT b.TeacherNo   \r\n " +
          "  	FROM EmployeeBank.dbo.tblDividend as a   \r\n " +
          "  	LEFT JOIN EmployeeBank.dbo.tblDividendDetail as b on a.DividendNo = b.DividendNo   \r\n " +
          "  	WHERE a.DividendNo = @DividendNo and a.Cancel = 1) \r\n " +
          " \r\n " +
          "UPDATE EmployeeBank.dbo.tblLoan  \r\n " +
          " SET LoanStatusNo = 4  \r\n " +
          " WHERE LoanStatusNo = 1;"
           ,

           //[1] Table[0]Select StartYear and Table[1]Select EndYear INPUT: -
           "DECLARE @Getnull int;  \r\n " +
          " DECLARE @LoanYear int;  \r\n " +
          "       \r\n " +
          " SELECT TOP 1 @Getnull = ISNULL(MAX(a.Year) + 1 , 0)    \r\n " +
          " FROM EmployeeBank.dbo.tblDividend as a     \r\n " +
          " WHERE a.Cancel = 1 ;  \r\n " +
          "   \r\n " +
          " SELECT TOP 1 @LoanYear = MIN(a.YearPay)   \r\n " +
          " FROM EmployeeBank.dbo.tblLoan as a   \r\n " +
          " WHERE a.LoanStatusNo = 2 or a.LoanStatusNo = 3   \r\n " +
          "       \r\n " +
          "   IF (@Getnull = 0)    \r\n " +
          "   BEGIN    \r\n " +
          "  	SELECT TOP 1 MIN(a.YearPay)   \r\n " +
          "  	FROM EmployeeBank.dbo.tblLoan as a   \r\n " +
          "  	WHERE a.LoanStatusNo = 2 or a.LoanStatusNo = 3   \r\n " +
          "   END    \r\n " +
          "   ELSE IF(@LoanYear = @Getnull - 1)  \r\n " +
          "   BEGIN  \r\n " +
          " 	SELECT NULL;  \r\n " +
          "   END  \r\n " +
          "   ELSE IF (@Getnull != 0)  \r\n " +
          "   BEGIN    \r\n " +
          "       SELECT TOP 1 ISNULL(MAX(a.Year) + 1 , 0)  \r\n " +
          "       FROM EmployeeBank.dbo.tblDividend as a     \r\n " +
          "       WHERE a.Cancel = 1 ;    \r\n " +
          "   END    \r\n " +
          "       \r\n " +
          "SELECT TOP 1 MAX(CASE \r\n " +
          "	WHEN a.YearPay = @LoanYear and a.MonthPay + a.PayNo - 1 > 12 THEN 2022 + 1 \r\n " +
          "	WHEN a.YearPay > @LoanYear THEN a.YearPay \r\n " +
          "	END) \r\n " +
          "FROM EmployeeBank.dbo.tblLoan as a \r\n " +
          "WHERE a.LoanStatusNo = 2 or a.LoanStatusNo = 3"
           ,

           
           //[2] Table[1]Get Name , a.SavingAmount , a.DividendAmount , a.Interest , a.RemainInterestLastYear , a.AverageDividend Table[2]Get InterestLastYear INPUT: {Year}
           "SELECT CAST(ISNULL(c.PrefixNameFull , '') + b.Fname + ' ' + b.Lname as nvarchar) , ISNULL(a.SavingAmount,0)  \r\n " +
          ", a.DividendAmount , a.Interest , ISNULL(a.RemainInterestLastYear,0) , ISNULL(a.AverageDividend,0) \r\n " +
          "FROM (SELECT a.TeacherNo , a.SavingAmount , a.DividendAmount , b.Interest , b.RemainInterestLastYear , b.AverageDividend \r\n " +
          "	FROM EmployeeBank.dbo.tblDividendDetail as a \r\n " +
          "	LEFT JOIN EmployeeBank.dbo.tblDividend as b on a.DividendNo = b.DividendNo WHERE b.Cancel = 1 and b.Year = {Year}) as a  \r\n " +
          "LEFT JOIN Personal.dbo.tblTeacherHis as b on a.TeacherNo = b.TeacherNo \r\n " +
          "LEFT JOIN BaseData.dbo.tblPrefix as c on b.PrefixNo = c.PrefixNo; \r\n " +
          " \r\n " +
          "SELECT ISNULL(a.RemainInterestLastYear , 0)  \r\n " +
          "FROM EmployeeBank.dbo.tblDividend as a \r\n " +
          "WHERE a.Year = {Year} - 1 and a.Cancel = 1"

             ,

           //[3] AfterDividentInfo INPUT: {Year}
           "DECLARE @Interest float ;    \r\n " +
          "    DECLARE @InterestNextYear float;    \r\n " +
          "    DECLARE @InterestBeforYear float;    \r\n " +
          "    DECLARE @AmountShare float ;    \r\n " +
          "    DECLARE @AVGDivident float;     \r\n " +
          "    DECLARE @AmountDivident float;    \r\n " +
          "    DECLARE @AmountSaving float;    \r\n " +
          "    DECLARE @PerShare int ;    \r\n " +
          "       \r\n " +
          "      --PerShare     \r\n " +
          "    SELECT @PerShare = a.PerShare     \r\n " +
          "    FROM EmployeeBank.dbo.tblSettingAmount as a ;     \r\n " +
          "       \r\n " +
          "     --AmountShare     \r\n " +
          "    SELECT @AmountShare = (SUM(b.SavingAmount ) / @PerShare )     \r\n " +
          "    FROM EmployeeBank.dbo.tblMember as a     \r\n " +
          "    LEFT JOIN EmployeeBank.dbo.tblShare as b on a.TeacherNo = b.TeacherNo     \r\n " +
          "    WHERE a.MemberStatusNo = 1;     \r\n " +
          "    --AmountSaving    \r\n " +
          "     SELECT @AmountSaving = (SUM(b.SavingAmount ))     \r\n " +
          "    FROM EmployeeBank.dbo.tblMember as a     \r\n " +
          "    LEFT JOIN EmployeeBank.dbo.tblShare as b on a.TeacherNo = b.TeacherNo     \r\n " +
          "    WHERE a.MemberStatusNo = 1;     \r\n " +
          "       \r\n " +
          "    --Interest     \r\n " +
          "   SELECT @Interest = SUM(ISNULL(CASE      \r\n " +
          "         WHEN a.YearPay = {Year} - 1 and a.MonthPay + a.PayNo - 1 > 12  THEN (ROUND((CAST(a.InterestRate as float) / 100) * a.LoanAmount , 0) / a.PayNo  * (a.PayNo - (12 - a.MonthPay + 1)))      \r\n " +
          "         WHEN a.YearPay = {Year} and a.MonthPay + a.PayNo - 1 > 12 THEN (ROUND((CAST(a.InterestRate as float) / 100) * a.LoanAmount , 0) / a.PayNo  * (12 - a.MonthPay + 1))      \r\n " +
          "   	  WHEN a.YearPay = {Year} and a.MonthPay + a.PayNo - 1 <= 12 THEN ROUND((CAST(a.InterestRate as float) / 100) * a.LoanAmount , 0)     \r\n " +
          "     END , 0))      \r\n " +
          "     FROM EmployeeBank.dbo.tblLoan as a      \r\n " +
          "     WHERE (a.LoanStatusNo = 2 or a.LoanStatusNo = 3) and a.YearPay = {Year} or (a.YearPay = {Year} - 1 and a.MonthPay + a.PayNo - 1 > 12);  \r\n " +
          "   \r\n " +
          "     SELECT @Interest = @Interest + RemainInterestLastYear    \r\n " +
          "    FROM EmployeeBank.dbo.tblDividend     \r\n " +
          "    WHERE Cancel = 1 and Year = {Year} - 1;    \r\n " +
          "     \r\n " +
          "    SELECT @InterestBeforYear = RemainInterestLastYear   \r\n " +
          "    FROM EmployeeBank.dbo.tblDividend     \r\n " +
          "    WHERE Cancel = 1 and Year = {Year} - 1;    \r\n " +
          "    --AVGDivident\r\n" +
             "if(@AmountShare > 0) \r\n " +
             "BEGIN \r\n" +
          "     SET @AVGDivident = @Interest/@AmountShare; \r\n " +
             "END \r\n" +
          "     \r\n " +
          "   --@AMountDivident    \r\n " +
          "   DECLARE @SumSavingAmount float;   \r\n " +
          "  SELECT @SumSavingAmount = SUM(SavingAmount)   \r\n " +
          "  FROM EmployeeBank.dbo.tblShare   \r\n " +
          "   SET @AmountDivident = (SELECT SUM(ROUND(SavingAmount / @SumSavingAmount * @Interest , 0 , 1))   \r\n " +
          "  FROM EmployeeBank.dbo.tblShare);    \r\n " +
          "     \r\n " +
          "  --InterestNextYear    \r\n " +
          "  SET @InterestNextYear =  ( SELECT @Interest - SUM(ROUND((SavingAmount/@SumSavingAmount) * @Interest , 0,1))  \r\n " +
          "  FROM EmployeeBank.dbo.tblShare );    \r\n " +
          "       \r\n " +
          "  SELECT ROUND(ISNULL(@Interest, 0) , 2) as InterestInyear , ISNULL(@InterestBeforYear,0) as InterestBeforYear, ROUND(ISNULL(@InterestNextYear , 0) , 2) as InterestNextYear , ISNULL(@AmountSaving, 0) as AmountSaving , ROUND(ISNULl(@AVGDivident,0) , 2) as AVGDivident , ISNULL(@AmountDivident,0) as AmountDivident   \r\n " +
          "     \r\n " +
          "  SELECT a.TeacherNo ,CAST(ISNULL(c.Prefixname,'')+ b.Fname + ' ' + b.Lname as nvarchar) , SavingAmount , ROUND((d.SavingAmount/@SumSavingAmount) * @Interest , 0,1) as Dividend   \r\n " +
          "  FROM EmployeeBank.dbo.tblMember as a    \r\n " +
          "  LEFT JOIN Personal.dbo.tblTeacherHis as b on a.TeacherNo = b.TeacherNo    \r\n " +
          "  LEFT JOIN BaseData.dbo.tblPrefix as c on b.PrefixNo =  c.PrefixNo    \r\n " +
          "  LEFT JOIN EmployeeBank.dbo.tblShare as d on a.TeacherNo = d.TeacherNo    \r\n " +
          "  WHERE MemberStatusNo =  1   \r\n " +
          "   \r\n " +
          " SELECT Year   \r\n " +
          "  FROM EmployeeBank.dbo.tblDividend    \r\n " +
          "  WHERE Cancel = 1   \r\n " +
          "  GROUP BY Year"
           ,

           //[4] Check Loan Paid are Complete in Year INPUT: {Year}
           "SELECT COUNT(a.LoanNo) \r\n " +
          " FROM EmployeeBank.dbo.tblLoan as a  \r\n " +
          " LEFT JOIN Personal.dbo.tblTeacherHis as b on a.TeacherNo = b.TeacherNo  \r\n " +
          " LEFT JOIN BaseData.dbo.tblPrefix as c on b.PrefixNo = c.PrefixNo  \r\n " +
          " WHERE a.LoanNo NOT IN (SELECT a.LoanNo  \r\n " +
          " FROM EmployeeBank.dbo.tblLoan as a   \r\n " +
          " LEFT JOIN EmployeeBank.dbo.tblBillDetail as b on a.LoanNo = b.LoanNo  \r\n " +
          " LEFT JOIN EmployeeBank.dbo.tblBill as c on b.BillNo = c.BillNo  \r\n " +
          " WHERE (b.Mount = 12 and b.Year = {Year} and c.Cancel = 1 and b.TypeNo = 2)  \r\n " +
          " GROUP BY a.LoanNo) and a.LoanStatusNo = 2"
           
           ,

           //[5] Get TeacherList Unpaid INPUT: {Year}
           "SELECT a.LoanNo , a.TeacherNo ,CAST(ISNULL(c.PrefixNameFull , '') + b.Fname + ' ' + b.Lname as NVARCHAR(255))  \r\n " +
          " FROM EmployeeBank.dbo.tblLoan as a  \r\n " +
          " LEFT JOIN Personal.dbo.tblTeacherHis as b on a.TeacherNo = b.TeacherNo  \r\n " +
          " LEFT JOIN BaseData.dbo.tblPrefix as c on b.PrefixNo = c.PrefixNo  \r\n " +
          " WHERE a.LoanNo NOT IN (SELECT a.LoanNo  \r\n " +
          " FROM EmployeeBank.dbo.tblLoan as a   \r\n " +
          " LEFT JOIN EmployeeBank.dbo.tblBillDetail as b on a.LoanNo = b.LoanNo  \r\n " +
          " LEFT JOIN EmployeeBank.dbo.tblBill as c on b.BillNo = c.BillNo  \r\n " +
          " WHERE (b.Mount = 12 and b.Year = {Year} and c.Cancel = 1 and b.TypeNo = 2)  \r\n " +
          " GROUP BY a.LoanNo) and a.LoanStatusNo = 2 \r\n " +
          " ORDER BY b.Fname"
           ,



         };

        private void Dividend_Load(object sender, EventArgs e)
        {
//<<<<<<< POON_File
            //DataSet dsStartYear = Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[1]+"\r\n"+SQLDefault[4]);
            //String ddd = dsStartYear.Tables[0].Rows[0][0].ToString();
            //if (dsStartYear.Tables[0].Rows[0][0].ToString() != "NULL")
//=======
            DataSet dsStartYear = Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[1]+"\r\n");
            if(dsStartYear.Tables[0].Rows[0][0].ToString() != "")
//>>>>>>> master
            {
                for(int x = 0; x < dsStartYear.Tables[0].Rows.Count; x++)
                {
                    CBYearDividend.Items.Add(dsStartYear.Tables[0].Rows[x][0]);
                }
                CBYearDividend.Enabled = true;
            }
//<<<<<<< POON_File
            //else if (dsStartYear.Tables[1].Rows[0][0].ToString() != "NULL")
//=======
            if(dsStartYear.Tables[1].Rows[0][0].ToString() != "")
//>>>>>>> master
            {
                for(int x = 0; x < dsStartYear.Tables[1].Rows.Count; x++)
                {
                    CBYearDividend.Items.Add(dsStartYear.Tables[1].Rows[x][0]);
                }
                CBYearDividend.Enabled = true;
            }
            //if (CBYearDividend.Items.Count != 0)
            //{
            //    for (int x = 0; x < CBYearDividend.Items.Count; x++)
            //    {
            //        for (int y = 0; y < CBYearDividend.Items.Count; y++)
            //        {
            //            if (dsStartYear.Tables[1].Rows[x][0].ToString() == CBYearDividend.Items[y].ToString())
            //            {
            //                CBYearDividend.Items.RemoveAt(y);
            //                break;
            //            }
            //        }
            //    }
            //}
            if (CBYearDividend.Items.Count == 0)
            {
                CBYearDividend.Enabled = false;
            }
            else
            {
                CBYearDividend.SelectedIndex = 0;
            }
        }

        private void BSaveDividend_Click(object sender, EventArgs e)
        {
            CBYearDividend.DroppedDown = false;
            if (MessageBox.Show("ยืนยันที่จะบันทึกหรือไม่", "แจ้งเตือน", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes && CBYearDividend.SelectedIndex != -1
                && CBYearDividend.SelectedIndex == 0)
            {
                DataTable dtCheckPaid = Class.SQLConnection.InputSQLMSSQL(SQLDefault[4].Replace("{Year}", CBYearDividend.SelectedItem.ToString()));
                if (Convert.ToInt32(dtCheckPaid.Rows[0][0].ToString()) == 0)
                {
                    try
                    {
                        Class.SQLConnection.InputSQLMSSQL(SQLDefault[0]
                        .Replace("{Year}", CBYearDividend.Items[CBYearDividend.SelectedIndex].ToString())
                        .Replace("{TeacherAddbyNo}", Class.UserInfo.TeacherNo));
                        MessageBox.Show("บันทึกสำเร็จ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        if (DGV.Rows.Count != 0)
                        {
                            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                            {
                                printDocument1.Print();
                            }
                        }

                        CBYearDividend.Items.RemoveAt(CBYearDividend.SelectedIndex);
                        if (CBYearDividend.Items.Count != 0)
                            CBYearDividend.SelectedIndex = -1;
                        else
                            CBYearDividend.Enabled = false;
                        BSaveDividend.Enabled = false;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"------------------------{ex}-------------------------");
                        MessageBox.Show("บันทึกล้มเหลว", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    if (MessageBox.Show("ต้องการที่จะดูรายชื่อผู้ที่ค้างชำระหรือไม่", "แจ้งเตือน", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        DataTable dtUnpaid = Class.SQLConnection.InputSQLMSSQL(SQLDefault[5].Replace("{Year}", CBYearDividend.SelectedItem.ToString()));
                        UnpaidLoanList UnpaidLoan = new UnpaidLoanList();
                        this.Enabled = false;
                        UnpaidLoan.Show();
                        UnpaidLoan.FDividend = this;
                        for (int a = 0; a < dtUnpaid.Rows.Count; a++)
                        {
                            UnpaidLoan.DGV.Rows.Add(dtUnpaid.Rows[a][0], dtUnpaid.Rows[a][1], dtUnpaid.Rows[a][2]);
                        }
                        UnpaidLoan.Focus();
                    }
                }

            }
            else if (CBYearDividend.SelectedIndex > 0)
                MessageBox.Show("โปรดปันผลปีก่อนหน้าก่อน", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (CBYearDividend.SelectedIndex == -1)
                MessageBox.Show("โปรดทำการเลือกปีที่จะปันผลก่อน", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void CBYearDividend_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CBYearDividend.SelectedIndex != -1)
            {
//<<<<<<< POON_File
                //DataSet ds = Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[3].Replace("{Year}",CBYearDividend.SelectedItem.ToString())+
                   // "\r\n"+
                    //SQLDefault[4]);
//=======
                DataSet ds = Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[3].Replace("{Year}",CBYearDividend.Items[CBYearDividend.SelectedIndex].ToString()));
//>>>>>>> master
                if(ds.Tables[0].Rows.Count != 0)
                {
                    DGV.Rows.Clear();
                    TBSavingAmount.Text = ds.Tables[0].Rows[0][3].ToString();
                        TBDividendAmount.Text = ds.Tables[0].Rows[0][5].ToString();
                        TBInterestAmount.Text = ds.Tables[0].Rows[0][0].ToString();
                        TBDividendPerShare.Text = (Math.Round(Convert.ToDouble(ds.Tables[0].Rows[0][4].ToString()),1)).ToString();
                        TBInterestNextYear.Text = ds.Tables[0].Rows[0][2].ToString();
                        TBRemainInterest.Text = ds.Tables[0].Rows[0][1].ToString();
                    BSaveDividend.Enabled = true;
                }
                else
                {

                }
                if(ds.Tables[1].Rows.Count != 0)
                {
                    for (int x = 0; x < ds.Tables[1].Rows.Count; x++)
                    {
                        DGV.Rows.Add(x+1,ds.Tables[1].Rows[x][0], ds.Tables[1].Rows[x][1], ds.Tables[1].Rows[x][2], ds.Tables[1].Rows[x][3]);
                    } 
                }
                if(ds.Tables[2].Rows.Count != 0)
                {
                    for(int x = 0; x < ds.Tables[2].Rows.Count; x++)
                    {
                        if(ds.Tables[2].Rows[x][0].ToString() == CBYearDividend.Items[CBYearDividend.SelectedIndex].ToString())
                        {
                            MessageBox.Show("ปีที่ท่านเลือกได้มีการปันผลไปแล้ว","ระบบ",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                            break;
                        }
                        else if (x == ds.Tables[2].Rows.Count - 1)
                        {
                            BSaveDividend.Enabled = true;
                        }
                    }
                }
            }
        }

        private void BExitForm_Click(object sender, EventArgs e)
        {
            BankTeacher.Class.FromSettingMedtod.ReturntoHome(this);
        }

        private void Dividend_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if(CBYearDividend.SelectedIndex > -1)
                {
                    CBYearDividend.DroppedDown = false;
                    CBYearDividend.SelectedIndex = -1;
                    TBDividendAmount.Text = "";
                    TBDividendPerShare.Text = "";
                    TBInterestAmount.Text = "";
                    TBInterestNextYear.Text = "";
                    TBRemainInterest.Text = "";
                    TBSavingAmount.Text = "";
                    DGV.Rows.Clear();
                }
                else
                    BExitForm_Click(new object(), new EventArgs());
            }
        }

        private void Dividend_SizeChanged(object sender, EventArgs e)
        {
            int x = this.Width / 2 - panel1.Size.Width / 2;
            int y = this.Height / 2 - panel1.Size.Height / 2;
            panel1.Location = new Point(x, y);
        }
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Class.Print.PrintPreviewDialog.PrintReportGrid(e, DGV, "ปันผล", AccessibilityObject.Name, true, true, "A4", 1); 
        }
    }
}
