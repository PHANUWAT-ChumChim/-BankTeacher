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
    public partial class UploadFile_Log : Form
    {
        List<String[]> ColumnDGVandSize = new List<string[]>();
        //String DateSelected = "";
        public UploadFile_Log()
        {
            InitializeComponent();
        }

        /// <summary> 
        /// SQLDefault 
        /// <para>[0] Get Info DGV INPUT: {TeacherNoAddBy}  {DateYearMonthDay} INPUT: </para> 
        /// <para>[1] GetDate INPUT: - </para>
        /// <para>[2] BSearch TecherAddBy INPUT: {Text} </para>
        /// </summary> 
        private String[] SQLDefault = new String[]
         { 
           //[0] Get Info DGV INPUT: {TeacherNoAddBy}  {DateYearMonthDay}
           "SELECT CAST(ISNULL(c.PrefixNameFull , '') + b.Fname + ' ' + b.Lname as NVARCHAR(255)) , d.Name , CAST(e.FileTypeName as NVARCHAR) as TypeName, ISNULL(CAST(a.LoanID as varchar) , '-') as LoanNo \r\n " +
          ", a.pathFile , a.DateAddFile \r\n " +
          "FROM EmployeeBank.dbo.tblFile as a \r\n " +
          "LEFT JOIN Personal.dbo.tblTeacherHis as b on a.TeacherAddBy = b.TeacherNo \r\n " +
          "LEFT JOIN BaseData.dbo.tblPrefix as c on b.PrefixNo = c.PrefixNo \r\n " +
          "LEFT JOIN (SELECT a.ID ,a.TeacherNo , CAST(ISNULL(c.PrefixNameFull , '') + b.Fname + ' ' + b.Lname as NVARCHAR(255)) as Name \r\n " +
          "FROM EmployeeBank.dbo.tblFile as a \r\n " +
          "LEFT JOIN Personal.dbo.tblTeacherHis as b on a.TeacherNo = b.TeacherNo \r\n " +
          "LEFT JOIN BaseData.dbo.tblPrefix as c on b.PrefixNo = c.PrefixNo) as d on a.ID = d.ID \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblFileType as e on a.FiletypeNo = e.FileTypeID \r\n " +
          "WHERE a.TeacherAddBy LIKE '%{TeacherNoAddBy}%' and CAST(a.DateAddFile as date) LIKE CAST('{DateYearMonthDay}' as date)"
           ,

           //[1] GetDate INPUT: -
           "SELECT CAST(GETDATE() as date);"
           ,

           //[2] BSearch TecherAddBy INPUT: {Text}
           "SELECT a.TeacherAddBy ,CAST(ISNULL(c.PrefixNameFull , '') + b.Fname + ' ' + b.Lname as NVARCHAR(255)) \r\n " +
          "FROM EmployeeBank.dbo.tblFile as a \r\n " +
          "LEFT JOIN Personal.dbo.tblTeacherHis as b on a.TeacherAddBy = b.TeacherNo \r\n " +
          "LEFT JOIN BaseData.dbo.tblPrefix as c on b.PrefixNo = c.PrefixNo \r\n " +
          "WHERE a.TeacherAddBy LIKE '%{Text}%' or CAST(ISNULL(c.PrefixNameFull , '') + b.Fname + ' ' + b.Lname as NVARCHAR(255)) LIKE '%{Text}%' \r\n " +
          "GROUP BY a.TeacherAddBy ,CAST(ISNULL(c.PrefixNameFull , '') + b.Fname + ' ' + b.Lname as NVARCHAR(255));"
           ,

         };

        private void UploadFile_Log_Load(object sender, EventArgs e)
        {
            for (int x = 0; x < DGV.Columns.Count; x++)
            {
                ColumnDGVandSize.Add(new string[] {DGV.Columns[x].HeaderText , DGV.Columns[x].Width.ToString() });
            }
            //DTP_ValueChanged(new object(), new EventArgs());
            //RBday_CheckedChanged(new object(), new EventArgs());
        }

        private void RBday_CheckedChanged(object sender, EventArgs e)
        {
            if (RBday.Checked)
            {
                panel2.Enabled = false;
                TBTeacherName.Text = "-";
                TBTeacherNo.Text = "-";
                DGV.Rows.Clear();
                DGV.Columns.Clear();
                for(int Col = 0; Col < ColumnDGVandSize.Count; Col++)
                {
                    DGV.Columns.Add($"Column{Col}", ColumnDGVandSize[Col][0]);
                    DGV.Columns[Col].Width = Convert.ToInt32(ColumnDGVandSize[Col][1]);
                }
                String Date = DTP.Value.ToString("yyyy:MM:dd");
                Date = Date.Replace(":", "/");

                DataTable dt = Class.SQLConnection.InputSQLMSSQL(SQLDefault[0]
                            .Replace("{TeacherAddbyNo}", "")
                            .Replace("{Date}", Date)); 
                if (dt.Rows.Count != 0)
                {
                    for(int Rowdt = 0; Rowdt < dt.Rows.Count; Rowdt++)
                    {
                        DGV.Rows.Add(dt.Rows[Rowdt][0], dt.Rows[Rowdt][1], dt.Rows[Rowdt][2], dt.Rows[Rowdt][3], dt.Rows[Rowdt][4], dt.Rows[Rowdt][5]);
                    }
                }
            }
        }

        private void RBSelectTeacherAdd_CheckedChanged(object sender, EventArgs e)
        {
            if (RBSelectTeacherAdd.Checked)
            {
                panel2.Enabled = true;
                TBTeacherName.Enabled = false;
                TBTeacherName.Text = "";
                TBTeacherNo.Text = "";
                DGV.Rows.Clear();
                DGV.Columns.Clear();
                for(int Col = 1; Col < ColumnDGVandSize.Count; Col++)
                {
                    DGV.Columns.Add($"Column{Col}", ColumnDGVandSize[Col][0]);
                    DGV.Columns[0].Width = Convert.ToInt32(ColumnDGVandSize[Col][1]);
                }
            }
        }

        private void TBTeacherNo_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                DGV.Rows.Clear();
                TBTeacherNo.Text = TBTeacherNo.Text.Replace("t", "T");
                String Date = Convert.ToString(DTP.Value.ToString("yyyy:MM:dd"));
                Date = Date.Replace(":", "/");
                DataTable dt = Class.SQLConnection.InputSQLMSSQL(SQLDefault[0]
                    .Replace("{TeacherNoAddBy}", TBTeacherNo.Text)
                    .Replace("{DateYearMonthDay}", Date));
                if(dt.Rows.Count != 0)
                {
                    for(int Rowdt = 0; Rowdt < dt.Rows.Count; Rowdt++)
                    {
                        DGV.Rows.Add(dt.Rows[Rowdt][1], dt.Rows[Rowdt][2], dt.Rows[Rowdt][3], dt.Rows[Rowdt][4], dt.Rows[Rowdt][5]);
                    }
                }
                else
                {
                    MessageBox.Show("ไม่พบข้อมูล หรือ รหัสผู้ใช้ไม่ถูกต้อง", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void DTP_ValueChanged(object sender, EventArgs e)
        {
            if (RBday.Checked)
            {
                RBday_CheckedChanged(sender, new EventArgs());
            }
            else if(RBSelectTeacherAdd.Checked && TBTeacherNo.Text != "")
            {
                TBTeacherNo_KeyDown(sender, new KeyEventArgs(Keys.Enter));
            }
        }

        private void BSearchTeacher_Click(object sender, EventArgs e)
        {
            String Date = Convert.ToString(DTP.Value.ToString("yyyy:MM:dd"));
            Date = Date.Replace(":", "/");
            Bank.Search IN = new Bank.Search(SQLDefault[0]
                    .Replace("{Date}", Date), "");
            IN.ShowDialog();
            if (Bank.Search.Return[0] != "")
            {
                TBTeacherNo.Text = Bank.Search.Return[0];
                TBTeacherName.Text = Bank.Search.Return[1];
                TBTeacherNo_KeyDown(new object(), new KeyEventArgs(Keys.Enter));
            }
        }
    }
}
