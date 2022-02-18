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

namespace BankTeacher.Bank.Add_Member
{
    public partial class CancelMember : Form
    {
        bool Check = false;
        int StatusBoxFile = 0;
        String imgeLocation = "";
        bool CheckBRegister = false;
        bool CheckBCancel = false;
        double Saving = 0;
        bool CheckSave = false;

        /// <summary>
        /// <para>[0] SELECT MEMBER INPUT: {Text}</para>
        /// <para>[1] Change Status Member INPUT: {TeacherNoAddBy} {TeacherNo} {Note} {DocStatusNo} {DocUploadPath} {Status}</para>
        /// <para>[2] SELECT Check SavingAmount INPUT: {TeacherNo}</para>
        /// <para>[3] Select MemberResignation INPUT: {Date}</para>
        /// <para>[4] SELECT Year INPUT: </para>
        /// <para>[5] SELECT MEMBER (Enter) INPUT: {Text}</para>
        /// <para>[6] Search Member and SavingAmount - RemainAmount in Guarantor INPUT: {TeacherNoNotLike}  {Text}</para>
        /// <para>[7] Count Cancel Member File INPUT: {TeacherNo}</para>
        /// <para>[8] Chcek flie Regmember INPUT : {TeacherNo}</para>
        /// <para>[9] UPDATE Status File INPUT: {TeacherNoAddBy} {ID} {TeacherNo} {PathFile} </para>
        /// <para>[10] Get SavingAmount and Get GuarantorAmount INPUT: {TeacherNo}INPUT: {TeacherNo}</para>
        /// <para>[11] if HE is Guarantor Get Amount and SavingAmount if(His Loan Get SumAmount and SavingAmount) INPUT: {TeacherNo} </para>
        /// <para>[12] Get Teacher's LoanNo INPUT: {TeacherNo} </para>
        /// <para>[13] Update End Loan if not received money (LoanStatus = 1) INPUT: {TeacherNo}</para>
        /// <para>[14] Chcek flie INPUT : {TeacherNo}</para>
        /// <para>[15] Insert File INPUT : {TeacharNo} {PathFile} {PathFile} {TeacherAddBy}</para>
        /// </summary>
        private String[] SQLDefault = new string[]
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
             //[1] Change Status Member INPUT: {TeacherNoAddBy} {TeacherNo} {Note} {DocStatusNo} {PathFile}
             "INSERT INTO EmployeeBank.dbo.tblMemberResignation (TeacherNoAddBy,TeacherNo,Date,Note,DocStatusNo,DocUploadPath)  \r\n " +
             "VALUES ('{TeacherNoAddBy}','{TeacherNo}',CURRENT_TIMESTAMP,'{Note}','{DocStatusNo}','{PathFile}');  \r\n " +
             "   \r\n " +
             "UPDATE EmployeeBank.dbo.tblMember  \r\n " +
             "SET MemberStatusNo = '2' , DocStatusNo = 2 , DocUploadPath = ''\r\n " +
             "WHERE TeacherNo = '{TeacherNo}'; \r\n " +
             " \r\n " +
             "UPDATE EmployeeBank.dbo.tblDividend  \r\n " +
             "SET RemainInterestLastYear = ROUND(RemainInterestLastYear + b.SavingAmount,2,1) \r\n " +
             "FROM (SELECT b.SavingAmount \r\n " +
             "	FROM EmployeeBank.dbo.tblMember as a \r\n " +
             "	LEFT JOIN EmployeeBank.dbo.tblShare as b on a.TeacherNo = b.TeacherNo \r\n " +
             "	WHERE a.TeacherNo LIKE '{TeacherNo}') as b \r\n " +
             "WHERE DividendNo = (SELECT TOP 1 DividendNo FROM EmployeeBank.dbo.tblDividend ORDER BY DividendNo DESC);"
            ,
            //[2] SELECT Check SavingAmount INPUT: {TeacherNo}
           "SELECT b.SavingAmount,a.DocStatusNo,a.DocUploadPath  \r\n " +
          "FROM EmployeeBank.dbo.tblMember as a \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblShare as b on a.TeacherNo = b.TeacherNo \r\n " +
          "WHERE a.TeacherNo = '{TeacherNo}'"
           ,
           //[3] Select MemberResignation INPUT: {Date}
           "SELECT Date,b.TeacherNo , CAST (ISNULL(c.PrefixName , '')+' '+Fname + ' ' + Lname as nvarchar),Note \r\n " +
          "FROM EmployeeBank.dbo.tblMemberResignation as a \r\n " +
          "LEFT JOIN Personal.dbo.tblTeacherHis as b on a.TeacherNo = b.TeacherNo \r\n " +
          "LEFT JOIN BaseData.dbo.tblPrefix as c on b.PrefixNo = c.PrefixNo \r\n " +
          "WHERE CAST(CAST(DATE  as  date)as nvarchar(50))  LIKE  '{Date}%'"
           ,
           //[4] SELECT Year INPUT: 
           "SELECT YEAR(Date) as Year \r\n " +
          "FROM EmployeeBank.dbo.tblMemberResignation \r\n " +
          "GROUP BY  YEAR(Date) "
           ,
           //[5] SELECT MEMBER (Enter) INPUT: {Text}
            "SELECT TOP(20) a.TeacherNo , CAST(ISNULL(c.PrefixName+' ','')+[Fname] +' '+ [Lname] as NVARCHAR)AS Name, e.SavingAmount,    \r\n " +
            "b.TeacherLicenseNo,b.IdNo AS IDNo,b.TelMobile ,a.StartAmount,CAST(d.MemberStatusName as nvarchar) AS UserStatususing    \r\n " +
            "FROM EmployeeBank.dbo.tblMember as a    \r\n " +
            "LEFT JOIN Personal.dbo.tblTeacherHis as b ON a.TeacherNo = b.TeacherNo    \r\n " +
            "LEFT JOIN BaseData.dbo.tblPrefix as c ON c.PrefixNo = b.PrefixNo   \r\n " +
            "INNER JOIN EmployeeBank.dbo.tblMemberStatus as d on a.MemberStatusNo = d.MemberStatusNo  \r\n " +
            "LEFT JOIN EmployeeBank.dbo.tblShare as e on a.TeacherNo = e.TeacherNo \r\n " +
            "WHERE a.MemberStatusNo = 1 and a.TeacherNo = '{Text}' and a.MemberStatusNo = 1         \r\n " +
            "GROUP BY a.TeacherNo , CAST(ISNULL(c.PrefixName+' ','')+[Fname] +' '+ [Lname] as NVARCHAR), e.SavingAmount,    \r\n " +
            "b.TeacherLicenseNo,b.IdNo ,b.TelMobile ,a.StartAmount,CAST(d.MemberStatusName as nvarchar)   \r\n " +
            "ORDER BY a.TeacherNo; "

