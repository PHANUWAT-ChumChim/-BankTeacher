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
    public partial class Dividend_log : Form
    {
        /// <summary> 
        /// SQLDefault 
        /// <para>[0] Select Year INPUT:  </para> 
        /// <para>[0] Select Detail In Year INPUT: {Year}</para>
        /// </summary> 
        private String[] SQLDefault = new String[]
         { 
           //[0] Select Year INPUT: 
           "SELECT Year \r\n " +
          "FROM EmployeeBank.dbo.tblDividend \r\n " +
          "GROUP BY Year \r\n " +
          "ORDER BY Min(Year)"
           ,
           //[0] Select Detail In Year INPUT: {Year}
           "SELECT a.TeacherNo , CAST(ISNULL(b.PrefixName,'')+a.Fname + ' '+ Lname as nvarchar(255)),CAST(c.DateAdd as date) , Cancel \r\n " +
          "FROM Personal.dbo.tblTeacherHis as a \r\n " +
          "LEFT JOIN BaseData.dbo.tblPrefix as b on a.PrefixNo = b.PrefixNo \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblDividend as c on a.TeacherNo = c.TeacherAddby \r\n " +
          "WHERE c.Year = 2022 \r\n " +
          "ORDER BY c.DateAdd"
           ,

         };
        public Dividend_log()
        {
            InitializeComponent();
            DataTable dt = BankTeacher.Class.SQLConnection.InputSQLMSSQL(SQLDefault[0]);
            if(dt.Rows.Count != 0)
            {
                for(int x  = 0; x < dt.Rows.Count; x++)
                {
                    comboBox1.Items.Add(dt.Rows[x][0].ToString());
                }
                comboBox1.SelectedIndex = comboBox1.Items.Count - 1;
            }
            else
            {
                comboBox1.Enabled = false;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            if (comboBox1.SelectedIndex != -1)
            {
                if(comboBox1.Text != "")
                {
                    DataTable dt = BankTeacher.Class.SQLConnection.InputSQLMSSQL(SQLDefault[1]
                        .Replace("{Year}", comboBox1.Items[comboBox1.SelectedIndex].ToString()));
                    if(dt.Rows.Count != 0)
                    {
                        for(int x = 0; x < dt.Rows.Count; x++)
                        {
                            String Status = "ยกเลิก";
                            if (dt.Rows[x][3].ToString() == "1")
                                Status = "ใช้งาน";
                            dataGridView1.Rows.Add(dt.Rows[x][0].ToString(),dt.Rows[x][1].ToString(),dt.Rows[x][2].ToString(),Status);
                        }
                    }
                }
            }
        }
    }
}