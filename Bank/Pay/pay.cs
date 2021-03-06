 using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;

namespace BankTeacher.Bank.Pay
{
    public partial class pay : Form
    {
        bool CheckPay = false;
        int SelectIndexRow = -1;
        List<List<int>> DM = new List<List<int>>();
        List<List<int>> BackupDM = new List<List<int>>();
        List<int> YearinCB = new List<int>();
        String[] StartLoan = new String[] { "Year", "Month" };
        bool CheckSave = false;

        /// <summary> 
        /// SQLDafault 
        /// <para>[0] SELECT MEMBER INPUT: {Text} </para> 
        /// <para>[1] Payment Choice INPUT: </para>
        /// <para>[2] Check Loan and Register Date and lasted Bill INPUT: {TeacherNo} </para>
        /// <para>[3] Count the Bill Year INPUT: {TeacherNo} , {Year}</para>
        /// <para>[4] Check if you have paid ( Saving ) INPUT: {TeacherNo} , {Month} , {Year} , {Date} </para>
        /// <para>[5] Check if you have paid ( Loan ) INPUT: {LoanNo} , {Month} , {Year} , {Date} </para>
        /// <para>[6] Check Loan INPUT: {TeacherNo} </para>
        /// <para>[7] BPaySave Insert Bill INPUT: {TeacherNo} , {TeacherNoaddby} , {Date}</para>
        /// <para>[8]Insert BillDetails Use ForLoop INPUT: {BillNo},{TypeNo},{LoanNo},{Amount},{Month},{Year}</para>
        /// <para>[9]Update Guarantor and Loan (BSavePayLoop) INPUT: {LoanAmount}  {LoanNo} {TeacharNo} {Amount} </para>
        /// <para>[10]Update Saving+ (BSavePayLoop) INPUT: {TeacherNo}  {SavingAmount}</para>
        /// <para>[11] SELECT lasted billno INPUT:</para>
        /// <para>[12] SELECT SharePayBill and SELECT ShareOfYear INPUT: {TeacherNo} , {Year}</para>
        /// <para>[13] SELECT Detail Loan INPUT: {LoanID}</para>
        /// <para>[14] Select Billinfomation INPUT: {TeacherNo , {Year}</para>
        /// <para>[15] print backwards IN: {billl} </para>
        /// <para>[16] print data IN: {BillNo} </para>
        /// <para>[17] SELECT MEMBER (Enter) INPUT: {Text} </para>
        /// <para>[18] Search Member and SavingAmount - RemainAmount in Guarantor INPUT: {TeacherNoNotLike} {Text} </para>
        /// <para>[19] Get YearBillInfo Back 5 year INPUT: {TeacherNo} </para>
        /// <para>[20] Find a Loan list as a Teacher Guarantor INPUT: {TeacherNo}</para>
        /// <para>[21] Select DetailLoan INPUT: {LoanNo} </para>
        /// <para>[22] Insert Bill Loanlist INPUT: {TeacherNoPay} {TeacherNoAddBy} {LoanNo} {Date}</para>
        /// </summary> 
        private String[] SQLDefault = new String[]
        { 
          //[0] SELECT MEMBER INPUT: {Text} 
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
          //[1] Payment Choice INPUT: 
           "SELECT Convert(nvarchar(50) , Name) , BillDetailpaymentNo  \r\n " +
          "FROM EmployeeBank.dbo.tblBillDetailPayment \r\n " +
          "WHERE Status = 1"
           ,
           //[2] Check Loan and Register Date and lasted Bill INPUT: {TeacherNo} 
           "SELECT LoanNo , CAST(CAST(YearPay as nvarchar(4))+'/'+CAST(MonthPay as nvarchar(2)) as nvarchar(10))\r\n " +
          "FROM EmployeeBank.dbo.tblLoan  \r\n " +
          "WHERE TeacherNo = '{TeacherNo}' and LoanStatusNo = 2 ; \r\n " +
          " \r\n " +
          "SELECT CAST(DateAdd as date) \r\n " +
          "FROM EmployeeBank.dbo.tblMember \r\n " +
          "WHERE TeacherNo = '{TeacherNo}' and MemberStatusNo != 2;  \r\n " +
          " \r\n " +
          "SELECT TOP(1)EOMONTH(CAST(CAST(Year as nvarchar)+'-'+ CAST(Mount as nvarchar) +'-10' as nvarchar))as MaxDate , b.BillNo ,DATEADD(MONTH,5,CAST(CAST(CAST(Year as nvarchar) +'/' + CAST(Mount AS nvarchar) + '/05' AS nvarchar) AS date))  \r\n " +
          "FROM EmployeeBank.dbo.tblBill as a \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblBillDetail as b on a.BillNo = b.BillNo \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblMember as c on a.TeacherNo = c.TeacherNo\r\n"+
          "WHERE a.TeacherNo = '{TeacherNo}' and a.Cancel = 1 and c.DateAdd <= a.TransactionDate \r\n " +
          "ORDER BY Maxdate DESC; "
           ,
           //[3] Count the Bill Year INPUT: {TeacherNo} , {Year}
           "SELECT b.BillNo \r\n " +
          "FROM EmployeeBank.dbo.tblBillDetail as a \r\n " +
          "RIGHT JOIN EmployeeBank.dbo.tblBill as b on b.BillNo = a.BillNo \r\n " +
          "WHERE TeacherNo = '{TeacherNo}' and Year = {Year} and b.Cancel = 1"
           ,
           //[4] Check if you have paid ( Saving ) INPUT: {TeacherNo} , {Month} , {Year} , {Date} 
           "SELECT TOP(1)a.TeacherNo, StartAmount ,CAST('หุ้นสะสม' as nvarchar(30))    \r\n " +
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
          "WHERE bb.Mount = '{Month}' and bb.Year = '{Year}' and (bb.TypeNo = 1 or bb.TypeNo = 3) and MemberStatusNo = 1 and DATEADD(YYYY,0,'{Date}') >= a.DateAdd  and aa.Cancel = 1 and aa.TransactionDate >= a.DateAdd)   \r\n " +
          "and a.TeacherNo = '{TeacherNo}' and (c.TypeNo = 1 or c.TypeNo = 3) and MemberStatusNo = 1 and b.Cancel = 1   and DATEADD(YYYY,0,'{Date}') >=  a.DateAdd \r\n " +
          "GROUP BY a.TeacherNo,f.TypeName, StartAmount   ;  "
           ,
           //[5] Check if you have paid ( Loan ) INPUT: {LoanNo} , {Month} , {Year} , {Date} 
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
           //[6] Check Loan INPUT: {TeacherNo} 
           "SELECT LoanNo \r\n " +
          "FROM EmployeeBank.dbo.tblLoan  \r\n " +
          "WHERE TeacherNo = '{TeacherNo}' and LoanStatusNo = 2 ;"
           ,
           //[7] BPaySave Insert Bill INPUT: {TeacherNo} , {TeacherNoaddby}  , {Date}
          "DECLARE @BIllNO INT;  \r\n " +
          "           \r\n " +
          "INSERT INTO EmployeeBank.dbo.tblBill (TeacherNoAddBy,TeacherNo,TeacherNoPay,DateAdd,Cancel,TransactionDate)  \r\n " +
          "VALUES('{TeacherNoAddBy}','{TeacherNo}','{TeacherNo}','{Date}',1,CURRENT_TIMESTAMP);  \r\n " +
          "SET @BIllNO = SCOPE_IDENTITY();  \r\n " +
          "SELECT @BIllNO; \r\n"
           ,
          //[8]Insert BillDetails Use ForLoop INPUT: {BillNo},{TypeNo},{LoanNo},{Amount},{Month},{Year},{BillDetailPaymentNo}
           "INSERT INTO EmployeeBank.dbo.tblBillDetail (BillNo,TypeNo,LoanNo,Amount,Mount,Year,BillDetailPaymentNo)  \r\n " +
          "VALUES ({BillNo},{TypeNo},{LoanNo},{Amount},{Month},{Year},{BillDetailPaymentNo});\r\n"
           ,
           //[9]Update Guarantor and Loan (BSavePayLoop) INPUT: {LoanAmount}  {LoanNo} {TeacharNo} {Amount}
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
          "END \r\n"
           ,
           //[10]Update Saving+ (BSavePayLoop) INPUT: {TeacherNo}  {SavingAmount}
           "-- บวก หุ้นสะสม \r\n " +
          "UPDATE EmployeeBank.dbo.tblShare \r\n " +
          "SET SavingAmount = {SavingAmount} + SavingAmount \r\n " +
          "WHERE TeacherNo = '{TeacherNo}';"
           ,
           //[11] SELECT lasted billno INPUT: 
          "SELECT IDENT_CURRENT('EmployeeBank.dbo.tblBill')+1 "
             ,
          //[12] SELECT SharePayBill and SELECT ShareOfYear INPUT: {TeacherNo} , {Year}
          "SELECT SUM(d.Amount) , d.Mount , d.Year , a.StartAmount\r\n" +
          "FROM EmployeeBank.dbo.tblMember as a\r\n" +
          "LEFT JOIN EmployeeBank.dbo.tblShare as b on a.TeacherNo = b.TeacherNo\r\n" +
          "LEFT JOIN EmployeeBank.dbo.tblBill as c on a.TeacherNo = c.TeacherNo\r\n" +
          "LEFT JOIN EmployeeBank.dbo.tblBillDetail as d on c.BillNo = d.BillNo\r\n" +
          "WHERE c.Cancel = 1 and (d.TypeNo = 1 or d.TypeNo = 3) and d.Mount <= 12 and d.Year = {Year} and a.TeacherNo LIKE '{TeacherNo}'  and a.DateAdd <= c.TransactionDate \r\n" +
          "GROUP BY a.TeacherNo , d.Amount , d.Mount , d.Year , a.StartAmount , CAST(a.DateAdd AS Date) , b.SavingAmount;\r\n" +
          "\r\n" +
             "DECLARE @Loan int = 0 \r\n" +
             "set @Loan = (select IIF(Sum(a.RemainsAmount) != 0,Sum(a.RemainsAmount),0) From EmployeeBank.dbo.tblGuarantor as a \r\n" +
             "where a.TeacherNo = '{TeacherNo}' ) \r\n" +

          "SELECT a.StartAmount , b.SavingAmount-@Loan, CAST(a.DateAdd as date)\r\n" +
          "FROM EmployeeBank.dbo.tblMember as a\r\n" +
          "LEFT JOIN EmployeeBank.dbo.tblShare as b on a.TeacherNo = b.TeacherNo\r\n" +
          "WHERE a.TeacherNo LIKE '{TeacherNo}'\r\n"
          ,
          //[13] SELECT Detail Loan INPUT: {LoanID}
          "SELECT b.TeacherNo , CAST(d.PrefixName + ' ' + c.Fname + ' ' + c.Lname AS NVARCHAR) AS NameTeacher,CAST(DateAdd as date), \r\n " +
          "a.PayDate,MonthPay,YearPay,PayNo,InterestRate,LoanAmount,ROUND(b.RemainsAmount , 0),a.LoanStatusNo  \r\n " +
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
          "WHERE a.LoanNo = '{LoanID}' and TypeNo = '2' and Cancel = 1; "

          ,
          //[14] Select Billinfomation INPUT: {TeacherNo , {Year}
          "SELECT a.BillNo ,CAST(CAST(b.Mount as nvarchar(2))+'/'+CAST(b.Year as nvarchar(4)) as nvarchar(10))  , TypeName , Amount ,CAST(d.Name as nvarchar), DateAdd \r\n " +
          "FROM EmployeeBank.dbo.tblBill as a \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblBillDetail as b on a.BillNo = b.BillNo \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblBillDetailType as c on b.TypeNo = c.TypeNo \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblBillDetailPayment as d on b.BillDetailPaymentNo = d.BillDetailPaymentNo \r\n " +
          "WHERE TeacherNo = '{TeacherNo}' and CAST(CAST(a.DateAdd as Date) as nvarchar) LIKE '%{Year}%' and a.Cancel = 1\r\n " +
          "ORDER BY a.BillNo"
          ,
           //[15] print backwards IN: {billl}
          "SELECT  a.BillNo,d.TeacherNo,CAST(e.PrefixName+' '+d.Fname+' '+d.Lname as nvarchar),a.Amount,b.TypeName,CAST(a.Mount as nvarchar)+'/'+CAST(a.Year as nvarchar)  as  Mountandyear,CAST(f.Name as nvarchar) \r\n " +
          "FROM EmployeeBank.dbo.tblBillDetail as a \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblBillDetailType as b ON a.TypeNo = b.TypeNo \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblBill  as c ON a.BillNo = c.BillNo \r\n " +
          "LEFT JOIN Personal.dbo.tblTeacherHis as d ON c.TeacherNoAddBy = d.TeacherNo \r\n " +
          "LEFT JOIN BaseData.dbo.tblPrefix as e ON d.PrefixNo = e.PrefixNo \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblBillDetailPayment f ON a.BillDetailPaymentNo = f.BillDetailPaymentNo \r\n " +
           "WHERE a.BillNo = {bill}"
           ,
           //[16] print data IN: {BillNo}
           "SELECT a.DateAdd,CAST(YEAR(a.DateAdd) as nvarchar)+'/'+CAST(MONTH(a.DateAdd) as nvarchar)+'/'+CAST(DAY(a.DateAdd) as nvarchar) \r\n" +
           "FROM EmployeeBank.dbo.tblBill as a \r\n" +
           "WHERE a.BillNo = {Bill}"
           ,
           
          //[17] SELECT MEMBER (Enter) INPUT: {Text} 
          "SELECT TOP(20) a.TeacherNo , CAST(ISNULL(c.PrefixName+' ','')+[Fname] +' '+ [Lname] as NVARCHAR)AS Name, e.SavingAmount,    \r\n " +
          "b.TeacherLicenseNo,b.IdNo AS IDNo,b.TelMobile ,a.StartAmount,CAST(d.MemberStatusName as nvarchar) AS UserStatususing    \r\n " +
          "FROM EmployeeBank.dbo.tblMember as a    \r\n " +
          "LEFT JOIN Personal.dbo.tblTeacherHis as b ON a.TeacherNo = b.TeacherNo    \r\n " +
          "LEFT JOIN BaseData.dbo.tblPrefix as c ON c.PrefixNo = b.PrefixNo   \r\n " +
          "INNER JOIN EmployeeBank.dbo.tblMemberStatus as d on a.MemberStatusNo = d.MemberStatusNo  \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblShare as e on a.TeacherNo = e.TeacherNo \r\n " +
          "WHERE a.MemberStatusNo = 1 and a.TeacherNo = '{Text}'\r\n " +
          "GROUP BY a.TeacherNo , CAST(ISNULL(c.PrefixName+' ','')+[Fname] +' '+ [Lname] as NVARCHAR), e.SavingAmount,    \r\n " +
          "b.TeacherLicenseNo,b.IdNo ,b.TelMobile ,a.StartAmount,CAST(d.MemberStatusName as nvarchar)   \r\n " +
          "ORDER BY a.TeacherNo; "

             ,

          //[18] Search Member and SavingAmount - RemainAmount in Guarantor INPUT: {TeacherNoNotLike}  {Text}
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
          "WHERE (a.TeacherNo LIKE '%{Text}%' or CAST(ISNULL(c.PrefixName,'')+' '+[Fname] +' '+ [Lname] as NVARCHAR) LIKE '%{Text}%') and a.MemberStatusNo = 1    \r\n " +
          "GROUP BY a.TeacherNo , CAST(ISNULL(c.PrefixName,'')+' '+Fname +' '+ Lname as NVARCHAR), e.SavingAmount, Fname ) as a     \r\n " +
          "WHERE RemainAmount IS NOT NULL {TeacherNoNotLike} \r\n " +
          "GROUP BY TeacherNo, Name, RemainAmount ,a.Fname  \r\n " +
          "ORDER BY a.Fname; "
           ,

           //[19] Get YearBillInfo Back 5 year INPUT: {TeacherNo}
           "SELECT TOP 5 YEAR(a.DateAdd) \r\n " +
          "FROM EmployeeBank.dbo.tblBill as a \r\n " +
          "WHERE a.TeacherNo LIKE '{TeacherNo}' and a.Cancel != 2 \r\n " +
          "GROUP BY YEAR(a.DateAdd) \r\n " +
          "ORDER BY YEAR(a.DateAdd) DESC"
           ,

        };
        //=================================== Load Form ============================================
        public pay()
        {
            InitializeComponent();
        }
        //ChangeSizeForm
        private void Menuf_SizeChanged(object sender, EventArgs e)
        {
            Class.FromSettingMedtod.ChangeSizePanal(this,panel1);
        }
        private void pay_Load(object sender, EventArgs e)
        {
            ComboBox[] cb = new ComboBox[] { CBPayment_Pay };
            // Pull List Payment
            DataTable dtPayment = Class.SQLConnection.InputSQLMSSQL(SQLDefault[1]);
            //ยัดรูปแบบการโอนเงินใส่ใน CBPayment และเก็บค่า id ของรูปแบบนั้นๆลงไปที่ Class Combobox
            for (int a = 0; a < dtPayment.Rows.Count; a++)
                for (int x = 0; x < cb.Length; x++)
                    cb[x].Items.Add(new BankTeacher.Class.ComboBoxPayment(dtPayment.Rows[a][0].ToString(),
                        dtPayment.Rows[a][1].ToString()));
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
            Bank.Search IN = new Bank.Search(SQLDefault[18]
                .Replace("{TeacherNoNotLike}", $"and a.TeacherNo NOT LIKE '{TBTeacherNo.Text}'"), "หุ้นสะสม");
            IN.ShowDialog();
            //ถ้า ID สมาชิกที่เลือกไม่เป็นว่างเปล่า ให้ ใส่ลงใน TBTeacherNo และ ไปทำ event Keydown ของ TBTeacherNo
            if (Bank.Search.Return[0] != "")
            {
                TBTeacherNo.Text = Bank.Search.Return[0];
                TBTeacherName.Text = Bank.Search.Return[1];
                TBTeacherNo_KeyDown(sender, new KeyEventArgs(Keys.Enter));
            }
        }
        //WriteIDTeacher
        private void TBTeacherNo_KeyDown(object sender, KeyEventArgs e)
        {
            //หากมีการกด Enter
            if (e.KeyCode == Keys.Enter)
            {
                TBTeacherNo.Text = TBTeacherNo.Text.Replace("t", "T");
                CB_SelectPrint.SelectedIndex = 0;
                tabControl1.SelectedIndex = 0;
                List<int> Loan = new List<int>();
                List<List<int>> RMonth = new List<List<int>>();
                //ลองค้นหาดูว่ามี ID สมาชิกนี้ในระบบมั้ย หากมีให้ทำใน if
                DataTable dt = Class.SQLConnection.InputSQLMSSQL(SQLDefault[17].Replace("{Text}", TBTeacherNo.Text));
                if (dt.Rows.Count != 0)
                {
                    CheckPay = false;
                    tabControl1.Enabled = true;
                    Cleartabpage1();
                    TBTeacherBill.Text = "";
                    DM.Clear();
                    BackupDM.Clear();
                    ClearForm();
                    YearinCB.Clear();
                    TBTeacherName.Text = dt.Rows[0][1].ToString();
                    ComboBox[] cb = new ComboBox[] { CBLoanSelection_LoanInfo };
                    //หารายการกู้ที่มีอยู๋ เวลาที่สมัครสมาชิก และ บิลล์ที่จ่ายล่าสุด
                    DataSet ds = Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[2].Replace("{TeacherNo}", TBTeacherNo.Text));
                    //loop จากจำนวนรายการกู้ที่มีอยู่ในระบบ ใส่ลงใน CBLoanSelection หน้าข้อมูลกู้
                    for (int x = 0; x < ds.Tables[0].Rows.Count; x++)
                    {
                        for (int loop = 0; loop < cb.Length; loop++)
                        {
                            cb[loop].Items.Add(new BankTeacher.Class.ComboBoxPayment($"รายการกู้ : ({ds.Tables[0].Rows[x][0].ToString()}) " + ds.Tables[0].Rows[x][1].ToString(), ds.Tables[0].Rows[x][0].ToString()));
                            Loan.Add(Convert.ToInt32(ds.Tables[0].Rows[x][0].ToString()));
                        }
                    }
                    CBLoanSelection_LoanInfo.Enabled = true;
                    //หาก CBLoanSelection หน้าดูข้อมูลกู้มี Items อยู่อันเดียวให้เลือก อัตโนมัติให้เลย
                    if (CBLoanSelection_LoanInfo.Items.Count >= 1)
                    {
                        CBLoanSelection_LoanInfo.SelectedIndex = 0;
                    }
                    else
                    {
                        CBLoanSelection_LoanInfo.Enabled = false;
                    }
                    //ประกาศตัวแปร ปีที่สมัครและปีที่จ่ายล่าสุด
                    int YearRegister = Convert.ToInt32((Convert.ToDateTime(ds.Tables[1].Rows[0][0].ToString())).ToString("yyyy"));
                    int Yearlastofpay = Convert.ToInt32((Convert.ToDateTime(ds.Tables[2].Rows[0][2].ToString())).ToString("yyyy"));
                    Yearlastofpay = Yearlastofpay - Convert.ToInt32(Bank.Menu.Date[0]);
                    //ถ้าปีที่สมัคร น้อยกว่าปีนี้ -2 ปีให้ทำ
                    if (YearRegister < Convert.ToInt32(BankTeacher.Bank.Menu.Date[0]) - 5)
                    {
                        //ประกาศให้ตัวแปร thisyeardiscount2year = ปีนี้ - 2 ปี
                        int thisyeardiscount2year = Convert.ToInt32(BankTeacher.Bank.Menu.Date[0]) - 5;
                        //หาก thisyeardiscount2year น้อยกว่าหรือเท่ากับ ปีนี้ + จำนวนปีที่จ่ายล่าสุด ให้ loop
                        while (thisyeardiscount2year <= Convert.ToInt32(BankTeacher.Bank.Menu.Date[0]) + Yearlastofpay)
                        {
                            //เพิ่ม thisyeardiscount2year ลงไปใน Combobox ปี หน้า จ่าย ข้อมูลหุ้น และ ข้อมูลบิลล์
                            CBYearSelection_Pay.Items.Add(thisyeardiscount2year);
                            CBYearSelection_ShareInfo.Items.Add(thisyeardiscount2year);
                            CBYearSelection_BillInfo.Items.Add(thisyeardiscount2year);
                            //ถ้า thisyeardiscount2year เท่ากับ ปีนี้ + จำนวนปีที่จ่ายล่าสุด
                            if (thisyeardiscount2year == Convert.ToInt32(BankTeacher.Bank.Menu.Date[0]) + Yearlastofpay)
                            {
                                //หาบิลล์ในปีนั้นๆหากไม่มีให้ลบปี ใน Combobox ปี หน้า ข้อมูลบิลล์
                                DataTable dtCheckMonthinlastyear = BankTeacher.Class.SQLConnection.InputSQLMSSQL(SQLDefault[3]
                                    .Replace("{TeacherNo}", TBTeacherNo.Text)
                                    .Replace("{Year}", CBYearSelection_BillInfo.Items[CBYearSelection_BillInfo.Items.Count - 1].ToString()));
                                if (dtCheckMonthinlastyear.Rows.Count == 0)
                                {
                                    CBYearSelection_BillInfo.Items.RemoveAt(CBYearSelection_BillInfo.Items.Count - 1);
                                    CBYearSelection_ShareInfo.Items.RemoveAt(CBYearSelection_ShareInfo.Items.Count - 1);
                                }
                            }
                            //thisyeardiscount2year +1 หลังจบ loop ทุกรอบ
                            thisyeardiscount2year++;
                        }
                    }
                    //หาก if บนไม่เป็นจริง ให้มาดูเงื่อนไขนี้ต่อ หาก วันที่สมัคร มากกว่า หรือ เท่ากับ ปีนี้ -5 ปี
                    else if (YearRegister >= Convert.ToInt32(BankTeacher.Bank.Menu.Date[0]) - 5)
                    {
                        //หาก วันที่สมัคร น้อยกว่า หรือเท่ากับ ปีนี้ + จำนวนปีที่จ่ายล่าสุด ให้ loop
                        while (YearRegister <= Convert.ToInt32(BankTeacher.Bank.Menu.Date[0]) + Yearlastofpay)
                        {
                            //เพิ่ม thisyeardiscount2year ลงไปใน Combobox ปี หน้า จ่าย ข้อมูลหุ้น และ ข้อมูลบิลล์
                            CBYearSelection_Pay.Items.Add(YearRegister);
                            CBYearSelection_ShareInfo.Items.Add(YearRegister);
                            CBYearSelection_BillInfo.Items.Add(YearRegister);
                            //หาก ปีที่สมัคร = ปีนี้ + จำนวนปีที่จ่ายล่าสุด
                            if (YearRegister == Convert.ToInt32(BankTeacher.Bank.Menu.Date[0]) + Yearlastofpay)
                            {
                                //หาบิลล์ในปีนั้นๆหากไม่มีให้ลบปี ใน Combobox ปี หน้า ข้อมูลบิลล์
                                DataTable dtCheckMonthinlastyear = BankTeacher.Class.SQLConnection.InputSQLMSSQL(SQLDefault[3]
                                    .Replace("{TeacherNo}", TBTeacherNo.Text)
                                    .Replace("{Year}", CBYearSelection_BillInfo.Items[CBYearSelection_BillInfo.Items.Count - 1].ToString()));
                                if (dtCheckMonthinlastyear.Rows.Count == 0)
                                {
                                    CBYearSelection_BillInfo.Items.RemoveAt(CBYearSelection_BillInfo.Items.Count - 1);
                                    CBYearSelection_ShareInfo.Items.RemoveAt(CBYearSelection_ShareInfo.Items.Count - 1);
                                }
                            }
                            //YearRegister + 1 หลังจบ loop ทุกรอบ
                            YearRegister++;

                        }
                    }
                    //---------------------------------------------------------------Year-------------------------------------------------
                    //loop จาก จำนวนปีใน Combobox ปี หน้าจ่าย
                    for (int Yearloop = 0; Yearloop < CBYearSelection_Pay.Items.Count; Yearloop++)
                    {
                        //DM เพิ่ม list หลัก ตามปี และประกาษตัวแปร เดือนปี ที่สมัคร และ เดือนปีที่ จ่ายล่าสุด
                        DM.Add(new List<int>());
                        int Month = Convert.ToInt32((Convert.ToDateTime(ds.Tables[1].Rows[0][0].ToString())).ToString("MM"));
                        int Year = Convert.ToInt32((Convert.ToDateTime(ds.Tables[1].Rows[0][0].ToString())).ToString("yyyy"));
                        int lastMonthofpay = Convert.ToInt32((Convert.ToDateTime(ds.Tables[2].Rows[0][2].ToString())).ToString("MM"));
                        int lastYearofpay = Convert.ToInt32((Convert.ToDateTime(ds.Tables[2].Rows[0][2].ToString())).ToString("yyyy"));
                        DateTime lastDateofPay = Convert.ToDateTime((Convert.ToDateTime((lastYearofpay + "-" + lastMonthofpay + "-" + DateTime.DaysInMonth(lastYearofpay, lastMonthofpay)).ToString())).ToString("yyyy-MM-dd"));
                        DateTime Dateinloop;

                        //หาก Combobox ปีหน้าจ่ายตำแหน่งที่ Yearloop ตรงกับปีที่สมัคร
                        if (CBYearSelection_Pay.Items[Yearloop].ToString() == Year.ToString())
                        {
                            //หากเดือนที่สมัคร น้อยกว่า หรือ เท่ากับเดือนที่ 12 ให้ loop
                            while (Month <= 12)
                            {
                                //Dateinloop = Combobox ปีที่ loop + เดือนที่ loop + วันที่มากที่สุดในเดือน
                                Dateinloop = Convert.ToDateTime((Convert.ToDateTime((Convert.ToInt32(CBYearSelection_Pay.Items[Yearloop].ToString()) + "-" + Month + "-" + DateTime.DaysInMonth(Convert.ToInt32(CBYearSelection_Pay.Items[Yearloop].ToString()), Month)).ToString())).ToString("yyyy-MM-dd"));
                                //หาก Dateinloop น้อยกว่า ปี เดือนที่จ่าย ล่าสุด ให้ หยุด loop
                                if (Dateinloop > lastDateofPay)
                                {
                                    break;
                                }
                                //เพิ่มเดือน ใน DM ปีที่ loop และให้ Month + 1 หลังจบ loop
                                DM[Yearloop].Add(Month);
                                Month++;
                            }
                        }
                        //หาก เงื่อนไขบนไม่เป็นจริงให้มาดูเงื่อนไขนี้
                        else
                        {
                            //ประกาศให้ Months = 1 และ loop 12 ครั้ง และให้ Months +1 ทุกครั้งหลัง จบ loop
                            for (int Months = 1; Months <= 12; Months++)
                            {
                                //Dateinloop = ปีที่ loop + เดือนที่ loop + วันที่มากที่สุดในเดือน
                                Dateinloop = Convert.ToDateTime((Convert.ToDateTime((Convert.ToInt32(CBYearSelection_Pay.Items[Yearloop].ToString()) + "-" + Months + "-" + DateTime.DaysInMonth(Convert.ToInt32(CBYearSelection_Pay.Items[Yearloop].ToString()), Months)).ToString())).ToString("yyyy-MM-dd"));
                                //หาก Dateinloop น้อยกว่า วันที่จ่ายล่าสุดให้หยุด loop
                                if (Dateinloop > lastDateofPay)
                                {
                                    break;
                                }
                                //เพิ่มเดือน ใน DM ปีที่ loop และให้ Month + 1 หลังจบ loop
                                DM[Yearloop].Add(Months);
                            }
                        }
                        //ประกาศตัวแปร ตำแหน่งที่ บย Monthloop วันที่เริ่มจ่ายกู้  สิ้นสุดกู้ และ วันเดือนปี ปัจจุบัน(ในloop)
                        List<int> RemovePosistion = new List<int>();
                        RemovePosistion.Clear();
                        int Monthloop = 0;
                        DateTime startDatePayLoan = Convert.ToDateTime(Convert.ToDateTime("1999-01-1").ToString("yyyy-MM-dd"));
                        DateTime EndDatePayLoan = Convert.ToDateTime(Convert.ToDateTime("1999-01-1").ToString("yyyy-MM-dd"));
                        DateTime Now = Convert.ToDateTime(Convert.ToDateTime("1999-01-1").ToString("yyyy-MM-dd"));
                        //ล้าง RMonth และ Add เข้าไปใหม่ 0-11 แทน เดือน
                        //RMonth ใช้เก็บจำนวนเดือนนั้นๆว่ามีบิลล์อยู่เท่าไหร่ หากมี เดือนนั้น จะเป็น 1 เช่น
                        //EX: loop RMonth[2].[x] == 1 จะเป็น RMonth เดือนที่ 3 ,มีรายการ ก็จะข้ามการลบเดือนนี้ออกไป
                        //แต่หาก ไม่เท่ากับ 1 จนหมดเดือนนั้นก็จะเอาไป ทำการ ลบ เดือนนั้นออก
                        RMonth.Clear();
                        for (int Count = 0; Count < 12; Count++)
                        {
                            RMonth.Add(new List<int>());
                        }
                        //loop เดือนจาก DM[ปีที่ loop] และจบ loop x+1
                        for (int x = 0; x < DM[Yearloop].Count; x++)
                        {
                            Monthloop = Convert.ToInt32(DM[Yearloop][x]);
                            Now = Convert.ToDateTime((Convert.ToDateTime((CBYearSelection_Pay.Items[Yearloop].ToString() + '-' + Monthloop.ToString() + '-' +
                                    DateTime.DaysInMonth(Convert.ToInt32(CBYearSelection_Pay.Items[Yearloop].ToString()), Monthloop))
                                    .ToString())).ToString("yyyy-MM-dd"));
                            //เช็คว่าจ่าย สะสมเดือนนี้ไปรึยัง
                            DataTable PaySavingCheck = Class.SQLConnection.InputSQLMSSQL(SQLDefault[4]
                                        .Replace("{Month}", (Monthloop).ToString())
                                        .Replace("{Year}", CBYearSelection_Pay.Items[Yearloop].ToString())
                                        .Replace("{TeacherNo}", TBTeacherNo.Text)
                                        .Replace("{Date}", (Convert.ToDateTime(CBYearSelection_Pay.Items[Yearloop].ToString() + "-" + Monthloop.ToString() + "-" + DateTime.DaysInMonth(Convert.ToInt32(CBYearSelection_Pay.Items[Yearloop].ToString()), Convert.ToInt32(Monthloop)))).ToString("yyyy-MM-dd")));
                            //จ่ายหุ้นสะสมเดือนนี้ไปแล้ว
                            if (PaySavingCheck.Rows.Count == 0)
                            {
                                RMonth[x].Add(0);
                            }
                            //มีหุ้นยังไม่จ่ายในเดือนนนั้น
                            else
                            {
                                RMonth[x].Add(1);
                            }

                            //loop จำนวนกู้
                            if (Loan.Count != 0)
                                for (int y = 0; y < Loan.Count; y++)
                                {
                                    //เช็คว่าเดือนนี้จ่ายกู้รึยัง 
                                    DataSet PayLoanCheck = Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[5]
                                            .Replace("{Month}", (Monthloop).ToString())
                                            .Replace("{Year}", CBYearSelection_Pay.Items[Yearloop].ToString())
                                            .Replace("{LoanNo}", Loan[y].ToString())
                                            .Replace("{Date}", (Convert.ToDateTime(CBYearSelection_Pay.Items[Yearloop].ToString() + "-" + Monthloop.ToString() + "-" + DateTime.DaysInMonth(Convert.ToInt32(CBYearSelection_Pay.Items[Yearloop].ToString()), Convert.ToInt32(Monthloop)))).ToString("yyyy-MM-dd")));
                                    //เช็คว่ามีกู้นี้ในระบบมั้ย
                                    if (PayLoanCheck.Tables[1].Rows.Count != 0)
                                    {
                                        startDatePayLoan = Convert.ToDateTime((Convert.ToDateTime((PayLoanCheck.Tables[1].Rows[0][2].ToString()
                                            + '-' + PayLoanCheck.Tables[1].Rows[0][1].ToString()
                                            + '-' + DateTime.DaysInMonth(Convert.ToInt32(PayLoanCheck.Tables[1].Rows[0][2].ToString())
                                            , Convert.ToInt32(PayLoanCheck.Tables[1].Rows[0][1].ToString()))).ToString())).ToString("yyyy-MM-dd"));
                                        EndDatePayLoan = Convert.ToDateTime((Convert.ToDateTime(PayLoanCheck.Tables[1].Rows[0][6].ToString())).ToString("yyyy-MM-dd"));
                                    }
                                    //หากมีกู้ที่ยังจ่ายอยู่ (เอาตรงๆเอาไว้นับเดือนแรกเฉยๆ) ต้องมีการกู้ แต่ยังไม่ได้จ่ายเดือนแรก และต้อง เดือนปีต้องมากกว่า วันที่เริ่มแจ่าย แต่ ห้ามเยอะกว่าวันที่สิ้นสุด
                                    if (PayLoanCheck.Tables[1].Rows.Count != 0 && PayLoanCheck.Tables[0].Rows.Count == 0 &&
                                        Now >= startDatePayLoan &&
                                        Now <= EndDatePayLoan && PayLoanCheck.Tables[2].Rows.Count == 0)
                                    {
                                        RMonth[x].Add(1);
                                    }
                                    //หาก เดือนนั้นยังไม่ได้จ่าย แต่เปิดบิลล์แรกแล้ว
                                    else if (PayLoanCheck.Tables[0].Rows.Count >= 1)
                                    {
                                        RMonth[x].Add(1);
                                    }
                                    //หากไม่ตรงกับเงื่อนไขอะไรเลย แปลว่าเดือนนั้นกู้จ่ายไปหมดแล้ว
                                    else
                                    {
                                        RMonth[x].Add(0);
                                    }
                                }
                            for (int DetailRMonthCount = 0; DetailRMonthCount < RMonth.Count; DetailRMonthCount++)
                            {
                                //ขอแก้แบบโง่ๆไปก่อนตอนนี้ จะ 6 โมงเช้าแล้วยังแก้ ไฟล์ไม่เสร็จเลยขอรับ -Mon
                                if (DetailRMonthCount == RMonth.Count - 1)
                                {
                                    //loop เอาเดือนจาก RMonth แล้ว loop จาก RMonth[เดือน] ออกมา
                                    //หาว่ามีรายการอยู่ในนั้นมั้ย หาก มี ให้ข้าม หาก ไม่จน loop หมดทั้งเดือน ให้ ลบ
                                    for (int Count = 0; Count < RMonth.Count; Count++)
                                    {
                                        for (int CountDetail = 0; CountDetail < RMonth[Count].Count; CountDetail++)
                                        {
                                            if (RMonth[Count][CountDetail] == 1)
                                            {
                                                break;
                                            }
                                            else if (CountDetail == RMonth[Count].Count - 1)
                                            {
                                                if (RemovePosistion.Count != 0)
                                                {
                                                    for (int y = 0; y < RemovePosistion.Count; y++)
                                                    {
                                                        if (RemovePosistion[y] == Count)
                                                        {
                                                            break;
                                                        }
                                                        else if (y == RemovePosistion.Count - 1 && RemovePosistion[y] != Count && RMonth[Count][CountDetail] == 0)
                                                        {
                                                            RemovePosistion.Add(Count);
                                                            break;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    RemovePosistion.Add(Count);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        //loop ลบเดือนของ DM ตามที่เช็คมาก่อนหน้า
                        for (int x = 0; x < RemovePosistion.Count; x++)
                        {
                            if (RemovePosistion[x] >= 0)
                                DM[Yearloop].RemoveAt(RemovePosistion[x]);
                            for (int y = 0; y < RemovePosistion.Count; y++)
                            {
                                RemovePosistion[y] = RemovePosistion[y] - 1;
                            }
                        }
                    }
                    List<int> RemovePosition = new List<int>();
                    for (int Count = 0; Count < DM.Count; Count++)
                    {
                        if (DM[Count].Count == 0)
                        {
                            RemovePosition.Add(Count);
                        }
                    }
                    for (int Count = 0; Count < RemovePosition.Count; Count++)
                    {
                        DM.RemoveAt(RemovePosition[Count]);
                        CBYearSelection_Pay.Items.RemoveAt(RemovePosition[Count]);
                        for (int y = 0; y < RemovePosition.Count; y++)
                        {
                            RemovePosition[y] = RemovePosition[y] - 1;
                        }
                    }
                    //ทำให้่เลือกอัตโนมัติตอนกด
                    if (CBYearSelection_Pay.Items.Count != 0)
                    {
                        for (int x = 0; x < CBYearSelection_Pay.Items.Count; x++)
                        {
                            if (CBYearSelection_Pay.Items[x].ToString() == BankTeacher.Bank.Menu.Date[0])
                            {
                                CBYearSelection_Pay.SelectedIndex = x;
                            }
                            else if (x == CBYearSelection_Pay.Items.Count - 1)
                            {
                                CBYearSelection_Pay.SelectedIndex = 0;
                            }
                        }
                        CBYearSelection_ShareInfo.Text = BankTeacher.Bank.Menu.Date[0];
                        CBYearSelection_BillInfo.Text = BankTeacher.Bank.Menu.Date[0];
                        CBYearSelection_Pay.Enabled = true;
                        CBYearSelection_ShareInfo.Enabled = true;
                        for (int x = 0; x < CBYearSelection_Pay.Items.Count; x++)
                            YearinCB.Add(Convert.ToInt32(CBYearSelection_Pay.Items[x].ToString()));
                    }
                    // BackupDM = DM
                    if (BackupDM.Count == 0)
                    {
                        if (DM.Count != 0)
                        {
                            for (int x = 0; x < DM.Count; x++)
                            {
                                BackupDM.Add(new List<int>());
                                if (DM[x].Count != 0)
                                {
                                    for (int y = 0; y < DM[x].Count; y++)
                                    {
                                        BackupDM[x].Add(DM[x][y]);
                                    }
                                }
                            }
                        }
                    }
                    TBTeacherNo.Enabled = false;

                    CBYearSelection_BillInfo.Items.Clear();
                    DataSet dtYearBillInfo = Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[19]
                        .Replace("{TeacherNo}", TBTeacherNo.Text));
                    if (dtYearBillInfo.Tables[0].Rows.Count != 0)
                    {
                        for (int x = 0; x < dtYearBillInfo.Tables[0].Rows.Count; x++)
                        {
                            CBYearSelection_BillInfo.Items.Add(dtYearBillInfo.Tables[0].Rows[x][0].ToString());
                        }
                        CBYearSelection_BillInfo.SelectedIndex = 0;
                    }
                    else
                        CBYearSelection_BillInfo.Enabled = false;

                    if (CBYearSelection_Pay.Items.Count > 3)
                    {
                        for (int x = 3; x < CBYearSelection_Pay.Items.Count; x++)
                        {
                            CBYearSelection_Pay.Items.RemoveAt(x);
                            DM.RemoveAt(x);
                            YearinCB.RemoveAt(x);
                            BackupDM.RemoveAt(x);
                        }
                    }
                }
                CheckSave = false;
            }
        }

        private void TBTeacherNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!Char.IsNumber(e.KeyChar)) && (!Char.IsControl(e.KeyChar)) && !(e.KeyChar != ' '))
            {
                e.Handled = true;
            }
        }
        //=============================================================================================

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
                if (CheckPay == true)
                {
                    BSave_Pay.Enabled = false;
                }
        }

        //============================== tabpage 1 (Pay) ============================================
        //SelectYear
        private void CBYearSelection_Pay_SelectedIndexChanged(object sender, EventArgs e)
        {
            //หาก Datagridview ไม่มีข้อมูลอะไรอยู่เลยตอนเปลี่ยน ปี ให้ปรับปุ่ม ช่องทางการจ่าย และบันทึก เป็น off และ รวมเงินให้เป็น 0 เผื่อเอาไว้
            if (DGV_Pay.Rows.Count == 0)
            {
                CBPayment_Pay.Enabled = false;
                CBPayment_Pay.SelectedIndex = -1;
                BSave_Pay.Enabled = false;
                LBalance_Pay.Text = "0";
            }
            //เลือกปีต้องไม่ = ว่างเปล่า ถ้าเป็นจริงให้ล้าง Combobox เดือน
            if (CBYearSelection_Pay.SelectedIndex != -1)
            {
                CBMonthSelection_Pay.Items.Clear();
                //loop เอาจำนวนเดือนจากตัวแปร DM มาใส่ใน Combobox
                for (int Count = 0; Count < DM[CBYearSelection_Pay.SelectedIndex].Count; Count++)
                {
                    CBMonthSelection_Pay.Items.Add(DM[CBYearSelection_Pay.SelectedIndex][Count]);
                }
                //ถ้าหากจำนวนเดือน ใน Combobox ไม่เป็น 0 ให้เลือก ช่องแรก
                if (CBMonthSelection_Pay.Items.Count != 0)
                {
                    CBMonthSelection_Pay.Enabled = true;
                    CBMonthSelection_Pay.SelectedIndex = 0;
                }
                //ปีที่เลือกต้องเป็นปีนี้ ถึงให้ไปหาว่ามีเดือนนี้ไหมที่ยังไม่จ่าย
                if (CBYearSelection_Pay.Text == BankTeacher.Bank.Menu.Date[0])
                {
                    //loop หาเดือนนี้ใน Combobox หากมี ให้ เลือกเป็น เดือนนี้
                    for (int Count = 0; Count < CBMonthSelection_Pay.Items.Count; Count++)
                    {
                        if (CBMonthSelection_Pay.Items[Count].ToString() == BankTeacher.Bank.Menu.Date[1].ToString())
                        {
                            CBMonthSelection_Pay.SelectedIndex = Count;
                            break;
                        }
                    }
                }
                //หากจำนวนราบการใน Combobox เป็น 0 ให้ทำใน if
                if (CBList_Pay.Items.Count == 0)
                {
                    ComboBox[] cb = new ComboBox[] { CBList_Pay };
                    DataTable PaySavingCheck = Class.SQLConnection.InputSQLMSSQL(SQLDefault[4]
                                        .Replace("{Month}", (CBMonthSelection_Pay.Text).ToString())
                                        .Replace("{Year}", CBYearSelection_Pay.Text.ToString())
                                        .Replace("{TeacherNo}", TBTeacherNo.Text)
                                        .Replace("{Date}", (Convert.ToDateTime(CBYearSelection_Pay.Text.ToString() + "-" + CBMonthSelection_Pay.Text.ToString() + "-" + DateTime.DaysInMonth(Convert.ToInt32(CBYearSelection_Pay.Text.ToString()), Convert.ToInt32(CBMonthSelection_Pay.Text)))).ToString("yyyy-MM-dd")));
                    if (PaySavingCheck.Rows.Count != 0)
                    {
                        for (int Count = 0; Count < PaySavingCheck.Rows.Count; Count++)
                        {
                            for (int x = 0; x < cb.Length; x++)
                            {
                                for (int CountDGV = 0; CountDGV < DGV_Pay.Rows.Count; CountDGV++)
                                {
                                    if (CBYearSelection_Pay.Text == DGV_Pay.Rows[CountDGV].Cells[4].Value.ToString() && CBMonthSelection_Pay.Text == DGV_Pay.Rows[CountDGV].Cells[5].Value.ToString() && DGV_Pay.Rows[CountDGV].Cells[3].Value.ToString() == "-")
                                    {
                                        break;
                                    }
                                    else if (CountDGV == DGV_Pay.Rows.Count - 1)
                                        cb[x].Items.Add(new BankTeacher.Class.ComboBoxPayment(PaySavingCheck.Rows[x][2].ToString(),
                                        "-"));
                                }
                            }
                        }
                    }
                    DataTable getloan = Class.SQLConnection.InputSQLMSSQL(SQLDefault[6].Replace("{TeacherNo}", TBTeacherNo.Text));

                    if (getloan.Rows.Count != 0)
                    {
                        DateTime startDatePayLoan = Convert.ToDateTime(Convert.ToDateTime("1999-01-1").ToString("yyyy-MM-dd"));
                        DateTime EndDatePayLoan = Convert.ToDateTime(Convert.ToDateTime("1999-01-1").ToString("yyyy-MM-dd"));
                        DateTime Now = Convert.ToDateTime(Convert.ToDateTime("1999-01-1").ToString("yyyy-MM-dd"));
                        Now = Convert.ToDateTime((Convert.ToDateTime((CBYearSelection_Pay.Text.ToString() + '-' + CBMonthSelection_Pay.Text.ToString() + '-' +
                       DateTime.DaysInMonth(Convert.ToInt32(CBYearSelection_Pay.Text.ToString()), Convert.ToInt32(CBMonthSelection_Pay.Text)))
                       .ToString())).ToString("yyyy-MM-dd"));
                        for (int y = 0; y < getloan.Rows.Count; y++)
                        {
                            DataSet dsLoan = Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[5]
                                        .Replace("{Month}", CBMonthSelection_Pay.Text)
                                        .Replace("{Year}", CBYearSelection_Pay.Text)
                                        .Replace("{LoanNo}", getloan.Rows[y][0].ToString())
                                        .Replace("{Date}", (Convert.ToDateTime(CBYearSelection_Pay.Text + "-" + CBMonthSelection_Pay.Text + "-" + DateTime.DaysInMonth(Convert.ToInt32(CBYearSelection_Pay.Text), Convert.ToInt32(CBMonthSelection_Pay.Text)))).ToString("yyyy-MM-dd")));
                            if (dsLoan.Tables[1].Rows.Count != 0)
                            {
                                startDatePayLoan = Convert.ToDateTime((Convert.ToDateTime((dsLoan.Tables[1].Rows[0][2].ToString()
                                    + '-' + dsLoan.Tables[1].Rows[0][1].ToString()
                                    + '-' + DateTime.DaysInMonth(Convert.ToInt32(dsLoan.Tables[1].Rows[0][2].ToString())
                                    , Convert.ToInt32(dsLoan.Tables[1].Rows[0][1].ToString()))).ToString())).ToString("yyyy-MM-dd"));
                                EndDatePayLoan = Convert.ToDateTime((Convert.ToDateTime(dsLoan.Tables[1].Rows[0][6].ToString())).ToString("yyyy-MM-dd"));
                            }
                            //เดือนนี้จ่านไปรึยัง หากยังจะขึ้น Rows = 1 (ไม่สามารถ ดูเดือนแรกได้เนื่องจากไม่มี Ref ข้อมูล)
                            if (dsLoan.Tables[0].Rows.Count > 0)
                            {
                                for (int x = 0; x < cb.Length; x++)
                                {
                                    int MonthLoan = Convert.ToInt32(dsLoan.Tables[1].Rows[0][1].ToString());
                                    int YearLoan = Convert.ToInt32(dsLoan.Tables[1].Rows[0][2].ToString());
                                    int PayNo = Convert.ToInt32(dsLoan.Tables[1].Rows[0][3].ToString()) - 1;
                                    int sumy = MonthLoan + PayNo;
                                    int Balance = 0;
                                    //+ เจำนวนเดือนที่จ่าย
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
                                        //หากเป็นจ่ายกู้เดือนสุดท้าย 
                                        if (DateLoan == Convert.ToDateTime(CBYearSelection_Pay.Text + '-' + CBMonthSelection_Pay.Text + '-' + DateTime.DaysInMonth(Convert.ToInt32(CBYearSelection_Pay.Text), Convert.ToInt32(CBMonthSelection_Pay.Text)).ToString()))
                                        {
                                            Balance = Convert.ToInt32(dsLoan.Tables[0].Rows[0][2].ToString());
                                        }
                                        //หากเงื่อนไขบนไม่เป็นจริง หรือก็คือ เป็นเดือนปกติ ที่ไม่ใช่เดือนสุดท้ายของการกู้
                                        else
                                        {
                                            //ให้ใช้ราคา เดือนปกติ
                                            Balance = Convert.ToInt32(dsLoan.Tables[0].Rows[0][1].ToString());
                                        }
                                        //หาก DGV มีค่าอยู่แล้ว
                                        if (DGV_Pay.Rows.Count != 0)
                                        {
                                            //loop จำนวนใน DGV
                                            for (int Count = 0; Count < DGV_Pay.Rows.Count; Count++)
                                            {
                                                //หากมีรายการกู้ที่ เดือน - ปี เดียวกัน และ รายการกู้เดียวกันใน DGV ไม่ต้องใส่
                                                if (CBYearSelection_Pay.Text == DGV_Pay.Rows[Count].Cells[4].Value.ToString() && CBMonthSelection_Pay.Text == DGV_Pay.Rows[Count].Cells[5].Value.ToString() && "รายการกู้ " + getloan.Rows[y][0].ToString() == DGV_Pay.Rows[Count].Cells[1].Value.ToString())
                                                {
                                                    break;
                                                }
                                                //หากหายันอันสุดท้ายแล้วไม่มี ให้เพิ่มเข้าไป
                                                else if (Count == DGV_Pay.Rows.Count - 1)
                                                {
                                                    for (int CountDGV = 0; CountDGV < DGV_Pay.Rows.Count; CountDGV++)
                                                    {
                                                        if (CBYearSelection_Pay.Text == DGV_Pay.Rows[CountDGV].Cells[4].Value.ToString() && CBMonthSelection_Pay.Text == DGV_Pay.Rows[CountDGV].Cells[5].Value.ToString() && DGV_Pay.Rows[CountDGV].Cells[3].Value.ToString() == getloan.Rows[y][0].ToString())
                                                        {
                                                            break;
                                                        }
                                                        cb[x].Items.Add(new BankTeacher.Class.ComboBoxPay("รายการกู้ " + getloan.Rows[y][0].ToString(), Balance.ToString(),
                                                        getloan.Rows[y][0].ToString()));
                                                    }
                                                }
                                            }
                                        }
                                        //หาก เงื่อนไขบน ไม่เป็นจริงหรือก็คือ ใน DGV  ไม่มีรายการให้เพิ่มเข้าไปได้เลย
                                        else
                                        {
                                            for (int CountDGV = 0; CountDGV < DGV_Pay.Rows.Count; CountDGV++)
                                            {
                                                if (CBYearSelection_Pay.Text == DGV_Pay.Rows[CountDGV].Cells[4].Value.ToString() && CBMonthSelection_Pay.Text == DGV_Pay.Rows[CountDGV].Cells[5].Value.ToString() && DGV_Pay.Rows[CountDGV].Cells[3].Value.ToString() == getloan.Rows[y][0].ToString())
                                                {
                                                    break;
                                                }
                                                else if (CountDGV == DGV_Pay.Rows.Count - 1)
                                                    cb[x].Items.Add(new BankTeacher.Class.ComboBoxPay("รายการกู้ " + getloan.Rows[y][0].ToString(), Balance.ToString(),
                                                        getloan.Rows[y][0].ToString()));
                                            }
                                        }
                                    }

                                }
                            }
                        }
                    }
                    if (CBList_Pay.Items.Count == 0)
                    {
                        MessageBox.Show("ไม่มีรายการให้ชำระภายในเดือนนี้", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        //CBList_Pay.Enabled = false;
                        //CBList_Pay.SelectedIndex = -1;
                        TBAmount_Pay.Text = "";
                        TBAmount_Pay.Text = "0";
                    }
                }
            }
            else
            {
                CBYearSelection_Pay.Enabled = false;
                CBMonthSelection_Pay.Enabled = false;
            }

        }

        //SelectMonth
        private void CBMonthSelection_Pay_SelectedIndexChanged(object sender, EventArgs e)
        {
            CBList_Pay.Items.Clear();
            CBList_Pay.SelectedIndex = -1;
            if (CBMonthSelection_Pay.SelectedIndex != -1)
            {
                DateTime startDatePayLoan = Convert.ToDateTime(Convert.ToDateTime("1999-01-1").ToString("yyyy-MM-dd"));
                DateTime EndDatePayLoan = Convert.ToDateTime(Convert.ToDateTime("1999-01-1").ToString("yyyy-MM-dd"));
                DateTime Now = Convert.ToDateTime(Convert.ToDateTime("1999-01-1").ToString("yyyy-MM-dd")); ;
                ComboBox[] cb = new ComboBox[] { CBList_Pay };
                //หา Saving ในเดือนนั้นว่ามีไม่้ได้จ่ายไหม
                DataTable dtSaving = Class.SQLConnection.InputSQLMSSQL(SQLDefault[4]
                    .Replace("{Month}", CBMonthSelection_Pay.Text)
                    .Replace("{Year}", CBYearSelection_Pay.Text)
                    .Replace("{TeacherNo}", TBTeacherNo.Text)
                    .Replace("{Date}", (Convert.ToDateTime(CBYearSelection_Pay.Text + "-" + CBMonthSelection_Pay.Text + "-" + DateTime.DaysInMonth(Convert.ToInt32(CBYearSelection_Pay.Text), Convert.ToInt32(CBMonthSelection_Pay.Text)))).ToString("yyyy-MM-dd")));

                Now = Convert.ToDateTime((Convert.ToDateTime((CBYearSelection_Pay.Text.ToString() + '-' + CBMonthSelection_Pay.Text.ToString() + '-' +
                    DateTime.DaysInMonth(Convert.ToInt32(CBYearSelection_Pay.Text.ToString()), Convert.ToInt32(CBMonthSelection_Pay.Text)))
                    .ToString())).ToString("yyyy-MM-dd"));
                //หุ้นสะสม
                if (dtSaving.Rows.Count != 0)
                    for (int a = 0; a < dtSaving.Rows.Count; a++)
                    {
                        for (int x = 0; x < cb.Length; x++)
                        {
                            if (DGV_Pay.Rows.Count != 0)
                            {
                                //loop จำนวนใน DGV
                                for (int Count = 0; Count < DGV_Pay.Rows.Count; Count++)
                                {
                                    //หากมีรายการกู้ที่ เดือน - ปี เดียวกัน และ หุ้นเดียวกันใน DGV ไม่ต้องใส่
                                    if (CBYearSelection_Pay.Text == DGV_Pay.Rows[Count].Cells[4].Value.ToString() && CBMonthSelection_Pay.Text == DGV_Pay.Rows[Count].Cells[5].Value.ToString() && dtSaving.Rows[a][2].ToString() == DGV_Pay.Rows[Count].Cells[1].Value.ToString())
                                    {
                                        break;
                                    }
                                    //หากหายันอันสุดท้ายแล้วไม่มี ให้เพิ่มเข้าไป
                                    else if (Count == DGV_Pay.Rows.Count - 1)
                                    {
                                        cb[x].Items.Add(new BankTeacher.Class.ComboBoxPay(dtSaving.Rows[a][2].ToString(),
                                        dtSaving.Rows[a][1].ToString(),
                                        "-"));

                                    }
                                }
                            }
                            //หาก เงื่อนไขบน ไม่เป็นจริงหรือก็คือ ใน DGV  ไม่มีรายการให้เพิ่มเข้าไปได้เลย
                            else
                            {
                                cb[x].Items.Add(new BankTeacher.Class.ComboBoxPay(dtSaving.Rows[a][2].ToString(),
                                dtSaving.Rows[a][1].ToString(),
                                "-"));
                            }
                        }
                    }
                //กู้
                //เช็คว่ามีกู้มั้ย
                DataTable getloan = Class.SQLConnection.InputSQLMSSQL(SQLDefault[6].Replace("{TeacherNo}", TBTeacherNo.Text));
                if (getloan.Rows.Count != 0)
                {
                    //loop จากจำนวนกู้้ที่มี
                    for (int CountLoan = 0; CountLoan < getloan.Rows.Count; CountLoan++)
                    {
                        DataSet dsLoan = Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[5]
                            .Replace("{Month}", CBMonthSelection_Pay.Text)
                            .Replace("{Year}", CBYearSelection_Pay.Text)
                            .Replace("{LoanNo}", getloan.Rows[CountLoan][0].ToString())
                            .Replace("{Date}", (Convert.ToDateTime(CBYearSelection_Pay.Text + "-" + CBMonthSelection_Pay.Text + "-" + DateTime.DaysInMonth(Convert.ToInt32(CBYearSelection_Pay.Text), Convert.ToInt32(CBMonthSelection_Pay.Text)))).ToString("yyyy-MM-dd")));
                        if (dsLoan.Tables[1].Rows.Count != 0)
                        {
                            startDatePayLoan = Convert.ToDateTime((Convert.ToDateTime((dsLoan.Tables[1].Rows[0][2].ToString()
                                + '-' + dsLoan.Tables[1].Rows[0][1].ToString()
                                + '-' + DateTime.DaysInMonth(Convert.ToInt32(dsLoan.Tables[1].Rows[0][2].ToString())
                                , Convert.ToInt32(dsLoan.Tables[1].Rows[0][1].ToString()))).ToString())).ToString("yyyy-MM-dd"));
                            EndDatePayLoan = Convert.ToDateTime((Convert.ToDateTime(dsLoan.Tables[1].Rows[0][6].ToString())).ToString("yyyy-MM-dd"));
                        }
                        //เดือนนี้จ่านไปรึยัง หากยังจะขึ้น Rows = 1 (ไม่สามารถ ดูเดือนแรกได้เนื่องจากไม่มี Ref ข้อมูล)
                        if (dsLoan.Tables[0].Rows.Count > 0)
                        {
                            for (int x = 0; x < cb.Length; x++)
                            {
                                int MonthLoan = Convert.ToInt32(dsLoan.Tables[1].Rows[0][1].ToString());
                                int YearLoan = Convert.ToInt32(dsLoan.Tables[1].Rows[0][2].ToString());
                                int PayNo = Convert.ToInt32(dsLoan.Tables[1].Rows[0][3].ToString()) - 1;
                                int sumy = MonthLoan + PayNo;
                                int Balance = 0;
                                //+ เจำนวนเดือนที่จ่าย
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
                                    //หากเป็นจ่ายกู้เดือนสุดท้าย 
                                    if (DateLoan == Convert.ToDateTime(CBYearSelection_Pay.Text + '-' + CBMonthSelection_Pay.Text + '-' + DateTime.DaysInMonth(Convert.ToInt32(CBYearSelection_Pay.Text), Convert.ToInt32(CBMonthSelection_Pay.Text)).ToString()))
                                    {
                                        if (DTPDate.Value <= DateLoan)
                                            Balance = Convert.ToInt32(dsLoan.Tables[0].Rows[0][2].ToString());
                                        else
                                            Balance = Convert.ToInt32(dsLoan.Tables[0].Rows[0][3].ToString());
                                    }
                                    //หากเงื่อนไขบนไม่เป็นจริง หรือก็คือ เป็นเดือนปกติ ที่ไม่ใช่เดือนสุดท้ายของการกู้
                                    else
                                    {
                                        //ให้ใช้ราคา เดือนปกติ
                                        Balance = Convert.ToInt32(dsLoan.Tables[0].Rows[0][1].ToString());
                                    }
                                    //หาก DGV มีค่าอยู่แล้ว
                                    if (DGV_Pay.Rows.Count != 0)
                                    {
                                        //loop จำนวนใน DGV
                                        for (int Count = 0; Count < DGV_Pay.Rows.Count; Count++)
                                        {
                                            //หากมีรายการกู้ที่ เดือน - ปี เดียวกัน และ รายการกู้เดียวกันใน DGV ไม่ต้องใส่
                                            if (CBYearSelection_Pay.Text == DGV_Pay.Rows[Count].Cells[4].Value.ToString() && CBMonthSelection_Pay.Text == DGV_Pay.Rows[Count].Cells[5].Value.ToString() && "รายการกู้ " + getloan.Rows[CountLoan][0].ToString() == DGV_Pay.Rows[Count].Cells[1].Value.ToString())
                                            {
                                                break;
                                            }
                                            //หากหายันอันสุดท้ายแล้วไม่มี ให้เพิ่มเข้าไป
                                            else if (Count == DGV_Pay.Rows.Count - 1)
                                            {
                                                cb[x].Items.Add(new BankTeacher.Class.ComboBoxPay("รายการกู้ " + getloan.Rows[CountLoan][0].ToString(), Balance.ToString(),
                                                    getloan.Rows[CountLoan][0].ToString()));
                                            }
                                        }
                                    }
                                    //หาก เงื่อนไขบน ไม่เป็นจริงหรือก็คือ ใน DGV  ไม่มีรายการให้เพิ่มเข้าไปได้เลย
                                    else
                                    {
                                        cb[x].Items.Add(new BankTeacher.Class.ComboBoxPay("รายการกู้ " + getloan.Rows[CountLoan][0].ToString(), Balance.ToString(),
                                                    getloan.Rows[CountLoan][0].ToString()));
                                    }
                                }

                            }
                        }
                        //หากยังไม่ได้เริ่มจ่ายเลย และ วันนี้ อยู่ในช่วง จ่ายได้ - เดือนสุดท้ายของกู้
                        else if (dsLoan.Tables[0].Rows.Count <= 0 && Now >= startDatePayLoan && Now <= EndDatePayLoan)
                        {
                            //ต้องไม่มีบิลล์ของ รายการกู้นี้
                            if (dsLoan.Tables[2].Rows.Count == 0)
                            {
                                int AmountPay = Convert.ToInt32(dsLoan.Tables[1].Rows[0][4].ToString());
                                //หากเดือนนี้เป็นเดือนสุดท้ายให้เปลี่ยนราคา
                                if (Now == EndDatePayLoan)
                                {
                                    try
                                    {
                                        AmountPay = Convert.ToInt32(dsLoan.Tables[0].Rows[0][2].ToString());
                                    }
                                    catch
                                    {
                                        AmountPay = Convert.ToInt32(dsLoan.Tables[1].Rows[0][5].ToString());
                                    }
                                }
                                if (DGV_Pay.Rows.Count != 0)
                                {
                                    for (int Count = 0; Count < DGV_Pay.Rows.Count; Count++)
                                    {
                                        //ถ้ามีรายการปี - เดือน แล้วกู้อันเดียวกันอยู่ให่้หยุดแต่หากไม่มีให้เพิ่มลงไปใน Combobox list
                                        if (CBYearSelection_Pay.Text == DGV_Pay.Rows[Count].Cells[4].Value.ToString() && CBMonthSelection_Pay.Text == DGV_Pay.Rows[Count].Cells[5].Value.ToString() && "รายการกู้ " + getloan.Rows[CountLoan][0].ToString() == DGV_Pay.Rows[Count].Cells[1].Value.ToString())
                                        {
                                            break;
                                        }
                                        //หาก เงื่อนไขบน ไม่เป็นจริงหรือก็คือ ใน DGV  ไม่มีรายการให้เพิ่มเข้าไปได้เลย
                                        else if (Count == DGV_Pay.Rows.Count - 1)
                                        {
                                            cb[0].Items.Add(new BankTeacher.Class.ComboBoxPay("รายการกู้ " + getloan.Rows[CountLoan][0], AmountPay.ToString(),
                                                getloan.Rows[CountLoan][0].ToString()));
                                        }
                                    }
                                }
                                else
                                {
                                    cb[0].Items.Add(new BankTeacher.Class.ComboBoxPay("รายการกู้ " + getloan.Rows[CountLoan][0], AmountPay.ToString(),
                                        getloan.Rows[CountLoan][0].ToString()));
                                }
                            }
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
            if (CBMonthSelection_Pay.SelectedIndex == -1 && CBYearSelection_Pay.SelectedIndex == -1)
            {
                if (Int32.TryParse(TBAmount_Pay.Text, out int Values) && Values != 0)
                    TBAmount_Pay.Text = "0";
                MessageBox.Show("โปรดเลือกเดือนและปีก่อนทำรายการ", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (CBList_Pay.Items.Count == 0)
                return;

            if (DGV_Pay.Rows.Count >= 17)
            {
                MessageBox.Show("รายการในบิลล์นี้เยอะเกินไป โปรดจ่ายต่อในบิลล์ถัดไป", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
          
            for (int x = 0; x < CBList_Pay.Items.Count; x++)
            {
                CBList_Pay.SelectedIndex = x;
                BankTeacher.Class.ComboBoxPay Loan = (CBList_Pay.SelectedItem as BankTeacher.Class.ComboBoxPay);
                String Time = CBYearSelection_Pay.Text + "/" + CBMonthSelection_Pay.Text;
                if (DGV_Pay.Rows.Count == 0)
                {
                    Notadd = true;
                    DGV_Pay.Rows.Add(Time, CBList_Pay.Text, TBAmount_Pay.Text, Loan.No, CBYearSelection_Pay.Text, CBMonthSelection_Pay.Text);
                    Notadd = false;
                    CheckInsertDGVBehidePay();
                }
                else
                {
                    for (int Count = 0; Count < DGV_Pay.Rows.Count; Count++)
                    {
                        if (Time == DGV_Pay.Rows[Count].Cells[0].Value.ToString() && CBList_Pay.Text == DGV_Pay.Rows[Count].Cells[1].Value.ToString())
                        {
                            break;
                        }
                        else if (Count == DGV_Pay.Rows.Count - 1)
                        {
                            Notadd = true;
                            DGV_Pay.Rows.Add(Time, CBList_Pay.Text, TBAmount_Pay.Text, Loan.No, CBYearSelection_Pay.Text, CBMonthSelection_Pay.Text);
                            Notadd = false;
                            CheckInsertDGVBehidePay();
                        }
                    }
                }
            }
            DM[CBYearSelection_Pay.SelectedIndex].RemoveAt(CBMonthSelection_Pay.SelectedIndex);
            CBMonthSelection_Pay.Items.RemoveAt(CBMonthSelection_Pay.SelectedIndex);
            ReadonlyDGVPay();
            CBList_Pay.Items.Clear();
            if (CBList_Pay.Items.Count != 0)
                CBList_Pay.SelectedIndex = 0;
            CBPayment_Pay.Enabled = true;
            CBPayment_Pay.SelectedIndex = 0;

            if (CBList_Pay.Items.Count <= 0)
            {
                CBList_Pay.Enabled = false;
                BListAdd_Pay.Enabled = false;
                TBAmount_Pay.Text = "0";
            }
            Summoney();
            if (CBYearSelection_Pay.Items.Count != 0)
            {
                if (CBMonthSelection_Pay.Items.Count != 0)
                {
                    CBMonthSelection_Pay.SelectedIndex = 0;
                }
                else
                {
                    DM.RemoveAt(CBYearSelection_Pay.SelectedIndex);
                    CBYearSelection_Pay.Items.RemoveAt(CBYearSelection_Pay.SelectedIndex);
                    if (CBYearSelection_Pay.Items.Count != 0)
                    {
                        CBYearSelection_Pay.SelectedIndex = 0;
                        RemoveComboboxhAfterAdd();
                    }
                    else
                    {
                        CBYearSelection_Pay.Enabled = false;
                        CBMonthSelection_Pay.Enabled = false;
                        TBAmount_Pay.Enabled = false;
                        BAutoSelection.Enabled = false;
                    }
                }
            }
        }

        //Select list in combobox
        private void CBList_Pay_SelectedIndexChanged(object sender, EventArgs e)
        {
            BankTeacher.Class.ComboBoxPay Status = (CBList_Pay.SelectedItem as BankTeacher.Class.ComboBoxPay);
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
                else if (Status.Type.Contains("หุ้นสะสม"))
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
        //Can Write Num
        TextBox tb;
        private void DGV_Pay_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (DGV_Pay.CurrentCell.ColumnIndex >= 2)
            {
                e.Control.KeyPress -= NumericCheck;
                e.Control.KeyPress += NumericCheck;
                tb = (TextBox)e.Control;
                tb.KeyPress += new KeyPressEventHandler(TBCellKeyPress);
                tb.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TBKeyUp);
                tb.TextChanged += new System.EventHandler(this.TBTextChanged);
            }
        }

        void TBTextChanged(object sendet, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(tb.Text, "[^0-9]"))
            {
                DGV_Pay.CancelEdit();
            }
            if (tb.Text.Length > 6)
            {
                DGV_Pay.EndEdit();
            }
        }
        void TBKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.V && e.Modifiers == Keys.Control)
            {
                DGV_Pay.CancelEdit();
            }
            else if (e.KeyCode == Keys.V)
            {
                DGV_Pay.CancelEdit();
            }
        }
        void TBCellKeyPress(object sender, KeyPressEventArgs e)
        {

            if ((!Char.IsNumber(e.KeyChar)) && (!Char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }
        //Summoney After ChangeAmount
        private void DGV_Pay_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (Int32.TryParse(DGV_Pay.Rows[e.RowIndex].Cells[2].Value.ToString(), out int Amount))
                if (Amount >= BankTeacher.Bank.Menu.startAmountMin && Amount <= BankTeacher.Bank.Menu.startAmountMax)
                    Summoney();
                else if (Amount > BankTeacher.Bank.Menu.startAmountMax)
                {
                    DGV_Pay.Rows[e.RowIndex].Cells[2].Value = "500";
                    MessageBox.Show($"จำนวนเงินที่ระบุเกินกำหนด ขั้นสูงในการจ่ายอยู่ที่ {BankTeacher.Bank.Menu.startAmountMax} บาท. \r\n", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    DGV_Pay.Rows[e.RowIndex].Cells[2].Value = "500";
                    MessageBox.Show($"จำนวนเงินที่ระบุต่ำเกินไป ขั้นต่ำในการจ่ายอยู่ที่ {BankTeacher.Bank.Menu.startAmountMin} บาท. \r\n", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            DGV_Printbypoon.Rows[e.RowIndex].Cells[3].Value = Amount;
        }
        //Can only Type Numbers in textbox Amount
        private void TBAmount_Pay_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!Char.IsNumber(e.KeyChar)) && (!Char.IsControl(e.KeyChar)))
            {
                if ((e.KeyChar < '0' || e.KeyChar > '9') && (e.KeyChar != '\b'))
                {
                    e.Handled = true;
                }
            }
        }
        //Add list to datagridview
        // เช็คว่ารายการไหนควรที่จะเพิ่ม
        private bool Notadd;
        // ตรวจสอบยอดกู้ที่ทำการจ่ายทั้งหมด
        private string Amount_payLoan;
        private void BListAdd_Pay_Click(object sender, EventArgs e)
        {
            if (CBList_Pay.SelectedIndex == -1)
            {
                  if (Convert.ToInt32(TBAmount_Pay.Text) != 0)
                        TBAmount_Pay.Text = "0";
                MessageBox.Show("โปรดเลือกรายการ", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
          
            if (CBYearSelection_Pay.SelectedIndex == -1 && CBMonthSelection_Pay.SelectedIndex == -1)
            {
                MessageBox.Show("โปรดเลือกปีกับเดือนก่อนทำรายการ", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            if (TBAmount_Pay.Text == "")
            {
                if (CBMonthSelection_Pay.Items.Count != 0)
                    CBMonthSelection_Pay.SelectedIndex = 0;
                else
                   if (CBYearSelection_Pay.Items.Count != 0)
                    CBYearSelection_Pay.SelectedIndex = 0;
                return;
            }
          
            if (DGV_Pay.Rows.Count >= 18)
            {
                MessageBox.Show("สามารถทำรายการได้สูงสุด 18 ต่อ หนึ่งบิลรายการเท่านั้น", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            if (Int32.TryParse(TBAmount_Pay.Text, out int x) && x > 0)
            {
                if (x >= BankTeacher.Bank.Menu.startAmountMin && x <= BankTeacher.Bank.Menu.startAmountMax  && CBList_Pay.Items[CBList_Pay.SelectedIndex].ToString().Contains("หุ้นสะสม"))
                {
                    BankTeacher.Class.ComboBoxPay Loan = (CBList_Pay.SelectedItem as BankTeacher.Class.ComboBoxPay);
                    String Time = CBYearSelection_Pay.Text + "/" + CBMonthSelection_Pay.Text;
                    if (DGV_Pay.Rows.Count != 0)
                    {
                        for (int y = 0; y < DGV_Pay.Rows.Count; y++)
                        {
                            if (Time == DGV_Pay.Rows[y].Cells[0].Value.ToString() && CBList_Pay.Text == DGV_Pay.Rows[y].Cells[1].Value.ToString())
                            {
                                break;
                            }
                            else if (y == DGV_Pay.Rows.Count - 1)
                            {
                                Notadd = true;
                                DGV_Pay.Rows.Add(Time, CBList_Pay.Text, TBAmount_Pay.Text, Loan.No, CBYearSelection_Pay.Text, CBMonthSelection_Pay.Text);
                                Amount_payLoan = TBAmount_Pay.Text;
                                Notadd = false;
                                CheckInsertDGVBehidePay();
                                CBList_Pay.Items.RemoveAt(CBList_Pay.SelectedIndex);
                                ReadonlyDGVPay();
                                RemoveComboboxhAfterAdd();
                                CBPayment_Pay.SelectedIndex = 0;
                                CBPayment_Pay.Enabled = true;
                                break;
                            }
                        }
                    }
                    else
                    {
                        Notadd = true;
                        DGV_Pay.Rows.Add(Time, CBList_Pay.Text, TBAmount_Pay.Text, Loan.No, CBYearSelection_Pay.Text, CBMonthSelection_Pay.Text);
                        Amount_payLoan = TBAmount_Pay.Text;
                        Notadd = false;
                        CheckInsertDGVBehidePay();

                        CBList_Pay.Items.RemoveAt(CBList_Pay.SelectedIndex);
                        ReadonlyDGVPay();
                        RemoveComboboxhAfterAdd();
                        CBPayment_Pay.SelectedIndex = 0;
                        CBPayment_Pay.Enabled = true;
                    }
                }
                else if (x < BankTeacher.Bank.Menu.startAmountMin && CBList_Pay.Items[CBList_Pay.SelectedIndex].ToString().Contains("หุ้นสะสม"))
                {
                    MessageBox.Show($"จำนวนเงินที่ระบุต่ำเกินไป ขั้นต่ำในการจ่ายอยู่ที่ {BankTeacher.Bank.Menu.startAmountMin} บาท. \r\n", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (x > BankTeacher.Bank.Menu.startAmountMax && CBList_Pay.Items[CBList_Pay.SelectedIndex].ToString().Contains("หุ้นสะสม"))
                {
                    MessageBox.Show($"จำนวนเงินที่ระบุเกินกำหนด ขั้นสูงสุดในการจ่ายอยู่ที่ {BankTeacher.Bank.Menu.startAmountMax} บาท. \r\n", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    BankTeacher.Class.ComboBoxPay Loan = (CBList_Pay.SelectedItem as BankTeacher.Class.ComboBoxPay);
                    String Time = CBYearSelection_Pay.Text + "/" + CBMonthSelection_Pay.Text;
                    if (DGV_Pay.Rows.Count != 0)
                    {
                        for (int y = 0; y < DGV_Pay.Rows.Count; y++)
                        {
                            if (Time == DGV_Pay.Rows[y].Cells[0].Value.ToString() && CBList_Pay.Text == DGV_Pay.Rows[y].Cells[1].Value.ToString())
                            {
                                break;
                            }
                            else if (y == DGV_Pay.Rows.Count - 1)
                            {
                                Notadd = true;
                                DGV_Pay.Rows.Add(Time, CBList_Pay.Text, TBAmount_Pay.Text, Loan.No, CBYearSelection_Pay.Text, CBMonthSelection_Pay.Text);
                                Amount_payLoan = TBAmount_Pay.Text;
                                Notadd = false;
                                CheckInsertDGVBehidePay();
                                CBList_Pay.Items.RemoveAt(CBList_Pay.SelectedIndex);
                                ReadonlyDGVPay();
                                RemoveComboboxhAfterAdd();
                                CBPayment_Pay.SelectedIndex = 0;
                                CBPayment_Pay.Enabled = true;
                                break;
                            }
                        }
                    }
                    else
                    {
                        Notadd = true;
                        DGV_Pay.Rows.Add(Time, CBList_Pay.Text, TBAmount_Pay.Text, Loan.No, CBYearSelection_Pay.Text, CBMonthSelection_Pay.Text);
                        Amount_payLoan = TBAmount_Pay.Text;
                        Notadd = false;
                        CheckInsertDGVBehidePay();

                        CBList_Pay.Items.RemoveAt(CBList_Pay.SelectedIndex);
                        ReadonlyDGVPay();
                        RemoveComboboxhAfterAdd();
                        CBPayment_Pay.SelectedIndex = 0;
                        CBPayment_Pay.Enabled = true;
                    }
                }
                Summoney();
                if (CBList_Pay.Items.Count != 0)
                {
                    CBList_Pay.SelectedIndex = 0;
                    CBList_Pay_SelectedIndexChanged(new object(), new EventArgs());
                }
            }
            else
            {
                MessageBox.Show("โปรดระบุยอดเงินให้ถูกต้อง", "System", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //Select Payment
        private void CBPayment_Pay_SelectIndexChange(object sender, EventArgs e)
        {
            if (CBPayment_Pay.Text.Contains("โอน"))
            {
                BUploadFile_Pay.Visible = true;
            }
            else
            {
                BUploadFile_Pay.Visible = false;
            }
        }

        //Cleartabpage 1 Button
        private void BClearTab_Pay_Click(object sender, EventArgs e)
        {
            TBTeacherNo_KeyDown(new object(), new KeyEventArgs(Keys.Enter));
        }

        //SaveInfo Button
        int Printbill = 0;
        private void BSave_Pay_Click(object sender, EventArgs e)
        {
            BankTeacher.Class.ComboBoxPayment Payment = (CBPayment_Pay.SelectedItem as BankTeacher.Class.ComboBoxPayment);
            if (DGV_Pay.Rows.Count != 0)
            {
                // Get the Bill Number for show
                String BillNo;
                String SQL = "", Share = SQL, Loan = SQL, Bill = SQL;
                BillNo = Class.SQLConnection.InputSQLMSSQL(SQLDefault[11]).Rows[0][0].ToString();
                TBTeacherBill.Text = BillNo;
                int Balance = Convert.ToInt32(LBalance_Pay.Text);
                // Open the Form Calculator
                BankTeacher.Bank.Pay.Calculator calculator = new BankTeacher.Bank.Pay.Calculator(Balance);
                calculator.ShowDialog();
                // Payment amount must be valid
                if (BankTeacher.Bank.Pay.Calculator.Return)
                {
                    // Select date form the Datetimepicker
                    String Date = DTPDate.Value.ToString("yyyy:MM:dd");
                    Date = Date.Replace(":", "/");
                    // Insert bill and geting billnumber 
                    Bill = SQLDefault[7]
                        .Replace("{TeacherNo}", TBTeacherNo.Text)
                        .Replace("{TeacherNoAddBy}", Class.UserInfo.TeacherNo)
                        .Replace("{Date}", Date);
                    //BillNo = dtBillNo.Tables[0].Rows[0][0].ToString();
                    // CheckList for Payment  (pay/loan)
                    int totalShareAmount = 0;
                    // Update balance including user share
                    String UpdateShare = "";
                    String Updategunratorloan = "";
                    for (int x = 0; x < DGV_Pay.Rows.Count; x++)
                    {
                        if (DGV_Pay.Rows[x].Cells[1].Value.ToString().Contains("หุ้น")) // Pay
                        {
                            // Inset Bill
                            Share += SQLDefault[8]
                            .Replace("{BillNo}", BillNo)
                            .Replace("{TypeNo}", "1")
                            .Replace("{LoanNo}", "null")
                            .Replace("{Amount}", DGV_Pay.Rows[x].Cells[2].Value.ToString())
                            .Replace("{Month}", DGV_Pay.Rows[x].Cells[5].Value.ToString())
                            .Replace("{Year}", DGV_Pay.Rows[x].Cells[4].Value.ToString())
                            .Replace("{BillDetailPaymentNo}", (CBPayment_Pay.SelectedIndex + 1).ToString());

                            if(int.TryParse(DGV_Pay.Rows[x].Cells[2].Value.ToString(),out _))
                            totalShareAmount += Convert.ToInt32(DGV_Pay.Rows[x].Cells[2].Value.ToString());
                            else{ 
                                MessageBox.Show("พบปัญหาในการทำรายการ โปรดเเจ้งผู้ทำระบบ","",MessageBoxButtons.OK,MessageBoxIcon.Error);
                                Application.Exit();
                            }
                        }
                        else if (DGV_Pay.Rows[x].Cells[1].Value.ToString().Contains("กู้")) // Loan
                        {
                            DataTable dt_InterestRate = Class.SQLConnection.InputSQLMSSQL(
                            "SELECT a.LoanNo, a.TeacherNo, a.LoanAmount, ROUND(CAST(a.InterestRate / 100 * a.LoanAmount as float), 0) as InterestRate,a.PayNo \r\n" +
                            "FROM EmployeeBank.dbo.tblLoan as a \r\n" +
                            "WHERE a.LoanNo = '{LoanNo}'".Replace("{LoanNo}", DGV_Pay.Rows[x].Cells[3].Value.ToString()));
                            var Amount_Loan = dt_InterestRate.Rows[0][3].ToString();
                            var LoanNo = DGV_Pay.Rows[x].Cells[3].Value.ToString();
                            // Inset Bill
                            Loan += SQLDefault[8]
                            .Replace("{BillNo}", BillNo)
                            .Replace("{TypeNo}", "2")
                            .Replace("{LoanNo}", DGV_Pay.Rows[x].Cells[3].Value.ToString())
                            .Replace("{Amount}", DGV_Pay.Rows[x].Cells[2].Value.ToString())
                            .Replace("{Month}", DGV_Pay.Rows[x].Cells[5].Value.ToString())
                            .Replace("{Year}", DGV_Pay.Rows[x].Cells[4].Value.ToString())
                            .Replace("{BillDetailPaymentNo}", (CBPayment_Pay.SelectedIndex + 1).ToString());
                            // Update loan
                             Updategunratorloan += SQLDefault[9]
                              .Replace("{LoanNo}", DGV_Pay.Rows[x].Cells[3].Value.ToString())
                              .Replace("{LoanAmount}", DGV_Pay.Rows[x].Cells[2].Value.ToString())
                              .Replace("{TeacherNo}", TBTeacherNo.Text)
                              .Replace("{Amount}", Amount_Loan.ToString());

                        }
                    }
                    UpdateShare = SQLDefault[10]
                              .Replace("{TeacherNo}", TBTeacherNo.Text)
                              .Replace("{SavingAmount}", totalShareAmount.ToString());
                    List<string> GetScrip = new List<string> { Share, UpdateShare, Loan , Updategunratorloan };
                    SQL += Bill + "\r\n";
                    for (int Round = 0; Round < GetScrip.Count(); Round++)
                    {
                        if(GetScrip[Round].Length != 0)
                            SQL += GetScrip[Round];
                    }
                    Class.SQLConnection.InputSQLMSSQL(SQL);
                    // inofmation for Document
                    Class.Print.PrintPreviewDialog.info_name = TBTeacherName.Text;
                    Class.Print.PrintPreviewDialog.info_id = TBTeacherNo.Text;
                    Class.Print.PrintPreviewDialog.info_TeacherAdd = Class.UserInfo.TeacherName;
                    Class.Print.PrintPreviewDialog.info_datepayShare = DateTime.Today.Day.ToString() + '/' + DateTime.Today.Month.ToString() + '/' + DateTime.Today.Year.ToString();
                    Class.Print.PrintPreviewDialog.info_Billpay = TBTeacherBill.Text;
                    Class.Print.PrintPreviewDialog.info_Payment = CBPayment_Pay.Items[CBPayment_Pay.SelectedIndex].ToString();

                    DataTable dt = Class.SQLConnection.InputSQLMSSQL("SELECT a.TeacherNo, a.SavingAmount,SUM(b.RemainsAmount) \r\n" +
                   "FROM EmployeeBank.dbo.tblShare as a \r\n" +
                   "LEFT JOIN EmployeeBank.dbo.tblGuarantor as b on a.TeacherNo = b.TeacherNo \r\n" +
                   "WHERE a.TeacherNo = '{TeacherNo}' \r\n".Replace("{TeacherNo}", TBTeacherNo.Text) +
                   "GROUP BY  a.TeacherNo, a.SavingAmount");
                    Class.Print.PrintPreviewDialog.info_Savingtotel = dt.Rows[0][1].ToString();
                    if (dt.Rows[0][2].ToString() != "")
                    {
                        Class.Print.PrintPreviewDialog.info_Lona_AmountRemain = dt.Rows[0][2].ToString();
                    }
                    // Setsize paper
                    printDocument1.DefaultPageSettings.PaperSize = new PaperSize("A4", 595, 842);
                    printDocument1.DefaultPageSettings.Landscape = true;

                    Printbill = 1;
                    printDocument1.DocumentName = $"TeacherID{TBTeacherNo.Text}_Bill{TBTeacherBill.Text}"; // Name File

                    printDocument1.Print();

                    Printbill = 0;
                    CBYearSelection_ShareInfo.SelectedIndex = CBYearSelection_ShareInfo.SelectedIndex;
                    CBLoanSelection_LoanInfo.SelectedIndex = CBLoanSelection_LoanInfo.SelectedIndex;
                    CBYearSelection_BillInfo.SelectedIndex = CBYearSelection_BillInfo.SelectedIndex;

                    TBTeacherNo.Enabled = false;
                    BSearchTeacher.Enabled = true;
                    CBList_Pay.Enabled = false;
                    CBPayment_Pay.Enabled = false;
                    CBYearSelection_Pay.Enabled = false;
                    CBMonthSelection_Pay.Enabled = false;
                    BListAdd_Pay.Enabled = false;
                    BSave_Pay.Enabled = false;
                    TBAmount_Pay.Enabled = false;
                    BAutoSelection.Enabled = false;

                    CheckPay = true;
                    CheckSave = true;
                    if (CBYearSelection_BillInfo.Items.Count != 0)
                        CBYearSelection_Pay.SelectedIndex = CBYearSelection_Pay.SelectedIndex;
                    if (CBLoanSelection_LoanInfo.Items.Count != 0)
                        CBLoanSelection_LoanInfo.SelectedIndex = 0;
                    if (CBLoanSelection_LoanInfo.Items.Count != 0)
                        CBLoanSelection_LoanInfo_SelectedIndexChanged(new object(), new EventArgs());
                    if (CBYearSelection_ShareInfo.Items.Count != 0)
                        CBYearSelection_ShareInfo_SelectedIndexChanged(new object(), new EventArgs());
                    TBTeacherNo_KeyDown(sender, new KeyEventArgs(Keys.Enter)); // Reinfo
                }
                else if (!(BankTeacher.Bank.Pay.Calculator.Return))
                {
                    MessageBox.Show("การชำระล้มเหลว", "การเเจ้งเตือนการชำระ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        //Enable Buttion Save
        private void DGV_Pay_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (DGV_Pay.Rows.Count >= 1 && CBPayment_Pay.Items.Count != 0)
                BSave_Pay.Enabled = true;
            else if (DGV_Pay.Rows.Count == 0)
                BSave_Pay.Enabled = false;
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
                DataSet ds = BankTeacher.Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[12]
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

                    DGV_ShareInfo.Rows.Add(a + "/" + CBYearSelection_ShareInfo.SelectedItem.ToString(), BankTeacher.Bank.Menu.startAmountMin, "ยังไม่ได้ชำระ");
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

        public int CheckDecimalAndPlusOne(Double NumDouble)
        {
            String[] Check = NumDouble.ToString().Split('.');
            if (NumDouble % Convert.ToDouble(NumDouble) != 0)
            {
                return Convert.ToInt32(Check[0]) + 1;
            }
            else
            {
                return Convert.ToInt32(Check[0]);
            }
        }
        //============================== tabpage 3 (Loaninfo) ============================================
        //Select Loan
        private void CBLoanSelection_LoanInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            BankTeacher.Class.ComboBoxPayment Loan = (CBLoanSelection_LoanInfo.SelectedItem as BankTeacher.Class.ComboBoxPayment);
            if (CBLoanSelection_LoanInfo.SelectedIndex != -1)
            {
                DataSet ds = BankTeacher.Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[13].Replace("{LoanID}", Loan.No));
                DGV_LoanInfo.Rows.Clear();
                if (ds.Tables[0].Rows.Count != 0)
                {
                    int RemainAmount = 0;
                    for (int x = 0; x < ds.Tables[0].Rows.Count; x++)
                    {
                        RemainAmount += Convert.ToInt32(Convert.ToDouble(ds.Tables[0].Rows[x][9].ToString()));
                    }
                    Double Interest = CheckDecimalAndPlusOne(Convert.ToDouble(Convert.ToDouble(ds.Tables[0].Rows[0][8].ToString())) * (Convert.ToDouble(ds.Tables[0].Rows[0][7].ToString()) / 100));
                    TBTotal__LoanInfo.Text = (Convert.ToDouble(Convert.ToDouble(ds.Tables[0].Rows[0][8].ToString()) + (Convert.ToDouble(ds.Tables[0].Rows[0][8].ToString()) * Convert.ToDouble(ds.Tables[0].Rows[0][7].ToString()) / 100)).ToString());
                    TBAmountRemain_LoanInfo.Text = RemainAmount.ToString();
                    TBInteresrt_LoanInfo.Text = Interest.ToString();
                    TBStartAmount_LoanInfo.Text = Convert.ToInt32(ds.Tables[0].Rows[0][8].ToString()).ToString();
                    TBInstallment_LoanInfo.Text = ds.Tables[0].Rows[0][6].ToString();

                    int Month = Convert.ToInt32(ds.Tables[0].Rows[0][4].ToString());
                    int Year = Convert.ToInt32(ds.Tables[0].Rows[0][5].ToString());

                    Interest = Interest / Convert.ToDouble(ds.Tables[0].Rows[0][6].ToString());

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
                            Interest = Math.Ceiling((Convert.ToDouble(ds.Tables[0].Rows[0][8].ToString()) * (Convert.ToDouble(ds.Tables[0].Rows[0][7].ToString()) / 100)) - (Convert.ToInt32(Interest) * Num));
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
                            if (ds.Tables[1].Rows.Count == 0)
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
        int LINE = 0;
        private void CBYearSelect_BillInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            LINE = 0; timer1.Stop(); P1_X.Visible = false; P2_X.Visible = false; P2_Y.Visible = false; P1_Y.Visible = false;
            int a = CBYearSelection_BillInfo.SelectedIndex;
            if (CBYearSelection_BillInfo.SelectedIndex != -1)
            {
                DGV_Tester.Rows.Clear();
                DataTable dt = BankTeacher.Class.SQLConnection.InputSQLMSSQL(SQLDefault[14]
                .Replace("{TeacherNo}", TBTeacherNo.Text)
                .Replace("{Year}", CBYearSelection_BillInfo.Text));

                if (dt.Rows.Count != 0)
                {
                    String BillNo = "";
                    DGV_BillInfo.Rows.Clear();
                    int Sum = 0;
                    int Balance = 0;
                    int firstBill = 0;
                    int Line = 0;
                    int Amountsum = 0;
                    int Number = 1;
                    for (int x = 0; x < dt.Rows.Count; x++)
                    {
                        if (firstBill == 0)
                        {
                            DGV_BillInfo.Rows.Add(Number++, dt.Rows[x][0].ToString(), Convert.ToDateTime(dt.Rows[x][5].ToString()).ToString("dd-MM-yyyy"), dt.Rows[x][1].ToString(), dt.Rows[x][2].ToString(), dt.Rows[x][4].ToString(), dt.Rows[x][3].ToString());
                            BillNo = dt.Rows[x][0].ToString();
                            firstBill++;
                            Sum = 0;
                        }
                        else if (BillNo == dt.Rows[x][0].ToString())
                        {
                            DGV_BillInfo.Rows.Add($"", "", "", dt.Rows[x][1].ToString(), dt.Rows[x][2].ToString(), "", dt.Rows[x][3].ToString());
                            //DGV_BillInfo.Rows[x-2].DefaultCellStyle.BackColor = Color.LightGoldenrodYellow;
                        }
                        else if (BillNo != dt.Rows[x][0].ToString())
                        {
                            DGV_BillInfo.Rows.Add("", "", "", "รวม  ", "", "", Sum);
                            DGV_BillInfo.Rows[DGV_BillInfo.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Yellow;
                            x--;
                            //DGV_BillInfo.Rows[x].DefaultCellStyle.BackColor = Color.LightGoldenrodYellow;
                            firstBill--;
                            Sum = 0;
                            Line++;
                        }
                        Sum = Sum + Convert.ToInt32(dt.Rows[x][3].ToString());
                        if (Amountsum < dt.Rows.Count)
                            Balance += Convert.ToInt32(dt.Rows[Amountsum++][3].ToString());
                        if (x == dt.Rows.Count - 1)
                        {
                            DGV_BillInfo.Rows.Add("", "", "", "รวม  ", "", "", Sum);
                            DGV_BillInfo.Rows[DGV_BillInfo.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Yellow;
                            Sum = 0;
                            Line++;
                        }
                        if (x + Line < 10)
                            LINE = 25 * (x + 1 + Line);
                        else
                            LINE = 257;
                    }
                    for (int loop = 0; loop < DGV_BillInfo.Rows.Count; loop++)
                    {
                        if (DGV_BillInfo.Rows[loop].Cells[1].Value.ToString() != "")
                        {
                            DGV_BillInfo.Rows[loop].DefaultCellStyle.BackColor = Color.LightGoldenrodYellow;
                        }
                        else if (DGV_BillInfo.Rows[loop].Cells[3].Value.ToString() != "รวม  ")
                        {
                            DGV_BillInfo.Rows[loop].DefaultCellStyle.BackColor = Color.LightYellow;
                        }
                    }
                    LBalance_BillInfo.Text = Balance.ToString();
                }
                else
                {
                    CBYearSelection_Pay.Enabled = false;
                    CBMonthSelection_Pay.Enabled = false;
                    MessageBox.Show("ไม่พบรายการ", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

        }
        //==============================================================================================


        //============================== tabpage 5 (Cancel Bill) ============================================

        //======================================= Method ===============================================
        private void ReadonlyDGVPay()
        {
            if (DGV_Pay.Rows.Count != 0)
                for (int x = 0; x < DGV_Pay.Rows.Count; x++)
                {
                    DGV_Pay.Rows[x].Cells[2].ReadOnly = true;
                    if (DGV_Pay.Rows[x].Cells[1].Value.ToString().Contains("หุ้น"))
                        DGV_Pay.Rows[x].Cells[2].ReadOnly = false;
                }
        }

        //DGV Delete Rows
        private void Delete_Click(object sender, EventArgs e)
        {
            if (SelectIndexRow != -1)
            {
                String Year = DGV_Pay.Rows[SelectIndexRow].Cells[4].Value.ToString();
                String Month = DGV_Pay.Rows[SelectIndexRow].Cells[5].Value.ToString();
                DGV_Pay.Rows.RemoveAt(SelectIndexRow);
                CBYearSelection_Pay_SelectedIndexChanged(new object(), new EventArgs());
                SelectIndexRow = -1;
                //this.RestoreComboboxafterdelete();
                ReloadYearMonth(Year, Month);
                if (DGV_Pay.Rows.Count == 0)
                {
                    CBPayment_Pay.Enabled = false;
                    CBPayment_Pay.SelectedIndex = -1;
                }
            }
            if (DGV_Pay.Rows.Count != 0)
            {
                Summoney();
            }
            if (CBYearSelection_Pay.Items.Count != 0)
                if (CBYearSelection_Pay.SelectedIndex == -1)
                {
                    CBYearSelection_Pay.Enabled = true;
                    CBYearSelection_Pay.SelectedIndex = 0;
                }
            if (CBYearSelection_Pay.Items.Count != 0)
            {
                CBMonthSelection_Pay.Items.Clear();
                for (int x = 0; x < DM[CBYearSelection_Pay.SelectedIndex].Count; x++)
                {
                    CBMonthSelection_Pay.Items.Add(DM[CBYearSelection_Pay.SelectedIndex][x]);
                }
                CBMonthSelection_Pay.SelectedIndex = 0;
            }
        }

        //DGV Restore Values after Delete
        private void RestoreComboboxafterdelete()
        {
            ComboBox[] cb = new ComboBox[] { CBList_Pay };
            for (int x = 0; x < cb.Length; x++)
                cb[x].Items.Add(new BankTeacher.Class.ComboBoxPay(DGV_Pay.Rows[SelectIndexRow].Cells[1].Value.ToString(), DGV_Pay.Rows[SelectIndexRow].Cells[2].Value.ToString(),
                                            DGV_Pay.Rows[SelectIndexRow].Cells[3].Value.ToString()));
            CBList_Pay.SelectedIndex = 0;
        }
        private void ReloadYearMonth(String Year, String Month)
        {
            /// เหลือ Remove แล้วยัดเข้าไปใส่โดยใช้  DGV  Behide เทียบกับ DGV Pay หาก ให้เท่ากับค่าตอนแรกแต่ไม่ต้องเอาค่าของ DGV Pay ยัดลงไป
            //Fix Formation : Month/Year
            //Ex: 1/2022
            List<String> NotFoundInDGV = new List<string>();
            for (int CountbehideDGV = 0; CountbehideDGV < DGV_Behidepay.Rows.Count; CountbehideDGV++)
            {
                if (DGV_Pay.Rows.Count != 0)
                {
                    for (int CountDGVPay = 0; CountDGVPay < DGV_Pay.Rows.Count; CountDGVPay++)
                    {
                        if (DGV_Behidepay.Rows[CountbehideDGV].Cells[3].Value.ToString() == DGV_Pay.Rows[CountDGVPay].Cells[3].Value.ToString() &&
                            DGV_Behidepay.Rows[CountbehideDGV].Cells[4].Value.ToString() == DGV_Pay.Rows[CountDGVPay].Cells[4].Value.ToString() &&
                            DGV_Behidepay.Rows[CountbehideDGV].Cells[5].Value.ToString() == DGV_Pay.Rows[CountDGVPay].Cells[5].Value.ToString())
                        {
                            break;
                        }
                        else if (CountDGVPay == DGV_Pay.Rows.Count - 1)
                        {
                            NotFoundInDGV.Add(DGV_Behidepay.Rows[CountbehideDGV].Cells[5].Value.ToString() + '/' + DGV_Behidepay.Rows[CountbehideDGV].Cells[4].Value.ToString());
                        }
                    }
                }
                else
                {
                    NotFoundInDGV.Add(DGV_Behidepay.Rows[CountbehideDGV].Cells[5].Value.ToString() + '/' + DGV_Behidepay.Rows[CountbehideDGV].Cells[4].Value.ToString());
                }
            }
            //String[] SplitNotFound = NotFoundInDGV[0].Split('/');
            //CBYearSelection_Pay.Items.Add(SplitNotFound[1]);
            List<int> PosYear = new List<int>();
            for (int CountNotFoundINDGV = 0; CountNotFoundINDGV < NotFoundInDGV.Count; CountNotFoundINDGV++)
            {
                String[] SplitNotFound = NotFoundInDGV[CountNotFoundINDGV].Split('/');
                for (int CountYearInCB = 0; CountYearInCB < YearinCB.Count; CountYearInCB++)
                {
                    if (SplitNotFound[1] == YearinCB[CountYearInCB].ToString())
                    {
                        if (PosYear.Count != 0)
                        {
                            for (int CountPosYear = 0; CountPosYear < PosYear.Count; CountPosYear++)
                            {
                                for (int CountPosYear2 = 0; CountPosYear2 < PosYear.Count; CountPosYear2++)
                                {
                                    if (PosYear[CountPosYear] == PosYear[CountPosYear2])
                                    {
                                        break;
                                    }
                                    else if (CountPosYear2 == PosYear.Count - 1)
                                    {
                                        PosYear.Add(CountYearInCB);
                                    }
                                }
                            }
                            break;
                        }
                        else
                        {
                            if (YearinCB[CountYearInCB].ToString() == Year.ToString())
                            {
                                PosYear.Add(CountYearInCB);
                                break;
                            }
                        }
                    }
                }
            }

            if (PosYear.Count != 0)
            {
                if (CBYearSelection_Pay.Items.Count != 0)
                {
                    for (int x = 0; x < CBYearSelection_Pay.Items.Count; x++)
                    {
                        if (CBYearSelection_Pay.Items[x].ToString() == Year)
                        {
                            break;
                        }
                        else if (x == CBYearSelection_Pay.Items.Count - 1)
                        {
                            for (int CountPosYear = 0; CountPosYear < PosYear.Count; CountPosYear++)
                            {
                                if (DM.Count != 0)
                                {
                                    try
                                    {
                                        DM.Insert(PosYear[CountPosYear], new List<int>());
                                        CBYearSelection_Pay.Items.Add(Year);
                                    }
                                    catch
                                    {
                                        DM.Add(new List<int>());
                                        PosYear[0] = DM.Count - 1;
                                        CBYearSelection_Pay.Items.Add(Year);
                                    }
                                }
                                else
                                {
                                    DM.Add(new List<int>());
                                    PosYear[0] = DM.Count - 1;
                                    CBYearSelection_Pay.Items.Add(Year);
                                }
                            }
                            break;
                        }
                    }
                }
                else
                {
                    for (int CountPosYear = 0; CountPosYear < PosYear.Count; CountPosYear++)
                    {
                        if (DM.Count != 0)
                        {
                            try
                            {
                                DM.Insert(PosYear[CountPosYear], new List<int>());
                                CBYearSelection_Pay.Items.Add(Year);
                            }
                            catch
                            {
                                DM.Insert(DM.Count + 1, new List<int>());
                                CBYearSelection_Pay.Items.Add(Year);
                            }
                        }
                        else
                        {
                            DM.Add(new List<int>());
                            CBYearSelection_Pay.Items.Add(Year);
                        }
                    }
                }
            }
            if (PosYear.Count != 0)
            {
                for (int CountPosYear = 0; CountPosYear < PosYear.Count; CountPosYear++)
                {
                    if (DM[0].Count == 0)
                    {
                        DM[0].Add(Convert.ToInt32(Month));
                        DM[0].Sort();
                    }
                    else if (DM.Count < PosYear[CountPosYear])
                    {
                        for (int x = 0; x < DM[PosYear[CountPosYear]].Count; x++)
                        {
                            if (DM[PosYear[CountPosYear]][x].ToString() == Month.ToString())
                                break;
                            else if (x == DM[PosYear[CountPosYear]].Count - 1)
                            {
                                DM[DM.Count - 1].Add(Convert.ToInt32(Month));
                                DM[DM.Count - 1].Sort();
                                break;
                            }
                        }
                    }
                    else
                    {
                        if (DM[PosYear[CountPosYear]].Count != 0)
                        {
                            for (int x = 0; x < DM[PosYear[CountPosYear]].Count; x++)
                            {
                                if (DM[PosYear[CountPosYear]][x].ToString() == Month.ToString())
                                    break;
                                else if (x == DM[PosYear[CountPosYear]].Count - 1)
                                {
                                    DM[PosYear[CountPosYear]].Add(Convert.ToInt32(Month));
                                    DM[PosYear[CountPosYear]].Sort();
                                    break;
                                }
                            }
                        }
                        else
                        {
                            DM[PosYear[CountPosYear]].Add(Convert.ToInt32(Month));
                            DM[PosYear[CountPosYear]].Sort();
                            break;
                        }
                    }
                }
            }

            CBYearSelection_Pay.Enabled = true;
        }

        //คำนวนยอดทั้งหมดใน DGV ลง label
        private void Summoney()
        {
            int sum = 0;
            for (int x = 0; x < DGV_Pay.Rows.Count; x++)
            {
                sum += Convert.ToInt32(DGV_Pay.Rows[x].Cells[2].Value.ToString());
            }
            LBalance_Pay.Text = sum.ToString();
        }

        //Cleartabpage 1
        private void Cleartabpage1()
        {
            //tabpage 1 (Pay) ===================================================CBYearSelection_BillInfo.DroppedDown = false
            CBYearSelection_Pay.DroppedDown = false;
            CBMonthSelection_Pay.DroppedDown = false;
            CBList_Pay.DroppedDown = false;
            CBPayment_Pay.DroppedDown = false;
            DGV_Printbypoon.Rows.Clear();
            DGV_Pay.Rows.Clear();
            DGV_Behidepay.Rows.Clear();
            CBYearSelection_Pay.SelectedIndex = -1;
            CBMonthSelection_Pay.SelectedIndex = -1;
            Num = 0;
            CBYearSelection_Pay.Items.Clear();
            CBMonthSelection_Pay.Items.Clear();
            CBList_Pay.SelectedIndex = -1;
            CBList_Pay.Items.Clear();
            TBAmount_Pay.Clear();
            LBalance_Pay.Text = "0";
            BAutoSelection.Enabled = false; ;
            //===================================================================
        }

        //Cleartabpage 2
        private void Cleartabpage2()
        {
            //tabpage 2 (ShareInfo) =============================================
            CBYearSelection_ShareInfo.DroppedDown = false;
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
            CBLoanSelection_LoanInfo.DroppedDown = false;
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
            CBYearSelection_BillInfo.DroppedDown = false;
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

            //DTP Enable
            if (DGV_Pay.Rows.Count == 0)
            {
                bool Checked = false;
                if (BankTeacher.Bank.Menu.DateAmountChange == 1)
                    Checked = true;
                DTPDate.Enabled = Checked;
            }
        }

        private void CBPapersize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CBPapersize.SelectedItem.ToString() == "A4")
            {
                printDocument1.DefaultPageSettings.PaperSize = new PaperSize("A4", 794, 1123);
                printDocument1.DefaultPageSettings.Landscape = false;
            }
            else
            {
                //printDocument1.DefaultPageSettings.PaperSize = new PaperSize("A5",420,595);
                printDocument1.DefaultPageSettings.PaperSize = new PaperSize("A4", 595, 842);
                printDocument1.DefaultPageSettings.Landscape = true;
            }
        }

        private void BT_Print_Click(object sender, EventArgs e)
        {
            if (DGV_BillInfo.RowCount != 0)
            {
                //420 x 595 A5  794 x 1123 A4
                //printDocument1.DefaultPageSettings.PaperSize = new PaperSize("A4",x,y);

                if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                {
                    printDocument1.Print();
                }
            }
            else
            {
                MessageBox.Show("ดูเหมือนคุณจะลืมอะไรนะ");
            }
        }

        private void RemoveComboboxhAfterAdd()
        {
            if (CBMonthSelection_Pay.SelectedIndex != -1 && CBYearSelection_Pay.SelectedIndex != -1)
            {
                if (CBList_Pay.Items.Count == 0)
                {
                    if (CBList_Pay.Items.Count != 0)
                        CBList_Pay.Items.RemoveAt(CBList_Pay.SelectedIndex);
                    if (CBMonthSelection_Pay.Items.Count != 0 && CBYearSelection_Pay.Items.Count != 0)
                    {
                        DM[CBYearSelection_Pay.SelectedIndex].RemoveAt(CBMonthSelection_Pay.SelectedIndex);
                        CBMonthSelection_Pay.Items.RemoveAt(CBMonthSelection_Pay.SelectedIndex);
                        if (CBMonthSelection_Pay.Items.Count != 0)
                            CBMonthSelection_Pay.SelectedIndex = 0;
                        else
                        {
                            DM.RemoveAt(CBYearSelection_Pay.SelectedIndex);
                            CBYearSelection_Pay.Items.RemoveAt(CBYearSelection_Pay.SelectedIndex);
                            if (CBYearSelection_Pay.Items.Count != 0)
                            {
                                CBYearSelection_Pay.SelectedIndex = 0;
                                CBYearSelection_Pay_SelectedIndexChanged(new object(), new EventArgs());
                            }
                            if (CBMonthSelection_Pay.Items.Count != 0)
                            {
                                CBMonthSelection_Pay.SelectedIndex = 0;
                            }
                            else
                            {
                                CBYearSelection_Pay.Enabled = false;
                                CBMonthSelection_Pay.Enabled = false;
                                if (Convert.ToInt32(TBAmount_Pay.Text) != 0)
                                    TBAmount_Pay.Text = "0";
                                BAutoSelection.Enabled = false;
                                BListAdd_Pay.Enabled = false;
                                CBList_Pay.Enabled = false;
                                TBAmount_Pay.Enabled = false;
                                //MessageBox.Show("ไม่พบรายการให้ชำระเพิ่มแล้ว", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                    else if (CBYearSelection_Pay.Items.Count != 0)
                    {
                        CBYearSelection_Pay.SelectedIndex = 0;
                        CBYearSelection_Pay_SelectedIndexChanged(new object(), new EventArgs());
                        if (CBMonthSelection_Pay.Items.Count != 0)
                        {
                            CBMonthSelection_Pay.SelectedIndex = 0;
                        }
                        else
                        {
                            CBYearSelection_Pay.Enabled = false;
                            CBMonthSelection_Pay.Enabled = false;
                            if (Convert.ToInt32(TBAmount_Pay.Text) != 0)
                                TBAmount_Pay.Text = "0";
                            BAutoSelection.Enabled = false;
                            BListAdd_Pay.Enabled = false;
                            CBList_Pay.Enabled = false;
                            //MessageBox.Show("ไม่พบรายการให้ชำระเพิ่มแล้ว", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        CBYearSelection_Pay.Enabled = false;
                        CBMonthSelection_Pay.Enabled = false;
                        if (Convert.ToInt32(TBAmount_Pay.Text) != 0)
                            TBAmount_Pay.Text = "0";
                        BAutoSelection.Enabled = false;
                        BListAdd_Pay.Enabled = false;
                        CBList_Pay.Enabled = false;
                        //MessageBox.Show("ไม่พบรายการให้ชำระเพิ่มแล้ว", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            
            if (CB_SelectPrint.SelectedIndex == 0 && Printbill != 1)
            {
                Class.Print.PrintPreviewDialog.Detailspayment(e, DGV_BillInfo, "รายละเอียดบิลรายบุลคล", AccessibilityObject.Name);
            }
            else if (CB_SelectPrint.SelectedIndex == 1 && Printbill != 1)
            {
                Class.Print.PrintPreviewDialog.PrintReportGrid(e, DGV_Tester, "ใบเสร็จรับเงิน(ย้อนหลัง)", this.AccessibilityObject.Name, checkBox_scrip.Checked, checkBox_copy.Checked, "A5", 0);
            }
            else
            {
                Class.Print.PrintPreviewDialog.PrintReportGrid(e, DGV_Printbypoon, "ใบเสร็จรับเงิน", this.AccessibilityObject.Name, true, true, "A5", 0);
            }

            Class.Print.PrintPreviewDialog.details = 0;
        }
        private static void NumericCheck(object sender, KeyPressEventArgs e)
        {
            DataGridViewTextBoxEditingControl s = sender as DataGridViewTextBoxEditingControl;
            if (s != null && (e.KeyChar == '.'))
            {
                e.KeyChar = System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator[0];
                e.Handled = s.Text.Contains(e.KeyChar);
            }
            else
                e.Handled = !char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar);
        }
        // คลิ๊กเพื่อปริ้นข้อมูลย้อนหลัง
        private void DGV_BillInfo_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                int Rows = e.RowIndex;
                string Text = DGV_BillInfo.Rows[Rows].Cells[1].Value.ToString();
                timer1.Stop(); P1_X.Visible = P2_X.Visible =  P2_Y.Visible = P1_Y.Visible = false;
                if (Text == "")
                {
                    do
                    {
                        Rows--;
                        Text = DGV_BillInfo.Rows[Rows].Cells[1].Value.ToString();
                    } while (Text == "");
                }
                if (Text != "")
                {
                    DGV_BillInfo.CurrentCell = DGV_BillInfo[1, Rows];
                    TB_Bill.Text = Text;
                    BTPrint.Enabled = true;
                    BTPrint.BackColor = Color.White;
                    Class.Print.PrintPreviewDialog.info_name = TBTeacherName.Text;
                    Class.Print.PrintPreviewDialog.info_id = TBTeacherNo.Text;
                    Class.Print.PrintPreviewDialog.info_Savingtotel = TBToatalSaving_ShareInfo.Text;
                    //Class.Print.PrintPreviewDialog.info_Lona_AmountRemain = TBAmountRemain_LoanInfo.Text;
                    DataTable dt_infoAmount = Class.SQLConnection.InputSQLMSSQL("SELECT a.TeacherNo, a.SavingAmount,SUM(b.RemainsAmount) \r\n" +
                    "FROM EmployeeBank.dbo.tblShare as a \r\n" +
                    "LEFT JOIN EmployeeBank.dbo.tblGuarantor as b on a.TeacherNo = b.TeacherNo \r\n" +
                    "WHERE a.TeacherNo = '{TeacherNo}' \r\n".Replace("{TeacherNo}", TBTeacherNo.Text) +
                    "GROUP BY  a.TeacherNo, a.SavingAmount");
                    if(dt_infoAmount.Rows[0][2].ToString() != "")
                    {
                        Class.Print.PrintPreviewDialog.info_Lona_AmountRemain = Convert.ToInt32(dt_infoAmount.Rows[0][2]).ToString("N0");
                    }
                    Class.Print.PrintPreviewDialog.info_Billpay = DGV_BillInfo.Rows[Rows].Cells[1].Value.ToString();
                    DataTable dt_date = Class.SQLConnection.InputSQLMSSQL(SQLDefault[16].Replace("{Bill}", DGV_BillInfo.Rows[Rows].Cells[1].Value.ToString()));
                    Class.Print.PrintPreviewDialog.info_datepayShare = dt_date.Rows[0][1].ToString();
                    DGV_Tester.Rows.Clear();
                    DataTable dt = Class.SQLConnection.InputSQLMSSQL(SQLDefault[15].Replace("{bill}", DGV_BillInfo.Rows[Rows].Cells[1].Value.ToString()));
                    Class.Print.PrintPreviewDialog.info_TeacherAdd = dt.Rows[0][2].ToString();
                    Class.Print.PrintPreviewDialog.info_Payment = dt.Rows[0][6].ToString(); ;
                    for (int Row = 0; Row < dt.Rows.Count; Row++)
                    {
                        DGV_Tester.Rows.Add(Row + 1, dt.Rows[Row][5].ToString(), dt.Rows[Row][4].ToString(), dt.Rows[Row][3]);
                    }
                }
            }
        }
        // เปลี่ยนรูปเเบบตามต้นฉบับ
        private void DGV_Pay_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            if (DGV_Printbypoon.Rows.Count != 0)
            {
                DGV_Printbypoon.Rows.RemoveAt(e.RowIndex);
                DGV_Printbypoon.Rows.Clear();
                for (int r = 0; r < DGV_Pay.RowCount; r++)
                {
                    DGV_Printbypoon.Rows.Add(r + 1, DGV_Pay.Rows[r].Cells[0].Value.ToString(),
                        DGV_Pay.Rows[r].Cells[1].Value.ToString(), DGV_Pay.Rows[r].Cells[2].Value.ToString(), DGV_Pay.Rows[r].Cells[3].Value.ToString(),
                        CBYearSelection_Pay.Text, CBMonthSelection_Pay.Text); ;
                }
            }
            if (DGV_Pay.Rows.Count == 0)
            {
                bool Checked = false;
                if (BankTeacher.Bank.Menu.DateAmountChange == 1)
                    Checked = true;
                DTPDate.Enabled = Checked;
            }
        }
        // เพิ่มความมูลตามต้นฉบับ
        static int Num = 1;
        private void DGV_Pay_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (DGV_Printbypoon.RowCount == 0)
            {
                Num = 1;
            }
            if (Notadd)
            {
                DGV_Printbypoon.Rows.Add(Num++, DGV_Pay.Rows[e.RowIndex].Cells[0].Value.ToString(), CBList_Pay.Text, TBAmount_Pay.Text, DGV_Pay.Rows[e.RowIndex].Cells[3].Value.ToString(), CBYearSelection_Pay.Text, CBMonthSelection_Pay.Text);
            }

            if (DGV_Pay.Rows.Count != 0)
            {
                DTPDate.Enabled = false;
            }
        }
        private void BTPrint_Click(object sender, EventArgs e)
        {
            if (DGV_BillInfo.RowCount != 0)
            {
                if (CB_SelectPrint.SelectedIndex == 0)
                {
                    printDocument1.DefaultPageSettings.PaperSize = new PaperSize("Letter", 850, 1100);
                    printDocument1.DefaultPageSettings.Landscape = false;
                    if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                    {
                        printDocument1.Print();
                    }
                }
                else
                {
                    if (DGV_Tester.Rows.Count != 0)
                    {
                        printDocument1.DefaultPageSettings.PaperSize = new PaperSize("A4", 595, 842);
                        printDocument1.DefaultPageSettings.Landscape = true;
                        if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                        {
                            printDocument1.Print();
                        }
                    }
                    else
                    {
                        P1_Y.Size = new Size(5, LINE);
                        P2_Y.Size = new Size(5, LINE);
                        P2_X.Location = new Point(2, 106 + LINE);
                        P1_X.Visible =  P2_X.Visible = P2_Y.Visible = P1_Y.Visible = true;
                        timer1.Start(); MessageBox.Show("โปรดเลือกรายการในตาราง สำหรับ การปริ้นเออกสารย้อนหลัง", "การเเจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("ไม่พบรายการในตาราง", "การเเจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        int pus = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            pus++;
            if (pus % 2 == 1)
            {
                System.Threading.Thread.Sleep(200);
                P1_X.Visible = P2_X.Visible = P2_Y.Visible = P1_Y.Visible = false;
            }
            else
            {
                System.Threading.Thread.Sleep(200);
                P1_X.Visible = P2_X.Visible = P2_Y.Visible = P1_Y.Visible = true;
            }
            if (pus >= 20)
            {
                System.Threading.Thread.Sleep(200);
                P1_X.Visible = P2_X.Visible = P2_Y.Visible = P1_Y.Visible = false;
                pus = 0;
                timer1.Stop();
            }

        }

        private void BExitForm_Click(object sender, EventArgs e)
        {
            BankTeacher.Class.FromSettingMedtod.ReturntoHome(this);
        }

        private void pay_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape || (CheckSave && e.KeyCode == Keys.Enter))
            {
                if (TBTeacherNo.Text.Length != 0)
                {
                    CBPayment_Pay.SelectedIndex = -1;
                    ClearForm();
                    TBTeacherNo.Text = "";
                    TBTeacherName.Text = "";
                    TBTeacherBill.Text = "";
                    BSearchTeacher.Enabled = true;
                    TBTeacherNo.Enabled = true;
                    CBLoanSelection_LoanInfo.Enabled = false;
                    CBMonthSelection_Pay.Enabled = false;
                    CBList_Pay.Enabled = false;
                    TBTeacherNo.Enabled = true;
                    CheckSave = false;
                    TBAmount_Pay.Enabled = false;
                    BListAdd_Pay.Enabled = false;
                    //RemoveClickEvent(CBYearSelection_BillInfo);

                }
                else
                {
                    BExitForm_Click(new object(), new EventArgs());
                }
            }
        }

        private void button1_MouseMove(object sender, MouseEventArgs e)
        {
            if (BT_Open.Text != "ปิด")
                if (e.X >= 1)
                {
                    flowLayoutPanel1.Visible = true;
                }
                else
                {
                    flowLayoutPanel1.Visible = false;
                }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (flowLayoutPanel1.Visible)
            {
                BT_Open.Text = "ปิด";
                flowLayoutPanel1.Visible = false;
            }
            else
            {
                BT_Open.Text = "เปิด";
                flowLayoutPanel1.Visible = true;
            }

        }
        private Point MouseD;
        private void flowLayoutPanel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (flowLayoutPanel1.Location.Y >= 1 && flowLayoutPanel1.Location.X >= 1 && flowLayoutPanel1.Location.Y <= 330 && flowLayoutPanel1.Location.X <= 520)
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    flowLayoutPanel1.Left = e.X + flowLayoutPanel1.Left - MouseD.X;
                    flowLayoutPanel1.Top = e.Y + flowLayoutPanel1.Top - MouseD.Y;
                }
            }
            else if (flowLayoutPanel1.Location.Y <= 0)
                flowLayoutPanel1.Location = new Point(flowLayoutPanel1.Left, 2);
            else if (flowLayoutPanel1.Location.Y >= 330)
                flowLayoutPanel1.Location = new Point(flowLayoutPanel1.Left, 328);
            else if (flowLayoutPanel1.Location.X <= 0)
                flowLayoutPanel1.Location = new Point(2, flowLayoutPanel1.Top);
            else if (flowLayoutPanel1.Location.X >= 520)
                flowLayoutPanel1.Location = new Point(518, flowLayoutPanel1.Top);
        }
        private void flowLayoutPanel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (flowLayoutPanel1.Location.Y >= 1)
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    MouseD = e.Location;
                }
            }
        }
        private void TB_Bill_TextChanged(object sender, EventArgs e)
        {
            DGV_Tester.Rows.Clear();
            try
            {
                Class.Print.PrintPreviewDialog.info_name = TBTeacherName.Text;
                Class.Print.PrintPreviewDialog.info_id = TBTeacherNo.Text;
                Class.Print.PrintPreviewDialog.info_Savingtotel = TBToatalSaving_ShareInfo.Text;
                Class.Print.PrintPreviewDialog.info_Lona_AmountRemain = TBAmountRemain_LoanInfo.Text;
                Class.Print.PrintPreviewDialog.info_Billpay = TB_Bill.Text;
                DataTable dt_date = Class.SQLConnection.InputSQLMSSQL(SQLDefault[16].Replace("{Bill}", TB_Bill.Text));
                Class.Print.PrintPreviewDialog.info_datepayShare = dt_date.Rows[0][1].ToString();
                DGV_Tester.Rows.Clear();
                DataTable dt = Class.SQLConnection.InputSQLMSSQL(SQLDefault[15].Replace("{bill}", TB_Bill.Text));
                Class.Print.PrintPreviewDialog.info_TeacherAdd = dt.Rows[0][2].ToString();
                Class.Print.PrintPreviewDialog.info_Payment = dt.Rows[0][6].ToString(); ;
                for (int Row = 0; Row < dt.Rows.Count; Row++)
                {
                    DGV_Tester.Rows.Add(Row + 1, dt.Rows[Row][5].ToString(), dt.Rows[Row][4].ToString(), dt.Rows[Row][3]);
                }
            }
            catch { }
        }

        private void TB_Bill_Click(object sender, EventArgs e)
        {
            if (TB_Bill.Text == "เลขบิลล์")
                TB_Bill.Text = "";
        }

        private void CB_SelectPrint_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CB_SelectPrint.SelectedIndex == 1)
            {
                checkBox_scrip.Enabled = true;
                checkBox_copy.Enabled = true;
            }
            else
            {
                checkBox_scrip.Enabled = false;
                checkBox_copy.Enabled = false;
            }
        }
        void CheckInsertDGVBehidePay()
        {
            BankTeacher.Class.ComboBoxPay Loan = (CBList_Pay.SelectedItem as BankTeacher.Class.ComboBoxPay);
            String Time = CBYearSelection_Pay.Text + "/" + CBMonthSelection_Pay.Text;
            if (DGV_Behidepay.Rows.Count != 0)
                for (int xx = 0; xx < DGV_Behidepay.Rows.Count; xx++)
                {
                    if (DGV_Behidepay.Rows[xx].Cells[3].Value.ToString() == Loan.No && DGV_Behidepay.Rows[xx].Cells[4].Value.ToString() == CBYearSelection_Pay.Text && DGV_Behidepay.Rows[xx].Cells[5].Value.ToString() == CBMonthSelection_Pay.Text)
                        break;
                    else if (xx == DGV_Behidepay.Rows.Count - 1)
                        DGV_Behidepay.Rows.Add(Time, CBList_Pay.Text, TBAmount_Pay.Text, Loan.No, CBYearSelection_Pay.Text, CBMonthSelection_Pay.Text);
                }
            else
                DGV_Behidepay.Rows.Add(Time, CBList_Pay.Text, TBAmount_Pay.Text, Loan.No, CBYearSelection_Pay.Text, CBMonthSelection_Pay.Text);
        }

        private void pay_KeyUp(object sender, KeyEventArgs e)
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
        private void TBAmount_Pay_TextChanged(object sender, EventArgs e)
        {
            BankTeacher.Class.FromSettingMedtod.ProtectedCtrlVTB(TBAmount_Pay);
        }

        private void DTPDate_ValueChanged(object sender, EventArgs e)
        {
            TBAmount_Pay.Text = "0";
            if (CBMonthSelection_Pay.Enabled)
                if (CBMonthSelection_Pay.Items.Count != 0)
                    CBMonthSelection_Pay_SelectedIndexChanged(new object(), new EventArgs());
        }
    }
}

