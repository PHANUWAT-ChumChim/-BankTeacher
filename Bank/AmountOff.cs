using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static BankTeacher.Class.ProtocolSharing.ConnectSMB;

namespace BankTeacher.Bank
{
    public partial class AmountOff : Form
    {
        // ======================= ข้อมูลเเบบปริ้น ====================
        //ข้อมูลส่วนตัว
        //public static string info_name;
        //public static string info_id;
        // จ่าย
        //public static string info_totelAmountpay;
        //public static string info_status;
        //public static string info_ShareNo;
        // กล่งอเอกสาาร
        //public static string info_BillAmounoff;
        //public static string info_datepayAmounoff;
        //// ยอดที่ถอน
        //public static string info_Amounoff;
        //public static string info_Amounoffinsystem;
        //public static string info_canbeAmounoff;

        //int StatusBoxFile = 0;
        //String imgeLocation = "";
        /// <summary>
        /// <para>[0] DGV SELECT LoanNo,RemainAmount,Name,EndDate INPUT: {TeacherNo}</para>
        /// <para>[1] SELECT TeacherNo,Name,SumRemainAmount,AmountCredit,SavingAmount,ShareNo INPUT: {TeacherNo}</para>
        /// <para>[2] UPDATE Share WithDraw INPUT: {ShareNo} , {WithDraw}</para>
        /// <para>[3] INSERT ShareWithDraw INPUT: {TeacherNoAddBy} , {ShareNo} , {WithDraw} , {PayMent} , {Date} </para>
        /// <para>[4] Check BillDetailPayment INPUT:  </para>
        /// <para>[5] SELECT MEMBER INPUT: {Text}</para>
        /// <para>[6] SELECT ShareWithDraw INPUT: {Date}</para>
        /// <para>[7] SELECT Withdraw (Year) INPUT: {Year} {TeacherNo}</para>
        /// <para>[9] SELECT for Print INPUT : {WithDrawNo} </para>
        /// <para>[10] SELECT for Print(WithDrawNo) </para>
        /// <para>[11] for(datagrid) INPUT : {TeacherNo} {Date} </para>
        /// <para>[12] for year of withdraw  INPUT : {TeacherNo} </para>
        /// <para>[13] (print)for search bill WithDrawNo INPUT : {WithDrawNo} </para>
        /// <para>[14] Search Member and SavingAmount - RemainAmount in Guarantor INPUT: {TeacherNoNotLike} {Text} </para>
        /// </summary>
        String[] SQLDefault = new String[]
        {
            //[0] DGV SELECT LoanNo,RemainAmount,Name,EndDate INPUT: {TeacherNo}
           " \r\n " +
          "SELECT a.LoanNo , a.RemainsAmount  , CAST(ISNULL(d.PrefixName+' ','') + c.Fname + ' ' + c.Lname AS NVARCHAR) AS NAME, \r\n " +
          "DATEADD(MONTH,b.PayNo,CAST(CAST(CAST(b.YearPay as nvarchar) +'/' + CAST(b.MonthPay AS nvarchar) + '/05' AS nvarchar) AS date)) as DateEnd , e.LoanStatusName \r\n " +
          "FROM EmployeeBank.dbo.tblGuarantor as a \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblLoan as b on a.LoanNo = b.LoanNo \r\n " +
          "LEFT JOIN Personal.dbo.tblTeacherHis as c on b.TeacherNo = c.TeacherNo \r\n " +
          "LEFT JOIN BaseData.dbo.tblPrefix as d on c.PrefixNo = d.PrefixNo \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblLoanStatus as e on b.LoanStatusNo = e.LoanStatusNo \r\n " +
          "WHERE a.TeacherNo = '{TeacherNo}' and a.RemainsAmount > 0 \r\n " +
          "GROUP BY a.LoanNo , a.RemainsAmount, CAST(ISNULL(d.PrefixName+' ','') + c.Fname + ' ' + c.Lname AS NVARCHAR), \r\n " +
          "DATEADD(MONTH,b.PayNo,CAST(CAST(CAST(b.YearPay as nvarchar) +'/' + CAST(b.MonthPay AS nvarchar) + '/05' AS nvarchar) AS date)) , e.LoanStatusName;"
           ,


            //[1] SELECT Name ,ShareNo ,SavingAmount ,CreditSupport , WithDrawSavingAmount INPUT: {TeacherNo}
           "SELECT CAST(ISNULL(c.PrefixName+' ','') + b.Fname + ' ' + b.Lname AS NVARCHAR) AS NAME , d.ShareNo , d.SavingAmount, \r\n " +
          "ISNULL(SUM(e.RemainsAmount),0) as CreditSupport , (d.SavingAmount - ISNULL(SUM(e.RemainsAmount) , 0)) as WithDrawSavingAmount \r\n " +
          "FROM EmployeeBank.dbo.tblMember as a \r\n " +
          "LEFT JOIN Personal.dbo.tblTeacherHis as b on a.TeacherNo = b.TeacherNo \r\n " +
          "LEFT JOIN BaseData.dbo.tblPrefix as c on b.PrefixNo = c.PrefixNo \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblShare as d on a.TeacherNo = d.TeacherNo \r\n " +
          "LEFT JOIN (SELECT b.TeacherNo , b.RemainsAmount \r\n " +
          "	FROM EmployeeBank.dbo.tblLoan as a \r\n " +
          "	LEFT JOIN EmployeeBank.dbo.tblGuarantor as b on a.LoanNo = b.LoanNo \r\n " +
          "	WHERE a.LoanStatusNo = 2 and b.TeacherNo = '{TeacherNo}') as e on a.TeacherNo = e.TeacherNo \r\n " +
          "WHERE a.TeacherNo = '{TeacherNo}' \r\n " +
          "GROUP BY a.TeacherNo , d.ShareNo , d.SavingAmount ,c.PrefixName ,  b.Fname , b.Lname;"
           , 

            //[2] UPDATE Share WithDraw INPUT: {ShareNo} , {WithDraw}
            "UPDATE EmployeeBank.dbo.tblShare\r\n" +
            "SET SavingAmount = SavingAmount - {WithDraw}\r\n" +
            "WHERE ShareNo = {ShareNo};"

            ,

            //[3] INSERT ShareWithDraw INPUT: {TeacherNoAddBy} , {ShareNo} , {WithDraw},{PayMent} , {Date}
            "INSERT INTO EmployeeBank.dbo.tblShareWithdraw (TeacherNoAddBy,ShareNo,DateAdd,Amount,BillDetailPayMentNo,WithDrawTransactionDate)\r\n" +
            "VALUES ('{TeacherNoAddBy}', '{ShareNo}','{Date}','{WithDraw}',{PayMent},CURRENT_TIMESTAMP);"

            ,

            //[4] Check BillDetailPayment INPUT:   
            "SELECT Convert(nvarchar(50) , Name) , BillDetailpaymentNo  \r\n " +
            "FROM EmployeeBank.dbo.tblBillDetailPayment \r\n " +
            "WHERE Status = 1 and BillDetailPaymentNo <> 3 "
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
            "SELECT CAST(c.DateAdd as date) ,a.TeacherNo , CAST(ISNULL(e.PrefixNameFull , '') + d.Fname + ' ' + d.Lname as NVARCHAR) , c.Amount,c.WithDrawNo    \r\n " +
            "FROM EmployeeBank.dbo.tblMember as a  \r\n " +
            "LEFT JOIN EmployeeBank.dbo.tblShare as b on a.TeacherNo = b.TeacherNo  \r\n " +
            "LEFT JOIN EmployeeBank.dbo.tblShareWithdraw as c on b.ShareNo = c.ShareNo  \r\n " +
            "LEFT JOIN Personal.dbo.tblTeacherHis as d on a.TeacherNo = d.TeacherNo  \r\n " +
            "LEFT JOIN BaseData.dbo.tblPrefix as e on d.PrefixNo = e.PrefixNo  \r\n " +
            "WHERE CAST(CAST(c.DateAdd as date) as varchar) LIKE '{Date}%' and a.TeacherNo LIKE '{TeacherNo}%' "



            ,
           //[7] SELECT Withdraw (Date) INPUT: {Date} {TeacherNo}
           "SELECT Count(a.WithDrawNo) \r\n" +
            "FROM EmployeeBank.dbo.tblShareWithdraw as a\r\n" +
            "LEFT JOIN EmployeeBank.dbo.tblShare as b on a.ShareNo = b.ShareNo\r\n" +
            "LEFT JOIN EmployeeBank.dbo.tblMember as c on b.TeacherNo = c.TeacherNo\r\n" +
          "WHERE CAST(CAST(a.DateAdd as date) as nvarchar(50)) LIKE '{Date}%' and c.TeacherNo = '{TeacherNo}'"
           ,
           //[8] Select DateAddMember INPUT: {TeacherNo} 
           "SELECT CAST(DateAdd as date) \r\n " +
          "FROM EmployeeBank.dbo.tblMember \r\n " +
          "WHERE TeacherNo = '{TeacherNo}' and MemberStatusNo != 2; "
           ,
           //[9] SELECT for Print(SELECT) INPUT : {WithDrawNo}
          "SELECT a.WithDrawNo,a.ShareNo,a.Amount,CAST(a.DateAdd as date) as date,a.BillDetailPayMentNo \r\n" +
          "FROM EmployeeBank.dbo.tblShareWithdraw as a \r\n" +
          "WHERE a.WithDrawNo = '{WithDrawNo}';"
            ,
           //[10] SELECT for Print(WithDrawNo) INPUT : -
          "SELECT MAX(a.WithDrawNo) as WithDrawNo \r\n" +
          "FROM EmployeeBank.dbo.tblShareWithdraw as a"
            ,
          //[11] (print)for(datagrid) INPUT : {TeacherNo} {Date}
          "SELECT a.ShareNo,c.TeacherNo,a.WithDrawNo,a.DateAdd,a.Amount  \r\n" +
          "FROM EmployeeBank.dbo.tblShareWithdraw as a  \r\n" +
          "LEFT JOIN EmployeeBank.dbo.tblShare as b on a.ShareNo = b.ShareNo  \r\n" +
          "LEFT JOIN EmployeeBank.dbo.tblMember as c on b.TeacherNo = c.TeacherNo  \r\n" +
          "WHERE c.TeacherNo = '{TeacherNo}' and YEAR(a.DateAdd) = '{Date}'"
            ,
          //[12] (print)for year of withdraw  INPUT : {TeacherNo}
          "SELECT YEAR(MAX(a.DateAdd)) \r\n " +
          "FROM EmployeeBank.dbo.tblShareWithdraw as a \r\n" +
          "LEFT JOIN EmployeeBank.dbo.tblShare as b on a.ShareNo = b.ShareNo \r\n" +
          "LEFT JOIN EmployeeBank.dbo.tblMember as c on b.TeacherNo = c.TeacherNo \r\n" +
          "WHERE c.TeacherNo = '{TeacherNo}'"
            ,
          //[13] (print)for search bill WithDrawNo INPUT : {WithDrawNo}
          "SELECT a.ShareNo,c.TeacherNo,a.WithDrawNo,CAST(a.DateAdd as date),a.Amount,CAST(d.Name as nvarchar),CAST(g.PrefixName+' '+f.Fname+' '+f.Lname as nvarchar)   \r\n" +
          "FROM EmployeeBank.dbo.tblShareWithdraw as a \r\n" +
          "LEFT JOIN EmployeeBank.dbo.tblShare as b on a.ShareNo = b.ShareNo \r\n" +
          "LEFT JOIN EmployeeBank.dbo.tblMember as c on b.TeacherNo = c.TeacherNo \r\n" +
          "LEFT JOIN EmployeeBank.dbo.tblBillDetailPayment d on a.BillDetailPayMentNo = d.BillDetailPaymentNo  \r\n" +
          "LEFT JOIN Personal.dbo.tblTeacherHis f on a.TeacherNoAddBy = f.TeacherNo \r\n" +
          "LEFT JOIN BaseData.dbo.tblPrefix g on f.PrefixNo = g.PrefixNo \r\n" +
          "WHERE a.WithDrawNo = {WithDrawNo}"

            ,

          //[14] Search Member and SavingAmount - RemainAmount in Guarantor INPUT: {TeacherNoNotLike} {Text}
           "SELECT TOP(20)TeacherNo, Name, RemainAmount  \r\n " +
          "FROM (SELECT a.TeacherNo , CAST(ISNULL(c.PrefixName,'')+' '+Fname +' '+ Lname as NVARCHAR)AS Name,     \r\n " +
          "ROUND(ISNULL(e.SavingAmount,0) - ISNULL(SUM(d.RemainsAmount),0),0,1) as RemainAmount, Fname    \r\n " +
          "FROM EmployeeBank.dbo.tblMember as a      \r\n " +
          "LEFT JOIN (    \r\n " +
          "SELECT TeacherNo , Fname , Lname , PrefixNo    \r\n " +
          "FROM Personal.dbo.tblTeacherHis     \r\n " +
          ") as b ON a.TeacherNo = b.TeacherNo      \r\n " +
          "LEFT JOIN BaseData.dbo.tblPrefix as c ON b.PrefixNo = c.PrefixNo      \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblGuarantor as d on a.TeacherNo = d.TeacherNo     \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblShare as e ON e.TeacherNo = a.TeacherNo     \r\n " +
          "LEFT JOIN (SELECT TeacherNo   \r\n " +
          "FROM EmployeeBank.dbo.tblLoan    \r\n " +
          "WHERE LoanStatusNo = 1 or LoanStatusNo = 2 GROUP BY TeacherNo) as f on a.TeacherNo = f.TeacherNo    \r\n " +
          "WHERE (a.TeacherNo LIKE '%{Text}%' or CAST(ISNULL(c.PrefixName,'')+' '+[Fname] +' '+ [Lname] as NVARCHAR) LIKE '%{Text}%') and a.MemberStatusNo = 1 or (a.MemberStatusNo = 2 and e.SavingAmount != 0)\r\n " +
          "GROUP BY a.TeacherNo , CAST(ISNULL(c.PrefixName,'')+' '+Fname +' '+ Lname as NVARCHAR), e.SavingAmount, Fname ) as a     \r\n " +
          "WHERE RemainAmount IS NOT NULL {TeacherNoNotLike} \r\n " +
          "GROUP BY TeacherNo, Name, RemainAmount ,a.Fname  \r\n " +
          "ORDER BY a.Fname; "
           ,


        };

