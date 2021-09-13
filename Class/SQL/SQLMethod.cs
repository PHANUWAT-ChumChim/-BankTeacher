using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;
using System.Windows.Forms;
using System.Drawing;

namespace example.Class
{
    class SQLMethod
    {

        /// <summary>
        /// SQLDafault 
        /// <para> [0] Write to Search ID Teacher INPUT: {TeacherNo} MemberStatus </para> 
        /// <para> [1] INSERT Register Member INPUT:  {TeacherNo} {TeacherAddBy} {StartAmount} {DocPath} </para>
        /// <para> [2] Search LoanMember INPUT: {TeacherNo} แก้ด้วยแก้ด้วยแก้ด้วยแก้ด้วยแก้ด้วยแก้ด้วยแก้ด้วยแก้ด้วยแก้ด้วยแก้ด้วยแก้ด้วยแก้ด้วยแก้ด้วยแก้ด้วยแก้ด้วยแก้ด้วยแก้ด้วยแก้ด้วยแก้ด้วย </para>
        /// <para> [3]  AmountpayANDAmountLoanINMonth INPUT: {TeacherNo}  </para>
        /// <para> [4] CheckAmount INPUT: -  </para>
        /// <para> [5] INSERT Bill and BillDetail INPUT: {TeacherAddBy} {TeacherNo} {TypeNo} {LoanNo} {Amount} {Mount} {Year}</para>
        /// <para> [6] SELECT Loan INPUT : {TeacherNo} </para>
        /// <para> [7] UPDATE SavingAmount INPUT: {TeacherNo} {Amount} </para>
        /// <para> [8] UPDATE REMAIN INPUT: {TeacherNo} {Amount} </para>
        /// <para> [9] Check Time Server INPUT: - </para>
        /// <para> [10] Change Status Member INPUT: {TeacherNoAddBy} {TeacherNo} {Note} {DocStatusNo} {DocUploadPath} {Status} {TeacherNo}</para>
        /// </para>[11] CheckAmountpayINPUT: {TeacherNo}  </para>
       
