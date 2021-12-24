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
    public partial class AddMember_Logg : Form
    {
        public AddMember_Logg()
        {
            InitializeComponent();
        }

        /// <summary> 
        /// SQLDefault 
        /// <para>[0]Get Info RBDay or per person INPUT: {TeacherNoAddBy}  {DateYearMonthDay} </para> 
        /// <para>[1]Get Date From DataBase INPUT: - </para>
        /// <para>[2]BSearch Just TeacherAddBy INPUT: {Text} </para>
        /// </summary> 
        private String[] SQLDefault = new String[]
         { 
           //[0]Get Info RBDay or per person INPUT: {TeacherNoAddBy}  {DateYearMonthDay}
           "SELECT CAST(ISNULL(c.PrefixNameFull , '') + b.Fname + ' ' + b.Lname as NVARCHAR(255)) as TeacherNameAdd , d.TeacherNo , d.TeacherName \r\n " +
          ", CAST(a.DateAdd as date) , a.StartAmount , e.MemberStatusName \r\n " +
          "FROM EmployeeBank.dbo.tblMember as a \r\n " +
          "LEFT JOIN Personal.dbo.tblTeacherHis as b on a.TeacherAddBy = b.TeacherNo \r\n " +
          "LEFT JOIN BaseData.dbo.tblPrefix as c on b.PrefixNo = c.PrefixNo \r\n " +
          "LEFT JOIN (SELECT a.TeacherNo , CAST(ISNULL(c.PrefixNameFull , '') + b.Fname + ' ' + b.Lname as NVARCHAR(255)) as TeacherName \r\n " +
          "FROM EmployeeBank.dbo.tblMember as a \r\n " +
          "LEFT JOIN Personal.dbo.tblTeacherHis as b on a.TeacherNo = b.TeacherNo \r\n " +
          "LEFT JOIN BaseData.dbo.tblPrefix as c on b.PrefixNo = c.PrefixNo) as d on a.TeacherNo = d.TeacherNo \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblMemberStatus as e on a.MemberStatusNo = e.MemberStatusNo \r\n " +
          "WHERE a.TeacherAddBy LIKE '%{TeacherNoAddBy}%' and CAST(a.DateAdd as date) LIKE CAST('{DateYearMonthDay}' as date)"
           ,

           //[1]Get Date From DataBase INPUT: -
           "SELECT CAST(GETDATE() as DATE)"
           ,

           //[2]BSearch Just TeacherAddBy INPUT: {Text}
           "SELECT a.TeacherAddBy , CAST(ISNULL(c.PrefixNameFull , '') + b.Fname + ' ' + b.Lname as NVARCHAR) \r\n " +
          "FROM EmployeeBank.dbo.tblMember as a \r\n " +
          "LEFT JOIN Personal.dbo.tblTeacherHis as b on a.TeacherAddBy = b.TeacherNo \r\n " +
          "LEFT JOIN BaseData.dbo.tblPrefix as c on b.PrefixNo = c.PrefixNo \r\n " +
          "WHERE a.TeacherAddBy LIKE '%{Text}%' or CAST(ISNULL(c.PrefixNameFull , '') + b.Fname + ' ' + b.Lname as NVARCHAR) LIKE '%{Text}%'\r\n " +
          "GROUP BY a.TeacherAddBy, CAST(ISNULL(c.PrefixNameFull , '') + b.Fname + ' ' + b.Lname as NVARCHAR)"
           ,

         };

        List<String[]> ColumnDGVHeaderSize = new List<string[]> { };

        private void RBday_CheckedChanged(object sender, EventArgs e)
        {
            if (RBday.Checked)
            {
                TBTeacherNo.Enabled = false;
                BSearchTeacher.Enabled = false;
                TBTeacherNo.Text = "";
                TBTeacherName.Text = "";
                DGV.Rows.Clear();
                DGV.Columns.Clear();
                for(int x = 0; x < ColumnDGVHeaderSize.Count; x++)
                {
                    DGV.Columns.Add($"Column{x + 1}", ColumnDGVHeaderSize[x][0]);
                    DGV.Columns[x].Width = Convert.ToInt32(ColumnDGVHeaderSize[x][1]);
                }
                if (DTP.Value.ToString() != "")
                    DTP_ValueChanged(sender, new EventArgs());
            }
            else if(!RBday.Checked || RBSelectTeacherAdd.Checked)
            {
                DGV.Rows.Clear();
                DGV.Columns.Clear();
                TBTeacherNo.Text = "";
                TBTeacherName.Text = "";
                for (int x = 1; x < ColumnDGVHeaderSize.Count; x++)
                {
                    DGV.Columns.Add($"Column{x}", ColumnDGVHeaderSize[x][0]);
                    DGV.Columns[x - 1].Width = Convert.ToInt32(ColumnDGVHeaderSize[x][1]);
                }
                TBTeacherNo.Enabled = true;
                BSearchTeacher.Enabled = true;
            }
            else if(!RBday.Checked && !RBSelectTeacherAdd.Checked)
            {
                DGV.Rows.Clear();
                DGV.Columns.Clear();
            }
        }

        private void AddMember_Log_Load(object sender, EventArgs e)
        {
            for(int x = 0; x < DGV.Columns.Count; x++)
            {
                ColumnDGVHeaderSize.Add(new string[] { DGV.Columns[x].HeaderText, DGV.Columns[x].Width.ToString() });
            }
            RBday.Checked = true;
        }

        private void TBTeacherNo_KeyDown(object sender, KeyEventArgs e)
        {
            if(RBSelectTeacherAdd.Checked == true && TBTeacherNo.Text != "" && e.KeyCode == Keys.Enter)
            {
                String Date = DTP.Value.ToString("yyyy:MM:dd");
                Date = Date.Replace(":", "/");
                DGV.Rows.Clear();

                DataTable dt = Class.SQLConnection.InputSQLMSSQL(SQLDefault[0]
                    .Replace("{TeacherNoAddBy}", TBTeacherNo.Text)
                    .Replace("{DateYearMonthDay}", Date));

                if(dt.Rows.Count != 0)
                {
                    TBTeacherName.Text = dt.Rows[0][0].ToString();
                    for(int x = 0; x < dt.Rows.Count; x++)
                    {
                        DGV.Rows.Add(dt.Rows[x][1].ToString(), dt.Rows[x][2].ToString(), dt.Rows[x][3].ToString(), dt.Rows[x][4].ToString(), dt.Rows[x][5].ToString());
                    }
                }
                else
                {
                    MessageBox.Show("ป้อนรหัสผิด หรือบุคคลนี้ไม่เคยทำรายการ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else if(RBSelectTeacherAdd.Checked == true && (e.KeyCode == Keys.Back))
            {
                TBTeacherName.Text = "";
                DGV.Rows.Clear();
            }
        }

        private void AddMember_Logg_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape && (DGV.Rows.Count > 0 || RBday.Checked == true || RBSelectTeacherAdd.Checked == true || TBTeacherNo.Text != "" || TBTeacherName.Text != ""))
            {
                TBTeacherName.Text = "";
                TBTeacherNo.Text = "";
                RBday.Checked = false;
                RBSelectTeacherAdd.Checked = false;
                DGV.Rows.Clear();
            }
        }

        private void DTP_ValueChanged(object sender, EventArgs e)
        {
            if(RBday.Checked == true && DTP.Value.ToString() != "")
            {
                String Date = DTP.Value.ToString("yyyy:MM:dd");
                Date = Date.Replace(":", "/");

                //{TeacherNoAddBy}  {DateYearMonthDay}
                DataTable dt = Class.SQLConnection.InputSQLMSSQL(SQLDefault[0]
                    .Replace("{TeacherNoAddBy}", "")
                    .Replace("{DateYearMonthDay}", Date));

                if(dt.Rows.Count != 0)
                {
                    for(int x = 0; x < dt.Rows.Count; x++)
                    {
                        DGV.Rows.Add(dt.Rows[x][0].ToString(), dt.Rows[x][1].ToString(), dt.Rows[x][2].ToString(), dt.Rows[x][3].ToString(), dt.Rows[x][4].ToString(), dt.Rows[x][5].ToString());
                    }
                }
            }
        }

        private void BSearchTeacher_Click(object sender, EventArgs e)
        {
            Bank.Search IN = new Bank.Search(SQLDefault[2]);
            IN.ShowDialog();
            if (Bank.Search.Return[0].ToString() != "")
            {
                TBTeacherNo.Text = Bank.Search.Return[0];
                TBTeacherNo_KeyDown(sender, new KeyEventArgs(Keys.Enter));
            }
        }

        private void RBSelectTeacherAdd_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void TBTeacherNo_KeyDown(object sender, KeyEventArgs e)
        {
            TBTeacherNo.Text = TBTeacherNo.Text.Replace("t", "T");
        }
    }
}
