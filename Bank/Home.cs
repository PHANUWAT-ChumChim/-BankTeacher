using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace example.GOODS
{
    public partial class Home : Form
    {
        public static Font F = new Font("TH Sarabun New",16,FontStyle.Regular);
        int Check = 0;

        /// <summary> 
        /// SQLDafault 
        /// <para>[0] SELECT Teachar IN Mont INPUT: {TeacherNo} </para> 
        /// <para>[1] SELECT not pay IN Mont INPUT: {TeacherNo} {CByear} {CBMonth} </para> 
        /// <para>[2] SELECT TIME INPUT : - </para>
        /// <para>[3] SELECT MEMBER INPUT: {TeacherNo} </para>
        /// <para>[4] SELECT pay IN Mont INPUT:  {TeacherNo} {CByear} {CBMonth} </para>
        /// </summary> 
        private String[] SQLDefault = new String[]
        {
          //[0] SELECT Teachar IN Mont INPUT: {TeacherNo}
            "SELECT a.TeacherNo , CAST(c.PrefixName+' '+Fname +' '+ Lname as NVARCHAR),f.TypeName,a.StartAmount,a.DateAdd \r\n" +
            "FROM EmployeeBank.dbo.tblMember as a \r\n" +
            "LEFT JOIN Personal.dbo.tblTeacherHis as b ON a.TeacherNo = b.TeacherNo \r\n" +
            "LEFT JOIN BaseData.dbo.tblPrefix as c ON b.PrefixNo = c.PrefixNo \r\n" +
            "LEFT JOIN EmployeeBank.dbo.tblBill as d ON a.TeacherNo = d.TeacherNo \r\n" +
            "LEFT JOIN EmployeeBank.dbo.tblBillDetail as e ON d.BillNo = e.BillNo \r\n" +
            "LEFT JOIN EmployeeBank.dbo.tblBillDetailType as f ON e.TypeNo = f.TypeNo \r\n" +
            "WHERE a.TeacherNo LIKE 'T%' AND e.Mount  IS NULL  AND e.Year IS NULL AND a.MemberStatusNo = 1 AND DATEPART(mm,a.DateAdd) = DATEPART(mm,GETDATE()); "
          ,
          //[1] SELECT not pay IN Mont INPUT: {TeacherNo} {CByear} {CBMonth}
            "SELECT a.TeacherNo,CAST(e.PrefixName+' '+Fname +' '+ Lname as NVARCHAR) as fname,f.TypeName,a.StartAmount \r\n"+
            "FROM EmployeeBank.dbo.tblMember as a \r\n"+
            "LEFT JOIN EmployeeBank.dbo.tblBill as b on a.TeacherNo = b.TeacherNo \r\n"+
            "LEFT JOIN EmployeeBank.dbo.tblBillDetail as c on b.BillNo = c.BillNo \r\n"+
            "LEFT JOIN Personal.dbo.tblTeacherHis as d on a.TeacherNo = d.TeacherNo \r\n"+
            "LEFT JOIN BaseData.dbo.tblPrefix as e on d.PrefixNo = e.PrefixNo \r\n"+
            "LEFT JOIN EmployeeBank.dbo.tblBillDetailType as f on c.TypeNo = f.TypeNo \r\n"+
            "WHERE a.TeacherNo LIKE 'T{TeacherNo}%' AND DATEADD(YYYY,0,'{CByear}-{CBMonth}-10') >= a.DateAdd AND a.TeacherNo NOT IN  \r\n"+
            "(SELECT aa.TeacherNo FROM EmployeeBank.dbo.tblBill as aa \r\n"+
            "LEFT JOIN EmployeeBank.dbo.tblBillDetail as bb on aa.BillNo = bb.BillNo \r\n"+
            "WHERE  bb.Mount = {CBMonth} and bb.Year = {CByear}) \r\n"+
            "GROUP BY a.TeacherNo,CAST(e.PrefixName+' '+Fname +' '+ Lname as NVARCHAR) ,f.TypeName,a.StartAmount \r\n"+
            "ORDER BY Fname;"
          ,
          //[2] SELECT TIME INPUT : -
          "SELECT CONVERT (DATE , CURRENT_TIMESTAMP); "
          ,
          //[3] SELECT MEMBER INPUT: {TeacherNo} 
          "SELECT a.TeacherNo ,  CAST(c.PrefixName+' '+Fname +' '+ Lname as NVARCHAR),a.StartAmount,a.DateAdd \r\n"+
          "FROM EmployeeBank.dbo.tblMember as a \r\n"+
          "LEFT JOIN Personal.dbo.tblTeacherHis as b ON a.TeacherNo = b.TeacherNo \r\n"+
          "LEFT JOIN BaseData.dbo.tblPrefix as c ON c.PrefixNo = b.PrefixNo \r\n"+
          "WHERE a.TeacherNo LIKE 'T{TeacherNo}%' and MemberStatusNo = 1 \r\n"+
          "ORDER BY Fname;"
          ,
          //[4] SELECT pay IN Mont INPUT: {TeacherNo} {CByear} {CBMonth}
          "SELECT a.TeacherNo , CAST(c.PrefixName + ' ' +[Fname] + ' ' + [Lname] as NVARCHAR)AS Name,f.TypeName,a.StartAmount \r\n" +
          "FROM EmployeeBank.dbo.tblMember as a \r\n" +
          "LEFT JOIN Personal.dbo.tblTeacherHis as b ON a.TeacherNo = b.TeacherNo \r\n" +
          "LEFT JOIN BaseData.dbo.tblPrefix as c ON c.PrefixNo = b.PrefixNo \r\n" +
          "LEFT JOIN EmployeeBank.dbo.tblBill as d ON b.TeacherNo = d.TeacherNo \r\n" +
          "LEFT JOIN EmployeeBank.dbo.tblBillDetail as e ON d.BillNo = e.BillNo \r\n" +
          "LEFT JOIN EmployeeBank.dbo.tblBillDetailType as f ON e.TypeNo = f.TypeNo \r\n" +
          "WHERE a.TeacherNo LIKE 'T{TeacherNo}%' AND e.Mount = {CBMonth} AND e.Year = {CByear} \r\n" +
          "ORDER BY a.TeacherNo;"

        };

        public Home()
        {
            InitializeComponent();
        }
     

        private void Form2_SizeChanged(object sender, EventArgs e)
        {
            Class.FromSettingMedtod.ChangeSizePanal(this, P1);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        { 
        }

        private void BSearchTeacher_Click(object sender, EventArgs e)
        {
            try
            {
                Bank.Search IN = new Bank.Search(SQLDefault[3].Replace("{TeacherNo}",""));
                IN.ShowDialog();
                TBTeacherNo.Text = Bank.Search.Return[0];
                TBTeacherNo_KeyDown(sender, new KeyEventArgs(Keys.Enter));
            }
            catch(Exception x)
            {
                Console.WriteLine( x );
            }

        }

        private void TBTeacherNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (TBTeacherNo.Text.Length == 6)
                {
                    DataTable dt = Class.SQLConnection.InputSQLMSSQL(SQLDefault[3].Replace("T{TeacherNo}%", TBTeacherNo.Text));
                    if (dt.Rows.Count != 0)
                    {
                        TBTeacherName.Text = dt.Rows[0][1].ToString();
                        TBTeacherBill.Text = dt.Rows[0][3].ToString().Remove(8,8);
                        Check = 1;
                    }
                    else
                    {
                        MessageBox.Show("รหัสไม่ถูกต้อง", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                }
            }
            else if (e.KeyCode == Keys.Delete)
            {
                if (Check == 1)
                {
                    TBTeacherName.Text = "";
                    TBTeacherBill.Text = "";
                    Check = 0;
                }

            }

        }
        private void Home_Load(object sender, EventArgs e)
        {

            int Year = Convert.ToInt32(example.GOODS.Menu.Date[0]);
            int Y = Year;
            for (int x = 0; x < 4; x++)
            {
                CByear.Items.Add(Year);
                Year--; 
            }
            DataTable dt = Class.SQLConnection.InputSQLMSSQL(SQLDefault[1]
                .Replace("{TeacherNo}","")
                .Replace("{CByear}",Y.ToString())
                .Replace("{CBMonth}", example.GOODS.Menu.Date[1]));
            for (int num = 0; num < dt.Rows.Count; num++)
            {
                dataGridView3.Rows.Add(dt.Rows[num][0], dt.Rows[num][1], "สะสม", dt.Rows[num][3]);
            }
        }

        private void automatic_Click(object sender, EventArgs e)
        {
            dataGridView3.Rows.Clear();
            int O = 0; 
            if (CBStatus.SelectedIndex == 0)
            {
                O = 4;
            }
            else if(CBStatus.SelectedIndex == 1) { O = 1; }
            else { O = 0; }
            if (TBTeacherName.Text == "")
            {
                DataTable dt = Class.SQLConnection.InputSQLMSSQL(SQLDefault[O]
                    .Replace("{TeacherNo}", "")
                    .Replace("{CBMonth}", CBMonth.Text)
                    .Replace("{CByear}", CByear.Text));
                if (O == 4 && dt.Rows.Count != 0)
                {
                    if (dt.Rows.Count != 0)
                    {
                        for (int num = 0; num < dt.Rows.Count; num++)
                        {
                            dataGridView3.Rows.Add(dt.Rows[num][0], dt.Rows[num][1], "สะสม", dt.Rows[num][3]);
                        }
                    }
                }
                else if (O == 1 && dt.Rows.Count != 0)
                {
                    if (dt.Rows.Count != 0)
                    {
                        for (int num = 0; num < dt.Rows.Count; num++)
                        {
                            dataGridView3.Rows.Add(dt.Rows[num][0], dt.Rows[num][1], "สะสม", dt.Rows[num][3]);
                        }
                    }
                }
                else { MessageBox.Show($"ไม่พบรายการ", "การแจ้งเตือนการค้นหา", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }
            else
            {
                DataTable dt = Class.SQLConnection.InputSQLMSSQL(SQLDefault[O]
                   .Replace("T{TeacherNo}%",TBTeacherNo.Text)
                   .Replace("{CBMonth}", CBMonth.Text)
                   .Replace("{CByear}", CByear.Text));
                if (O == 4 && dt.Rows.Count != 0)
                {
                    if (dt.Rows.Count != 0)
                    {
                        for (int num = 0; num < dt.Rows.Count; num++)
                        {
                            dataGridView3.Rows.Add(dt.Rows[num][0], dt.Rows[num][1], "สะสม", dt.Rows[num][3]);
                        }
                    }
                }
                else if (O == 1 && dt.Rows.Count != 0)
                {
                    if (dt.Rows.Count != 0)
                    {
                        for (int num = 0; num < dt.Rows.Count; num++)
                        {
                            dataGridView3.Rows.Add(dt.Rows[num][0], dt.Rows[num][1], "สะสม", dt.Rows[num][3]);
                        }
                    }
                }
                else { MessageBox.Show($"ไม่พบรายการ", "การแจ้งเตือนการค้นหา", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }
        }

        private void CBMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CByear.SelectedIndex != -1)
            {
                CBStatus.Enabled = true;
            }
        }

        private void CByear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CByear.SelectedIndex != -1)
            {
                CBMonth.Enabled = true;
            }
        }

        private void CBStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CBStatus.SelectedIndex != -1)
            {
                automatic.Enabled = true;
            }
        }

        private void BTClean_Click(object sender, EventArgs e)
        {
            CByear.SelectedIndex = -1;
            CBMonth.SelectedIndex = -1;
            CBStatus.SelectedIndex = -1;
            dataGridView3.Rows.Clear();
            TBTeacherNo.Clear();
            TBTeacherName.Clear();
            TBTeacherBill.Clear();
        }
    }
}

