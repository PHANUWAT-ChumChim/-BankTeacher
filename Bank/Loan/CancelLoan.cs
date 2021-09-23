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
        int Check = 0;

        /// <summary>
        /// <para>[0] SELECT MemberLona  INPUT: {TeacherNo}</para>
        /// <para>[1] SELECT LOAN INPUT: {TeacherNo} </para>
        /// <para>[2] SELECT Detail Loan INPUT: {LoanNo}</para>
        /// <para>[3] UPDATE Cancel Loan INPUT: {WhereLoanNo}</para>
        /// <para>[4] UPDATE Cancel Guarantor INPUT: {WhereLoanNo}</para>
        /// </summary>
        private String[] SQLDefault =
        {
             //[0] SELECT MemberLona  INPUT: {Text}
          " SELECT TOP(20) TeacherNo , NAME , SavingAmount  \r\n" +
          " FROM(   \r\n " +
          " SELECT a.TeacherNo, CAST(c.PrefixName + ' ' + Fname + ' ' + Lname AS nvarchar)AS NAME,SavingAmount,Fname ,LoanStatusNo \r\n " +
          " FROM EmployeeBank.dbo.tblLoan as a  \r\n " +
          " LEFT JOIN Personal.dbo.tblTeacherHis as b on a.TeacherNo = b.TeacherNo  \r\n " +
          " LEFT JOIN BaseData.dbo.tblPrefix as c on b.PrefixNo = c.PrefixNo  \r\n " +
          " LEFT JOIN EmployeeBank.dbo.tblShare as d on a.TeacherNo = d.TeacherNo  \r\n " +
          " WHERE a.LoanStatusNo != 4 and a.LoanStatusNo != 3 \r\n " +
          " GROUP BY a.TeacherNo,CAST(c.PrefixName+' '+Fname+' '+Lname as NVARCHAR),d.SavingAmount ,Fname , LoanStatusNo) AS A   \r\n " +
          " WHERE a.TeacherNo LIKE '%{Text}%' or Fname LIKE '%{Text}%'  \r\n " +
          " ORDER BY Fname;   "
          ,
          //[1] SELECT LOAN INPUT: {TeacherNo} : 
          "SELECT a.LoanNo , CAST(d.PrefixName + ' ' + Fname + ' ' + Lname AS NVARCHAR) \r\n " +
          " FROM EmployeeBank.dbo.tblLoan as a  \r\n " +
          " LEFT JOIN EmployeeBank.dbo.tblGuarantor as b on a.LoanNo = b.LoanNo  \r\n " +
          " LEFT JOIN Personal.dbo.tblTeacherHis as c on a.TeacherNo = c.TeacherNo \r\n " +
          " LEFT JOIN BaseData.dbo.tblPrefix as d on c.PrefixNo = d.PrefixNo \r\n " +
          " WHERE a.TeacherNo = '{TeacherNo}' and LoanStatusNo != 4  \r\n " +
          " GROUP BY a.LoanNo , CAST(d.PrefixName + ' ' + Fname + ' ' + Lname AS NVARCHAR) \r\n " +
          " ORDER BY a.LoanNo  "

          ,
         //[2] SELECT Detail Loan INPUT: {LoanNo} 
          "SELECT CAST(d.PrefixName + ' ' + Fname + ' ' + Lname AS NVARCHAR) ,DateAdd,a.PayDate \r\n " +
          "FROM EmployeeBank.dbo.tblLoan as a \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblGuarantor as b on a.LoanNo = b.LoanNo \r\n " +
          "LEFT JOIN Personal.dbo.tblTeacherHis as c on b.TeacherNo = c.TeacherNo \r\n " +
          "LEFT JOIN BaseData.dbo.tblPrefix as d on c.PrefixNo = d.PrefixNo \r\n " +
          "WHERE a.LoanNo = '{LoanNo} ' and LoanStatusNo != 4 \r\n " +
          " "
          ,
          //[3] UPDATE Cancel Loan INPUT: {WhereLoanNo}
          "UPDATE EmployeeBank.dbo.tblLoan\r\n" +
          "SET LoanStatusNo = 4\r\n" +
          "WHERE LoanNo = {WhereLoanNo}"
          
          ,
          //[4] UPDATE Cancel Guarantor INPUT: {WhereLoanNo}
          "UPDATE EmployeeBank.dbo.tblGuarantor\r\n" +
          "SET RemainsAmount = 0\r\n" +
          "WHERE LoanNo = {WhereLoanNo}"

        };

        public CancelLoan()
        {
            InitializeComponent();
        }

        private void BSearchTeacher_Click_1(object sender, EventArgs e)
        {
            Bank.Search IN;
            try
            {
                
                IN = new Bank.Search(SQLDefault[0]);
                IN.ShowDialog();
                TBTeacherNo.Text = Bank.Search.Return[0];
                TBTeacherName.Text = Bank.Search.Return[1];
                comboBox1.Enabled = true;
                comboBox1.Items.Clear();
                Check = 1;
                ComboBox[] cb = new ComboBox[] { comboBox1 };
                DataTable dt = Class.SQLConnection.InputSQLMSSQL(SQLDefault[1]
                    .Replace("{TeacherNo}", TBTeacherNo.Text));
                for(int x = 0; x < dt.Rows.Count; x++)
                {
                    for (int aa = 0; aa < cb.Length; aa++)
                    {
                    cb[aa].Items.Add(new example.Class.ComboBoxPayment("รายการกู้ " + (x+1), dt.Rows[x][0].ToString()));
                    }
                }
            }
            catch (Exception x)
            {
                Console.WriteLine(x);
            }
        }


        private void TBTeacherNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (TBTeacherNo.Text.Length == 6)
                {
                    DataTable dt = Class.SQLConnection.InputSQLMSSQL(SQLDefault[1].Replace("{TeacherNo}", TBTeacherNo.Text));
                    if (dt.Rows.Count != 0)
                    {
                        TBTeacherName.Text = dt.Rows[0][1].ToString();
                        comboBox1.Enabled = true;
                        comboBox1.Items.Clear();
                        Check = 1;
                        ComboBox[] cb = new ComboBox[] { comboBox1 };
                        for (int x = 0; x < dt.Rows.Count; x++)
                        {
                            for (int aa = 0; aa < cb.Length; aa++)
                            {
                                cb[aa].Items.Add(new example.Class.ComboBoxPayment("รายการกู้ " + (x + 1), dt.Rows[x][0].ToString()));
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("รหัสผู้ใช้ไม่ถูกต้อง","System",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    }

                }

            }
            else if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
            {
                if (Check == 1)
                {
                    comboBox1.Items.Clear();
                    comboBox1.SelectedIndex = -1;
                    TBTeacherName.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                    textBox5.Text = "";
                    textBox6.Text = "";
                    comboBox1.Enabled = false;
                    Check = 0;
                }
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            example.Class.ComboBoxPayment Loan = (comboBox1.SelectedItem as example.Class.ComboBoxPayment);
            DataTable dt = example.Class.SQLConnection.InputSQLMSSQL(SQLDefault[2].Replace("{LoanNo}",Loan.No));
            if (dt.Rows.Count != 0)
            {
                textBox2.Text = dt.Rows[1][0].ToString();
                textBox3.Text = dt.Rows[2][0].ToString();
                textBox4.Text = dt.Rows[3][0].ToString();
                textBox5.Text = (Convert.ToDateTime(dt.Rows[0][1].ToString())).ToString("dd/MM/yyyy");
                if(dt.Rows[0][2].ToString() != "" || dt.Rows[0][2].ToString() != null)
                {
                    textBox6.Text = "ยังไม่ได้จ่าย";
                }
                else
                {
                    textBox6.Text = "จ่ายแล้ว";
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //เหลือปุ่มจ้า
        }

        private void BCancelSave_Click(object sender, EventArgs e)
        {
            example.Class.ComboBoxPayment Loan = (comboBox1.SelectedItem as example.Class.ComboBoxPayment);
            if (TBTeacherNo.Text != "" && TBTeacherName.Text != "" && textBox6.Text == "ยังไม่ได้จ่าย" && MessageBox.Show("ยืนยันที่จะลบการกู้นี้หรือไม่","ระบบ",MessageBoxButtons.YesNo,MessageBoxIcon.Information) == DialogResult.Yes)
            {
                Class.SQLConnection.InputSQLMSSQL(SQLDefault[3]
                    .Replace("{WhereLoanNo}", Loan.No.ToString()));

                Class.SQLConnection.InputSQLMSSQL(SQLDefault[4]
                    .Replace("{WhereLoanNo}", Loan.No.ToString()));

                TBTeacherNo.Text = "";
                TBTeacherName.Text = "";
                comboBox1.Items.Clear();
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";

            }
        }

        private void CancelLoan_SizeChanged(object sender, EventArgs e)
        {
            Class.FromSettingMedtod.ChangeSizePanal(this, panel1);
        }
    }
}
