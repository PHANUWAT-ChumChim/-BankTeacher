using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace example.Bank
{
    public partial class Search : Form
    {
        public static String[] Return = { "" };
        ///<summary>
        /// <para>[AllTeacher_or_Member]</para> 
        /// <para>ถ้าใส่ 0 จะหาอาจารย์ทั้งหมด</para>
        /// <para>ใส่ 1 จะหาแค่อาจารยฺ์ที่สมัครสมาชิกแล้ว ( สถาณะ ใช้งานเท่านั้น ) Return : รหัสอาจารย์ ชื่อ เลขบัตรปชช.</para>
        /// <para>ใส่ 2 จะหาอาจารย์ที่สมัครสมาชิกแล้ว แต่มีข้อมูลสำหรับการกู้เพิ่มเข้ามา (สถาณะใช้งานเท่านั้น) Return : รหัสอาจารย์ ชื่อ เลขบัตรปชช. สถาณะ เลขที่สัญญากู้ หุ้นสะสม</para>
        ///</summary>

        /// <summary> 
        /// SQLDafault 
        /// <para>[0] SELECT Detail Member INPUT: {TeacherNo} {Name} </para>
        /// </summary> 
        private String[] SQLDefault = new String[]
        {
         //[0] SELECT TBSearch Member INPUT: {TeacherNo} {Name}
          "SELECT TOP(20) a.TeacherNo , CAST(c.PrefixName+' '+[Fname] +' '+ [Lname] as NVARCHAR)AS Name, b.IdNo AS TeacherID,   \r\n " +
          " b.TeacherLicenseNo,b.IdNo AS IDNo,b.TelMobile ,a.StartAmount,CAST(d.MemberStatusName as nvarchar) AS UserStatususing   \r\n " +
          " FROM EmployeeBank.dbo.tblMember as a   \r\n " +
          " LEFT JOIN Personal.dbo.tblTeacherHis as b ON a.TeacherNo = b.TeacherNo   \r\n " +
          " LEFT JOIN BaseData.dbo.tblPrefix as c ON c.PrefixNo = b.PrefixNo  \r\n " +
          " INNER JOIN EmployeeBank.dbo.tblMemberStatus as d on a.MemberStatusNo = d.MemberStatusNo  \r\n " +
          "  WHERE a.TeacherNo LIKE '%T{TeacherNo}%'  or CAST(c.PrefixName+' '+[Fname] +' '+ [Lname] as NVARCHAR) LIKE '%{Name}%'   and a.MemberStatusNo = 1      \r\n " +
          " ORDER BY a.TeacherNo;  "
          ,
        };

        public Search(String SQLCode)
        {
            InitializeComponent();
            Return = new String[] { "" };
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            DataTable dt = Class.SQLConnection.InputSQLMSSQL(SQLCode);
            for (int x = 0; x < dt.Rows.Count; x++)
            {
                dataGridView1.Rows.Add(dt.Rows[x][0], dt.Rows[x][1],dt.Rows[x][2]);

                if (x % 2 == 1)
                {
                    dataGridView1.Rows[x].DefaultCellStyle.BackColor = Color.AliceBlue;
                }
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                Return = new String[]
                {
                        dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString(),
                };

                this.Dispose();

            }
        }

        private void Search_Load(object sender, EventArgs e)
        {

        }

        private void TBTeacherNo_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = Class.SQLConnection.InputSQLMSSQL(SQLDefault[0]
                .Replace("T{TeacherNo}", TBSearch.Text)
                .Replace("{Name}", TBSearch.Text));
            if (dataGridView1.Rows.Count != 0) { dataGridView1.Rows.Clear(); }
            if (TBSearch.Text != "")
            {
                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    dataGridView1.Rows.Add(dt.Rows[x][0], dt.Rows[x][1],"");
                }
            }
            else
            {
                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    dataGridView1.Rows.Add(dt.Rows[x][0], dt.Rows[x][1], "");
                }


            }
        }
    }
}
