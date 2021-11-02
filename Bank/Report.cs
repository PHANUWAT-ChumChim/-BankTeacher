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
        /// </summary> 
        private String[] SQLDefault = new String[]
         { 
          //[0] SELECT Report Income INPUT: {Date} 
          "SELECT a.TeacherNo ,a.Name ,SUM(ISNULL(a.Amount , 0)) - (SUM(ISNULL(a.DefualtPayOfMonth , 0)) + SUM(ISNULL(a.Interest , 0))) as Share , SUM(ISNULL(a.DefualtPayOfMonth , 0)) as PayIncome , SUM(ISNULL(a.Interest , 0)) as Interest \r\n " +
          ",(SUM(ISNULL(a.Amount , 0)) - (SUM(ISNULL(a.DefualtPayOfMonth , 0)) + SUM(ISNULL(a.Interest , 0)))) + (SUM(ISNULL(a.DefualtPayOfMonth , 0))) + (SUM(ISNULL(a.Interest , 0))) as SumIncome \r\n " +
          "FROM(SELECT a.TeacherNo ,c.Amount , \r\n " +
          "CASE  \r\n " +
          "    WHEN CAST(CAST(c.Year as varchar) + '/' + CAST(c.Mount as varchar) + '/01' as date) LIKE DATEADD(MONTH, d.PayNo - 1,CAST(CAST(d.YearPay as varchar) + '/' + CAST(d.MonthPay as varchar) + '/01' as date)) \r\n " +
          "    THEN d.LoanAmount - ((d.LoanAmount / d.PayNo) * (d.PayNo - 1)) \r\n " +
          "ELSE (d.LoanAmount / d.PayNo) \r\n " +
          "END as DefualtPayOfMonth \r\n " +
          "    , \r\n " +
          "CASE  \r\n " +
          "    WHEN CAST(CAST(c.Year as varchar) + '/' + CAST(c.Mount as varchar) + '/01' as date) LIKE DATEADD(MONTH, d.PayNo - 1,CAST(CAST(d.YearPay as varchar) + '/' + CAST(d.MonthPay as varchar) + '/01' as date)) \r\n " +
          "    THEN c.Amount - (d.LoanAmount - ((d.LoanAmount / d.PayNo) * (d.PayNo - 1))) \r\n " +
          "ELSE c.Amount - (d.LoanAmount / d.PayNo) \r\n " +
          "END as Interest \r\n " +
          ",CAST(ISNULL(f.PrefixNameFull , '') + e.Fname + e.Lname as varchar) as Name , e.Fname \r\n " +
          "FROM EmployeeBank.dbo.tblMember as a \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblBill as b on a.TeacherNo = b.TeacherNo \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblBillDetail as c on b.BillNo = c.BillNo \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblLoan as d on c.LoanNo = d.LoanNo \r\n " +
          "LEFT JOIN Personal.dbo.tblTeacherHis as e on a.TeacherNo = e.TeacherNo \r\n " +
          "LEFT JOIN BaseData.dbo.tblPrefix as f on e.PrefixNo = f.PrefixNo \r\n " +
          "WHERE CAST(CAST(b.DateAdd as date) as varchar) LIKE '{Date}%') as a \r\n " +
          "GROUP BY a.TeacherNo ,a.Name "
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
        }

        private void CBYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(CBYear.SelectedIndex != -1)
            {
                int ShareSum = 0, LoanAmountSum = 0, InterestSum = 0, SumIncome = 0;
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
                DataSet ds = Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[0]
                    .Replace("{Date}", CBYear.Text));
                DGVReportIncome.Rows.Clear();
                for (int a = 0; a < ds.Tables[0].Rows.Count; a++)
                {
                    DGVReportIncome.Rows.Add(ds.Tables[0].Rows[a][0].ToString(), ds.Tables[0].Rows[a][1].ToString(), ds.Tables[0].Rows[a][2].ToString(), ds.Tables[0].Rows[a][3].ToString()
                        ,ds.Tables[0].Rows[a][4].ToString(), ds.Tables[0].Rows[a][5].ToString());
                    ShareSum += Convert.ToInt32(ds.Tables[0].Rows[a][2].ToString());
                    LoanAmountSum += Convert.ToInt32(ds.Tables[0].Rows[a][3].ToString());
                    InterestSum += Convert.ToInt32(ds.Tables[0].Rows[a][4].ToString());
                    SumIncome += Convert.ToInt32(ds.Tables[0].Rows[a][5].ToString());
                }
                TBSavingAmount.Text = ShareSum.ToString();
                TBLoanAmount.Text = LoanAmountSum.ToString();
                TBInterest.Text = InterestSum.ToString();
                TBSumIncome.Text = SumIncome.ToString();
            }
        }

        private void CBMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CBMonth.SelectedIndex != -1)
            {
                DataSet ds;
                int ShareSum = 0, LoanAmountSum = 0, InterestSum = 0, SumIncome = 0;
                if (CBMonth.SelectedIndex >= 1 && CBMonth.SelectedIndex < 10)
                {
                    ds = Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[0]
                    .Replace("{Date}", CBYear.SelectedItem.ToString() + "-0" + CBMonth.SelectedItem.ToString()));
                }
                else if (CBMonth.SelectedIndex >= 10)
                {
                    ds = Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[0]
                    .Replace("{Date}", CBYear.SelectedItem.ToString() + "-" + CBMonth.SelectedItem.ToString()));
                }
                else
                {
                    ds = Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[0]
                   .Replace("{Date}", CBYear.SelectedItem.ToString() + "-"));
                }
                DGVReportIncome.Rows.Clear();

                for (int a = 0; a < ds.Tables[0].Rows.Count; a++)
                {
                    DGVReportIncome.Rows.Add(ds.Tables[0].Rows[a][0].ToString(), ds.Tables[0].Rows[a][1].ToString(), ds.Tables[0].Rows[a][2].ToString(), ds.Tables[0].Rows[a][3].ToString()
                        , ds.Tables[0].Rows[a][4].ToString(), ds.Tables[0].Rows[a][5].ToString());
                    ShareSum += Convert.ToInt32(ds.Tables[0].Rows[a][2].ToString());
                    LoanAmountSum += Convert.ToInt32(ds.Tables[0].Rows[a][3].ToString());
                    InterestSum += Convert.ToInt32(ds.Tables[0].Rows[a][4].ToString());
                    SumIncome += Convert.ToInt32(ds.Tables[0].Rows[a][5].ToString());
                }
                TBSavingAmount.Text = ShareSum.ToString();
                TBLoanAmount.Text = LoanAmountSum.ToString();
                TBInterest.Text = InterestSum.ToString();
                TBSumIncome.Text = SumIncome.ToString();
            }
        }
    }
}
