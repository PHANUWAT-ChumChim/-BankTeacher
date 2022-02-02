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
    public partial class CancelDividend_Log : Form
    {
        public CancelDividend_Log()
        {
            InitializeComponent();
        }
        
        /// <summary> 
        /// SQLDefault 
        /// <para>[0] Get Year List INPUT: - INPUT: </para> 
        /// <para>[1] Search TeacherCancelBy Per Person or Per Year INPUT: {Year}  {TeacherNo}</para>
        /// <para>[2] Search TeacherCanCelBy INPUT: - </para>
        /// </summary> 
        private String[] SQLDefault = new String[]
         { 
           //[0] Get Year List INPUT: -
           "SELECT TOP 5 YEAR(a.DateCancel) \r\n " +
          "FROM EmployeeBank.dbo.tblDividend as a \r\n " +
          "WHERE a.Cancel = 2 \r\n " +
          "GROUP BY a.DateCancel \r\n " +
          "ORDER BY a.DateCancel DESC;"
           ,

           //[1] Search TeacherCancelBy Per Person or Per Year INPUT: {Year}  {TeacherNo}
           "SELECT a.CancelBy , CAST(ISNULL(c.PrefixNameFull , '') + b.Fname + ' ' + b.Lname as NVARCHAR(255)) , a.DateCancel , a.Interest , a.AverageDividend \r\n " +
          "FROM EmployeeBank.dbo.tblDividend as a \r\n " +
          "LEFT JOIN Personal.dbo.tblTeacherHis as b on a.CancelBy = b.TeacherNo \r\n " +
          "LEFT JOIN BaseData.dbo.tblPrefix as c on b.PrefixNo = c.PrefixNo \r\n " +
          "WHERE a.Cancel = 2 and YEAR(a.DateCancel) = {Year} and a.CancelBy LIKE '%{TeacherNo}%';"
           ,

           //[2] Search TeacherCancelBy INPUT: {Year}
           "SELECT a.CancelBy , CAST(ISNULL(c.PrefixNameFull , '') + b.Fname + ' ' + b.Lname as NVARCHAR(255)) \r\n " +
          "FROM EmployeeBank.dbo.tblDividend as a \r\n " +
          "LEFT JOIN Personal.dbo.tblTeacherHis as b on a.CancelBy = b.TeacherNo \r\n " +
          "LEFT JOIN BaseData.dbo.tblPrefix as c on b.PrefixNo = c.PrefixNo \r\n " +
          "WHERE a.CancelBy LIKE '%{Text}%' or CAST(ISNULL(c.PrefixNameFull , '') + b.Fname + ' ' + b.Lname as NVARCHAR) LIKE '%{Text}%'  and YEAR(a.DateCancel) = {Year}\r\n " +
          "GROUP BY a.CancelBy , CAST(ISNULL(c.PrefixNameFull , '') + b.Fname + ' ' + b.Lname as NVARCHAR(255));"
           ,


         };

        List<String> ColumsDGV = new List<string>();
        List<int> SizeColumsDGV = new List<int>();
        private void CancelDividend_Log_Load(object sender, EventArgs e)
        {
            DataTable dtYearList = Class.SQLConnection.InputSQLMSSQL(SQLDefault[0]);

            for(int x = 0; x < DGV.Columns.Count; x++)
            {
                ColumsDGV.Add(DGV.Columns[x].HeaderText);
                SizeColumsDGV.Add(DGV.Columns[x].Width);
            }

            for(int a = 0; a < dtYearList.Rows.Count; a++)
            {
                CBYear.Items.Add(dtYearList.Rows[a][0].ToString());
            }

            if(dtYearList.Rows.Count != 0)
            {
                RBYear.Checked = true;
                CBYear.Enabled = true;
                CBYear.SelectedIndex = 0;
            }
        }

        private void CBYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RBYear.Checked)
            {
                CBYear.DroppedDown = false;
                DGV.Rows.Clear();
                DGV.Columns.Clear();

                for (int a = 0; a < ColumsDGV.Count; a++)
                {
                    DGV.Columns.Add($"Col{a}", ColumsDGV[a]);
                    DGV.Columns[a].Width = SizeColumsDGV[a];
                }

                DataTable dtDGVYear = Class.SQLConnection.InputSQLMSSQL(SQLDefault[1]
                    .Replace("{Year}", CBYear.SelectedItem.ToString())
                    .Replace("{TeacherNo}", ""));

                if (dtDGVYear.Rows.Count != 0)
                {
                    for(int x = 0; x < dtDGVYear.Rows.Count; x++)
                    {
                        DGV.Rows.Add(dtDGVYear.Rows[x][0], dtDGVYear.Rows[x][1], dtDGVYear.Rows[x][2], dtDGVYear.Rows[x][3], dtDGVYear.Rows[x][4]);
                    }
                }
            }
            else if (RBSelectTeacherAdd.Checked && TBTeacherNo.Text != "")
            {
                CBYear.DroppedDown = false;
                DGV.Rows.Clear();
                DGV.Columns.Clear();

                for (int a = 1; a < ColumsDGV.Count; a++)
                {
                    DGV.Columns.Add($"Col{a}", ColumsDGV[a]);
                    DGV.Columns[a].Width = SizeColumsDGV[a];
                }

                DataTable dtSelectTeacher = Class.SQLConnection.InputSQLMSSQL(SQLDefault[1]
                    .Replace("{Year}", CBYear.SelectedItem.ToString())
                    .Replace("{TeacherNo}", TBTeacherNo.Text));

                if(dtSelectTeacher.Rows.Count != 0)
                {
                    for(int a = 0; a < dtSelectTeacher.Rows.Count; a++)
                    {
                        DGV.Rows.Add(dtSelectTeacher.Rows[a][1], dtSelectTeacher.Rows[a][2], dtSelectTeacher.Rows[a][3], dtSelectTeacher.Rows[a][4]);
                    }
                }
            }
        }

        private void RBYear_CheckedChanged(object sender, EventArgs e)
        {
            if(RBYear.Checked && CBYear.SelectedIndex != -1 && !RBSelectTeacherAdd.Checked)
            {
                panel2.Enabled = false;
                TBTeacherNo.Text = "";
                TBTeacherName.Text = "";
                CBYear.SelectedIndex = CBYear.SelectedIndex;
            }
        }

        private void RBSelectTeacherAdd_CheckedChanged(object sender, EventArgs e)
        {
            if(RBSelectTeacherAdd.Checked && !RBYear.Checked)
            {
                panel2.Enabled = true;
            }
        }

        private void TBTeacherNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CBYear.SelectedIndex = CBYear.SelectedIndex;
            }
            else if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
            {
                DGV.Rows.Clear();
                TBTeacherName.Text = "";
            }
        }

        private void BSearchTeacher_Click(object sender, EventArgs e)
        {
            Bank.Search IN = new Bank.Search(SQLDefault[2]
                    .Replace("{Year}", CBYear.SelectedItem.ToString()), "");
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
