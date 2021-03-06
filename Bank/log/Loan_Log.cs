using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankTeacher.Bank.log
{
    public partial class Loan_Log : Form
    {
        public Loan_Log()
        {
            InitializeComponent();
        }
        /// <summary> 
        /// SQLDefault 
        /// <para>[0] Get All DGV  INPUT: {TeacherNo}  {DateAddYearMonthDay} </para> 
        /// <para>[1] BSearch TeacherAddBy Just the person who used to AddLoan INPUT: {Text} </para>
        /// <para>[2] Get Date From Database INPUT: - </para>
        /// </summary> 
        private String[] SQLDefault = new String[]
         { 
           //[0] Get All DGV  INPUT: {TeacherNo}  {DateAddYearMonthDay}
           "SELECT d.Name as TeacherAddByName , a.LoanNo , CAST(ISNULL(c.PrefixNameFull , '') + b.Fname + ' ' + b.Lname as NVARCHAR) as Name   \r\n " +
          ", CAST(a.MonthPay as varchar) + '/' + CAST(a.YearPay as varchar) as DateStartPayLoan ,   \r\n " +
          "FORMAT(EOMONTH(CAST('01/' + CAST(a.MonthPay as varchar) + '/' + CAST(a.YearPay as varchar) as date), a.PayNo), 'MM/yyyy') as DateEndPayLoan  \r\n " +
          ", a.InterestRate , a.LoanAmount , e.LoanStatusName \r\n " +
          "FROM EmployeeBank.dbo.tblLoan as a  \r\n " +
          "LEFT JOIN Personal.dbo.tblTeacherHis as b on a.TeacherNo = b.TeacherNo  \r\n " +
          "LEFT JOIN BaseData.dbo.tblPrefix as c on b.PrefixNo = c.PrefixNo \r\n " +
          "LEFT JOIN  \r\n " +
          "(SELECT a.TeacherNoAddBy , CAST(ISNULL(c.PrefixNameFull , '') + b.Fname + ' ' + b.Lname as NVARCHAR) as Name \r\n " +
          "FROM EmployeeBank.dbo.tblLoan as a  \r\n " +
          "LEFT JOIN Personal.dbo.tblTeacherHis as b on a.TeacherNoAddBy = b.TeacherNo  \r\n " +
          "LEFT JOIN BaseData.dbo.tblPrefix as c on b.PrefixNo = c.PrefixNo ) as d on a.TeacherNoAddBy = d.TeacherNoAddBy \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblLoanStatus as e on a.LoanStatusNo = e.LoanStatusNo \r\n " +
          "WHERE a.TeacherNoAddBy LIKE '%{TeacherNo}%' and CAST(a.DateAdd as date) = CAST('{DateAddYearMonthDay}' as date) \r\n " +
          "GROUP BY a.LoanNo , d.Name, c.PrefixNameFull ,b.Fname , b.Lname ,a.MonthPay , a.YearPay , a.InterestRate , a.LoanAmount , a.PayNo, e.LoanStatusName"

           ,

           //[1] BSearch TeacherAddBy Just the person who used to AddLoan INPUT: {Text}
           "SELECT a.TeacherNoAddBy , CAST(ISNULL(c.PrefixNameFull , '') + b.Fname + ' ' + b.Lname as NVARCHAR(255)) , Fname \r\n " +
          "FROM EmployeeBank.dbo.tblLoan as a \r\n " +
          "LEFT JOIN Personal.dbo.tblTeacherHis as b on a.TeacherNoAddBy = b.TeacherNo \r\n " +
          "LEFT JOIN BaseData.dbo.tblPrefix as c on b.PrefixNo = c.PrefixNo \r\n " +
          "WHERE a.TeacherNoAddBy LIKE '%{Text}%' or CAST(ISNULL(c.PrefixNameFull , '') + b.Fname + ' ' + b.Lname as NVARCHAR(255)) LIKE '%{Text}%' \r\n " +
          "GROUP BY a.TeacherNoAddBy , CAST(ISNULL(c.PrefixNameFull , '') + b.Fname + ' ' + b.Lname as NVARCHAR(255)), Fname \r\n " +
          "ORDER BY Fname"
           ,

           //[2] Get Date From Database INPUT: -
           "SELECT CAST(GETDATE() as date)"
           ,

         };

        private void Loan_Log_Load(object sender, EventArgs e)
        {
            RBday.Checked = true;
            DTPSelectDate.Value = Convert.ToDateTime(Class.SQLConnection.InputSQLMSSQL(SQLDefault[2]).Rows[0][0].ToString());
        }

        private void DTPSelectDate_ValueChanged(object sender, EventArgs e)
        {
            if(DTPSelectDate.Value.ToString() != "")
            {
                String DateDTP = DTPSelectDate.Value.ToString("yyyy:MM:dd");
                DateDTP = DateDTP.Replace(":", "/");
                if (RBday.Checked)
                {
                    DGVLoanDay.Rows.Clear();
                    DataTable dtGetDGVDay = Class.SQLConnection.InputSQLMSSQL(SQLDefault[0]
                    .Replace("{TeacherNo}", "")
                    .Replace("{DateAddYearMonthDay}", DateDTP));

                    for(int x = 0; x < dtGetDGVDay.Rows.Count; x++)
                    {
                        DGVLoanDay.Rows.Add(dtGetDGVDay.Rows[x][0].ToString(), dtGetDGVDay.Rows[x][1].ToString(), dtGetDGVDay.Rows[x][2].ToString(), dtGetDGVDay.Rows[x][3].ToString(),
                            dtGetDGVDay.Rows[x][4].ToString(), dtGetDGVDay.Rows[x][5].ToString(), dtGetDGVDay.Rows[x][6].ToString(), dtGetDGVDay.Rows[x][7].ToString());
                    }
                }
                else if(RBday.Checked == false && TBTeacherNo.Text.Length == 6)
                {
                    TBTeacherNo_KeyDown(sender, new KeyEventArgs(Keys.Enter));
                }
                
            }
        }

        private void TBTeacherNo_KeyDown(object sender, KeyEventArgs e)
        {
            if(RBday.Checked == false)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    try
                    {
                        TBTeacherNo.Text = TBTeacherNo.Text.Replace("t", "T");
                        DGVSelectTeacherAdd.Rows.Clear();
                        String Date = DTPSelectDate.Value.ToString("yyyy:MM:dd");
                        Date = Date.Replace(":", "/");
                        DataTable dtDGVTeacherNo = Class.SQLConnection.InputSQLMSSQL(SQLDefault[0]
                            .Replace("{TeacherNo}", TBTeacherNo.Text)
                            .Replace("{DateAddYearMonthDay}", Date));

                        if (dtDGVTeacherNo.Rows.Count != 0)
                        {
                            TBTeacherName.Text = dtDGVTeacherNo.Rows[0][0].ToString();
                            for (int x = 0; x < dtDGVTeacherNo.Rows.Count; x++)
                            {
                                DGVSelectTeacherAdd.Rows.Add(dtDGVTeacherNo.Rows[x][1].ToString(), dtDGVTeacherNo.Rows[x][2].ToString(), dtDGVTeacherNo.Rows[x][3].ToString()
                                    , dtDGVTeacherNo.Rows[x][4].ToString(), dtDGVTeacherNo.Rows[x][5].ToString(), dtDGVTeacherNo.Rows[x][6].ToString(), dtDGVTeacherNo.Rows[x][7].ToString());
                            }
                        }
                        Checkmember(false);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"------------------------{ex}---------------------------");
                        MessageBox.Show("การค้นหาล้มเหลว โปรดลองใหม่อีกครั้ง", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
                {
                    DGVSelectTeacherAdd.Rows.Clear();
                    TBTeacherName.Text = "";
                    Checkmember(true);
                }
            }
        }

        private void RBday_CheckedChanged(object sender, EventArgs e)
        {
            if (RBday.Checked)
            {
                DGVSelectTeacherAdd.Rows.Clear();
                DGVLoanDay.Visible = true;
                DGVSelectTeacherAdd.Visible = false;
                panel2.Enabled = false;
                TBTeacherNo.Text = "";
                TBTeacherName.Text = "";
                if (DTPSelectDate.Value.ToString() != "")
                    DTPSelectDate_ValueChanged(sender, e);
            }
            else
            {
                DGVLoanDay.Rows.Clear();
                DGVLoanDay.Visible = false;
                DGVSelectTeacherAdd.Visible = true;
                panel2.Enabled = true;
            }
        }

        private void Loan_Log_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (RBSelectTeacherAdd.Enabled == true)
                {
                    if (DGVSelectTeacherAdd.Rows.Count != 0)
                    {
                        DGVSelectTeacherAdd.Rows.Clear();
                        Checkmember(true);
                        TBTeacherName.Text = "";
                        TBTeacherNo.Text = "";
                    }
                    else
                    {
                        BankTeacher.Class.FromSettingMedtod.ReturntoHome(this);
                    }
                }
            }
        }
        private void Checkmember(bool tf)
        {
            TBTeacherNo.Enabled = tf;
            BSearchTeacher.Enabled = tf;
        }

        private void BSearchTeacher_Click(object sender, EventArgs e)
        {
            Bank.Search IN;
            IN = new Bank.Search(SQLDefault[1]);
            IN.ShowDialog();
            if (Bank.Search.Return[0].ToString() != "")
            {
                TBTeacherNo.Text = Bank.Search.Return[0];
                TBTeacherNo_KeyDown(sender, new KeyEventArgs(Keys.Enter));
            }
        }
    }
}
