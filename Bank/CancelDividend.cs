﻿using System;
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
    public partial class CancelDividend : Form
    {
        public CancelDividend()
        {
            InitializeComponent();
        }
        /// <summary> 
        /// SQLDefault 
        /// <para>[0]SELECT Max 1 Year INPUT:  </para> 
        /// <para>[1] Table[1]Get Name , a.SavingAmount , a.DividendAmount , a.Interest , a.RemainInterestLastYear , a.AverageDividend Table[2]Get InterestLastYear INPUT: {Year} </para>
        /// <para>[2]Save Cancel Dividend and - SavingAmount INPUT: {Year}  {TeacherNo} </para>
        /// </summary> 
        private String[] SQLDefault = new String[]
         { 
           //[0]SELECT Max 1 Year INPUT: 
           "SELECT MAX(a.Year) \r\n " +
          "FROM EmployeeBank.dbo.tblDividend as a \r\n " +
          "WHERE a.Cancel = 1"


           ,

           //[1] Table[1]Get Name , a.SavingAmount , a.DividendAmount , a.Interest , a.RemainInterestLastYear , a.AverageDividend Table[2]Get InterestLastYear INPUT: {Year}
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

           //[2]Save Cancel Dividend and - SavingAmount INPUT: {Year}  {TeacherNo}
           "UPDATE EmployeeBank.dbo.tblShare \r\n " +
          "SET SavingAmount = EmployeeBank.dbo.tblShare.SavingAmount - (SELECT b.DividendAmount \r\n " +
          "FROM EmployeeBank.dbo.tblDividend as a \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblDividendDetail as b on a.DividendNo = b.DividendNo \r\n " +
          "WHERE a.Cancel = 1 and a.Year = {Year} and EmployeeBank.dbo.tblShare.TeacherNo = b.TeacherNo) \r\n " +
          "WHERE EmployeeBank.dbo.tblShare.TeacherNo IN (SELECT a.TeacherNo \r\n " +
          "FROM EmployeeBank.dbo.tblDividendDetail as a \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblDividend as b on a.DividendNo = b.DividendNo \r\n " +
          "WHERE b.Cancel = 1 and b.Year = {Year}) \r\n " +
          " \r\n " +
          "UPDATE EmployeeBank.dbo.tblDividend \r\n " +
          "SET Cancel = 2 , CancelBy = '{TeacherNo}' , DateCancel = CURRENT_TIMESTAMP \r\n " +
          "WHERE Year = {Year} and Cancel = 1"
           ,


         };

        private void CBYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CBYear.SelectedIndex != -1)
            {
                DGVReportDividend.Rows.Clear();

                DataSet dsReport = Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[1]
                    .Replace("{Year}", CBYear.Text));
                if(dsReport.Tables[0].Rows.Count != 0)
                {
                    int SumSavingAmount = 0, SumDividendAmount = 0;
                    for (int x = 0; x < dsReport.Tables[0].Rows.Count; x++)
                    {
                        DGVReportDividend.Rows.Add(dsReport.Tables[0].Rows[x][0].ToString(), dsReport.Tables[0].Rows[x][1].ToString(), dsReport.Tables[0].Rows[x][2].ToString());
                        SumSavingAmount += Convert.ToInt32(dsReport.Tables[0].Rows[x][1].ToString());
                        SumDividendAmount += Convert.ToInt32(dsReport.Tables[0].Rows[x][2].ToString());
                    }
                    TB_SavingAmount.Text = SumSavingAmount.ToString();
                    TB_DividendAmount.Text = SumDividendAmount.ToString();
                    TB_InterestAmount.Text = dsReport.Tables[0].Rows[0][3].ToString();
                    TB_InterestNextYear.Text = dsReport.Tables[0].Rows[0][4].ToString();
                    TB_DividendPerShare.Text = dsReport.Tables[0].Rows[0][5].ToString();
                    if(dsReport.Tables[1].Rows.Count != 0)
                    {
                        TB_RemainInterest.Text = dsReport.Tables[1].Rows[0][0].ToString();
                    }
                    else
                    {
                        TB_RemainInterest.Text = 0.ToString();
                    }

                    BSaveCancelDividend.Enabled = true;
                }
            }
        }

        private void BSaveCancelDividend_Click(object sender, EventArgs e)
        {
            try
            {
                Class.SQLConnection.InputSQLMSSQL(SQLDefault[2]
                .Replace("{Year}", CBYear.SelectedItem.ToString())
                .Replace("{TeacherNo}" , Class.UserInfo.TeacherNo));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"-------------------{ex}---------------------");
                MessageBox.Show("การบันทึกล้มเหลว", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BExitForm_Click(object sender, EventArgs e)
        {
            BankTeacher.Class.FromSettingMedtod.ReturntoHome(this);
        }


        private void CancelDividend_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (CBYear.SelectedIndex != -1)
                {
                    CBYear.SelectedIndex = -1;
                }
                else
                {
                    BExitForm_Click(new object(), new EventArgs());
                }
            }
        }

        private void CancelDividend_Load(object sender, EventArgs e)
        {
            DataTable dtYear = Class.SQLConnection.InputSQLMSSQL(SQLDefault[0]);
            if(dtYear.Rows.Count != 0 && dtYear.Rows[0][0].ToString() != "")
                for (int x = 0; x < dtYear.Rows.Count; x++)
                {
                    CBYear.Items.Add(dtYear.Rows[x][0].ToString());
                }

            if(CBYear.Items.Count != 0)
            {
                CBYear.Enabled = true;
                CBYear.SelectedIndex = 0;
            }
            else if(dtYear.Rows[0][0].ToString() == "")
            {
                label1.Visible = true;
                CBYear.Enabled = false;
                BSaveCancelDividend.Enabled = false;
            }
            else
            {
                CBYear.Enabled = false;
            }
        }

        private void CancelDividend_SizeChanged(object sender, EventArgs e)
        {
            int x = this.Width / 2 - panel1.Size.Width / 2;
            int y = this.Height / 2 - panel1.Size.Height / 2;
            panel1.Location = new Point(x, y);
        }
    }
}
