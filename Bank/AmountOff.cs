using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static BankTeacher.Class.ProtocolSharing.ConnectSMB;

namespace BankTeacher.Bank
{
    public partial class AmountOff : Form
    {
        int StatusBoxFile = 0;
        String imgeLocation = "";
        /// <summary>
        /// <para>[0] SELECT LoanNo,RemainAmount,Name,EndDate INPUT: {TeacherNo}</para>
        /// <para>[1] SELECT TeacherNo,Name,SumRemainAmount,AmountCredit,SavingAmount,ShareNo INPUT: {TeacherNo}</para>
        /// <para>[2] UPDATE Share WithDraw INPUT: {ShareNo} , {WithDraw}</para>
        /// <para>[3] INSERT ShareWithDraw INPUT: {TeacherNoAddBy} , {ShareNo} , {WithDraw} , {PayMent}</para>
        /// <para>[4] Check BillDetailPayment INPUT: -  </para>
        /// <para>[5] SELECT MEMBER INPUT: {Text}</para>
        /// <para>[6] SELECT ShareWithDraw INPUT: {Date}</para>
        /// <para>[7] SELECT Withdraw (Year) INPUT: {Year}</para>
        /// </summary>
        String[] SQLDefault = new String[]
        {
            //[0] SELECT LoanNo,RemainAmount,Name,EndDate INPUT: {TeacherNo}
            "SELECT a.LoanNo , a.RemainsAmount  , CAST(ISNULL(d.PrefixName+' ','') + c.Fname + ' ' + c.Lname AS NVARCHAR) AS NAME,\r\n" +
            "DATEADD(MONTH,b.PayNo,CAST(CAST(CAST(b.YearPay as nvarchar) +'/' + CAST(b.MonthPay AS nvarchar) + '/05' AS nvarchar) AS date)) as DateEnd\r\n" +
            "FROM EmployeeBank.dbo.tblGuarantor as a\r\n" +
            "LEFT JOIN EmployeeBank.dbo.tblLoan as b on a.LoanNo = b.LoanNo\r\n" +
            "LEFT JOIN Personal.dbo.tblTeacherHis as c on b.TeacherNo = c.TeacherNo\r\n" +
            "LEFT JOIN BaseData.dbo.tblPrefix as d on c.PrefixNo = d.PrefixNo\r\n" +
            "WHERE a.TeacherNo LIKE '%{TeacherNo}%' and a.RemainsAmount > 0\r\n" +
            "GROUP BY a.LoanNo , a.RemainsAmount, CAST(ISNULL(d.PrefixName+' ','') + c.Fname + ' ' + c.Lname AS NVARCHAR),\r\n" +
            "DATEADD(MONTH,b.PayNo,CAST(CAST(CAST(b.YearPay as nvarchar) +'/' + CAST(b.MonthPay AS nvarchar) + '/05' AS nvarchar) AS date));"


            ,

            //[1] SELECT Name ,ShareNo ,SavingAmount ,CreditSupport , WithDrawSavingAmount INPUT: {TeacherNo}
            "SELECT CAST(ISNULL(c.PrefixName+' ','') + b.Fname + ' ' + b.Lname AS NVARCHAR) AS NAME , d.ShareNo , d.SavingAmount,\r\n" +
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
            "SELECT Convert(nvarchar(50) , Name) , BillDetailpaymentNo  \r\n " +
            "FROM EmployeeBank.dbo.tblBillDetailPayment \r\n " +
            "WHERE Status = 1 and BillDetailPaymentNo <> 3 ;"
            ,
            //[5] SELECT MEMBER INPUT: {Text}
            "SELECT TOP(20) a.TeacherNo , CAST(ISNULL(c.PrefixName+' ','')+[Fname] +' '+ [Lname] as NVARCHAR)AS Name, e.SavingAmount,    \r\n " +
            "b.TeacherLicenseNo,b.IdNo AS IDNo,b.TelMobile ,a.StartAmount,CAST(d.MemberStatusName as nvarchar) AS UserStatususing    \r\n " +
            "FROM EmployeeBank.dbo.tblMember as a    \r\n " +
            "LEFT JOIN Personal.dbo.tblTeacherHis as b ON a.TeacherNo = b.TeacherNo    \r\n " +
            "LEFT JOIN BaseData.dbo.tblPrefix as c ON c.PrefixNo = b.PrefixNo   \r\n " +
            "INNER JOIN EmployeeBank.dbo.tblMemberStatus as d on a.MemberStatusNo = d.MemberStatusNo  \r\n " +
            "LEFT JOIN EmployeeBank.dbo.tblShare as e on a.TeacherNo = e.TeacherNo \r\n " +
            "WHERE a.MemberStatusNo = 1 and a.TeacherNo LIKE '%{Text}%'  or CAST(ISNULL(c.PrefixName+' ','')+[Fname] +' '+ [Lname] as NVARCHAR) LIKE '%{Text}%'   and a.MemberStatusNo = 1         \r\n " +
            "GROUP BY a.TeacherNo , CAST(ISNULL(c.PrefixName+' ','')+[Fname] +' '+ [Lname] as NVARCHAR), e.SavingAmount,    \r\n " +
            "b.TeacherLicenseNo,b.IdNo ,b.TelMobile ,a.StartAmount,CAST(d.MemberStatusName as nvarchar)   \r\n " +
            "ORDER BY a.TeacherNo; "

            ,

            //[6] SELECT ShareWithDraw INPUT: {Date} ,{TeacherNo}
            "SELECT CAST(c.DateAdd as date) ,a.TeacherNo , CAST(ISNULL(e.PrefixNameFull , '') + d.Fname + ' ' + d.Lname as varchar) , c.Amount  \r\n " +
            "FROM EmployeeBank.dbo.tblMember as a  \r\n " +
            "LEFT JOIN EmployeeBank.dbo.tblShare as b on a.TeacherNo = b.TeacherNo  \r\n " +
            "LEFT JOIN EmployeeBank.dbo.tblShareWithdraw as c on b.ShareNo = c.ShareNo  \r\n " +
            "LEFT JOIN Personal.dbo.tblTeacherHis as d on a.TeacherNo = d.TeacherNo  \r\n " +
            "LEFT JOIN BaseData.dbo.tblPrefix as e on d.PrefixNo = e.PrefixNo  \r\n " +
            "WHERE CAST(CAST(c.DateAdd as date) as varchar) LIKE '{Date}%' and a.TeacherNo LIKE '{TeacherNo}%'"
           


            ,
           //[7] SELECT Withdraw (Date) INPUT: {Date}
           "SELECT * \r\n " +
          "FROM EmployeeBank.dbo.tblShareWithdraw  \r\n " +
          "WHERE CAST(CAST(DateAdd as date) as nvarchar(50)) LIKE '{Date}%'"
           ,

        };

