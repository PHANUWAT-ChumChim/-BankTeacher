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
        ///<para>[13] SELECT lasted billno INPUT: </para>
        ///<para>[12] Check last pay date INPUT: {TeacherNo} </para>
        ///<para>[13] SELECT lasted billno INPUT:</para>
        ///<para>[14] Select Billinfomation INPUT: {TeacherNo , {Year}</para>
        ///<para>[15] Check Month in year INPUT: {TeacherNo} {Year} </para>
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
          "WHERE a.MemberStatusNo = 1 and a.TeacherNo LIKE '%{Text}%'  or CAST(c.PrefixName+' '+[Fname] +' '+ [Lname] as NVARCHAR) LIKE '%{Text}%'   and a.MemberStatusNo = 1         \r\n " +
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
          "--{haveLoan1}UPDATE EmployeeBank.dbo.tblGuarantor \r\n " +
          "--{haveLoan1}SET RemainsAmount = (SELECT RemainsAmount FROM EmployeeBank.dbo.tblGuarantor WHERE TeacherNo = '{TeacherGuaNo1}' and LoanNo = {LoanNo}) - Convert(float , (((SELECT Amount FROM EmployeeBank.dbo.tblGuarantor WHERE TeacherNo = '{TeacherGuaNo1}' and LoanNo = {LoanNo}) / @Amount) * '{AmountPayLoan}')) \r\n " +
          "--{haveLoan1}WHERE TeacherNo = '{TeacherGuaNo1}' and LoanNo = {LoanNo}; \r\n " +
          " \r\n " +
          "--{haveLoan2}UPDATE EmployeeBank.dbo.tblGuarantor \r\n " +
          "--{haveLoan2}SET RemainsAmount = (SELECT RemainsAmount FROM EmployeeBank.dbo.tblGuarantor WHERE TeacherNo = '{TeacherGuaNo2}' and LoanNo = {LoanNo}) - Convert(float , (((SELECT Amount FROM EmployeeBank.dbo.tblGuarantor WHERE TeacherNo = '{TeacherGuaNo2}' and LoanNo = {LoanNo}) / @Amount) * '{AmountPayLoan}')) \r\n " +
          "--{haveLoan2}WHERE TeacherNo = '{TeacherGuaNo2}' and LoanNo = {LoanNo}; \r\n " +
          " \r\n " +
          "--{haveLoan3}UPDATE EmployeeBank.dbo.tblGuarantor \r\n " +
          "--{haveLoan3}SET RemainsAmount = (SELECT RemainsAmount FROM EmployeeBank.dbo.tblGuarantor WHERE TeacherNo = '{TeacherGuaNo3}' and LoanNo = {LoanNo}) - Convert(float , (((SELECT Amount FROM EmployeeBank.dbo.tblGuarantor WHERE TeacherNo = '{TeacherGuaNo3}' and LoanNo = {LoanNo}) / @Amount)* '{AmountPayLoan}')) \r\n " +
          "--{haveLoan3}WHERE TeacherNo = '{TeacherGuaNo3}' and LoanNo = {LoanNo}; \r\n " +
          " \r\n " +
          "--{haveLoan4}UPDATE EmployeeBank.dbo.tblGuarantor \r\n " +
          "--{haveLoan4}SET RemainsAmount = (SELECT RemainsAmount FROM EmployeeBank.dbo.tblGuarantor WHERE TeacherNo = '{TeacherGuaNo4}' and LoanNo = {LoanNo}) - Convert(float , (((SELECT Amount FROM EmployeeBank.dbo.tblGuarantor WHERE TeacherNo = '{TeacherGuaNo4}' and LoanNo = {LoanNo}) /@Amount)* '{AmountPayLoan}')) \r\n " +
          "--{haveLoan4}WHERE TeacherNo = '{TeacherGuaNo4}' and LoanNo = {LoanNo}; \r\n " +
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
          "--{haveSaving}INSERT INTO EmployeeBank.dbo.tblBillDetail (BillNo,TypeNo,Amount,Mount,Year,BillDetailPaymentNo) \r\n " +
          "--{haveSaving}VALUES({BillNo} , '1','{AmountPaySaving}','{Month}','{Year}','{Payment}') \r\n " +
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
          "  and dd.MemberStatusNo = 1 and bb.TypeNo = 2  and LoanStatusNo = 2 )and a.TeacherNo = '{TeacherNo}'  and c.TypeNo = 2 and LoanStatusNo =2 and DATEADD(YYYY,0,'{DateSet}') <= EOMONTH(DATEADD(MONTH , PayNo-1,CAST(CAST(CAST(YearPay as nvarchar) +'/' + CAST(MonthPay AS nvarchar) + '/01' AS nvarchar) AS date)))) and (DATEADD(YYYY,0,'{DateSet}') >= EOMONTH(CAST(YearPay as nvarchar) +'/' + CAST(MonthPay as nvarchar) +'/01') ) \r\n " +
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
          "and Cancel = 1 and TypeNo = 2 and c.LoanStatusNo = 2 ; \r\n "+
          "\r\n \r\n"+
          " SELECT * \r\n " +
          " FROM EmployeeBank.dbo.tblBill as a \r\n " +
          " LEFT JOIN EmployeeBank.dbo.tblBillDetail as b on a.BillNo = b.BillNo \r\n " +
          " LEFT JOIN EmployeeBank.dbo.tblLoan as c on b.LoanNo = b.LoanNo \r\n " +
          " WHERE  LoanStatusNo = 2 and c.TeacherNo =  '{TeacherNo}' and a.TeacherNo = '{TeacherNo}' and  Cancel = 1 and Mount ={Month} and Year = {Year};"

          ,
          //[8] Check BillDetailPayment INPUT: -  
          "SELECT Convert(nvarchar(50) , Name) , BillDetailpaymentNo  \r\n " +
          "FROM EmployeeBank.dbo.tblBillDetailPayment \r\n " +
          "WHERE Status = 1 "
          ,
          //[9] SELECT LOANID and SELECT DATE Register Member INPUT : {TeacherNo}
          "SELECT LoanNo \r\n " +
          "FROM EmployeeBank.dbo.tblLoan  \r\n " +
          "WHERE TeacherNo = '{TeacherNo}' and LoanStatusNo = 2 ; \r\n " +
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
          //[12] Check last pay date INPUT: {TeacherNo} 
          "SELECT EOMONTH(CAST(CAST(Year as nvarchar)+'-'+ CAST(Mount as nvarchar) +'-10' as nvarchar))as MaxDate , b.BillNo ,DATEADD(MONTH,5,CAST(CAST(CAST(Year as nvarchar) +'/' + CAST(Mount AS nvarchar) + '/05' AS nvarchar) AS date))  \r\n " +
          "FROM EmployeeBank.dbo.tblBill as a \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblBillDetail as b on a.BillNo = b.BillNo \r\n " +
          "WHERE TeacherNo = '{TeacherNo}' \r\n " +
          "ORDER BY Maxdate DESC; "
          ,
          //[13] SELECT lasted billno INPUT: 
          "SELECT IDENT_CURRENT('EmployeeBank.dbo.tblBill')+1 "
            ,
          //[14] Select Billinfomation INPUT: {TeacherNo , {Year}
          "SELECT a.BillNo ,CAST(CAST(b.Mount as nvarchar(2))+'/'+CAST(b.Year as nvarchar(4)) as nvarchar(10))  , TypeName , Amount , d.Name , DateAdd \r\n " +
          "FROM EmployeeBank.dbo.tblBill as a \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblBillDetail as b on a.BillNo = b.BillNo \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblBillDetailType as c on b.TypeNo = c.TypeNo \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblBillDetailPayment as d on b.BillDetailPaymentNo = d.BillDetailPaymentNo \r\n " +
          "WHERE TeacherNo = '{TeacherNo}' and Year = {Year} and Cancel = 1 \r\n " +
          "ORDER BY b.Mount "
          ,
          //[15] Check Month in year INPUT: {TeacherNo} {Year} 
          "SELECT b.BillNo \r\n " +
          "FROM EmployeeBank.dbo.tblBillDetail as a \r\n " +
          "RIGHT JOIN EmployeeBank.dbo.tblBill as b on b.BillNo = a.BillNo \r\n " +
          "WHERE TeacherNo = '{TeacherNo}' and Year = {Year} "
          ,


         };


        //=================================== Load Form ============================================
        //Open Form
        public pay(int TabIndex)
        {
            InitializeComponent();
            tabControl1.SelectedIndex = TabIndex;

            Font F = new Font("TH Sarabun New", 16, FontStyle.Regular);
            DGV_Pay.ColumnHeadersDefaultCellStyle.Font = F;
            DGV_ShareInfo.ColumnHeadersDefaultCellStyle.Font = F;
        }

        //ChangeSizeForm
        private void Menuf_SizeChanged(object sender, EventArgs e)
        {
            int x = this.Width / 2 - panel1.Size.Width / 2;
            int y = this.Height / 2 - panel1.Size.Height / 2;
            panel1.Location = new Point(x, y);
        }

        //Load Form
        private void pay_Load(object sender, EventArgs e)
        {
            ComboBox[] cb = new ComboBox[] { CBPayment_Pay };
            DataTable dtPayment = Class.SQLConnection.InputSQLMSSQL(SQLDefault[8]);
            for (int a = 0; a < dtPayment.Rows.Count; a++)
                for (int x = 0; x < cb.Length; x++)
                    cb[x].Items.Add(new example.Class.ComboBoxPayment(dtPayment.Rows[a][0].ToString(),
                        dtPayment.Rows[a][1].ToString()));
        }
        //=============================================================================================


        //==================================Header===================================================
        //SearchButton
        private void BSearchTeacher_Click(object sender, EventArgs e)
        {
            Bank.Search IN = new Bank.Search(SQLDefault[0]);
            IN.ShowDialog();
            if(Bank.Search.Return[0] != "")
            {
                TBTeacherNo.Text = Bank.Search.Return[0];
                TBTeacherNo_KeyDown(sender, new KeyEventArgs(Keys.Enter));
            }
        }

        //WriteIDTeacher
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

                        TBTeacherBill.Text = "";
                        Freezing_Form(true);
                        ClearForm();
                        TBTeacherName.Text = dt.Rows[0][1].ToString();
                        Check = 1;
                        CBList_Pay.SelectedIndex = -1;
                        CBYearSelection_Pay.Enabled = true;
                        ComboBox[] cb = new ComboBox[] { CBLoanSelection_LoanInfo };
                        DataSet ds = Class.SQLConnection.InputSQLMSSQLDS((SQLDefault[9]+
                            "\r\n" +
                            SQLDefault[12]).Replace("{TeacherNo}", TBTeacherNo.Text));
                        for (int x = 0; x < ds.Tables[0].Rows.Count; x++)
                        {
                            for (int aa = 0; aa < cb.Length; aa++)
                            {
                                cb[aa].Items.Add(new example.Class.ComboBoxPayment("รายการกู้ " + ds.Tables[0].Rows[x][0].ToString(), ds.Tables[0].Rows[x][0].ToString()));
                            }
                        }

                        if (CBLoanSelection_LoanInfo.Items.Count == 1)
                        {
                            CBLoanSelection_LoanInfo.SelectedIndex = 0;
                        }
                        int YearRegister = Convert.ToInt32((Convert.ToDateTime(ds.Tables[1].Rows[0][0].ToString())).ToString("yyyy"));
                        int Yearlastofpay = Convert.ToInt32((Convert.ToDateTime(ds.Tables[2].Rows[0][2].ToString())).ToString("yyyy"));
                        Yearlastofpay = Yearlastofpay - YearRegister;
                        if (YearRegister < Convert.ToInt32(example.GOODS.Menu.Date[0]) - 2)
                        {
                            int Yeard2 = Convert.ToInt32(example.GOODS.Menu.Date[0]) - 2;

                            while (Yeard2 <= Convert.ToInt32(example.GOODS.Menu.Date[0]) + Yearlastofpay)
                            {
                                CBYearSelection_Pay.Items.Add(Yeard2);
                                CBYearSelection_ShareInfo.Items.Add(Yeard2);
                                CBYearSelection_BillInfo.Items.Add(Yeard2);
                                if(Yeard2 == Convert.ToInt32(example.GOODS.Menu.Date[0]) + Yearlastofpay)
                                {
                                    DataTable dtCheckMonthinlastyear = example.Class.SQLConnection.InputSQLMSSQL(SQLDefault[15]
                                        .Replace("{TeacherNo}", TBTeacherNo.Text)
                                        .Replace("{Year}", CBYearSelection_BillInfo.Items[CBYearSelection_BillInfo.Items.Count - 1].ToString()));
                                    if(dtCheckMonthinlastyear.Rows.Count == 0)
                                    {
                                        CBYearSelection_BillInfo.Items.RemoveAt(CBYearSelection_BillInfo.Items.Count-1);
                                    }
                                }
                                Yeard2++;
                            }
                        }
                        else if (YearRegister > Convert.ToInt32(example.GOODS.Menu.Date[0]) - 2)
                        {
                            while (YearRegister <= Convert.ToInt32(example.GOODS.Menu.Date[0]) + Yearlastofpay)
                            {
                                CBYearSelection_Pay.Items.Add(YearRegister);
                                CBYearSelection_ShareInfo.Items.Add(YearRegister);
                                CBYearSelection_BillInfo.Items.Add(YearRegister);
                                if (YearRegister == Convert.ToInt32(example.GOODS.Menu.Date[0]) + Yearlastofpay)
                                {
                                    DataTable dtCheckMonthinlastyear = example.Class.SQLConnection.InputSQLMSSQL(SQLDefault[15]
                                        .Replace("{TeacherNo}", TBTeacherNo.Text)
                                        .Replace("{Year}", CBYearSelection_BillInfo.Items[CBYearSelection_BillInfo.Items.Count - 1].ToString()));
                                    if (dtCheckMonthinlastyear.Rows.Count == 0)
                                    {
                                        CBYearSelection_BillInfo.Items.RemoveAt(CBYearSelection_BillInfo.Items.Count - 1);
                                    }
                                }
                                YearRegister++;
                            }
                        }
                        if (CBList_Pay.Items.Count > 0)
                        {
                            BAutoSelection_Click(sender, new EventArgs());
                        }
                        CBYearSelection_Pay.Text = example.GOODS.Menu.Date[0];
                        CBMonthSelection_Pay.SelectedIndex = 0;
                    }

                }

            }
            else if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back || e.KeyCode == Keys.Escape)
            {
                if (Check == 1)
                {
                    sum = 0;
                    x = 0;
                    //Header =============================================
                    TBTeacherBill.Text = "";
                    TBTeacherName.Text = "";
                    //====================================================
                    ClearForm();
                    Check = 0;
                }
            }
        }
        //=============================================================================================


        // ---------------------------------------------TABCONTROL ------------------------------------------------------------------------------------------------

        //KeyDown in alltab
        private void tabControl1_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Escape)
            {
                TBTeacherNo_KeyDown(sender, new KeyEventArgs(Keys.Delete));
                CBMonthSelection_Pay.SelectedIndex = -1;
                CBYearSelection_Pay.Enabled = true;
                CBYearSelection_ShareInfo.Enabled = true;
                CBLoanSelection_LoanInfo.Enabled = true;
            }
        }


        //============================== tabpage 1 (Pay) ============================================
        //SelectYear
        private void CBYearSelection_Pay_SelectedIndexChanged(object sender, EventArgs e)
        {
            CBPayment_Pay.Enabled = false;
            CBPayment_Pay.SelectedIndex = -1;
            BSave_Pay.Enabled = false;
            LBalance_Pay.Text = "0";
            if (DGV_Pay.Rows.Count > 0)
            {
                DGV_Pay.Rows.Clear();
            }
            if (CBYearSelection_Pay.SelectedIndex != -1)
            {

                DataSet ds = Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[9]
                            .Replace("{TeacherNo}", TBTeacherNo.Text) +
                            "\r\n" +
                            SQLDefault[12].Replace("{TeacherNo}", TBTeacherNo.Text));
                int Month = Convert.ToInt32((Convert.ToDateTime(ds.Tables[1].Rows[0][0].ToString())).ToString("MM"));
                int Year = Convert.ToInt32((Convert.ToDateTime(ds.Tables[1].Rows[0][0].ToString())).ToString("yyyy"));
                int lastMonthofpay = Convert.ToInt32((Convert.ToDateTime(ds.Tables[2].Rows[0][2].ToString())).ToString("MM"));
                int lastYearofpay = Convert.ToInt32((Convert.ToDateTime(ds.Tables[2].Rows[0][2].ToString())).ToString("yyyy"));
                CBMonthSelection_Pay.Enabled = true;
                CBMonthSelection_Pay.Items.Clear();
                CBMonthSelection_Pay.Text = example.GOODS.Menu.Date[1];
                if (CBYearSelection_Pay.Text == Year.ToString())
                {
                    while (Month <= 12)
                    {
                        if (Convert.ToDateTime((Convert.ToDateTime((Convert.ToInt32(CBYearSelection_Pay.Text) + "-" + Month + "-" + DateTime.DaysInMonth(Convert.ToInt32(CBYearSelection_Pay.Text), Month)).ToString())).ToString("yyyy-MM-dd"))
                            > Convert.ToDateTime((Convert.ToDateTime((lastYearofpay + "-" + lastMonthofpay + "-" + DateTime.DaysInMonth(lastYearofpay, lastMonthofpay)).ToString())).ToString("yyyy-MM-dd")))
                        {
                            break;
                        }
                        CBMonthSelection_Pay.Items.Add(Month);
                        Month++;
                    }
                }
                else
                {
                    for (int x = 1; x <= 12; x++)
                    {
                        if (Convert.ToDateTime((Convert.ToDateTime((Convert.ToInt32(CBYearSelection_Pay.Text) + "-" + x + "-" + DateTime.DaysInMonth(Convert.ToInt32(CBYearSelection_Pay.Text), x)).ToString())).ToString("yyyy-MM-dd"))
                            > Convert.ToDateTime((Convert.ToDateTime((lastYearofpay + "-" + lastMonthofpay + "-" + DateTime.DaysInMonth(lastYearofpay, lastMonthofpay)).ToString())).ToString("yyyy-MM-dd")))
                        {
                            break;
                        }
                        CBMonthSelection_Pay.Items.Add(x);
                    }
                }
                if (CBMonthSelection_Pay.Items.Count != 0)
                {
                    List<int> Remove = new List<int>();
                    int Monthloop = 0;
                    for (int x = 0; x < CBMonthSelection_Pay.Items.Count; x++)
                    {
                        Monthloop = Convert.ToInt32(CBMonthSelection_Pay.Items[x]);
                        DataSet dss = Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[7]
                                    .Replace("{Month}", (Monthloop).ToString())
                                    .Replace("{Year}", CBYearSelection_Pay.Text)
                                    .Replace("{TeacherNo}", TBTeacherNo.Text)
                                    .Replace("{DateSet}", (Convert.ToDateTime(CBYearSelection_Pay.Text + "-" + Monthloop.ToString() + "-" + DateTime.DaysInMonth(Convert.ToInt32(CBYearSelection_Pay.Text), Convert.ToInt32(Monthloop)))).ToString("yyyy-MM-dd")));
                        if (dss.Tables[0].Rows.Count < 1 && dss.Tables[1].Rows.Count < 1) // วัดเดือนแรกไม่ได้
                        {
                            if(dss.Tables[2].Rows.Count != 0)
                            {
                                //ถ้ามีกู้ตรงกับเดือนที่ loop
                                if (dss.Tables[2].Rows[0][0].ToString() == CBMonthSelection_Pay.Items[x].ToString() && dss.Tables[2].Rows[0][1].ToString() == CBYearSelection_Pay.Text)
                                {
                                    if(dss.Tables[4].Rows.Count == 0)
                                    {
                                        CBMonthSelection_Pay.SelectedIndex = 0;
                                        continue;
                                    }
                                }
                            }//รันตั้งแต่ Items 0 - Item.Count -1
                            Remove.Add(x);
                        }
                    }
                    for (int x = 0; x < Remove.Count; x++)
                    {
                        CBMonthSelection_Pay.Items.RemoveAt(Remove[x]);
                        for (int y = 0; y < Remove.Count; y++)
                        {
                            Remove[y] = Remove[y] - 1;
                        }
                    }
                    if (CBMonthSelection_Pay.Items.Count == 0)
                    {
                        CBYearSelection_Pay.Items.RemoveAt(CBYearSelection_Pay.SelectedIndex);
                        CBYearSelection_Pay.SelectedIndex = 0;
                    }
                    else if (CBMonthSelection_Pay.Items.Count != 0)
                    {

                        for (int x = 0; x < CBYearSelection_Pay.Items.Count; x++)
                        {
                            if (CBYearSelection_Pay.Items[x].ToString() == example.GOODS.Menu.Date[0])
                            {
                                for (int y = 0; y < CBMonthSelection_Pay.Items.Count; y++)
                                {
                                    if (CBMonthSelection_Pay.Items[y].ToString() == example.GOODS.Menu.Date[1])
                                    {
                                        break;
                                    }
                                    else if (y == CBMonthSelection_Pay.Items.Count - 1)
                                    {
                                        CBMonthSelection_Pay.SelectedIndex = 0;
                                        CBMonthSelection_Pay.Enabled = true;
                                        break;
                                    }
                                }
                                break;
                            }
                            else if (x == CBYearSelection_Pay.Items.Count - 1)
                            {
                                CBMonthSelection_Pay.SelectedIndex = 0;
                                CBMonthSelection_Pay.Enabled = true;
                                break;
                            }
                        }
                    }
                }

            }
            else
            {
                CBMonthSelection_Pay.Enabled = false;
            }
            if (CBList_Pay.Items.Count == 0)
            {
                CBList_Pay.SelectedIndex = -1;
                TBAmount_Pay.Text = "";
            }

        }

        //SelectMonth
        private void CBMonthSelection_Pay_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CBMonthSelection_Pay.SelectedIndex != -1)
            {
                CBList_Pay.SelectedIndex = -1;
                CBList_Pay.Enabled = false;
                CBPayment_Pay.Enabled = false;
                BListAdd_Pay.Enabled = false;
                CBPayment_Pay.SelectedIndex = -1;
                BSave_Pay.Enabled = false;
                TBAmount_Pay.Text = string.Empty;
                DGV_Pay.Rows.Clear();
                BAutoSelection.Enabled = false;
                CBList_Pay.Items.Clear();
                TBAmount_Pay.Text = "";
                BListAdd_Pay.Enabled = false;
                Auto = 0;
                LBalance_Pay.Text = "0";
                ComboBox[] cb = new ComboBox[] { CBList_Pay };
                DataSet ds = Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[7]
                    .Replace("{Month}", CBMonthSelection_Pay.Text)
                    .Replace("{Year}", CBYearSelection_Pay.Text)
                    .Replace("{TeacherNo}", TBTeacherNo.Text)
                    .Replace("{DateSet}", (Convert.ToDateTime(CBYearSelection_Pay.Text + "-" + CBMonthSelection_Pay.Text + "-" + DateTime.DaysInMonth(Convert.ToInt32(CBYearSelection_Pay.Text), Convert.ToInt32(CBMonthSelection_Pay.Text)))).ToString("yyyy-MM-dd")));
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
                                int PayNo = Convert.ToInt32(ds.Tables[1].Rows[a][4].ToString()) - 1;
                                int sumy = MonthLoan + PayNo;
                                int Balance = 0;
                                while (sumy >= 13)
                                {
                                    YearLoan++;
                                    sumy = sumy - 12;
                                }
                                if (sumy < 13)
                                    MonthLoan = sumy;
                                DateTime DateLoan = Convert.ToDateTime(YearLoan + "-" + MonthLoan + "-" + DateTime.DaysInMonth(YearLoan, MonthLoan).ToString());
                                if (Convert.ToDateTime(CBYearSelection_Pay.Text + '-' + CBMonthSelection_Pay.Text + '-' + DateTime.DaysInMonth(Convert.ToInt32(CBYearSelection_Pay.Text), Convert.ToInt32(CBMonthSelection_Pay.Text)).ToString()) <= DateLoan)
                                {
                                    Balance = 0;
                                    if (DateLoan == Convert.ToDateTime(CBYearSelection_Pay.Text + '-' + CBMonthSelection_Pay.Text + '-' + DateTime.DaysInMonth(Convert.ToInt32(CBYearSelection_Pay.Text), Convert.ToInt32(CBMonthSelection_Pay.Text)).ToString()))
                                    {
                                        try
                                        {
                                            Balance = Convert.ToInt32(ds.Tables[1].Rows[a][7].ToString());
                                        }
                                        catch
                                        {
                                            Balance = Convert.ToInt32(Decimal.Truncate(Convert.ToDecimal(Convert.ToDouble(ds.Tables[1].Rows[a][7].ToString()))) + 1);
                                        }

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
                        DateTime.DaysInMonth(Convert.ToInt32(ds.Tables[2].Rows[0][1].ToString()), Convert.ToInt32(ds.Tables[2].Rows[0][0].ToString())).ToString())).ToString() == (Convert.ToDateTime(CBYearSelection_Pay.Text + '-' + CBMonthSelection_Pay.Text + '-' + DateTime.DaysInMonth(Convert.ToInt32(CBYearSelection_Pay.Text), Convert.ToInt32(CBMonthSelection_Pay.Text)).ToString())).ToString())
                    {
                        if (ds.Tables[3].Rows.Count == 0)
                        {
                            cb[0].Items.Add(new example.Class.ComboBoxPay("รายการกู้ " + ds.Tables[2].Rows[0][3].ToString(), ds.Tables[2].Rows[0][2].ToString(),
                                        ds.Tables[2].Rows[0][3].ToString()));
                        }

                    }
                }
                BAutoSelection.Enabled = true;
            }
            if (CBList_Pay.Items.Count > 0)
            {
                if (CBList_Pay.Items.Count == 1)
                    CBList_Pay.SelectedIndex = 0;
                CBList_Pay.Enabled = true;
            }

        }

        //Auto Input Combobox to datagridview
        private void BAutoSelection_Click(object sender, EventArgs e)
        {
            if (CBMonthSelection_Pay.Text != "")
            {
                if (CBList_Pay.Items.Count != 0)
                {
                    if (Auto == 0)
                    {

                        DGV_Pay.Rows.Clear();
                        LBalance_Pay.Text = "0";
                        sum = 0;
                        for (int x = 0; x < CBList_Pay.Items.Count; x++)
                        {
                            CBList_Pay.SelectedIndex = x;
                            example.Class.ComboBoxPay Loan = (CBList_Pay.SelectedItem as example.Class.ComboBoxPay);
                            String Time = CBYearSelection_Pay.Text + "/" + CBMonthSelection_Pay.Text;
                            DGV_Pay.Rows.Add(Time, CBList_Pay.Text, TBAmount_Pay.Text, Loan.No);
                            sum += Convert.ToInt32(DGV_Pay.Rows[x].Cells[2].Value);
                            LBalance_Pay.Text = sum.ToString();
                        }
                        for (int a = 0; a < CBList_Pay.Items.Count; a++)
                        {
                            CBList_Pay.SelectedIndex = a;
                            if (CBList_Pay.Text.Contains("กู้"))
                                CBList_Pay.Items.RemoveAt(a);
                        }
                        try
                        {
                            CBList_Pay.SelectedIndex = 0;
                            Auto = 1;
                        }
                        catch
                        {
                            Console.WriteLine("===== Don't have Index in Combobox. =====");
                            //
                        }
                        CBPayment_Pay.Enabled = true;
                        CBPayment_Pay.SelectedIndex = 0;

                        if (CBList_Pay.Items.Count <= 0)
                        {
                            CBList_Pay.Enabled = false;
                            BListAdd_Pay.Enabled = false;
                            TBAmount_Pay.Text = "0";
                            //
                        }
                    }
                }
                else
                {
                    MessageBox.Show("ไม่พบรายการ", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

        }

        //Select list in combobox
        private void CBList_Pay_SelectedIndexChanged(object sender, EventArgs e)
        {
            example.Class.ComboBoxPay Status = (CBList_Pay.SelectedItem as example.Class.ComboBoxPay);
            if (CBList_Pay.SelectedIndex != -1 && TBTeacherNo.Text.Length == 6)
            {

                TBAmount_Pay.Enabled = false;
                if (Status.Type.Contains("กู้"))
                {
                    TBAmount_Pay.Text = Status.Balance;
                    if (TBAmount_Pay.Text == "")
                        TBAmount_Pay.Text = "0";
                    BListAdd_Pay.Enabled = true;
                }
                else if (Status.Type.Contains("สะสม"))
                {
                    TBAmount_Pay.Text = "500";
                    TBAmount_Pay.Enabled = true;
                    BListAdd_Pay.Enabled = true;
                }
            }
            else
            {
                TBAmount_Pay.Text = "";
                CBList_Pay.Text = "";

                BListAdd_Pay.Enabled = false;
            }
        }

        //Can only Type Numbers in textbox Amount
        private void TBAmount_Pay_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!Char.IsNumber(e.KeyChar)) && (!Char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        //Add list to datagridview
        private void BListAdd_Pay_Click(object sender, EventArgs e)
        {
            example.Class.ComboBoxPay Loan = (CBList_Pay.SelectedItem as example.Class.ComboBoxPay);
            if (TBAmount_Pay.Text != "")
            {
                if (DGV_Pay.Rows.ToString() != "")
                {

                    if (DGV_Pay.Rows.Count == 0)
                    {
                        CBPayment_Pay.Enabled = true;
                        String Time = CBYearSelection_Pay.Text + "/" + CBMonthSelection_Pay.Text;
                        DGV_Pay.Rows.Add(Time, CBList_Pay.Text, TBAmount_Pay.Text, Loan.No);
                        CBPayment_Pay.SelectedIndex = 0;
                        CBPayment_Pay.Enabled = true;
                        if (CBList_Pay.Text.Contains("กู้"))
                        {
                            CBList_Pay.Items.RemoveAt(CBList_Pay.SelectedIndex);
                            if (CBList_Pay.Items.Count == 0)
                                BListAdd_Pay.Enabled = false;
                        }
                    }
                    else
                    {
                        int TicketName = 0;
                        for (int x = 0; x < DGV_Pay.Rows.Count; x++)
                        {
                            if (CBList_Pay.Text == DGV_Pay.Rows[x].Cells[1].Value.ToString())
                            {
                                if (DGV_Pay.Rows[x].Cells[1].Value.ToString().Contains("สะสม"))
                                    if (Int32.TryParse(TBAmount_Pay.Text, out int value) && value > 0)
                                    {
                                        DGV_Pay.Rows[x].Cells[2].Value = TBAmount_Pay.Text;
                                    }
                                TicketName = 1;
                            }

                        }
                        if (TicketName == 0)
                        {
                            String Time = CBYearSelection_Pay.Text + "/" + CBMonthSelection_Pay.Text;
                            DGV_Pay.Rows.Add(Time, CBList_Pay.Text, TBAmount_Pay.Text, Loan.No);
                            CBPayment_Pay.SelectedIndex = 0;
                            CBPayment_Pay.Enabled = true;
                            if (CBList_Pay.Text.Contains("กู้"))
                            {
                                CBList_Pay.Items.RemoveAt(CBList_Pay.SelectedIndex);
                                if (CBList_Pay.Items.Count == 0)
                                    BListAdd_Pay.Enabled = false;
                            }
                        }
                    }
                    //sum ยอด
                    sum = 0;
                    for (int x = 0; x < DGV_Pay.Rows.Count; x++)
                    {
                        sum += Convert.ToInt32(DGV_Pay.Rows[x].Cells[2].Value.ToString());
                    }
                    LBalance_Pay.Text = sum.ToString();
                    if (CBList_Pay.Items.Count != 0)
                        CBList_Pay.SelectedIndex = 0;
                    else if (CBList_Pay.Items.Count == 0)
                        CBList_Pay.SelectedIndex = -1;

                    if (CBList_Pay.Items.Count <= 0)
                    {
                        CBList_Pay.Enabled = false;
                        BListAdd_Pay.Enabled = false;
                        TBAmount_Pay.Text = "0";
                    }

                }

            }
            else
            {
                MessageBox.Show("กรอกข้อมูลไม่ถูกต้อง", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //Select Payment
        private void CBPayment_Pay_SelectIndexChange(object sender, EventArgs e)
        {
            if (DGV_Pay.RowCount != 0)
            {
                CBPayment_Pay.Enabled = true;
            }
            if (CBPayment_Pay.SelectedIndex != -1 && CBPayment_Pay.Enabled)
            {
                BSave_Pay.Enabled = true;

            }
            else { BSave_Pay.Enabled = false; }
        }

        //Cleartabpage 1 Button
        private void BClearTab_Pay_Click(object sender, EventArgs e)
        {
            Cleartabpage1();
        }

        //SaveInfo Button
        private void BSave_Pay_Click(object sender, EventArgs e)
        {
            example.Class.ComboBoxPayment Payment = (CBPayment_Pay.SelectedItem as example.Class.ComboBoxPayment);
            if (DGV_Pay.Rows.Count != 0)
            {
                DialogResult dialogResult = MessageBox.Show("ยืนยันการชำระ", "การเเจ้งเตือนการชำระ", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dialogResult == DialogResult.Yes)
                {
                    TBTeacherBill.Text = Class.SQLConnection.InputSQLMSSQL(SQLDefault[13]).Rows[0][0].ToString();
                    int Balance = Convert.ToInt32(LBalance_Pay.Text);
                    Freezing_Form(false);
                    example.Bank.Pay.Calculator calculator = new example.Bank.Pay.Calculator(Balance);
                    calculator.ShowDialog();
                    if (example.Bank.Pay.Calculator.Return)
                    {
                        DataSet dsbill = example.Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[2]
                                .Replace("{TeacherNo}", TBTeacherNo.Text)
                                .Replace("{TeacherNoaddby}", example.Class.UserInfo.TeacherNo)
                                .Replace("{Month}", CBMonthSelection_Pay.Text)
                                .Replace("{Year}", CBYearSelection_Pay.Text)
                                .Replace("{Payment}", Payment.No.ToString())
                                .Replace("--{addbill}", ""));
                        if (dsbill.Tables[0].Rows.Count != 0)
                        {
                            for (int x = 0; x < DGV_Pay.Rows.Count; x++)
                            {

                                if (DGV_Pay.Rows[x].Cells[1].Value.ToString().Contains("สะสม"))
                                {
                                    try
                                    {
                                        example.Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[2]
                                        .Replace("{TeacherNo}", TBTeacherNo.Text)
                                        .Replace("{TeacherNoaddby}", example.Class.UserInfo.TeacherNo)
                                        .Replace("{Month}", CBMonthSelection_Pay.Text)
                                        .Replace("{Year}", CBYearSelection_Pay.Text)
                                        .Replace("{Payment}", Payment.No.ToString())
                                        .Replace("--{haveSaving}", "")
                                        .Replace("{AmountPaySaving}", DGV_Pay.Rows[x].Cells[2].Value.ToString())
                                        .Replace("{BillNo}", dsbill.Tables[0].Rows[0][0].ToString()));
                                    }
                                    catch
                                    {
                                        MessageBox.Show("ชำระเงินล้มเหลว", "แจ้งเตือนการขำระ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }

                                }
                                else if (DGV_Pay.Rows[x].Cells[1].Value.ToString().Contains("กู้"))
                                {
                                    DataTable dtGuarantor = example.Class.SQLConnection.InputSQLMSSQL(SQLDefault[3]
                                    .Replace("{LoanID}", DGV_Pay.Rows[x].Cells[3].Value.ToString()));
                                    if (dtGuarantor.Rows.Count != 0)
                                    {
                                        try
                                        {
                                            if(dtGuarantor.Rows.Count == 1)
                                            {
                                                DataSet dsCheckMonth = example.Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[2]
                                                    .Replace("{TeacherNo}", TBTeacherNo.Text)
                                                    .Replace("{TeacherNoaddby}", example.Class.UserInfo.TeacherNo)
                                                    .Replace("{Month}", CBMonthSelection_Pay.Text)
                                                    .Replace("{Year}", CBYearSelection_Pay.Text)
                                                    .Replace("{Payment}", Payment.No.ToString())
                                                    .Replace("--{haveLoan}", "")
                                                    .Replace("{AmountPayLoan}", DGV_Pay.Rows[x].Cells[2].Value.ToString())
                                                    .Replace("{BillNo}", dsbill.Tables[0].Rows[0][0].ToString())
                                                    .Replace("{LoanNo}", DGV_Pay.Rows[x].Cells[3].Value.ToString())
                                                    .Replace("{TeacherGuaNo1}", dtGuarantor.Rows[0][0].ToString())
                                                    .Replace("{TeacherGuaNo2}", dtGuarantor.Rows[1][0].ToString())
                                                    .Replace("--{haveLoan1}", ""));
                                                if (dsCheckMonth.Tables[0].Rows.Count == Convert.ToInt32(dsCheckMonth.Tables[0].Rows[0][1].ToString()))
                                                {
                                                    example.Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[2]
                                                        .Replace("--{Close}", "")
                                                        .Replace("{LoanNo}", DGV_Pay.Rows[x].Cells[3].Value.ToString())
                                                        .Replace("{TeacherGuaNo1}", dtGuarantor.Rows[0][0].ToString()));
                                                }
                                            }
                                            else if (dtGuarantor.Rows.Count == 2)
                                            {
                                                DataSet dsCheckMonth = example.Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[2]
                                                .Replace("{TeacherNo}", TBTeacherNo.Text)
                                                .Replace("{TeacherNoaddby}", example.Class.UserInfo.TeacherNo)
                                                .Replace("{Month}", CBMonthSelection_Pay.Text)
                                                .Replace("{Year}", CBYearSelection_Pay.Text)
                                                .Replace("{Payment}", Payment.No.ToString())
                                                .Replace("--{haveLoan}", "")
                                                .Replace("{AmountPayLoan}", DGV_Pay.Rows[x].Cells[2].Value.ToString())
                                                .Replace("{BillNo}", dsbill.Tables[0].Rows[0][0].ToString())
                                                .Replace("{LoanNo}", DGV_Pay.Rows[x].Cells[3].Value.ToString())
                                                .Replace("{TeacherGuaNo1}", dtGuarantor.Rows[0][0].ToString())
                                                .Replace("{TeacherGuaNo2}", dtGuarantor.Rows[1][0].ToString())
                                                .Replace("--{haveLoan1}", "")
                                                .Replace("--{haveLoan2}", ""));
                                                if (dsCheckMonth.Tables[0].Rows.Count == Convert.ToInt32(dsCheckMonth.Tables[0].Rows[0][1].ToString()))
                                                {
                                                    example.Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[2]
                                                        .Replace("--{Close}", "")
                                                        .Replace("{LoanNo}", DGV_Pay.Rows[x].Cells[3].Value.ToString())
                                                        .Replace("{TeacherGuaNo1}", dtGuarantor.Rows[0][0].ToString())
                                                        .Replace("{TeacherGuaNo2}", dtGuarantor.Rows[1][0].ToString()));
                                                }
                                            }
                                            else if (dtGuarantor.Rows.Count == 3)
                                            {
                                                DataSet dsCheckMonth = example.Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[2]
                                                    .Replace("{TeacherNo}", TBTeacherNo.Text)
                                                    .Replace("{TeacherNoaddby}", example.Class.UserInfo.TeacherNo)
                                                    .Replace("{Month}", CBMonthSelection_Pay.Text)
                                                    .Replace("{Year}", CBYearSelection_Pay.Text)
                                                    .Replace("{Payment}", Payment.No.ToString())
                                                    .Replace("--{haveLoan}", "")
                                                    .Replace("{AmountPayLoan}", DGV_Pay.Rows[x].Cells[2].Value.ToString())
                                                    .Replace("{BillNo}", dsbill.Tables[0].Rows[0][0].ToString())
                                                    .Replace("{LoanNo}", DGV_Pay.Rows[x].Cells[3].Value.ToString())
                                                    .Replace("{TeacherGuaNo1}", dtGuarantor.Rows[0][0].ToString())
                                                    .Replace("{TeacherGuaNo2}", dtGuarantor.Rows[1][0].ToString())
                                                    .Replace("{TeacherGuaNo3}", dtGuarantor.Rows[2][0].ToString())
                                                    .Replace("--{haveLoan1}", "")
                                                    .Replace("--{haveLoan2}", "")
                                                    .Replace("--{haveLoan3}", ""));
                                                if (dsCheckMonth.Tables[0].Rows.Count == Convert.ToInt32(dsCheckMonth.Tables[0].Rows[0][1].ToString()))
                                                {
                                                    example.Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[2]
                                                        .Replace("--{Close}", "")
                                                        .Replace("{LoanNo}", DGV_Pay.Rows[x].Cells[3].Value.ToString())
                                                        .Replace("{TeacherGuaNo1}", dtGuarantor.Rows[0][0].ToString())
                                                        .Replace("{TeacherGuaNo2}", dtGuarantor.Rows[1][0].ToString())
                                                        .Replace("{TeacherGuaNo3}", dtGuarantor.Rows[2][0].ToString()));
                                                }
                                            }
                                            else if(dtGuarantor.Rows.Count == 4)
                                            {
                                                DataSet dsCheckMonth = example.Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[2]
                                                .Replace("{TeacherNo}", TBTeacherNo.Text)
                                                .Replace("{TeacherNoaddby}", example.Class.UserInfo.TeacherNo)
                                                .Replace("{Month}", CBMonthSelection_Pay.Text)
                                                .Replace("{Year}", CBYearSelection_Pay.Text)
                                                .Replace("{Payment}", Payment.No.ToString())
                                                .Replace("--{haveLoan}", "")
                                                .Replace("{AmountPayLoan}", DGV_Pay.Rows[x].Cells[2].Value.ToString())
                                                .Replace("{BillNo}", dsbill.Tables[0].Rows[0][0].ToString())
                                                .Replace("{LoanNo}", DGV_Pay.Rows[x].Cells[3].Value.ToString())
                                                .Replace("{TeacherGuaNo1}", dtGuarantor.Rows[0][0].ToString())
                                                .Replace("{TeacherGuaNo2}", dtGuarantor.Rows[1][0].ToString())
                                                .Replace("{TeacherGuaNo3}", dtGuarantor.Rows[2][0].ToString())
                                                .Replace("{TeacherGuaNo4}", dtGuarantor.Rows[3][0].ToString())
                                                .Replace("--{haveLoan1}", "")
                                                .Replace("--{haveLoan2}", "")
                                                .Replace("--{haveLoan3}", "")
                                                .Replace("--{haveLoan4}", ""));
                                                if (dsCheckMonth.Tables[0].Rows.Count == Convert.ToInt32(dsCheckMonth.Tables[0].Rows[0][1].ToString()))
                                                {
                                                    example.Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[2]
                                                        .Replace("--{Close}", "")
                                                        .Replace("{LoanNo}", DGV_Pay.Rows[x].Cells[3].Value.ToString())
                                                        .Replace("{TeacherGuaNo1}", dtGuarantor.Rows[0][0].ToString())
                                                        .Replace("{TeacherGuaNo2}", dtGuarantor.Rows[1][0].ToString())
                                                        .Replace("{TeacherGuaNo3}", dtGuarantor.Rows[2][0].ToString())
                                                        .Replace("{TeacherGuaNo4}", dtGuarantor.Rows[3][0].ToString()));
                                                }
                                            }
                                        }
                                        catch ( Exception ex)
                                        {
                                            Console.WriteLine(ex);
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

                        MessageBox.Show("ชำระสำเร็จ", "แจ้งเตือนการขำระ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        TBTeacherNo_KeyDown(sender, new KeyEventArgs(Keys.Enter));
                        sum = 0;
                    }
                    else if (!(example.Bank.Pay.Calculator.Return))
                    {
                        Freezing_Form(true);
                        MessageBox.Show("การชำระล้มเหลว", "การเเจ้งเตือนการชำระ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        }

        //Mouse Click datagridview
        private void DGV_Pay_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int currentMouseOverRow = DGV_Pay.HitTest(e.X, e.Y).RowIndex;
                if (currentMouseOverRow != -1)
                {
                    SelectIndexRow = currentMouseOverRow;
                    ContextMenu m = new ContextMenu();
                    m.MenuItems.Add(new MenuItem("ลบออก"));
                    m.Show(DGV_Pay, new Point(e.X, e.Y));
                    m.MenuItems[0].Click += new System.EventHandler(this.Delete_Click);
                }
            }
        }
        //==============================================================================================


        //============================== tabpage 2 (Shareinfo) ============================================
        //Select Year
        private void CBYearSelection_ShareInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DGV_ShareInfo.Rows.Clear();
            if (CBYearSelection_ShareInfo.Text != "")
            {
                int Month = 1;
                DataSet ds = example.Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[11]
                .Replace("{TeacherNo}", TBTeacherNo.Text)
                .Replace("{Year}", CBYearSelection_ShareInfo.Text));
                TBToatalSaving_ShareInfo.Text = ds.Tables[1].Rows[0][1].ToString();
                DateTime Date;
                int ShareOfYear = 0;
                if (ds.Tables[1].Rows.Count != 0)
                {
                    Date = DateTime.Parse(ds.Tables[1].Rows[0][2].ToString());
                    //int Mont = int.Parse(Date.ToString("MM"));

                    if (int.Parse(CBYearSelection_ShareInfo.SelectedItem.ToString()) == int.Parse(Date.ToString("yyyy")))
                    {
                        Month = int.Parse(Date.ToString("MM"));
                    }
                }
                for (int a = Month; a <= 12; a++)
                {

                    DGV_ShareInfo.Rows.Add(a + "/" + CBYearSelection_ShareInfo.SelectedItem.ToString(), ds.Tables[1].Rows[0][0].ToString(), "ยังไม่ได้ชำระ");
                    DGV_ShareInfo.Rows[a - Month].Cells[2].Style = new DataGridViewCellStyle { ForeColor = Color.Red };
                    bool Check = true;
                    for (int x = 0; x < ds.Tables[0].Rows.Count; x++)
                    {
                        if (Convert.ToInt32(ds.Tables[0].Rows[x][1].ToString()) == a)
                        {
                            DGV_ShareInfo.Rows.RemoveAt(a - Month);
                            DGV_ShareInfo.Rows.Add(a + "/" + CBYearSelection_ShareInfo.SelectedItem.ToString(), ds.Tables[0].Rows[x][0].ToString(), "ชำระแล้ว");
                            DGV_ShareInfo.Rows[a - Month].Cells[2].Style = new DataGridViewCellStyle { ForeColor = Color.Green };
                            ShareOfYear += Convert.ToInt32(ds.Tables[0].Rows[x][0].ToString());
                        }
                        if (!Check)
                            break;
                    }
                }
                TBSavingOnYear_ShareInfo.Text = ShareOfYear.ToString();
            }

        }
        //==============================================================================================


        //============================== tabpage 3 (Loaninfo) ============================================
        //Select Loan
        private void CBLoanSelection_LoanInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            example.Class.ComboBoxPayment Loan = (CBLoanSelection_LoanInfo.SelectedItem as example.Class.ComboBoxPayment);
            if (CBLoanSelection_LoanInfo.SelectedIndex != -1)
            {
                DataSet ds = example.Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[10].Replace("{LoanID}", Loan.No));
                DGV_LoanInfo.Rows.Clear();
                if (ds.Tables[0].Rows.Count != 0)
                {
                    int RemainAmount = 0;
                    for (int x = 0; x < ds.Tables[0].Rows.Count; x++)
                    {
                        RemainAmount += Convert.ToInt32(Convert.ToDouble(ds.Tables[0].Rows[x][9].ToString()));
                    }

                    TBTotal__LoanInfo.Text = (Convert.ToDouble(Convert.ToDouble(ds.Tables[0].Rows[0][8].ToString()) + (Convert.ToDouble(ds.Tables[0].Rows[0][8].ToString()) * Convert.ToDouble(ds.Tables[0].Rows[0][7].ToString()) / 100)).ToString());
                    TBAmountRemain_LoanInfo.Text = RemainAmount.ToString();
                    TBInteresrt_LoanInfo.Text = Convert.ToInt32(Convert.ToDouble(ds.Tables[0].Rows[0][7].ToString()) / 100 * Convert.ToDouble(ds.Tables[0].Rows[0][8].ToString())).ToString();
                    TBStartAmount_LoanInfo.Text = Convert.ToInt32(ds.Tables[0].Rows[0][8].ToString()).ToString();
                    TBInstallment_LoanInfo.Text = ds.Tables[0].Rows[0][6].ToString();

                    int Month = Convert.ToInt32(ds.Tables[0].Rows[0][4].ToString());
                    int Year = Convert.ToInt32(ds.Tables[0].Rows[0][5].ToString());

                    Double Interest = Convert.ToDouble(Convert.ToDouble(ds.Tables[0].Rows[0][8].ToString())) * (Convert.ToDouble(ds.Tables[0].Rows[0][7].ToString()) / 100) / Convert.ToDouble(ds.Tables[0].Rows[0][6].ToString());

                    int Pay = Convert.ToInt32(Convert.ToInt32(ds.Tables[0].Rows[0][8].ToString()) / Convert.ToInt32(ds.Tables[0].Rows[0][6].ToString()));
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
                            if(ds.Tables[1].Rows.Count == 0)
                            {
                                StatusPay = "ยังไม่จ่าย";
                            }

                        }
                        catch
                        {
                            StatusPay = "ยังไม่จ่าย";
                        }

                        DGV_LoanInfo.Rows.Add($"{Month}/{Year}", Pay, Convert.ToInt32(Interest), SumInstallment, StatusPay);
                        if (StatusPay == "ยังไม่จ่าย")
                        {
                            DGV_LoanInfo.Rows[Num].Cells[4].Style = new DataGridViewCellStyle { ForeColor = Color.Red };
                        }
                        else
                            DGV_LoanInfo.Rows[Num].Cells[4].Style = new DataGridViewCellStyle { ForeColor = Color.Green };
                        Month++;
                    }
                }
            }
        }
        //==============================================================================================


        //============================== tabpage 4 (Billinfo) ============================================
        //Select Year
        private void CBYearSelect_BillInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(CBYearSelection_Pay.SelectedIndex != -1)
            {
                DataTable dt = example.Class.SQLConnection.InputSQLMSSQL(SQLDefault[14]
                .Replace("{TeacherNo}", TBTeacherNo.Text)
                .Replace("{Year}", CBYearSelection_BillInfo.Text));
                if(dt.Rows.Count != 0)
                {
                    DGV_BillInfo.Rows.Clear();
                    int Sum = 0;
                    for(int x = 0; x < dt.Rows.Count; x++)
                    {
                        DGV_BillInfo.Rows.Add(dt.Rows[x][0].ToString(), dt.Rows[x][1].ToString(), dt.Rows[x][2].ToString(), dt.Rows[x][3].ToString(), dt.Rows[x][4].ToString(), Convert.ToDateTime(dt.Rows[x][5].ToString()).ToString("dd-MM-yyyy"));
                        Sum = Sum + Convert.ToInt32(dt.Rows[x][3].ToString());
                        LBalance_BillInfo.Text = Sum.ToString();

                        if (x % 2 == 1)
                        {
                            DGV_BillInfo.Rows[x].DefaultCellStyle.BackColor = Color.AliceBlue;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("ไม่พบรายการ","ระบบ",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }
            }
            
        }
        //==============================================================================================
        //--------------------------------------------------------------------------------------------------------------------------------------------------------------


        //======================================= Method ===============================================
        //DGV Delete Rows
        private void Delete_Click(object sender, EventArgs e)
        {
            if (DGV_Pay.Rows.Count != 0)
            {
                sum -= int.Parse(DGV_Pay.Rows[SelectIndexRow].Cells[2].Value.ToString());
                x = sum;
                LBalance_Pay.Text = sum.ToString();
            }
            if (SelectIndexRow != -1)
            {
                this.RestoreComboboxafterdelete();
                DGV_Pay.Rows.RemoveAt(SelectIndexRow);
                SelectIndexRow = -1;
                if (DGV_Pay.Rows.Count == 0)
                {
                    CBPayment_Pay.Enabled = false;
                    CBPayment_Pay.SelectedIndex = -1;
                    Auto = 0;
                }
            }
        }

        //DGV Restore Values after Delete
        private void RestoreComboboxafterdelete ()
        {
            if (DGV_Pay.Rows[SelectIndexRow].Cells[1].Value.ToString().Contains("กู้"))
            {
                ComboBox[] cb = new ComboBox[] { CBList_Pay };
                for (int x = 0; x < cb.Length; x++)
                    cb[x].Items.Add(new example.Class.ComboBoxPay(DGV_Pay.Rows[SelectIndexRow].Cells[1].Value.ToString(), DGV_Pay.Rows[SelectIndexRow].Cells[2].Value.ToString(),
                                             DGV_Pay.Rows[SelectIndexRow].Cells[3].Value.ToString()));
                CBList_Pay.SelectedIndex = 0;
            }
            
        }

        //Cleartabpage 1
        private void Cleartabpage1()
        {
            //tabpage 1 (Pay) ===================================================
            DGV_Pay.Rows.Clear();
            CBYearSelection_Pay.SelectedIndex = -1;
            CBYearSelection_Pay.Items.Clear();
            CBMonthSelection_Pay.SelectedIndex = -1;
            CBMonthSelection_Pay.Items.Clear();
            CBList_Pay.SelectedIndex = -1;
            TBAmount_Pay.Clear();
            LBalance_Pay.Text = sum.ToString();
            sum = 0;
            BAutoSelection.Enabled = false;
            //===================================================================
        }

        //Cleartabpage 2
        private void Cleartabpage2()
        {
            //tabpage 2 (ShareInfo) =============================================
            DGV_ShareInfo.Rows.Clear();
            CBYearSelection_ShareInfo.SelectedIndex = -1;
            CBYearSelection_ShareInfo.Items.Clear();
            TBToatalSaving_ShareInfo.Text = "";
            TBSavingOnYear_ShareInfo.Text = "";
            //===================================================================
        }

        //Cleartabpage 3
        private void Cleartabpage3()
        {
            //tabpage 3 (LoanInfo) ==============================================
            DGV_LoanInfo.Rows.Clear();
            CBLoanSelection_LoanInfo.SelectedIndex = -1;
            CBLoanSelection_LoanInfo.Items.Clear();
            TBTotal__LoanInfo.Text = "";
            TBAmountRemain_LoanInfo.Text = "";
            TBInteresrt_LoanInfo.Text = "";
            TBStartAmount_LoanInfo.Text = "";
            TBInstallment_LoanInfo.Text = "";
            //====================================================================
        }

        //Cleartabpage 4
        private void Cleartabpage4()
        {
            //tabpage 4 (BillInfo) ==============================================
            CBYearSelection_BillInfo.Items.Clear();
            DGV_BillInfo.Rows.Clear();
            LBalance_BillInfo.Text = "0";
            //====================================================================
        }

        //Clear all tab (all Form)
        private void ClearForm()
        {
            Cleartabpage1();
            Cleartabpage2();
            Cleartabpage3();
            Cleartabpage4();
        }

        //Form Enable = (true , flase)
        private void Freezing_Form(bool Status)
        {
            CBYearSelection_Pay.Enabled = Status;
            CBMonthSelection_Pay.Enabled = Status;
            CBList_Pay.Enabled = Status;
            CBPayment_Pay.Enabled = Status;
            TBAmount_Pay.Enabled = Status;
            BAutoSelection.Enabled = Status;
            BClearTab_Pay.Enabled = Status;
            BListAdd_Pay.Enabled = Status;
            DGV_Pay.Enabled = Status;
            tabControl1.Enabled = Status;
            TBTeacherNo.Enabled = Status;
            BSearchTeacher.Enabled = Status;
        }
        //===============================================================================================
    }
}
