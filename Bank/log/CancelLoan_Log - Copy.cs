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
    public partial class CancelLoan_Log : Form
    {
        bool CheckMember = false;
        List<String> ColumsDGV = new List<string>();
        List<int> SizeColumsDGV = new List<int>();
        List<DataGridViewAutoSizeColumnMode> AutoSizeDGV = new List<DataGridViewAutoSizeColumnMode>();
        String DateSelected = "";

        /// <summary> 
        /// SQLDefault 
        /// <para>[0] Get All TeacherNoCancel or Get TeacherNoCancel individual INPUT: {CancelByTeacherNo}  {DateYearMonthDay}</para> 
        /// <para>[1] Search CancelByTeacher BSearchTeacher Just the person who used to CancelLoan INPUT: {Text} </para>
        /// <para>[2] Get Date From Database INPUT: - </para>
        /// </summary> 
        private String[] SQLDefault = new String[]
         { 
           //[0]Get All TeacherNoCancel or Get TeacherNoCancel individual INPUT: {CancelByTeacherNo}  {DateYearMonthDay}
           "SELECT d.Name as CancelByTeacher , a.LoanNo , CAST(ISNULL(c.PrefixNameFull , '') + b.Fname + ' ' + b.Lname as NVARCHAR) as Name   \r\n " +
          ", CAST(a.MonthPay as varchar) + '/' + CAST(a.YearPay as varchar) as DateStartPayLoan ,   \r\n " +
          "FORMAT(EOMONTH(CAST('01/' + CAST(a.MonthPay as varchar) + '/' + CAST(a.YearPay as varchar) as date), a.PayNo), 'MM/yyyy') as DateEndPayLoan  \r\n " +
          ", a.InterestRate , a.LoanAmount  \r\n " +
          "FROM EmployeeBank.dbo.tblLoan as a  \r\n " +
          "LEFT JOIN Personal.dbo.tblTeacherHis as b on a.TeacherNo = b.TeacherNo  \r\n " +
          "LEFT JOIN BaseData.dbo.tblPrefix as c on b.PrefixNo = c.PrefixNo \r\n " +
          "LEFT JOIN  \r\n " +
          "(SELECT a.CancelByTeacherNo , CAST(ISNULL(c.PrefixNameFull , '') + b.Fname + ' ' + b.Lname as NVARCHAR) as Name \r\n " +
          "FROM EmployeeBank.dbo.tblLoan as a  \r\n " +
          "LEFT JOIN Personal.dbo.tblTeacherHis as b on a.CancelByTeacherNo = b.TeacherNo  \r\n " +
          "LEFT JOIN BaseData.dbo.tblPrefix as c on b.PrefixNo = c.PrefixNo ) as d on a.CancelByTeacherNo = d.CancelByTeacherNo  \r\n " +
          "WHERE a.CancelByTeacherNo LIKE '%{CancelByTeacherNo}%' and a.CancelDate = CAST('{DateYearMonthDay}' as date)\r\n " +
          "GROUP BY a.LoanNo , d.Name, c.PrefixNameFull ,b.Fname , b.Lname ,a.MonthPay , a.YearPay , a.InterestRate , a.LoanAmount , a.PayNo \r\n " 

           ,


           //[1]Search CancelByTeacher BSearchTeacher Just the person who used to CancelLoan INPUT: {Text}
           "SELECT a.CancelByTeacherNo , CAST(ISNULL(c.PrefixNameFull , '') + b.Fname + ' ' + b.Lname as NVARCHAR) , b.Fname\r\n " +
          "FROM EmployeeBank.dbo.tblLoan as a \r\n " +
          "LEFT JOIN Personal.dbo.tblTeacherHis as b on a.CancelByTeacherNo = b.TeacherNo \r\n " +
          "LEFT JOIN BaseData.dbo.tblPrefix as c on b.PrefixNo = c.PrefixNo \r\n " +
          "WHERE a.CancelByTeacherNo LIKE '%{Text}%' or b.Fname LIKE '%{Text}%' \r\n " +
          "GROUP BY a.CancelByTeacherNo , CAST(ISNULL(c.PrefixNameFull , '') + b.Fname + ' ' + b.Lname as NVARCHAR), b.Fname\r\n" +
          "ORDER BY b.Fname;"

           ,

           //[2] Get Date From Database INPUT: -
           "SELECT CAST(GETDATE() as date)"
           ,


         };
        public CancelLoan_Log()
        {
            InitializeComponent();
        }

        private void TBTeacherNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (RBday.Checked == false)
            {
                //

                if (e.KeyCode == Keys.Enter && TBTeacherNo.Text.Length >= 6 && DTPSelectDate.Value.ToString() != "")
                {
                    DGVSelectTeacherAdd.Rows.Clear();
                    String Date = DTPSelectDate.Value.ToString("yyyy:MM:dd");
                    Date = Date.Replace(":", "/");

                    DataTable dtTeacherNoCancel = Class.SQLConnection.InputSQLMSSQL(SQLDefault[0]
                        .Replace("{CancelByTeacherNo}", TBTeacherNo.Text)
                        .Replace("{DateYearMonthDay}", Date));

                    if (dtTeacherNoCancel.Rows.Count != 0)
                    {
                        TBTeacherName.Text = dtTeacherNoCancel.Rows[0][0].ToString();
                        for (int x = 0; x < dtTeacherNoCancel.Rows.Count; x++)
                        {
                            DGVSelectTeacherAdd.Rows.Add(dtTeacherNoCancel.Rows[x][0].ToString(), dtTeacherNoCancel.Rows[x][1].ToString(), dtTeacherNoCancel.Rows[x][2].ToString(), dtTeacherNoCancel.Rows[x][3].ToString()
                                , dtTeacherNoCancel.Rows[x][4].ToString(), dtTeacherNoCancel.Rows[x][5].ToString(), dtTeacherNoCancel.Rows[x][6].ToString());

                        }
                    }
                    else
                    {
                        MessageBox.Show("ไม่พบรายการ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }


                }
                else if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
                {
                    TBTeacherName.Text = "";
                    DGVSelectTeacherAdd.Rows.Clear();
                }
            }
        }

        private void BSearchTeacher_Click(object sender, EventArgs e)
        {
            Bank.Search IN;
            IN = new Bank.Search(SQLDefault[1], "");
            IN.ShowDialog();
            if (Bank.Search.Return[0].ToString() != "")
            {
                TBTeacherNo.Text = Bank.Search.Return[0];
                TBTeacherName.Text = Bank.Search.Return[1];
                TBTeacherNo_KeyDown(sender, new KeyEventArgs(Keys.Enter));
            }
        }
        private void RBday_CheckedChanged(object sender, EventArgs e)
        {
            if (RBday.Checked)
            {
                TBTeacherName.Text = "";
                TBTeacherNo.Text = "";
                DGVSelectTeacherAdd.Rows.Clear();
                DGVSelectTeacherAdd.Visible = false;
                DGVCancelLoan.Visible = true;
                panel2.Enabled = false;
                if (DTPSelectDate.Value.ToString() != "")
                    DTPSelectDate_ValueChanged(sender, new EventArgs());
            }
            else if (RBday.Checked == false)
            {
                DGVCancelLoan.Rows.Clear();
                panel2.Enabled = true;
                DGVCancelLoan.Visible = false;
                DGVSelectTeacherAdd.Visible = true;
                if (DTPSelectDate.Value.ToString() != "")
                    DTPSelectDate_ValueChanged(sender, new EventArgs());
            }
        }

        private void RBSelectTeacherAdd_CheckedChanged(object sender, EventArgs e)
        {

        }

        //private void ClearForm()
        //{
        //    TBTeacherName.Text = "";
        //    TBTeacherNo.Text = "";
        //    DGVCancelLoan.Rows.Clear();
        //}

        private void CancelLoan_Log_Load(object sender, EventArgs e)
        {
            RBday.Checked = true;
            if (DGVCancelLoan.Rows.Count == 0)
                DTPSelectDate.Value = Convert.ToDateTime(Class.SQLConnection.InputSQLMSSQL(SQLDefault[2]).Rows[0][0].ToString());
        }

        private void DTPSelectDate_ValueChanged(object sender, EventArgs e)
        {
            String DTPDate = DTPSelectDate.Value.ToString("yyyy:MM:dd");
            DTPDate = DTPDate.Replace(":", "/");
            if (RBday.Checked == true && DTPDate != "")
            {
                DGVCancelLoan.Rows.Clear();
                DataTable dtRBDay = Class.SQLConnection.InputSQLMSSQL(SQLDefault[0]
                    .Replace("{CancelByTeacherNo}", "")
                    .Replace("{DateYearMonthDay}", DTPDate));

                for (int x = 0; x < dtRBDay.Rows.Count; x++)
                {
                    DGVCancelLoan.Rows.Add(dtRBDay.Rows[x][1].ToString(), dtRBDay.Rows[x][2].ToString(), dtRBDay.Rows[x][3].ToString(), dtRBDay.Rows[x][4].ToString(), dtRBDay.Rows[x][5].ToString(), dtRBDay.Rows[x][6]);
                }
            }
            else if (RBday.Checked == false && TBTeacherNo.Text.Length == 6)
            {
                TBTeacherNo_KeyDown(sender, new KeyEventArgs(Keys.Enter));
            }
        }
    }
}
