using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static BankTeacher.Class.ProtocolSharing.ConnectSMB;

namespace BankTeacher.Bank.Add_Member
{
    public partial class Member : Form
    {
        public Member()
        {
            InitializeComponent();
            TBStartAmountShare_Reg.Text = BankTeacher.Bank.Menu.startAmountMin.ToString();
        }
        // Resize panle center form
        private void membership_SizeChanged(object sender, EventArgs e)
        {
            Class.FromSettingMedtod.ChangeSizePanal(this, panel1);
            TBTeacherNo_Reg.Select();
        }
        /// <summary>
        /// SQLDafault
        /// <para>[0] Insert Teacher Data INPUT:{TeacherNo}{TeacherAddBy}, {StartAmount} </para>
        /// <para>[1] SELECT Member  INPUT:{Text} </para>
        /// <para>[2]  Select Detail Memner INPUT: {TeacherNo} </para>
        /// <para>[3] INSERT Member To Member  Bill BillDetail  INPUT: {TeacherNo} {TeacherNoAddBy} {StartAmount} {Mount} {Year}  </para>
        /// <para>[4] Print info_Member  INPUT : {TeacherNo} </para>
        /// <para>[6] Chcek flie INPUT : {TeacherNo} </para>
        /// <para>[7] Insert File INPUT : {TeacharNo} {PathFile} {PathFile} {TeacherAddBy} </para> 
        /// <para>[8] Update File INPUT : {TeacharNo} {PathFile}  </para> 
        /// </summary>
        private String[] SQLDefault = new String[]
        {
		   //[0] Insert Teacher Data INPUT:{TeacherNo},{TeacherAddBy},{StartAmount} 
		   "INSERT INTO EmployeeBank.dbo.tblMember(TeacherNo,TeacherAddBy,StartAmount,DateAdd) \r\n"+
           "VALUES('{TeacherNo}','{TeacherAddBy}',{StartAmount}, CURRENT_TIMESTAMP); \r\n\r\n"
            ,
           //[1] SELECT Member  INPUT:{Text}
           "SELECT TeacherNo,Name,null,Fname  \r\n " +
          "FROM(SELECT ISNULL(b.TeacherNo , a.TeacherNo) as TeacherNo ,  CAST(ISNULL(c.PrefixName+' ','') + Fname +' '+ Lname as NVARCHAR)AS NAME, Fname , b.MemberStatusNo  \r\n " +
          "FROM Personal.dbo.tblTeacherHis as a  \r\n " +
          "FULL OUTER JOIN EmployeeBank.dbo.tblMember as b on a.TeacherNo = b.TeacherNo  \r\n " +
          "LEFT JOIN BaseData.dbo.tblPrefix as c ON a.PrefixNo = c.PrefixNo   \r\n " +
          "WHERE (a.TeacherNo LIKE '%{Text}%' or CAST(Fname +' '+ Lname as NVARCHAR) LIKE '%{Text}%') and a.IsUse = 1 ) as a  \r\n " +
          "WHERE a.MemberStatusNo = 2 or a.MemberStatusNo IS NULL  \r\n " +
          "ORDER BY a.Fname; "
           ,
          //[2]  Select Detail Memner INPUT: {TeacherNo} 
          "SELECT a.TeacherNo ,CAST(ISNULL(b.PrefixName+' ','')+a.Fname +' '+ a.Lname as NVARCHAR)AS Name,f.TeacherAddBy,CAST(ISNULL(b.PrefixName+' ','')+h.Fname +' '+ h.Lname as NVARCHAR) as NameTadd,a.IdNo, IIF(a.cNo != null,'-',a.cNo) as cNo,IIF(CAST(a.cMu as nvarchar) != null,'-',a.cMu) as cMu,IIF(CAST(c.TumBonName as nvarchar) != null,'-',CAST(c.TumBonName as nvarchar)) as TumBonName,IIF(CAST(d.AmPhurName as nvarchar) != null,'-',CAST(d.AmPhurName as nvarchar)) as AmPhurName,IIF(CAST(e.JangWatLongName as nvarchar) != null,'-',CAST(e.JangWatLongName as nvarchar)) as JangWatLongName,a.TelMobile,f.StartAmount \r\n " +
          "FROM Personal.dbo.tblTeacherHis as a \r\n " +
          "LEFT JOIN BaseData.dbo.tblPrefix as b ON a.PrefixNo = b.PrefixNo \r\n " +
          "LEFT JOIN BaseData.dbo.tblTumBon as c on a.cTumBonNo = c.TumBonNo \r\n " +
          "LEFT JOIN BaseData.dbo.tblAmphur as d on a.cAmPhurNo = d.AmphurNo \r\n " +
          "LEFT JOIN BaseData.dbo.tblJangWat as e on a.cJangWatNo = e.JangWatNo \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblMember as f on a.TeacherNo = f.TeacherNo \r\n " +
          "LEFT JOIN Personal.dbo.tblTeacherHis as h on f.TeacherAddBy = h.TeacherNo \r\n " +
          "WHERE a.TeacherNo = '{TeacherNo}'; "

          ,
           //[3] INSERT Member To Member  Bill BillDetail  and Insert FileINPUT: {TeacherNo} {TeacherNoAddBy} {StartAmount} {Mount} {Year}  {PathFile}
          "DECLARE @CountTeacher INT; \r\n " +
          "DECLARE @BillNo INT;  \r\n " +
          " \r\n " +
          "SET @CountTeacher = (SELECT Count(TeacherNo) \r\n " +
          "FROM EmployeeBank.dbo.tblMember \r\n " +
          "WHERE TeacherNo = '{TeacherNo}' and MemberStatusNo = 2) ; \r\n " +
          " \r\n " +
          "IF(@CountTeacher > 0) \r\n " +
          "BEGIN \r\n " +
          "UPDATE EmployeeBank.dbo.tblMember \r\n " +
          "SET MemberStatusNo = 1 ,DateAdd = CURRENT_TIMESTAMP,DocStatusNo = 2,DocUploadPath = '{PathFile}' \r\n " +
          "WHERE TeacherNo = '{TeacherNo}'; \r\n " +

          "UPDATE EmployeeBank.dbo.tblShare \r\n " +
          "SET SavingAmount = '{StartAmount}' \r\n " +
          "WHERE TeacherNo = '{TeacherNo}'; \r\n " +

          "INSERT INTO EmployeeBank.dbo.tblBill(TeacherNo, TeacherNoAddBy,TeacherNoPay, DateAdd,TransactionDate)   \r\n " +
          "VALUES('{TeacherNo}','{TeacherNoAddBy}','{TeacherNo}', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP)  \r\n " +

          "SELECT @BillNo = SCOPE_IDENTITY();  \r\n " +

          "INSERT INTO EmployeeBank.dbo.tblBillDetail(BillNo, TypeNo, Amount, Mount, Year,BillDetailPaymentNo) \r\n " +
          "VALUES(@BillNo,3,{StartAmount},{Month},{Year},1) \r\n " +
          " \r\n " +
          "END; \r\n " +
          " \r\n " +
          "ELSE \r\n " +
          "BEGIN \r\n " +
          " \r\n " +
          "INSERT INTO EmployeeBank.dbo.tblMember(TeacherNo, TeacherAddBy, StartAmount, DateAdd,DocStatusNo,DocUploadPath)  \r\n " +
          "VALUES('{TeacherNo}','{TeacherNoAddBy}',{StartAmount},CURRENT_TIMESTAMP,2,'{PathFile}')   \r\n " +

          "INSERT INTO EmployeeBank.dbo.tblShare(TeacherNo, SavingAmount) \r\n " +
          "VALUES('{TeacherNo}',{StartAmount})  \r\n " +

          "INSERT INTO EmployeeBank.dbo.tblBill(TeacherNo, TeacherNoAddBy,TeacherNoPay, DateAdd,TransactionDate)   \r\n " +
          "VALUES('{TeacherNo}','{TeacherNoAddBy}','{TeacherNo}', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP)  \r\n " +

          "SELECT @BillNo = SCOPE_IDENTITY();  \r\n " +

          "INSERT INTO EmployeeBank.dbo.tblBillDetail(BillNo, TypeNo, Amount, Mount, Year,BillDetailPaymentNo) \r\n " +
          "VALUES(@BillNo,3,{StartAmount},{Month},{Year},1) \r\n " +
          " \r\n " +
          "END; \r\n " +

          "--INSERT INTO EmployeeBank.dbo.tblFile(TeacherNo , FiletypeNo , pathFile ,TeacherAddBy,LoanID,DateAddFile,IsUse , TeacherRemoveFileBy ,DateRemoveFile, StatusFileInSystem) \r\n " +
          "--VALUES ('{TeacherNo}',1,'{PathFile}','{TeacherNoAddBy}',null,CURRENT_TIMESTAMP,1,null,null,2);"
          ,
          //[4] Print info_Member  INPUT : {TeacherNo}
          "SELECT a.TeacherNo,a.TeacherAddBy,CAST(c.PrefixName+' '+b.Fname+' '+b.Lname as nvarchar),a.StartAmount,a.DateAdd  \r\n " +
          "FROM EmployeeBank.dbo.tblMember as a  \r\n " +
          "LEFT JOIN Personal.dbo.tblTeacherHis as b on a.TeacherNo = b.TeacherNo  \r\n " +
          "LEFT JOIN BaseData.dbo.tblPrefix as c on b.PrefixNo = c.PrefixNo  \r\n " +
          "WHERE a.TeacherNo = '{TeacherNo}';"
            ,
          //[5] SELECT Member (Enter) INPUT:{Text}
           "SELECT TeacherNo,Name,null,CAST(Fname as NVARCHAR)  \r\n " +
          "FROM(SELECT ISNULL(b.TeacherNo , a.TeacherNo) as TeacherNo ,  CAST(ISNULL(c.PrefixName+' ','') + Fname +' '+ Lname as NVARCHAR)AS NAME, Fname , b.MemberStatusNo  \r\n " +
          "FROM Personal.dbo.tblTeacherHis as a  \r\n " +
          "FULL OUTER JOIN EmployeeBank.dbo.tblMember as b on a.TeacherNo = b.TeacherNo  \r\n " +
          "LEFT JOIN BaseData.dbo.tblPrefix as c ON a.PrefixNo = c.PrefixNo   \r\n " +
          "WHERE (a.TeacherNo = '{Text}') and a.IsUse = 1 ) as a  \r\n " +
          "WHERE a.MemberStatusNo = 2 or a.MemberStatusNo IS NULL  \r\n " +
          "ORDER BY a.Fname; "
           ,
           //[6] Chcek flie INPUT : {TeacherNo}
           "SELECT c.ID , c.pathFile\r\n " +
          " FROM EmployeeBank.dbo.tblMember as a   \r\n " +
          " LEFT JOIN EmployeeBank.dbo.tblDocStatus as b on a.DocStatusNo = b.DocStatusNo   \r\n " +
          " LEFT JOIN EmployeeBank.dbo.tblFile as c on a.TeacherNo = c.TeacherNo \r\n " +
          " LEFT JOIN EmployeeBank.dbo.tblStatusFileInSystem as d on c.StatusFileInSystem = d.ID \r\n " +
          " WHERE a.TeacherNo = '{TeacherNo}' and c.IsUse = 1 and FiletypeNo = 1 and StatusFileInSystem = 2"
           ,
           //[7] Insert File INPUT : {TeacharNo} {PathFile} {PathFile} {TeacherAddBy}
           "INSERT INTO EmployeeBank.dbo.tblFile(TeacherNo , FiletypeNo , pathFile ,TeacherAddBy,LoanID,DateAddFile,IsUse , TeacherRemoveFileBy ,DateRemoveFile, StatusFileInSystem) \r\n " +
          "VALUES ('{TeacherNo}',1,'{PathFile}','{TeacherAddBy}',null,CURRENT_TIMESTAMP,1,null,null,2); \r\n " +
          " \r\n " +
          "UPDATE EmployeeBank.dbo.tblMember \r\n " +
          "SET DocStatusNo = 1 , DocUploadPath = '{PathFile}'\r\n"+
          "WHERE TeacherNo = '{TeacherNo}'"
           ,
          //[8] Update File INPUT : {TeacharNo} {PathFile} 
          "UPDATE EmployeeBank.dbo.tblMember \r\n " +
          "SET DocStatusNo = 2 , DocUploadPath = '{PathFile}'\r\n"+
          "WHERE TeacherNo = '{TeacherNo}'"
            ,
        };
        // SearchTeacher when Click botton
        private void BTSearchTeacher_Click(object sender, EventArgs e)
        {
            Search IN = new Search(SQLDefault[1]);
            IN.ShowDialog();
            if (Search.Return[0] != "")
            {
                TBTeacherNo_Reg.Text = Search.Return[0];
                TBTeacherName_Reg.Text = Search.Return[1];
                TBTeacherNo_KeyDown(sender, new KeyEventArgs(Keys.Enter));
            }
        }
        private void TBStartAmountShare_Reg_TextChanged(object sender, EventArgs e)
        {
            BankTeacher.Class.FromSettingMedtod.ProtectedCtrlVTB(TBStartAmountShare_Reg);
            if (TBStartAmountShare_Reg.Text != "" && TBTeacherNo_Reg.Text != "")
                BTSave_Reg.Enabled = true;
            else
                BTSave_Reg.Enabled = false;
        }
        // Available values| TB /
        private void TBStartAmountShare_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && (e.KeyChar != '\b'))
            {
                e.Handled = true;
            }
        }
        // Available values|  SQLDefault[1] / TB //
        private void BSave_Click_1(object sender, EventArgs e)
        {
            if (TBTeacherName_Reg.Text != "")
            {
                // Get Amount of Filling user
                int AmountShare = Convert.ToInt32(TBStartAmountShare_Reg.Text);
                // Search the User inofmation in the Database
                DataTable dt = Class.SQLConnection.InputSQLMSSQL(SQLDefault[1].Replace("{TeacherNo}", TBTeacherNo_Reg.Text));
                if (AmountShare >= BankTeacher.Bank.Menu.startAmountMin && AmountShare <= BankTeacher.Bank.Menu.startAmountMax)
                {
                    DialogResult dialogResult = MessageBox.Show("ยืนยันการสมัคร", "สมัครสมาชิก", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (dialogResult == DialogResult.Yes)
                    {
                        // Enable using
                        TBStartAmountShare_Reg.Enabled = false;
                        BTSave_Reg.Enabled = false;
                        Checkmember(true);
                        // Send scrip sql
                        Class.SQLConnection.InputSQLMSSQL(SQLDefault[3].Replace("{TeacherNo}", TBTeacherNo_Reg.Text)
                        .Replace("{TeacherNoAddBy}", BankTeacher.Class.UserInfo.TeacherNo)
                        .Replace("{StartAmount}", TBStartAmountShare_Reg.Text)
                        .Replace("{Month}", BankTeacher.Bank.Menu.Date[1])
                        .Replace("{Year}", BankTeacher.Bank.Menu.Date[0])
                        .Replace("{PathFile}", ""));
                        // Message
                        MessageBox.Show("สมัครเสร็จสิ้น", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // Print
                        printDocument1.DocumentName = $"TeacherID{TBTeacherNo_Reg.Text}";
                        printDocument1.Print();
                        TBTeacherNo_Reg.Clear();
                        TBTeacherName_Reg.Clear();
                    }
                    else
                    {
                        MessageBox.Show("ยกเลิกการสมัคร", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show($"ไม่สามารถสมัครสมาชิกได้เนื่องจาก \r\n ราคาหุ้นเริ่มต้นต่ำหรือสูงเกินไป \r\n โปรดแก้ไข ราคาหุ้นขั้นต่ำ {BankTeacher.Bank.Menu.startAmountMin} หรือ สูงสุด {BankTeacher.Bank.Menu.startAmountMax}", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
        }
        // Print
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            BankTeacher.Class.Print.PrintPreviewDialog.PrintMember(e,SQLDefault[2],BankTeacher.Bank.Menu.Date[2],BankTeacher.Bank.Menu.Monthname,(Convert.ToInt32(BankTeacher.Bank.Menu.Date[0]) + 543).ToString(),TBTeacherNo_Reg.Text,TBStartAmountShare_Reg.Text,true,true);
        }
        private void TBTeacherNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TBTeacherNo_Reg.Text = TBTeacherNo_Reg.Text.Replace("t", "T");
                DataSet ds = Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[5].Replace("{Text}", TBTeacherNo_Reg.Text));
                if(ds.Tables[0].Rows.Count != 0)
                {
                    TBTeacherName_Reg.Text = ds.Tables[0].Rows[0][1].ToString();
                    Checkmember(false);
                    TBStartAmountShare_Reg.Enabled = true;
                }
            }
            else if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
            {
                TBTeacherName_Reg.Clear();
                Checkmember(true);
            }
        }
        private void Checkmember(bool tf)
        {
            TBTeacherNo_Reg.Enabled = tf;
        }
        private void BExitForm_Click(object sender, EventArgs e)
        {
            BankTeacher.Class.FromSettingMedtod.ReturntoHome(this);
        }
        private void Member_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape)
            BExitForm_Click(new object(), new EventArgs());
        }
        private void TBTeacherNo_Reg_EnabledChanged(object sender, EventArgs e)
        {
            if (TBTeacherNo_Reg.Enabled == true)
            {
                BTSave_Reg.Enabled = false;
            }
            else
            {
                BTSave_Reg.Enabled = true;
            }
        }
    }
}