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

        /// <summary> 
        /// SQLDefault 
        /// <para>[0] Get InfoMember INPUT: {TeacherNo}  </para> 
        /// <para>[1] Search Teacher INPUT: {TeacherNotLike}</para>
        /// <para>[2] Check Bill Teacher Have Ever Paid INPUT: {TeacherNo}</para>
        /// <para>[3] Save Edit Bsave INPUT: {Amount}  {TeacherNo} </para>
        /// <para>[4] Select Detail Memner INPUT: {TeacherNo} </para>
        /// <para>[5] Chcek flie INPUT : {TeacherNo} </para>
        /// <para>[6] update file INPUT : {TeacharNo} {num} {PathFile} </para>
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
          "WHERE a.TeacherNo = '{TeacherNo}' and a.MemberStatusNo = 1 "
           ,

           //[1]Search Teacher INPUT: {TeacherNotLike}
           "SELECT a.TeacherNo , CAST(ISNULL(c.PrefixNameFull , '') + b.Fname + ' ' + Lname as NVARCHAR) \r\n " +
          "FROM EmployeeBank.dbo.tblMember as a \r\n " +
          "LEFT JOIN Personal.dbo.tblTeacherHis as b on a.TeacherNo = b.TeacherNo \r\n " +
          "LEFT JOIN BaseData.dbo.tblPrefix as c on b.PrefixNo = c.PrefixNo \r\n " +
          "WHERE (a.TeacherNo LIKE '%{Text}%'  or CAST(ISNULL(c.PrefixNameFull , '') + b.Fname + ' ' + Lname as NVARCHAR) LIKE '%{Text}%')and a.MemberStatusNo = 1 {TeacherNotLike};"
           ,
           //[2] Check Bill Teacher Have Ever Paid INPUT: {TeacherNo}
           "SELECT COUNT(b.BillNo)  \r\n " +
          "FROM EmployeeBank.dbo.tblMember as a \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblBill as b on a.TeacherNo = b.TeacherNo \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblBillDetail as c on b.BillNo = c.BillNo \r\n " +
          "WHERE a.TeacherNo LIKE '{TeacherNo}' and c.TypeNo !=3 \r\n " +
          " \r\n " +
          "SELECT COUNT(c.WithDrawNo) \r\n " +
          "FROM EmployeeBank.dbo.tblMember as a \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblShare as b on a.TeacherNo = b.TeacherNo \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblShareWithdraw as c on b.ShareNo = c.ShareNo \r\n " +
          "WHERE a.TeacherNo LIKE '{TeacherNo}'"

           ,


           //[3]Save Edit Bsave INPUT: {Amount}  {TeacherNo}
           "-- BSave Edit \r\n " +
          "UPDATE EmployeeBank.dbo.tblMember  \r\n " +
          "SET StartAmount = {Amount}\r\n " +
          "WHERE TeacherNo = '{TeacherNo}'; \r\n " +
          " \r\n " +
          "UPDATE EmployeeBank.dbo.tblShare \r\n " +
          "SET SavingAmount = {Amount} \r\n " +
          "WHERE TeacherNo = '{TeacherNo}';"
           ,
           //[4]  Select Detail Memner INPUT: {TeacherNo} 
         "SELECT a.TeacherNo ,CAST(ISNULL(b.PrefixName+' ','')+a.Fname +' '+ a.Lname as NVARCHAR)AS Name,f.TeacherAddBy,CAST(ISNULL(b.PrefixName+' ','')+h.Fname +' '+ h.Lname as NVARCHAR) as NameTadd,a.IdNo, IIF(a.cNo != null,a.cNo,'-'),IIF(CAST(a.cMu as nvarchar) != null,a.cMu,'-'),IIF(CAST(c.TumBonName as nvarchar) != null,c.TumBonName,'-'),IIF(CAST(d.AmPhurName as nvarchar) != null,d.AmPhurName,'-'),IIF(CAST(e.JangWatLongName as nvarchar) != null,e.JangWatLongName,'-'),a.TelMobile,f.StartAmount  \r\n " +
          "FROM Personal.dbo.tblTeacherHis as a \r\n " +
          "LEFT JOIN BaseData.dbo.tblPrefix as b ON a.PrefixNo = b.PrefixNo  \r\n " +
          "LEFT JOIN BaseData.dbo.tblTumBon as c on a.cTumBonNo = c.TumBonNo \r\n " +
          "LEFT JOIN BaseData.dbo.tblAmphur as d on a.cAmPhurNo = d.AmphurNo \r\n " +
          "LEFT JOIN BaseData.dbo.tblJangWat as e on a.cJangWatNo = e.JangWatNo \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblMember as f on a.TeacherNo = f.TeacherNo  \r\n " +
          "LEFT JOIN Personal.dbo.tblTeacherHis as h on f.TeacherAddBy = h.TeacherNo  \r\n " +
          "WHERE a.TeacherNo = '{TeacherNo}'; "
          ,
          //[5] Chcek flie INPUT : {TeacherNo}
          "SELECT CAST(b.DocStatusName as NVARCHAR)  \r\n " +
          "FROM EmployeeBank.dbo.tblMember as a  \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblDocStatus as b on a.DocStatusNo = b.DocStatusNo  \r\n " +
          "WHERE a.TeacherNo = '{TeacherNo}' " 
           ,
           //[6] update file INPUT : {TeacharNo} {num} {PathFile}
           "UPDATE EmployeeBank.dbo.tblMember \r\n " +
           "set DocStatusNo = '{num}',DocUploadPath = '{PathFile}' \r\n " +
           "where TeacherNo = '{TeacherNo}'"
         };

        int StartAmount = 0;
        private void infoMeber_SizeChanged(object sender, EventArgs e)
        {
            Class.FromSettingMedtod.ChangeSizePanal(this, PL);
        }
        private void BExitForm_Click(object sender, EventArgs e)
        {
            BankTeacher.Class.FromSettingMedtod.ReturntoHome(this);
        }
        private void infoMeber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (TBTeacherNo.Text.Length != 0)
                {
                    TBTeacherName.Text = "";
                    TBTeacherNo.Text = "";
                    TBNameInfo.Text = "";
                    TBTeacherAddByName.Text = "";
                    TBMemberStatus.Text = "";
                    TBDateAdd.Text = "";
                    TBStartAmount.Text = "";
                    TBSavingAmount.Text = "";
                    BSaveEdit.Enabled = false;
                    SavingAmountStart = "";
                    TBStartAmount.Enabled = false;
                    TB_selectflie.Enabled = false;
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
            BSearchTeacher.Enabled = tf;
        }
        private void BSearchTeacher_Click(object sender, EventArgs e)
        {
            try
            {
                Bank.Search IN;
                String NotLike = "";
                if (TBTeacherNo.Text.Length == 6)
                {
                    NotLike += " and a.TeacherNo NOT LIKE " + $"'{TBTeacherNo.Text}'";
                }
                IN = new Bank.Search(SQLDefault[1]
                       .Replace("{TeacherNotLike}" , NotLike));

                IN.ShowDialog();
                if (Bank.Search.Return[0] != "" )
                {
                    TBTeacherNo.Text = Bank.Search.Return[0];
                    TBTeacherNo_KeyDown(sender, new KeyEventArgs(Keys.Enter));
                    tabControl1.SelectedIndex = 0;
                }
            }
            catch (Exception x)
            {
                Console.WriteLine(x);
            }
        }

        String SavingAmountStart = "";

        private void TBStartAmount_Leave(object sender, EventArgs e)
        {
            if(SavingAmountStart != TBStartAmount.Text)
            {
                BSaveEdit.Enabled = true;
            }
        }

        private void TBNameInfo_Leave(object sender, EventArgs e)
        {
            if(SavingAmountStart != TBStartAmount.Text)
            {
                BSaveEdit.Enabled = true;
                SavingAmountStart = Convert.ToInt32(SavingAmountStart) - Convert.ToInt32(TBStartAmount.Text) + "";
            }
        }

        private void TBTeacherNo_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                try
                {
                    TBTeacherNo.Text = TBTeacherNo.Text.Replace("t", "T");
                    DataSet dsInfoMember = Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[0]
                        .Replace("{TeacherNo}", TBTeacherNo.Text) +
                        "\r\n" + 
                        SQLDefault[2]
                        .Replace("{TeacherNo}" , TBTeacherNo.Text));
                    if(dsInfoMember.Tables[0].Rows.Count != 0 && dsInfoMember.Tables[1].Rows.Count != 0 && dsInfoMember.Tables[2].Rows.Count != 0)
                    {
                        //
                        TBTeacherName.Text = dsInfoMember.Tables[0].Rows[0][0].ToString();
                        TBNameInfo.Text = dsInfoMember.Tables[0].Rows[0][0].ToString();
                        TBTeacherAddByName.Text = dsInfoMember.Tables[0].Rows[0][1].ToString();
                        TBMemberStatus.Text = dsInfoMember.Tables[0].Rows[0][2].ToString();
                        TBDateAdd.Text = dsInfoMember.Tables[0].Rows[0][3].ToString();
                        TBStartAmount.Text = dsInfoMember.Tables[0].Rows[0][4].ToString();
                        TBSavingAmount.Text = dsInfoMember.Tables[0].Rows[0][5].ToString();
                        SavingAmountStart = dsInfoMember.Tables[0].Rows[0][4].ToString();
                        StartAmount = Convert.ToInt32(dsInfoMember.Tables[0].Rows[0][4].ToString());

                        if (Convert.ToInt32(dsInfoMember.Tables[1].Rows[0][0].ToString()) == 0 && Convert.ToInt32(dsInfoMember.Tables[2].Rows[0][0].ToString()) == 0)
                            TBStartAmount.Enabled = true;
                        button1.Enabled = true;
                        tabControl1.Enabled = true;
                        Checkmember(false);

                        // เช็คการเชื่อมต่อ
                        var wifi = new BankTeacher.Class.ProtocolSharing.ConnectSMB.SmbFileContainer("RegMember");
                        if (wifi.IsValidConnection()) { label17.Text = "ปกติ"; label17.ForeColor = Color.Green; pb_disconnectwifi.Visible = false; pb_connectwifi.Visible = true; }
                        else { label17.Text = "ขาดการเชื่อมต่อ"; label17.ForeColor = Color.Red; pb_disconnectwifi.Visible = true; pb_connectwifi.Visible = false; BT_Rewifi.Visible = true; }

                        // เช็คการส่งไฟล์
                        DataTable dt = Class.SQLConnection.InputSQLMSSQL(SQLDefault[5].Replace("{TeacherNo}", TBTeacherNo.Text));
                        if(dt.Rows[0][0].ToString() != "ไม่ได้อัพโหลด")
                        {
                            label12.Text = "อัพโหลดไฟล์เรียบร้อย";
                            label12.ForeColor = Color.Green;
                            TB_selectflie.Enabled = true;
                            TB_deletefile.Enabled = true;
                        }
                        else
                        {
                            label12.Text = "ยังไม่ได้อัพโหลดไฟล์";
                            label12.ForeColor = Color.Red;
                            TB_selectflie.Enabled = false;
                            TB_deletefile.Enabled = false;
                        }
                            
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"-----------------------------{ex}-----------------------------------");
                }
            }
            else if(e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
            {
                TBTeacherName.Text = "";
                TBNameInfo.Text = "";
                TBTeacherAddByName.Text = "";
                TBMemberStatus.Text = "";
                TBDateAdd.Text = "";
                TBStartAmount.Text = "";
                TBSavingAmount.Text = "";
                BSaveEdit.Enabled = false;
                TBStartAmount.Enabled = false;
                TB_selectflie.Enabled = false;
                SavingAmountStart = "";
                Checkmember(true);
            }
            else if(e.KeyCode == Keys.K)
            {
                // เช็คการเชื่อมต่อ
                var wifi = new BankTeacher.Class.ProtocolSharing.ConnectSMB.SmbFileContainer("RegMember");
                if (wifi.IsValidConnection()) { label17.Text = "ปกติ"; label17.ForeColor = Color.Green; pb_disconnectwifi.Visible = false; pb_connectwifi.Visible = true; BT_Rewifi.Visible = false; }
                else { label17.Text = "ขาดการเชื่อมต่อ"; label17.ForeColor = Color.Red; pb_disconnectwifi.Visible = true; pb_connectwifi.Visible = false; BT_Rewifi.Visible = true; BT_Rewifi.Text = "รีเซ็ตการเชื่อมต่อ";
                    BT_Rewifi.Enabled = true;
                    BT_Rewifi.BackColor = Color.Transparent;
                }

                // เช็คการส่งไฟล์
                DataTable dt = Class.SQLConnection.InputSQLMSSQL(SQLDefault[5].Replace("{TeacherNo}", TBTeacherNo.Text));
                if (dt.Rows[0][0].ToString() != "ไม่ได้อัพโหลด")
                {
                    label12.Text = "อัพโหลดไฟล์เรียบร้อย";
                    label12.ForeColor = Color.Green;
                    TB_selectflie.Enabled = true;
                    TB_deletefile.Enabled = true;
                }
            }
        }

        private void BSaveEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if(MessageBox.Show("ยืนยันการเปลี่ยนแปลง","แจ้งเตือน",MessageBoxButtons.YesNo,MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    int DifferenceAmount = StartAmount - Convert.ToInt32(TBStartAmount.Text);
                    StartAmount = StartAmount - DifferenceAmount;
                    Class.SQLConnection.InputSQLMSSQL(SQLDefault[3]
                    .Replace("{Amount}", StartAmount.ToString())
                    .Replace("{TeacherNo}", TBTeacherNo.Text));
                    Checkmember(true);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"--------------------------{ex}----------------------------");
                MessageBox.Show("การบันทึกล้มเหลว", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            BankTeacher.Class.Print.PrintPreviewDialog.PrintMember(e, SQLDefault[4], BankTeacher.Bank.Menu.Date[2], BankTeacher.Bank.Menu.Monthname, (Convert.ToInt32(BankTeacher.Bank.Menu.Date[0]) + 543).ToString(),TBTeacherNo.Text,TBTeacherName.Text,checkBox_scrip.Checked,checkBox_copy.Checked);
        }
        int SandCRonot = 0;
        private void BTPrint_Click(object sender, EventArgs e)
        {
            // เลือก ต้น ฉบับ หรือ สำเนา หรือ ไม่
            if (checkBox_scrip.Checked == true) { SandCRonot = 3; }
            if (checkBox_copy.Checked == true) { SandCRonot = 4; }
            if (checkBox_scrip.Checked == true && checkBox_copy.Checked == true) { SandCRonot = 1; }
            if (checkBox_scrip.Checked == false && checkBox_copy.Checked == false) { SandCRonot = 0; }

            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        String imgeLocation = "";
        private void BTOpenfile_Reg_Click(object sender, EventArgs e)
        {
            if (TBTeacherNo.Text.Length >= 6)
            {
                DataTable dt_CheckFlie = Class.SQLConnection.InputSQLMSSQL(SQLDefault[5].Replace("{TeacherNo}", TBTeacherNo.Text));
                if (dt_CheckFlie.Rows[0][0].ToString() == "ไม่ได้อัพโหลด")
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
                            DataTable dt_Chcek = Class.SQLConnection.InputSQLMSSQL("SELECT a.DocUploadPath \r\n" +
                            "FROM EmployeeBank.dbo.tblMemberResignation as a \r\n" +
                            "WHERE a.TeacherNo = '{TeacherNo}'".Replace("{TeacherNo}", TBTeacherNo.Text));
                            var smb = new BankTeacher.Class.ProtocolSharing.ConnectSMB.SmbFileContainer("RegMember");
                            if (dt_Chcek.Rows.Count != 0)
                            {
                                Class.PathFile.File = $"Member_{TBTeacherNo.Text}_{dt_Chcek.Rows.Count}.pdf";
                                Class.PathFile.FileNo = dt_Chcek.Rows.Count.ToString();
                            }
                            else
                            {
                                Class.PathFile.File = $@"Member_{TBTeacherNo.Text}.pdf";
                            }

                                if (smb.IsValidConnection())
                                {
                                String Return = smb.SendFile(imgeLocation,Class.PathFile.File,TBTeacherNo.Text, 1, BankTeacher.Class.UserInfo.TeacherNo);
                                MessageBox.Show(Return, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                if (Return.Contains("อัพโหลดเอกสารสำเร็จ"))
                                {
                                    DataTable dt = Class.SQLConnection.InputSQLMSSQL(SQLDefault[6].Replace("{TeacherNo}", TBTeacherNo.Text).Replace("{num}","1").Replace("{PathFile}",smb.PathFile+Class.PathFile.File));
                                    label12.Text = "อัพโหลดไฟล์เรียบร้อย";
                                    label12.ForeColor = Color.Green;
                                    TB_selectflie.Enabled = true;
                                    TB_deletefile.Enabled = true;
                                    imgeLocation = "";
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
                else
                {
                    MessageBox.Show("ทำการส่งไฟล์แล้ว ไม่สามารถดำเนินการส่งไฟล์ซ้ำได้", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("กรุณากรอก รหัสผู้ใช้ก่อน", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        public static string OroD;
        private void button3_Click(object sender, EventArgs e)
        {
            OroD = "เปิดไฟล์";
            DataTable dt = Class.SQLConnection.InputSQLMSSQL(SQLDefault[5].Replace("{TeacherNo}", TBTeacherNo.Text));
            if (dt.Rows[0][0].ToString() == "อัพโหลดเอกสารแล้ว")
            {
                if (TBTeacherNo.Text != "")
                {
                    //Input Location Folder
                    TBTeacherNo_KeyDown(sender, new KeyEventArgs(Keys.K));
                    var smb = new BankTeacher.Class.ProtocolSharing.ConnectSMB.SmbFileContainer("RegMember");
                    if (smb.IsValidConnection())
                    {
                        //Input Contain words แนะนำ เป็นรหัสอาจารย์ ในหน้าทั่วไปส่วนหน้าไหนถ้ามีการทำรายการเยอะๆให้เอาเป็นเลขบิลล์ของหน้านั้นๆเช่นหน้าดูเอกสารกู้ จะใส่เป็นเลขกู้ หน้าดูเอกสาร สมัครสมาชิกจะใส่เป็นชื่ออาจารย์
                        DataTable dtGetpath = Class.SQLConnection.InputSQLMSSQL($"SELECT DocUploadPath FROM EmployeeBank.dbo.tblMember WHERE TeacherNo LIKE '%{TBTeacherNo.Text}%'");
                        String FileName = dtGetpath.Rows[0][0].ToString();
                        FileName = FileName.Replace(smb.PathFile, "");
                        smb.GetFile(FileName);
                    }
                    else { MessageBox.Show($"โปรดตรวจสอบการเชื่อมต่อ ไม่สามรถเข้าถึงโฟร์เดอร์ได้\r\n{Class.ProtocolSharing.ConnectSMB.StatusRetrun}", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                    //System.IO.Path.Combine(@"net use * / delete");
                }
            }
            else
            {
                label12.Text = "ยังไม่ได้อัพโหลดไฟล์";
                label12.ForeColor = Color.Red;
                TB_selectflie.Enabled = false;
                TB_deletefile.Enabled = false;
            }
        }
        public void button4_Click(object sender, EventArgs e)
        {
            do
            {
                Bank.SelectFile.info_File.No = TBTeacherNo.Text;
                Bank.SelectFile.info_File.Type = "MemberNo";
                OroD = "ลบ";
                DataTable dt = Class.SQLConnection.InputSQLMSSQL(SQLDefault[5].Replace("{TeacherNo}", TBTeacherNo.Text));
                if (dt.Rows[0][0].ToString() == "อัพโหลดเอกสารแล้ว")
                {
                    if (TBTeacherNo.Text != "")
                    {
                        TBTeacherNo_KeyDown(sender, new KeyEventArgs(Keys.K));
                        //Input Location Folder
                        var smb = new BankTeacher.Class.ProtocolSharing.ConnectSMB.SmbFileContainer("RegMember");
                        if (smb.IsValidConnection())
                        {
                            DataTable dtGetpath = Class.SQLConnection.InputSQLMSSQL($"SELECT DocUploadPath FROM EmployeeBank.dbo.tblMember WHERE TeacherNo LIKE '%{TBTeacherNo.Text}%'");
                            String FileName = dtGetpath.Rows[0][0].ToString();
                            FileName = FileName.Replace(smb.PathFile, "");
                            smb.GetFile(FileName);
                            TBTeacherNo_KeyDown(sender, new KeyEventArgs(Keys.Enter));
                        }
                        else { 
                            MessageBox.Show("โปรดตรวจสอบการเชื่อมต่อ ไม่สามรถเข้าถึงโฟร์เดอร์ได้", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            Bank.SelectFile.OpenEnableButton = true;
                        }
                        
                       
                    }
                }
                else
                {
                    Bank.SelectFile.OpenEnableButton = true;
                    label12.Text = "ยังไม่ได้อัพโหลดไฟล์";
                    label12.ForeColor = Color.Red;
                    TB_selectflie.Enabled = false;
                    TB_deletefile.Enabled = false;
                }
            } while(!Bank.SelectFile.OpenEnableButton);
        }
        public static string T = "";
        private void button1_Click(object sender, EventArgs e)
        {
            if(TBStartAmount.Enabled == true)
            {
                TBStartAmount.Enabled = false;
            }
            else
            {
                TBStartAmount.Enabled = true;
            }
        }
        //System.Threading.Thread Tark; 
        private void BT_Rewifi_Click(object sender, EventArgs e)
        {
            BT_Rewifi.Text = "กำลังโหลด";
            BT_Rewifi.Enabled = false;
            BT_Rewifi.BackColor = Color.Silver;
            TBTeacherNo_KeyDown(sender, new KeyEventArgs(Keys.K));

          
            //System.Diagnostics.Stopwatch time = new System.Diagnostics.Stopwatch();
            //Tark = new System.Threading.Thread(() => Rewifi(sender,e));
            //Tark.Start();
            //time.Start();
            //while (Tark.ThreadState == System.Threading.ThreadState.Running)
            //{
            //    if(time.ElapsedMilliseconds >= 5000 && Tark.IsAlive)
            //    {
            //        Tark.Abort();
            //        BT_Rewifi.Text = "รีเซ็ตการเชื่อมต่อ";
            //        BT_Rewifi.Enabled = true;
            //        BT_Rewifi.BackColor = Color.Transparent;
            //        break;
            //    }
            //}
        }
        private void Rewifi(object sender, EventArgs e)
        {
            //TBTeacherNo_KeyDown(sender, new KeyEventArgs(Keys.K));
        }
    }
}
