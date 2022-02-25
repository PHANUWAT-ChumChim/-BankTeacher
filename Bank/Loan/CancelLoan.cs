using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankTeacher.Bank.Loan
{
    public partial class CancelLoan : Form
    {
        int Check = 0;
        bool CheckSave = false;

        /// <summary>
        /// <para>[0] SELECT MemberLona  INPUT: {TeacherNo}</para>
        /// <para>[1] SELECT LOAN INPUT: {TeacherNo} </para>
        /// <para>[2] SELECT Detail Loan INPUT: {LoanNo}</para>
        /// <para>[3] UPDATE Cancel Loan INPUT: {WhereLoanNo} {TeacherNoUser} </para>
        /// <para>[4] UPDATE Cancel Guarantor INPUT: {WhereLoanNo}</para>
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
           "SELECT CAST(ISNULL(d.PrefixName , '') + ' ' + Fname + ' ' + Lname AS NVARCHAR) , CAST(a.YearPay as nvarchar)+ '/' + CAST(a.MonthPay as nvarchar) ,a.PayDate  ,a. LoanNo   \r\n " +
          "  FROM EmployeeBank.dbo.tblLoan as a   \r\n " +
          "  LEFT JOIN EmployeeBank.dbo.tblGuarantor as b on a.LoanNo = b.LoanNo   \r\n " +
          "  LEFT JOIN Personal.dbo.tblTeacherHis as c on a.TeacherNo = c.TeacherNo  \r\n " +
          "  LEFT JOIN BaseData.dbo.tblPrefix as d on c.PrefixNo = d.PrefixNo  \r\n " +
          " LEFT JOIN EmployeeBank.dbo.tblMember as e on c.TeacherNo = e.TeacherNo  \r\n " +
          " WHERE a.TeacherNo = '{TeacherNo}' and LoanStatusNo = 1 and MemberStatusNo = 1  \r\n " +
          "  GROUP BY CAST(ISNULL(d.PrefixName , '') + ' ' + Fname + ' ' + Lname AS NVARCHAR) , a.DateAdd ,a.PayDate  ,a. LoanNo , CAST(a.YearPay as nvarchar)+ '/' + CAST(a.MonthPay as nvarchar) \r\n " +
          "  ORDER BY a.LoanNo  "

          ,
         //[2] SELECT Detail Loan INPUT: {LoanNo} 
          "SELECT CAST(ISNULL(d.PrefixName+' ','') + Fname + ' ' + Lname AS NVARCHAR) ,CAST(DateAdd as date),a.PayDate ,b. Amount\r\n " +
          "FROM EmployeeBank.dbo.tblLoan as a \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblGuarantor as b on a.LoanNo = b.LoanNo \r\n " +
          "LEFT JOIN Personal.dbo.tblTeacherHis as c on b.TeacherNo = c.TeacherNo \r\n " +
          "LEFT JOIN BaseData.dbo.tblPrefix as d on c.PrefixNo = d.PrefixNo \r\n " +
          "WHERE a.LoanNo = '{LoanNo} ' and LoanStatusNo = 1 \r\n " +
          " "
          ,
          //[3] UPDATE Cancel Loan INPUT: {WhereLoanNo}  {TeacherNoUser}
          "UPDATE EmployeeBank.dbo.tblLoan\r\n" +
          "SET LoanStatusNo = 4 , CancelByTeacherNo = '{TeacherNoUser}' , CancelDate = CAST(GETDATE() as date)\r\n" +
          "WHERE LoanNo = {WhereLoanNo}"
          
          ,
          //[4] UPDATE Cancel Guarantor INPUT: {WhereLoanNo}
          "UPDATE EmployeeBank.dbo.tblGuarantor\r\n" +
          "SET RemainsAmount = 0\r\n" +
          "WHERE LoanNo = {WhereLoanNo}"

        };

        public CancelLoan()
        {
            InitializeComponent();
        }

        private void BSearchTeacher_Click_1(object sender, EventArgs e)
        {
            TBTeacherNo_KeyDown(new object(), new KeyEventArgs(Keys.Escape));
            Bank.Search IN;
            IN = new Bank.Search(SQLDefault[0]);
            IN.ShowDialog();
            if (Bank.Search.Return[0].ToString() != "")
            {
                TBTeacherNo.Text = Bank.Search.Return[0];
                TBTeacherName.Text = Bank.Search.Return[1];
                TBTeacherNo_KeyDown(new object(), new KeyEventArgs(Keys.Enter));
            }
        }

        List<String[]> ListLoan = new List<string[]> { };
        private void TBTeacherNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                TBTeacherNo.Text = TBTeacherNo.Text.Replace("t", "T");
                DataTable dt = Class.SQLConnection.InputSQLMSSQL(SQLDefault[1].Replace("{TeacherNo}", TBTeacherNo.Text));
                if (dt.Rows.Count != 0)
                {
                    DGVCancelLoan.Rows.Clear();
                    TBTeacherName.Text = dt.Rows[0][0].ToString();
                    CBlist.Enabled = true;
                    CBlist.Items.Clear();
                    Check = 1;
                    ComboBox[] cb = new ComboBox[] { CBlist };
                    for (int x = 0; x < dt.Rows.Count; x++)
                    {
                        for (int aa = 0; aa < cb.Length; aa++)
                        {
                            cb[aa].Items.Add(new BankTeacher.Class.ComboBoxPayment(dt.Rows[x][1].ToString() + " รายการกู้ " + dt.Rows[x][3].ToString(), dt.Rows[x][3].ToString()));
                            ListLoan.Add( new string[] { dt.Rows[x][1].ToString() + " รายการกู้ " + dt.Rows[x][3].ToString(), dt.Rows[x][3].ToString() });
                        }
                            
                    }
                    if(CBlist.Items.Count != 0)
                    {
                        CBlist.SelectedIndex = 0;
                    }
                    Checkmember(false);
                    CheckSave = false;
                }
                else
                {
                    MessageBox.Show("รหัสผู้ใช้ไม่ถูกต้อง","System",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }
            }
            else if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
            {
                if (Check == 1)
                {
                    CBlist.Items.Clear();
                    CBlist.SelectedIndex = -1;
                    TBTeacherName.Text = "";
                    DGVCancelLoan.Rows.Clear();
                    CBlist.Enabled = false;
                    Checkmember(true);
                    Check = 0;
                }
            }

        }
        private void Checkmember(bool tf)
        {
            TBTeacherNo.Enabled = tf;
        }

        private void CBList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(CBlist.SelectedIndex != -1)
            {
                DGVCancelLoan.Rows.Clear();
                BankTeacher.Class.ComboBoxPayment Loan = (CBlist.SelectedItem as BankTeacher.Class.ComboBoxPayment);
                DataTable dt = BankTeacher.Class.SQLConnection.InputSQLMSSQL(SQLDefault[2].Replace("{LoanNo}", Loan.No));
                if (dt.Rows.Count != 0)
                {
                    TBTeacherName.Text = dt.Rows[0][0].ToString();
                    DGVCancelLoan.Rows.Add(Loan.No, Convert.ToDateTime(dt.Rows[0][1].ToString()).ToString("dd/MM/yyyy"), dt.Rows[0][0].ToString(), dt.Rows[0][3].ToString());
                    for (int x = 1; x < dt.Rows.Count; x++)
                    {
                        DGVCancelLoan.Rows.Add("", "", dt.Rows[x][0].ToString(), dt.Rows[x][3].ToString());
                    }
                    //CBlist.Items.RemoveAt(CBlist.SelectedIndex);
                }
            }
        }

        private void BCancelSave_Click(object sender, EventArgs e)
        {
            if (TBTeacherNo.Text != "" && TBTeacherName.Text != ""  && MessageBox.Show("ยืนยันที่จะลบการกู้นี้หรือไม่","ระบบ",MessageBoxButtons.YesNo,MessageBoxIcon.Information) == DialogResult.Yes)
            {
                try
                {
                    for (int x = 0; x < DGVCancelLoan.Rows.Count; x++)
                    {
                        if (DGVCancelLoan.Rows[x].Cells[0].Value.ToString() != "")
                        {
                            Class.SQLConnection.InputSQLMSSQL(SQLDefault[3]
                            .Replace("{WhereLoanNo}", DGVCancelLoan.Rows[x].Cells[0].Value.ToString())
                            .Replace("{TeacherNoUser}", Class.UserInfo.TeacherNo));

                            Class.SQLConnection.InputSQLMSSQL(SQLDefault[4]
                                .Replace("{WhereLoanNo}", DGVCancelLoan.Rows[x].Cells[0].Value.ToString()));
                        }
                    }

                    MessageBox.Show("บันทึกสำเร็จ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TBTeacherNo.Text = "";
                    TBTeacherName.Text = "";
                    CBlist.Items.Clear();
                    DGVCancelLoan.Rows.Clear();
                    CheckSave = true;
                    Checkmember(true);
                }
                catch(Exception ex)
                {
                    MessageBox.Show("บันทึกล้มเหลว", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Console.WriteLine($"-------------------------{ex}---------------------------");
                }
            }
        }

        private void CancelLoan_SizeChanged(object sender, EventArgs e)
        {
            Class.FromSettingMedtod.ChangeSizePanal(this, panel1);
        }

        private void BExitForm_Click(object sender, EventArgs e)
        {
            BankTeacher.Class.FromSettingMedtod.ReturntoHome(this);
        }

        private void CancelLoan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape || (CheckSave && e.KeyCode == Keys.Enter))
            {
                if (TBTeacherNo.Text.Length != 0)
                {
                    CBlist.DroppedDown = false;
                    TBTeacherNo.Text = "";
                    CBlist.Items.Clear();
                    CBlist.SelectedIndex = -1;
                    TBTeacherName.Text = "";
                    DGVCancelLoan.Rows.Clear();
                    CBlist.Enabled = false;
                    Check = 0;
                    Checkmember(true);
                    CheckSave = false;
                }
                else
                {
                    BExitForm_Click(new object(), new EventArgs());
                }
            }
        }

        int SelectIndexRowDelete = 0;
        private void Delete_Click(object sender, EventArgs e)
        {
            if (SelectIndexRowDelete > -1)
            {
                DGVCancelLoan.Rows.RemoveAt(SelectIndexRowDelete);
                SelectIndexRowDelete = -1;

            }
        }

        private void DGVCancelLoan_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                //
                int currentMouseOverRow = DGVCancelLoan.HitTest(e.X, e.Y).RowIndex;
                if (currentMouseOverRow > -1)
                {
                    SelectIndexRowDelete = currentMouseOverRow;
                    ContextMenu m = new ContextMenu();
                    m.MenuItems.Add(new MenuItem("ลบออก"));
                    m.Show(DGVCancelLoan, new Point(e.X, e.Y));
                    m.MenuItems[0].Click += new System.EventHandler(this.Delete_Click);

                    ComboBox[] cb = new ComboBox[] { CBlist };
                    for (int x = 0; x < ListLoan.Count; x++)
                    {
                        if(ListLoan[x][1] == DGVCancelLoan.Rows[currentMouseOverRow].Cells[0].Value.ToString())
                        {
                            for (int aa = 0; aa < cb.Length; aa++)
                            {
                                cb[aa].Items.Add(new BankTeacher.Class.ComboBoxPayment(ListLoan[x][0], ListLoan[x][1]));
                            }
                        }
                    }
                }
            }
        }
    }
}
