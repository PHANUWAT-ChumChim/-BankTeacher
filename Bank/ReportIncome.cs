using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankTeacher.Bank
{
    public partial class ReportIncome : Form
    {
        bool CheckMember = false;
        /// <summary> 
        /// SQLDefault 
        /// <para>[0] Select all bill in Day INPUT: {Date} {TeacherNoAddBy} </para> 
        /// <para>[1] Select Bill Detail INPUT: {BillNo} </para>
        /// <para>[2] SELECT TeacherAddBill INPUT: {Text} {Date}</para>
        /// </summary> 
        private String[] SQLDefault = new String[]
         { 
           //[0] Select all bill in Day INPUT: {Date} {TeacherNoAddBy}
           "SELECT a.BillNo ,  CAST(ISNULL(d.PrefixName ,'') + c.Fname + ' ' +c.Lname  as nvarchar ) as TeacherName ,  CAST(ISNULL(f.PrefixName ,'') + e.Fname + ' ' +e.Lname  as nvarchar ) as TeacherMakebillName   \r\n " +
          "FROM EmployeeBank.dbo.tblBill as a \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblMember as b on a.TeacherNo = b.TeacherNo \r\n " +
          "LEFT JOIN Personal.dbo.tblTeacherHis as c on b.TeacherNo = c.TeacherNo \r\n " +
          "LEFT JOIN BaseData.dbo.tblPrefix as d on c.PrefixNo = d.PrefixNo \r\n " +
          "LEFT JOIN Personal.dbo.tblTeacherHis as e on a.TeacherNoAddBy = e.TeacherNo \r\n " +
          "LEFT JOIN BaseData.dbo.tblPrefix as f on e.PrefixNo = f.PrefixNo \r\n " +
          "WHERE Cast(a.DateAdd as date) Like '%{Date}%' and TeacherAddBy Like '%{TeacherNoAddBy}%' and Cancel = 1 \r\n " +
          "GROUP BY a.BillNo ,  CAST(ISNULL(d.PrefixName ,'') + c.Fname + ' ' +c.Lname  as nvarchar ) ,  CAST(ISNULL(f.PrefixName ,'') + e.Fname + ' ' +e.Lname  as nvarchar ) "
           ,
           //[1] Select Bill Detail INPUT: {BillNo} 
           "SELECT BillNo , CAST(TypeName + ' '+ISNULL(CAST(LoanNo as nvarchar),'') as nvarchar) as list , CAST(c.Name as nvarchar(255)) as PaymentName , Amount \r\n " +
          "FROM EmployeeBank.dbo.tblBillDetail as a \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblBillDetailType as b on a.TypeNo = b.TypeNo \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblBillDetailPayment as c on a.BillDetailPaymentNo = c.BillDetailPaymentNo \r\n " +
          "WHERE BillNo = '{BillNo}'"
           ,
           //[2] SELECT TeacherAddBill INPUT: {Text} {Date}
           "SELECT TOP(20) TeacherNoAddBy,CAST(ISNULL(c.PrefixName,'') +' '+ b.FName + ' ' + b.LName as nvarchar) \r\n " +
          "FROM EmployeeBank.dbo.tblBill as a \r\n " +
          "LEFT JOIN Personal.dbo.tblTeacherHis as b on a.TeacherNoAddBy = b.TeacherNo \r\n " +
          "LEFT JOIN BaseData.dbo.tblPrefix as c on b.PrefixNo = c.PrefixNo \r\n " +
          "WHERE (Cancel = 1 and b.IsUse = 1 and Cast(a.DateAdd as date) Like '%{Date}%') and (a.TeacherNoAddBy LIKE '%{Text}%' or CAST(ISNULL(c.PrefixName,'') + b.FName + ' ' + b.LName as nvarchar)  LIKE '%{Text}%')   \r\n " +
          "GROUP BY TeacherNoAddBy,CAST(ISNULL(c.PrefixName,'') +' '+ b.FName + ' ' + b.LName as nvarchar) "
           ,
           //[3] SELECT TeacherAddBill (Enter) INPUT: {TeacherNo} {Date}
           "SELECT TOP(20) TeacherNoAddBy,CAST(ISNULL(c.PrefixName,'') +' '+ b.FName + ' ' + b.LName as nvarchar) \r\n " +
          "FROM EmployeeBank.dbo.tblBill as a \r\n " +
          "LEFT JOIN Personal.dbo.tblTeacherHis as b on a.TeacherNoAddBy = b.TeacherNo \r\n " +
          "LEFT JOIN BaseData.dbo.tblPrefix as c on b.PrefixNo = c.PrefixNo \r\n " +
          "WHERE (Cancel = 1 and b.IsUse = 1 and Cast(a.DateAdd as date) Like '%{Date}%') and (a.TeacherNoAddBy = '{TeacherNo}')   \r\n " +
          "GROUP BY TeacherNoAddBy,CAST(ISNULL(c.PrefixName,'') +' '+ b.FName + ' ' + b.LName as nvarchar) "
           ,

         };
        public ReportIncome()
        {
            InitializeComponent();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DGV_one.Rows.Clear();
            CheckMember = false;
            TBAmount.Text = "0";
            TBPaymentCash.Text = "0";
            TBPaymentCradit.Text = "0";
            TBPaymentTranfer.Text = "0";
            TBTeacherName.Text = "";
            TBTeacherNo.Text = "";

        }
        private void BSearchTeacher_Click(object sender, EventArgs e)
        {
            String Year = DTP.Value.ToString("yyyy");
            String Month = DTP.Value.ToString("MM");
            String Day = DTP.Value.ToString("dd");
            if (Convert.ToInt32(Month) < 10)
            {
                Month = "0" + Convert.ToInt32(Month);
            }
            if (Convert.ToInt32(Day) < 10)
            {
                Day = "0" + Convert.ToInt32(Day);
            }
            Bank.Search IN = new Bank.Search(SQLDefault[2]
                .Replace("{Date}", Year + '-' + Month + '-' + Day),"");
            IN.ShowDialog();
            if (Bank.Search.Return[0] != "")
            {
                TBTeacherNo.Text = Bank.Search.Return[0];
                CheckMember = false;
                TBTeacherNo_KeyDown(new object(), new KeyEventArgs(Keys.Enter));
            }
        }

        private void TBTeacherNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (CheckMember == false)
                {
                    TBTeacherNo.Text = TBTeacherNo.Text.Replace("t", "T");
                    String Year = DTP.Value.ToString("yyyy");
                    String Month = DTP.Value.ToString("MM");
                    String Day = DTP.Value.ToString("dd");
                    if (Convert.ToInt32(Month) < 10)
                    {
                        Month = "0" + Convert.ToInt32(Month);
                    }
                    if (Convert.ToInt32(Day) < 10)
                    {
                        Day = "0" + Convert.ToInt32(Day);
                    }
                    DataTable dtTeacherAddbill = Class.SQLConnection.InputSQLMSSQL(SQLDefault[3]
                        .Replace("{TeacherNo}", TBTeacherNo.Text)
                        .Replace("{Date}", Year + '-' + Month + '-' + Day));
                    if (dtTeacherAddbill.Rows.Count != 0)
                    {
                        TBTeacherName.Text = dtTeacherAddbill.Rows[0][1].ToString();
                        DataTable dtCheckBillInDay = Class.SQLConnection.InputSQLMSSQL(SQLDefault[0]
                        .Replace("{Date}", Year + '-' + Month + '-' + Day)
                        .Replace("{TeacherNoAddBy}", TBTeacherNo.Text));
                        if (dtCheckBillInDay.Rows.Count != 0)
                        {
                            DGV_one.Rows.Clear();
                            int DGVPosition = -1;
                            int SumAmount = 0;
                            int Amountcash = 0;
                            int AmountTranfer = 0;
                            int AmountCradit = 0;
                            DGV_one.Rows.Clear();
                            for (int x = 0; x < dtCheckBillInDay.Rows.Count; x++)
                            {
                                int AmountBill = 0;
                                DGV_one.Rows.Add(x+1,dtCheckBillInDay.Rows[x][0].ToString(), dtCheckBillInDay.Rows[x][1].ToString());
                                DGVPosition = DGV_one.Rows.Count - 1;
                                DataTable dtCheckBillDetail = Class.SQLConnection.InputSQLMSSQL(SQLDefault[1]
                                    .Replace("{BillNo}", dtCheckBillInDay.Rows[x][0].ToString()));
                                if (dtCheckBillDetail.Rows.Count != 0)
                                {
                                    for (int y = 0; y < dtCheckBillDetail.Rows.Count; y++)
                                    {
                                        AmountBill += Convert.ToInt32(dtCheckBillDetail.Rows[y][3]);
                                        SumAmount += Convert.ToInt32(dtCheckBillDetail.Rows[y][3]);
                                        if (dtCheckBillDetail.Rows[y][2].ToString().Contains("เงินสด"))
                                            Amountcash += Convert.ToInt32(dtCheckBillDetail.Rows[y][3]);
                                        else if (dtCheckBillDetail.Rows[y][2].ToString().Contains("โอน"))
                                            AmountTranfer += Convert.ToInt32(dtCheckBillDetail.Rows[y][3]);
                                        else if (dtCheckBillDetail.Rows[y][2].ToString().Contains("เครดิต"))
                                            AmountCradit += Convert.ToInt32(dtCheckBillDetail.Rows[y][3]);
                                        if (y == 0)
                                        {
                                            DGV_one.Rows[DGVPosition].Cells[3].Value = dtCheckBillDetail.Rows[y][1].ToString();
                                            DGV_one.Rows[DGVPosition].Cells[4].Value = dtCheckBillDetail.Rows[y][2].ToString();
                                            DGV_one.Rows[DGVPosition].Cells[5].Value = dtCheckBillDetail.Rows[y][3].ToString();

                                            if (y == dtCheckBillDetail.Rows.Count - 1)
                                            {
                                                DGV_one.Rows.Add("","", "", "สรุปยอดบิลล์", "", AmountBill, "");
                                                DGV_one.Rows[DGV_one.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Cornsilk;
                                            }
                                            continue;
                                        }
                                        DGV_one.Rows.Add("","", "", dtCheckBillDetail.Rows[y][1].ToString(), dtCheckBillDetail.Rows[y][2].ToString(), dtCheckBillDetail.Rows[y][3].ToString(),"");
                                        if (y == dtCheckBillDetail.Rows.Count - 1)
                                        {
                                            DGV_one.Rows.Add("","", "", "สรุปยอดบิลล์", "", AmountBill, "");
                                            DGV_one.Rows[DGV_one.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Cornsilk;
                                        }

                                        continue;
                                    }
                                    //DGV_one.Rows.Add("", "", dtCheckBillDetail.Rows[y][1].ToString(), dtCheckBillDetail.Rows[y][2].ToString(), dtCheckBillDetail.Rows[y][3].ToString());
                                    //if (y == dtCheckBillDetail.Rows.Count - 1)
                                    //{
                                    //    DGV_one.Rows.Add("", "", "สรุปยอดบิลล์", "", AmountBill);
                                    //    DGV_one.Rows[DGV_one.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Cornsilk;
                                    //}
                                }
                            }
                            TBAmount.Text = SumAmount.ToString();
                            TBPaymentCash.Text = Amountcash.ToString();
                            TBPaymentTranfer.Text = AmountTranfer.ToString();
                            TBPaymentCradit.Text = AmountCradit.ToString();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("ไม่พบรายชื่อนี้ในประวัติการจัดทำรายการให้", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                CheckMember = true;
                Checkmember(false);
            }
            else if(e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
            {
                if (CheckMember)
                {
                    Cleartabpage1();
                    CheckMember = false;
                    Checkmember(true);
                }
            }
        }
        private void Cleartabpage1()
        {
            TBAmount.Text = "0";
            TBPaymentCash.Text = "0";
            TBPaymentCradit.Text = "0";
            TBPaymentTranfer.Text = "0";
            DGV_one.Rows.Clear();
            TBTeacherName.Text = "";
        }

        private void BExitForm_Click(object sender, EventArgs e)
        {
            BankTeacher.Class.FromSettingMedtod.ReturntoHome(this);
        }

        private void ReportIncome_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if(TBTeacherNo.Text.Length != 0)
                {
                    TBTeacherNo.Text = "";
                    Cleartabpage1();
                    Checkmember(true);
                }
                else
                {
                    BExitForm_Click(new object(), new EventArgs());
                }
            }
        }

        private void ReportIncome_SizeChanged(object sender, EventArgs e)
        {
            int x = this.Width / 2 - panel1.Size.Width / 2;
            int y = this.Height / 2 - panel1.Size.Height / 2;
            panel1.Location = new Point(x, y);
        }
        private void TBTeacherNo_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
        private void Checkmember(bool tf)
        {
            TBTeacherNo.Enabled = tf;
            BSearchTeacher.Enabled = tf;
        }
        private void BTPrint_Click(object sender, EventArgs e)
        {
            if(DGV_one.Rows.Count != 0)
            {
                if(printPreviewDialog1.ShowDialog() == DialogResult.OK)
                {
                    printDocument1.Print();
                }
            }
            else
            {
                MessageBox.Show("ไม่พบรายการบิลล์ ในตาราง", "การเเจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Class.Print.PrintPreviewDialog.PrintReportGrid(e, DGV_one, "รายการบิลล์", AccessibilityObject.Name, 2, "A4", 1);
        }
    }
}
