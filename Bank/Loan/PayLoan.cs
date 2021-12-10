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

namespace BankTeacher.Bank.Loan
{
    public partial class PayLoan : Form
    {
        int Check = 0;
        int StatusBoxFile = 0;
        String imgeLocation = "";

        /// <summary>
        /// <para>[0] SELECT MemberLona  INPUT: {Text}</para>
        /// <para>[1] SELECT LOAN INPUT: {TeacherNo} </para>
        /// <para>[2] SELECT Detail Loan INPUT: {LoanNo}</para>
        /// <para>[3] Select Payment Name INPUT: - </para>
        /// <para>[4] UPDATE Payment Loan INPUT: {LoanID} {TeacherNoPay} {PaymentNo}</para>
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
          "SELECT a.LoanNo , CAST(ISNULL(d.PrefixName+' ','')+ Fname + ' ' + Lname AS NVARCHAR) , ISNULL(CAST(a.PayDate as int) , 1) \r\n " +
          "FROM EmployeeBank.dbo.tblLoan as a   \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblGuarantor as b on a.LoanNo = b.LoanNo   \r\n " +
          "LEFT JOIN Personal.dbo.tblTeacherHis as c on a.TeacherNo = c.TeacherNo  \r\n " +
          "LEFT JOIN BaseData.dbo.tblPrefix as d on c.PrefixNo = d.PrefixNo  \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblMember as e on b.TeacherNo = e.TeacherNo \r\n" +
          "WHERE a.TeacherNo = '{TeacherNo}' and LoanStatusNo = 1 and MemberStatusNo = 1 \r\n " +
          "GROUP BY a.LoanNo , CAST(ISNULL(d.PrefixName+' ','') + Fname + ' ' + Lname AS NVARCHAR) ,ISNULL(CAST(a.PayDate as int) , 1) \r\n " +
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
          //[4] UPDATE Payment Loan INPUT: {LoanID} {TeacherNoPay} {PaymentNo}
          "UPDATE EmployeeBank.dbo.tblLoan \r\n " +
          "SET PayDate = CURRENT_TIMESTAMP , TeacherNoPay = '{TeacherNoPay}', BillDetailPaymentNo = '{PaymentNo}' , LoanStatusNo = 2 \r\n " +
          "WHERE LoanNo = '{LoanID}'; "
          ,

        };
        public PayLoan()
        {
            InitializeComponent();
            ComboBox[] cb = new ComboBox[] { CBB4Oppay };
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
                IN = new Bank.Search(SQLDefault[0],"");
                IN.ShowDialog();
                
                if(Bank.Search.Return.Length != 1)
                {
                    TBTeacherNo.Text = Bank.Search.Return[0];
                    TBTeacherName.Text = Bank.Search.Return[1];
                    label3.Text = "0";
                    CBB4Oppay.Enabled = false;
                    button1.Enabled = false;
                    comboBox1.Items.Clear();
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
            if (e.KeyCode == Keys.Enter)
            {
                if (TBTeacherNo.Text.Length == 6)
                {
                    DataTable dt = Class.SQLConnection.InputSQLMSSQL(SQLDefault[1].Replace("{TeacherNo}", TBTeacherNo.Text));
                    if (dt.Rows.Count != 0)
                    {
                        TBTeacherName.Text = dt.Rows[0][1].ToString();
                        comboBox1.Enabled = true;
                        comboBox1.Items.Clear();
                        Check = 1;
                        ComboBox[] cb = new ComboBox[] { comboBox1 };
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
                        TBLoanNo.Text = "";
                        TBLoanStatus.Text = "";
                        TBDate.Text = "";
                        comboBox1.Enabled = true;
                        CBB4Oppay.SelectedIndex = 0;
                        if (comboBox1.Items.Count == 1)
                        {
                            comboBox1.SelectedIndex = 0;
                            label9.Visible = false;
                        }
                        else if (comboBox1.Items.Count == 0)
                        {
                            label9.Visible = true;
                            comboBox1.Enabled = false;
                        }

                    }
                    else
                    {
                        MessageBox.Show("รหัสผู้ใช้ไม่ถูกต้อง", "System", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        TBTeacherNo.Text = "";
                        TBTeacherNo.Focus();
                    }

                }

            }
            else if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
            {
                if (Check == 1)
                {
                    comboBox1.Items.Clear();
                    comboBox1.SelectedIndex = -1;
                    TBTeacherName.Text = "";
                    TBLoanNo.Text = "";
                    TBLoanStatus.Text = "";
                    TBDate.Text = "";
                    label3.Text = "0";
                    if (CBB4Oppay.SelectedIndex != -1)
                        CBB4Oppay.SelectedIndex = -1;
                    CBB4Oppay.Enabled = false;
                    button1.Enabled = false;
                    comboBox1.Enabled = false;
                    StatusBoxFile = 0;
                    imgeLocation = "";
                    Check = 0;
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            BankTeacher.Class.ComboBoxPayment Loan = (comboBox1.SelectedItem as BankTeacher.Class.ComboBoxPayment);
            DataTable dt = BankTeacher.Class.SQLConnection.InputSQLMSSQL(SQLDefault[2].Replace("{LoanNo}", Loan.No));
            if (dt.Rows.Count != 0)
            {
                TBDate.Text = (Convert.ToDateTime(dt.Rows[0][1].ToString())).ToString("dd/MM/yyyy");
                TBLoanNo.Text = Loan.No;
                TBLoanStatus.Text = "ดำเนินการ";
                CBB4Oppay.Enabled = true;
                label3.Text = dt.Rows[0][3].ToString();
            }
        }

        private void CBB4Oppay_SelectedIndexChanged(object sender, EventArgs e)
        {
            button1.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("ยืนยันการจ่ายเงิน", "ระบบ", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes && CBB4Oppay.SelectedIndex != -1/* && StatusBoxFile == 1*/)
            {
                try
                {
                    BankTeacher.Class.ComboBoxPayment Payment = (CBB4Oppay.SelectedItem as BankTeacher.Class.ComboBoxPayment);
                    Class.SQLConnection.InputSQLMSSQL(SQLDefault[4].Replace("{LoanID}", TBLoanNo.Text)
                        .Replace("{TeacherNoPay}", Class.UserInfo.TeacherNo)
                        .Replace("{PaymentNo}", Payment.No));
                    MessageBox.Show("จ่ายสำเร็จ", "System", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    comboBox1.Items.Clear();
                    comboBox1.SelectedIndex = -1;
                    TBTeacherName.Text = "";
                    TBLoanNo.Text = "";
                    TBLoanStatus.Text = "";
                    TBDate.Text = "";
                    label3.Text = "0";
                    CBB4Oppay.SelectedIndex = -1;
                    CBB4Oppay.Enabled = false;
                    TBTeacherNo.Text = "";
                    button1.Enabled = false;
                    comboBox1.Enabled = false;
                    TBTeacherNo.Focus();
                    Check = 0;

                }
                catch
                {
                    MessageBox.Show("จ่ายล้มเหลว", "System", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (StatusBoxFile == 0)
            {
                MessageBox.Show("จ่ายไม่สำเร็จ โปรดอัพโหลดไฟล์ก่อน", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (CBB4Oppay.SelectedIndex == -1)
                MessageBox.Show("จ่ายไม่สำเร็จ โปรดระบุช่องทางการจ่ายเงิน", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            
        }


        private void BTOpenfile_Click(object sender, EventArgs e)
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
                        
                        var smb = new SmbFileContainer("Loan");
                        if (smb.IsValidConnection())
                        {
                            String Return = smb.SendFile(imgeLocation, "Loan_" + TBTeacherNo.Text + ".pdf");
                            MessageBox.Show(Return, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            if (Return.Contains("อัพโหลดสำเร็จ"))
                            {
                                imgeLocation = "";
                                label5.ForeColor = Color.Green;
                                label5.Text = "อัพโหลดไฟล์สำเร็จ";
                                BTdeletefile.Enabled = true;
                                StatusBoxFile = 1;
                            }
                            else
                                StatusBoxFile = 0;
                        }
                        else
                        {
                            MessageBox.Show("ไม่สามารถสร้างไฟล์ในที่นั้นได้", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            StatusBoxFile = 0;
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

        private void BTdeletefile_Click(object sender, EventArgs e)
        {
            //StatusBoxFile = 0;
            ////BTOpenfile.Text = "เปิดไฟล์";
            ////label6.Text = "Scan(  ไม่พบ  )";
            //label5.ForeColor = Color.Black;
            //label5.Text = "อัพโหลดเอกสารสัญญากู้ที่มีลายเซ็นครบถ้วน";
            //imgeLocation = "";
            //BTdeletefile.Enabled = false;
        }
        private void PayLoan_SizeChanged(object sender, EventArgs e)
        {
            Class.FromSettingMedtod.ChangeSizePanal(this, panel1);
        }

        private void BExitForm_Click(object sender, EventArgs e)
        {
            BankTeacher.Class.FromSettingMedtod.ReturntoHome(this);
        }

        private void PayLoan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (TBTeacherNo.Text.Length != 0)
                {
                    TBTeacherNo.Text = "";
                    comboBox1.Items.Clear();
                    comboBox1.SelectedIndex = -1;
                    TBTeacherName.Text = "";
                    TBLoanNo.Text = "";
                    TBLoanStatus.Text = "";
                    TBDate.Text = "";
                    label3.Text = "0";
                    if (CBB4Oppay.SelectedIndex != -1)
                        CBB4Oppay.SelectedIndex = -1;
                    CBB4Oppay.Enabled = false;
                    button1.Enabled = false;
                    comboBox1.Enabled = false;
                    StatusBoxFile = 0;
                    imgeLocation = "";
                    Check = 0;
                }
                else
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
                //panel7.Enabled = true;
                tabControl1.Enabled = true;
            }
            else
            {
                //panel7.Enabled = false;
                tabControl1.Enabled = false;
            }
        }
        private void tabControl1_VisibleChanged(object sender, EventArgs e)
        {
            if (tabControl1.Enabled == false)
            {
                MessageBox.Show("ไม่พบรายการ กรูณาลงรายการใหม่อีกครั้งค่ะ");
            }
        }
    }
}