            ,

            //[6] Search Member and SavingAmount - RemainAmount in Guarantor INPUT: {TeacherNoNotLike}  {Text}
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
           //[7] Count Cancel Member File INPUT: {TeacherNo}
           "SELECT Count(ID) \r\n " +
          "  FROM EmployeeBank.dbo.tblFile \r\n " +
          "  WHERE FiletypeNo = 2 and IsUse = 1 and TeacherNo = '{TeacherNo}'"
           ,
           //[8] Chcek flie Regmember INPUT : {TeacherNo}
           "SELECT c.ID , c.pathFile\r\n " +
          " FROM EmployeeBank.dbo.tblMember as a   \r\n " +
          " LEFT JOIN EmployeeBank.dbo.tblDocStatus as b on a.DocStatusNo = b.DocStatusNo   \r\n " +
          " LEFT JOIN EmployeeBank.dbo.tblFile as c on a.TeacherNo = c.TeacherNo \r\n " +
          " LEFT JOIN EmployeeBank.dbo.tblStatusFileInSystem as d on c.StatusFileInSystem = d.ID \r\n " +
          " WHERE a.TeacherNo = '{TeacherNo}' and c.IsUse = 1 and FiletypeNo = 1 and StatusFileInSystem = 2"
           ,
           //[9] UPDATE Status File INPUT: {TeacherNoAddBy} {ID} {TeacherNo} {PathFile} 
           "UPDATE EmployeeBank.dbo.tblFile \r\n " +
          "SET TeacherRemoveFileBy = '{TeacherNoAddBy}' , IsUse = 0 , DateRemoveFile = CURRENT_TIMESTAMP , StatusFileInSystem = 1 \r\n " +
          "WHERE ID = '{ID}' \r\n " +
          " \r\n " +
          "INSERT INTO EmployeeBank.dbo.tblFile(TeacherNo,FiletypeNo,pathFile,TeacherAddBy,LoanID,DateAddFile,IsUse,TeacherRemoveFileBy,DateRemoveFile,StatusFileInSystem) \r\n " +
          "VALUES('{TeacherNo}','2','{PathFile}','{TeacherNoAddBy}',null,CURRENT_TIMESTAMP,1,null,null,1)"
           ,