        /// </summary>
        private static String[] SQLDefault = new String[]
         { 

          //[0] Write to Search & Search All ID Teacher INPUT: {TeacherNo} {MemberStatus}
          "SELECT a.TeacherNo,CAST(c.PrefixName+' '+[Fname] +' '+ [Lname] as NVARCHAR),[IdNo]  \r\n " +
          "FROM[Personal].[dbo].[tblTeacherHis] as a  \r\n " +
          "LEFT JOIN Personal.dbo.tblGroupPosition as b ON a.GroupPositionNo = b.GroupPositionNo \r\n " +
          "LEFT JOIN BaseData.dbo.tblPrefix as c ON c.PrefixNo = a.PrefixNo \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblMember as d ON a.TeacherNo = d.TeacherNo \r\n " +
          "WHERE a.TeacherNo LIKE 'T{TeacherNo}%' and {d.MemberStatusNo = '{MemberStatus}}' \r\n " +
          "ORDER BY a.TeacherNo;  "

          ,
         //[1] INSERT Register Member INPUT:  {TeacherNo} {TeacherAddBy} {StartAmount} {DocPath}
          "INSERT INTO EmployeeBank.dbo.tblMember(TeacherNo,TeacherAddBy,StartAmount,DocUploadPath,DateAdd) \r\n " +
          "VALUES('{TeacherNo}','{TeacherAddBy}',{StartAmount},'{DocPath}',CURRENT_TIMESTAMP); "
          ,     
          //[2] Search LoanMember INPUT: {TeacherNo} แก้ด้วย
          "SELECT Mb.TeacherNo , CAST(p.PrefixName+' '+[Fname] +' '+ [Lname] as NVARCHAR)AS Name, Th.IdNo AS TeacherID,  \r\n " +
          " Th.TeacherLicenseNo,Th.IdNo AS IDNo,Th.TelMobile,d.LoanNo, \r\n " +
          " Ls.LoanStatusName,Mb.StartAmount,f.SavingAmount,CAST(Ms.MemberStatusName as nvarchar) AS UserStatususing  \r\n " +
          " FROM EmployeeBank.dbo.tblMember as Mb  \r\n " +
          " LEFT JOIN Personal.dbo.tblTeacherHis as Th ON Mb.TeacherNo = Th.TeacherNo  \r\n " +
          " LEFT JOIN BaseData.dbo.tblPrefix as p ON Th.PrefixNo = p.PrefixNo  \r\n " +
          " LEFT JOIN EmployeeBank.dbo.tblLoan as d ON Mb.TeacherNo = d.TeacherNo  \r\n " +
          " LEFT JOIN EmployeeBank.dbo.tblLoanStatus as Ls ON d.LoanStatusNo = Ls.LoanStatusNo  \r\n " +
          " LEFT JOIN EmployeeBank.dbo.tblShare as f ON Mb.TeacherNo = f.TeacherNo  \r\n " +
          " INNER JOIN EmployeeBank.dbo.tblMemberStatus as Ms on Mb.MemberStatusNo = Ms.MemberStatusNo  \r\n " +
          " WHERE Mb.TeacherNo LIKE 'T{TeacherNo}%' and Mb.MemberStatusNo = 1  \r\n " +
          " ORDER BY Mb.TeacherNo; "

          ,
          //[3] AmountpayANDAmountLoanINMonth INPUT: {TeacherNo}
          "SELECT Mb.TeacherNo,Mb.StartAmount AS  Amountpay,Mb.DateAdd AS Datepay,Lp.Amount AS AmountLoan,Ln.DateAdd AS DateLoan \r\n"+
          "FROM EmployeeBank.dbo.tblMember as Mb\r\n"+
          "LEFT JOIN EmployeeBank.dbo.tblLoan as Ln on Mb.TeacherNo = Ln.TeacherNo\r\n"+
          "LEFT JOIN EmployeeBank.dbo.tblLoanPay as Lp  on Ln.TeacherNo = Lp.TeacherNo\r\n" +
          "WHERE Mb.TeacherNo = '{TeacherNo}';"
          ,
          //[4] CheckAmount INPUT: -
           "SELECT Bi.TeacherNo,Bi.BillNo,Bd.BillDetailNo,TeacherNoAddBy,TeacherNo,Cancel,Bd.Date,CAST(Bt.TypeName as nvarchar),Bd.Amount \r\n"+
           "FROM EmployeeBank.dbo.tblBill AS Bi\r\n"+
           "LEFT JOIN EmployeeBank.dbo.tblBillDetail As Bd on Bi.BillNo = Bd.BillNo\r\n"+
           "LEFT JOIN EmployeeBank.dbo.tblBillDetailType As Bt on Bd.TypeNo = Bt.TypeNo\r\n"+
           "WHERE   Bt.TypeName IS NULL AND Bd.Date IS NULL AND bd.Amount = 0;"
           ,
          //[5] INSERT Bill and BillDetail INPUT: {TeacherAddBy} {TeacherNo} {TypeNo} {LoanNo} {Amount} {Mount} {Year}
          "DECLARE @BillNo INT; \r\n " +
          "DECLARE @TeacherNo VARCHAR(20); \r\n " +
          "DECLARE @TeacherNoAddBy VARCHAR (20); \r\n " +
          " \r\n " +
          "SET @TeacherNoAddBy = '{TeacherAddBy}'; \r\n " +
          "SET @TeacherNo = '{TeacherNo}'; \r\n " +
          " \r\n " +
          "INSERT INTO EmployeeBank.dbo.tblBill (TeacherNo, TeacherNoAddBy , DateAdd) \r\n " +
          "VALUES (@TeacherNo, @TeacherNoAddBy , CURRENT_TIMESTAMP); \r\n " +
          " \r\n " +
          "SELECT @BillNo = SCOPE_IDENTITY(); \r\n " +
          " \r\n " +
          "INSERT INTO EmployeeBank.dbo.tblBillDetail (BillNo,TypeNo,LoanNo,Amount,Mount,Year) \r\n " +
          "VALUES (@BillNo,'{TypeNo}','{LoanNo}','{Amount}','{Mount}','{Year}'); "
          ,
          //[6] SELECT Loan INPUT : {TeacherNo}
          "SELECT LoanNo  \r\n " +
          "FROM EmployeeBank.dbo.tblLoan \r\n " +
          "WHERE TeacherNo = '{TeacherNo}' \r\n " +
          " "
          ,
         //[7] UPDATE SavingAmount INPUT: {TeacherNo} {Amount}
          "DECLARE @Saving INT; \r\n " +
          " \r\n " +
          "SET @Saving = (SELECT SavingAmount \r\n " +
          "FROM EmployeeBank.dbo.tblShare \r\n " +
          "WHERE TeacherNo = '{TeacherNo}'); \r\n " +
          " \r\n " +
          "UPDATE EmployeeBank.dbo.tblShare \r\n " +
          "SET SavingAmount = @Saving + '{Amount}' \r\n " +
          "WHERE TeacherNo = '{TeacherNo}' \r\n " +
          " \r\n " +
          " "
          ,
          //[8] UPDATE REMAIN INPUT: {TeacherNo} {Amount}
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

          //[9] Check Time Server INPUT: - 
          "SELECT CONVERT (DATE , CURRENT_TIMESTAMP); "
          ,
          //[10] Change Status Member INPUT: {TeacherNoAddBy} {TeacherNo} {Note} {DocStatusNo} {DocUploadPath} {Status} {TeacherNo}
          "INSERT INTO EmployeeBank.dbo.tblMemberResignation (TeacherNoAddBy,TeacherNo,Date,Note,DocStatusNo,DocUploadPath) \r\n " +
          "VALUES ('{TeacherNoAddBy}','{TeacherNo}',CURRENT_TIMESTAMP,'{Note}','{DocStatusNo}','{DocUploadPath}'); \r\n " +
          " \r\n " +
          "UPDATE EmployeeBank.dbo.tblMember \r\n " +
          "SET MemberStatusNo = '{Status}' \r\n " +
          "WHERE TeacherNo = '{TeacherNo}' "
          ,
          //[11] CheckAmountpay INPUT: {TeacherNo} 
          "SELECT  Bi.BillNo,Bd.BillDetailNo,TeacherNoAddBy,Bi.TeacherNo,Cancel,Bi.DateAdd,Bd.TypeNo,Bt.TypeName,Bd.Amount,Mount,Bd.Year \r\n" +
          "FROM EmployeeBank.dbo.tblBill AS Bi \r\n" +
          "LEFT JOIN EmployeeBank.dbo.tblBillDetail AS Bd ON Bi.BillNo =  Bd.BillNo \r\n" +
          "LEFT JOIN EmployeeBank.dbo.tblBillDetailType AS Bt ON Bd.TypeNo = Bt.TypeNo \r\n" +
          "LEFT JOIN EmployeeBank.dbo.tblMember AS Mb ON Bi.TeacherNo = Mb.TeacherNo \r\n" +
          "WHERE Bi.TeacherNo LIKE '{TeacherNo}' AND Bd.Mount IS NULL AND Bd.Year IS  NULL AND Bd.Amount = Mb.StartAmount ;"
          ,
       


        };
        