        int Check;
        bool CheckSave = false;
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
                    cb[x].Items.Add(new BankTeacher.Class.ComboBoxPayment(dtPayment.Rows[a][0].ToString(),
                        dtPayment.Rows[a][1].ToString()));
            CBYear.Enabled = false;

            DTPDate.Value = Convert.ToDateTime(Bank.Menu.Date[0] + "/" + Bank.Menu.Date[1] + "/" + Bank.Menu.Date[2]);
            if (BankTeacher.Bank.Menu.DateAmountChange == 1)
                DTPDate.Enabled = true;
            else
                DTPDate.Enabled = false;
        }

        public void TBTeacherNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TBTeacherNo.Text = TBTeacherNo.Text.Replace("t", "T");
                CBYear.Enabled = false;
                DGVAmountOffHistory.Enabled = false;
                tabControl1.SelectedIndex = 0;
                tabControl1.Enabled = true;
                DTPDate.Value = Convert.ToDateTime(Bank.Menu.Date[0] + "/" + Bank.Menu.Date[1] + "/" + Bank.Menu.Date[2]);
                DGVAmountOffHistory.Rows.Clear();
                CBYear.Items.Clear();
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
                    TBTeacherName.Text = ds.Tables[0].Rows[0][0].ToString();
                    TBShareNo.Text = ds.Tables[0].Rows[0][1].ToString();
                    TBSavingAmount.Text = ds.Tables[0].Rows[0][2].ToString();
                    TBCreditSystem.Text = Credit[0];
                    Credit = ds.Tables[0].Rows[0][4].ToString().Split('.');
                    if(Double.TryParse(Credit[0], out double Amount))
                    {
                        if(Amount < 0)
                        {
                            TBCreditWithDraw.Text = 0.ToString();
                            TBWithDraw.Enabled = false;
                            BMaxWithDraw_AmountOff.Enabled = false;
                            MessageBox.Show("ยอดเงินของคุณยังติดกู้อยู่");
                        }
                        else
                        {
                            TBCreditWithDraw.Text = Credit[0];
                            TBWithDraw.Enabled = true;
                            BMaxWithDraw_AmountOff.Enabled = true;
                        }
                    }
                    Check = 1;
                    Checkmember(false);
                    CBTypePay.SelectedIndex = 0;
                    int SumCredit = 0;

