using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace BankTeacher.Bank.Add_Member
{
    public partial class infoMeber : Form
    {
        public infoMeber()
        {
            InitializeComponent();
        }
        private void infoMeber_SizeChanged(object sender, EventArgs e)
        {
            Class.FromSettingMedtod.ChangeSizePanal(this, PL);
        }
        /// <summary> 
        /// SQLDefault 
        /// <para>[0] Get InfoMember INPUT: {TeacherNo}  </para> 
        /// <para>[1]Search Teacher INPUT: {TeacherNotLike}</para>
        /// <para>[2]Save Edit Bsave INPUT: {Amount}  {TeacherNo}</para>
        /// <para>[3]  Select Detail Memner INPUT: {TeacherNo}  </para>
        /// <para>[4] Chcek flie INPUT : {TeacherNo}</para>
        /// <para>[5] Insert File INPUT : {TeacharNo} {PathFile} {PathFile} {TeacherAddBy}</para>
        /// <para>[6] Update Status RemoveFile INPUT: {TeacherRemoveBy} , {ID} , {TeacherNo} </para>
        /// <para>[7]  Update ChargeAmount  INPUT: {Amount} , {TeacherNo} </para>
        /// </summary> 
        private String[] SQLDefault = new String[]
        { 
           //[0]Get InfoMember INPUT: {TeacherNo}  
           "SELECT CAST(ISNULL(c.PrefixNameFull , '') + b.Fname + ' ' + b.Lname as NVARCHAR) as Name , f.Name as TeacherAddByName ,CAST(d.MemberStatusName as NVARCHAR) , CAST(a.DateAdd as date)  \r\n " +
          ", a.StartAmount , e.SavingAmount \r\n " +
          "FROM EmployeeBank.dbo.tblMember as a \r\n " +
          "LEFT JOIN Personal.dbo.tblTeacherHis as b on a.TeacherNo = b.TeacherNo \r\n " +
          "LEFT JOIN BaseData.dbo.tblPrefix as c on b.PrefixNo = c.PrefixNo \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblMemberStatus as d on a.MemberStatusNo = d.MemberStatusNo \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblShare as e on a.TeacherNo = e.TeacherNo \r\n " +
          "LEFT JOIN (SELECT a.TeacherAddBy , CAST(ISNULL(c.PrefixNameFull , '') + b.Fname + ' ' + b.Lname as NVARCHAR) as Name \r\n " +
          "	FROM EmployeeBank.dbo.tblMember as a  \r\n " +
          "	LEFT JOIN Personal.dbo.tblTeacherHis as b on a.TeacherAddBy = b.TeacherNo \r\n " +
          "	LEFT JOIN BaseData.dbo.tblPrefix as c on b.PrefixNo = c.PrefixNo \r\n " +
          "	GROUP BY a.TeacherAddBy, CAST(ISNULL(c.PrefixNameFull , '') + b.Fname + ' ' + b.Lname as NVARCHAR)) as f on a.TeacherAddBy = f.TeacherAddBy \r\n " +
          "WHERE a.TeacherNo = '{TeacherNo}' and a.MemberStatusNo = 1  \r\n\r\n"+

           "SELECT COUNT(b.BillNo)  \r\n " +
          "FROM EmployeeBank.dbo.tblMember as a \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblBill as b on a.TeacherNo = b.TeacherNo \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblBillDetail as c on b.BillNo = c.BillNo \r\n " +
          "WHERE a.TeacherNo = '{TeacherNo}' and c.TypeNo !=3 and b.Cancel != 2\r\n " +
          " \r\n " +
          "SELECT COUNT(c.WithDrawNo) \r\n " +
          "FROM EmployeeBank.dbo.tblMember as a \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblShare as b on a.TeacherNo = b.TeacherNo \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblShareWithdraw as c on b.ShareNo = c.ShareNo \r\n " +
          "WHERE a.TeacherNo = '{TeacherNo}'"
           ,
           //[1]Search Teacher INPUT: {TeacherNotLike}
           "SELECT a.TeacherNo , CAST(ISNULL(c.PrefixNameFull , '') + b.Fname + ' ' + Lname as NVARCHAR) \r\n " +
          "FROM EmployeeBank.dbo.tblMember as a \r\n " +
          "LEFT JOIN Personal.dbo.tblTeacherHis as b on a.TeacherNo = b.TeacherNo \r\n " +
          "LEFT JOIN BaseData.dbo.tblPrefix as c on b.PrefixNo = c.PrefixNo \r\n " +
          "WHERE (a.TeacherNo LIKE '%{Text}%'  or CAST(ISNULL(c.PrefixNameFull , '') + b.Fname + ' ' + Lname as NVARCHAR) LIKE '%{Text}%')and a.MemberStatusNo = 1 {TeacherNotLike};"
           ,
           //[2]Save Edit Bsave INPUT: {Amount}  {TeacherNo}
           "-- BSave Edit \r\n " +
          "UPDATE EmployeeBank.dbo.tblMember  \r\n " +
          "SET StartAmount = {Amount}\r\n " +
          "WHERE TeacherNo = '{TeacherNo}'; \r\n " +
          " \r\n " +
          "UPDATE EmployeeBank.dbo.tblShare \r\n " +
          "SET SavingAmount = {Amount} \r\n " +
          "WHERE TeacherNo = '{TeacherNo}';"
           ,
           //[3]  Select Detail Memner INPUT: {TeacherNo} 
          "SELECT a.TeacherNo ,CAST(ISNULL(b.PrefixName+' ','')+a.Fname +' '+ a.Lname as NVARCHAR)AS Name,f.TeacherAddBy,CAST(ISNULL(b.PrefixName+' ','')+h.Fname +' '+ h.Lname as NVARCHAR) as NameTadd,a.IdNo, ISNULL(IIF(a.cNo = '','-',a.cNo),'-') as cNo,ISNULL(IIF(CAST(a.cMu as nvarchar) = '','-',CAST(a.cMu as nvarchar)),'-') as cMu ,ISNULL(IIF(CAST(c.TumBonName as nvarchar) = '','-',CAST(c.TumBonName as nvarchar)),'-') as TumBonName,ISNULL(IIF(CAST(d.AmPhurName as nvarchar) = '','-',CAST(d.AmPhurName as nvarchar)),'-') as AmPhurName,ISNULL(IIF(CAST(e.JangWatLongName as nvarchar) = '','-',CAST(e.JangWatLongName as nvarchar)),'-') as JangWatLongName,ISNULL(IIF(a.TelMobile = '','-',a.TelMobile),'-'),f.StartAmount   \r\n " +
          "FROM Personal.dbo.tblTeacherHis as a \r\n " +
          "LEFT JOIN BaseData.dbo.tblPrefix as b ON a.PrefixNo = b.PrefixNo  \r\n " +
          "LEFT JOIN BaseData.dbo.tblTumBon as c on a.cTumBonNo = c.TumBonNo \r\n " +
          "LEFT JOIN BaseData.dbo.tblAmphur as d on a.cAmPhurNo = d.AmphurNo \r\n " +
          "LEFT JOIN BaseData.dbo.tblJangWat as e on a.cJangWatNo = e.JangWatNo \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblMember as f on a.TeacherNo = f.TeacherNo  \r\n " +
          "LEFT JOIN Personal.dbo.tblTeacherHis as h on f.TeacherAddBy = h.TeacherNo  \r\n " +
          "WHERE a.TeacherNo = '{TeacherNo}'; "
          ,
          //[4] Chcek flie INPUT : {TeacherNo}
           "SELECT c.ID , c.pathFile\r\n " +
          " FROM EmployeeBank.dbo.tblMember as a   \r\n " +
          " LEFT JOIN EmployeeBank.dbo.tblDocStatus as b on a.DocStatusNo = b.DocStatusNo   \r\n " +
          " LEFT JOIN EmployeeBank.dbo.tblFile as c on a.TeacherNo = c.TeacherNo \r\n " +
          " LEFT JOIN EmployeeBank.dbo.tblStatusFileInSystem as d on c.StatusFileInSystem = d.ID \r\n " +
          " WHERE a.TeacherNo = '{TeacherNo}' and c.IsUse = 1 and FiletypeNo = 1 and StatusFileInSystem = 2"
           ,
           //[5] Insert File INPUT : {TeacharNo} {PathFile} {PathFile} {TeacherAddBy}
           "INSERT INTO EmployeeBank.dbo.tblFile(TeacherNo , FiletypeNo , pathFile ,TeacherAddBy,LoanID,DateAddFile,IsUse , TeacherRemoveFileBy ,DateRemoveFile, StatusFileInSystem) \r\n " +
          "VALUES ('{TeacherNo}',1,'{PathFile}','{TeacherAddBy}',null,CURRENT_TIMESTAMP,1,null,null,2); \r\n " +
          " \r\n " +
          "UPDATE EmployeeBank.dbo.tblMember \r\n " +
          "SET DocStatusNo = 1 , DocUploadPath = '{PathFile}'\r\n"+
          "WHERE TeacherNo = '{TeacherNo}'"
           ,
           //[6] Update Status RemoveFile INPUT: {TeacherRemoveBy} , {ID} , {TeacherNo} 
           "UPDATE EmployeeBank.dbo.tblFile \r\n " +
          "SET IsUse = 0, TeacherRemoveFileBy = '{TeacherRemoveBy}', DateRemoveFile = CURRENT_TIMESTAMP , StatusFileInSystem = 1 \r\n " +
          "WHERE ID = '{ID}'; \r\n " +
          " \r\n " +
          "UPDATE EmployeeBank.dbo.tblMember \r\n " +
          "SET DocStatusNo = 2 , DocUploadPath = '' \r\n " +
          "WHERE TeacherNo = '{TeacherNo}'"
           ,
           //[7] Update ChargeAmount  INPUT: {Amount} , {TeacherNo} 
           "update EmployeeBank.dbo.tblBillDetail \r\n " +
           "set Amount = '{Amount}' \r\n " +
           "from EmployeeBank.dbo.tblBillDetail as a \r\n " +
                "inner join EmployeeBank.dbo.tblBill s on \r\n " +
                   "a.BillNo = s.BillNo \r\n " +
            "WHERE s.TeacherNo = '{TeacherNo}' AND a.TypeNo = 3"
           ,
        };

        private void infoMeber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape || (e.KeyCode == Keys.Enter))
            {
                if (TBTeacherNo.Text.Length != 0)
                {
                    TBTeacherName.Clear();
                    TBTeacherNo.Clear();
                    TBNameInfo.Clear();
                    TBTeacherAddByName.Clear();
                    TBMemberStatus.Clear();
                    TBDateAdd.Clear();
                    TBStartAmount.Clear();
                    TBSavingAmount.Clear();
                    SavingAmountStart = "";
                    TBStartAmount.Enabled = false;
                    BTOpenFile.Enabled = false;
                    Checkmember(true);
                    tabControl1.SelectedIndex = 0;
                }
                else
                {
                    BExitForm_Click(new object(), new EventArgs());
                }
            }
        }

        private void BExitForm_Click(object sender, EventArgs e)
        {
            BankTeacher.Class.FromSettingMedtod.ReturntoHome(this);
        }

        private void Checkmember(bool tf)
        {
            TBTeacherNo.Enabled = tf;
        }
        private void BTSearchTeacher_Click(object sender, EventArgs e)
        {            
            String NotLike = "";
            if (TBTeacherNo.Text.Length != 0)
            {
                NotLike += " and a.TeacherNo NOT LIKE " + $"'{TBTeacherNo.Text}'";
            }
            Bank.Search IN = new Bank.Search(SQLDefault[1]
                    .Replace("{TeacherNotLike}", NotLike));
            IN.ShowDialog();
            if (Bank.Search.Return[0] != "")
            {
                TBTeacherNo.Text = Bank.Search.Return[0];
                TBTeacherNo_KeyDown(sender, new KeyEventArgs(Keys.Enter));
                tabControl1.SelectedIndex = 0;
            }
        }
        String SavingAmountStart = "";
        private void TBTeacherNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TBTeacherNo.Text = TBTeacherNo.Text.Replace("t", "T");
                DataSet dsInfoMember = Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[0]
                    .Replace("{TeacherNo}", TBTeacherNo.Text) +
                    "\r\n" +
                    SQLDefault[4]
                    .Replace("{TeacherNo}", TBTeacherNo.Text));
                if (dsInfoMember.Tables[0].Rows.Count != 0 && dsInfoMember.Tables[1].Rows.Count != 0 && dsInfoMember.Tables[2].Rows.Count != 0)
                {
                    TBTeacherName.Text = dsInfoMember.Tables[0].Rows[0][0].ToString();
                    TBNameInfo.Text = dsInfoMember.Tables[0].Rows[0][0].ToString();
                    TBTeacherAddByName.Text = dsInfoMember.Tables[0].Rows[0][1].ToString();
                    TBMemberStatus.Text = dsInfoMember.Tables[0].Rows[0][2].ToString();
                    TBDateAdd.Text = dsInfoMember.Tables[0].Rows[0][3].ToString();
                    TBStartAmount.Text = dsInfoMember.Tables[0].Rows[0][4].ToString();
                    TBSavingAmount.Text = dsInfoMember.Tables[0].Rows[0][5].ToString();
                    SavingAmountStart = dsInfoMember.Tables[0].Rows[0][4].ToString();

                    if (Convert.ToInt32(dsInfoMember.Tables[1].Rows[0][0].ToString()) == 0 && Convert.ToInt32(dsInfoMember.Tables[2].Rows[0][0].ToString()) == 0 && TBStartAmount.Text == TBSavingAmount.Text)
                    {
                        TBStartAmount.Enabled = true;
                    }
                    tabControl1.Enabled = true;
                    Checkmember(false);
                    // Check Status UploadFile
                    if(dsInfoMember.Tables[3].Rows.Count != 0)
                    {
                        if (dsInfoMember.Tables[3].Rows[0][1].ToString() != "")
                        {
                            label12.Text = "อัพโหลดไฟล์เรียบร้อย";
                            label12.ForeColor = Color.Green;
                            BTOpenFile.Enabled = true;
                            BTRemoveFile.Enabled = true;
                            BTUploadFile_Reg.Enabled = false;
                        }
                        else
                        {
                            label12.Text = "ยังไม่ได้อัพโหลดไฟล์";
                            label12.ForeColor = Color.Red;
                            BTOpenFile.Enabled = false;
                            BTRemoveFile.Enabled = false;
                        }
                    }
                }
            }
            else if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
            {
                TBTeacherNo.Clear();
                TBTeacherName.Clear();
                TBNameInfo.Clear();
                TBTeacherAddByName.Clear();
                TBMemberStatus.Clear();
                TBDateAdd.Clear();
                TBStartAmount.Clear();
                TBSavingAmount.Clear();
                SavingAmountStart = "";
                TBStartAmount.Enabled = false;
                BTOpenFile.Enabled = false;
                Checkmember(true);
                tabControl1.SelectedIndex = 0;
            }
        }
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            BankTeacher.Class.Print.PrintPreviewDialog.PrintMember(e
                , SQLDefault[3], BankTeacher.Bank.Menu.Date[2]
                , BankTeacher.Bank.Menu.Monthname
                , (Convert.ToInt32(BankTeacher.Bank.Menu.Date[0]) + 543).ToString()
                , TBTeacherNo.Text, TBTeacherName.Text
                , checkBox_scrip.Checked
                , checkBox_copy.Checked);
        }
        private void BTPrint_Click(object sender, EventArgs e)
        {
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }
        // UpLoadFile in the ComputerServer
        private void BTUploadFile_Click(object sender, EventArgs e)
        {
            String PathFile = null;
            DataTable dtChackStatusFile = Class.SQLConnection.InputSQLMSSQL(SQLDefault[4].Replace("{TeacherNo}", TBTeacherNo.Text));
            if(dtChackStatusFile.Rows.Count != 0)
            {
                if (dtChackStatusFile.Rows[0][1].ToString() == "")
                {
                    SendFile(PathFile);
                }
                else
                {
                    MessageBox.Show("ทำการอัพโหลดเอกสารแล้ว ไม่สามารถดำเนินการส่งเอกสารซ้ำได้", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                SendFile(PathFile);
            }
        }
        private void SendFile(String PathFile)
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
                    StatusEnableBT(false);
                    FTP.FTPSendFile(PathFile, $"Member_{TBTeacherNo.Text}.pdf");
                    if (BankTeacher.Class.ProtocolSharing.FileZilla.StatusReturn == true)
                    {
                        StatusEnableBT(true);
                        Class.SQLConnection.InputSQLMSSQL(SQLDefault[5]
                            .Replace("{TeacherNo}", TBTeacherNo.Text)
                            .Replace("{PathFile}", FTP.HostplusPathFile + $"Member_{TBTeacherNo.Text}.pdf")
                            .Replace("{TeacherAddBy}", BankTeacher.Class.UserInfo.TeacherNo));
                        label12.Text = "อัพโหลดไฟล์เรียบร้อย";
                        label12.ForeColor = Color.Green;
                        MessageBox.Show("อัพโหลดเอกสารสำเร็จ", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                        BTUploadFile_Reg.Enabled = true;
                    Cursor.Current = Cursors.Default;
                }
            }
        }
        // OpenFile in the ComputerServer
        private void BTOpenFile_Click(object sender, EventArgs e)
        {
            DataTable dt = Class.SQLConnection.InputSQLMSSQL(SQLDefault[4]
                .Replace("{TeacherNo}", TBTeacherNo.Text));
            if(dt.Rows.Count != 0)
            {
                if (TBTeacherNo.Text != "")
                {
                    BankTeacher.Class.ProtocolSharing.FileZilla.FileZillaConnection FTP = new BankTeacher.Class.ProtocolSharing.FileZilla.FileZillaConnection("RegMember");
                    StatusEnableBT(false);
                    FTP.FTPOpenFile($"Member_{TBTeacherNo.Text}.pdf");
                    StatusEnableBT(true);
                }
            }
            else
            {
                label12.Text = "ยังไม่ได้อัพโหลดไฟล์";
                label12.ForeColor = Color.Red;
                BTOpenFile.Enabled = false;
                BTRemoveFile.Enabled = false;
            }
        }
        // Remove File in the ComputerServer
        public void BTRemoveFile_Click(object sender, EventArgs e)
        {
            DataTable dt = BankTeacher.Class.SQLConnection.InputSQLMSSQL(SQLDefault[4]
                .Replace("{TeacherNo}", TBTeacherNo.Text));
            if(dt.Rows.Count != 0)
            {
                StatusEnableBT(false);
                Class.ProtocolSharing.FileZilla.FileZillaConnection FTP = new Class.ProtocolSharing.FileZilla.FileZillaConnection("RegMember");
                FTP.FTPRemoveFile("Member_"+ TBTeacherNo.Text +".pdf");
                StatusEnableBT(true);
                if(BankTeacher.Class.ProtocolSharing.FileZilla.StatusReturn == true)
                {
                    BankTeacher.Class.SQLConnection.InputSQLMSSQL(SQLDefault[6]
                        .Replace("{TeacherRemoveBy}" , BankTeacher.Class.UserInfo.TeacherNo)
                        .Replace("{ID}", dt.Rows[0][0].ToString())
                        .Replace("{TeacherNo}", TBTeacherNo.Text));
                    MessageBox.Show("ลบเอกสารสำเร็จ","ระบบ",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    label12.Text = "ยังไม่ได้อัพโหลดไฟล์";
                    label12.ForeColor = Color.Red;
                    BTOpenFile.Enabled = false;
                    BTRemoveFile.Enabled = false;
                }
            }
        }
        private void StatusEnableBT(bool Status)
        {
            BTOpenFile.Enabled = Status;
            BTRemoveFile.Enabled = Status;
            BTUploadFile_Reg.Enabled = Status;
        }
        private void infoMeber_KeyUp(object sender, KeyEventArgs e)
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