           //[10] Get SavingAmount & Get GuarantorAmount INPUT: {TeacherNo}
           "SELECT b.SavingAmount , ISNULL(c.Amount , 0) as LoanAmount \r\n " +
          "FROM EmployeeBank.dbo.tblMember as a \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblShare as b on a.TeacherNo = b.TeacherNo \r\n " +
          "LEFT JOIN (SELECT SUM(b.Amount) as Amount , a.TeacherNo \r\n " +
          "FROM EmployeeBank.dbo.tblLoan as a \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblGuarantor as b on a.LoanNo = b.LoanNo \r\n " +
          "WHERE a.TeacherNo = '{TeacherNo}' and a.LoanStatusNo = 2 \r\n " +
          "GROUP BY a.TeacherNo) as c on a.TeacherNo = c.TeacherNo \r\n " +
          "WHERE a.TeacherNo = '{TeacherNo}';"
           ,

           //[11] if (HE is Guarantor Get Amount & SavingAmount) if(His Loan Get SumAmount & SavingAmount) INPUT: {TeacherNo}
           "DECLARE @YourLoan int; \r\n " +
          " \r\n " +
          "SELECT @YourLoan = COUNT(a.LoanNo) \r\n " +
          "FROM EmployeeBank.dbo.tblLoan as a \r\n " +
          "WHERE a.TeacherNo = '{TeacherNo}' and a.LoanStatusNo = 2 \r\n " +
          " \r\n " +
          "IF(@YourLoan > 0) \r\n " +
          "BEGIN  \r\n " +
          "	SELECT ISNULL(c.Amount , 0) as LoanAmount , b.SavingAmount   \r\n " +
          "	FROM EmployeeBank.dbo.tblMember as a \r\n " +
          "	LEFT JOIN EmployeeBank.dbo.tblShare as b on a.TeacherNo = b.TeacherNo \r\n " +
          "	LEFT JOIN (SELECT SUM(b.Amount) as Amount , a.TeacherNo \r\n " +
          "	FROM EmployeeBank.dbo.tblLoan as a \r\n " +
          "	LEFT JOIN EmployeeBank.dbo.tblGuarantor as b on a.LoanNo = b.LoanNo \r\n " +
          "	WHERE a.TeacherNo = '{TeacherNo}'  \r\n " +
          "	and a.LoanStatusNo = 2 \r\n " +
          "	GROUP BY a.TeacherNo) as c on a.TeacherNo = c.TeacherNo \r\n " +
          "	WHERE a.TeacherNo = '{TeacherNo}'; \r\n " +
          "END \r\n " +
          "ELSE \r\n " +
          "BEGIN \r\n " +
          "	SELECT (b.Amount) as Amount , c.SavingAmount \r\n " +
          "	FROM EmployeeBank.dbo.tblLoan as a \r\n " +
          "	LEFT JOIN EmployeeBank.dbo.tblGuarantor as b on a.LoanNo = b.LoanNo \r\n " +
          "	LEFT JOIN EmployeeBank.dbo.tblShare as c on b.TeacherNo = c.TeacherNo \r\n " +
          "	WHERE b.TeacherNo = '{TeacherNo}' and a.LoanStatusNo = 2 \r\n " +
          "	GROUP BY (b.Amount) , c.SavingAmount \r\n " +
          "END;" +
            "\r\n" +
            "SELECT @YourLoan;"
           ,

