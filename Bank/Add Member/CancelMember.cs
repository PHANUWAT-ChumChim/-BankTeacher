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

        /// <summary>
        /// <para>[0] SELECT MEMBER INPUT: {Text}</para>
        /// <para>[1] Change Status Member INPUT: {TeacherNoAddBy} {TeacherNo} {Note} {DocStatusNo} {DocUploadPath} {Status}</para>
        /// <para>[2] SELECT Check SavingAmount INPUT: {TeacherNo}</para>
        /// <para>[3] Select MemberResignation INPUT: {Date}</para>
        /// <para>[4] SELECT Year INPUT: </para>
        /// <para>[5] SELECT MEMBER (Enter) INPUT: {Text}</para>
        /// <para>[6] Search Member and SavingAmount - RemainAmount in Guarantor INPUT: {TeacherNoNotLike}  {Text}</para>
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
             //[1] Change Status Member INPUT: {TeacherNoAddBy} {TeacherNo} {Note} {DocStatusNo} {DocUploadPath} {Status}
             "INSERT INTO EmployeeBank.dbo.tblMemberResignation (TeacherNoAddBy,TeacherNo,Date,Note,DocStatusNo,DocUploadPath)  \r\n " +
             "VALUES ('{TeacherNoAddBy}','{TeacherNo}',CURRENT_TIMESTAMP,'{Note}','{DocStatusNo}','{DocUploadPath}');  \r\n " +
             "   \r\n " +
             "UPDATE EmployeeBank.dbo.tblMember  \r\n " +
             "SET MemberStatusNo = '{Status}' \r\n " +
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
          "WHERE a.TeacherNo LIKE '{TeacherNo}%'"
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
                    .Replace("{TeacherNoNotLike}", $"and a.TeacherNo NOT LIKE '{TBTeacherNo.Text}'"), "หุ้นสะสม");
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
                    TBTeacherNo.Enabled = true;
                    Checkmember(false);
                    BSave.Enabled = true;
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
            }
        }

        private void BSave_Click(object sender, EventArgs e)
        {
            if (/*CheckBCancel == false && */TBTeacherName.Text != "")
            {
                
                if (TBTeacherNo.Text != "")
                {
                    DataTable dtCheckSavingAmount = Class.SQLConnection.InputSQLMSSQL(SQLDefault[2]
                    .Replace("{TeacherNo}", TBTeacherNo.Text));
                    if (Convert.ToInt32(dtCheckSavingAmount.Rows[0][0].ToString()) < 1)
                    {
                        //Delete File
                        bool Connect;
                        var smb = new BankTeacher.Class.ProtocolSharing.ConnectSMB.SmbFileContainer("RegMember");
                        Connect = smb.FileConncet(dtCheckSavingAmount.Rows[0][2].ToString());
                        if (Connect)
                        {
                            //System.IO.File.Delete($@"\\LAPTOP-A1H4E5P4\ShareFileTestSBM\RegMember\Member_{TBTeacherNo.Text}.pdf");
                            DataTable dt = Class.SQLConnection.InputSQLMSSQL("UPDATE EmployeeBank.dbo.tblMember \r\n " +
                               "set DocStatusNo = '2',DocUploadPath = '' \r\n " +
                               "where TeacherNo = '{TeacherNo}'"
                               .Replace("{TeacherNo}", TBTeacherNo.Text));
                            Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[1]
                            .Replace("{TeacherNoAddBy}", Class.UserInfo.TeacherNo)
                            .Replace("{TeacherNo}", TBTeacherNo.Text)
                            .Replace("{Note}", TBNote.Text)
                            .Replace("{DocStatusNo}", "2")
                            .Replace("{DocUploadPath}", dtCheckSavingAmount.Rows[0][2].ToString())
                            .Replace("{Status}", "2"));
                            MessageBox.Show("ยกเลิกผู้ใช้เรียบร้อย", "System", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CheckBCancel = true;
                            Checkmember(true);
                            imgeLocation = "";
                            TBTeacherNo.Text = "";
                            TBTeacherName.Text = "";
                            TBNote.Text = "";
                            BSave.Enabled = false;
                        }
                        else { MessageBox.Show("กรุณาส่งเอกสารสมัครสมาชิก เพื่อยืนยันการสมัคร", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                    }
                    else
                    {
                        if ((MessageBox.Show("ยอดเงินคงเหลือของท่านยังอยู่ในระบบ \r\n ต้องถอนเงินออกจากระบบก่อน", "แจ้งเตือน", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes))
                        {
                            AmountOff FAmountOff = new AmountOff();
                            Menu FMenu = new Menu();
                            FAmountOff.FormBorderStyle = FormBorderStyle.Sizable;
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
            }
        }

        private void BOpenFile_Click(object sender, EventArgs e)
        {
            if (Check)
            {
                if (StatusBoxFile == 0)
                {

                    try
                    {
                        OpenFileDialog dialog = new OpenFileDialog();
                        dialog.Filter = "pdf files(*.pdf)|*.pdf";
                        if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            imgeLocation = dialog.FileName;
                        }
                        if (imgeLocation != "")
                        {
                            //LScan_Cancel.Text = "Scan( พบไฟล์ )";
                            //BOpenFile_Cancel.Text = "ส่งไฟล์";
                            StatusBoxFile = 1;
                            var smb = new SmbFileContainer("CancelMember");
                            if (smb.IsValidConnection())
                            {
                                String Return = smb.SendFile(imgeLocation, "CancelMember_" + TBTeacherNo.Text + ".pdf" , TBTeacherNo.Text , 2 , BankTeacher.Class.UserInfo.TeacherNo);
                                if (Return.Contains("อัพโหลดสำเร็จ"))
                                {
                                    MessageBox.Show(Return, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    //BOpenFile_Cancel.Text = "เปิดไฟล์";
                                    LScan_Cancel.Text = Return;
                                    LScan_Cancel.ForeColor = Color.Green;
                                    imgeLocation = "";
                                    BDeleteFile_Cancel.Enabled = true;
                                }
                            }
                            else
                            {
                                MessageBox.Show("ไม่สามารถสร้างไฟล์ในที่นั้นได้", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }

                    }
                    catch
                    {
                        MessageBox.Show("An Error Occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (StatusBoxFile == 1)
                {
                    MessageBox.Show("ทำการส่งไฟล์แล้ว ไม่สามารถดำเนินการส่งไฟล์ซ้ำได้", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("กรุณากรอก รหัสผู้ใช้ก่อน", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BDeleteFile_Click(object sender, EventArgs e)
        {
            //LScan_Cancel.Text = "ยังไม่ได้อัพโหลดไฟล์";
            //LScan_Cancel.ForeColor = Color.Black;
            //imgeLocation = "";
            //BDeleteFile_Cancel.Enabled = false;
            //StatusBoxFile = 0;
        }

        private void CBYear_HistoryCancel_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = Class.SQLConnection.InputSQLMSSQL(SQLDefault[3].Replace("{Date}", CBYear_HistoryCancel.Text));
            if (dt.Rows.Count != 0)
            {
                DGV_HistoryCancel.Rows.Clear();
                for (int x = 0; x < dt.Rows.Count; x++)
                    DGV_HistoryCancel.Rows.Add((Convert.ToDateTime(dt.Rows[x][0].ToString())).ToString("yyyy-MM-dd"), dt.Rows[x][1].ToString(), dt.Rows[x][2].ToString(), dt.Rows[x][3].ToString());
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
            if(e.KeyCode == Keys.Escape)
            {
                if(TBTeacherNo.Text.Length != 0)
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
                    Checkmember(true);
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
            BSearch.Enabled = tf;
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
    }
}
