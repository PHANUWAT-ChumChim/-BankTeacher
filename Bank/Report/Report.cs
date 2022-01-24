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
    public partial class Report : Form
    {
        public Report()
        {
            InitializeComponent();
        }

        /// <summary> 
        /// SQLDafault 
        /// <para>[0] SELECT Report Income INPUT: {Date} </para> 
        /// <para>[1] Table(1) SELECT Expenses/Teacher , Table(2)SELECT SUMAmountOff , SUMDividend , SUMPayLoan INPUT: {Date} , {Year} </para>
        /// </summary> 
        private String[] SQLDefault = new String[]
         { 
          //[0] SELECT Report Income INPUT: {Date} 
           "SELECT a.TeacherNo ,a.Name ,SUM(ISNULL(a.Amount , 0)) - (SUM(ISNULL(a.DefualtPayOfMonth , 0)) + SUM(ISNULL(a.Interest , 0))) as Share , SUM(ISNULL(a.DefualtPayOfMonth , 0)) as PayIncome , SUM(ISNULL(a.Interest , 0)) as Interest \r\n " +
          ",(SUM(ISNULL(a.Amount , 0)) - (SUM(ISNULL(a.DefualtPayOfMonth , 0)) + SUM(ISNULL(a.Interest , 0)))) + (SUM(ISNULL(a.DefualtPayOfMonth , 0))) + (SUM(ISNULL(a.Interest , 0))) as SumIncome  \r\n " +
          "FROM(SELECT a.TeacherNo ,c.Amount ,  \r\n " +
          "CASE   \r\n " +
          "    WHEN CAST(CAST(c.Year as varchar) + '/' + CAST(c.Mount as varchar) + '/01' as date) LIKE DATEADD(MONTH, d.PayNo - 1,CAST(CAST(d.YearPay as varchar) + '/' + CAST(d.MonthPay as varchar) + '/01' as date)) \r\n " +
          "    THEN d.LoanAmount - ((d.LoanAmount / d.PayNo) * (d.PayNo - 1))  \r\n " +
          "ELSE (d.LoanAmount / d.PayNo)  \r\n " +
          "END as DefualtPayOfMonth  \r\n " +
          "    ,  \r\n " +
          "CASE   \r\n " +
          "    WHEN CAST(CAST(c.Year as varchar) + '/' + CAST(c.Mount as varchar) + '/01' as date) LIKE DATEADD(MONTH, d.PayNo - 1,CAST(CAST(d.YearPay as varchar) + '/' + CAST(d.MonthPay as varchar) + '/01' as date))  \r\n " +
          "    THEN c.Amount - (d.LoanAmount - ((d.LoanAmount / d.PayNo) * (d.PayNo - 1)))  \r\n " +
          "ELSE c.Amount - (d.LoanAmount / d.PayNo)  \r\n " +
          "END as Interest  \r\n " +
          ",CAST(ISNULL(f.PrefixNameFull , '') + e.Fname + ' ' + e.Lname as nvarchar) as Name , e.Fname  \r\n " +
          "FROM EmployeeBank.dbo.tblMember as a  \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblBill as b on a.TeacherNo = b.TeacherNo  \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblBillDetail as c on b.BillNo = c.BillNo  \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblLoan as d on c.LoanNo = d.LoanNo  \r\n " +
          "LEFT JOIN Personal.dbo.tblTeacherHis as e on a.TeacherNo = e.TeacherNo  \r\n " +
          "LEFT JOIN BaseData.dbo.tblPrefix as f on e.PrefixNo = f.PrefixNo  \r\n " +
          "WHERE CAST(CAST(b.DateAdd as date) as nvarchar) LIKE '%' and b.Cancel = 1 ) as a  \r\n " +
          "GROUP BY a.TeacherNo ,a.Name "
           
          ,

          //[1]Table(1) SELECT Expenses/Teacher   Table(2)SELECT SUMAmountOff , SUMDividend , SUMPayLoan INPUT: {Date} , {Year}
           "SELECT a.TeacherNo , a.Name , a.AmountOff , a.Dividend , a.LoanPay  \r\n " +
          "FROM(SELECT a.TeacherNo ,CAST(ISNULL(h.PrefixNameFull, '') + g.Fname + ' ' + g.Lname as nvarchar) as Name \r\n " +
          ",SUM(ISNULL(c.Amount ,0)) as AmountOff ,d.DividendAmount as Dividend ,ISNULL(f.LoanPay ,0) as LoanPay , g.Fname  \r\n " +
          "FROM (SELECT *  \r\n " +
          "FROM EmployeeBank.dbo.tblMember)as a  \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblShare as b on a.TeacherNo = b.TeacherNo  \r\n " +
          "LEFT JOIN (  \r\n " +
          "SELECT ShareNo , SUM(Amount) as Amount  \r\n " +
          "FROM EmployeeBank.dbo.tblShareWithdraw  \r\n " +
          "WHERE CAST(DateAdd as date) LIKE '%' \r\n " +
          "GROUP BY ShareNo)  \r\n " +
          "as c on b.ShareNo = c.ShareNo  \r\n " +
          "LEFT JOIN (  \r\n " +
          "SELECT a.TeacherNo , SUM(DividendAmount) as DividendAmount  \r\n " +
          "FROM EmployeeBank.dbo.tblDividendDetail as a  \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblDividend as b on a.DividendNo = b.DividendNo \r\n " +
          "WHERE b.Year = 2021 \r\n " +
          "GROUP BY a.TeacherNo) as d on a.TeacherNo = d.TeacherNo \r\n " +
          "LEFT JOIN(  \r\n " +
          "SELECT SUM(ISNULL(a.LoanAmount ,0)) as LoanPay , a.TeacherNo  \r\n " +
          "FROM EmployeeBank.dbo.tblLoan as a \r\n " +
          "WHERE CAST(a.PayDate as date) LIKE '%' and a.LoanStatusNo != 4 \r\n " +
          "GROUP BY a.TeacherNo) as f on a.TeacherNo = f.TeacherNo  \r\n " +
          "LEFT JOIN Personal.dbo.tblTeacherHis as g on a.TeacherNo = g.TeacherNo  \r\n " +
          "LEFT JOIN BaseData.dbo.tblPrefix as h on g.PrefixNo = h.PrefixNo  \r\n " +
          "GROUP BY a.TeacherNo ,CAST(ISNULL(h.PrefixNameFull, '') + g.Fname + ' ' + g.Lname as nvarchar) , g.Fname ,ISNULL(f.LoanPay ,0) ,d.DividendAmount) as a  \r\n " +
          "ORDER BY a.Fname;  \r\n " +
          " \r\n " +
          " \r\n " +
          " SELECT SUM(ISNULL(a.AmountOff , 0)) , SUM(ISNULL(a.Dividend,0)) , SUM(ISNULL(a.LoanPay,0))  , SUM(ISNULL(a.AmountOff,0)) + SUM(ISNULL(a.Dividend,0)) + SUM(ISNULL(a.LoanPay,0))\r\n " +
          "FROM(SELECT a.TeacherNo ,CAST(ISNULL(h.PrefixNameFull, '') + g.Fname + ' ' + g.Lname as nvarchar) as Name  \r\n " +
          ",SUM(ISNULL(c.Amount ,0)) as AmountOff ,d.DividendAmount as Dividend ,ISNULL(f.LoanPay ,0) as LoanPay , g.Fname  \r\n " +
          "FROM (SELECT *  \r\n " +
          "FROM EmployeeBank.dbo.tblMember  \r\n " +
          "WHERE MemberStatusNo = 1) as a  \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblShare as b on a.TeacherNo = b.TeacherNo  \r\n " +
          "LEFT JOIN (  \r\n " +
          "SELECT ShareNo , SUM(Amount) as Amount  \r\n " +
          "FROM EmployeeBank.dbo.tblShareWithdraw  \r\n " +
          "WHERE CAST(DateAdd as date) LIKE '%'  \r\n " +
          "GROUP BY ShareNo)  \r\n " +
          "as c on b.ShareNo = c.ShareNo \r\n " +
          "LEFT JOIN (  \r\n " +
          "SELECT a.TeacherNo , SUM(DividendAmount) as DividendAmount  \r\n " +
          "FROM EmployeeBank.dbo.tblDividendDetail as a  \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblDividend as b on a.DividendNo = b.DividendNo  \r\n " +
          "WHERE b.Year = 2021 \r\n " +
          "GROUP BY a.TeacherNo) as d on a.TeacherNo = d.TeacherNo  \r\n " +
          "LEFT JOIN(  \r\n " +
          "SELECT SUM(ISNULL(a.LoanAmount ,0)) as LoanPay , a.TeacherNo  \r\n " +
          "FROM EmployeeBank.dbo.tblLoan as a  \r\n " +
          "WHERE CAST(a.PayDate as date) LIKE '%' and a.LoanStatusNo != 4 \r\n " +
          "GROUP BY a.TeacherNo) as f on a.TeacherNo = f.TeacherNo \r\n " +
          "LEFT JOIN Personal.dbo.tblTeacherHis as g on a.TeacherNo = g.TeacherNo \r\n " +
          "LEFT JOIN BaseData.dbo.tblPrefix as h on g.PrefixNo = h.PrefixNo \r\n " +
          "GROUP BY a.TeacherNo ,CAST(ISNULL(h.PrefixNameFull, '') + g.Fname + ' ' + g.Lname as nvarchar) , g.Fname ,ISNULL(f.LoanPay ,0) ,d.DividendAmount) as a \r\n " +
          ""

           ,
         };
        private void Report_Load(object sender, EventArgs e)
        {
            int Year = Convert.ToInt32(BankTeacher.Bank.Menu.Date[0]);
            
            for(int a = 0; a < 5; a++)
            {
                CBYear.Items.Add(Year - a);
            }
            CBYear.SelectedIndex = 0;
            if(DGVReportIncome.Rows.Count != 0)
            {
                BTPrint.Visible = true;
            }
            else
            {
                BTPrint.Visible = false;
            }
        }

        private void CBYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(CBYear.SelectedIndex != -1)
            {
                //int ShareSum = 0, LoanAmountSum = 0, InterestSum = 0, SumIncome = 0;
                if(CBMonth.Items.Count != 0)
                {
                    CBMonth.Items.Clear();
                    CBMonth.SelectedIndex = -1;
                }
                int Month = Convert.ToInt32(BankTeacher.Bank.Menu.Date[1]);
                if (CBYear.SelectedIndex == 0)
                {
                    for (int a = 0; a <= Month; a++)
                    {
                        if (a == 0)
                            CBMonth.Items.Add("(none)");
                        else
                            CBMonth.Items.Add(a);
                    }
                }
                else
                {
                    for(int a = 0; a <= 12; a++)
                    {
                        if (a == 0)
                            CBMonth.Items.Add("(none)");
                        else
                            CBMonth.Items.Add(a);
                    }
                }
                CBMonth.Enabled = true;
                CBMonth.SelectedIndex = 0;
                //DataSet ds = Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[0]
                //    .Replace("{Date}", CBYear.Text));
                //DGVReportIncome.Rows.Clear();
                //for (int a = 0; a < ds.Tables[0].Rows.Count; a++)
                //{
                //    DGVReportIncome.Rows.Add(ds.Tables[0].Rows[a][0].ToString(), ds.Tables[0].Rows[a][1].ToString(), ds.Tables[0].Rows[a][2].ToString(), ds.Tables[0].Rows[a][3].ToString()
                //        ,ds.Tables[0].Rows[a][4].ToString(), ds.Tables[0].Rows[a][5].ToString());
                //    ShareSum += Convert.ToInt32(ds.Tables[0].Rows[a][2].ToString());
                //    LoanAmountSum += Convert.ToInt32(ds.Tables[0].Rows[a][3].ToString());
                //    InterestSum += Convert.ToInt32(ds.Tables[0].Rows[a][4].ToString());
                //    SumIncome += Convert.ToInt32(ds.Tables[0].Rows[a][5].ToString());
                //}
                //TBSavingAmount.Text = ShareSum.ToString();
                //TBLoanAmount.Text = LoanAmountSum.ToString();
                //TBInterest.Text = InterestSum.ToString();
                //TBSumIncome.Text = SumIncome.ToString();
            }
        }

        private void CBMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CBMonth.SelectedIndex != -1)
            {
                DataSet dsIncomeReport;
                DataSet dsExpensesReport;
                int ShareSum = 0, LoanAmountSum = 0, InterestSum = 0, SumIncome = 0;
                if (CBMonth.SelectedIndex >= 1 && CBMonth.SelectedIndex < 10)
                {
                    dsIncomeReport = Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[0]
                        .Replace("{Date}", CBYear.SelectedItem.ToString() + "-0" + CBMonth.SelectedItem.ToString()));

                    dsExpensesReport = Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[1]
                        .Replace("{Date}", CBYear.SelectedItem.ToString() + "-0" + CBMonth.SelectedItem.ToString())
                        .Replace("{Year}", CBYear.SelectedItem.ToString()));
                }
                else if (CBMonth.SelectedIndex >= 10)
                {
                    dsIncomeReport = Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[0]
                        .Replace("{Date}", CBYear.SelectedItem.ToString() + "-" + CBMonth.SelectedItem.ToString()));

                    dsExpensesReport = Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[1]
                        .Replace("{Date}", CBYear.SelectedItem.ToString() + "-" + CBMonth.SelectedItem.ToString())
                        .Replace("{Year}", CBYear.SelectedItem.ToString()));
                }
                else
                {
                    dsIncomeReport = Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[0]
                   .Replace("{Date}", CBYear.SelectedItem.ToString() + "-"));

                    dsExpensesReport = Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[1]
                        .Replace("{Date}", CBYear.SelectedItem.ToString() + "-")
                        .Replace("{Year}", CBYear.SelectedItem.ToString()));
                }
                DGVReportIncome.Rows.Clear();

                //TabControl Page IncomeReport
                for (int a = 0; a < dsIncomeReport.Tables[0].Rows.Count; a++)
                {
                    DGVReportIncome.Rows.Add(dsIncomeReport.Tables[0].Rows[a][0].ToString(), dsIncomeReport.Tables[0].Rows[a][1].ToString(), dsIncomeReport.Tables[0].Rows[a][2].ToString(), dsIncomeReport.Tables[0].Rows[a][3].ToString()
                        , dsIncomeReport.Tables[0].Rows[a][4].ToString(), dsIncomeReport.Tables[0].Rows[a][5].ToString());
                    ShareSum += Convert.ToInt32(dsIncomeReport.Tables[0].Rows[a][2].ToString());
                    LoanAmountSum += Convert.ToInt32(dsIncomeReport.Tables[0].Rows[a][3].ToString());
                    InterestSum += Convert.ToInt32(dsIncomeReport.Tables[0].Rows[a][4].ToString());
                    SumIncome += Convert.ToInt32(dsIncomeReport.Tables[0].Rows[a][5].ToString());
                }
                TBSavingAmount.Text = ShareSum.ToString();
                TBLoanAmount.Text = LoanAmountSum.ToString();
                TBInterest.Text = InterestSum.ToString();
                TBSumIncome.Text = SumIncome.ToString();

                //TabControl Page ExpensesReport
                DGVExpensesReport.Rows.Clear();
                for(int a = 0; a < dsExpensesReport.Tables[0].Rows.Count; a++)
                {
                    DGVExpensesReport.Rows.Add(dsExpensesReport.Tables[0].Rows[a][0] , dsExpensesReport.Tables[0].Rows[a][1], dsExpensesReport.Tables[0].Rows[a][2]
                        , dsExpensesReport.Tables[0].Rows[a][3], dsExpensesReport.Tables[0].Rows[a][4]);
                }
                TBAmountOff_Expenses.Text = dsExpensesReport.Tables[1].Rows[0][0].ToString();
                TBDividend_Expenses.Text = dsExpensesReport.Tables[1].Rows[0][1].ToString();
                TBPayLoan_Expenses.Text = dsExpensesReport.Tables[1].Rows[0][2].ToString();
                TBSumExpenses.Text = dsExpensesReport.Tables[1].Rows[0][3].ToString();
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
        private void Report_SizeChanged(object sender, EventArgs e)
        {
            Class.FromSettingMedtod.ChangeSizePanal(this, panel1);
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Class.Print.PrintPreviewDialog.PrintReportGrid(e, DGVReportIncome, TP_LUP.SelectedTab.Text, this.AccessibilityObject.Name, true, true, "A4",0);
        }

        private void BTPrint_Click(object sender, EventArgs e)
        {
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void Report_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                if (tabControl1.SelectedIndex == tabControl1.TabCount - 1)
                {
                    tabControl1.SelectedIndex = 0;
                }
                else
                {
                    tabControl1.SelectedIndex = tabControl1.SelectedIndex + 1;
                }
            }
        }
    }
}
