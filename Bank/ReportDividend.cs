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
    public partial class ReportDividend : Form
    {
        public ReportDividend()
        {
            InitializeComponent();
        }

        /// <summary> 
        /// SQLDefault 
        /// <para>[0]SELECT Top 5 Year descending INPUT: </para> 
        /// <para>[1] Table[1]Get Name , a.SavingAmount , a.DividendAmount , a.Interest , a.RemainInterestLastYear , a.AverageDividend Table[2]Get InterestLastYear INPUT: {Year} </para>
        /// </summary> 
        private String[] SQLDefault = new String[]
         { 
           //[0]SELECT Top 5 Year descending INPUT: 
           "SELECT TOP 5 a.Year \r\n " +
          "FROM EmployeeBank.dbo.tblDividend as a \r\n " +
          "WHERE a.Cancel = 1 \r\n " +
          "GROUP BY a.Year \r\n " +
          "ORDER BY a.Year DESC \r\n "
           ,

           //[1] Table[1]Get Name , a.SavingAmount , a.DividendAmount , a.Interest , a.RemainInterestLastYear , a.AverageDividend Table[2]Get InterestLastYear INPUT: {Year}
           "SELECT CAST(ISNULL(c.PrefixNameFull , '') + b.Fname + ' ' + b.Lname as nvarchar) , a.SavingAmount  \r\n " +
          ", a.DividendAmount , a.Interest , a.RemainInterestLastYear , a.AverageDividend \r\n " +
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


         };
        private void ReportDividend_Load(object sender, EventArgs e)
        {
            DataTable dtYear = Class.SQLConnection.InputSQLMSSQL(SQLDefault[0]);
            for(int x = 0; x < dtYear.Rows.Count; x++)
            {
                CBYear.Items.Add(dtYear.Rows[x][0].ToString());
            }
        }

        private void CBYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(CBYear.SelectedIndex != -1)
            {
                DGVReportDividend.Rows.Clear();

                DataSet dsReport = Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[1]
                    .Replace("{Year}", CBYear.SelectedItem.ToString()));

                int SumSavingAmount = 0, SumDividendAmount = 0;
                for(int x = 0; x < dsReport.Tables[0].Rows.Count; x++)
                {
                    DGVReportDividend.Rows.Add(dsReport.Tables[0].Rows[x][0].ToString(), dsReport.Tables[0].Rows[x][1].ToString(), dsReport.Tables[0].Rows[x][2].ToString());
                    SumSavingAmount += Convert.ToInt32(dsReport.Tables[0].Rows[x][1].ToString());
                    SumDividendAmount += Convert.ToInt32(dsReport.Tables[0].Rows[x][2].ToString());
                }
                LSavingAmount.Text = SumSavingAmount.ToString();
                LDividendAmount.Text = SumDividendAmount.ToString();
                LInterestAmount.Text = dsReport.Tables[0].Rows[0][3].ToString();
                LInterestNextYear.Text = dsReport.Tables[0].Rows[0][4].ToString();
                LDividendPerShare.Text = dsReport.Tables[0].Rows[0][5].ToString();
                LRemainInterest.Text = dsReport.Tables[1].Rows[0][0].ToString();
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
