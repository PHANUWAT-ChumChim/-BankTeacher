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
    public partial class AmountOff : Form
    {
        /// <summary>
        /// <para>[0] SELECT LoanNo,RemainAmount,Name,EndDate INPUT: {TeacherNo}</para>
        /// <para>[1] SELECT TeacherNo,Name,SumRemainAmount,AmountCredit,SavingAmount,ShareNo INPUT: {TeacherNo}</para>
        /// <para>[2] UPDATE Share WithDraw INPUT: {ShareNo} , {WithDraw}</para>
        /// <para>[3] INSERT ShareWithDraw INPUT: {TeacherNoAddBy} , {ShareNo} , {WithDraw} , {PayMent}</para>
        /// <para>[4] Check BillDetailPayment INPUT: -  </para>
        /// <para>[5] SELECT MEMBER INPUT: {Text}</para>
        /// </summary>
        String[] SQLDefault = new String[]
        {
            //[0] SELECT LoanNo,RemainAmount,Name,EndDate INPUT: {TeacherNo}
            "SELECT a.LoanNo , a.RemainsAmount  , CAST(d.PrefixName + '' + c.Fname + ' ' + c.Lname AS NVARCHAR) AS NAME,\r\n" +
            "DATEADD(MONTH,b.PayNo,CAST(CAST(CAST(b.YearPay as nvarchar) +'/' + CAST(b.MonthPay AS nvarchar) + '/05' AS nvarchar) AS date)) as DateEnd\r\n" +
            "FROM EmployeeBank.dbo.tblGuarantor as a\r\n" +
            "LEFT JOIN EmployeeBank.dbo.tblLoan as b on a.LoanNo = b.LoanNo\r\n" +
            "LEFT JOIN Personal.dbo.tblTeacherHis as c on b.TeacherNo = c.TeacherNo\r\n" +
            "LEFT JOIN BaseData.dbo.tblPrefix as d on c.PrefixNo = d.PrefixNo\r\n" +
            "WHERE a.TeacherNo LIKE '%{TeacherNo}%' and a.RemainsAmount > 0\r\n" +
            "GROUP BY a.LoanNo , a.RemainsAmount, CAST(d.PrefixName + '' + c.Fname + ' ' + c.Lname AS NVARCHAR),\r\n" +
            "DATEADD(MONTH,b.PayNo,CAST(CAST(CAST(b.YearPay as nvarchar) +'/' + CAST(b.MonthPay AS nvarchar) + '/05' AS nvarchar) AS date));"


            ,

            //[1] SELECT Name ,ShareNo ,SavingAmount ,CreditSupport , WithDrawSavingAmount INPUT: {TeacherNo}
            "SELECT CAST(c.PrefixName + '' + b.Fname + ' ' + b.Lname AS NVARCHAR) AS NAME , d.ShareNo , d.SavingAmount,\r\n" +
            "ISNULL(SUM(e.RemainsAmount),0) as CreditSupport , (d.SavingAmount - ISNULL(SUM(e.RemainsAmount) , 0)) as WithDrawSavingAmount\r\n" +
            "FROM EmployeeBank.dbo.tblMember as a\r\n" +
            "LEFT JOIN Personal.dbo.tblTeacherHis as b on a.TeacherNo = b.TeacherNo\r\n" +
            "LEFT JOIN BaseData.dbo.tblPrefix as c on b.PrefixNo = c.PrefixNo\r\n" +
            "LEFT JOIN EmployeeBank.dbo.tblShare as d on a.TeacherNo = d.TeacherNo\r\n" +
            "LEFT JOIN EmployeeBank.dbo.tblGuarantor as e on a.TeacherNo = e.TeacherNo\r\n" +
            "WHERE a.TeacherNo LIKE '%{TeacherNo}%'\r\n" +
            "GROUP BY a.TeacherNo , d.ShareNo , d.SavingAmount ,c.PrefixName ,  b.Fname , b.Lname;"

            ,   

            //[2] UPDATE Share WithDraw INPUT: {ShareNo} , {WithDraw}
            "UPDATE EmployeeBank.dbo.tblShare\r\n" +
            "SET SavingAmount = SavingAmount - {WithDraw}\r\n" +
            "WHERE ShareNo = {ShareNo};"

            ,

            //[3] INSERT ShareWithDraw INPUT: {TeacherNoAddBy} , {ShareNo} , {WithDraw},{PayMent}
            "INSERT INTO EmployeeBank.dbo.tblShareWithdraw (TeacherNoAddBy,ShareNo,DateAdd,Amount,BillDetailPayMentNo)\r\n" +
            "VALUES ('{TeacherNoAddBy}', '{ShareNo}',CAST(CURRENT_TIMESTAMP as Date),'{WithDraw}',{PayMent});"

            ,

            //[4] Check BillDetailPayment INPUT: -  
            "SELECT Name , BillDetailpaymentNo  \r\n " +
            "FROM EmployeeBank.dbo.tblBillDetailPayment \r\n " +
            "WHERE Status = 1 ;"
            ,
            //[5] SELECT MEMBER INPUT: {Text}
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
        };

        int Check;
        public AmountOff()
        {
            InitializeComponent();
        }

        private void AmountOff_Load(object sender, EventArgs e)
        {
            ComboBox[] cb = new ComboBox[] { CBTypePay };
            DataTable dtPayment = Class.SQLConnection.InputSQLMSSQL(SQLDefault[4]);
            for (int a = 0; a < dtPayment.Rows.Count; a++)
                for (int x = 0; x < cb.Length; x++)
                    cb[x].Items.Add(new example.Class.ComboBoxPayment(dtPayment.Rows[a][0].ToString(),
                        dtPayment.Rows[a][1].ToString()));
        }

        private void TBTeacherNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (TBTeacherNo.Text.Length == 6)
                {

                    DGVLoan.Rows.Clear();
                    DataSet ds = Class.SQLConnection.InputSQLMSSQLDS(
                        SQLDefault[1].Replace("{TeacherNo}", TBTeacherNo.Text) + 
                        SQLDefault[0].Replace("{TeacherNo}", TBTeacherNo.Text));
                    if (ds.Tables[0].Rows.Count != 0)
                    {
                        TBWithDraw.Enabled = true;
                        CBTypePay.Enabled = true;
                        BSaveAmountOff.Enabled = true;
                        TBTeacherName.Text = ds.Tables[0].Rows[0][0].ToString();
                        TBShareNo.Text = ds.Tables[0].Rows[0][1].ToString();
                        TBSavingAmount.Text = ds.Tables[0].Rows[0][2].ToString();
                        TBCreditSystem.Text = ds.Tables[0].Rows[0][3].ToString();
                        TBCreditWithDraw.Text = ds.Tables[0].Rows[0][4].ToString();

                        for(int Num = 0;Num < ds.Tables[1].Rows.Count; Num++)
                        {
                            DGVLoan.Rows.Add(ds.Tables[1].Rows[Num][0].ToString(), ds.Tables[1].Rows[Num][1].ToString(), ds.Tables[1].Rows[Num][2].ToString(), ds.Tables[1].Rows[Num][3].ToString());
                        }
                    }
                    else
                    {
                        MessageBox.Show("รหัสผู้ใช้ไม่ถูกต้อง", "System", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                }

            }
            else if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
            {
                if (Check == 1)
                {
                    TBTeacherName.Text = "";
                    TBShareNo.Text = "";
                    TBSavingAmount.Text = "";
                    DGVLoan.Rows.Clear();
                    TBCreditSystem.Text = "";
                    TBCreditWithDraw.Text = "";
                    TBWithDraw.Text = "";
                    TBWithDraw.Enabled = false;
                    CBTypePay.Enabled = false;
                    BSaveAmountOff.Enabled = false;
                    Check = 0;
                }
            }
        }

        private void BSaveAmountOff_Click(object sender, EventArgs e)
        {
            example.Class.ComboBoxPayment Payment = (CBTypePay.SelectedItem as example.Class.ComboBoxPayment);
            if (TBWithDraw.Text != "" && CBTypePay.SelectedIndex != -1)
            {
                Class.SQLConnection.InputSQLMSSQLDS( (SQLDefault[2] + 
                    "\r\n"+
                    SQLDefault[3])
                    .Replace("{ShareNo}", TBShareNo.Text)
                    .Replace("{WithDraw}", TBWithDraw.Text)
                    .Replace("{TeacherNoAddBy}", Class.UserInfo.TeacherNo)
                    .Replace("{PayMent}", Payment.No));
            }
        }

        private void TBWithDraw_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!Char.IsNumber(e.KeyChar)) && (!Char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }
        private void TBCreditWithDraw_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!Char.IsNumber(e.KeyChar)) && (!Char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void TBWithDraw_Leave(object sender, EventArgs e)
        {
            if(int.Parse(TBWithDraw.Text) > int.Parse(TBCreditWithDraw.Text))
            {
                //MessageBox.Show("")
            }
        }

        private void AmountOff_SizeChanged(object sender, EventArgs e)
        {
            Class.FromSettingMedtod.ChangeSizePanal(this, panel1);

        }

        private void BSearchTeacher_Click(object sender, EventArgs e)
        {
            try
            {
                Bank.Search IN = new Bank.Search(SQLDefault[5]);
                IN.ShowDialog();
                TBTeacherNo.Text = Bank.Search.Return[0];
                TBTeacherNo_KeyDown(sender, new KeyEventArgs(Keys.Enter));
            }
            catch (Exception x)
            {
                Console.WriteLine(x);
            }
        }
    }
}
