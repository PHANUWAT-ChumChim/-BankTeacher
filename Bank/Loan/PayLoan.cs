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
        List<string[]> ItemList = new List<string[]>();
        /// <summary>
        /// <para>[0] SELECT MemberLona  INPUT: {Text}</para>
        /// <para>[1] SELECT LOAN INPUT: {TeacherNo} </para>
        /// <para>[2] SELECT Detail Loan INPUT: {LoanNo}</para>
        /// <para>[3] Select Payment Name INPUT: - </para>
        /// <para>[4] UPDATE Payment Loan INPUT: {LoanID} {TeacherNoPay} {PaymentNo}</para>
        /// <para>[5] Chcek Lonapay INPUT : {TeacherNo} </para>
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
          //[5] Chcek Lonapay INPUT : {TeacherNo}
          "SELECT a.LoanNo,a.TeacherNo,a.LoanStatusNo \r\n " +
          "FROM EmployeeBank.dbo.tblLoan as a \r\n " +
          "WHERE a.TeacherNo = '{TeacherNo}' and a.LoanStatusNo = 2 "

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
                    CB_LoanNo.Items.Clear();
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
            tabControl1.Enabled = false;
            if (e.KeyCode == Keys.Enter) 
            {
                if (TBTeacherNo.Text.Length == 6)
                {
                    DataTable dt = Class.SQLConnection.InputSQLMSSQL(SQLDefault[1].Replace("{TeacherNo}", TBTeacherNo.Text));
                    if (dt.Rows.Count != 0)
                    {
                        TBTeacherName.Text = dt.Rows[0][1].ToString();
                        CB_LoanNo.Enabled = true;
                        CB_LoanNo.Items.Clear();
                        Check = 1;
                        ComboBox[] cb = new ComboBox[] { CB_LoanNo };
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
                        CB_LoanNo.Enabled = true;
                        CBB4Oppay.SelectedIndex = 0;
                        if (CB_LoanNo.Items.Count == 1)
                        {
                            CB_LoanNo.SelectedIndex = 0;
                            label9.Visible = false;
                        }
                        else if (CB_LoanNo.Items.Count == 0)
                        {
                            label9.Visible = true;
                            CB_LoanNo.Enabled = false;
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
                    DGV_PayLoan.Rows.Clear();
                    CB_LoanNo.Items.Clear();
                    CB_LoanNo.SelectedIndex = -1;
                    TBTeacherName.Text = "";
                    label3.Text = "0";
                    if (CBB4Oppay.SelectedIndex != -1)
                        CBB4Oppay.SelectedIndex = -1;
                    CBB4Oppay.Enabled = false;
                    CB_LoanNo.Enabled = false;
                    StatusBoxFile = 0;
                    imgeLocation = "";
                    Check = 0;
                }
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            BankTeacher.Class.ComboBoxPayment Loan = (CB_LoanNo.SelectedItem as BankTeacher.Class.ComboBoxPayment);
            DataTable dt = BankTeacher.Class.SQLConnection.InputSQLMSSQL(SQLDefault[2].Replace("{LoanNo}", Loan.No));
            if (DGV_PayLoan.RowCount < 1)
            {
             
                if (dt.Rows.Count != 0)
                {
                    for (int Row = 0; Row < dt.Rows.Count; Row++)
                    {
                        DGV_PayLoan.Rows.Add(Row + 1, Loan.No, (Convert.ToDateTime(dt.Rows[0][1].ToString())).ToString("dd/MM/yyyy"), "จ่ายกู้", dt.Rows[0][3].ToString());
                    }
                    //CB_LoanNo.Items.RemoveAt()
                    CBB4Oppay.Enabled = true;
                    label3.Text = dt.Rows[0][3].ToString();
                    // ================ Clear ItemList ======================
                    CB_LoanNo.Items.RemoveAt(CB_LoanNo.SelectedIndex);
                    if (CB_LoanNo.Items.Count == 0)
                        CB_LoanNo.Enabled = false;
                }
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("ไม่สามารถ ทำรายการจ่ายมากกว่า 2 รายการขึ้นไป\r\n คุณต้องการเเทนที่รายการใหม่ หรือ ไม่", "เจ้งเตือน", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if(dialogResult == DialogResult.Yes)
                {
                    CB_LoanNo.Items.RemoveAt(CB_LoanNo.SelectedIndex); // ลบรายการใน cb
                    Class.ComboxNumberRanking.NumberRanking(0, DGV_PayLoan, CB_LoanNo, "รายการกู้"); // เช็ครายการใหม่
                    DGV_PayLoan.Rows.Add(1, Loan.No, (Convert.ToDateTime(dt.Rows[0][1].ToString())).ToString("dd/MM/yyyy"), "จ่ายกู้", dt.Rows[0][3].ToString());
                }
            }
            BT_Loanpay.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
           if (MessageBox.Show("ยืนยันการจ่ายเงิน", "ระบบ", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes && CBB4Oppay.SelectedIndex != -1/* && StatusBoxFile == 1*/)
            {
                try
                {
                    DataTable dt = Class.SQLConnection.InputSQLMSSQL(SQLDefault[5].Replace("{TeacherNo}", TBTeacherNo.Text));
                    if(dt.Rows.Count == 0)
                    {
                        BankTeacher.Class.ComboBoxPayment Payment = (CBB4Oppay.SelectedItem as BankTeacher.Class.ComboBoxPayment);
                        Class.SQLConnection.InputSQLMSSQL(SQLDefault[4].Replace("{LoanID}", DGV_PayLoan.Rows[0].Cells[1].Value.ToString())
                            .Replace("{TeacherNoPay}", Class.UserInfo.TeacherNo)
                            .Replace("{PaymentNo}", Payment.No));
                        DGV_PayLoan.Rows.RemoveAt(0);

                        MessageBox.Show("จ่ายสำเร็จ", "System", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CB_LoanNo.Items.Clear();
                        CB_LoanNo.SelectedIndex = -1;
                        TBTeacherName.Text = "";
                        label3.Text = "0";
                        CBB4Oppay.SelectedIndex = -1;
                        CBB4Oppay.Enabled = false;
                        TBTeacherNo.Text = "";
                        BT_Loanpay.Enabled = false;
                        CB_LoanNo.Enabled = false;
                        TBTeacherNo.Focus();
                        Check = 0;
                    }
                    else
                    {
                        MessageBox.Show("มีรายการ กู้ อยู่ในระบบ\r\nโปรดชำระรายการกู้ให้เรียบร้อย", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
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
                    CB_LoanNo.Items.Clear();
                    CB_LoanNo.SelectedIndex = -1;
                    TBTeacherName.Text = "";
                    label3.Text = "0";
                    if (CBB4Oppay.SelectedIndex != -1)
                        CBB4Oppay.SelectedIndex = -1;
                    CBB4Oppay.Enabled = false;
                    CB_LoanNo.Enabled = false;
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
            Class.ComboxNumberRanking.NumberRanking(SelectIndexRow, DGV_PayLoan,CB_LoanNo,"รายการกู้");
            if(CB_LoanNo.Items.Count != 0)
            {
                CB_LoanNo.Enabled = true;
            }
            if (DGV_PayLoan.Rows.Count == 0)
            {
                BT_Loanpay.Enabled = false;
                label3.Text = "0";
            }
        }
    }
}