        int Check;
        public AmountOff()
        {
            InitializeComponent();
            Console.WriteLine("==================Open AmountOff Form======================");
            CBMonth.Text = Bank.Menu.Date[1];
            
        }

        private void AmountOff_Load(object sender, EventArgs e)
        {
            ComboBox[] cb = new ComboBox[] { CBTypePay };
            DataTable dtPayment = Class.SQLConnection.InputSQLMSSQL(SQLDefault[4]);
            for (int a = 0; a < dtPayment.Rows.Count; a++)
                for (int x = 0; x < cb.Length; x++)
                    cb[x].Items.Add(new BankTeacher.Class.ComboBoxPayment(dtPayment.Rows[a][0].ToString(),
                        dtPayment.Rows[a][1].ToString()));

            int Year = Convert.ToInt32(BankTeacher.Bank.Menu.Date[0]);
            for (int a = 0; a < 5; a++)
            {
                if (Class.SQLConnection.InputSQLMSSQL(SQLDefault[7]
                    .Replace("{Date}", Year.ToString())).Rows.Count == 0)
                {
                    continue;
                }
                CBYear.Items.Add(Year);
                Year--;
            }
            CBYear.SelectedIndex = 0;
            if (TBTeacherNo.Text != "")
                TBTeacherNo_KeyDown(sender, new KeyEventArgs(Keys.Enter));
        }

