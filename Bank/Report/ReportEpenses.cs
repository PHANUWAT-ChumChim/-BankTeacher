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
    public partial class ReportEpenses : Form
    {
        /// <summary> 
        /// SQLDefault 
        /// <para>[0] Report Epenses Info(Loan and ShareWithdraw) INPUT: {TeacherNo} , {Date} </para> 
        /// <para>[1] Select TeacherAdd INPUT: {Text} {Date}</para>
        /// </summary> 
        private String[] SQLDefault = new String[]
         { 
           //[0] Report Epenses Info (Loan and ShareWithdraw) INPUT: {TeacherNo} , {Date} 
           "SELECT CAST(ISNULL(f.PrefixName, '') +e.Fname + ' ' + e.LName as nvarchar)as TeacherAddName , CAST(ISNULL(c.PrefixName, '') +b.Fname + ' ' + b.LName as nvarchar) as TeacherName , a.LoanNo , CAST(Name as nvarchar(255)) as BillDetailPaymentName , a.LoanAmount  \r\n " +
          "FROM EmployeeBank.dbo.tblLoan as a \r\n " +
          "LEFT JOIN Personal.dbo.tblTeacherHis as b on a.TeacherNo = b.TeacherNo \r\n " +
          "LEFT JOIN BaseData.dbo.tblPrefix as c on b.PrefixNo = c.PrefixNo \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblBillDetailPayment as d on a.BillDetailPaymentNo = d.BillDetailPaymentNo \r\n " +
          "LEFT JOIN Personal.dbo.tblTeacherHis as e on a.TeacherNoAddBy = e.TeacherNo \r\n " +
          "LEFT JOIN BaseData.dbo.tblPrefix as f on e.PrefixNo = f.PrefixNo \r\n " +
          "WHERE ( LoanStatusNo = 2 or LoanStatusNo = 3 ) and CAST(PayDate as date) LIKE '{Date}%' and e.TeacherNo = '{TeacherNo}' and e.IsUse = 1; \r\n " +
          " \r\n " +
          "SELECT CAST(ISNULL(g.PrefixName, '') +f.Fname + ' ' + f.LName as nvarchar)as TeacherAddName , CAST(ISNULL(d.PrefixName, '') +c.Fname + ' ' +c.LName as nvarchar) as TeacherName , CAST(Name as nvarchar(255)) as BillDetailPaymentName , a.Amount \r\n " +
          "FROM EmployeeBank.dbo.tblShareWithdraw as a \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblShare as b on a.ShareNo = b.ShareNo \r\n " +
          "LEFT JOIN Personal.dbo.tblTeacherHis as c on b.TeacherNo = c.TeacherNo \r\n " +
          "LEFT JOIN BaseData.dbo.tblPrefix as d on c.PrefixNo = d.PrefixNo \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblBillDetailPayment as e on a.BillDetailPayMentNo = e.BillDetailPaymentNo \r\n " +
          "LEFT JOIN Personal.dbo.tblTeacherHis as f on a.TeacherNoAddBy = f.TeacherNo \r\n " +
          "LEFT JOIN BaseData.dbo.tblPrefix as g on f.PrefixNo = g.PrefixNo \r\n " +
          "WHERE CAST(DateAdd  as date) LIKE '{Date}%' and f.TeacherNo = '{TeacherNo}' and f.IsUse = 1"
           ,
           //[1] Select TeacherAdd INPUT: {Text} {Date}
           "SELECT a.TeacherNo, CAST(ISNULL(b.PrefixName , '') + ' ' + a.Fname + ' ' + a.LName as nvarchar) \r\n " +
          " FROM Personal.dbo.tblTeacherHis as a  \r\n " +
          " LEFT JOIN BaseData.dbo.tblPrefix as b on a.PrefixNo = b.PrefixNo  \r\n " +
          " WHERE (a.TeacherNo IN (SELECT TeacherNoAddBy FROM EmployeeBank.dbo.tblLoan WHERE CAST(PayDate as date) LIKE  '%{Date}%') or a.TeacherNo IN (SELECT TeacherNoAddBy FROM EmployeeBank.dbo.tblShareWithdraw WHERE CAST(DateAdd as Date) LIKE '%{Date}%')) and (a.TeacherNo LIKE '%{Text}%'  or  CAST(ISNULL(b.PrefixName , '') + ' ' + a.Fname + ' ' + a.LName as nvarchar) LIKE '%{Text}%') \r\n " +
          " GROUP BY a.TeacherNo , CAST(ISNULL(b.PrefixName , '') + ' ' + a.Fname + ' ' + a.LName as nvarchar)"

           ,


         };
        bool CheckMember = false;
        public ReportEpenses()
        {
            InitializeComponent();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DGV.Rows.Clear();
            CheckMember = false;
            TBAmountLoan.Text = "0";
            TBAmountWithDraw.Text = "0";
            TBAmount.Text = "0";
            if(TBTeacherNo.Text != "")
                TBTeacherNo_KeyDown(new object(), new KeyEventArgs(Keys.Enter));
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
            Bank.Search IN = new Bank.Search(SQLDefault[1]
                .Replace("{Date}", (Convert.ToDateTime(Year + '-' + Month + '-' + Day)).ToString("yyyy-MM-dd")),"");
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
            if(e.KeyCode == Keys.Enter)
            {
                TBTeacherNo.Text = TBTeacherNo.Text.Replace("t", "T");
                DGV.Rows.Clear();
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
                DataSet EpensesInfo = Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[0]
                    .Replace("{TeacherNo}",TBTeacherNo.Text)
                    .Replace("{Date}",(Convert.ToDateTime(Year+'-'+Month+'-'+Day)).ToString("yyyy-MM-dd")));
                if(EpensesInfo.Tables[0].Rows.Count != 0 || EpensesInfo.Tables[1].Rows.Count != 0)
                {
                    try
                    {
                        TBTeacherName.Text = EpensesInfo.Tables[0].Rows[0][0].ToString();
                    }
                    catch
                    {
                        TBTeacherName.Text = EpensesInfo.Tables[1].Rows[0][0].ToString();
                    }
                    int SumAmount = 0;
                    if (EpensesInfo.Tables[0].Rows.Count != 0)
                    {
                        int AmountLoan = 0;
                        for(int x = 0; x < EpensesInfo.Tables[0].Rows.Count; x++)
                        {
                            DGV.Rows.Add(x+1,EpensesInfo.Tables[0].Rows[x][1].ToString(),"รายการกู้"+ EpensesInfo.Tables[0].Rows[x][2].ToString() , EpensesInfo.Tables[0].Rows[x][3], EpensesInfo.Tables[0].Rows[x][4]) ;
                            AmountLoan += Convert.ToInt32(EpensesInfo.Tables[0].Rows[x][4]);
                            SumAmount += Convert.ToInt32(EpensesInfo.Tables[0].Rows[x][4]);
                        }
                        DGV.Rows.Add("","", "สรุปรายการกู้  ", "", AmountLoan.ToString());
                        DGV.Rows[DGV.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Cornsilk;
                        TBAmountLoan.Text = AmountLoan.ToString();
                    }
                    if(EpensesInfo.Tables[1].Rows.Count != 0)
                    {
                        int AmountSaving = 0;
                        for (int x = 0; x < EpensesInfo.Tables[1].Rows.Count; x++)
                        {
                            DGV.Rows.Add(x+1,EpensesInfo.Tables[1].Rows[x][1].ToString(), "รายการถอนหุ้นสะสม", EpensesInfo.Tables[1].Rows[x][2], EpensesInfo.Tables[1].Rows[x][3]);
                            AmountSaving += Convert.ToInt32(EpensesInfo.Tables[1].Rows[x][3]);
                            SumAmount += Convert.ToInt32(EpensesInfo.Tables[1].Rows[x][3]);
                        }
                        DGV.Rows.Add("","", "สรุปรายการถอนหุ้นสะสม  ", "", AmountSaving.ToString());
                        DGV.Rows[DGV.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Cornsilk;
                        TBAmountWithDraw.Text = AmountSaving.ToString();
                    }
                    TBAmount.Text = SumAmount.ToString();
                    DGV.Rows.Add("","", "รวมรายการทั้งหมด  ", "", SumAmount.ToString());
                    DGV.Rows[DGV.Rows.Count - 1].DefaultCellStyle.BackColor = Color.CornflowerBlue;
                    BTPrint.Enabled = true;
                    Checkmember(false);
                }
            }
            else if(e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
            {
                if (CheckMember)
                {
                    ClearForm();
                    CheckMember = false;
                    Checkmember(true);
                }
            }
        }
        private void ClearForm()
        {
            TBAmount.Text = "0";
            TBAmountLoan.Text = "0";
            TBAmountWithDraw.Text = "0";
            TBTeacherName.Text = "";
            DGV.Rows.Clear();
        }

        private void BExitForm_Click(object sender, EventArgs e)
        {
            BankTeacher.Class.FromSettingMedtod.ReturntoHome(this);
        }

        private void ReportEpenses_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if(TBTeacherNo.Text.Length != 0)
                {
                    TBTeacherNo.Text = "";
                    ClearForm();
                    Checkmember(true);
                }
                else
                {
                    BExitForm_Click(new object(), new EventArgs());
                }
            }
        }

        private void ReportEpenses_SizeChanged(object sender, EventArgs e)
        {
            int x = this.Width / 2 - panel1.Size.Width / 2;
            int y = this.Height / 2 - panel1.Size.Height / 2;
            panel1.Location = new Point(x, y);
        }                           
        private void BTPrint_Click(object sender, EventArgs e)
        {
            if(DGV.Rows.Count != 0)
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
            Class.Print.PrintPreviewDialog.Detailspayment(e, DGV, "รายการ : รายจ่ายระจำวัน", this.AccessibilityObject.Name);
        }
        private void Checkmember(bool tf)
        {
            TBTeacherNo.Enabled = tf;
            BSearchTeacher.Enabled = tf;
        }
    }
}
