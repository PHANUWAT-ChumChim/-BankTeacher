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
        //------------------------- index -----------------
        int Check = 0;
        int StatusBoxFile = 0;
        String imgeLocation = "";
        bool CheckBRegister = false;
        bool CheckBCancel = false;
        double Saving = 0;

        //----------------------- index code -------------------- ////////

        public Member()
        {
            InitializeComponent();
            TBStartAmountShare_Reg.Text = BankTeacher.Bank.Menu.startAmountMin.ToString();
        }

        //------------------------- FormSize -----------------
        // Comment!
        private void membership_SizeChanged(object sender, EventArgs e)
        {
            Class.FromSettingMedtod.ChangeSizePanal(this, panel1);
        }
        //----------------------- End code -------------------- ////////

        /// <summary>
        /// SQLDafault
        /// <para>[0] Insert Teacher Data INPUT:{TeacherNo}{TeacherAddBy}, {StartAmount} </para>
        /// <para>[1] SELECT Member  INPUT:{Text} </para>
        /// <para>[2]  Select Detail Memner INPUT: {TeacherNo} </para>
        /// <para>[3] INSERT Member To Member  Bill BillDetail  INPUT: {TeacherNo} {TeacherNoAddBy} {StartAmount} {Mount} {Year}  </para>
        /// <para>[4] Print info_Member  INPUT : {TeacherNo} </para>
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

           //[3] INSERT Member To Member  Bill BillDetail  INPUT: {TeacherNo} {TeacherNoAddBy} {StartAmount} {Mount} {Year}
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
          "SET MemberStatusNo = 1 ,DateAdd = CURRENT_TIMESTAMP \r\n " +
          "WHERE TeacherNo = '{TeacherNo}'; \r\n " +
          "UPDATE EmployeeBank.dbo.tblShare \r\n " +
          "SET SavingAmount = '{StartAmount}' \r\n " +
          "WHERE TeacherNo = '{TeacherNo}'; \r\n " +
          "INSERT INTO EmployeeBank.dbo.tblBill(TeacherNo, TeacherNoAddBy, DateAdd)  \r\n " +
          "VALUES('{TeacherNo}','{TeacherNoAddBy}', CURRENT_TIMESTAMP)  \r\n " +
          "SELECT @BillNo = SCOPE_IDENTITY();  \r\n " +
          "INSERT INTO EmployeeBank.dbo.tblBillDetail(BillNo, TypeNo, Amount, Mount, Year,BillDetailPaymentNo) \r\n " +
          "VALUES(@BillNo,3,{StartAmount},{Month},{Year},1) \r\n " +
          " \r\n " +
          "END; \r\n " +
          " \r\n " +
          "ELSE \r\n " +
          "BEGIN \r\n " +
          " \r\n " +
          "INSERT INTO EmployeeBank.dbo.tblMember(TeacherNo, TeacherAddBy, StartAmount, DateAdd)  \r\n " +
          "VALUES('{TeacherNo}','{TeacherNoAddBy}',{StartAmount},CURRENT_TIMESTAMP)   \r\n " +
          "INSERT INTO EmployeeBank.dbo.tblShare(TeacherNo, SavingAmount) \r\n " +
          "VALUES('{TeacherNo}',{StartAmount})  \r\n " +
          "INSERT INTO EmployeeBank.dbo.tblBill(TeacherNo, TeacherNoAddBy, DateAdd)  \r\n " +
          "VALUES('{TeacherNo}','{TeacherNoAddBy}', CURRENT_TIMESTAMP)  \r\n " +
          "SELECT @BillNo = SCOPE_IDENTITY();  \r\n " +
          "INSERT INTO EmployeeBank.dbo.tblBillDetail(BillNo, TypeNo, Amount, Mount, Year,BillDetailPaymentNo) \r\n " +
          "VALUES(@BillNo,3,{StartAmount},{Month},{Year},1) \r\n " +
          " \r\n " +
          "END; \r\n " +
          " "
          ,
          //[4] Print info_Member  INPUT : {TeacherNo}
          "SELECT a.TeacherNo,a.TeacherAddBy,CAST(c.PrefixName+' '+b.Fname+' '+b.Lname as nvarchar),a.StartAmount,a.DateAdd  \r\n " +
          "FROM EmployeeBank.dbo.tblMember as a  \r\n " +
          "LEFT JOIN Personal.dbo.tblTeacherHis as b on a.TeacherNo = b.TeacherNo  \r\n " +
          "LEFT JOIN BaseData.dbo.tblPrefix as c on b.PrefixNo = c.PrefixNo  \r\n " +
          "WHERE a.TeacherNo = '{TeacherNo}';"
            ,
          //[5] SELECT Member (Enter) INPUT:{Text}
           "SELECT TeacherNo,Name,null,Fname  \r\n " +
          "FROM(SELECT ISNULL(b.TeacherNo , a.TeacherNo) as TeacherNo ,  CAST(ISNULL(c.PrefixName+' ','') + Fname +' '+ Lname as NVARCHAR)AS NAME, Fname , b.MemberStatusNo  \r\n " +
          "FROM Personal.dbo.tblTeacherHis as a  \r\n " +
          "FULL OUTER JOIN EmployeeBank.dbo.tblMember as b on a.TeacherNo = b.TeacherNo  \r\n " +
          "LEFT JOIN BaseData.dbo.tblPrefix as c ON a.PrefixNo = c.PrefixNo   \r\n " +
          "WHERE (a.TeacherNo = '{Text}') and a.IsUse = 1 ) as a  \r\n " +
          "WHERE a.MemberStatusNo = 2 or a.MemberStatusNo IS NULL  \r\n " +
          "ORDER BY a.Fname; "
           ,

    };

        private void BSearchTeacher_Click(object sender, EventArgs e)
        {
            try
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
            catch (Exception x)
            {
                Console.WriteLine(x);
            }
        }
        // Comment!
        // Available values|  SQLDefault[1] / TB //
        private void BSave_Click_1(object sender, EventArgs e)
        {
            if (CheckBRegister == false && TBTeacherName_Reg.Text != "")
            {
                //Input Location Folder
                var smb = new BankTeacher.Class.ProtocolSharing.ConnectSMB.SmbFileContainer("RegMember");
                //Input Contain words แนะนำ เป็นรหัสอาจารย์ ในหน้าทั่วไปส่วนหน้าไหนถ้ามีการทำรายการเยอะๆให้เอาเป็นเลขบิลล์ของหน้านั้นๆเช่นหน้าดูเอกสารกู้ จะใส่เป็นเลขกู้ หน้าดูเอกสาร สมัครสมาชิกจะใส่เป็นชื่ออาจารย์
                smb.ThreadCheckFiles(TBTeacherNo_Reg.Text, "RegMember");
                if (BankTeacher.Class.ProtocolSharing.ConnectSMB.StatusRetrun.Contains("ไม่พบ"))
                {
                    MessageBox.Show(BankTeacher.Class.ProtocolSharing.ConnectSMB.StatusRetrun, "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (BankTeacher.Class.ProtocolSharing.ConnectSMB.StatusRetrun != "" && !(BankTeacher.Class.ProtocolSharing.ConnectSMB.StatusRetrun.Contains("หมดเวลา")))
                {

                    if (TBStartAmountShare_Reg.Text == "")
                        TBStartAmountShare_Reg.Text = "0";
                    int AmountShare = Convert.ToInt32(TBStartAmountShare_Reg.Text);
                    if (AmountShare.ToString() == "" || AmountShare == 0)
                    {
                        AmountShare = BankTeacher.Bank.Menu.startAmountMin;
                    }
                    DataTable dt = Class.SQLConnection.InputSQLMSSQL(SQLDefault[1].Replace("{TeacherNo}", TBTeacherNo_Reg.Text));
                    if (TBTeacherName_Reg.Text != "")
                    {
                        if (AmountShare >= BankTeacher.Bank.Menu.startAmountMin && AmountShare <= BankTeacher.Bank.Menu.startAmountMax)
                        {
                            if (dt.Rows.Count == 0)
                            {

                                DialogResult dialogResult = MessageBox.Show("ยืนยันการสมัคร", "สมัครสมาชิก", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                                if (dialogResult == DialogResult.Yes)
                                {
                                    Class.SQLConnection.InputSQLMSSQL(SQLDefault[3].Replace("{TeacherNo}", TBTeacherNo_Reg.Text)
                                    .Replace("{TeacherNoAddBy}", BankTeacher.Class.UserInfo.TeacherNo)
                                    .Replace("{StartAmount}", AmountShare.ToString())
                                    .Replace("{Month}", BankTeacher.Bank.Menu.Date[1])
                                    .Replace("{Year}", BankTeacher.Bank.Menu.Date[0]));
                                    MessageBox.Show("สมัครเสร็จสิ้น", "สมัครสมาชิก", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    CheckBRegister = true;
                                    Checkmember(true);
                                }
                                else
                                {
                                    MessageBox.Show("ยกเลิกการสมัคร", "สมัครสมาชิก", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }

                            }
                            else
                            {
                                MessageBox.Show("รายชื่อซ้ำ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {
                            MessageBox.Show("ไม่สามารถสมัครสมาชิกได้เนื่องจาก \r\n ราคาหุ้นเริ่มต้นต่ำหรือสูงเกินไป \r\n โปรดแก้ไข ราคาหุ้นขั้นต่ำ หรือ สูงสุด ที่หน้าตั้งค่า", "System Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("โปรดเลือกสมาชิกในการสมัคร", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }
        // Available values| TB /
        private void TBStartAmountShare_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && (e.KeyChar != '\b'))
            {
                e.Handled = true;
            }
        }
        private void BExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void BTPrintfShare_Click(object sender, EventArgs e)
        {
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();

            }
        }

        //----------------------- Printf -------------------- ////////
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            BankTeacher.Class.Print.PrintPreviewDialog.PrintMember(e,SQLDefault[2],BankTeacher.Bank.Menu.Date[2],BankTeacher.Bank.Menu.Monthname,(Convert.ToInt32(BankTeacher.Bank.Menu.Date[0]) + 543).ToString(),TBTeacherNo_Reg.Text,TBStartAmountShare_Reg.Text,true,true);
        }

        private void BTOpenfile_Click(object sender, EventArgs e)
        {
            if(TBTeacherNo_Reg.Text.Length != 0)
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
                            
                            var smb = new SmbFileContainer("RegMember");
                            if (smb.IsValidConnection())
                            {
                                String Return = smb.SendFile(imgeLocation, "Member_" + TBTeacherNo_Reg.Text + ".pdf" , TBTeacherNo_Reg.Text, 1, BankTeacher.Class.UserInfo.TeacherNo);
                                MessageBox.Show(Return, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                if (Return.Contains("อัพโหลดสำเร็จ"))
                                {
                                    BTdeletefile_Reg.Enabled = true;
                                    LScan_Reg.Text = "อัพโหลดไฟล์สำเร็จ";
                                    LScan_Reg.ForeColor = Color.Green;
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

        private void TBTeacherNo_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void TBTeacherNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    TBTeacherNo_Reg.Text = TBTeacherNo_Reg.Text.Replace("t", "T");
                        DataSet ds = Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[5].Replace("{Text}", TBTeacherNo_Reg.Text));
                    if(ds.Tables[0].Rows.Count != 0)
                    {
                        TBTeacherName_Reg.Text = ds.Tables[0].Rows[0][1].ToString();
                        Check = 1;
                        CheckBRegister = false;
                        BTPrintfShare_Reg.Enabled = true;
                        StatusBoxFile = 0;
                        Checkmember(false);
                    }
                }
                catch (Exception ex)
                {
                    BTPrintfShare_Reg.Enabled = false;
                    Console.WriteLine(ex);
                }
            }
            else if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back && Check == 1)
            {
                TBTeacherName_Reg.Text = "";
                CheckBRegister = false;
                Checkmember(true);
                Check = 0;
            }
        }
        private void Checkmember(bool tf)
        {
            TBTeacherNo_Reg.Enabled = tf;
            BSearchTeacher_Reg.Enabled = tf;
        }
        private void BExitForm_Click(object sender, EventArgs e)
        {
            BankTeacher.Class.FromSettingMedtod.ReturntoHome(this);
        }

        private void Member_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (TBTeacherNo_Reg.Text.Length != 0)
                {
                    TBTeacherNo_Reg.Text = "";
                    TBTeacherName_Reg.Text = "";
                    TBStartAmountShare_Reg.Text = "";
                    Check = 0;
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

        private void TBTeacherNo_Reg_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = Class.SQLConnection.InputSQLMSSQL(SQLDefault[4].Replace("{TeacherNo}", TBTeacherNo_Reg.Text));
            if (dt.Rows.Count != 0 && dt.Rows[0][0].ToString() == TBTeacherNo_Reg.Text)
            {
                label5.Text = "เอกสมัครสมาชิกย้อนหลัง";
                BTPrintfShare_Reg.Enabled = true;
            }
            else
            {
                label5.Text = "เอกสารในการสมัครชิกสหกร์ครู";
                BTPrintfShare_Reg.Enabled = false;
            }
        }

        private void TBTeacherNo_Reg_EnabledChanged(object sender, EventArgs e)
        {
            if (TBTeacherNo_Reg.Enabled == true)
            {
                BSave_Reg.Enabled = false;
            }
            else
            {
                BSave_Reg.Enabled = true;
            }
        }
    }
}