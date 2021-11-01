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
    public partial class SQLEditing : Form
    {
        /// <summary> 
        /// SQLDafault 
        /// <para>[0] INPUT: </para> 
        /// </summary> 
        private String[] SQLDefault = new String[]
         { 
          //[] INPUT: 
          "DELETE  \r\n " +
          "FROM EmployeeBank.dbo.tblBill; \r\n " +
          "DELETE  \r\n " +
          "FROM EmployeeBank.dbo.tblBillDetail; \r\n " +
          "DELETE  \r\n " +
          "FROM EmployeeBank.dbo.tblMember; \r\n " +
          "DELETE  \r\n " +
          "FROM EmployeeBank.dbo.tblLoan; \r\n " +
          "DELETE  \r\n " +
          "FROM EmployeeBank.dbo.tblGuarantor; \r\n " +
          "DELETE  \r\n " +
          "FROM EmployeeBank.dbo.tblShare; \r\n " +
          "DELETE  \r\n " +
          "FROM EmployeeBank.dbo.tblShareWithdraw; \r\n " +
          "DELETE  \r\n " +
          "FROM EmployeeBank.dbo.tblMemberResignation; \r\n " +
          " "
          ,

         };
        public SQLEditing()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            example.Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[0]);
            MessageBox.Show("ลบเสร็จเรียบร้อย");
        }

        private void SQLEditing_Load(object sender, EventArgs e)
        {
            while (true)
            {
                if (MessageBox.Show("จะไปจริงหรอ", "เข้าจริงอะ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                {
                    break;
                }
                else
                {
                    this.Close();
                }
            }
                
        }
    }
}
