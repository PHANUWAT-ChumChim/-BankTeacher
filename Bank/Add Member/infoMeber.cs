﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        /// </summary> 
        private String[] SQLDefault = new String[]
         { 
           //[0]Get InfoMember INPUT: {TeacherNo}  
           "SELECT CAST(ISNULL(c.PrefixNameFull , '') + b.Fname + ' ' + b.Lname as NVARCHAR) as Name , f.Name as TeacherAddByName , d.MemberStatusName , CAST(a.DateAdd as date)  \r\n " +
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
          "WHERE a.TeacherNo LIKE '%{TeacherNo}%' and a.MemberStatusNo = 1 "
           ,

           //[1]Search Teacher INPUT: {TeacherNotLike}
           "SELECT a.TeacherNo , CAST(ISNULL(c.PrefixNameFull , '') + b.Fname + ' ' + Lname as NVARCHAR) \r\n " +
          "FROM EmployeeBank.dbo.tblMember as a \r\n " +
          "LEFT JOIN Personal.dbo.tblTeacherHis as b on a.TeacherNo = b.TeacherNo \r\n " +
          "LEFT JOIN BaseData.dbo.tblPrefix as c on b.PrefixNo = c.PrefixNo \r\n " +
          "WHERE (a.TeacherNo LIKE '%{Text}%'  or CAST(ISNULL(c.PrefixNameFull , '') + b.Fname + ' ' + Lname as NVARCHAR) LIKE '%{Text}%')and a.MemberStatusNo = 1 {TeacherNotLike};"
           ,
           //[2] Check Bill Teacher Have Ever Paid INPUT: {TeacherNo}
           "SELECT COUNT(a.BillNo)  \r\n " +
          "FROM EmployeeBank.dbo.tblBill as a  \r\n " +
          "LEFT JOIN (SELECT a.BillNo  \r\n " +
          "FROM EmployeeBank.dbo.tblBillDetail as a \r\n " +
          "WHERE a.TypeNo <> 3 \r\n " +
          "GROUP BY a.BillNo) as b on a.BillNo = b.BillNo  \r\n " +
          "WHERE a.Cancel = 1 and a.TeacherNo LIKE '%{TeacherNo}%' \r\n " +
          " \r\n " +
          "SELECT COUNT(c.WithDrawNo) \r\n " +
          "FROM EmployeeBank.dbo.tblMember as a \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblShare as b on a.TeacherNo = b.TeacherNo \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblShareWithdraw as c on b.ShareNo = c.ShareNo \r\n " +
          "WHERE a.TeacherNo LIKE '%{TeacherNo}%'"
           ,

           //[3]Save Edit Bsave INPUT: {Amount}  {TeacherNo}
           "-- BSave Edit \r\n " +
          "UPDATE EmployeeBank.dbo.tblMember  \r\n " +
          "SET StartAmount = {Amount} \r\n " +
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
         };
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
                    button3.Enabled = false;
                }
                else
                {
                    BExitForm_Click(new object(), new EventArgs());
                }
            }
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
                       .Replace("{TeacherNotLike}" , NotLike) , "");

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
            if(e.KeyCode == Keys.Enter && TBTeacherNo.Text.Length == 6)
            {
                try
                {
                    DataSet dsInfoMember = Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[0]
                        .Replace("{TeacherNo}", TBTeacherNo.Text) +
                        "\r\n" + SQLDefault[2]
                        .Replace("{TeacherNo}" , TBTeacherNo.Text));
                    if(dsInfoMember.Tables[0].Rows.Count != 0 || dsInfoMember.Tables[1].Rows.Count != 0)
                    {
                        TBTeacherName.Text = dsInfoMember.Tables[0].Rows[0][0].ToString();
                        TBNameInfo.Text = dsInfoMember.Tables[0].Rows[0][0].ToString();
                        TBTeacherAddByName.Text = dsInfoMember.Tables[0].Rows[0][1].ToString();
                        TBMemberStatus.Text = dsInfoMember.Tables[0].Rows[0][2].ToString();
                        TBDateAdd.Text = dsInfoMember.Tables[0].Rows[0][3].ToString();
                        TBStartAmount.Text = dsInfoMember.Tables[0].Rows[0][4].ToString();
                        TBSavingAmount.Text = dsInfoMember.Tables[0].Rows[0][5].ToString();
                        SavingAmountStart = dsInfoMember.Tables[0].Rows[0][4].ToString();

                        if (Convert.ToInt32(dsInfoMember.Tables[1].Rows[0][0].ToString()) == 0 && Convert.ToInt32(dsInfoMember.Tables[2].Rows[0][0].ToString()) == 0)
                            TBStartAmount.Enabled = true;
                        button3.Enabled = true;

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
                button3.Enabled = false;
                SavingAmountStart = "";
            }
        }

        private void BSaveEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if(MessageBox.Show("ยืนยันการเปลี่ยนแปลง","แจ้งเตือน",MessageBoxButtons.YesNo,MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    Class.SQLConnection.InputSQLMSSQL(SQLDefault[3]
                .Replace("{Amount}", TBStartAmount.Text)
                .Replace("{TeacherNo}", TBTeacherNo.Text));
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
            BankTeacher.Class.Print.PrintPreviewDialog.PrintMember(e, SQLDefault[4], BankTeacher.Bank.Menu.Date[2], BankTeacher.Bank.Menu.Monthname, (Convert.ToInt32(BankTeacher.Bank.Menu.Date[0]) + 543).ToString(),TBTeacherNo.Text,TBTeacherName.Text,SandCRonot);
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
        int StatusBoxFile = 0;
        String imgeLocation = "";
        private void BTOpenfile_Reg_Click(object sender, EventArgs e)
        {
            if (TBTeacherNo.Text.Length == 6)
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
                            //BTOpenfile_Reg.Text = "ส่งไฟล์";
                            StatusBoxFile = 1;

                            var smb = new BankTeacher.Class.ProtocolSharing.ConnectSMB.SmbFileContainer("RegMember");
                            if (smb.IsValidConnection())
                            {
                                String Return = smb.SendFile(imgeLocation, "Regmember" + TBTeacherNo.Text + ".pdf");
                                MessageBox.Show(Return, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                if (Return.Contains("อัพโหลดสำเร็จ"))
                                {
                                    //BTdeletefile_Reg.Enabled = true;
                                    //LScan_Reg.Text = "อัพโหลดไฟล์สำเร็จ";
                                    //LScan_Reg.ForeColor = Color.Green;
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
        private void button3_Click(object sender, EventArgs e)
        {
            if(TBTeacherNo.Text != "")
            {
                //Input Location Folder
                var smb = new BankTeacher.Class.ProtocolSharing.ConnectSMB.SmbFileContainer("RegMember");
                //Input Contain words แนะนำ เป็นรหัสอาจารย์ ในหน้าทั่วไปส่วนหน้าไหนถ้ามีการทำรายการเยอะๆให้เอาเป็นเลขบิลล์ของหน้านั้นๆเช่นหน้าดูเอกสารกู้ จะใส่เป็นเลขกู้ หน้าดูเอกสาร สมัครสมาชิกจะใส่เป็นชื่ออาจารย์
                smb.ThreadOpenFile(TBTeacherNo.Text);
                if(BankTeacher.Class.ProtocolSharing.ConnectSMB.StatusRetrun != "")
                {
                    MessageBox.Show(BankTeacher.Class.ProtocolSharing.ConnectSMB.StatusRetrun, "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
