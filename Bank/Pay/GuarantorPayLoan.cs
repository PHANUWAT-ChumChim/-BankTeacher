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
using System.Threading;
using System.Diagnostics;
using System.Reflection;

namespace BankTeacher.Bank.Pay
{
    public partial class GuarantorPayLoan : Form
    {
        /// <summary> 
        /// SQLDefault 
        /// <para>[0] Search Member INPUT: {Text} , {TeacherNoNotLike}</para>
        /// <para>[1] Textbox Search Member INPUT: {TeacherNo}</para>
        /// <para>[2] Find a Loan list as a Teacher Guarantor INPUT: {TeacherNo}</para>
        /// <para>[3] SELECT lasted billno INPUT: </para>
        /// <para>[4] Select DetailLoan INPUT: {LoanNo} </para>
        /// <para>[5] Insert Bill Loanlist INPUT: {TeacherNoPay} {TeacherNoAddBy} {LoanNo} {Date}</para>
        /// <para>[6]Insert BillDetails Use ForLoop INPUT: {BillNo},{TypeNo},{LoanNo},{Amount},{Month},{Year},{BillDetailPaymentNo}</para>
        /// <para>[7]Update Guarantor and Loan (BSavePayLoop) INPUT: {LoanAmount}  {LoanNo} {TeacharNo} {Amount}</para>
        /// <para>[8] Check if you have paid ( Loan ) INPUT: {LoanNo} , {Month} , {Year} , {Date} </para>
        /// <para>[9] Payment Choice INPUT: </para>
        /// </summary> 
        private String[] SQLDefault = new String[]
         { 
           //[0] Search Member INPUT: {Text} , {TeacherNoNotLike}
           "SELECT TOP(20)a.TeacherNo , CAST(ISNULL(e.PrefixName,'')+d.Fname + ' ' + Lname as nvarchar(255))as Name  \r\n " +
          "FROM EmployeeBank.dbo.tblMember as a \r\n " +
          "INNER JOIN EmployeeBank.dbo.tblGuarantor as b on a.TeacherNo = b.TeacherNo \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblLoan as c on b.LoanNo = c.LoanNo \r\n " +
          "LEFT JOIN Personal.dbo.tblTeacherHis as  d on a.TeacherNo = d.TeacherNo \r\n " +
          "LEFT JOIN BaseData.dbo.tblPrefix as e on d.PrefixNo = e.PrefixNo \r\n " +
          "WHERE LoanStatusNo = 2 and MemberStatusNo = 1 and (a.TeacherNo LIKE '%{Text}%' or CAST(ISNULL(e.PrefixName,'')+d.Fname + ' ' + Lname as nvarchar(255)) LIKE '%{Text}%') and a.TeacherNo != '{TeacherNoNotLike}' \r\n " +
          "GROUP BY a.TeacherNo , CAST(ISNULL(e.PrefixName,'')+d.Fname + ' ' + Lname as nvarchar(255))"
           ,
           //[1] Textbox Search Member INPUT: {TeacherNo}
           "SELECT TOP(20)a.TeacherNo , CAST(ISNULL(e.PrefixName,'')+d.Fname + ' ' + Lname as nvarchar(255))as Name  \r\n " +
          "FROM EmployeeBank.dbo.tblMember as a \r\n " +
          "INNER JOIN EmployeeBank.dbo.tblGuarantor as b on a.TeacherNo = b.TeacherNo \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblLoan as c on b.LoanNo = c.LoanNo \r\n " +
          "LEFT JOIN Personal.dbo.tblTeacherHis as  d on a.TeacherNo = d.TeacherNo \r\n " +
          "LEFT JOIN BaseData.dbo.tblPrefix as e on d.PrefixNo = e.PrefixNo \r\n " +
          "WHERE LoanStatusNo = 2 and MemberStatusNo = 1 and a.TeacherNo = '{TeacherNo}' \r\n " +
          "GROUP BY a.TeacherNo , CAST(ISNULL(e.PrefixName,'')+d.Fname + ' ' + Lname as nvarchar(255))"
           ,
           //[2] Find a Loan list as a Teacher Guarantor INPUT: {TeacherNo}  CAST('รายการกู้ : ( ' + CAST(a.LoanNo as nvarchar(255)) + ' ) ' + CAST(b.YearPay as nvarchar(10)) + ' / ' +CAST( b.MonthPay as nvarchar(10)) as nvarchar(255))
           "SELECT CAST('รายการกู้ : ( ' + CAST(a.LoanNo as nvarchar(255)) + ' ) ' as nvarchar(255)), a.LoanNo \r\n " +
          "FROM EmployeeBank.dbo.tblGuarantor as a \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblLoan as b on a.LoanNo = b.LoanNo \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblMember as c on a.TeacherNo = c.TeacherNo \r\n " +
          "WHERE a.TeacherNo = '{TeacherNo}' and LoanStatusNo = 2 and c.MemberStatusNo = 1"
           ,
           //[3] SELECT lasted billno INPUT: 
          "SELECT IDENT_CURRENT('EmployeeBank.dbo.tblBill')+1 "
             ,
          //[4] Select DetailLoan INPUT: {LoanNo} 
           "SELECT    LoanNo,ROUND(Convert(float, ( (InterestRate / 100) * LoanAmount)/ PayNo) ,0) + ROUND(Convert(float ,  LoanAmount / PayNo),0) AS PayLoan , \r\n " +
          "CEILING((LoanAmount  + Convert(float , (InterestRate / 100) * LoanAmount)) - (ROUND(Convert(float, ( (InterestRate / 100) * LoanAmount)/ PayNo) ,0) + ROUND(Convert(float , LoanAmount / PayNo),0)) * (PayNo -1)) AS LastPay,  \r\n " +
          " CEILING((LoanAmount  + Convert(float , (InterestRate / 100) * LoanAmount)) - (ROUND(Convert(float, ( (InterestRate / 100) * LoanAmount)/ PayNo) ,0) + ROUND(Convert(float , LoanAmount / PayNo),0)) * (PayNo -1) + CEILING((InterestRate / 100) * LoanAmount)) AS Latepayment , \r\n " +
          "EOMONTH(CAST(CAST(CAST(YearPay as nvarchar) +'/' + CAST(MonthPay AS nvarchar) + '/05' AS nvarchar(255)) AS date)) AS StartLoan ,   \r\n " +
          "EOMONTH(DATEADD(MONTH,PayNo-1,CAST(CAST(CAST(YearPay as nvarchar) +'/' + CAST(MonthPay AS nvarchar) + '/05' AS nvarchar) AS date))) AS EndLoan  , PayNo - 1 as PayNo\r\n " +
          "FROM EmployeeBank.dbo.tblLoan \r\n " +
          "WHERE LoanNo = '{LoanNo}' and LoanStatusNo = 2"
           ,
           //[5] Insert Bill Loanlist INPUT: {TeacherNoPay} {TeacherNoAddBy} {LoanNo} {Date}
           "DECLARE @BIllNO INT;   \r\n " +
          "DECLARE @TeacherNoLoan varchar(30); \r\n " +
          " \r\n " +
          "SET @TeacherNoLoan = ( SELECT a.TeacherNo \r\n " +
          "FROM EmployeeBank.dbo.tblLoan as a \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblGuarantor as b on a.LoanNo = b.LoanNo \r\n " +
          "WHERE b.TeacherNo = '{TeacherNoPay}' and b.LoanNo = {LoanNo} and a.LoanStatusNo = 2) \r\n " +
          " \r\n " +
          " INSERT INTO EmployeeBank.dbo.tblBill (TeacherNoAddBy,TeacherNo,TeacherNoPay,DateAdd,Cancel,TransactionDate)   \r\n " +
          " VALUES('{TeacherNoAddBy}',@TeacherNoLoan,'{TeacherNoPay}','{Date}',1,CURRENT_TIMESTAMP);   \r\n " +
          " SET @BIllNO = SCOPE_IDENTITY();   \r\n " +
          " SELECT @BIllNO ;"
           ,
           //[6]Insert BillDetails Use ForLoop INPUT: {BillNo},{TypeNo},{LoanNo},{Amount},{Month},{Year},{BillDetailPaymentNo}
           "INSERT INTO EmployeeBank.dbo.tblBillDetail (BillNo,TypeNo,LoanNo,Amount,Mount,Year,BillDetailPaymentNo)  \r\n " +
          "VALUES ({BillNo},{TypeNo},{LoanNo},{Amount},{Month},{Year},{BillDetailPaymentNo});"
           ,
           //[7]Update Guarantor and Loan (BSavePayLoop) INPUT: {LoanAmount}  {LoanNo} {TeacharNo} {Amount}
           "-- ลบ คนค้ำ + ปิดกู้ \r\n " +
           "DECLARE @CountPayLoan int \r\n" +
           "DECLARE @PayNo int \r\n" +
           "\r\n" +
          "SELECT @CountPayLoan = COUNT(b.BillDetailNo) \r\n " +
          "FROM EmployeeBank.dbo.tblBill as a \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblBillDetail as b on a.BillNo = b.BillNo \r\n " +
          "WHERE b.LoanNo = {LoanNo} and a.Cancel = 1 \r\n " +
          " \r\n " +
          "SELECT @PayNo = PayNo FROM EmployeeBank.dbo.tblLoan WHERE LoanNo = {LoanNo} and LoanStatusNo = 2; \r\n " +
          " \r\n " +
          "IF (@PayNo = @CountPayLoan) \r\n " +
          "BEGIN \r\n " +
          "UPDATE EmployeeBank.dbo.tblGuarantor  \r\n " +
          "SET RemainsAmount = 0 \r\n " +
          "WHERE EmployeeBank.dbo.tblGuarantor.LoanNo = {LoanNo}; \r\n " +
          " \r\n " +
          " --- ลบ ดอกเบี้ย หุ้นสะสสม \r\n " +
          "update EmployeeBank.dbo.tblShare \r\n " +
          "set SavingAmount = SavingAmount - '{Amount}' \r\n " +
          "WHERE TeacherNo = '{TeacherNo}' \r\n " +
          "--------------------------------------- \r\n " +
          "\r\n  "+
          "UPDATE EmployeeBank.dbo.tblLoan \r\n " +
          "SET LoanStatusNo = 3 \r\n " +
          "WHERE LoanNo = {LoanNo} \r\n " +
          "END \r\n " +
          "ELSE \r\n " +
          "BEGIN \r\n " +
          "UPDATE EmployeeBank.dbo.tblGuarantor  \r\n " +
          "SET RemainsAmount = EmployeeBank.dbo.tblGuarantor.RemainsAmount - (SELECT ((a.Amount * 100 ) / (b.LoanAmount * (b.InterestRate/100) + b.LoanAmount) * {LoanAmount} / 100) as AmountPerTeacher \r\n " +
          "FROM EmployeeBank.dbo.tblGuarantor as a  \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblLoan as b on a.LoanNo = b.LoanNo  \r\n " +
          "WHERE a.LoanNo = {LoanNo} and EmployeeBank.dbo.tblGuarantor.TeacherNo LIKE a.TeacherNo)  \r\n " +
          "WHERE EmployeeBank.dbo.tblGuarantor.LoanNo = {LoanNo} \r\n " +
          "END"
           ,
           //[8] Check if you have paid ( Loan ) INPUT: {LoanNo} , {Month} , {Year} , {Date} 
           "  SELECT a.TeacherNo, \r\n " +
          "  ROUND(Convert(float, ( (g.InterestRate / 100) * LoanAmount)/ PayNo) ,0) + ROUND(Convert(float , LoanAmount / PayNo),0)  AS PayLoan, \r\n " +
          "  CEILING((LoanAmount  + Convert(float , (InterestRate / 100) * LoanAmount)) - (ROUND(Convert(float, ( (InterestRate / 100) * LoanAmount)/ PayNo) ,0) + ROUND(Convert(float , LoanAmount / PayNo),0)) * (PayNo -1)) AS LastPay,   \r\n " +
             "CEILING((LoanAmount  + Convert(float , (InterestRate / 100) * LoanAmount)) - (ROUND(Convert(float, ( (InterestRate / 100) * LoanAmount)/ PayNo) ,0) + ROUND(Convert(float , LoanAmount / PayNo),0)) * (PayNo -1) + CEILING((InterestRate / 100) * LoanAmount)) AS Latepayment  \r\n"+
          "  FROM EmployeeBank.dbo.tblMember as a    \r\n " +
          "  LEFT JOIN EmployeeBank.dbo.tblBill as b on a.TeacherNo = b.TeacherNo   \r\n " +
          "  LEFT JOIN EmployeeBank.dbo.tblBillDetail as c on b.BillNo = c.BillNo   \r\n " +
          "  LEFT JOIN Personal.dbo.tblTeacherHis as d on a.TeacherNo = d.TeacherNo    \r\n " +
          "  LEFT JOIN BaseData.dbo.tblPrefix as e on d.PrefixNo = e.PrefixNo    \r\n " +
          "  LEFT JOIN EmployeeBank.dbo.tblBillDetailType as f on c.TypeNo = f.TypeNo   \r\n " +
          "  LEFT JOIN EmployeeBank.dbo.tblLoan as g on a.TeacherNo = g.TeacherNo   \r\n " +
          " \r\n " +
          "   \r\n " +
          "  WHERE (a.TeacherNo NOT IN     \r\n " +
          "  (SELECT aa.TeacherNo  \r\n " +
          "  FROM EmployeeBank.dbo.tblBill as aa     \r\n " +
          "  LEFT JOIN EmployeeBank.dbo.tblBillDetail as bb on aa.BillNo = bb.BillNo     \r\n " +
          "  LEFT JOIN EmployeeBank.dbo.tblLoan as cc on bb.LoanNo = cc.LoanNo \r\n " +
          "  LEFT JOIN EmployeeBank.dbo.tblMember as dd on aa.TeacherNo = dd.TeacherNo \r\n " +
          "  WHERE bb.Mount = '{Month}' and bb.Year = '{Year}' and aa.Cancel = 1   \r\n " +
          "  and dd.MemberStatusNo = 1 and bb.TypeNo = 2  and LoanStatusNo = 2 )and  g.LoanNo = {LoanNo}   and c.TypeNo = 2 and LoanStatusNo =2 and DATEADD(YYYY,0,'{Date}') <= EOMONTH(DATEADD(MONTH , PayNo-1,CAST(CAST(CAST(YearPay as nvarchar) +'/' + CAST(MonthPay AS nvarchar) + '/01' AS nvarchar) AS date)))) and (DATEADD(YYYY,0,'{Date}') >= EOMONTH(CAST(YearPay as nvarchar) +'/' + CAST(MonthPay as nvarchar) +'/01') ) \r\n " +
          "   and b.Cancel = 1  \r\n " +
          "  GROUP BY   a.TeacherNo, \r\n"+
          "ROUND(Convert(float, ( (g.InterestRate / 100) * LoanAmount)/ PayNo) ,0) + ROUND(Convert(float , LoanAmount / PayNo),0) , \r\n"+
          "CEILING((LoanAmount  + Convert(float , (InterestRate / 100) * LoanAmount)) - (ROUND(Convert(float, ( (InterestRate / 100) * LoanAmount)/ PayNo) ,0) + ROUND(Convert(float , LoanAmount / PayNo),0)) * (PayNo -1)) , \r\n"+
          "CEILING((LoanAmount  + Convert(float , (InterestRate / 100) * LoanAmount)) - (ROUND(Convert(float, ( (InterestRate / 100) * LoanAmount)/ PayNo) ,0) + ROUND(Convert(float , LoanAmount / PayNo),0)) * (PayNo -1) + CEILING((InterestRate / 100) * LoanAmount)) \r\n "+
          " \r\n " +


          "  SELECT LoanNo ,MonthPay , YearPay , PayNo,  \r\n " +
          "   ROUND(Convert(float, ( (InterestRate / 100) * LoanAmount)/ PayNo) ,0) + ROUND(Convert(float ,  LoanAmount / PayNo),0) AS PayLoan , \r\n " +
          "	CEILING((LoanAmount  + Convert(float , (InterestRate / 100) * LoanAmount)) - (ROUND(Convert(float, ( (InterestRate / 100) * LoanAmount)/ PayNo) ,0) + ROUND(Convert(float , LoanAmount / PayNo),0)) * (PayNo -1)) AS LastPay,   \r\n " +
          "    EOMONTH(DATEADD(MONTH,PayNo-1,CAST(CAST(CAST(YearPay as nvarchar) +'/' + CAST(MonthPay AS nvarchar) + '/05' AS nvarchar) AS date))) AS EndLoan ,\r\n " +
          "CEILING((LoanAmount  + Convert(float , (InterestRate / 100) * LoanAmount)) - (ROUND(Convert(float, ( (InterestRate / 100) * LoanAmount)/ PayNo) ,0) + ROUND(Convert(float , LoanAmount / PayNo),0)) * (PayNo -1) + CEILING((InterestRate / 100) * LoanAmount)) AS Latepayment  \r\n"+
          "   FROM EmployeeBank.dbo.tblLoan \r\n " +
          "   WHERE LoanNo = {LoanNo} and LoanStatusNo = 2 ;  \r\n " +
          " \r\n " +



          " SELECT  c.BillNo  \r\n " +
          "  FROM EmployeeBank.dbo.tblLoan as a \r\n " +
          "  LEFT JOIN EmployeeBank.dbo.tblBillDetail as b on a.LoanNo = b.LoanNo \r\n " +
          "  LEFT JOIN EmployeeBank.dbo.tblBill as c on b.BillNo = c.BillNo \r\n " +
          "  WHERE b.LoanNo = {LoanNo} and  Cancel = 1 and Mount ={Month} and Year = {Year}; \r\n "
           ,
           //[9] Payment Choice INPUT: 
           "SELECT Convert(nvarchar(50) , Name) , BillDetailpaymentNo  \r\n " +
          "FROM EmployeeBank.dbo.tblBillDetailPayment \r\n " +
          "WHERE Status = 1 "
         };
        bool CheckSave = false; 
        List<List<int>> DMLoanlist = new List<List<int>>();
        List<int> YearInCBLoanlist = new List<int>();
        DateTime DateLastofPay_Loanlist = new DateTime();
        int NormalPrice_Loanlist = 0;
        int LastofMonthPrice_Loanlist = 0;
        int Paylate_Loanlist = 0;
        int[] Endloan_loanlist = { 1999, 1 };
        int SelectIndexRow = -1;
        static int NumPrint = 0;
        public GuarantorPayLoan()
        {
            InitializeComponent();
            Font F = new Font("TH Sarabun New", 16, FontStyle.Regular);

            var paperSize = printDocument1.PrinterSettings.PaperSizes.Cast<System.Drawing.Printing.PaperSize>().FirstOrDefault(e => e.PaperName == "A5");
            printDocument1.PrinterSettings.DefaultPageSettings.PaperSize = paperSize;
        }

