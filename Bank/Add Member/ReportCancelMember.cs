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
    public partial class ReportCancelMember : Form
    {
        int Check = 0;
        /// <summary> 
        /// SQLDafault 
        /// <para>[0] INPUT: </para> 
        /// </summary> 
        private String[] SQLDefault = new String[]
         { 
          //[] INPUT: 
          "SELECT TOP(20) a.TeacherNo , CAST(c.PrefixName+' '+[Fname] +' '+ [Lname] as NVARCHAR)AS Name, e.SavingAmount,    \r\n " +
          "b.TeacherLicenseNo,b.IdNo AS IDNo,b.TelMobile ,a.StartAmount,CAST(d.MemberStatusName as nvarchar) AS UserStatususing     \r\n " +
          "FROM EmployeeBank.dbo.tblMember as a     \r\n " +
          "LEFT JOIN Personal.dbo.tblTeacherHis as b ON a.TeacherNo = b.TeacherNo    \r\n " +
          "LEFT JOIN BaseData.dbo.tblPrefix as c ON c.PrefixNo = b.PrefixNo    \r\n " +
          "INNER JOIN EmployeeBank.dbo.tblMemberStatus as d on a.MemberStatusNo = d.MemberStatusNo   \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblShare as e on a.TeacherNo = e.TeacherNo \r\n " +
          "WHERE a.TeacherNo LIKE '%{Text}%'  or CAST(c.PrefixName+' '+[Fname] +' '+ [Lname] as NVARCHAR) LIKE '%{Text}%'   and a.MemberStatusNo = 2    \r\n " +
          "GROUP BY a.TeacherNo , CAST(c.PrefixName+' '+[Fname] +' '+ [Lname] as NVARCHAR), e.SavingAmount,    \r\n " +
          "b.TeacherLicenseNo,b.IdNo ,b.TelMobile ,a.StartAmount,CAST(d.MemberStatusName as nvarchar)    \r\n " +
          "ORDER BY a.TeacherNo;  "
          ,

         };
        public ReportCancelMember()
        {
            InitializeComponent();
            
        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ReportCancelMember_SizeChanged(object sender, EventArgs e)
        {
            Class.FromSettingMedtod.ChangeSizePanal(this, panel1);
        }

        private void ReportCancelMember_Load(object sender, EventArgs e)
        {

        }

        private void BSearchTeacher_Click(object sender, EventArgs e)
        {
            try
            {
                Bank.Search IN = new Bank.Search(SQLDefault[1]);
                IN.ShowDialog();
                TBTeacherNo.Text = Bank.Search.Return[0];
            }
            catch (Exception x)
            {
                Console.WriteLine(x);
            }
        }

        private void TBTeacherNo_TextChanged(object sender, EventArgs e)
        {
            //if (TBTeacherNo.Text.Length == 6)
            //{
            //    Class.SQLMethod.ResearchUserAllTLC(TBTeacherNo.Text, TBTeacherName, TBTeacherBill,2);
            //}
            //else
            //{
            //    TBTeacherBill.Text = "";
            //    TBTeacherName.Text = "";
            //}
        }

        private void TBTeacherNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (TBTeacherNo.Text.Length == 6)
                {
                    try
                    {
                        DataSet ds = Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[0].Replace("{Text}", TBTeacherNo.Text));
                        TBTeacherName.Text = ds.Tables[0].Rows[0][1].ToString();
                        TBTeacherBill.Text = "รอใส่จ้าาา";
                        Check = 1;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
            }
            else if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back && Check == 1)
            {
                TBTeacherBill.Text = "";
                TBTeacherName.Text = "";
                Check = 0;
            }
        }
    }
}
