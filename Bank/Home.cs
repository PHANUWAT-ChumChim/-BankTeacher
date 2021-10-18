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
        /// <para>[3] SELECT MEMBER INPUT: {Text} </para>
        /// <para>[4] SELECT pay IN Mont INPUT:  {TeacherNo} {CByear} {CBMonth} </para>
        /// <para>[5] SELECT DATE Register Member INPUT : {TeacherNo}</para>
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
          //[3] SELECT MEMBER INPUT: {Text}
          "SELECT TOP(20) a.TeacherNo , CAST(c.PrefixName+' '+[Fname] +' '+ [Lname] as NVARCHAR)AS Name, e.SavingAmount,    \r\n " +
          "b.TeacherLicenseNo,b.IdNo AS IDNo,b.TelMobile ,a.StartAmount,CAST(d.MemberStatusName as nvarchar) AS UserStatususing    \r\n " +
          "FROM EmployeeBank.dbo.tblMember as a    \r\n " +
          "LEFT JOIN Personal.dbo.tblTeacherHis as b ON a.TeacherNo = b.TeacherNo    \r\n " +
          "LEFT JOIN BaseData.dbo.tblPrefix as c ON c.PrefixNo = b.PrefixNo   \r\n " +
          "INNER JOIN EmployeeBank.dbo.tblMemberStatus as d on a.MemberStatusNo = d.MemberStatusNo  \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblShare as e on a.TeacherNo = e.TeacherNo \r\n " +
          "WHERE a.TeacherNo LIKE '%{Text}%'  or CAST(c.PrefixName+' '+[Fname] +' '+ [Lname] as NVARCHAR) LIKE '%{Text}%'   and a.MemberStatusNo = 1         \r\n " +
          "GROUP BY a.TeacherNo , CAST(c.PrefixName+' '+[Fname] +' '+ [Lname] as NVARCHAR), e.SavingAmount,    \r\n " +
          "b.TeacherLicenseNo,b.IdNo ,b.TelMobile ,a.StartAmount,CAST(d.MemberStatusName as nvarchar)   \r\n " +
          "ORDER BY a.TeacherNo; "

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
            ,
          //[5]SELECT DATE Register Member INPUT : {TeacherNo}
          "SELECT CAST(DateAdd as date) \r\n " +
          "FROM EmployeeBank.dbo.tblMember \r\n " +
          "WHERE TeacherNo = '{TeacherNo}' and MemberStatusNo != 2; "
          ,
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
                Bank.Search IN = new Bank.Search(SQLDefault[3]);
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
                    DataTable dt = Class.SQLConnection.InputSQLMSSQL(SQLDefault[3].Replace("{Text}", TBTeacherNo.Text));
                    if (dt.Rows.Count != 0)
                    {
                        TBTeacherName.Text = dt.Rows[0][1].ToString();
                        TBTeacherBill.Text = dt.Rows[0][4].ToString();
                        Check = 1;

                        CByear.Items.Clear();
                        CByear.SelectedIndex = -1;

                        DataTable dts = Class.SQLConnection.InputSQLMSSQL(SQLDefault[5]
                            .Replace("{TeacherNo}", TBTeacherNo.Text));
                        int YearRegister = Convert.ToInt32((Convert.ToDateTime(dts.Rows[0][0].ToString())).ToString("yyyy"));
                        if (YearRegister < Convert.ToInt32(example.GOODS.Menu.Date[0]) - 2)
                        {
                            int Yeard2 = Convert.ToInt32(example.GOODS.Menu.Date[0]) - 2;

                            while (Yeard2 <= Convert.ToInt32(example.GOODS.Menu.Date[0]) + 1)
                            {
                                CByear.Items.Add(Yeard2);
                                Yeard2++;
                            }
                        }
                        else if (YearRegister > Convert.ToInt32(example.GOODS.Menu.Date[0]) - 2)
                        {
                            while (YearRegister <= Convert.ToInt32(example.GOODS.Menu.Date[0]) + 1)
                            {
                                CByear.Items.Add(YearRegister);
                                YearRegister++;
                            }
                        }
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
        }

        private void automatic_Click(object sender, EventArgs e)
        {
            
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
                CBStatus.Enabled = true;
            }
        }

        private void CBStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CBStatus.SelectedIndex != -1)
            {

            }
        }

        private void BTClean_Click(object sender, EventArgs e)
        {
            CByear.SelectedIndex = -1;
            CBStatus.SelectedIndex = -1;
            dataGridView3.Rows.Clear();
            TBTeacherNo.Clear();
            TBTeacherName.Clear();
            TBTeacherBill.Clear();
        }
    }
}