        //ChangeSizeForm
        private void Menuf_SizeChanged(object sender, EventArgs e)
        {
            int x = this.Width / 2 - panel1.Size.Width / 2;
            int y = this.Height / 2 - panel1.Size.Height / 2;
            panel1.Location = new Point(x, y);
        }

        //Load Form
        private void GuarantorPayLoan_Load(object sender, EventArgs e)
        {
            ComboBox[] cbb = new ComboBox[] { CBPayment_Loanlist };
            //SQL หารูปแบบการโอนเงิน
            DataTable dtt = Class.SQLConnection.InputSQLMSSQL(SQLDefault[9]);
            //ยัดรูปแบบการโอนเงินใส่ใน CBPayment_Loanlist และเก็บค่า id ของรูปแบบนั้นๆลงไปที่ Class Combobox
            for (int a = 0; a < dtt.Rows.Count; a++)
                for (int x = 0; x < cbb.Length; x++)
                    cbb[x].Items.Add(new BankTeacher.Class.ComboBoxPayment(dtt.Rows[a][0].ToString(),
                        dtt.Rows[a][1].ToString()));

            DTPDate.Value = Convert.ToDateTime(Bank.Menu.Date[0] + "/" + Bank.Menu.Date[1] + "/" + Bank.Menu.Date[2]);
            if (BankTeacher.Bank.Menu.DateAmountChange == 1)
                DTPDate.Enabled = true;
            else
                DTPDate.Enabled = false;
        }
        //==================================Header===================================================
        //SearchButton
        private void BSearchTeacher_Click(object sender, EventArgs e)
        {
            //เปิดหน้าค้นหาแล้วให้ใส่ Code จาก SQLDefault[0] ที่ใช้สำหรับค้นหาสมาชิก
            Bank.Search IN = new Bank.Search(SQLDefault[0]
                .Replace("{TeacherNoNotLike}", TBTeacherNo.Text));
            IN.ShowDialog();
            //ถ้า ID สมาชิกที่เลือกไม่เป็นว่างเปล่า ให้ ใส่ลงใน TBTeacherNo และ ไปทำ event Keydown ของ TBTeacherNo
            if (Bank.Search.Return[0] != "")
            {
                TBTeacherNo.Text = Bank.Search.Return[0];
                TBTeacherName.Text = BankTeacher.Bank.Search.Return[1];
                TBTeacherNo_KeyDown(sender, new KeyEventArgs(Keys.Enter));
            }
        }
        private void TBTeacherNo_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                TBTeacherNo.Text = TBTeacherNo.Text.Replace("t", "T");
                if(Class.SQLConnection.InputSQLMSSQL(SQLDefault[1].Replace("{TeacherNo}",TBTeacherNo.Text)).Rows.Count != 0)
                {
                    DataTable dtLoanlist = Class.SQLConnection.InputSQLMSSQL(SQLDefault[2]
                        .Replace("{TeacherNo}", TBTeacherNo.Text));
                    if (dtLoanlist.Rows.Count != 0)
                    {
                        ComboBox[] CB = new ComboBox[] { CBLoanlist };
                        for (int x = 0; x < dtLoanlist.Rows.Count; x++)
                            for (int y = 0; y < CB.Length; y++)
                                CB[y].Items.Add(new Class.loanlist(dtLoanlist.Rows[x][0].ToString(), dtLoanlist.Rows[x][1].ToString()));
                        if (CBLoanlist.Items.Count != 0)
                        {
                            CBLoanlist.SelectedIndex = 0;
                            CBLoanlist.Enabled = true;
                            //CBLoanlist_SelectedIndexChanged(new object(), new EventArgs());
                        }
                        CBLoanlist.Enabled = true;
                    }
                    else
                    {
                        CBLoanlist.Enabled = false;
                    }
                    TBTeacherNo.Enabled = false;
                }
                else
                {
                    MessageBox.Show("ไม่พบรหัสนี้ในระบบ กรุณาลองใหม่อีกครั้ง","ระบบ",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }
            }
        }

