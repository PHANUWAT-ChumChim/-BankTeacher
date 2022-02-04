using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankTeacher.Bank.Loan
{
    public partial class PayLoan : Form
    {
        //// ======================= ข้อมูลเเบบปริ้น ====================
        ////ข้อมูลส่วนตัว
        //public static string info_name;
        //public static string info_id;
        //// จ่าย
        //public static string info_totelAmountpay;
        //public static string info_BillLoan;
        //public static string info_datepay;
        //// กู้
        //public static string info_Lona_AmountRemain;

        int Check = 0;
        int StatusBoxFile = 0;
        String PathFile = "";
        bool CheckStatusWorking = false;
        List<string[]> ItemList = new List<string[]>();
        /// <summary>
        /// <para>[0] SELECT MemberLona  INPUT: {Text}</para>
        /// <para>[1] SELECT LOAN INPUT: {TeacherNo} </para>
        /// <para>[2] SELECT Detail Loan INPUT: {LoanNo}</para>
        /// <para>[3] Select Payment Name INPUT: - </para>
        /// <para>[4] UPDATE Payment Loan INPUT: {LoanID} {TeacherNoPay} {PaymentNo}  {Date}</para>
        /// <para>[5] Chcek Lonapay INPUT : {TeacherNo} </para>
        /// <para>[6] BackPrint payLoan INPUT : {TeacherNo} {Year} </para>
        /// <para>[7] INSERT File INPUT: {TeacherNo} , {PathFile} , {TeacherNoAddBy} {Date}</para>
        /// </summary>
        private String[] SQLDefault = 
        {
          //[0] SELECT MemberLona  INPUT: {Text} 
          " SELECT TOP(20) TeacherNo , NAME  \r\n" +
          " FROM(   \r\n " +
          " SELECT a.TeacherNo, CAST(ISNULL(c.PrefixName+' ','') + Fname + ' ' + Lname AS nvarchar)AS NAME,SavingAmount,Fname ,LoanStatusNo \r\n " +
          " FROM EmployeeBank.dbo.tblLoan as a  \r\n " +
          " LEFT JOIN Personal.dbo.tblTeacherHis as b on a.TeacherNo = b.TeacherNo  \r\n " +
          " LEFT JOIN BaseData.dbo.tblPrefix as c on b.PrefixNo = c.PrefixNo  \r\n " +
          " LEFT JOIN EmployeeBank.dbo.tblShare as d on a.TeacherNo = d.TeacherNo  \r\n " +
          " WHERE a.LoanStatusNo = 1 \r\n " +
          " GROUP BY a.TeacherNo,CAST(ISNULL(c.PrefixName+' ','')+Fname+' '+Lname as NVARCHAR),d.SavingAmount ,Fname , LoanStatusNo) AS A   \r\n " +
          " WHERE a.TeacherNo LIKE '%{Text}%' or Fname LIKE '%{Text}%'  \r\n " +
          " ORDER BY Fname;   "
          ,
          //[1] SELECT LOAN INPUT: {TeacherNo}
          "SELECT a.LoanNo , CAST(ISNULL(d.PrefixName+' ','')+ Fname + ' ' + Lname AS NVARCHAR) , a.LoanStatusNo \r\n " +
          "FROM EmployeeBank.dbo.tblLoan as a   \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblGuarantor as b on a.LoanNo = b.LoanNo   \r\n " +
          "LEFT JOIN Personal.dbo.tblTeacherHis as c on a.TeacherNo = c.TeacherNo  \r\n " +
          "LEFT JOIN BaseData.dbo.tblPrefix as d on c.PrefixNo = d.PrefixNo  \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblMember as e on b.TeacherNo = e.TeacherNo \r\n" +
          "WHERE a.TeacherNo = '{TeacherNo}' and LoanStatusNo = 1 and MemberStatusNo = 1 \r\n " +
          "GROUP BY a.LoanNo , CAST(ISNULL(d.PrefixName+' ','') + Fname + ' ' + Lname AS NVARCHAR) ,a.LoanStatusNo \r\n " +
          "ORDER BY a.LoanNo "
          

                ,
          //[2] SELECT Detail Loan INPUT: {LoanNo} 
          "SELECT CAST(ISNULL(d.PrefixName+' ','') + Fname + ' ' + Lname AS NVARCHAR) ,DateAdd,a.PayDate,LoanAmount \r\n " +
          "FROM EmployeeBank.dbo.tblLoan as a \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblGuarantor as b on a.LoanNo = b.LoanNo \r\n " +
          "LEFT JOIN Personal.dbo.tblTeacherHis as c on b.TeacherNo = c.TeacherNo \r\n " +
          "LEFT JOIN BaseData.dbo.tblPrefix as d on c.PrefixNo = d.PrefixNo \r\n " +
          "WHERE a.LoanNo = '{LoanNo} ' and LoanStatusNo != 4 and LoanStatusNo != 3\r\n "
          ,
          //[3] Check BillDetailPayment INPUT: -  
          "SELECT Convert(nvarchar(20),Name) , BillDetailpaymentNo  \r\n " +
          "FROM EmployeeBank.dbo.tblBillDetailPayment \r\n " +
          "WHERE Status = 1 and BillDetailpaymentNo <> 3"
          ,
          //[4] UPDATE Payment Loan INPUT: {LoanID} {TeacherNoPay} {PaymentNo}  {Date}
          "UPDATE EmployeeBank.dbo.tblLoan \r\n " +
          "SET PayDate = '{Date}' , TeacherNoPay = '{TeacherNoPay}', BillDetailPaymentNo = '{PaymentNo}' , LoanStatusNo = 2 \r\n " +
          "WHERE LoanNo = '{LoanID}'; "
          ,
          //[5] Chcek Lonapay INPUT : {TeacherNo}
          "SELECT a.LoanNo,a.TeacherNo,a.LoanStatusNo \r\n " +
          "FROM EmployeeBank.dbo.tblLoan as a \r\n " +
          "WHERE a.TeacherNo = '{TeacherNo}' and a.LoanStatusNo = 2 "
          ,
          //[6] BackPrint payLoan INPUT : {TeacherNo} {Year}
          "SELECT a.LoanNo,CAST(a.PayDate as date),a.LoanAmount,b.LoanStatusName,CAST(d.PrefixName+' '+c.Fname+' '+c.Lname as nvarchar),CAST(f.Name as nvarchar)  \r\n " +
          "FROM EmployeeBank.dbo.tblLoan as a \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblLoanStatus b ON a.LoanStatusNo = b.LoanStatusNo \r\n " +
          "LEFT JOIN Personal.dbo.tblTeacherHis c ON a.TeacherNoAddBy = c.TeacherNo \r\n " +
          "LEFT JOIN BaseData.dbo.tblPrefix d ON c.PrefixNo = d.PrefixNo \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblBillDetailPayment f ON a.BillDetailPaymentNo = f.BillDetailPaymentNo \r\n " +
          "WHERE a.TeacherNo = '{TeacherNo}' AND YEAR(a.PayDate) = {Year} AND a.LoanStatusNo = 2"
           ,
            //[7] INSERT File INPUT: {TeacherNo} , {PathFile} , {TeacherNoAddBy}  {Date}
           "INSERT INTO EmployeeBank.dbo.tblFile(TeacherNo,FiletypeNo,pathFile,TeacherAddBy,LoanID,DateAddFile,IsUse,TeacherRemoveFileBy,DateRemoveFile,StatusFileInSystem) \r\n " +
          "VALUES('{TeacherNo}','3','{PathFile}','{TeacherNoAddBy}',{LoanID},CAST('{Date}' as date),1,null,null,1) \r\n " +
          " \r\n " +
          "UPDATE EmployeeBank.dbo.tblLoan \r\n " +
          "SET DocStatusNo = 1 , DocUploadPath = '{PathFile}' \r\n"+
          "WHERE LoanNo = '{LoanID}' "
           ,

        };
        public PayLoan()
        {
            InitializeComponent();
            ComboBox[] cb = new ComboBox[] { CBPayment };
            DataTable dtPayment = Class.SQLConnection.InputSQLMSSQL(SQLDefault[3]);
            for (int a = 0; a < dtPayment.Rows.Count; a++)
                for (int x = 0; x < cb.Length; x++)
                    cb[x].Items.Add(new BankTeacher.Class.ComboBoxPayment(dtPayment.Rows[a][0].ToString(),
                        dtPayment.Rows[a][1].ToString()));
        }
        private void BSearchTeacher_Click(object sender, EventArgs e)
        {
            Bank.Search IN;
            try
            {
                StatusBoxFile = 0;
                IN = new Bank.Search(SQLDefault[0]);
                IN.ShowDialog();
                
                if(Bank.Search.Return[0] != "")
                {
                    TBTeacherNo.Text = Bank.Search.Return[0];
                    TBTeacherName.Text = Bank.Search.Return[1];
                    label3.Text = "0";
                    CBPayment.Enabled = false;
                    CBlistpayloan.Items.Clear();
                    DGV_PayLoan.Rows.Clear();
                    Check = 1;
                    TBTeacherNo_KeyDown(new object(), new KeyEventArgs(Keys.Enter));
                }
                //
            }
            catch (Exception x)
            {
                Console.WriteLine(x);
            }
        }
        private void TBTeacherNo_KeyDown(object sender, KeyEventArgs e)
        {
            
            panel5.Visible = false;
            tabControl1.Enabled = false;
            tabControl1.SelectedIndex = 0;
            if (e.KeyCode == Keys.Enter) 
            {
                TBTeacherNo.Text = TBTeacherNo.Text.Replace("t", "T");
                DataTable dt = Class.SQLConnection.InputSQLMSSQL(SQLDefault[1].Replace("{TeacherNo}", TBTeacherNo.Text));
                if (dt.Rows.Count != 0)
                {
                    TBTeacherName.Text = dt.Rows[0][1].ToString();
                    CBlistpayloan.Enabled = true;
                    CBlistpayloan.Items.Clear();
                    Check = 1;
                    ComboBox[] cb = new ComboBox[] { CBlistpayloan };
                    int CheckPay = 0;
                    for (int x = 0; x < dt.Rows.Count; x++)
                    {
                        for (int aa = 0; aa < cb.Length; aa++)
                        {
                            int.TryParse(dt.Rows[x][2].ToString(), out CheckPay);
                            if (CheckPay == 1)
                            {
                                cb[aa].Items.Add(new BankTeacher.Class.ComboBoxPayment("รายการกู้ " + dt.Rows[x][0], dt.Rows[x][0].ToString()));
                                    
                            }
                        }
                    }
                    tabControl1.Enabled = true;
                    DGV_PayLoan.Rows.Clear();
                    CBlistpayloan.Enabled = true;
                    CBPayment.SelectedIndex = 0;
                    if (CBlistpayloan.Items.Count == 1)
                    {
                        CBlistpayloan.SelectedIndex = 0;
                        label9.Visible = false;
                    }
                    else if (CBlistpayloan.Items.Count == 0)
                    {
                        label9.Visible = true;
                        CBlistpayloan.Enabled = false;
                    }
                    Checkmember(false);
                }
                else
                {
                    MessageBox.Show("รหัสผู้ใช้ไม่ถูกต้อง", "System", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    TBTeacherNo.Text = "";
                    TBTeacherNo.Focus();
                }
            }
            else if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
            {
                if (Check == 1)
                {
                    DGV_PayLoan.Rows.Clear();
                    CBlistpayloan.Items.Clear();
                    CBlistpayloan.SelectedIndex = -1;
                    TBTeacherName.Text = "";
                    label3.Text = "0";
                    BTRemoveFile.Visible = false; 
                    PathFile = "";
                    if (CBPayment.SelectedIndex != -1)
                        CBPayment.SelectedIndex = -1;
                    CBPayment.Enabled = false;
                    CBlistpayloan.Enabled = false;
                    Checkmember(true);
                    StatusBoxFile = 0;
                    PathFile = "";
                    Check = 0;
                }
            }
        }

        private void Checkmember(bool tf)
        {
            TBTeacherNo.Enabled = tf;
            BTSearch.Enabled = tf;
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(CBlistpayloan.SelectedIndex != -1)
            {
                panel5.Visible = true;
                BankTeacher.Class.ComboBoxPayment Loan = (CBlistpayloan.SelectedItem as BankTeacher.Class.ComboBoxPayment);
                DataTable dt = BankTeacher.Class.SQLConnection.InputSQLMSSQL(SQLDefault[2].Replace("{LoanNo}", Loan.No));
                if (DGV_PayLoan.RowCount == 0)
                {
                    if (dt.Rows.Count != 0)
                    {
                        DGV_PayLoan.Rows.Add(1, Loan.No, (Convert.ToDateTime(dt.Rows[0][1].ToString())).ToString("dd/MM/yyyy"), "จ่ายกู้", dt.Rows[0][3].ToString());
                        CBPayment.Enabled = true;
                        label3.Text = dt.Rows[0][3].ToString();
                        // ================ Clear ItemList ======================
                        CBlistpayloan.Items.RemoveAt(CBlistpayloan.SelectedIndex);
                        if (CBlistpayloan.Items.Count == 0)
                            CBlistpayloan.Enabled = false;
                        BTSave.Enabled = true;
                    }
                }
                else
                {
                    DialogResult dialogResult = MessageBox.Show("ไม่สามารถ ทำรายการจ่ายมากกว่า 2 รายการขึ้นไป\r\n คุณต้องการเเทนที่รายการใหม่ หรือ ไม่", "แจ้งเตือน", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if(dialogResult == DialogResult.Yes)
                    {
                        label3.Text = dt.Rows[0][3].ToString();
                        CBlistpayloan.Items.RemoveAt(CBlistpayloan.SelectedIndex); // ลบรายการใน cb
                        Class.ComboxAdd_item.NumberRanking(0, DGV_PayLoan, CBlistpayloan, "รายการกู้"); // เช็ครายการใหม่
                        DGV_PayLoan.Rows.Add(1, Loan.No, (Convert.ToDateTime(dt.Rows[0][1].ToString())).ToString("dd/MM/yyyy"), "จ่ายกู้", dt.Rows[0][3].ToString());
                        BTSave.Enabled = true;
                    }
                }
            }
        }

        private void StatusEnableForm (bool Status)
        {
            CBPayment.Enabled = Status;
            BTSave.Enabled = Status;
            CBlistpayloan.Enabled = Status;
            request = Status;
            TBTeacherNo.Enabled = Status;
            DGV_PayLoan.Enabled = Status;
        }
        private void BTSave_Click(object sender, EventArgs e)
        {
            BankTeacher.Class.ProtocolSharing.FileZilla.FileZillaConnection FTP = new Class.ProtocolSharing.FileZilla.FileZillaConnection("Loan");
            DialogResult SaveCheck = MessageBox.Show("ยืนยันการจ่ายเงิน", "ระบบ", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (SaveCheck == DialogResult.Yes && CBPayment.SelectedIndex != -1)
            {
                try
                {
                    if (PathFile != "" && PathFile != null)
                    {
                        StatusEnableForm(false);
                        CheckStatusWorking = true;
                        FTP.FTPSendFile(PathFile, $"Loan{DGV_PayLoan.Rows[0].Cells[1].Value.ToString()}.pdf");
                        if (BankTeacher.Class.ProtocolSharing.FileZilla.StatusReturn == true)
                        {
                            bool Yes = true;
                            DataTable dt = Class.SQLConnection.InputSQLMSSQL(SQLDefault[5].Replace("{TeacherNo}", TBTeacherNo.Text));
                            if(dt.Rows.Count > 0)
                            {
                                DialogResult Dr = MessageBox.Show("มีรายการ กู้ อยู่ในระบบอยู่เเล้วคุณต้องการทำรายการเพิ่มหรือไม่", "กู้", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                                if(Dr == DialogResult.Yes)
                                {
                                    Yes = true;
                                }
                                else { Yes = false; }
                            }
                            if (Yes)
                            {
                                String Date = DTPDate.Value.ToString("yyyy:MM:dd");
                                Date = Date.Replace(":", "/");
                                BankTeacher.Class.ComboBoxPayment Payment = (CBPayment.SelectedItem as BankTeacher.Class.ComboBoxPayment);
                                Class.SQLConnection.InputSQLMSSQL((SQLDefault[4] + "\r\n" + SQLDefault[7])
                                    .Replace("{LoanID}", DGV_PayLoan.Rows[0].Cells[1].Value.ToString())
                                    .Replace("{TeacherNoPay}", Class.UserInfo.TeacherNo)
                                    .Replace("{PaymentNo}", Payment.No)
                                    .Replace("{TeacherNo}",TBTeacherNo.Text)
                                    .Replace("{TeacherNoAddBy}",Class.UserInfo.TeacherNo)
                                    .Replace("{PathFile}",FTP.HostplusPathFile+ $"Loan{DGV_PayLoan.Rows[0].Cells[1].Value.ToString()}.pdf")
                                    .Replace("{Date}" , Date));

                                MessageBox.Show("ทำรายการสำเร็จ", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                tabControl1.Enabled = false;
                                printDocument1.DefaultPageSettings.PaperSize = new PaperSize("A4", 595, 842);
                                printDocument1.DefaultPageSettings.Landscape = true;
                                Class.Print.PrintPreviewDialog.info_id = TBTeacherNo.Text;
                                Class.Print.PrintPreviewDialog.info_name = TBTeacherName.Text;
                                Class.Print.PrintPreviewDialog.info_TeacherAdd = Class.UserInfo.TeacherName;
                                Class.Print.PrintPreviewDialog.info_Payment = CBPayment.SelectedItem.ToString();
                                Class.Print.PrintPreviewDialog.info_PayLoanBill = DGV_PayLoan.Rows[0].Cells[1].Value.ToString();
                                Class.Print.PrintPreviewDialog.info_PayLoandate = Bank.Menu.Date_Time_SQL_Now.Rows[0][0].ToString();
                                panel5.Visible = false;
                                

                                if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                                {
                                    printDocument1.Print();
                                }
                            }
                            else
                            {
                                MessageBox.Show("รายการได้ถูกยกเลิกเรียบร้อย", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                StatusEnableForm(true);
                            }
                        }
                        else
                        {
                            MessageBox.Show("ทำรายการล้มเหลวโปรดลองใหม่อีกครั้ง","ระบบ",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                            StatusEnableForm(true);
                        }
                        PathFile = "";
                        CheckStatusWorking = false;
                    }
                    else
                    {
                        MessageBox.Show("ทำรายการไม่สำเร็จ โปรดเลือกเอกสารก่อนทำรายการ", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch
                {
                    MessageBox.Show("ทำรายการไม่สำเร็จ", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (CBPayment.SelectedIndex == -1)
                MessageBox.Show("ทำรายการไม่สำเร็จ โปรดระบุช่องทางการจ่ายเงิน", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        private void BTOpenfile_Click(object sender, EventArgs e)
        {
            if (DGV_PayLoan.Rows.Count != 0)
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "pdf files(*.pdf)|*.pdf";
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    PathFile = dialog.FileName;
                    BTRemoveFile.Visible = true;
                }
            }
        }

        private void BTdeletefile_Click(object sender, EventArgs e)
        {
            PathFile = null;
            BTRemoveFile.Visible = false;
        }
        private void PayLoan_SizeChanged(object sender, EventArgs e)
        {
            Class.FromSettingMedtod.ChangeSizePanal(this, panel1);
        }

        private void BExitForm_Click(object sender, EventArgs e)
        {
            if(!CheckStatusWorking)
                BankTeacher.Class.FromSettingMedtod.ReturntoHome(this);
        }

        private void PayLoan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape && !CheckStatusWorking)
            {
                if (TBTeacherNo.Text.Length != 0)
                {
                    TBTeacherNo.Text = "";
                    CBlistpayloan.Items.Clear();
                    CBlistpayloan.SelectedIndex = -1;
                    TBTeacherName.Text = "";
                    label3.Text = "0"; 
                    PathFile = "";
                    if (CBPayment.SelectedIndex != -1)
                        CBPayment.SelectedIndex = -1;
                    DGV_PayLoan.Rows.Clear();
                    CBPayment.Enabled = false;
                    CBlistpayloan.Enabled = false;
                    StatusBoxFile = 0;
                    PathFile = "";
                    Check = 0;
                    Checkmember(true);
                    panel5.Visible = false;
                    BTRemoveFile.Visible = false;
                }
                else if (!CheckStatusWorking)
                {
                    BExitForm_Click(new object(), new EventArgs());
                }
            }
        }

        private void PayLoan_Load(object sender, EventArgs e)
        {
            DataTable dt = Class.SQLConnection.InputSQLMSSQL(SQLDefault[0].Replace("{Text}", ""));
            if (dt.Rows.Count != 0)
            {
                panel7.Enabled = true;
            }
            else
            {
                panel7.Enabled = false;
                tabControl1.Enabled = false;
            }

            DTPDate.Value = Convert.ToDateTime(Bank.Menu.Date[0] + "/" + Bank.Menu.Date[1] + "/" + Bank.Menu.Date[2]);
            DTPDate.Enabled = DTPDate.Enabled = Bank.Setting.CheckTimeBack;
        }
        private void panel7_VisibleChanged(object sender, EventArgs e)
        {
            if (panel7.Enabled == false)
            {
                MessageBox.Show("ไม่พบรายการ กรุณาลงรายการใหม่อีกครั้ง","เเจ้งเตือนการทำงาน",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                //BExitForm_Click(new object(), new EventArgs());
                Class.FromSettingMedtod.ReturntoHome(this);
            }
        }

        static int SelectIndexRow;
        private void DGV_PayLoan_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int currentMouseOverRow = DGV_PayLoan.HitTest(e.X, e.Y).RowIndex;
                if (currentMouseOverRow != -1)
                {
                    SelectIndexRow = currentMouseOverRow;
                    ContextMenu m = new ContextMenu();
                    m.MenuItems.Add(new MenuItem("ลบออก"));
                    m.Show(DGV_PayLoan, new Point(e.X, e.Y));
                    m.MenuItems[0].Click += new System.EventHandler(this.Delete_Click);
                }
            }
        }
        private void Delete_Click(object sender, EventArgs e)
        {
            Class.ComboxAdd_item.NumberRanking(SelectIndexRow, DGV_PayLoan,CBlistpayloan,"รายการกู้");
            if(CBlistpayloan.Items.Count != 0)
            {
                CBlistpayloan.Enabled = true;
            }
            if (DGV_PayLoan.Rows.Count == 0)
            {
                BTSave.Enabled = false;
                label3.Text = "0";
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
                Class.Print.PrintPreviewDialog.PrintReportGrid(e,DGV_PayLoan, "จ่ายกู้", this.AccessibilityObject.Name, true, true, "A5", 0);
        }
        private bool request;
        private void TB_password_KeyDown(object sender, KeyEventArgs e)
        {
            if(TB_password.Text != "")
                if(e.KeyCode == Keys.Enter)
                {
                    if(TB_password.Text == "EEC")
                    {
                        BTSave.Enabled = true;
                        request = true;
                    }
                    else 
                        MessageBox.Show("รหัสขอทำรายการไม่ถูกต้อง กรูณาเรียกขอสิทธิ์จากผู้บอกสิทธิ์", "รายการพิเศษ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
        }
    }
}
