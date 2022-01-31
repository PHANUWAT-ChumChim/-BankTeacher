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
        bool CheckBRegister = false;
        bool CheckBCancel = false;
        double Saving = 0;
        String PathFile = null;

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
            TBTeacherNo_Reg.Select();
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
          "SET MemberStatusNo = 1 ,DateAdd = CURRENT_TIMESTAMP,DocStatusNo = 1,DocUploadPath = '{PathFile}' \r\n " +
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
          "INSERT INTO EmployeeBank.dbo.tblMember(TeacherNo, TeacherAddBy, StartAmount, DateAdd,DocStatusNo,DocUploadPath)  \r\n " +
          "VALUES('{TeacherNo}','{TeacherNoAddBy}',{StartAmount},CURRENT_TIMESTAMP,1,'{PathFile}')   \r\n " +

          "INSERT INTO EmployeeBank.dbo.tblShare(TeacherNo, SavingAmount) \r\n " +
          "VALUES('{TeacherNo}',{StartAmount})  \r\n " +

          "INSERT INTO EmployeeBank.dbo.tblBill(TeacherNo, TeacherNoAddBy, DateAdd)  \r\n " +
          "VALUES('{TeacherNo}','{TeacherNoAddBy}', CURRENT_TIMESTAMP)  \r\n " +

          "SELECT @BillNo = SCOPE_IDENTITY();  \r\n " +

          "INSERT INTO EmployeeBank.dbo.tblBillDetail(BillNo, TypeNo, Amount, Mount, Year,BillDetailPaymentNo) \r\n " +
          "VALUES(@BillNo,3,{StartAmount},{Month},{Year},1) \r\n " +
          " \r\n " +
          "END; \r\n " +


           "INSERT INTO EmployeeBank.dbo.tblFile(TeacherNo , FiletypeNo , pathFile ,TeacherAddBy,LoanID,DateAddFile,IsUse , TeacherRemoveFileBy ,DateRemoveFile, StatusFileInSystem) \r\n " +
          "VALUES ('{TeacherNo}',1,'{PathFile}','{TeacherNoAddBy}',null,CURRENT_TIMESTAMP,1,null,null,2); \r\n " +
          " \r\n " +

          "UPDATE EmployeeBank.dbo.tblMember \r\n " +
          "SET DocStatusNo = 1 , DocUploadPath = '{PathFile}'\r\n"+
          "WHERE TeacherNo = '{TeacherNo}'"
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
                if(PathFile != "" && PathFile != null)
                {
                    BankTeacher.Class.ProtocolSharing.FileZilla.FileZillaConnection FTP = new Class.ProtocolSharing.FileZilla.FileZillaConnection("RegMember");
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

                                    BSave_Reg.Enabled = false;
                                    CheckBRegister = true;
                                    Checkmember(true);
                                    BTdeletefile_Reg.Visible = false;
                                    BTOpenfile_Reg.Enabled = false;
                                    TBStartAmountShare_Reg.Enabled = false;

                                    FTP.FTPSendFile(PathFile, $"Member_{TBTeacherNo_Reg.Text}.pdf");
                                    if(BankTeacher.Class.ProtocolSharing.FileZilla.StatusReturn == true)
                                    {
                                        Class.SQLConnection.InputSQLMSSQL(SQLDefault[3].Replace("{TeacherNo}", TBTeacherNo_Reg.Text)
                                        .Replace("{TeacherNoAddBy}", BankTeacher.Class.UserInfo.TeacherNo)
                                        .Replace("{StartAmount}",TBStartAmountShare_Reg.Text)
                                        .Replace("{Month}", BankTeacher.Bank.Menu.Date[1])
                                        .Replace("{Year}", BankTeacher.Bank.Menu.Date[0])
                                        .Replace("{PathFile}", FTP.HostplusPathFile + $"Member_{TBTeacherNo_Reg.Text}.pdf"));
                                    
                                        MessageBox.Show("สมัครเสร็จสิ้น", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    else
                                    {
                                        MessageBox.Show("สมัครสมาชิกไม่สำเร็จโปรดลองใหม่อีกครั้ง", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("ยกเลิกการสมัคร", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }

                            }
                            else
                            {
                                MessageBox.Show("รายชื่อซ้ำ", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {
                            MessageBox.Show("ไม่สามารถสมัครสมาชิกได้เนื่องจาก \r\n ราคาหุ้นเริ่มต้นต่ำหรือสูงเกินไป \r\n โปรดแก้ไข ราคาหุ้นขั้นต่ำ หรือ สูงสุด ที่หน้าตั้งค่า", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("โปรดเลือกสมาชิกในการสมัคร", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("ไม่พบเอกสารสำหรับสมัครสมาชิก \r\n โปรดอัพโหลดเอกสารก่อนทำรายการ", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            printPreviewDialog1.Document = printDocument1;
            ToolStripButton b = new ToolStripButton();
            //b.Image = Properties.Resources.Reports1;
            b.DisplayStyle = ToolStripItemDisplayStyle.Image;
            b.Click += printPreview_PrintClick;
            ((ToolStrip)(printPreviewDialog1.Controls[1])).Items.RemoveAt(0);
            ((ToolStrip)(printPreviewDialog1.Controls[1])).Items.Insert(0, b);
            printPreviewDialog1.ShowDialog();
            //if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            //{
            //    printDocument1.Print();

            //}
        }
        private void printPreview_PrintClick(object sender, EventArgs e)
        {
            try
            {
                printDialog1.Document = printDocument1;
                if (printDialog1.ShowDialog() == DialogResult.OK)
                {
                    printDocument1.Print();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ToString());
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
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "pdf files(*.pdf)|*.pdf";
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    PathFile = dialog.FileName;
                    BTdeletefile_Reg.Visible = true;
                }
            }
            else
            {
                MessageBox.Show("กรุณากรอก รหัสผู้ใช้ก่อน", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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
                        BTOpenfile_Reg.Enabled = true;
                        Checkmember(false);
                        TBStartAmountShare_Reg.Enabled = true;
                    }
                }
                catch (Exception ex)
                {
                    BTPrintfShare_Reg.Enabled = false;
                    BTOpenfile_Reg.Enabled = false;
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
                    BTdeletefile_Reg.Visible = false;
                    TBTeacherNo_Reg.Text = "";
                    TBTeacherName_Reg.Text = "";
                    TBStartAmountShare_Reg.Text = BankTeacher.Bank.Menu.startAmountMin.ToString(); ;
                    Check = 0;
                    StatusBoxFile = 0;
                    CheckBRegister = false;
                    CheckBCancel = false;
                    Saving = 0;
                    // ไฟล์
                    StatusBoxFile = 0;
                    BTOpenfile_Reg.Text = "อัพโหลดไฟล์";
                    LScan_Reg.Text = "ยังไม่ได้อัพโหลดไฟล์";
                    BTPrintfShare_Reg.Enabled = false;
                    LScan_Reg.ForeColor = Color.Red;
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

        private void BTdeletefile_Reg_Click(object sender, EventArgs e)
        {
            PathFile = null;
            BTdeletefile_Reg.Visible = false;
        }
    }
}