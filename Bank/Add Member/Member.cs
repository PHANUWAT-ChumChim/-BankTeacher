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

namespace BankTeacher.Bank
{
    public partial class MemberShip : Form
    {
        //------------------------- index -----------------
        int Check = 0;
        int StatusBoxFile = 0;
        String imgeLocation = "";
        bool CheckBRegister = false;
        bool CheckBCancel = false;

        //----------------------- index code -------------------- ////////

        public MemberShip()
        {
            InitializeComponent();
            TBStartAmountShare_Reg.Text = BankTeacher.Bank.Menu.startAmountMin.ToString();
            Relaodcancelmember();
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
        /// <para>[4] Change Status Member INPUT: {TeacherNoAddBy} {TeacherNo} {Note} {DocStatusNo} {DocUploadPath} {Status} {TeacherNo}</para>
        /// <para>[5] SELECT MEMBER INPUT: {Text}</para>
        /// <para>[6] Select MemberResignation INPUT: {Date} </para>
        /// </summary>
        private String[] SQLDefault = new String[]
        {
		   //[0] Insert Teacher Data INPUT:{TeacherNo},{TeacherAddBy},{StartAmount} 
		   "INSERT INTO EmployeeBank.dbo.tblMember(TeacherNo,TeacherAddBy,StartAmount,DateAdd) \r\n"+
           "VALUES('{TeacherNo}','{TeacherAddBy}',{StartAmount}, CURRENT_TIMESTAMP); \r\n\r\n"
            ,

           //[1] SELECT Member  INPUT:{Text}
          "SELECT TeacherNo,Name,null,Fname \r\n " +
          "FROM(SELECT ISNULL(b.TeacherNo , a.TeacherNo) as TeacherNo ,  CAST(ISNULL(c.PrefixName , '') + '' + Fname +' '+ Lname as NVARCHAR)AS NAME, Fname , b.MemberStatusNo \r\n " +
          "FROM Personal.dbo.tblTeacherHis as a \r\n " +
          "FULL OUTER JOIN EmployeeBank.dbo.tblMember as b on a.TeacherNo = b.TeacherNo \r\n " +
          "LEFT JOIN BaseData.dbo.tblPrefix as c ON a.PrefixNo = c.PrefixNo  \r\n " +
          "WHERE a.TeacherNo LIKE '{Text}%' or CAST(Fname +' '+ Lname as NVARCHAR) LIKE '{Text}%' ) as a \r\n " +
          "WHERE a.MemberStatusNo = 2 or a.MemberStatusNo IS NULL \r\n " +
          "ORDER BY a.Fname; "
          ,

          //[2]  Select Detail Memner INPUT: {TeacherNo} 
          "SELECT a.TeacherNo ,CAST(b.PrefixName+' '+Fname +' '+ Lname as NVARCHAR)AS Name, a.IdNo ,a.cNo,a.cMu,c.TumBonName,d.AmPhurName,e.JangWatLongName,a.TelMobile \r\n " +
          "FROM Personal.dbo.tblTeacherHis as a \r\n " +
          "LEFT JOIN BaseData.dbo.tblPrefix as b ON a.PrefixNo = b.PrefixNo  \r\n " +
          "LEFT JOIN BaseData.dbo.tblTumBon as c on a.cTumBonNo = c.TumBonNo \r\n " +
          "LEFT JOIN BaseData.dbo.tblAmphur as d on a.cAmPhurNo = d.AmphurNo \r\n " +
          "LEFT JOIN BaseData.dbo.tblJangWat as e on a.cJangWatNo = e.JangWatNo \r\n " +
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
          "VALUES(@BillNo,1,{StartAmount},{Month},{Year},1) \r\n " +
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
          "VALUES(@BillNo,1,{StartAmount},{Month},{Year},1) \r\n " +
          " \r\n " +
          "END; \r\n " +
          " "
          ,
          
        //[4] Change Status Member INPUT: {TeacherNoAddBy} {TeacherNo} {Note} {DocStatusNo} {DocUploadPath} {Status}
            "INSERT INTO EmployeeBank.dbo.tblMemberResignation (TeacherNoAddBy,TeacherNo,Date,Note,DocStatusNo,DocUploadPath) \r\n " +
            "VALUES ('{TeacherNoAddBy}','{TeacherNo}',CURRENT_TIMESTAMP,'{Note}','{DocStatusNo}','{DocUploadPath}'); \r\n " +
            " \r\n " +
            "UPDATE EmployeeBank.dbo.tblMember \r\n " +
            "SET MemberStatusNo = '{Status}' \r\n " +
            "WHERE TeacherNo = '{TeacherNo}' "
        ,
        //[5] SELECT MEMBER INPUT: {Text}
            "SELECT TOP(20) a.TeacherNo , CAST(c.PrefixName+' '+[Fname] +' '+ [Lname] as NVARCHAR)AS Name, e.SavingAmount,    \r\n " +
            "b.TeacherLicenseNo,b.IdNo AS IDNo,b.TelMobile ,a.StartAmount,CAST(d.MemberStatusName as nvarchar) AS UserStatususing    \r\n " +
            "FROM EmployeeBank.dbo.tblMember as a    \r\n " +
            "LEFT JOIN Personal.dbo.tblTeacherHis as b ON a.TeacherNo = b.TeacherNo    \r\n " +
            "LEFT JOIN BaseData.dbo.tblPrefix as c ON c.PrefixNo = b.PrefixNo   \r\n " +
            "INNER JOIN EmployeeBank.dbo.tblMemberStatus as d on a.MemberStatusNo = d.MemberStatusNo  \r\n " +
            "LEFT JOIN EmployeeBank.dbo.tblShare as e on a.TeacherNo = e.TeacherNo \r\n " +
            "WHERE a.MemberStatusNo = 1 and a.TeacherNo LIKE '%{Text}%'  or CAST(c.PrefixName+' '+[Fname] +' '+ [Lname] as NVARCHAR) LIKE '%{Text}%'   and a.MemberStatusNo = 1         \r\n " +
            "GROUP BY a.TeacherNo , CAST(c.PrefixName+' '+[Fname] +' '+ [Lname] as NVARCHAR), e.SavingAmount,    \r\n " +
            "b.TeacherLicenseNo,b.IdNo ,b.TelMobile ,a.StartAmount,CAST(d.MemberStatusName as nvarchar)   \r\n " +
            "ORDER BY a.TeacherNo; "
            ,
            //[6] Select MemberResignation INPUT: {Date}
           "SELECT Date,b.TeacherNo , CAST (ISNULL(c.PrefixName , '')+' '+Fname + ' ' + Lname as nvarchar),Note \r\n " +
          "FROM EmployeeBank.dbo.tblMemberResignation as a \r\n " +
          "LEFT JOIN Personal.dbo.tblTeacherHis as b on a.TeacherNo = b.TeacherNo \r\n " +
          "LEFT JOIN BaseData.dbo.tblPrefix as c on b.PrefixNo = c.PrefixNo \r\n " +
          "WHERE CAST(CAST(DATE  as  date)as nvarchar(50))  LIKE  '{Date}%'"
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
        // Available values|  SQLDefault[1] / TB /
        private void BSave_Click_1(object sender, EventArgs e)
        {
            if (CheckBRegister == false && TBTeacherName_Reg.Text != "")
            {
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
                            }
                            else
                            {
                                MessageBox.Show("ยกเลิกการสมัคร", "สมัครสมาชิก", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        // ใช้งานเเน่นอนไม่ต้องลบนะครับ
        private void T()
        {
            //private void button2_Click(object sender, EventArgs e)
            //{
            //    Image File;
            //    //String imgeLocation = "";
            //    try
            //    {
            //        OpenFileDialog dialog = new OpenFileDialog();
            //        dialog.Filter = "pdf files(*.pdf)|*.pdf";
            //        if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //        {
            //            //imgeLocation = dialog.FileName;
            //            File = Image.FromFile(dialog.FileName);
            //            //pictureBox1.Image = File;

            //            //        }
            //            //    }
            //            //    catch (Exception)
            //            //    {
            //            //        MessageBox.Show("An Error Occured","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            //            //    }
            //        }
            //    }
            //    catch
            //    {

            //    }
            //}
        }
        //----------------------- Printf -------------------- ////////
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            BankTeacher.Class.Print.PrintPreviewDialog.PrintMember(e,SQLDefault[2],BankTeacher.Bank.Menu.Date[2],BankTeacher.Bank.Menu.Monthname,(Convert.ToInt32(BankTeacher.Bank.Menu.Date[0]) + 543).ToString(),TBTeacherNo_Reg.Text,TBStartAmountShare_Reg.Text);
        }

        private void BTOpenfile_Click(object sender, EventArgs e)
        {
            if(TBTeacherNo_Reg.Text.Length == 6)
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
                            BTOpenfile_Reg.Text = "ส่งไฟล์";
                            StatusBoxFile = 1;
                            LScan_Reg.Text = "Scan(  พบไฟล์  )";
                        }

                    }
                    catch
                    {
                        MessageBox.Show("An Error Occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (StatusBoxFile == 1)
                {
                    var smb = new SmbFileContainer("Member");
                    if (smb.IsValidConnection())
                    {
                        String Return = smb.SendFile(imgeLocation, "Member" + TBTeacherNo_Reg.Text + ".pdf");
                        MessageBox.Show(Return, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        StatusBoxFile = 0;
                        BTOpenfile_Reg.Text = "เปิดไฟล์";
                        imgeLocation = "";
                    }
                    else
                    {
                        MessageBox.Show("ไม่สามารถสร้างไฟล์ในที่นั้นได้", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
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
                if(TBTeacherNo_Reg.Text.Length == 6)
                {
                    try
                    {
                         DataSet ds = Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[1].Replace("{Text}", TBTeacherNo_Reg.Text));
                        TBTeacherName_Reg.Text = ds.Tables[0].Rows[0][1].ToString();
                        Check = 1;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
            }
            else if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back && Check == 1)
            {
                TBTeacherName_Reg.Text = "";
                CheckBRegister = false;
                Check = 0;
            }
        }

        private void BTdeletefile_Click(object sender, EventArgs e)
        {
            StatusBoxFile = 0;
            BTOpenfile_Reg.Text = "เปิดไฟล์";
            imgeLocation = "";
        }

        private void BSearch_Cancel_Click(object sender, EventArgs e)
        {
            try
            {
                Bank.Search IN = new Bank.Search(SQLDefault[5]);
                IN.ShowDialog();
                if(Bank.Search.Return[0] != "")
                {
                  TBTeacherNO_Cancel.Text = Bank.Search.Return[0];
                    TBTeacherNO_Cancel_KeyDown(new object() , new KeyEventArgs(Keys.Enter));
                }
            }
            catch (Exception x)
            {
                Console.WriteLine(x);
            }
        }

        private void TBTeacherNO_Cancel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (TBTeacherNO_Cancel.Text.Length == 6)
                {
                    try
                    {
                        DataSet ds = Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[5].Replace("{Text}", TBTeacherNO_Cancel.Text));
                        TBTeacherName_Cancel.Text = ds.Tables[0].Rows[0][1].ToString();
                        Check = 1;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
            }
            else if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back && Check == 1)
            {
                TBTeacherName_Cancel.Text = "";
                CheckBCancel = false;
                Check = 0;
            }
        }

        private void BSave_Cancel_Click(object sender, EventArgs e)
        {
            if (CheckBCancel == false && TBTeacherName_Cancel.Text != "")
            {
                if (TBTeacherNO_Cancel.Text != "")
                {
                    Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[4]
                        .Replace("{TeacherNoAddBy}",Class.UserInfo.TeacherNo)
                        .Replace("{TeacherNo}", TBTeacherNO_Cancel.Text)
                        .Replace("{Note}", TBNote_Cancel.Text)
                        .Replace("{DocStatusNo}", "2")
                        .Replace("{DocUploadPath}", "")
                        .Replace("{Status}", "2"));
                    MessageBox.Show("ยกเลิกผู้ใช้เรียบร้อย", "System", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CheckBCancel = true;
                    imgeLocation = "";
                }
                else
                {
                    MessageBox.Show("กรุณาใส่รหัสให้ถูกต้อง", "เตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void BOpenFile_Cancel_Click(object sender, EventArgs e)
        {
            if (TBTeacherNO_Cancel.Text.Length == 6)
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
                            LScan_Cancel.Text = "Scan( พบไฟล์ )";
                            BOpenFile_Cancel.Text = "ส่งไฟล์";
                            StatusBoxFile = 1;
                        }

                    }
                    catch
                    {
                        MessageBox.Show("An Error Occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (StatusBoxFile == 1)
                {
                    var smb = new SmbFileContainer("CancelMember");
                    if (smb.IsValidConnection())
                    {
                        String Return = smb.SendFile(imgeLocation, "CancelMember" + TBTeacherNO_Cancel.Text + ".pdf");
                        if (Return.Contains("Fail"))
                        {
                            MessageBox.Show(Return, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            MessageBox.Show(Return, "System", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        StatusBoxFile = 0;
                        BOpenFile_Cancel.Text = "เปิดไฟล์";
                        LScan_Cancel.Text = "Scan(  ไม่พบ  )";
                        imgeLocation = "";
                    }
                    else
                    {
                        MessageBox.Show("ไม่สามารถสร้างไฟล์ในที่นั้นได้", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            else
            {
                MessageBox.Show("กรุณากรอก รหัสผู้ใช้ก่อน", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BDeleteFile_Cancel_Click(object sender, EventArgs e)
        {
            imgeLocation = "";
            BOpenFile_Cancel.Text = "เปิดไฟล์";
            LScan_Cancel.Text = "Scan(  ไม่พบ  )";
            StatusBoxFile = 0;
        }
        private void Relaodcancelmember()
        {
            int Year = Convert.ToInt32(BankTeacher.Bank.Menu.Date[0]);
            for (int a = 0; a < 5; a++)
            {
                if(Class.SQLConnection.InputSQLMSSQL(SQLDefault[6]
                    .Replace("{Date}",Year.ToString())).Rows.Count != 0)
                CBYear_HistoryCancel.Items.Add(Year);
                Year--;
            }
            CBYear_HistoryCancel.SelectedIndex = 0;
        }

        private void CBYear_HistoryCancel_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = Class.SQLConnection.InputSQLMSSQL(SQLDefault[6].Replace("{Date}", CBYear_HistoryCancel.Text));
            if(dt.Rows.Count != 0)
            {
                DGV_HistoryCancel.Rows.Clear();
                for(int x = 0; x < dt.Rows.Count; x++)
                    DGV_HistoryCancel.Rows.Add((Convert.ToDateTime(dt.Rows[x][0].ToString())).ToString("yyyy-MM-dd") , dt.Rows[x][1].ToString() , dt.Rows[x][2].ToString(), dt.Rows[x][3].ToString());
                for(int x = 0; x < dt.Rows.Count; x++)
                {
                    if (x % 2 == 1)
                    {
                        DGV_HistoryCancel.Rows[x].DefaultCellStyle.BackColor = Color.AliceBlue;
                    }
                }
            }
        }

        private void tabControl1_TabIndexChanged(object sender, EventArgs e)
        {
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 2)
            {
                CBYear_HistoryCancel_SelectedIndexChanged(new object(), new EventArgs());
            }
        }
    }
}