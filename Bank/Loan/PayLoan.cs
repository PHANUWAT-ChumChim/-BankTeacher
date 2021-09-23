﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static example.Class.ProtocolSharing.ConnectSMB;

namespace example.Bank.Loan
{
    public partial class PayLoan : Form
    {
        int Check = 0;
        int StatusBoxFile = 0;
        String imgeLocation = "";

        /// <summary>
        /// <para>[0] SELECT MemberLona  INPUT: {TeacherNo}</para>
        /// <para>[1] SELECT LOAN INPUT: {TeacherNo} </para>
        /// <para>[2] SELECT Detail Loan INPUT: {LoanNo}</para>
        /// <para>[3] Select Payment Name INPUT: - </para>
        /// <para>[4] UPDATE Payment INPUT: {LoanID} {TeacherNoPay} {PaymentNo}</para>
        /// </summary>
        private String[] SQLDefault =
{
          //[0] SELECT MemberLona  INPUT: {TeacherNo}
          "SELECT TOP(20) TeacherNo , NAME , StartAmount \r\n " +
          "FROM( \r\n " +
          "SELECT a.TeacherNo,CAST(c.PrefixName+''+b.Fname+''+b.Lname as NVARCHAR)AS NAME,d.StartAmount ,b.Fname \r\n " +
          "FROM EmployeeBank.dbo.tblLoan as a  \r\n " +
          "LEFT JOIN Personal.dbo.tblTeacherHis as b on a.TeacherNo = b.TeacherNo  \r\n " +
          "LEFT JOIN BaseData.dbo.tblPrefix as c on b.PrefixNo = c.PrefixNo  \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblMember as d on a.TeacherNo = d.TeacherNo \r\n " +
          "WHERE a.TeacherNo LIKE 'T{TeacherNo}%' and a.LoanStatusNo != 4 \r\n " +
          "GROUP BY a.TeacherNo,CAST(c.PrefixName+''+b.Fname+''+b.Lname as NVARCHAR),d.StartAmount ,Fname) AS A \r\n " +
          "ORDER BY Fname; \r\n " +
          " "
          ,
          //[1] SELECT LOAN INPUT: {TeacherNo} : 
          "SELECT a.LoanNo , CAST(d.PrefixName + ' ' + Fname + ' ' + Lname AS NVARCHAR) \r\n " +
          " FROM EmployeeBank.dbo.tblLoan as a  \r\n " +
          " LEFT JOIN EmployeeBank.dbo.tblGuarantor as b on a.LoanNo = b.LoanNo  \r\n " +
          " LEFT JOIN Personal.dbo.tblTeacherHis as c on a.TeacherNo = c.TeacherNo \r\n " +
          " LEFT JOIN BaseData.dbo.tblPrefix as d on c.PrefixNo = d.PrefixNo \r\n " +
          " WHERE a.TeacherNo = '{TeacherNo}' and LoanStatusNo != 4  \r\n " +
          " GROUP BY a.LoanNo , CAST(d.PrefixName + ' ' + Fname + ' ' + Lname AS NVARCHAR) \r\n " +
          " ORDER BY a.LoanNo  "
                ,
          //[2] SELECT Detail Loan INPUT: {LoanNo} 
          "SELECT CAST(d.PrefixName + ' ' + Fname + ' ' + Lname AS NVARCHAR) ,DateAdd,a.PayDate,LoanAmount \r\n " +
          "FROM EmployeeBank.dbo.tblLoan as a \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblGuarantor as b on a.LoanNo = b.LoanNo \r\n " +
          "LEFT JOIN Personal.dbo.tblTeacherHis as c on b.TeacherNo = c.TeacherNo \r\n " +
          "LEFT JOIN BaseData.dbo.tblPrefix as d on c.PrefixNo = d.PrefixNo \r\n " +
          "WHERE a.LoanNo = '{LoanNo} ' and LoanStatusNo != 4 and LoanStatusNo != 3\r\n "
          ,
          //[3] Check BillDetailPayment INPUT: -  
          "SELECT Name , BillDetailpaymentNo  \r\n " +
          "FROM EmployeeBank.dbo.tblBillDetailPayment \r\n " +
          "WHERE Status = 1 "
          ,
          //[4] UPDATE Payment INPUT: {LoanID} {TeacherNoPay} {PaymentNo}
          "UPDATE EmployeeBank.dbo.tblLoan \r\n " +
          "SET PayDate = CURRENT_TIMESTAMP , TeacherNoPay = '{TeacherNoPay}', BillDetailPaymentNo = '{PaymentNo}' \r\n " +
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
                    cb[x].Items.Add(new example.Class.ComboBoxPayment(dtPayment.Rows[a][0].ToString(),
                        dtPayment.Rows[a][1].ToString()));
        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void BSearchTeacher_Click(object sender, EventArgs e)
        {
            Bank.Search IN;
            try
            {

                IN = new Bank.Search(SQLDefault[0]
                     .Replace("{TeacherNo}", ""));
                IN.ShowDialog();
                TBTeacherNo.Text = Bank.Search.Return[0];
                TBTeacherName.Text = Bank.Search.Return[1];
                label3.Text = "0";
                CBB4Oppay.Enabled = false;
                button1.Enabled = false;
                comboBox1.Enabled = true;
                comboBox1.Items.Clear();
                Check = 1;
                ComboBox[] cb = new ComboBox[] { comboBox1 };
                DataTable dt = Class.SQLConnection.InputSQLMSSQL(SQLDefault[1]
                    .Replace("{TeacherNo}", TBTeacherNo.Text));
                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    for (int aa = 0; aa < cb.Length; aa++)
                    {
                        cb[aa].Items.Add(new example.Class.ComboBoxPayment("รายการกู้ " + (x + 1), dt.Rows[x][0].ToString()));
                    }
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
                        for (int x = 0; x < dt.Rows.Count; x++)
                        {
                            for (int aa = 0; aa < cb.Length; aa++)
                            {
                                cb[aa].Items.Add(new example.Class.ComboBoxPayment("รายการกู้ " + (x + 1), dt.Rows[x][0].ToString()));
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("รหัสผู้ใช้ไม่ถูกต้อง", "System", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    CBB4Oppay.Enabled = false;
                    button1.Enabled = false;
                    comboBox1.Enabled = false;
                    Check = 0;
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            example.Class.ComboBoxPayment Loan = (comboBox1.SelectedItem as example.Class.ComboBoxPayment);
            DataTable dt = example.Class.SQLConnection.InputSQLMSSQL(SQLDefault[2].Replace("{LoanNo}", Loan.No));
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
            try
            {
                example.Class.ComboBoxPayment Payment = (CBB4Oppay.SelectedItem as example.Class.ComboBoxPayment);
                Class.SQLConnection.InputSQLMSSQL(SQLDefault[4].Replace("{LoanID}", TBLoanNo.Text
                    .Replace("{TeacherNoPay}", Class.UserInfo.TeacherNo)
                    .Replace("{PaymentNo}", Payment.No)));
                MessageBox.Show("จ่ายสำเร็จ","System",MessageBoxButtons.OK,MessageBoxIcon.Information);
                comboBox1.Items.Clear();
                comboBox1.SelectedIndex = -1;
                TBTeacherName.Text = "";
                TBLoanNo.Text = "";
                TBLoanStatus.Text = "";
                TBDate.Text = "";
                label3.Text = "0";
                CBB4Oppay.Enabled = false;
                button1.Enabled = false;
                comboBox1.Enabled = false;
                Check = 0;
            }
            catch
            {
                MessageBox.Show("จ่ายล้มเหลว", "System", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
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
                        BTOpenfile.Text = "ส่งไฟล์";
                        StatusBoxFile = 1;
                        label6.Text = "Scan(  พบไฟล์  )";
                    }

                }
                catch
                {
                    MessageBox.Show("An Error Occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (StatusBoxFile == 1)
            {
                var smb = new SmbFileContainer("Loan");
                if (smb.IsValidConnection())
                {
                    String Return = smb.SendFile(imgeLocation, "Loan_" + TBTeacherNo.Text + ".pdf");
                    MessageBox.Show(Return, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    StatusBoxFile = 0;
                    BTOpenfile.Text = "เปิดไฟล์";
                    label6.Text = "Scan(  ไม่พบ  )";
                    imgeLocation = "";
                }
                else
                {
                    MessageBox.Show("ไม่สามารถสร้างไฟล์ในที่นั้นได้", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void BTdeletefile_Click(object sender, EventArgs e)
        {
            StatusBoxFile = 0;
            BTOpenfile.Text = "เปิดไฟล์";
            label6.Text = "Scan(  ไม่พบ  )";
            imgeLocation = "";

        private void PayLoan_SizeChanged(object sender, EventArgs e)
        {
            Class.FromSettingMedtod.ChangeSizePanal(this, panel1);
        }
    }
}