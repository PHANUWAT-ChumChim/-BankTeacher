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
    public partial class pay : Form
    {

        //------------------------- index -----------------
        public static int x = 0;
        public static int sum = 0;
        public static int SelectIndexRow = -1;
        int Check = 0;
        int Auto = 0;
        //----------------------- index code -------------------- ////////


        /// <summary> 
        /// SQLDafault 
        /// <para>[0] SELECT MEMBER INPUT: {Text} </para> 
        /// <para>[1] SELECT TIME INPUT : - </para>
        /// <para>[2] INSERT Bill BillDetail Saving or Loan and Change Status Loan  INPUT: --{addbill} , {TeacherNo} , {TeacherNoaddby} ,{Month} , {Year} , {Payment} , {BillNo}</para>
        /// <para>, --{haveLoan} , {LoanNo} , {AmountLoanPay} , {TeacherGuaNo1} ,{TeacherGuaNo2} , {TeacherGuaNo3} , {TeacherGuaNo4} </para>
        /// <para>, --{haveSaving} , {AmountPaySaving}</para>
        /// <para>, --{Close}</para>
        /// <para>[3] SELECT Guarantor IN Loan INPUT: {LoanID}</para>
        /// <para>[4] UPDATE REMAIN INPUT: {TeacherNINMonth INPUT: {TeacherNo}</para>
        /// <para>[6] SELECT Detail Member INPUT: {TeacherNo}</para>
        ///<para>[7] SELECT Type pay (2Table) INPUT : {Month} , {Year} , {TeacherNo} {DateSet} </para>
        ///<para>[8] Check BillDetailPayment INPUT: o} {Amount}</para>
        /// <para>[5] AmountpayANDAmountLoan- </para>
        ///<para>[9] SELECT LOANID and SELECT DATE Register Member INPUT : {TeacherNo} </para>
        ///<para>[10] SELECT Detail Loan INPUT: {LoanID} </para>
        ///<para>[11] SELECT SharePayBill and SELECT ShareOfYear INPUT: {TeacherNo} , {Year}</para>
        /// </summary> 
        private String[] SQLDefault = new String[]
         { 
          //[0] SELECT MEMBER INPUT: {Text} 
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
          //[1] SELECT TIME INPUT : -
          "SELECT CONVERT (DATE , CURRENT_TIMESTAMP); "
             ,
          //[2] INSERT Bill BillDetail Saving or Loan and Change Status Loan  INPUT: --{addbill} , {TeacherNo} , {TeacherNoaddby} ,{Month} , {Year} , {Payment} ,{BillNo}, --{haveLoan} , {LoanNo} , {AmountLoanPay} , {TeacherGuaNo1} ,{TeacherGuaNo2} , {TeacherGuaNo3} , {TeacherGuaNo4} , --{haveSaving} , {AmountPaySaving}, --{Close}
          "DECLARE @BIllNO INT; \r\n " +
          "DECLARE @SavingAmount INT; \r\n " +
          "DECLARE @Amount INT; \r\n " +
          "DECLARE @PayNo INT; \r\n " +
          " \r\n " +
          "SET @SavingAmount = (SELECT SavingAmount FROM EmployeeBank.dbo.tblShare WHERE TeacherNo = '{TeacherNo}'); \r\n " +
          " \r\n " +
          "--{addbill}INSERT INTO EmployeeBank.dbo.tblBill (TeacherNoAddBy,TeacherNo,DateAdd,Cancel) \r\n " +
          "--{addbill}VALUES('{TeacherNoaddby}','{TeacherNo}',CURRENT_TIMESTAMP,1); \r\n " +
          "--{addbill}SET @BIllNO = SCOPE_IDENTITY(); \r\n " +
          " \r\n " +
          "--Loan \r\n " +
          " \r\n " +
          "--{haveLoan}SET @Amount = (SELECT (LoanAmount * (InterestRate / 100)) + LoanAmount FROM EmployeeBank.dbo.tblLoan WHERE LoanNo = '{LoanNo}') \r\n " +
          " \r\n " +
          "--{haveLoan}INSERT INTO EmployeeBank.dbo.tblBillDetail (BillNo,TypeNo,LoanNo,Amount,Mount,Year,BillDetailPaymentNo) \r\n " +
          "--{haveLoan}VALUES({BillNo} , '2','{LoanNo}','{AmountPayLoan}','{Month}','{Year}','{Payment}') \r\n " +
          " \r\n " +
          "--{haveLoan}UPDATE EmployeeBank.dbo.tblGuarantor \r\n " +
          "--{haveLoan}SET RemainsAmount = (SELECT RemainsAmount FROM EmployeeBank.dbo.tblGuarantor WHERE TeacherNo = '{TeacherGuaNo1}' and LoanNo = {LoanNo}) - Convert(float , (((SELECT Amount FROM EmployeeBank.dbo.tblGuarantor WHERE TeacherNo = '{TeacherGuaNo1}' and LoanNo = {LoanNo}) / @Amount) * '{AmountPayLoan}')) \r\n " +
          "--{haveLoan}WHERE TeacherNo = '{TeacherGuaNo1}' and LoanNo = {LoanNo}; \r\n " +
          " \r\n " +
          "--{haveLoan}UPDATE EmployeeBank.dbo.tblGuarantor \r\n " +
          "--{haveLoan}SET RemainsAmount = (SELECT RemainsAmount FROM EmployeeBank.dbo.tblGuarantor WHERE TeacherNo = '{TeacherGuaNo2}' and LoanNo = {LoanNo}) - Convert(float , (((SELECT Amount FROM EmployeeBank.dbo.tblGuarantor WHERE TeacherNo = '{TeacherGuaNo2}' and LoanNo = {LoanNo}) / @Amount) * '{AmountPayLoan}')) \r\n " +
          "--{haveLoan}WHERE TeacherNo = '{TeacherGuaNo2}' and LoanNo = {LoanNo}; \r\n " +
          " \r\n " +
          "--{haveLoan}UPDATE EmployeeBank.dbo.tblGuarantor \r\n " +
          "--{haveLoan}SET RemainsAmount = (SELECT RemainsAmount FROM EmployeeBank.dbo.tblGuarantor WHERE TeacherNo = '{TeacherGuaNo3}' and LoanNo = {LoanNo}) - Convert(float , (((SELECT Amount FROM EmployeeBank.dbo.tblGuarantor WHERE TeacherNo = '{TeacherGuaNo3}' and LoanNo = {LoanNo}) / @Amount)* '{AmountPayLoan}')) \r\n " +
          "--{haveLoan}WHERE TeacherNo = '{TeacherGuaNo3}' and LoanNo = {LoanNo}; \r\n " +
          " \r\n " +
          "--{haveLoan}UPDATE EmployeeBank.dbo.tblGuarantor \r\n " +
          "--{haveLoan}SET RemainsAmount = (SELECT RemainsAmount FROM EmployeeBank.dbo.tblGuarantor WHERE TeacherNo = '{TeacherGuaNo4}' and LoanNo = {LoanNo}) - Convert(float , (((SELECT Amount FROM EmployeeBank.dbo.tblGuarantor WHERE TeacherNo = '{TeacherGuaNo4}' and LoanNo = {LoanNo}) /@Amount)* '{AmountPayLoan}')) \r\n " +
          "--{haveLoan}WHERE TeacherNo = '{TeacherGuaNo4}' and LoanNo = {LoanNo}; \r\n " +
          " \r\n " +
          "--{haveLoan}SELECT b.LoanNo , PayNo \r\n " +
          "--{haveLoan}FROM EmployeeBank.dbo.tblBill as a \r\n " +
          "--{haveLoan}LEFT JOIN EmployeeBank.dbo.tblBillDetail as b on a.BillNo = b.BillNo \r\n " +
          "--{haveLoan}LEFT JOIN EmployeeBank.dbo.tblLoan as c on b.LoanNo = c.LoanNo \r\n " +
          "--{haveLoan}WHERE b.LoanNo = '{LoanNo}' \r\n"+
          "---------------------------------------------------------\r\n"+
          "--Close BillLoan \r\n " +
          "--{Close}UPDATE EmployeeBank.dbo.tblLoan \r\n " +
          "--{Close}SET LoanStatusNo = 3 \r\n " +
          "--{Close}WHERE LoanNo = '{LoanNo}'; \r\n " +
          "--{Close}UPDATE EmployeeBank.dbo.tblGuarantor \r\n " +
          "--{Close}SET RemainsAmount = 0 \r\n"+
          "--{Close}WHERE TeacherNo = '{TeacherGuaNo1}' and LoanNo = {LoanNo}; \r\n " +
          " \r\n " +
          "--{Close}UPDATE EmployeeBank.dbo.tblGuarantor \r\n " +
          "--{Close}SET RemainsAmount = 0 \r\n"+
          "--{Close}WHERE TeacherNo = '{TeacherGuaNo2}' and LoanNo = {LoanNo}; \r\n " +
          " \r\n " +
          "--{Close}UPDATE EmployeeBank.dbo.tblGuarantor \r\n " +
          "--{Close}SET RemainsAmount = 0 \r\n"+
          "--{Close}WHERE TeacherNo = '{TeacherGuaNo3}' and LoanNo = {LoanNo}; \r\n " +
          " \r\n " +
          "--{Close}UPDATE EmployeeBank.dbo.tblGuarantor \r\n " +
          "--{Close}SET RemainsAmount = 0 \r\n"+
          "--{Close}WHERE TeacherNo = '{TeacherGuaNo4}' and LoanNo = {LoanNo}; \r\n " +
          "------------------------------------------------------------ \r\n " +
          " \r\n " +
          "--Saving \r\n " +
          "--{haveSaving}INSERT INTO EmployeeBank.dbo.tblBillDetail (BillNo,TypeNo,LoanNo,Amount,Mount,Year,BillDetailPaymentNo) \r\n " +
          "--{haveSaving}VALUES({BillNo} , '1','','{AmountPaySaving}','{Month}','{Year}','{Payment}') \r\n " +
          " \r\n " +
          "--{haveSaving}UPDATE EmployeeBank.dbo.tblShare \r\n " +
          "--{haveSaving}SET SavingAmount = @SavingAmount + '{AmountPaySaving}' \r\n " +
          "--{haveSaving}WHERE TeacherNo = '{TeacherNo}'; \r\n " +
          "------------------------------------------------------------- \r\n"+
          "--{addbill}SELECT @BIllNO "
             ,
         //[3] SELECT Guarantor IN Loan INPUT: {LoanID}
          "SELECT b.TeacherNo \r\n " +
          "FROM EmployeeBank.dbo.tblLoan as a \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblGuarantor as b on a.LoanNo = b.LoanNo \r\n " +
          "WHERE a.LoanNo = {LoanID} and LoanStatusNo != 3 and LoanStatusNo != 4 "
          , 

         //[4] UPDATE REMAIN INPUT: {TeacherNo} {Amount}
          "DECLARE @Remain INT; \r\n " +
          " \r\n " +
          "SET @Remain = (SELECT Remain \r\n " +
          "FROM EmployeeBank.dbo.tblLoanPay \r\n " +
          "WHERE TeacherNo = '{TeacherNo}'); \r\n " +
          " \r\n " +
          "UPDATE EmployeeBank.dbo.tblLoanPay \r\n " +
          "SET Remain = @Remain - '{Amount}' \r\n " +
          "WHERE TeacherNo = '{TeacherNo}' \r\n " +
          " \r\n " +
          " "
          ,
          //[5] AmountpayANDAmountLoanINMonth INPUT: {TeacherNo}
          "SELECT Mb.TeacherNo,Mb.StartAmount AS  Amountpay,Mb.DateAdd AS Datepay,Lp.Amount AS AmountLoan,Ln.DateAdd AS DateLoan \r\n"+
          "FROM EmployeeBank.dbo.tblMember as Mb\r\n"+
          "LEFT JOIN EmployeeBank.dbo.tblLoan as Ln on Mb.TeacherNo = Ln.TeacherNo\r\n"+
          "LEFT JOIN EmployeeBank.dbo.tblLoanPay as Lp  on Ln.TeacherNo = Lp.TeacherNo\r\n" +
          "WHERE Mb.TeacherNo = '{TeacherNo}';"
             ,
          //[6] SELECT Detail Member INPUT: {TeacherNo}
          "SELECT a.TeacherNo , CAST(c.PrefixName+' '+[Fname] +' '+ [Lname] as NVARCHAR)AS Name, b.IdNo AS TeacherID,   \r\n " +
          " b.TeacherLicenseNo,b.IdNo AS IDNo,b.TelMobile ,a.StartAmount,CAST(d.MemberStatusName as nvarchar) AS UserStatususing   \r\n " +
          " FROM EmployeeBank.dbo.tblMember as a   \r\n " +
          " LEFT JOIN Personal.dbo.tblTeacherHis as b ON a.TeacherNo = b.TeacherNo   \r\n " +
          " LEFT JOIN BaseData.dbo.tblPrefix as c ON c.PrefixNo = b.PrefixNo  \r\n " +
          " INNER JOIN EmployeeBank.dbo.tblMemberStatus as d on a.MemberStatusNo = d.MemberStatusNo  \r\n " +
          " WHERE a.TeacherNo LIKE 'T{TeacherNo}%' and a.MemberStatusNo = 1   \r\n " +
          " ORDER BY a.TeacherNo;  "
          ,
          //[7] SELECT Type pay (2Table) INPUT : {Month} , {Year} , {TeacherNo} {DateSet}          //[] INPUT: 
          "SELECT a.TeacherNo, StartAmount , f.TypeName    \r\n " +
          "FROM EmployeeBank.dbo.tblMember as a     \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblBill as b on a.TeacherNo = b.TeacherNo    \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblBillDetail as c on b.BillNo = c.BillNo    \r\n " +
          "LEFT JOIN Personal.dbo.tblTeacherHis as d on a.TeacherNo = d.TeacherNo     \r\n " +
          "LEFT JOIN BaseData.dbo.tblPrefix as e on d.PrefixNo = e.PrefixNo     \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblBillDetailType as f on c.TypeNo = f.TypeNo    \r\n " +
          "    \r\n " +
          "WHERE a.TeacherNo NOT IN     \r\n " +
          "(SELECT aa.TeacherNo  \r\n " +
          "FROM EmployeeBank.dbo.tblBill as aa     \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblBillDetail as bb on aa.BillNo = bb.BillNo     \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblLoan as cc on aa.TeacherNo = cc.TeacherNo   \r\n " +
          "WHERE bb.Mount = {Month} and bb.Year = {Year} and bb.TypeNo = 1 and MemberStatusNo = 1 and DATEADD(YYYY,0,'{DateSet}') >= a.DateAdd  )   \r\n " +
          "and a.TeacherNo = '{TeacherNo}' and c.TypeNo = 1 and MemberStatusNo = 1 \r\n " +
          "GROUP BY a.TeacherNo,f.TypeName, StartAmount   ;  \r\n"+
          "   \r\n " +

          "  SELECT a.TeacherNo , \r\n " +
          "  ROUND(Convert(float, ( (g.InterestRate / 100) * LoanAmount)/ PayNo) ,0) + ROUND(Convert(float , LoanAmount / PayNo),0) , \r\n " +
          "  f.TypeName  ,g.LoanNo ,PayNo , g.MonthPay, g.YearPay , \r\n " +
          "  (LoanAmount  + Convert(float , (InterestRate / 100) * LoanAmount)) - (ROUND(Convert(float, ( (InterestRate / 100) * LoanAmount)/ PayNo) ,0) + ROUND(Convert(float , LoanAmount / PayNo),0)) * (PayNo -1) \r\n " +
          "  FROM EmployeeBank.dbo.tblMember as a    \r\n " +
          "  LEFT JOIN EmployeeBank.dbo.tblBill as b on a.TeacherNo = b.TeacherNo   \r\n " +
          "  LEFT JOIN EmployeeBank.dbo.tblBillDetail as c on b.BillNo = c.BillNo   \r\n " +
          "  LEFT JOIN Personal.dbo.tblTeacherHis as d on a.TeacherNo = d.TeacherNo    \r\n " +
          "  LEFT JOIN BaseData.dbo.tblPrefix as e on d.PrefixNo = e.PrefixNo    \r\n " +
          "  LEFT JOIN EmployeeBank.dbo.tblBillDetailType as f on c.TypeNo = f.TypeNo   \r\n " +
          "  LEFT JOIN EmployeeBank.dbo.tblLoan as g on a.TeacherNo = g.TeacherNo   \r\n " +
          "   \r\n " +
          "  WHERE (a.TeacherNo NOT IN     \r\n " +
          "  (SELECT aa.TeacherNo  \r\n " +
          "  FROM EmployeeBank.dbo.tblBill as aa     \r\n " +
          "  LEFT JOIN EmployeeBank.dbo.tblBillDetail as bb on aa.BillNo = bb.BillNo     \r\n " +
          "  LEFT JOIN EmployeeBank.dbo.tblLoan as cc on bb.LoanNo = cc.LoanNo \r\n " +
          "  LEFT JOIN EmployeeBank.dbo.tblMember as dd on aa.TeacherNo = dd.TeacherNo \r\n " +
          "  WHERE bb.Mount = {Month} and bb.Year = {Year} \r\n " +
          "  and dd.MemberStatusNo = 1 and (DATEADD(YYYY,0,'{DateSet}') >= cc.DateAdd ) and bb.TypeNo = 2  and LoanStatusNo = 2 )and a.TeacherNo = '{TeacherNo}'  and c.TypeNo = 2 and LoanStatusNo =2 and DATEADD(YYYY,0,'{DateSet}') <= EOMONTH(DATEADD(MONTH , PayNo-1,CAST(CAST(CAST(YearPay as nvarchar) +'/' + CAST(MonthPay AS nvarchar) + '/01' AS nvarchar) AS date)))) \r\n " +
          "  GROUP BY  a.TeacherNo ,  \r\n " +
          "  ROUND(Convert(float, ( (g.InterestRate / 100) * LoanAmount)/ PayNo) ,0) + ROUND(Convert(float , LoanAmount / PayNo),0) ,  \r\n " +
          "  f.TypeName  ,g.LoanNo ,PayNo , g.MonthPay, g.YearPay ,  \r\n " +
          "  (LoanAmount  + Convert(float , (InterestRate / 100) * LoanAmount)) - (ROUND(Convert(float, ( (InterestRate / 100) * LoanAmount)/ PayNo) ,0) + ROUND(Convert(float , LoanAmount / PayNo),0)) * (PayNo -1)  \r\n" + 
             "\r\n\r\n"+

          "  SELECT MonthPay , YearPay , ROUND(Convert(float, ( (InterestRate / 100) * LoanAmount)/ PayNo) ,0) + ROUND(Convert(float , LoanAmount / PayNo),0) ,LoanNo\r\n " +
          "   FROM EmployeeBank.dbo.tblLoan \r\n " +
          "   WHERE TeacherNo = '{TeacherNo}' and LoanStatusNo = 2; \r\n\r\n" + 

          "SELECT a.TeacherNo \r\n " +
          "FROM EmployeeBank.dbo.tblBill as a  \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblBillDetail as b on a.BillNo = b.BillNo \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblLoan as c on b.LoanNo = c.LoanNo \r\n " +
          "Where a.TeacherNo = '{TeacherNo}' and Mount = {Month} and Year = {Year}  \r\n " +
          "and Cancel = 1 and TypeNo = 2 and c.LoanStatusNo = 2 "

          , 
 

          //[8] Check BillDetailPayment INPUT: -  
          "SELECT Name , BillDetailpaymentNo  \r\n " +
          "FROM EmployeeBank.dbo.tblBillDetailPayment \r\n " +
          "WHERE Status = 1 "
          ,
          //[9] SELECT LOANID and SELECT DATE Register Member INPUT : {TeacherNo}
          "SELECT LoanNo \r\n " +
          "FROM EmployeeBank.dbo.tblLoan  \r\n " +
          "WHERE TeacherNo = '{TeacherNo}' and LoanStatusNo != 4 ; \r\n " +
          " \r\n " +
          "SELECT CAST(DateAdd as date) \r\n " +
          "FROM EmployeeBank.dbo.tblMember \r\n " +
          "WHERE TeacherNo = '{TeacherNo}' and MemberStatusNo != 2; "
          ,

          //[10] SELECT Detail Loan INPUT: {LoanID}
          "SELECT b.TeacherNo , CAST(d.PrefixName + ' ' + c.Fname + ' ' + c.Lname AS NVARCHAR) AS NameTeacher,CAST(DateAdd as date), \r\n " +
          "a.PayDate,MonthPay,YearPay,PayNo,InterestRate,LoanAmount,b.RemainsAmount,a.LoanStatusNo  \r\n " +
          ",TeacherNoAddBy, CAST(f.PrefixName + ' ' + e.Fname + ' ' + e.Lname AS NVARCHAR) AS NameTeacherAddby  \r\n " +
          "FROM EmployeeBank.dbo.tblLoan as a   \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblGuarantor as b on a.LoanNo = b.LoanNo  \r\n " +
          "LEFT JOIN Personal.dbo.tblTeacherHis as c on b.TeacherNo = c.TeacherNo   \r\n " +
          "LEFT JOIN BaseData.dbo.tblPrefix as d on c.PrefixNo = d.PrefixNo    \r\n " +
          "LEFT JOIN Personal.dbo.tblTeacherHis as e on a.TeacherNoAddBy = e.TeacherNo  \r\n " +
          "LEFT JOIN BaseData.dbo.tblPrefix as f on e.PrefixNo = f.PrefixNo    \r\n " +
          "WHERE a.LoanNo = '{LoanID}' and LoanStatusNo != 4 ; \r\n " +
          " \r\n " +
          "SELECT Concat(b.Mount , '/' , Year) \r\n " +
          "FROM EmployeeBank.dbo.tblLoan as a  \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblBillDetail as b on a.LoanNo = b.LoanNo \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblBill as c on b.BillNo = c.BillNo \r\n " +
          "WHERE a.LoanNo = '{LoanID}' and TypeNo = '2' and Cancel != 0; "

          ,

          //[11] SELECT SharePayBill and SELECT ShareOfYear INPUT: {TeacherNo} , {Year}
          "SELECT SUM(d.Amount) , d.Mount , d.Year , a.StartAmount\r\n" +
          "FROM EmployeeBank.dbo.tblMember as a\r\n" +
          "LEFT JOIN EmployeeBank.dbo.tblShare as b on a.TeacherNo = b.TeacherNo\r\n" +
          "LEFT JOIN EmployeeBank.dbo.tblBill as c on a.TeacherNo = c.TeacherNo\r\n" +
          "LEFT JOIN EmployeeBank.dbo.tblBillDetail as d on c.BillNo = d.BillNo\r\n" +
          "WHERE c.Cancel = 1 and d.TypeNo = 1 and d.Mount <= 12 and d.Year = {Year} and a.TeacherNo LIKE '{TeacherNo}'\r\n" +
          "GROUP BY a.TeacherNo , d.Amount , d.Mount , d.Year , a.StartAmount , CAST(a.DateAdd AS Date) , b.SavingAmount;\r\n" +
          "\r\n" +
          "SELECT a.StartAmount , b.SavingAmount, CAST(a.DateAdd as date)\r\n" +
          "FROM EmployeeBank.dbo.tblMember as a\r\n" +
          "LEFT JOIN EmployeeBank.dbo.tblShare as b on a.TeacherNo = b.TeacherNo\r\n" +
          "WHERE a.TeacherNo LIKE '{TeacherNo}'\r\n"
          ,

         };
        public pay(int TabIndex)
        {
            InitializeComponent();
            tabControl1.SelectedIndex = TabIndex;

            Font F = new Font("TH Sarabun New", 16, FontStyle.Regular);



            dataGridView1.ColumnHeadersDefaultCellStyle.Font = F;
            dataGridView2.ColumnHeadersDefaultCellStyle.Font = F;
        }
        //------------------------- FormSize -----------------
        // Comment!
        private void Menuf_SizeChanged(object sender, EventArgs e)
        {
            int x = this.Width / 2 - panel1.Size.Width / 2;
            int y = this.Height / 2 - panel1.Size.Height / 2;
            panel1.Location = new Point(x, y);
            //TB3.Location = new Point(TB+120, 15);
            //panel7.Size = new Size(TB+500,72); 
            //tabControl1.Location = new Point(x,y);

            //panel1.MinimumSize = new Size(columnSize * 40, rowSize * 40);
            //panel1.Height = rowSize * 40;
            //panel1.Width = columnSize * 40;
            //MessageBox.Show(this.Width + "" + this.Height);
        }
        //----------------------- End code -------------------- ////////



        //----------------------- PullSQL -------------------- ////////
        // Comment! Pull SQL Member & CheckTBTeacherNo

        // Comment! Pull SQL Member & CheckTBTeacherNo
        private void BSearchTeacher_Click(object sender, EventArgs e)
        {
            //try
            //{
            Bank.Search IN = new Bank.Search(SQLDefault[0]);
            IN.ShowDialog();

            TBTeacherNo.Text = Bank.Search.Return[0];
            TBTeacherNo_KeyDown(sender, new KeyEventArgs(Keys.Enter));
            //}
            //catch (Exception x)
            //{
            //    Console.WriteLine(x);
            //}
            if (Bank.Search.Return[0] != "")
            {
                TBTeacherNo.Text = Bank.Search.Return[0];
                TBTeacherNo_KeyDown(sender, new KeyEventArgs(Keys.Enter));

                sum = 0; x = 0;
                label5.Text = sum.ToString();
                dataGridView1.Rows.Clear();
                TBStartAmountShare.Clear();
                CBStatus.SelectedIndex = -1;
                CByeartap1.SelectedIndex = -1;
                CByeartap1.Items.Clear();
                CBMonth.SelectedIndex = -1;
                dataGridView2.Rows.Clear();
                dataGridView3.Rows.Clear();
                CByeartap2.SelectedIndex = -1;
                CByeartap2.Items.Clear();
                CBSelectLoan.SelectedIndex = -1;
                CBSelectLoan.Items.Clear();
                CBMonth.Items.Clear();
                CBMonth.SelectedIndex = -1;
                ComboBox[] cb = new ComboBox[] { CBSelectLoan };
                DataSet ds = Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[9]
                    .Replace("{TeacherNo}", TBTeacherNo.Text));
                //for (int x = 0; x < ds.Tables[0].Rows.Count; x++)
                //{
                    for (int x = 0; x < ds.Tables[0].Rows.Count; x++)
                    {
                        for (int aa = 0; aa < cb.Length; aa++)
                        {
                            cb[aa].Items.Add(new example.Class.ComboBoxPayment("รายการกู้ " + ds.Tables[0].Rows[x][0].ToString(), ds.Tables[0].Rows[x][0].ToString()));
                        }
                    }

                    if (CBSelectLoan.Items.Count == 1)
                    {
                        CBSelectLoan.SelectedIndex = 0;
                    }
                //}
                int YearRegister = Convert.ToInt32((Convert.ToDateTime(ds.Tables[1].Rows[0][0].ToString())).ToString("yyyy"));
                if (YearRegister < Convert.ToInt32(example.GOODS.Menu.Date[0]) - 2)
                {
                    int Yeard2 = Convert.ToInt32(example.GOODS.Menu.Date[0]) - 2;
                    
                    while (Yeard2 <= Convert.ToInt32(example.GOODS.Menu.Date[0]) + 1)
                    {
                        CByeartap1.Items.Add(Yeard2);
                        CByeartap2.Items.Add(Yeard2);
                        Yeard2++;
                    }
                }
                else if (YearRegister > Convert.ToInt32(example.GOODS.Menu.Date[0]) - 2)
                {
                    while (YearRegister <= Convert.ToInt32(example.GOODS.Menu.Date[0]) + 1)
                    {
                        CByeartap1.Items.Add(YearRegister);
                        CByeartap2.Items.Add(YearRegister);
                        YearRegister++;
                    }
                }
            }
        }
        // บันทึกรายการเเล้ว ส่งขึ้นไปบนฐานข้อมูล
        private void BTsave_Click(object sender, EventArgs e)
        {
            example.Class.ComboBoxPayment Payment = (CBB4Oppay.SelectedItem as example.Class.ComboBoxPayment);
            if (dataGridView1.Rows.Count != 0)
            {
                DialogResult dialogResult = MessageBox.Show("ยืนยันการชำระ", "การเเจ้งเตือนการชำระ", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dialogResult == DialogResult.Yes)
                {
                    DataSet dsbill = example.Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[2]
                                .Replace("{TeacherNo}", TBTeacherNo.Text)
                                .Replace("{TeacherNoaddby}", example.Class.UserInfo.TeacherNo)
                                .Replace("{Month}", CBMonth.Text)
                                .Replace("{Year}", CByeartap1.Text)
                                .Replace("{Payment}", Payment.No.ToString())
                                .Replace("--{addbill}",""));
                    if(dsbill.Tables[0].Rows.Count != 0)
                    {
                        for (int x = 0; x < dataGridView1.Rows.Count; x++)
                        {

                            if (dataGridView1.Rows[x].Cells[1].Value.ToString().Contains("สะสม"))
                            {
                                try
                                {
                                    example.Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[2]
                                    .Replace("{TeacherNo}", TBTeacherNo.Text)
                                    .Replace("{TeacherNoaddby}", example.Class.UserInfo.TeacherNo)
                                    .Replace("{Month}", CBMonth.Text)
                                    .Replace("{Year}", CByeartap1.Text)
                                    .Replace("{Payment}", Payment.No.ToString())
                                    .Replace("--{haveSaving}", "")
                                    .Replace("{AmountPaySaving}", dataGridView1.Rows[x].Cells[2].Value.ToString())
                                    .Replace("{BillNo}",dsbill.Tables[0].Rows[0][0].ToString()));
                                }
                                catch
                                {
                                    MessageBox.Show("ชำระเงินล้มเหลว", "แจ้งเตือนการขำระ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }

                            }
                            else if (dataGridView1.Rows[x].Cells[1].Value.ToString().Contains("กู้"))
                            {
                                DataTable dtGuarantor = example.Class.SQLConnection.InputSQLMSSQL(SQLDefault[3]
                                .Replace("{LoanID}", dataGridView1.Rows[x].Cells[3].Value.ToString()));
                                if (dtGuarantor.Rows.Count != 0)
                                {
                                    try
                                    {
                                        DataSet dsCheckMonth = example.Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[2]
                                            .Replace("{TeacherNo}", TBTeacherNo.Text)
                                            .Replace("{TeacherNoaddby}", example.Class.UserInfo.TeacherNo)
                                            .Replace("{Month}", CBMonth.Text)
                                            .Replace("{Year}", CByeartap1.Text)
                                            .Replace("{Payment}", Payment.No.ToString())
                                            .Replace("--{haveLoan}", "")
                                            .Replace("{AmountPayLoan}", dataGridView1.Rows[x].Cells[2].Value.ToString())
                                            .Replace("{BillNo}", dsbill.Tables[0].Rows[0][0].ToString())
                                            .Replace("{LoanNo}", dataGridView1.Rows[x].Cells[3].Value.ToString())
                                            .Replace("{TeacherGuaNo1}", dtGuarantor.Rows[0][0].ToString())
                                            .Replace("{TeacherGuaNo2}", dtGuarantor.Rows[1][0].ToString())
                                            .Replace("{TeacherGuaNo3}", dtGuarantor.Rows[2][0].ToString())
                                            .Replace("{TeacherGuaNo4}", dtGuarantor.Rows[3][0].ToString()));
                                        if (dsCheckMonth.Tables[0].Rows.Count == Convert.ToInt32(dsCheckMonth.Tables[0].Rows[0][1].ToString()))
                                        {
                                            example.Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[2]
                                                .Replace("--{Close}", "")
                                                .Replace("{LoanNo}", dataGridView1.Rows[x].Cells[3].Value.ToString())
                                                .Replace("{TeacherGuaNo1}", dtGuarantor.Rows[0][0].ToString())
                                                .Replace("{TeacherGuaNo2}", dtGuarantor.Rows[1][0].ToString())
                                                .Replace("{TeacherGuaNo3}", dtGuarantor.Rows[2][0].ToString())
                                                .Replace("{TeacherGuaNo4}", dtGuarantor.Rows[3][0].ToString()));
                                        }
                                    }
                                    catch
                                    {
                                        MessageBox.Show("ชำระกู้ล้มเหลว", "แจ้งเตือนการขำระ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("ชำระกู้ล้มเหลว", "แจ้งเตือนการขำระ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                            else
                            {
                                MessageBox.Show("ชำระเงินล้มเหลว", "แจ้งเตือนการขำระ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }

                        }
                    }
                    else
                    {
                        MessageBox.Show("ชำระเงินล้มเหลว", "แจ้งเตือนการขำระ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                  
                    CBMonth.SelectedIndex = -1;
                    CBStatus.SelectedIndex = -1;
                    CBB4Oppay.SelectedIndex = -1;
                    CByeartap1.SelectedIndex = -1;
                    CBB4Oppay.Enabled = false;
                    CBStatus.Enabled = false;
                    CBMonth.Enabled = false;
                    dataGridView1.Rows.Clear();
                    label5.Text = "0";
                    sum = 0;
                }
                else if (dialogResult == DialogResult.No)
                {
                    MessageBox.Show("การชำระล้มเหลว", "การเเจ้งเตือนการชำระ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            else
            {
                MessageBox.Show("รายการชำระไม่ถูกต้อง", "การเเจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        //----------------------- End code -------------------- ////////


        //-------------------------- if.Enbled Text ------------------------
        // if message in textCByer nothing will not Open next
        // ถ้า ไม่มีข้อความ ใน กล่องปี จะไม่เปิดใช่งานกล่อง ถัดไป
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            CBB4Oppay.Enabled = false;
            BTsave.Enabled = false;
            label5.Text = "0";
            if(dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Rows.Clear();
            }
            if (CByeartap1.SelectedIndex != -1)
            {
                DataSet ds = example.Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[9].Replace("{TeacherNo}", TBTeacherNo.Text));
                int Month = Convert.ToInt32((Convert.ToDateTime(ds.Tables[1].Rows[0][0].ToString())).ToString("MM"));
                int Year = Convert.ToInt32((Convert.ToDateTime(ds.Tables[1].Rows[0][0].ToString())).ToString("yyyy"));
                CBMonth.Enabled = true;
                CBMonth.Items.Clear();
                if (CByeartap1.Text == Year.ToString())
                {
                    while(Month <= 12)
                    {
                        CBMonth.Items.Add(Month);
                        Month++;
                    }
                }
                else
                {
                    for (int x  = 0; x < 12; x++)
                    {
                        CBMonth.Items.Add(x + 1);
                    }
                }
            }
            else
            {
                CBMonth.Enabled = false;

            }
            CBStatus.SelectedIndex = -1;
            TBStartAmountShare.Text = "";
        }
        // if message in textMonth nothing will not Open next
        // ถ้า ไม่มีข้อความ ใน กล่องเดือน จะไม่เปิดใช่งานกล่อง ถัดไป
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CBMonth.SelectedIndex != -1)
            {
                CBStatus.SelectedIndex = -1;
                CBStatus.Enabled = false;
                CBB4Oppay.Enabled = false;
                BTAdd.Enabled = false;
                CBB4Oppay.SelectedIndex = -1;
                BTsave.Enabled = false;
                TBStartAmountShare.Text = string.Empty;
                dataGridView1.Rows.Clear();
                button4.Enabled = false;
                CBStatus.Items.Clear();
                TBStartAmountShare.Text = "";
                BTAdd.Enabled = false;
                Auto = 0;
                label5.Text = "0";
                ComboBox[] cb = new ComboBox[] { CBStatus };
                DataSet ds = Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[7]
                    .Replace("{Month}", CBMonth.Text)
                    .Replace("{Year}", CByeartap1.Text)
                    .Replace("{TeacherNo}", TBTeacherNo.Text)
                    .Replace("{DateSet}", (Convert.ToDateTime(CByeartap1.Text + "-" + CBMonth.Text + "-" + DateTime.DaysInMonth(Convert.ToInt32(CByeartap1.Text), Convert.ToInt32(CBMonth.Text)))).ToString("yyyy-MM-dd")));
                //หุ้นสะสม
                for (int a = 0; a < ds.Tables[0].Rows.Count; a++)
                {
                    for (int x = 0; x < cb.Length; x++)
                    {
                        cb[x].Items.Add(new example.Class.ComboBoxPay(ds.Tables[0].Rows[a][2].ToString(),
                        ds.Tables[0].Rows[a][1].ToString(),
                        "500"));

                    }
                }
                //กู้
                
                if (ds.Tables[2].Rows.Count != 0)
                {
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        for (int a = 0; a < ds.Tables[1].Rows.Count; a++)
                        {
                            for (int x = 0; x < cb.Length; x++)
                            {
                                int MonthLoan = Convert.ToInt32(ds.Tables[1].Rows[a][5].ToString());
                                int YearLoan = Convert.ToInt32(ds.Tables[1].Rows[a][6].ToString());
                                int PayNo = Convert.ToInt32(ds.Tables[1].Rows[a][4].ToString()) -1 ;
                                int sumy = MonthLoan + PayNo;
                                int Balance = 0;
                                while (sumy >= 13)
                                {
                                    YearLoan++;
                                    sumy = sumy - 12;
                                    if (sumy < 13)
                                    {
                                        MonthLoan = sumy;
                                    }
                                }

                                DateTime DateLoan = Convert.ToDateTime(YearLoan + "-" + MonthLoan + "-" + DateTime.DaysInMonth(YearLoan, MonthLoan).ToString());
                                if (Convert.ToDateTime(CByeartap1.Text + '-' + CBMonth.Text + '-' + DateTime.DaysInMonth(Convert.ToInt32(CByeartap1.Text), Convert.ToInt32(CBMonth.Text)).ToString()) <= DateLoan)
                                {
                                    Balance = 0;
                                    if (DateLoan == Convert.ToDateTime(CByeartap1.Text + '-' + CBMonth.Text + '-' + DateTime.DaysInMonth(Convert.ToInt32(CByeartap1.Text), Convert.ToInt32(CBMonth.Text)).ToString()))
                                    {
                                        Balance = Convert.ToInt32(ds.Tables[1].Rows[a][7].ToString());
                                    }
                                    else
                                    {
                                        Balance = Convert.ToInt32(ds.Tables[1].Rows[a][1].ToString());
                                    }
                                    cb[x].Items.Add(new example.Class.ComboBoxPay("รายการกู้ " + ds.Tables[1].Rows[a][3].ToString(), Balance.ToString(),
                                        ds.Tables[1].Rows[a][3].ToString()));
                                }

                            }

                        }
                    }
                        //กรณีเดือนแรก
                    else if (ds.Tables[1].Rows.Count <= 0 && (Convert.ToDateTime(ds.Tables[2].Rows[0][1].ToString() + '-' + ds.Tables[2].Rows[0][0].ToString() + '-' +
                        DateTime.DaysInMonth(Convert.ToInt32(ds.Tables[2].Rows[0][1].ToString()), Convert.ToInt32(ds.Tables[2].Rows[0][0].ToString())).ToString())).ToString() == (Convert.ToDateTime(CByeartap1.Text + '-' + CBMonth.Text + '-' + DateTime.DaysInMonth(Convert.ToInt32(CByeartap1.Text), Convert.ToInt32(CBMonth.Text)).ToString())).ToString())
                    {
                        if(ds.Tables[3].Rows.Count == 0)
                        {
                            cb[0].Items.Add(new example.Class.ComboBoxPay("รายการกู้ " + ds.Tables[2].Rows[0][3].ToString(), ds.Tables[2].Rows[0][2].ToString(),
                                        ds.Tables[2].Rows[0][3].ToString()));
                        }
                        
                    }
                    if(CBStatus.Items.Count > 0)
                    {
                        CBStatus.Enabled = true;
                    }
                }
                button4.Enabled = true;
                //else
                //{ 
                //    CBStatus.Enabled = false;
                //    CBStatus.SelectedIndex = -1;
                //}
            }

        }
        // if message in Text nothing will not Open next
        // ถ้า ไม่มีข้อความ ใน กล่อง จะไม่เปิดใช่งานกล่อง ถัดไป
        
        // if message in Text nothing will not Open next
        // ถ้า ไม่มีข้อความ ใน กล่อง จะไม่เปิดใช่งานกล่อง ถัดไป

        // if message in Text nothing will not Open next
        // ถ้า ไม่มีข้อความ ใน กล่อง จะไม่เปิดใช่งานกล่อง ถัดไป
        private void CBB3_SelectedIndexChanged(object sender, EventArgs e)
        {
            example.Class.ComboBoxPay Status = (CBStatus.SelectedItem as example.Class.ComboBoxPay);
            if (CBStatus.SelectedIndex != -1 && TBTeacherNo.Text.Length == 6)
            {

                TBStartAmountShare.Enabled = false;
                if (Status.Type.Contains("กู้"))
                {
                    TBStartAmountShare.Text = Status.Balance;
                    if (TBStartAmountShare.Text == "")
                        TBStartAmountShare.Text = "0";
                    BTAdd.Enabled = true;
                }
                else if (Status.Type.Contains("สะสม"))
                {
                    TBStartAmountShare.Text = "500";
                    TBStartAmountShare.Enabled = true;
                    BTAdd.Enabled = true;
                }
                CBB4Oppay.SelectedIndex = 0;
            }
            else
            {
                TBStartAmountShare.Text = "";
                CBStatus.Text = "";

                BTAdd.Enabled = false;
            }
        }
        // if message in Text nothing will not Open next
        // ถ้า ไม่มีข้อความ ใน กล่อง จะไม่เปิดใช่งานกล่อง ถัดไป
        private void CBB4Oppay_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount != 0)
            {
                CBB4Oppay.Enabled = true;
            }
            if (CBB4Oppay.SelectedIndex != -1)
            {
                BTsave.Enabled = true;

            }
            else { BTsave.Enabled = false; }
        }
        //----------------------- End code -------------------- ////////



        //------------------------- ClickDelete&Empty --------- //
        // Comment!
        private void button1_Click_1(object sender, EventArgs e)
        {
            CByeartap1.SelectedIndex = -1;
            CBMonth.SelectedIndex = -1;
            CBStatus.SelectedIndex = -1;
            CBStatus.Enabled = false;
            CBB4Oppay.Enabled = false;
            BTAdd.Enabled = false;
            CBB4Oppay.SelectedIndex = -1;
            BTsave.Enabled = false;
            TBStartAmountShare.Text = string.Empty;
            dataGridView1.Rows.Clear();
            label5.Text = "";
            sum = 0;
            button4.Enabled = false;
        }
        // Comment!
        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int currentMouseOverRow = dataGridView1.HitTest(e.X, e.Y).RowIndex;
                if (currentMouseOverRow != -1)
                {
                    SelectIndexRow = currentMouseOverRow;
                    ContextMenu m = new ContextMenu();
                    m.MenuItems.Add(new MenuItem("ลบออก"));
                    m.Show(dataGridView1, new Point(e.X, e.Y));
                    m.MenuItems[0].Click += new System.EventHandler(this.Delete_Click);
                }
            }
        }
        // Comment!
        private void Delete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count != 0)
            {
                sum -= int.Parse(dataGridView1.Rows[SelectIndexRow].Cells[2].Value.ToString());
                x = sum;
                label5.Text = sum.ToString();
            }
            if (SelectIndexRow != -1)
            {
                this.RestoreComboboxafterdelete();
                dataGridView1.Rows.RemoveAt(SelectIndexRow);
                SelectIndexRow = -1;
                if (dataGridView1.Rows.Count == 0)
                {
                    CBB4Oppay.Enabled = false;
                    CBB4Oppay.SelectedIndex = -1;
                }
            }
        }
        private void RestoreComboboxafterdelete ()
        {
            if (dataGridView1.Rows[SelectIndexRow].Cells[1].Value.ToString().Contains("กู้"))
            {
                ComboBox[] cb = new ComboBox[] { CBStatus };
                for (int x = 0; x < cb.Length; x++)
                    cb[x].Items.Add(new example.Class.ComboBoxPay(dataGridView1.Rows[SelectIndexRow].Cells[1].Value.ToString(), dataGridView1.Rows[SelectIndexRow].Cells[2].Value.ToString(),
                                             dataGridView1.Rows[SelectIndexRow].Cells[3].Value.ToString()));
            }
            
        }
        //----------------------- End code -------------------- ////////

        //------------------------- SUMAmountShare --------- //
        // Comment! //
        private void BTAdd_Click(object sender, EventArgs e)
        {
            
            example.Class.ComboBoxPay Loan = (CBStatus.SelectedItem as example.Class.ComboBoxPay);
            if (dataGridView1.Rows.ToString() != "")
            {
                
                if (dataGridView1.Rows.Count == 0)
                {
                    int BALANCE = 0;
                    CBB4Oppay.Enabled = true;
                    String Time = CByeartap1.Text + "/" + CBMonth.Text;
                    dataGridView1.Rows.Add(Time, CBStatus.Text, TBStartAmountShare.Text, Loan.No);
                    if (CBStatus.Text.Contains("กู้"))
                    {
                        CBStatus.Items.RemoveAt(CBStatus.SelectedIndex);
                    }
                    for(int x = 0; x < dataGridView1.Rows.Count; x++)
                    {
                        BALANCE += Convert.ToInt32(dataGridView1.Rows[x].Cells[2].Value.ToString());
                    }
                    label5.Text = BALANCE.ToString();
                }
                else
                {
                    int TicketName = 0;
                    for (int x = 0; x < dataGridView1.Rows.Count; x++)
                    {
                        if (CBStatus.Text == dataGridView1.Rows[x].Cells[1].Value.ToString())
                        {
                            if(dataGridView1.Rows[x].Cells[1].Value.ToString().Contains("สะสม"))
                                if(Int32.TryParse(TBStartAmountShare.Text,out int value)&& value > 0)
                                {
                                    dataGridView1.Rows[x].Cells[2].Value = TBStartAmountShare.Text;
                                    sum = 0;
                                    for(int a = 0; x < dataGridView1.Rows.Count; x++)
                                    {
                                        sum += Convert.ToInt32(dataGridView1.Rows[a].Cells[2].Value);
                                    }
                                    label5.Text = sum.ToString();
                                }
                            TicketName = 1;
                        }

                    }
                    if (TicketName == 0)
                    {
                        int BALANCE = 0;
                        String Time = CByeartap1.Text + "/" + CBMonth.Text;
                        dataGridView1.Rows.Add(Time, CBStatus.Text, TBStartAmountShare.Text,Loan.No);
                        if (CBStatus.Text.Contains("กู้"))
                        {
                            CBStatus.Items.RemoveAt(CBStatus.SelectedIndex);
                        }
                        for (int x = 0; x < dataGridView1.Rows.Count; x++)
                        {
                            BALANCE += Convert.ToInt32(dataGridView1.Rows[x].Cells[2].Value.ToString());
                        }
                        label5.Text = BALANCE.ToString();
                    }
                }
                CBStatus.SelectedIndex = 0;

            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
        }
        public void tabPage2_Click(object sender, EventArgs e)
        {
        }
        private void TBStartAmountShare_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && (e.KeyChar != '\b'))
            {
                e.Handled = true;
            }
        }

        private void TBTeacherNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (TBTeacherNo.Text.Length == 6)
                {
                    DataTable dt = Class.SQLConnection.InputSQLMSSQL(SQLDefault[6].Replace("T{TeacherNo}", TBTeacherNo.Text));
                    if (dt.Rows.Count != 0)
                    {
                        sum = 0; x = 0;
                        label5.Text = sum.ToString();
                        dataGridView1.Rows.Clear();
                        TBStartAmountShare.Clear();
                        CBStatus.SelectedIndex = -1;
                        CByeartap1.SelectedIndex = -1;
                        CByeartap1.Items.Clear();
                        CBMonth.SelectedIndex = -1;
                        dataGridView2.Rows.Clear();
                        dataGridView3.Rows.Clear();
                        CByeartap2.SelectedIndex = -1;
                        CByeartap2.Items.Clear();
                        CBSelectLoan.SelectedIndex = -1;
                        CBSelectLoan.Items.Clear();
                        CBMonth.Items.Clear();
                        CBMonth.SelectedIndex = -1;
                        TBAmountShareofsystem.Text = "";
                        TBAmountShareofyear.Text = "";
                        TBSum.Text = "";
                        TBAmountRemain.Text = "";
                        TBInterrest.Text = "";
                        TBAmoun.Text = "";
                        TBPayNo.Text = "";

                            TBTeacherName.Text = dt.Rows[0][1].ToString();
                        TBTeacherBill.Text = dt.Rows[0][2].ToString();
                        TBTeacherIDNo.Text = dt.Rows[0][3].ToString();
                        TBidno.Text = dt.Rows[0][4].ToString();
                        TBTel.Text = dt.Rows[0][5].ToString();
                        TBStartAmount2.Text = dt.Rows[0][6].ToString();
                        TBstatus.Text = dt.Rows[0][7].ToString();
                        Check = 1;
                        CBStatus.SelectedIndex = -1;
                        CByeartap1.Enabled = true;
                    }

                }

            }
            else if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
            {
                if (Check == 1)
                {
                    sum = 0;
                    x = 0;
                    label5.Text = sum.ToString();
                    dataGridView1.Rows.Clear();
                    TBStartAmountShare.Text = "";
                    CBStatus.SelectedIndex = -1;
                    TBTeacherBill.Text = "";
                    TBTeacherName.Text = "";
                    CBStatus.Enabled = false;
                    Check = 0;
                    CByeartap2.Enabled = false;
                    CBSelectLoan.Enabled = false;
                    CByeartap1.Enabled = false;
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (CBMonth.Text != "")
            {
                if (CBStatus.Items.Count != 0)
                {
                    if(Auto == 0)
                    {

                        dataGridView1.Rows.Clear();
                        label5.Text = "0";
                        sum = 0;
                        for (int x = 0; x < CBStatus.Items.Count; x++)
                        {
                            CBStatus.SelectedIndex = x;
                            example.Class.ComboBoxPay Loan = (CBStatus.SelectedItem as example.Class.ComboBoxPay);
                            String Time = CByeartap1.Text + "/" + CBMonth.Text;
                            dataGridView1.Rows.Add(Time, CBStatus.Text, TBStartAmountShare.Text, Loan.No);
                            sum += Convert.ToInt32(Loan.Balance);
                            label5.Text = sum.ToString();
                        }
                        for (int a = 0; a < CBStatus.Items.Count; a++)
                        {
                            CBStatus.SelectedIndex = a;
                            if (CBStatus.Text.Contains("กู้"))
                                CBStatus.Items.RemoveAt(a);
                        }
                        try
                        {
                            CBStatus.SelectedIndex = 0;
                            CBB4Oppay.Enabled = true;
                            CBB4Oppay.SelectedIndex = 0;
                            Auto = 1;
                        }
                        catch
                        {
                            Console.WriteLine("===== Don't have Index in Combobox. =====");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("ไม่พบรายการ", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            
        }

        private void pay_Load(object sender, EventArgs e)
        {
            ComboBox[] cb = new ComboBox[] { CBB4Oppay };
            DataTable dtPayment = Class.SQLConnection.InputSQLMSSQL(SQLDefault[8]);
            for (int a = 0; a < dtPayment.Rows.Count; a++)
                for (int x = 0; x < cb.Length; x++)
                    cb[x].Items.Add(new example.Class.ComboBoxPayment(dtPayment.Rows[a][0].ToString(),
                        dtPayment.Rows[a][1].ToString()));
        }
        private void CBSelectLoan_SelectedIndexChanged(object sender, EventArgs e)
        {
            example.Class.ComboBoxPayment Loan = (CBSelectLoan.SelectedItem as example.Class.ComboBoxPayment);
            if (CBSelectLoan.SelectedIndex != -1)
            {
                DataSet ds = example.Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[10].Replace("{LoanID}", Loan.No));
                dataGridView3.Rows.Clear();
                if (ds.Tables[0].Rows.Count != 0)
                {
                    int RemainAmount = 0;
                    for (int x = 0; x < ds.Tables[0].Rows.Count; x++)
                    {
                        RemainAmount += Convert.ToInt32(Convert.ToDouble(ds.Tables[0].Rows[x][9].ToString()));
                    }

                    TBSum.Text = (Convert.ToDouble(Convert.ToDouble(ds.Tables[0].Rows[0][8].ToString()) + (Convert.ToDouble(ds.Tables[0].Rows[0][8].ToString()) * Convert.ToDouble(ds.Tables[0].Rows[0][7].ToString()) / 100)).ToString());
                    TBAmountRemain.Text = RemainAmount.ToString();
                    TBInterrest.Text = Convert.ToInt32(Convert.ToDouble(ds.Tables[0].Rows[0][7].ToString()) / 100 * Convert.ToDouble(ds.Tables[0].Rows[0][8].ToString())).ToString();
                    TBAmoun.Text = Convert.ToInt32(ds.Tables[0].Rows[0][8].ToString()).ToString();
                    TBPayNo.Text = ds.Tables[0].Rows[0][6].ToString();

                    int Month = Convert.ToInt32(ds.Tables[0].Rows[0][4].ToString());
                    int Year = Convert.ToInt32(ds.Tables[0].Rows[0][5].ToString());
                    //DGVLoanDetail.Rows.Clear();

                    Double Interest = Convert.ToDouble(Convert.ToDouble(ds.Tables[0].Rows[0][8].ToString())) * (Convert.ToDouble(ds.Tables[0].Rows[0][7].ToString()) / 100) / Convert.ToDouble(ds.Tables[0].Rows[0][6].ToString());

                    int Pay = Convert.ToInt32(Convert.ToDouble(ds.Tables[0].Rows[0][8].ToString()) / Convert.ToInt32(ds.Tables[0].Rows[0][6].ToString()));
                    int SumInstallment = Convert.ToInt32(Pay + Interest);
                    String StatusPay = "";

                    for (int Num = 0; Num < int.Parse(ds.Tables[0].Rows[0][6].ToString()); Num++)
                    {
                        if (Month > 12)
                        {
                            Month = 1;
                            Year++;
                        }
                        if (Num == Convert.ToInt32(ds.Tables[0].Rows[0][6].ToString()) - 1)
                        {
                            Interest = Convert.ToInt32((Convert.ToDouble(ds.Tables[0].Rows[0][8].ToString()) * (Convert.ToDouble(ds.Tables[0].Rows[0][7].ToString()) / 100)) - (Convert.ToInt32(Interest) * Num));
                            Pay = Pay * Num;
                            Pay = Convert.ToInt32(ds.Tables[0].Rows[0][8].ToString()) - Pay;
                            SumInstallment = Convert.ToInt32(Pay + Interest);
                            //hell
                        }
                        try
                        {
                            for (int a = 0; a < ds.Tables[1].Rows.Count; a++)
                            {
                                if (Month + "/" + Year == ds.Tables[1].Rows[a][0].ToString())
                                {
                                    StatusPay = "จ่ายแล้ว";
                                    break;
                                }
                                else
                                {
                                    StatusPay = "ยังไม่จ่าย";
                                }
                            }

                        }
                        catch
                        {
                            StatusPay = "ยังไม่จ่าย";
                        }

                        dataGridView3.Rows.Add($"{Month}/{Year}", Pay, Convert.ToInt32(Interest), SumInstallment, StatusPay);
                        Month++;
                    }
                }
            }
        }
        private void CByeartap2_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView2.Rows.Clear();
            if(CByeartap2.Text != "")
            {
                int Month = 1;
                DataSet ds = example.Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[11]
                .Replace("{TeacherNo}", TBTeacherNo.Text)
                .Replace("{Year}", CByeartap2.Text));
                TBAmountShareofsystem.Text = ds.Tables[1].Rows[0][1].ToString();
                DateTime Date;
                int ShareOfYear = 0;
                if (ds.Tables[1].Rows.Count != 0)
                {
                    Date = DateTime.Parse(ds.Tables[1].Rows[0][2].ToString());
                    //int Mont = int.Parse(Date.ToString("MM"));
                    
                    if (int.Parse(CByeartap2.SelectedItem.ToString()) == int.Parse(Date.ToString("yyyy")))
                    {
                        Month = int.Parse(Date.ToString("MM"));
                    }
                }
                for (int a = Month; a <= 12; a++)
                {
                    
                    dataGridView2.Rows.Add(a + "/" + CByeartap2.SelectedItem.ToString(), ds.Tables[1].Rows[0][0].ToString(), "ยังไม่ได้ชำระ");
                    bool Check = true;
                    for (int x = 0; x < ds.Tables[0].Rows.Count; x++)
                    {
                        if(Convert.ToInt32(ds.Tables[0].Rows[x][1].ToString()) == a)
                        {
                            dataGridView2.Rows.RemoveAt(a - Month);
                            dataGridView2.Rows.Add(a + "/" + CByeartap2.SelectedItem.ToString(), ds.Tables[0].Rows[x][0].ToString(), "ชำระแล้ว");
                            ShareOfYear += Convert.ToInt32(ds.Tables[0].Rows[x][0].ToString());
                        }
                        if (!Check)
                            break;
                    }
                }
                TBAmountShareofyear.Text = ShareOfYear.ToString();
            }
            
        }
        //----------------------- End code -------------------//
    }
}