                    for (int Num = 0; Num < ds.Tables[1].Rows.Count; Num++)
                    {
                        Credit = ds.Tables[1].Rows[Num][1].ToString().Split('.');
                        DGVLoan.Rows.Add(Num+1,ds.Tables[1].Rows[Num][0].ToString(), ds.Tables[1].Rows[Num][2].ToString(), Credit[0], Convert.ToDateTime(ds.Tables[1].Rows[Num][3].ToString()).ToString("dd/MM/yyyy") , ds.Tables[1].Rows[Num][4]);
                        SumCredit += Convert.ToInt32(Credit[0]);
                    }
                    TBCreditSystem.Text = SumCredit.ToString();
                    if(SumCredit >= Convert.ToInt32(TBSavingAmount.Text))
                    {
                        BMaxWithDraw_AmountOff.Enabled = false;
                        BSave.Enabled = false;
                        TBWithDraw.Enabled = false;
                        TBCreditWithDraw.Text = "0";
                    }
                    else
                    {
                        BMaxWithDraw_AmountOff.Enabled = true;
                        BSave.Enabled = true;
                        TBWithDraw.Enabled = true;
                        TBCreditWithDraw.Text = (Convert.ToInt32(TBSavingAmount.Text) - SumCredit).ToString();
                    }
                    if (DGVLoan.Rows.Count != 0)
                    {
                        TBLoanStatus.Text = "ติดกู้";
                    }
                    else
                        TBLoanStatus.Text = "ปกติ";
                }
                else
                {
                    MessageBox.Show("รหัสผู้ใช้ไม่ถูกต้อง", "System", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                //DataTable dt = Class.SQLConnection.InputSQLMSSQL(SQLDefault[8]
                //    .Replace("{TeacherNo}", TBTeacherNo.Text));
                //if (dt.Rows.Count != 0)
                //{
                //    int Year = Convert.ToInt32((Convert.ToDateTime(dt.Rows[0][0].ToString())).ToString("yyyy")) < Convert.ToInt32(Bank.Menu.Date[0]) - 2 ? Convert.ToInt32(Bank.Menu.Date[0]) - 2 : Convert.ToInt32((Convert.ToDateTime(dt.Rows[0][0].ToString())).ToString("yyyy"));
                //    while (Year <= Convert.ToInt32(Bank.Menu.Date[0].ToString()))
                //    {
                //        CBYear.Items.Add(Year);
                //        Year++;
                //    }
                //    CBYear.Enabled = true;
                //}
                for(int Year = 0; Year < 2; Year++)
                {
                    // เอา ปี ล่าสุดที่ถอนมา
                    DataTable ds_date = Class.SQLConnection.InputSQLMSSQL(SQLDefault[12].Replace("{TeacherNo}", TBTeacherNo.Text));
                    if(ds_date.Rows.Count != 0 && ds_date.Rows[0][0].ToString() != "")
                    {
                        int Year_later = Convert.ToInt32(ds_date.Rows[0][0].ToString());
                        // เอา ปีที่มีการถอนอย่างต่ำมา 2 ปี 
                        DataTable ds_CheckYear = Class.SQLConnection.InputSQLMSSQL(SQLDefault[11].Replace("{TeacherNo}", TBTeacherNo.Text)
                            .Replace("{Date}", Convert.ToInt32(Year_later - Year).ToString()));
                        if (ds_CheckYear.Rows.Count != 0)
                        {
                            CBYear.Items.Add(Year_later - Year);
                        }
                    }
                }
                if (CBYear.Items.Count != 0)
                {
                    CBYear.Enabled = true;
                    DGVAmountOffHistory.Enabled = true;
                    CBYear.SelectedIndex = 0;
                    CB_SelectPrint.SelectedIndex = 0;
                }
                else
                {
                    CBYear.Enabled = false;
                    DGVAmountOffHistory.Enabled = false;
                }
                CheckSave = false;
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
                    tabControl1.Enabled = false;
                    TBWithDraw.Enabled = false;
                    CBTypePay.SelectedIndex = -1;
                    CBTypePay.Enabled = false;
                    //BSaveAmountOff.Enabled = false;
                    Check = 0;
                    Checkmember(true);
                }
            }
        }
        private static int PinrtChcek = 0;
        private void BSaveAmountOff_Click(object sender, EventArgs e)
        {
            BankTeacher.Class.ComboBoxPayment Payment = (CBTypePay.SelectedItem as BankTeacher.Class.ComboBoxPayment);

            if (Int32.TryParse(TBCreditWithDraw.Text, out int CraditwithDraw)&& CraditwithDraw >= 1 && CBTypePay.SelectedIndex != -1)
            {
                DGV_Testter.Rows.Clear();
                try
                {
                    if (MessageBox.Show("ยืนยันการจ่าย", "ระบบ", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        String Date = DTPDate.Value.ToString("yyyy:MM:dd");
                        Date = Date.Replace(":", "/");
                        DGV_Testter.Rows.Add(1,Bank.Menu.Date_Time_SQL_Now.Rows[0][0].ToString(), "ถอนหุ้นสะสม",TBWithDraw.Text);
                        Class.SQLConnection.InputSQLMSSQLDS((SQLDefault[2] +
                    "\r\n" +
                    SQLDefault[3])
                    .Replace("{ShareNo}", TBShareNo.Text)
                    .Replace("{WithDraw}", TBWithDraw.Text)
                    .Replace("{TeacherNoAddBy}", Class.UserInfo.TeacherNo)
                    .Replace("{PayMent}", Payment.No)
                    .Replace("{Date}" , Date));
                        MessageBox.Show("บันทึกสำเร็จ", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Class.Print.PrintPreviewDialog.info_Amounoff = TBWithDraw.Text;
                        Class.Print.PrintPreviewDialog.info_Payment = CBTypePay.Items[CBTypePay.SelectedIndex].ToString();
                        Class.Print.PrintPreviewDialog.info_TeacherAdd = Class.UserInfo.TeacherName;
                        Class.Print.PrintPreviewDialog.info_Savingtotel = Convert.ToInt32(Convert.ToInt32(TBSavingAmount.Text) - Convert.ToInt32(TBWithDraw.Text)).ToString();
                        printDocument1.DefaultPageSettings.PaperSize = new PaperSize("A4", 595, 842);
                        printDocument1.DefaultPageSettings.Landscape = true;
                        PinrtChcek = 1;
                        printDocument1.DocumentName = $"TeacherID{TBTeacherNo.Text}";
                        printDocument1.Print();
                        PinrtChcek = 0;
                        TBTeacherNo.Text = "";
                        TBTeacherNo_KeyDown(sender, new KeyEventArgs(Keys.Back));
                    }
                    CheckSave = true;
                }
                catch (Exception x)
                {
                    MessageBox.Show("ถอนเงินล้มเหลวกรุณาลองใหม่อีกครัง","ระบบ",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    Console.Write(x);
                }
             
            }
            else
                MessageBox.Show("ยอดเงินไม่เพียงพอ", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //Form ThisAmountOff = this;

            //FormCollection fc = Application.OpenForms;
            //foreach (Form f in fc)
            //{
            //    if (f.Name == "Menu")
            //    {
            //        f.Enabled = true;
            //        f.Show();
            //        break;
            //    }
            //}
            //if (fc.Count > 1)
            //    this.Close();

        }

        private void TBWithDraw_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!Char.IsNumber(e.KeyChar)) && (!Char.IsControl(e.KeyChar)))
            {
                if ((e.KeyChar < '0' || e.KeyChar > '9') && (e.KeyChar != '\b'))
                {
                    e.Handled = true;
                }
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
                Bank.Search IN = new Bank.Search(SQLDefault[14]
                    .Replace("{TeacherNoNotLike}", $"and a.TeacherNo NOT LIKE '{TBTeacherNo.Text}'"), "หุ้นสะสม");
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
            List<int> SUM = new List<int>();
            SUM.Clear();
            if (CBYear.SelectedIndex != -1)
            {
                DataSet ds = Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[6]
                    .Replace("{Date}", CBYear.SelectedItem.ToString() + "-")
                    .Replace("{TeacherNo}", TBTeacherNo.Text));

                DGVAmountOffHistory.Rows.Clear();
                for (int a = 0; a < ds.Tables[0].Rows.Count; a++)
                {
                    DGVAmountOffHistory.Rows.Add(a+1,ds.Tables[0].Rows[a][4], ds.Tables[0].Rows[a][0], ds.Tables[0].Rows[a][3]);
                    SUM.Add(Convert.ToInt32(ds.Tables[0].Rows[a][3].ToString()));
                }
                if (SUM.Count != 0)
                    LBalance_AmountOff.Text = SUM.Sum().ToString();
                if (ds.Tables[0].Rows.Count == 0 && tabControl1.SelectedIndex == 1)
                {
                    MessageBox.Show("ไม่พบรายการ", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        public void BMaxWithDraw_AmountOff_Click(object sender, EventArgs e)
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
                if (f.Name == "Menu" && f.Enabled == false)
                {
                    f.Enabled = true;
                    break;
                }
            }
        }

        private void TBWithDraw_TextChanged(object sender, EventArgs e)
        {
            BankTeacher.Class.FromSettingMedtod.ProtectedCtrlVTB(TBWithDraw);
            if (TBWithDraw.Text != "")
            {
                BSaveAmountOff.Enabled = true;
            }
            else
                BSaveAmountOff.Enabled = false;
        }
        private void printDocument1_PrintPage_1(object sender, PrintPageEventArgs e)
        {
            //======== INFO =================
            DataTable dt = Class.SQLConnection.InputSQLMSSQL(SQLDefault[10]);
            DataTable dt_date = Class.SQLConnection.InputSQLMSSQL(SQLDefault[9].Replace("{WithDrawNo}", dt.Rows[0][0].ToString()));
            Class.Print.PrintPreviewDialog.info_name = TBTeacherName.Text;
            Class.Print.PrintPreviewDialog.info_id = TBTeacherNo.Text;
            Class.Print.PrintPreviewDialog.info_ShareNo = TBShareNo.Text;
            Class.Print.PrintPreviewDialog.info_Loanstatus = TBLoanStatus.Text;
            Class.Print.PrintPreviewDialog.info_Amounoffinsystem = TBCreditSystem.Text;
            Class.Print.PrintPreviewDialog.info_canbeAmounoff = TBCreditWithDraw.Text;

            if (CB_SelectPrint.SelectedIndex == 0 && PinrtChcek != 1)
            {
                Class.Print.PrintPreviewDialog.details = 1;
                Class.Print.PrintPreviewDialog.PrintReportGrid(e, DGVAmountOffHistory, "ถอนหุ้นสะสม", this.AccessibilityObject.Name,false,false, "A4", 1);
            }
            else if(CB_SelectPrint.SelectedIndex == 1 && PinrtChcek != 1)
            {
                Class.Print.PrintPreviewDialog.PrintReportGrid(e, DGV_Testter, "ถอนหุ้นสะสม", this.AccessibilityObject.Name,checkBox_scrip.Checked,checkBox_copy.Checked, "A5", 0);
            }
            else
            { 
                Class.Print.PrintPreviewDialog.info_BillAmounoff = dt.Rows[0][0].ToString();
                Class.Print.PrintPreviewDialog.info_datepayAmounoff = dt_date.Rows[0][3].ToString();
                Class.Print.PrintPreviewDialog.PrintReportGrid(e, DGV_Testter, "ถอนหุ้นสะสม", this.AccessibilityObject.Name,true, true, "A5", 0);
            }
            Class.Print.PrintPreviewDialog.details = 0;
            if(Class.Print.PrintPreviewDialog.start_and_stop == 1 || Class.Print.PrintPreviewDialog.start_and_stop == 2)
            {
                TB_Bill.Text = "";
                Class.Print.PrintPreviewDialog.start_and_stop = 0;
            }
        }

        private void BExitForm_Click(object sender, EventArgs e)
        {
            //Add_Member.CancelMember FCancelMember = new Add_Member.CancelMember();
            FormCollection fc = Application.OpenForms;
            if (fc.Count > 2)
            {
                
                foreach (Form f in fc)
                {
                    if (f.Name == "Menu")
                    {
                        f.Enabled = true;
                        f.Show();
                        break;
                    }
                }
                this.Close();
            }
            else
                BankTeacher.Class.FromSettingMedtod.ReturntoHome(this);
        }

        private void AmountOff_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape || (CheckSave && e.KeyCode == Keys.Enter))
            {
                if (TBTeacherNo.Text.Length != 0)
                {
                    CBYear.DroppedDown = false;
                    CBYear.Items.Clear();
                    DGVAmountOffHistory.Rows.Clear();
                    TBTeacherNo.Text = "";
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
                    CBYear.Enabled = false;
                    CheckSave = false;
                    //BSaveAmountOff.Enabled = false;
                    Check = 0;
                    Checkmember(true);
    }
                else
                {
                    BExitForm_Click(new object(), new EventArgs());
                }
            }
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            if(DGVAmountOffHistory.Rows.Count == 0)
            {
                MessageBox.Show("ไม่พบรายการถอน กรูณาตรวจสอบใหม่อีกครั้ง", "การเเจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tabControl1.SelectedIndex = 0;
            }
        }
        //int SandCRonot = 0;
        private void BT_Print_Click(object sender, EventArgs e)
        {
            //// เลือก ต้น ฉบับ หรือ สำเนา หรือ ไม่
            //if (checkBox_scrip.Checked == true) { SandCRonot = 3; }
            //if (checkBox_copy.Checked == true) { SandCRonot = 4; }
            //if (checkBox_scrip.Checked == true && checkBox_copy.Checked == true) { SandCRonot = 1; }
            //if (checkBox_scrip.Checked == false && checkBox_copy.Checked == false) { SandCRonot = 0; }
            if (TB_Bill.Text == "") { DGV_Testter.Rows.Clear(); }
            if (DGVAmountOffHistory.Rows.Count != 0)
            {
                if (CB_SelectPrint.SelectedIndex == 1 && DGV_Testter.Rows.Count != 0)
                {
                    printDocument1.DefaultPageSettings.PaperSize = new PaperSize("A4", 595, 842);
                    printDocument1.DefaultPageSettings.Landscape = true;
                    if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                    {
                        printDocument1.Print();
                    }
                }
                else if(CB_SelectPrint.SelectedIndex == 0)
                {
                    printDocument1.DefaultPageSettings.PaperSize = new PaperSize("Letter", 850, 1100);
                    printDocument1.DefaultPageSettings.Landscape = false;
                    if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                    {
                        printDocument1.Print();
                    }
                }
                else MessageBox.Show("โปรเลือกเลขบิลล์ในตาราง หรือ กรอกเลขบิลล์ให้ถูกต้อง", "การเเจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Information); 
            }
            else
            {
                MessageBox.Show("โปรเลือกเลขบิลล์ในตาราง หรือ กรอกเลขบิลล์ให้ถูกต้อง", "การเเจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void DGVAmountOffHistory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex != -1)
            {
                DataTable dt = Class.SQLConnection.InputSQLMSSQL(SQLDefault[13].Replace("{WithDrawNo}", DGVAmountOffHistory.Rows[e.RowIndex].Cells[1].Value.ToString()));
                Class.Print.PrintPreviewDialog.info_BillAmounoff = dt.Rows[0][2].ToString();
                Class.Print.PrintPreviewDialog.info_datepayAmounoff = dt.Rows[0][3].ToString();
                Class.Print.PrintPreviewDialog.info_Amounoff = dt.Rows[0][4].ToString();
                Class.Print.PrintPreviewDialog.info_Payment = dt.Rows[0][5].ToString();
                Class.Print.PrintPreviewDialog.info_TeacherAdd = dt.Rows[0][6].ToString();
                Class.Print.PrintPreviewDialog.info_Savingtotel = TBSavingAmount.Text;
                TB_Bill.Text = DGVAmountOffHistory.Rows[e.RowIndex].Cells[1].Value.ToString();
                if (dt.Rows.Count != 0)
                {
                    DGV_Testter.Rows.Clear();
                    for (int loop = 0; loop < dt.Rows.Count; loop++)
                    {
                        DGV_Testter.Rows.Add(loop + 1, dt.Rows[0][3].ToString(), "ถอนเงิน", dt.Rows[0][4].ToString());
                    }
                }
            }
        }

        private void TB_Bill_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!Char.IsNumber(e.KeyChar)) && (!Char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }
        private void TB_Bill_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = Class.SQLConnection.InputSQLMSSQL(SQLDefault[13].Replace("{WithDrawNo}", TB_Bill.Text));
                Class.Print.PrintPreviewDialog.info_BillAmounoff = dt.Rows[0][2].ToString();
                Class.Print.PrintPreviewDialog.info_datepayAmounoff = dt.Rows[0][3].ToString();
                Class.Print.PrintPreviewDialog.info_Amounoff = dt.Rows[0][4].ToString();
                Class.Print.PrintPreviewDialog.info_Payment = dt.Rows[0][5].ToString();
                Class.Print.PrintPreviewDialog.info_TeacherAdd = dt.Rows[0][6].ToString();
                if (dt.Rows.Count != 0)
                {
                    DGV_Testter.Rows.Clear();
                    for (int loop = 0; loop < dt.Rows.Count; loop++)
                    {
                        DGV_Testter.Rows.Add(loop + 1, dt.Rows[0][3].ToString(), "ถอนเงิน", dt.Rows[0][4].ToString());
                    }
                }
            }
            catch { }
        }
        private void Checkmember(bool tf)
        {
            TBTeacherNo.Enabled = tf;
        }

        private void AmountOff_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                if (tabControl1.SelectedIndex == tabControl1.TabCount - 1)
                {
                    tabControl1.SelectedIndex = 0;
                }
                else
                {
                    tabControl1.SelectedIndex = tabControl1.SelectedIndex + 1;
                }
            }
        }
    }
}