        private void TBTeacherNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!Char.IsNumber(e.KeyChar)) && (!Char.IsControl(e.KeyChar)) && !(e.KeyChar != ' '))
            {
                e.Handled = true;
            }
        }

        private void printDocument2_PrintPage_1(object sender, PrintPageEventArgs e)
        {
            Class.Print.PrintPreviewDialog.PrintReportGrid(e, DGV_PayLoan, "ใบเสร็จรับเงินการจ่ายกู้", this.AccessibilityObject.Name, true, true, "A5", 0);
        }

        private void GuarantorPayLoan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape || (CheckSave && e.KeyCode == Keys.Enter))
            {
                if (TBTeacherNo.Text.Length != 0)
                {
                    TBTeacherNo.Text = "";
                    TBTeacherName.Text = "";
                    TBTeacherBill.Text = "";
                    BSearchTeacher.Enabled = true;
                    TBTeacherNo.Enabled = true;
                    TBTeacherNo.Enabled = true;
                    CheckSave = false;

                    ClearForm();
                }
                else
                {
                    BExitForm_Click(new object(), new EventArgs());
                }
            }
        }
        private void ClearForm()
        {
            CBLoanlist.Items.Clear();
            DGV_Loanlist.Rows.Clear();
            CBMonth_Loanlist.Items.Clear();
            CBYear_Loanlist.Items.Clear();
            TBAmount_Loanlist.Text = "0";
            CBLoanlist.Enabled = false;
            CBMonth_Loanlist.Enabled = false;
            CBYear_Loanlist.Enabled = false;
            BTSave_Loanlist.Enabled = false;
            CBPayment_Loanlist.Enabled = false;
            BTAddlist_Loanlist.Enabled = false;
            CBPayment_Loanlist.SelectedIndex = -1;

            if (DGV_Loanlist.Rows.Count == 0)
            {
                bool Checked = false;
                if (BankTeacher.Bank.Menu.DateAmountChange == 1)
                    Checked = true;
                DTPDate.Enabled = Checked;
            }
        }

        private void BExitForm_Click(object sender, EventArgs e)
        {
            BankTeacher.Class.FromSettingMedtod.ReturntoHome(this);
        }

        private void CBLoanlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CBLoanlist.SelectedIndex != -1)
            {
                DateLastofPay_Loanlist = new DateTime();
                CBYear_Loanlist.Items.Clear();
                CBMonth_Loanlist.Items.Clear();
                DGV_Loanlist.Rows.Clear();
                YearInCBLoanlist.Clear();
                DMLoanlist.Clear();
                LBSum_Loanlist.Text = "0";
                NormalPrice_Loanlist = 0;
                LastofMonthPrice_Loanlist = 0;
                Paylate_Loanlist = 0;

                Class.loanlist Ll = (CBLoanlist.SelectedItem as Class.loanlist);
                DataTable dt = Class.SQLConnection.InputSQLMSSQL(SQLDefault[4]
                    .Replace("{LoanNo}", Ll.LoanID));
                if (dt.Rows.Count != 0)
                {
                    int Year = 0;
                    int Month = 0;
                    int PayNo = 0;
                    DateLastofPay_Loanlist = Convert.ToDateTime(Convert.ToDateTime(dt.Rows[0][5].ToString()).ToString("yyyy-MM-dd"));
                    Year = Convert.ToInt32(Convert.ToDateTime(dt.Rows[0][4].ToString()).ToString("yyyy"));
                    Month = Convert.ToInt32(Convert.ToDateTime(dt.Rows[0][4].ToString()).ToString("MM"));
                    PayNo = Convert.ToInt32(dt.Rows[0][6].ToString());
                    int MonthplusPayNo = Month + PayNo;
                    int lastyearofpay = Convert.ToInt32(Convert.ToDateTime(dt.Rows[0][5].ToString()).ToString("yyyy"));
                    int lastmonthofpay = Convert.ToInt32(Convert.ToDateTime(dt.Rows[0][5].ToString()).ToString("MM"));

                    DateTime startDatePayLoan = Convert.ToDateTime(dt.Rows[0][4].ToString());
                    DateTime EndDatePayLoan = Convert.ToDateTime(dt.Rows[0][5].ToString());


                    Endloan_loanlist[0] = lastyearofpay;
                    Endloan_loanlist[1] = lastmonthofpay;

                    NormalPrice_Loanlist = Convert.ToInt32(dt.Rows[0][1].ToString());
                    LastofMonthPrice_Loanlist = Convert.ToInt32(dt.Rows[0][2].ToString());
                    Paylate_Loanlist = Convert.ToInt32(dt.Rows[0][3].ToString());

                    int RemoveCount = 0;
                    YearInCBLoanlist.Add(Year);
                    DMLoanlist.Add(new List<int>());
                    String CountMonthFormatDatetime = "01";


                    for (int Count = Month; Count <= 12; Count++)
                    {
                        CountMonthFormatDatetime = Count.ToString();
                        if (Count < 10)
                            CountMonthFormatDatetime = "0" + Count.ToString();
                        DataSet ds = Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[8]
                            .Replace("{LoanNo}", Ll.LoanID)
                            .Replace("{Year}", YearInCBLoanlist[0].ToString())
                            .Replace("{Month}", Count.ToString())
                            .Replace("{Date}", (Convert.ToDateTime(YearInCBLoanlist[0].ToString() + '-' + CountMonthFormatDatetime.ToString() + '-' + (DateTime.DaysInMonth(YearInCBLoanlist[0], Count)).ToString())).ToString("yyyy-MM-dd")));

                        if (ds.Tables[2].Rows.Count != 0)
                            continue;

                        DateTime Now = Convert.ToDateTime((Convert.ToDateTime((YearInCBLoanlist[0].ToString() + '-' + Count.ToString() + '-' +
                                        DateTime.DaysInMonth(Convert.ToInt32(YearInCBLoanlist[0].ToString()), Count))
                                        .ToString())).ToString("yyyy-MM-dd"));
                        if (Now >= startDatePayLoan && Now <= EndDatePayLoan && ds.Tables[2].Rows.Count == 0)
                        {
                            DMLoanlist[0].Add(Count);
                            RemoveCount++;
                        }
                        else if (ds.Tables[0].Rows.Count >= 1)
                        {
                            DMLoanlist[0].Add(Count);
                            RemoveCount++;
                        }
                        else
                        {
                            continue;
                        }
                    }

                    CBYear_Loanlist.Items.Add(YearInCBLoanlist[0].ToString());
                    //MonthplusPayNo -= RemoveCount;
                    if (MonthplusPayNo > 12)
                        while (MonthplusPayNo > 12)
                        {
                            MonthplusPayNo -= 12;
                            Year++;
                            if (YearInCBLoanlist.Count != 0)
                            {
                                for (int x = 0; x < YearInCBLoanlist.Count; x++)
                                {
                                    if (YearInCBLoanlist[x] == Year)
                                        break;
                                    else if (x == YearInCBLoanlist.Count - 1)
                                    {
                                        YearInCBLoanlist.Add(Year);
                                        DMLoanlist.Add(new List<int>());
                                    }
                                }
                            }
                            else
                            {
                                YearInCBLoanlist.Add(Year);
                                DMLoanlist.Add(new List<int>());
                            }
                        }
                    else if (MonthplusPayNo > 0)
                    {
                        if (YearInCBLoanlist.Count != 0)
                        {
                            for (int x = 0; x < YearInCBLoanlist.Count; x++)
                            {
                                if (YearInCBLoanlist[x] == Year)
                                    break;
                                else if (x == YearInCBLoanlist.Count - 1)
                                {
                                    YearInCBLoanlist.Add(Year);
                                    DMLoanlist.Add(new List<int>());
                                }
                            }
                        }
                        else
                        {
                            YearInCBLoanlist.Add(Year);
                            DMLoanlist.Add(new List<int>());
                        }
                    }
                    for (int x = 1; x < YearInCBLoanlist.Count; x++)
                    {
                        CBYear_Loanlist.Items.Add(YearInCBLoanlist[x].ToString());
                        if (YearInCBLoanlist[x].ToString() == lastyearofpay.ToString())
                        {
                            for (int y = 1; y <= lastmonthofpay; y++)
                            {
                                CountMonthFormatDatetime = y.ToString();
                                if (y < 10)
                                    CountMonthFormatDatetime = "0" + y.ToString();
                                DataSet ds = Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[8]
                                    .Replace("{LoanNo}", Ll.LoanID)
                                    .Replace("{Year}", YearInCBLoanlist[x].ToString())
                                    .Replace("{Month}", y.ToString())
                                    .Replace("{Date}", (Convert.ToDateTime(YearInCBLoanlist[x].ToString() + '-' + CountMonthFormatDatetime.ToString() + '-' + (DateTime.DaysInMonth(YearInCBLoanlist[x], y)).ToString())).ToString("yyyy-MM-dd")));

                                if (ds.Tables[2].Rows.Count != 0)
                                    continue;

                                DateTime Now = Convert.ToDateTime((Convert.ToDateTime((YearInCBLoanlist[x].ToString() + '-' + y.ToString() + '-' +
                                        DateTime.DaysInMonth(Convert.ToInt32(YearInCBLoanlist[x].ToString()), y))
                                        .ToString())).ToString("yyyy-MM-dd"));
                                if (Now >= startDatePayLoan && Now <= EndDatePayLoan && ds.Tables[2].Rows.Count == 0)
                                {
                                    DMLoanlist[x].Add(y);
                                }
                                else if (ds.Tables[0].Rows.Count >= 1)
                                {
                                    DMLoanlist[x].Add(y);
                                }
                                else
                                {
                                    continue;
                                }
                            }
                        }
                        else
                        {
                            for (int y = 1; y <= 12; y++)
                            {
                                CountMonthFormatDatetime = y.ToString();
                                if (y < 10)
                                    CountMonthFormatDatetime = "0" + y.ToString();
                                DataSet ds = Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[8]
                                    .Replace("{LoanNo}", Ll.LoanID)
                                    .Replace("{Year}", YearInCBLoanlist[x].ToString())
                                    .Replace("{Month}", y.ToString())
                                    .Replace("{Date}", (Convert.ToDateTime(YearInCBLoanlist[x].ToString() + '-' + CountMonthFormatDatetime.ToString() + '-' + (DateTime.DaysInMonth(YearInCBLoanlist[x], y)).ToString())).ToString("yyyy-MM-dd")));

                                if (ds.Tables[2].Rows.Count != 0)
                                    continue;

                                DateTime Now = Convert.ToDateTime((Convert.ToDateTime((YearInCBLoanlist[x].ToString() + '-' + y.ToString() + '-' +
                                        DateTime.DaysInMonth(Convert.ToInt32(YearInCBLoanlist[x].ToString()), y))
                                        .ToString())).ToString("yyyy-MM-dd"));
                                if (Now >= startDatePayLoan && Now <= EndDatePayLoan && ds.Tables[2].Rows.Count == 0)
                                {
                                    DMLoanlist[x].Add(y);
                                }
                                else if (ds.Tables[0].Rows.Count >= 1)
                                {
                                    DMLoanlist[x].Add(y);
                                }
                                else
                                {
                                    continue;
                                }
                            }
                        }
                        for(int CY = 0; CY < DMLoanlist.Count; CY++)
                        {
                            if(DMLoanlist[CY].Count == 0)
                            {
                                DMLoanlist.RemoveAt(CY);
                                YearInCBLoanlist.RemoveAt(CY);
                                CBYear_Loanlist.Items.RemoveAt(CY);
                            }
                        }
                    }
                    if (CBYear_Loanlist.Items.Count != 0)
                    {
                        CBYear_Loanlist.SelectedIndex = 0;
                        CBYear_Loanlist.Enabled = true;
                    }
                    else
                    {
                        CBYear_Loanlist.Enabled = false;
                        CBYear_Loanlist.SelectedIndex = -1;
                        CBMonth_Loanlist.Enabled = false;
                        CBMonth_Loanlist.SelectedIndex = -1;
                        BTAddlist_Loanlist.Enabled = false;
                        CBPayment_Loanlist.Enabled = false;
                        CBPayment_Loanlist.SelectedIndex = -1;
                        BTClear_Loanlist.Enabled = false;
                        BTSave_Loanlist.Enabled = false;
                    }
                }
                else
                {
                    CBYear_Loanlist.Enabled = false;
                    CBYear_Loanlist.SelectedIndex = -1;
                    CBMonth_Loanlist.Enabled = false;
                    CBMonth_Loanlist.SelectedIndex = -1;
                    BTAddlist_Loanlist.Enabled = false;
                    CBPayment_Loanlist.Enabled = false;
                    CBPayment_Loanlist.SelectedIndex = -1;
                    BTClear_Loanlist.Enabled = false;
                    BTSave_Loanlist.Enabled = false;
                    MessageBox.Show("ไม่พบรายการกู้ดังกล่าว", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void CBYear_Loanlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            CBMonth_Loanlist.Items.Clear();
            if (CBYear_Loanlist.SelectedIndex != -1)
            {
                for (int x = 0; x < YearInCBLoanlist.Count; x++)
                {
                    if (CBYear_Loanlist.SelectedItem.ToString() == YearInCBLoanlist[x].ToString())
                    {
                        for (int y = 0; y < DMLoanlist[x].Count; y++)
                        {
                            CBMonth_Loanlist.Items.Add(DMLoanlist[x][y].ToString());
                        }
                        break;
                    }
                }
                if (CBMonth_Loanlist.Items.Count != 0)
                {
                    CBMonth_Loanlist.Enabled = true;
                    CBMonth_Loanlist.SelectedIndex = 0;
                }
                else
                {
                    CBMonth_Loanlist.Enabled = false;
                    CBMonth_Loanlist.SelectedIndex = -1;
                    BTAddlist_Loanlist.Enabled = false;
                    CBPayment_Loanlist.Enabled = false;
                    CBPayment_Loanlist.SelectedIndex = -1;
                    BTClear_Loanlist.Enabled = false;
                    BTSave_Loanlist.Enabled = false;
                }
            }
        }

        private void CBMonth_Loanlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CBMonth_Loanlist.SelectedIndex != -1)
            {
                TBAmount_Loanlist.Text = "0";
                if (Convert.ToInt32(CBYear_Loanlist.SelectedItem) != Endloan_loanlist[0] || Convert.ToInt32(CBMonth_Loanlist.SelectedItem) != Endloan_loanlist[1])
                    TBAmount_Loanlist.Text = NormalPrice_Loanlist.ToString();
                else if (Convert.ToInt32(CBYear_Loanlist.SelectedItem) == Endloan_loanlist[0] && Convert.ToInt32(CBMonth_Loanlist.SelectedItem) == Endloan_loanlist[1])
                {
                    if (DTPDate.Value.Date > DateLastofPay_Loanlist)
                        TBAmount_Loanlist.Text = Paylate_Loanlist.ToString();
                    else
                        TBAmount_Loanlist.Text = LastofMonthPrice_Loanlist.ToString();
                }
                BTAddlist_Loanlist.Enabled = true;
            }
            else
            {
                BTAddlist_Loanlist.Enabled = false;
                CBPayment_Loanlist.Enabled = false;
                CBPayment_Loanlist.SelectedIndex = -1;
                BTClear_Loanlist.Enabled = false;
                BTSave_Loanlist.Enabled = false;
            }
        }

        private void BTAddlist_Loanlist_Click(object sender, EventArgs e)
        {
            if (DMLoanlist.Count != 0)
            {
                if (CBYear_Loanlist.SelectedIndex != -1 && CBMonth_Loanlist.SelectedIndex != -1)
                {
                    if (DGV_Loanlist.Rows.Count <= 17)
                    {
                        Class.loanlist Ll = (CBLoanlist.SelectedItem as Class.loanlist);
                        String Time = CBYear_Loanlist.SelectedItem + "/" + CBMonth_Loanlist.SelectedItem;
                        DGV_Loanlist.Rows.Add(Time, $"รายการกู้ ( {Ll.LoanID} )", TBAmount_Loanlist.Text, Ll.LoanID.ToString(), CBYear_Loanlist.SelectedItem, CBMonth_Loanlist.SelectedItem);
                        TBAmount_Loanlist.Text = "0";
                        RemvoeCBLoanlist();
                        SumPriceLoanlist();
                        if (DMLoanlist.Count == 0)
                        {
                            CBMonth_Loanlist.Enabled = false;
                            CBYear_Loanlist.Enabled = false;
                        }
                    }
                    else
                    {
                        MessageBox.Show("รายการในบิลล์นี้เยอะเกินไป โปรดจ่ายต่อในบิลล์ถัดไป", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("โปรดระบุ ปีและเดือนก่อนทำรายการ", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                TBAmount_Loanlist.Text = "0";
                CBMonth_Loanlist.Enabled = false;
                CBYear_Loanlist.Enabled = false;
                MessageBox.Show("ไม่มีรายการให้เพิ่มแล้ว", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void RemvoeCBLoanlist()
        {
            if (CBYear_Loanlist.SelectedIndex != -1 && CBMonth_Loanlist.SelectedIndex != -1)
            {
                int PosYear = 0;
                int PosMonth = 0;
                for (int x = 0; x < YearInCBLoanlist.Count; x++)
                {
                    if (CBYear_Loanlist.SelectedItem.ToString() == YearInCBLoanlist[x].ToString())
                    {
                        PosYear = x;
                        for (int y = 0; y < DMLoanlist[x].Count; y++)
                        {
                            if (CBMonth_Loanlist.SelectedItem.ToString() == DMLoanlist[x][y].ToString())
                            {
                                PosMonth = y;
                                break;
                            }
                        }
                    }
                }
                DMLoanlist[PosYear].RemoveAt(PosMonth);
                CBMonth_Loanlist.Items.RemoveAt(PosMonth);
                if (DMLoanlist[PosYear].Count == 0)
                {
                    DMLoanlist.RemoveAt(PosYear);
                    CBYear_Loanlist.Items.RemoveAt(PosYear);
                    YearInCBLoanlist.RemoveAt(PosYear);
                }
                if (CBYear_Loanlist.Items.Count != 0)
                {
                    CBYear_Loanlist.SelectedIndex = 0;
                }

                if (CBMonth_Loanlist.Items.Count != 0)
                {
                    CBMonth_Loanlist.SelectedIndex = 0;
                }
            }
        }
        private void SumPriceLoanlist()
        {
            int Amount = 0;
            for (int x = 0; x < DGV_Loanlist.Rows.Count; x++)
            {
                Amount += Convert.ToInt32(DGV_Loanlist.Rows[x].Cells[2].Value.ToString());
            }
            LBSum_Loanlist.Text = Amount.ToString();
        }

        private void DGV_Loanlist_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int currentMouseOverRow = DGV_Loanlist.HitTest(e.X, e.Y).RowIndex;
                if (currentMouseOverRow != -1)
                {
                    SelectIndexRow = currentMouseOverRow;
                    ContextMenu m = new ContextMenu();
                    m.MenuItems.Add(new MenuItem("ลบรายการ"));
                    m.Show(DGV_Loanlist, new Point(e.X, e.Y));
                    m.MenuItems[0].Click += new System.EventHandler(this.Delete_ClickLoanlist);
                }
            }
        }
        private void Delete_ClickLoanlist(object sender, EventArgs e)
        {
            String Year = DGV_Loanlist.Rows[SelectIndexRow].Cells[4].Value.ToString();
            String Month = DGV_Loanlist.Rows[SelectIndexRow].Cells[5].Value.ToString();
            DGV_Loanlist.Rows.RemoveAt(SelectIndexRow);
            if (DMLoanlist.Count != 0)
            {
                int PosYear = -1;
                for (int x = 0; x < YearInCBLoanlist.Count; x++)
                {
                    if (Year == YearInCBLoanlist[x].ToString())
                    {
                        PosYear = x;
                        break;
                    }
                }
                //Found Year
                if (PosYear != -1)
                {
                    DMLoanlist[PosYear].Add(Convert.ToInt32(Month));
                    DMLoanlist[PosYear].Sort();
                }
                //Not Found Year
                else
                {
                    if (YearInCBLoanlist.Count != 0)
                    {
                        for (int x = 0; x < YearInCBLoanlist.Count; x++)
                        {
                            if (YearInCBLoanlist[x] > Convert.ToInt32(Year))
                            {
                                YearInCBLoanlist.Insert(x, Convert.ToInt32(Year));
                                DMLoanlist.Insert(x, new List<int>());
                                DMLoanlist[x].Add(Convert.ToInt32(Month));
                                CBYear_Loanlist.Items.Add(Year);
                                break;
                            }
                            else if(x == YearInCBLoanlist.Count - 1)
                            {
                                DMLoanlist.Add(new List<int>());
                                DMLoanlist[x+1].Add(Convert.ToInt32(Month));
                                YearInCBLoanlist.Add(Convert.ToInt32(Year));
                                CBYear_Loanlist.Items.Add(Year);
                                break;
                            }
                        }
                    }
                }
            }

            //Don't Have Year IN Combobox
            else
            {
                DMLoanlist.Add(new List<int>());
                DMLoanlist[0].Add(Convert.ToInt32(Month));
                YearInCBLoanlist.Add(Convert.ToInt32(Year));
                CBYear_Loanlist.Items.Add(Year);
            }


            if (CBYear_Loanlist.SelectedIndex != -1)
            {
                if (CBYear_Loanlist.Items.Count != 0)
                {
                    CBYear_Loanlist_SelectedIndexChanged(new object(), new EventArgs());
                }
            }
            else
            {
                if (CBYear_Loanlist.Items.Count != 0)
                {
                    CBYear_Loanlist.SelectedIndex = 0;
                    CBYear_Loanlist_SelectedIndexChanged(new object(), new EventArgs());
                }
            }
            SumPriceLoanlist();
            CBYear_Loanlist.Enabled = true;
            CBMonth_Loanlist.Enabled = true;
            SelectIndexRow = -1;
        }

        private void DGV_Loanlist_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (DGV_Loanlist.Rows.Count != 0)
            {
                if (CBPayment_Loanlist.Items.Count != 0)
                {
                    if (CBPayment_Loanlist.Enabled == false)
                        CBPayment_Loanlist.SelectedIndex = 0;
                    CBPayment_Loanlist.Enabled = true;
                    BTSave_Loanlist.Enabled = true;
                }
                NumPrint++;
                DGV_PayLoan.Rows.Add(NumPrint, DGV_Loanlist.Rows[e.RowIndex].Cells[0].Value, CBLoanlist.Text, DGV_Loanlist.Rows[e.RowIndex].Cells[2].Value);
            }
            if (DGV_Loanlist.Rows.Count != 0)
            {
                DTPDate.Enabled = false;
            }
        }

        private void DGV_Loanlist_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            if (DGV_Loanlist.Rows.Count == 0)
            {
                CBPayment_Loanlist.Enabled = false;
                CBPayment_Loanlist.SelectedIndex = -1;
                BTSave_Loanlist.Enabled = false;
            }
            if (DGV_PayLoan.RowCount != 0)
            {
                DGV_PayLoan.Rows.RemoveAt(e.RowIndex);
                DGV_PayLoan.Rows.Clear();
                NumPrint--;
                for (int r = 0; r < DGV_Loanlist.RowCount; r++)
                {
                    DGV_PayLoan.Rows.Add(r + 1, DGV_Loanlist.Rows[r].Cells[0].Value.ToString(),
                         DGV_Loanlist.Rows[r].Cells[1].Value.ToString(), DGV_Loanlist.Rows[r].Cells[2].Value.ToString());
                }
                if (DGV_Loanlist.Rows.Count == 0)
                {
                    bool Checked = false;
                    if (BankTeacher.Bank.Menu.DateAmountChange == 1)
                        Checked = true;
                    DTPDate.Enabled = Checked;
                }
            }
        }

        private void CBPayment_Loanlist_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void BTClear_Loanlist_Click(object sender, EventArgs e)
        {
            NumPrint = 0;
            if (CBLoanlist.SelectedIndex != -1)
                CBLoanlist_SelectedIndexChanged(new object(), new EventArgs());
        }
        int Printbill = 0; 
        string LoanNo = "";
        private void BTSave_Loanlist_Click(object sender, EventArgs e)
        {
            Class.loanlist Ll = (CBLoanlist.SelectedItem as Class.loanlist);

            BankTeacher.Class.ComboBoxPayment Payment = (CBPayment_Loanlist.SelectedItem as BankTeacher.Class.ComboBoxPayment);
            if (DGV_Loanlist.Rows.Count != 0)
            {
                TBTeacherBill.Text = Class.SQLConnection.InputSQLMSSQL(SQLDefault[3]).Rows[0][0].ToString();
                int Balance = Convert.ToInt32(LBSum_Loanlist.Text);
                BankTeacher.Bank.Pay.Calculator calculator = new BankTeacher.Bank.Pay.Calculator(Balance);
                calculator.ShowDialog();
                if (BankTeacher.Bank.Pay.Calculator.Return)
                {
                    String Date = DTPDate.Value.ToString("yyyy:MM:dd");
                    Date = Date.Replace(":", "/");
                    String BillNo = Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[5]
                        .Replace("{TeacherNoPay}", TBTeacherNo.Text)
                        .Replace("{TeacherNoAddBy}", Class.UserInfo.TeacherNo)
                        .Replace("{Date}", Date)
                        .Replace("{LoanNo}", Ll.LoanID)).Tables[0].Rows[0][0].ToString();
                    for (int x = 0; x < DGV_Loanlist.Rows.Count; x++)
                    {
                        if (DGV_Loanlist.Rows[x].Cells[1].Value.ToString().Contains("กู้"))
                        {
                            DataTable dt_InterestRate = Class.SQLConnection.InputSQLMSSQL(
                            "SELECT a.LoanNo, a.TeacherNo, a.LoanAmount, ROUND(CAST(a.InterestRate / 100 * a.LoanAmount as float), 0) as InterestRate,a.PayNo \r\n" +
                            "FROM EmployeeBank.dbo.tblLoan as a \r\n" +
                            "WHERE a.LoanNo = '{LoanNo}'".Replace("{LoanNo}", DGV_Loanlist.Rows[x].Cells[3].Value.ToString()));
                            var Amount_Loan = dt_InterestRate.Rows[0][3].ToString();
                            LoanNo = DGV_Loanlist.Rows[x].Cells[3].Value.ToString();
                            Class.SQLConnection.InputSQLMSSQL(SQLDefault[6]
                            .Replace("{BillNo}", BillNo)
                            .Replace("{TypeNo}", "2")
                            .Replace("{LoanNo}", DGV_Loanlist.Rows[x].Cells[3].Value.ToString())
                            .Replace("{Amount}", DGV_Loanlist.Rows[x].Cells[2].Value.ToString())
                            .Replace("{Month}", DGV_Loanlist.Rows[x].Cells[5].Value.ToString())
                            .Replace("{Year}", DGV_Loanlist.Rows[x].Cells[4].Value.ToString())
                            .Replace("{BillDetailPaymentNo}", (Payment.No).ToString()));

                            Class.SQLConnection.InputSQLMSSQL(SQLDefault[7]
                                .Replace("{LoanNo}", DGV_Loanlist.Rows[x].Cells[3].Value.ToString())
                                .Replace("{LoanAmount}", DGV_Loanlist.Rows[x].Cells[2].Value.ToString())
                                .Replace("{TeacherNo}", TBTeacherNo.Text)
                                .Replace("{Amount}", Amount_Loan.ToString()));
                        }
                    }
                    DataTable dt = Class.SQLConnection.InputSQLMSSQL("SELECT a.TeacherNo, a.SavingAmount,SUM(b.RemainsAmount) \r\n" +
                    "FROM EmployeeBank.dbo.tblShare as a \r\n" +
                    "LEFT JOIN EmployeeBank.dbo.tblGuarantor as b on a.TeacherNo = b.TeacherNo \r\n" +
                    "WHERE a.TeacherNo = '{TeacherNo}' \r\n".Replace("{TeacherNo}", TBTeacherNo.Text) +
                    "GROUP BY  a.TeacherNo, a.SavingAmount");
                    printDocument1.DefaultPageSettings.PaperSize = new PaperSize("A4", 595, 842);
                    printDocument1.DefaultPageSettings.Landscape = true;
                    Class.Print.PrintPreviewDialog.info_Savingtotel = Convert.ToInt32(Convert.ToInt32(dt.Rows[0][1]) - Convert.ToInt32(dt.Rows[0][2])).ToString("N0");
                    if(dt.Rows[0][2].ToString() != "")
                    {
                        Class.Print.PrintPreviewDialog.info_Lona_AmountRemain = Convert.ToInt32(dt.Rows[0][2]).ToString("N0");
                    }
                    Class.Print.PrintPreviewDialog.info_name = TBTeacherName.Text;
                    Class.Print.PrintPreviewDialog.info_id = TBTeacherNo.Text;
                    Class.Print.PrintPreviewDialog.info_TeacherAdd = Class.UserInfo.TeacherName;
                    Class.Print.PrintPreviewDialog.info_datepayShare = DateTime.Today.Day.ToString() + '/' + DateTime.Today.Month.ToString() + '/' + DateTime.Today.Year.ToString();
                    Class.Print.PrintPreviewDialog.info_Payment = CBPayment_Loanlist.Items[CBPayment_Loanlist.SelectedIndex].ToString();
                    Class.Print.PrintPreviewDialog.info_Billpay = TBTeacherBill.Text;
                    if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                    {
                        printDocument1.Print();
                    }
                  
                    // printDocument1.Print();
                    MessageBox.Show("ชำระสำเร็จ", "แจ้งเตือนการขำระ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    TBTeacherNo.Enabled = false;
                    BSearchTeacher.Enabled = true;
                    CBLoanlist.Enabled = false;
                    CBMonth_Loanlist.Enabled = false;
                    CBYear_Loanlist.Enabled = false;
                    BTSave_Loanlist.Enabled = false;
                    CBPayment_Loanlist.Enabled = false;
                    BTAddlist_Loanlist.Enabled = false;

                    CheckSave = true;
                    TBTeacherNo.Enabled = false;
                    BSearchTeacher.Enabled = true;
                }
                else if (!(BankTeacher.Bank.Pay.Calculator.Return))
                {
                    MessageBox.Show("การชำระล้มเหลว", "การเเจ้งเตือนการชำระ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }
}

