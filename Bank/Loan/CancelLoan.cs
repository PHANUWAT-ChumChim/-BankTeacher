using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace example.Bank.Loan
{
    public partial class CancelLoan : Form
    {
        /// <summary>
        /// <para>[0] SELECT MemberLona  INPUT: {TeacherNo}</para>
        /// </summary>
        private String[] SQLDefault =
        {
             //[0] SELECT MemberLona  INPUT: {TeacherNo}
          "SELECT TOP(20) TeacherNo , NAME , StartAmount \r\n " +
          "FROM( \r\n " +
          "SELECT a.TeacherNo,CAST(c.PrefixName+''+b.Fname+''+b.Lname as NVARCHAR)AS NAME,d.StartAmount ,b.Fname \r\n " +
          "FROM EmployeeBank.dbo.tblLoan as a  \r\n " +
          "LEFT JOIN Personal.dbo.tblTeacherHis as b on a.TeacherNo = b.TeacherNo  \r\n " +
          "LEFT JOIN BaseData.dbo.tblPrefix as c on b.PrefixNo = c.PrefixNo  \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblMember as d on a.TeacherNo = d.TeacherNo \r\n " +
          "WHERE a.TeacherNo LIKE 'T{TeacherNo}%' and a.LoanStatusNo != 4 \r\n " +
          "GROUP BY a.TeacherNo,CAST(c.PrefixName+''+b.Fname+''+b.Lname as NVARCHAR),d.StartAmount ,Fname) AS A \r\n " +
          "ORDER BY Fname; \r\n " +
          " "
          ,

        };

        public CancelLoan()
        {
            InitializeComponent();
        }

        private void CancelLoan_Load(object sender, EventArgs e)
        {
            Class.FromSettingMedtod.ChangeSizePanal(this, panel1);
        }

        private void BSearchTeacher_Click(object sender, EventArgs e)
        {
            Bank.Search IN;
            try
            {
                IN = new Bank.Search(SQLDefault[0]
                     .Replace("{TeacherNo}", ""));
                IN.ShowDialog();
                TBTeacherNo.Text = Bank.Search.Return[0];
                TBTeacherName.Text = Bank.Search.Return[1];
                comboBox1.Enabled = true;
            }
            catch (Exception x)
            {
                Console.WriteLine(x);
            }
        }

        private void BSaveCancel_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