        public void TBTeacherNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (TBTeacherNo.Text.Length == 6)
                {

                    DGVLoan.Rows.Clear();
                    DataSet ds = Class.SQLConnection.InputSQLMSSQLDS(
                        SQLDefault[1].Replace("{TeacherNo}", TBTeacherNo.Text) +
                        "\r\n" +
                        SQLDefault[0].Replace("{TeacherNo}", TBTeacherNo.Text));
                    String[] Credit = new string[] { };
                    if (ds.Tables[0].Rows.Count != 0)
                    {
                        Credit = ds.Tables[0].Rows[0][3].ToString().Split('.');
                        TBWithDraw.Enabled = true;
                        CBTypePay.Enabled = true;
                        //BSaveAmountOff.Enabled = true;
                        TBTeacherName.Text = ds.Tables[0].Rows[0][0].ToString();
                        TBShareNo.Text = ds.Tables[0].Rows[0][1].ToString();
                        TBSavingAmount.Text = ds.Tables[0].Rows[0][2].ToString();
                        TBCreditSystem.Text = Credit[0];
                        Credit = ds.Tables[0].Rows[0][4].ToString().Split('.');
                        TBCreditWithDraw.Text = Credit[0];
                        Check = 1;
                        CBTypePay.SelectedIndex = 0;

                        for (int Num = 0; Num < ds.Tables[1].Rows.Count; Num++)
                        {
                            Credit = ds.Tables[1].Rows[Num][1].ToString().Split('.');
                            DGVLoan.Rows.Add(ds.Tables[1].Rows[Num][0].ToString(), ds.Tables[1].Rows[Num][2].ToString(), Credit[0], ds.Tables[1].Rows[Num][3].ToString());
                        }
                        //if (CBTypePay.SelectedIndex != -1)
                        //    CBTypePay.SelectedIndex = -1;
                    }
                    else
                    {
                        MessageBox.Show("รหัสผู้ใช้ไม่ถูกต้อง", "System", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    //String[] d = (CBMonth.SelectedItem as String[]);
                    //object eddd = CBMonth.SelectedItem;
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
                    CBTypePay.SelectedIndex = -1;
                    CBTypePay.Enabled = false;
                    BSaveAmountOff.Enabled = false;
                    Check = 0;
                }
            }
        }