           //[12] Get Teacher's LoanNo INPUT: {TeacherNo} 
           "SELECT a.LoanNo \r\n " +
          "FROM EmployeeBank.dbo.tblLoan as a \r\n " +
          "WHERE a.TeacherNo = '{TeacherNo}' and a.LoanStatusNo = 2"
           ,
           
           //[13] Update End Loan if not received money (LoanStatus = 1) INPUT: {TeacherNo}
           "UPDATE EmployeeBank.dbo.tblLoan \r\n " +
          "SET LoanStatusNo = 4 \r\n " +
          "WHERE TeacherNo = '{TeacherNo}' and LoanStatusNo = 1"
           ,
          //[14] Chcek flie INPUT : {TeacherNo}
           "SELECT c.ID , c.pathFile\r\n " +
          " FROM EmployeeBank.dbo.tblMember as a   \r\n " +
          " LEFT JOIN EmployeeBank.dbo.tblDocStatus as b on a.DocStatusNo = b.DocStatusNo   \r\n " +
          " LEFT JOIN EmployeeBank.dbo.tblFile as c on a.TeacherNo = c.TeacherNo \r\n " +
          " LEFT JOIN EmployeeBank.dbo.tblStatusFileInSystem as d on c.StatusFileInSystem = d.ID \r\n " +
          " WHERE a.TeacherNo = '{TeacherNo}' and c.IsUse = 1 and FiletypeNo = 1 and StatusFileInSystem = 2"
           ,
           //[15] Insert File INPUT : {TeacharNo} {PathFile} {PathFile} {TeacherAddBy}
           "INSERT INTO EmployeeBank.dbo.tblFile(TeacherNo , FiletypeNo , pathFile ,TeacherAddBy,LoanID,DateAddFile,IsUse , TeacherRemoveFileBy ,DateRemoveFile, StatusFileInSystem) \r\n " +
          "VALUES ('{TeacherNo}',1,'{PathFile}','{TeacherAddBy}',null,CURRENT_TIMESTAMP,1,null,null,2); \r\n " +
          " \r\n " +
          "UPDATE EmployeeBank.dbo.tblMember \r\n " +
          "SET DocStatusNo = 1 , DocUploadPath = '{PathFile}'\r\n"+
          "WHERE TeacherNo = '{TeacherNo}'"
           ,
        };
        public CancelMember()
        {
            InitializeComponent();
            DataTable dt = Class.SQLConnection.InputSQLMSSQL(SQLDefault[4]);
            if(dt.Rows.Count != 0)
            {
                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    CBYear_HistoryCancel.Items.Add(dt.Rows[x][0].ToString());
                }
                if (CBYear_HistoryCancel.Items.Count != 0)
                    CBYear_HistoryCancel.SelectedIndex = 0;
                else
                    CBYear_HistoryCancel.Enabled = false;
            }
            Relaodcancelmember();
            //tabControl1 = tabControl1a;
        }
        private void CancelMember_SizeChanged_1(object sender, EventArgs e)
        {
            Class.FromSettingMedtod.ChangeSizePanal(this,panel1);
        }

        private void BSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Bank.Search IN = new Bank.Search(SQLDefault[6]
                    .Replace("{TeacherNoNotLike}", $"and a.TeacherNo NOT LIKE '{TBTeacherNo.Text}'"));
                IN.ShowDialog();
                if (Bank.Search.Return[0] != "")
                {
                    TBTeacherNo.Text = Bank.Search.Return[0];
                    TBTeacherName.Text = Bank.Search.Return[1];
                    TBTeacherNo_KeyDown(new object(), new KeyEventArgs(Keys.Enter));
                }
            }
            catch (Exception x)
            {
                Console.WriteLine(x);
            }
        }

        private void TBTeacherNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TBTeacherNo.Text = TBTeacherNo.Text.Replace("t", "T");
                DataSet ds = Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[0].Replace("{Text}", TBTeacherNo.Text));
                if(ds.Tables[0].Rows.Count != 0)
                {
                    TBTeacherName.Text = ds.Tables[0].Rows[0][1].ToString();
                    Saving = Convert.ToDouble(ds.Tables[0].Rows[0][2].ToString());
                    Check = true;
                    TBTeacherNo.Enabled = false;
                    Checkmember(false);
                    BSearch.Enabled = true;
                    BSave.Enabled = true;
                    CheckSave = false;
                    BTUploadFIle.Visible = false;
                }
                else
                {
                    MessageBox.Show("ไม่พยรายชื่อ","ระบบ",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }
            }
            else if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back && Check)
            {
                TBTeacherName.Text = "";
                CheckBCancel = false;
                Check = false;
                Checkmember(true);
                BTUploadFIle.Visible = false;
            }
        }

        private void BSave_Click(object sender, EventArgs e)
        {
            if (TBTeacherName.Text != "")
            {
                
                if (TBNote.Text != "")
                {
                    BankTeacher.Class.ProtocolSharing.FileZilla.FileZillaConnection FTP = new Class.ProtocolSharing.FileZilla.FileZillaConnection("RegMember");
                    DataSet ds = Class.SQLConnection.InputSQLMSSQLDS((SQLDefault[2]+ "\r\n\r\n" + SQLDefault[7] + "\r\n\r\n" + SQLDefault[8])
                    .Replace("{TeacherNo}", TBTeacherNo.Text));

                    DataSet dsLoanLoanAmount = Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[11]
                        .Replace("{TeacherNo}", TBTeacherNo.Text));
                    
                    if(dsLoanLoanAmount.Tables[0].Rows.Count != 0 && Convert.ToInt32(dsLoanLoanAmount.Tables[0].Rows[0][1].ToString()) >= Convert.ToInt32(dsLoanLoanAmount.Tables[0].Rows[0][0]))
                    {
                        if(Convert.ToInt32(dsLoanLoanAmount.Tables[1].Rows[0][0]) != 0  && MessageBox.Show("มียอดกู้อยู่ในระบบ และคุณสามารถปิดยอดกู้ตอนนี้ได้เลย\r\n โดยใช้เงินเก็บสะสมของคุณ ต้องการจะปิดยอดกู้เลยหรือไม่","แจ้งเตือน",MessageBoxButtons.YesNo,MessageBoxIcon.Warning) == DialogResult.Yes)
                        {
                            Bank.Add_Member.CancelMemberCloseTheLoan FCloseLoan = new CancelMemberCloseTheLoan();
                            DataTable dtGetLoanNo = Class.SQLConnection.InputSQLMSSQL(SQLDefault[12]
                                .Replace("{TeacherNo}", TBTeacherNo.Text));
                            if(dtGetLoanNo.Rows.Count != 0)
                            {
                                FCloseLoan.Show();
                                FCloseLoan.TeacherNo = TBTeacherNo.Text;
                                for (int a = 0; a < dtGetLoanNo.Rows.Count; a++)
                                {
                                    FCloseLoan.CBLoanNo.Items.Add(dtGetLoanNo.Rows[a][0]);
                                }
                                FCloseLoan.CBLoanNo.SelectedIndex = 0;
                                FCloseLoan.TBTeacherName.Text = TBTeacherName.Text;
                                this.Enabled = false;
                                this.Hide();
                            }

                        }
                        else if(Convert.ToInt32(dsLoanLoanAmount.Tables[1].Rows[0][0]) == 0 && MessageBox.Show("คุณมียอดค้ำกู้อยู่ในระบบ และคุณสามารถจ่ายยอดค้ำกู้ได้เลย \r\n" +
                            "โดยใช้เงินเก็บสะสมของคุณในระบบ และสามารถขอคืนเงินเก็บสะสมได้ในภายหลัง ต้องการจ่ายยอดค้ำเลยหรือไม่" , "แจ้งเตือน" , MessageBoxButtons.YesNo ,MessageBoxIcon.Warning) == DialogResult.Yes)
                        {
                            if (ds.Tables[2].Rows.Count != 0)
                            {
                                String Rename = $"Member_{TBTeacherNo.Text}.pdf";
                                if (ds.Tables[1].Rows.Count != 0)
                                {
                                    Rename = Rename.Replace(".pdf", $"_{ds.Tables[1].Rows[0][0].ToString()}.pdf");
                                }

                                

                                FTP.FTPMoveFileandRename($"Member_{TBTeacherNo.Text}.pdf", "CancelMember", Rename);
                                if (BankTeacher.Class.ProtocolSharing.FileZilla.StatusReturn == true)
                                {
                                    Class.SQLConnection.InputSQLMSSQLDS((SQLDefault[1] + "\r\n" + SQLDefault[9] + "\r\n" + SQLDefault[13])
                                    .Replace("{TeacherNoAddBy}", Class.UserInfo.TeacherNo)
                                    .Replace("{TeacherNo}", TBTeacherNo.Text)
                                    .Replace("{Note}", TBNote.Text)
                                    .Replace("{DocStatusNo}", "1")
                                    .Replace("{PathFile}", FTP.HostplusPathFile + Rename.Replace("RegMember", "CancelMember"))
                                    .Replace("{ID}", ds.Tables[2].Rows[0][0].ToString()));
                                    MessageBox.Show("ยกเลิกผู้ใช้สำเร็จ", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    CheckSave = true;
                                    CheckBCancel = true;
                                    Checkmember(true);
                                    BSave.Enabled = false;
                                    BTUploadFIle.Visible = false;
                                    TBTeacherNo_KeyDown(new object(), new KeyEventArgs(Keys.Delete));
                                }

                            }
                            else
                            {
                                MessageBox.Show("กรุณาส่งเอกสารสมัครสมาชิก เพื่อยืนยันการสมัคร", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                BTUploadFIle.Visible = true;
                            }
                        }
                    }
                    else if (dsLoanLoanAmount.Tables[0].Rows.Count != 0 && Convert.ToInt32(dsLoanLoanAmount.Tables[1].Rows[0][0]) != 0)
                    {
                        MessageBox.Show("มียอดกู้อยู่ในระบบไม่สามารถยกเลิกสมาชิกได้ และเงินสะสมไม่พอที่จะชำระ \r\n" +
                            "ต้องชำระกู้ให้หมดก่อน ถึงจะยกเลิกสมาชิกได้", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (dsLoanLoanAmount.Tables[0].Rows.Count != 0 && Convert.ToInt32(dsLoanLoanAmount.Tables[1].Rows[0][0]) == 0)
                    {
                        MessageBox.Show("มียอดค้ำกู้อยู่ในระบบไม่สามารถยกเลิกสมาชิกได้ และเงินสะสมไม่พอที่จะวางเงินค้ำประกัน \r\n" +
                            "ต้องรอผู้กู้ชำระกู้ให้หมดก่อน", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString()) < 1)
                    {
                        if (ds.Tables[2].Rows.Count != 0)
                        {
                            String Rename = $"Member_{TBTeacherNo.Text}.pdf";
                            if (ds.Tables[1].Rows.Count != 0)
                            {
                                Rename = Rename.Replace(".pdf", $"_{ds.Tables[1].Rows[0][0].ToString()}.pdf");
                            }

                            

                            FTP.FTPMoveFileandRename($"Member_{TBTeacherNo.Text}.pdf", "CancelMember", Rename);
                            if (BankTeacher.Class.ProtocolSharing.FileZilla.StatusReturn == true)
                            {
                                Class.SQLConnection.InputSQLMSSQLDS((SQLDefault[1] + "\r\n" + SQLDefault[9] + "\r\n" + SQLDefault[13])
                                .Replace("{TeacherNoAddBy}", Class.UserInfo.TeacherNo)
                                .Replace("{TeacherNo}", TBTeacherNo.Text)
                                .Replace("{Note}", TBNote.Text)
                                .Replace("{DocStatusNo}", "1")
                                .Replace("{PathFile}", FTP.HostplusPathFile + Rename.Replace("RegMember", "CancelMember"))
                                .Replace("{ID}", ds.Tables[2].Rows[0][0].ToString()));
                                MessageBox.Show("ยกเลิกผู้ใช้สำเร็จ", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                CheckSave = true;
                                CheckBCancel = true;
                                Checkmember(true);
                                BSave.Enabled = false;
                                TBTeacherNo_KeyDown(new object(), new KeyEventArgs(Keys.Delete));
                            }

                        }
                        else
                        {   
                            MessageBox.Show("ไม่พบเอกสารสมัครสมาชิก \n กรุณาส่งเอกสารก่อนทำรายการ", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            BTUploadFIle.Visible = true;

                        }
                    }
                    else
                    {
                        if ((MessageBox.Show("ยอดเงินคงเหลือของท่านยังอยู่ในระบบ \r\n กรุณาถอนเงินออกจากระบบก่อนทำรายการ", "ระบบ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes))
                        {
                            AmountOff FAmountOff = new AmountOff();
                            Menu FMenu = new Menu();
                            FAmountOff.FormBorderStyle = FormBorderStyle.None;
                            FAmountOff.Show();
                            FAmountOff.TBTeacherNo.Text = TBTeacherNo.Text;
                            FAmountOff.TBTeacherNo_KeyDown(sender, new KeyEventArgs(Keys.Enter));
                            FAmountOff.BMaxWithDraw_AmountOff_Click(sender,e);
                            FAmountOff.TBWithDraw.Enabled = false;
                            foreach (Form f in Application.OpenForms)
                            {
                                if (f.Name == "Menu")
                                {
                                    f.Enabled = false;
                                    f.Hide();
                                    break;
                                }
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("กรุณากรอก ข้อมูลที่หมายเหตุให้เรียบร้อย", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void CBYear_HistoryCancel_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = Class.SQLConnection.InputSQLMSSQL(SQLDefault[3].Replace("{Date}", CBYear_HistoryCancel.Text));
            if (dt.Rows.Count != 0)
            {
                DGV_HistoryCancel.Rows.Clear();
                for (int x = 0; x < dt.Rows.Count; x++)
                
                    DGV_HistoryCancel.Rows.Add((Convert.ToDateTime(dt.Rows[x][0].ToString())).ToString("dd-MM-yyyy"), dt.Rows[x][1].ToString(), dt.Rows[x][2].ToString(), dt.Rows[x][3].ToString());
                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    if (x % 2 == 1)
                    {
                        DGV_HistoryCancel.Rows[x].DefaultCellStyle.BackColor = Color.AliceBlue;
                    }
                }
            }
        }
        private void Relaodcancelmember()
        {
            int Year = Convert.ToInt32(BankTeacher.Bank.Menu.Date[0]);
            for (int a = 0; a < 5; a++)
            {
                if (Class.SQLConnection.InputSQLMSSQL(SQLDefault[2]
                    .Replace("{Date}", Year.ToString())).Rows.Count != 0)
                    CBYear_HistoryCancel.Items.Add(Year);
                Year--;
            }
            if (CBYear_HistoryCancel.Items.Count != 0)
                CBYear_HistoryCancel.SelectedIndex = 0;
        }

      

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
            {
                CBYear_HistoryCancel_SelectedIndexChanged(new object(), new EventArgs());
            }
        }

        private void BExitForm_Click(object sender, EventArgs e)
        {
            BankTeacher.Class.FromSettingMedtod.ReturntoHome(this);
        }

        private void CancelMember_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape || (e.KeyCode == Keys.Enter && CheckSave))
            {
                if(TBTeacherNo.Text.Length != 0 || DGV_HistoryCancel.Rows.Count > 0 || CBYear_HistoryCancel.SelectedIndex != -1)
                {
                    TBTeacherNo.Text = "";
                    TBTeacherName.Text = "";
                    TBNote.Text = "";
                    Check = false;
                    StatusBoxFile = 0;
                    imgeLocation = "";
                    CheckBRegister = false;
                    CheckBCancel = false;
                    Saving = 0;
                    CBYear_HistoryCancel.DroppedDown = false;
                    CBYear_HistoryCancel.Items.Clear();
                    DGV_HistoryCancel.Rows.Clear();
                    Checkmember(true);
                    CheckSave = false;
                }
                else
                {
                    BExitForm_Click(new object(), new EventArgs());
                }
            }
        }
        
        private void Checkmember(bool tf)
        {
            TBTeacherNo.Enabled = tf;
            //BSearch.Enabled = tf;
        }

        private void tabControl1_KeyDown(object sender, KeyEventArgs e)
        {
            
        }
        public class MyTextBox : TextBox
        {
        }

        private void CancelMember_KeyUp(object sender, KeyEventArgs e)
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

        private void BTUploadFIle_Click(object sender, EventArgs e)
        {
            String PathFile = null;
            DataTable dtChackStatusFile = Class.SQLConnection.InputSQLMSSQL(SQLDefault[14].Replace("{TeacherNo}", TBTeacherNo.Text));
            if (dtChackStatusFile.Rows.Count != 0)
            {
                if (dtChackStatusFile.Rows[0][1].ToString() == "")
                {
                    Class.ProtocolSharing.FileZilla.FileZillaConnection FTP = new Class.ProtocolSharing.FileZilla.FileZillaConnection("RegMember");
                    OpenFileDialog dialog = new OpenFileDialog();
                    dialog.Filter = "pdf files(*.pdf)|*.pdf";
                    if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        PathFile = dialog.FileName;
                        if (PathFile != "")
                        {
                            FTP.FTPSendFile(PathFile, $"Member_{TBTeacherNo.Text}.pdf");
                            if (BankTeacher.Class.ProtocolSharing.FileZilla.StatusReturn == true)
                            {
                                Class.SQLConnection.InputSQLMSSQL(SQLDefault[15]
                                    .Replace("{TeacherNo}", TBTeacherNo.Text)
                                    .Replace("{PathFile}", FTP.HostplusPathFile + $"Member_{TBTeacherNo.Text}.pdf")
                                    .Replace("{TeacherAddBy}", BankTeacher.Class.UserInfo.TeacherNo));
                                MessageBox.Show("อัพโหลดเอกสารสำเร็จ", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            PathFile = "";
                        }
                    }
                    else
                    {
                        MessageBox.Show("ทำการอัพโหลดเอกสารแล้ว ไม่สามารถดำเนินการส่งเอกสารซ้ำได้", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    BTUploadFIle.Visible = false;
                }
            }
            else
            {
                Class.ProtocolSharing.FileZilla.FileZillaConnection FTP = new Class.ProtocolSharing.FileZilla.FileZillaConnection("RegMember");
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "pdf files(*.pdf)|*.pdf";
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    PathFile = dialog.FileName;
                    if (PathFile != "")
                    {
                        FTP.FTPSendFile(PathFile, $"Member_{TBTeacherNo.Text}.pdf");
                        if (BankTeacher.Class.ProtocolSharing.FileZilla.StatusReturn == true)
                        {
                            Class.SQLConnection.InputSQLMSSQL(SQLDefault[15]
                                .Replace("{TeacherNo}", TBTeacherNo.Text)
                                .Replace("{PathFile}", FTP.HostplusPathFile + $"Member_{TBTeacherNo.Text}.pdf")
                                .Replace("{TeacherAddBy}", BankTeacher.Class.UserInfo.TeacherNo));
                            MessageBox.Show("อัพโหลดเอกสารสำเร็จ", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        PathFile = "";
                    }
                }
                else
                {
                    MessageBox.Show("ทำการอัพโหลดเอกสารแล้ว ไม่สามารถดำเนินการส่งเอกสารซ้ำได้", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                BTUploadFIle.Visible = false;
            }
        }
    }
}