        public static void ChangeStatusMember(String TeacherNoAddBy , String TeacherNo , String Note , int DocStatusNo, String DocUpLoadPath , int Status )
        {
            if(DocUpLoadPath == "") { DocUpLoadPath = "Null"; }
            Class.SQLConnection.InputSQLMSSQL(SQLDefault[10].Replace("{TeacherNoAddBy}", TeacherNoAddBy)
                .Replace("{TeacherNo}", TeacherNo)
                .Replace("{Note}", Note)
                .Replace("{DocStatusNo}", DocStatusNo.ToString())
                .Replace("{DocUploadPath}", DocUpLoadPath)
                .Replace("{Status}", Status.ToString()));
        }
      
        public static String CheckTimeServer()
        {
            String Time = Convert.ToDateTime(Class.SQLConnection.InputSQLMSSQL(SQLDefault[9]).Rows[0][0]).ToString("yyyy/MM");
            return Time;
        }
        // ของ POON  สำหรับตรวจสอบการชำระในเเต่ละปี
        public static void CheckAmountpay(string TeacherNo,string Mount,string Year)
        {
            DataTable dt = Class.SQLConnection.InputSQLMSSQL(SQLDefault[11].Replace("{TeacherNo}", TeacherNo));
            if(dt.Rows.Count != 0)
            {
                Mount = dt.Rows[0][9].ToString();
                Year = dt.Rows[0][9].ToString();
                MessageBox.Show("สมาขิกได้จ่ายยอดไปเเล้ว \r\n" +
                                $"ในเดือน{Mount}ปี{Year}", "เเจ้งเตือนการชำระ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public static void InsertBillandUpdateValue(String TeacherNo ,String Teacheraddby , DataGridView DGV)
        {
            String LoanNo = "";
            int TypeNo = 0;

            for (int x = 0; x < DGV.Rows.Count; x++)
            {
                string[] MountandYear = DGV.Rows[x].Cells[0].Value.ToString().Replace(" ","").Split('/');
                if (DGV.Rows[x].Cells[1].Value.ToString() == "สะสม")
                {
                    DataTable LoanID = Class.SQLConnection.InputSQLMSSQL(SQLDefault[6].Replace("{TeacherNo}", TeacherNo));
                    if (LoanID.Rows.Count == 1)
                    {
                        LoanNo = LoanID.Rows[0][0].ToString();
                    }
                    else
                    {
                        MessageBox.Show("ไม่พบรายการกู้", "การแจ้งเตือนการชำระ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    TypeNo = 1;
                }
                else if (DGV.Rows[x].Cells[1].Value.ToString() == "กู้")
                {
                    TypeNo = 2;
                }
                    try
                    {
                        Class.SQLConnection.InputSQLMSSQL(SQLDefault[5].Replace("{TeacherNo}", TeacherNo)
                            .Replace("{TeacherAddBy}",Teacheraddby)
                            .Replace("{TypeNo}",TypeNo.ToString())
                            .Replace("{LoanNo}",LoanNo)
                            .Replace("{Amount}",DGV.Rows[x].Cells[2].Value.ToString())
                            .Replace("{Mount}",MountandYear[1])
                            .Replace("{Year}",MountandYear[0]));
                    if (TypeNo == 1)
                    {
                        Class.SQLConnection.InputSQLMSSQL(SQLDefault[7].Replace("{TeacherNo}",TeacherNo)
                            .Replace("{Amount}",DGV.Rows[x].Cells[2].Value.ToString()));
                    }
                    else if (TypeNo == 2)
                    {
                        Class.SQLConnection.InputSQLMSSQL(SQLDefault[8].Replace("{TeacherNo}", TeacherNo)
                             .Replace("{Amount}", DGV.Rows[x].Cells[2].Value.ToString()));
                    }
                        MessageBox.Show("การชำระเสร็จสิ้น", "การเเจ้งเตือนการชำระ", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    
                    }
                    catch
                    {
                        MessageBox.Show("การชำระล้มเหลว", "การเเจ้งเตือนการชำระ", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    }
            }


        }
        //ค่อยมาแก้
        public static void ReSearchGuarantor(String TBTeacherNo, DataGridView DGV)
        {
            DataTable dt = Class.SQLConnection.InputSQLMSSQL(SQLDefault[6].Replace("T{TeacherNo}", TBTeacherNo));
            if (dt.Rows.Count != 0)
            {
                DGV.Rows.Add(dt.Rows[0][0], dt.Rows[0][1], dt.Rows[0][3]);
            }
            else
            {
                MessageBox.Show("รหัสอาจารย์ไม่ถูกต้อง", "System", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //เช็ค ยอดจ่ายของสมาชิกในเดือน อัตโนมัติ
        public static void AmountpayANDAmountLoanINMonth(string TeacherNo,TextBox Amount,ComboBox SELECTINDEX)
        {
            DataTable dt = Class.SQLConnection.InputSQLMSSQL(SQLDefault[3].Replace("{TeacherNo}",TeacherNo));
            if(dt.Rows.Count != 0)
            {
                if(SELECTINDEX.SelectedIndex == 0)
                {
                    Amount.Text = dt.Rows[0][1].ToString();
                }
                else
                {
                    Amount.Text = dt.Rows[0][3].ToString();
                }
               
               
            }
        }

        // ค้นหารายชื่อผู้ลงทะเบียนในระบบครู ใส่หมายยเลข
        /// <summary>
        /// <para>ค่าที่ต้องการในช่องสเรตตัส</para>
        /// <para>ใส่่ 0 เป็นการหา Member ที่ยังไม่ได้สมัครสมาชิก</para>
        /// <para> ใส่ 1 เป็นการหา Member ที่ยังใช้งานอยู่</para>
        /// <para> ใส่ 2 เป็นการหา Member ที่ยกเลิกไปแล้ว</para>
        /// </summary>
        public static void ResearchUserAllTLC(string TBTeacherNo, TextBox TBTeacherName, TextBox TBTeacherIDNo , int StatusMemeber)
        {
            if(StatusMemeber != 0)
            {
             DataTable dt = Class.SQLConnection.InputSQLMSSQL(SQLDefault[0].Replace("T{TeacherNo}", TBTeacherNo)
                .Replace("{MemberStatus}", StatusMemeber.ToString())
                .Replace("{","")
                .Replace("}","")) ;
                if (dt.Rows.Count != 0)
                {
                TBTeacherName.Text = dt.Rows[0][1].ToString();
                TBTeacherIDNo.Text = dt.Rows[0][2].ToString();
                }
            }
            else
            {
                DataTable dt = Class.SQLConnection.InputSQLMSSQL(SQLDefault[0].Replace("T{TeacherNo}", TBTeacherNo)
                .Replace("{Status}", StatusMemeber.ToString())
                .Replace(" and {d.MemberStatusNo = '{MemberStatus}}' ", ""));
                if (dt.Rows.Count != 0)
                {
                    TBTeacherName.Text = dt.Rows[0][1].ToString();
                    TBTeacherIDNo.Text = dt.Rows[0][2].ToString();
                }
               
            }


        }
        // ค้นหารายชื่อผู้สมัครยอดสะสมโดยการ ใส่หมายยเลข
        public static void ResearhMerberANDinformation(string TeacherNo, TextBox TBTeacherName, TextBox TBTeacherIDNo, TextBox TeacherLicenseNo, TextBox TeacherIdNo, TextBox TelMobile, TextBox MemberStatusName, TextBox StartAmount)
        {
            DataTable dt = Class.SQLConnection.InputSQLMSSQL(SQLDefault[2].Replace("T{TeacherNo}%",TeacherNo));
            if (dt.Rows.Count != 0)
            {
                TBTeacherName.Text = dt.Rows[0][1].ToString();
                TBTeacherIDNo.Text = dt.Rows[0][2].ToString();
                TeacherLicenseNo.Text = dt.Rows[0][3].ToString();
                TeacherIdNo.Text = dt.Rows[0][4].ToString();
                TelMobile.Text = dt.Rows[0][5].ToString();
                StartAmount.Text = dt.Rows[0][8].ToString();
                MemberStatusName.Text = dt.Rows[0][10].ToString();
            }
        }
        // ค้นหารายชื่อผู้กู้โดยการ ใส่หมายยเลข
        public static void ReSearchLoan (string TBTeacherNo,TextBox TBTeacherNane,TextBox LoanNo, TextBox LoanStatusName, TextBox SavingAmount)
        {
            DataTable dt = Class.SQLConnection.InputSQLMSSQL(SQLDefault[2].Replace("T{TeacherNo}%", TBTeacherNo));
            if (dt.Rows.Count != 0)
            {
                TBTeacherNane.Text = dt.Rows[0][1].ToString(); 
                LoanStatusName.Text = dt.Rows[0][7].ToString();
                LoanNo.Text = dt.Rows[0][6].ToString();
                SavingAmount.Text = dt.Rows[0][9].ToString();
            }
        }
        ///<summary>
        /// <para>[AllTeacher_or_Member]</para> 
        /// <para>ถ้าใส่ 0 จะหาอาจารย์ทั้งหมด</para>
        /// <para>ใส่ 1 จะหาแค่อาจารยฺ์ที่สมัครสมาชิกแล้ว ( สถาณะ ใช้งานเท่านั้น ) Return : รหัสอาจารย์ ชื่อ เลขบัตรปชช.</para>
        /// <para>ใส่ 2 จะหาอาจารย์ที่สมัครสมาชิกแล้ว แต่มีข้อมูลสำหรับการกู้เพิ่มเข้ามา (สถาณะใช้งานเท่านั้น) Return : รหัสอาจารย์ ชื่อ เลขบัตรปชช. สถาณะ เลขที่สัญญากู้ หุ้นสะสม</para>
        /// <para>ใส่ 3 จะหาแค่อาจารยฺ์ที่สมัครสมาชิกแล้ว ( สถาณะ ไม่ใช้งาน ) Return : รหัสอาจารย์ ชื่อ เลขบัตรปชช.</para>
        ///</summary>

        public static void SearchINNserDataGridViewLoan(DataGridView G, int AllTeacher_or_Member)
        {
            DataTable dt = Class.SQLConnection.InputSQLMSSQL(SQLDefault[2].Replace("{TeacherNo}", ""));
            for (int x = 0; x < dt.Rows.Count; x++)
            {
                G.Rows.Add(dt.Rows[x][0], dt.Rows[x][1], dt.Rows[x][2], dt.Rows[x][3], dt.Rows[x][4], dt.Rows[x][5]);

                if (x % 2 == 1)
                {
                    G.Rows[x].DefaultCellStyle.BackColor = Color.AliceBlue;
                }
            }
        }

        // สมัครสมาชิกสหกร์ครู
        public static void INERApplyTeacher(string TeacherNo,string TeacherAddBy, int StartAmount, string DocUploadPath)
        {
            DataTable dt = Class.SQLConnection.InputSQLMSSQL(SQLDefault[1].Replace("{TeacherNo}",TeacherNo));
            if (TeacherNo != "")
            {
                if (dt.Rows.Count == 0)
                {
                    DataTable INSERTMember = Class.SQLConnection.InputSQLMSSQL(SQLDefault[2].Replace("{TeacherNo}", TeacherNo)
                        .Replace("{TeacherAddBy}", TeacherAddBy)
                        .Replace("{StartAmount}", StartAmount.ToString())
                        .Replace("{DocPath}",DocUploadPath));
                    MessageBox.Show("Register Complete.", "System", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("รายชื่อซ้ำ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("โปรดเลือกสมาชิกในการสมัคร", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
       
        // ถังขยะสำหรับโค้ดที่ไม่ได้ฝช้งานเเต่ต้องการเก็บไว้ศึกษา หรือ เก็บไว้
        public void CodeRecycleBin()
        {
            /// <para>[1] Check Register INPUT: {TeacherNo} </para>
            //  //[1] Check Register INPUT: TeacherNo
            //  "SELECT * \r\n " +
            //  "FROM EmployeeBank.dbo.tblMember \r\n " +
            //  "WHERE TeacherNo = '{TeacherNo}'; "
            //,

            /// <para>[3] Search Member INPUT: -</para>
            //  //[3] Search Member INPUT: -
            //  "SELECT a.TeacherNo,CAST(d.PrefixName+' '+Fname +' '+ Lname as NVARCHAR),IdNo \r\n " +
            //  "FROM EmployeeBank.dbo.tblMember as a \r\n " +
            //  "LEFT JOIN Personal.dbo.tblTeacherHis as b ON a.TeacherNo = b.TeacherNo \r\n " +
            //  "LEFT JOIN Personal.dbo.tblGroupPosition as c ON b.GroupPositionNo = c.GroupPositionNo \r\n " +
            //  "LEFT JOIN BaseData.dbo.tblPrefix as d ON d.PrefixNo = b.PrefixNo \r\n " +
            //  "WHERE a.MemberStatusNo = 1 \r\n" +
            //  "ORDER BY a.TeacherNo; "
            //,

            /// <para>[4] paydataMember INPUT:{TeacherNo} {TeacherLicenseNo} {TeacherIdNo} {TelMobile} {MemberStatusName} {StartAmount}  </para>
            //  //[4]  paydataMember INPUT:{TeacherNo}
            //  "SELECT Pn.TeacherLicenseNo,Pn.IdNo,Pn.TelMobile,CAST(Ms.MemberStatusName as nvarchar),Mb.StartAmount" +
            //  "FROM[Personal].[dbo].[tblTeacherHis] as PnINNER " +
            //  "JOIN EmployeeBank.dbo.tblMember as Mb on Pn.TeacherNo = Mb.TeacherNoINNER " +
            //  "JOIN EmployeeBank.dbo.tblMemberStatus as Ms on Mb.MemberStatusNo = Ms.MemberStatusNoWHERE    " +
            //  "Mb.TeacherNo LIKE '{TeacherNo}' " +
            //  "ORDER BY Mb.TeacherNo;"
            //,

        }
    }
}