        private void BSaveAmountOff_Click(object sender, EventArgs e)
        {
            BankTeacher.Class.ComboBoxPayment Payment = (CBTypePay.SelectedItem as BankTeacher.Class.ComboBoxPayment);

            if (Int32.TryParse(TBCreditWithDraw.Text, out int CraditwithDraw)&& CraditwithDraw >= 1 && CBTypePay.SelectedIndex != -1)
            {
                try
                {
                    if (MessageBox.Show("ยืนยันการจ่าย", "ระบบ", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        Class.SQLConnection.InputSQLMSSQLDS((SQLDefault[2] +
                    "\r\n" +
                    SQLDefault[3])
                    .Replace("{ShareNo}", TBShareNo.Text)
                    .Replace("{WithDraw}", TBWithDraw.Text)
                    .Replace("{TeacherNoAddBy}", Class.UserInfo.TeacherNo)
                    .Replace("{PayMent}", Payment.No));
                        TBTeacherNo_KeyDown(sender, new KeyEventArgs(Keys.Back));
                        TBTeacherNo.Text = "";
                        MessageBox.Show("บันทึกสำเร็จ", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception x)
                {
                    Console.Write(x);
                }
                //
            }
            else
                MessageBox.Show("ยอดเงินไม่เพียงพอ", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Information);

            foreach (Form f in Application.OpenForms)
            {
                if (f.Name == "Menu" && f.Visible == false)
                {
                    f.Enabled = true;
                    this.Close();
                }
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
            if (TBWithDraw.Text != "")
            {
                if (Int32.TryParse(TBWithDraw.Text , out int WithDraw) && Int32.TryParse(TBCreditWithDraw.Text , out int CraditWithDraw) && WithDraw > CraditWithDraw)
                {
                    MessageBox.Show("ยอดเงินเกินกำหนด", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TBWithDraw.Text = "";
                    TBWithDraw.Focus();
                }
                else
                {
                    CBTypePay.Enabled = true;
                }
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
                if (Bank.Search.Return[0] != "")
                {
                    TBTeacherNo.Text = Bank.Search.Return[0];
                    TBTeacherNo_KeyDown(sender, new KeyEventArgs(Keys.Enter));
                }
            }
            catch (Exception x)
            {
                Console.WriteLine(x);
            }
        }

        private void CBYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CBYear.SelectedIndex != -1)
            {
                CBMonth.SelectedIndex = -1;
                CBMonth.Items.Clear();
                String MonthInsert = "";
                int Month = Convert.ToInt32(BankTeacher.Bank.Menu.Date[1]);
                if (CBYear.SelectedIndex == 0)
                {
                    for (int a = 0; a <= Month; a++)
                    {
                        if (a < 10)
                        {
                            MonthInsert = "0" + a;
                        }
                        else
                        {
                            MonthInsert = a.ToString();
                        }
                        if (a == 0)
                        {
                            CBMonth.Items.Add("(none)");
                        }
                        else
                        {
                            if (Class.SQLConnection.InputSQLMSSQL(SQLDefault[7]
                                .Replace("{Date}", CBYear.Text + "-" + MonthInsert)).Rows.Count == 0)
                            {
                                continue;
                            }
                            CBMonth.Items.Add(a);
                        }
                    }
                }
                else
                {
                    for (int a = 0; a <= 12; a++)
                    {
                        if (a == 0)
                            CBMonth.Items.Add("(none)");
                        else
                            CBMonth.Items.Add(a);
                    }
                }
                CBMonth.Enabled = true;
                CBMonth.SelectedIndex = 0;
            }
        }

        private void CBMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CBMonth.SelectedIndex != -1)
            {
                DataSet ds;
                if (Int32.TryParse(CBMonth.Text, out int Month) && Month >= 1 && Month < 10)
                {
                    ds = Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[6]
                    .Replace("{Date}", CBYear.SelectedItem.ToString() + "-0" + CBMonth.SelectedItem.ToString())
                    .Replace("{TeacherNo}", TBTeacherNo.Text));
                }
                else if (Int32.TryParse(CBMonth.Text, out int Montha) && Montha >= 10)
                {
                    ds = Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[6]
                    .Replace("{Date}", CBYear.SelectedItem.ToString() + "-" + CBMonth.SelectedItem.ToString())
                    .Replace("{TeacherNo}", TBTeacherNo.Text));
                }
                else
                {
                    ds = Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[6]
                   .Replace("{Date}", CBYear.SelectedItem.ToString() + "-")
                   .Replace("{TeacherNo}" , TBTeacherNo.Text));
                }

                DGVAmountOffHistory.Rows.Clear();
                for (int a = 0; a < ds.Tables[0].Rows.Count; a++)
                {
                    DGVAmountOffHistory.Rows.Add(ds.Tables[0].Rows[a][0], ds.Tables[0].Rows[a][1], ds.Tables[0].Rows[a][2], ds.Tables[0].Rows[a][3]);
                }
                if (ds.Tables[0].Rows.Count == 0 && tabControl1.SelectedIndex == 1)
                {
                    MessageBox.Show("ไม่พบรายการ", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void CBTypePay_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CBTypePay.SelectedIndex != -1)
            {
                BSaveAmountOff.Enabled = true;
            }
            else
                BSaveAmountOff.Enabled = false;
        }

        private void BMaxWithDraw_AmountOff_Click(object sender, EventArgs e)
        {
            if(TBCreditWithDraw.Text != "")
            {
                TBWithDraw.Text = TBCreditWithDraw.Text;
            }
        }

        private void AmountOff_FormClosed(object sender, FormClosedEventArgs e)
        {
            foreach (Form f in Application.OpenForms)
            {
                if (f.Name == "Menu" && f.Visible == false)
                {
                    f.Enabled = true;
                    this.Close();
                }
            }
        }
    }
}