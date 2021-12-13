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
    public partial class ReportEpensesAll : Form
    {
        bool CheckMember = false;
        /// <summary> 
        /// SQLDefault 
        /// <para>[0] Report Epenses Info (Loan and ShareWithdraw) INPUT: {TeacherNo} , {Date} </para> 
        /// </summary> 
        private String[] SQLDefault = new String[]
         { 
           //[0] Report Epenses Info (Loan and ShareWithdraw) INPUT: {TeacherNo} , {Date} 
           "SELECT CAST(ISNULL(f.PrefixName, '') +e.Fname + ' ' + e.LName as nvarchar)as TeacherAddName , CAST(ISNULL(c.PrefixName, '') +b.Fname + ' ' + b.LName as nvarchar) as TeacherName , a.LoanNo , Name as BillDetailPaymentName , a.LoanAmount  \r\n " +
          "FROM EmployeeBank.dbo.tblLoan as a \r\n " +
          "LEFT JOIN Personal.dbo.tblTeacherHis as b on a.TeacherNo = b.TeacherNo \r\n " +
          "LEFT JOIN BaseData.dbo.tblPrefix as c on b.PrefixNo = c.PrefixNo \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblBillDetailPayment as d on a.BillDetailPaymentNo = d.BillDetailPaymentNo \r\n " +
          "LEFT JOIN Personal.dbo.tblTeacherHis as e on a.TeacherNoAddBy = e.TeacherNo \r\n " +
          "LEFT JOIN BaseData.dbo.tblPrefix as f on e.PrefixNo = f.PrefixNo \r\n " +
          "WHERE ( LoanStatusNo = 2 or LoanStatusNo = 3 ) and CAST(PayDate as date) LIKE '{Date}%' and e.TeacherNo LIKE '%{TeacherNo}%' and e.IsUse = 1 \r\n " +
          " ORDER BY CAST(ISNULL(f.PrefixName, '') +e.Fname + ' ' + e.LName as nvarchar) \r\n " +
          "\r\n" + 
          "SELECT CAST(ISNULL(g.PrefixName, '') +f.Fname + ' ' + f.LName as nvarchar)as TeacherAddName , CAST(ISNULL(d.PrefixName, '') +c.Fname + ' ' +c.LName as nvarchar) as TeacherName , Name as BillDetailPaymentName , a.Amount \r\n " +
          "FROM EmployeeBank.dbo.tblShareWithdraw as a \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblShare as b on a.ShareNo = b.ShareNo \r\n " +
          "LEFT JOIN Personal.dbo.tblTeacherHis as c on b.TeacherNo = c.TeacherNo \r\n " +
          "LEFT JOIN BaseData.dbo.tblPrefix as d on c.PrefixNo = d.PrefixNo \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblBillDetailPayment as e on a.BillDetailPayMentNo = e.BillDetailPaymentNo \r\n " +
          "LEFT JOIN Personal.dbo.tblTeacherHis as f on a.TeacherNoAddBy = f.TeacherNo \r\n " +
          "LEFT JOIN BaseData.dbo.tblPrefix as g on f.PrefixNo = g.PrefixNo \r\n " +
          "WHERE CAST(DateAdd  as date) LIKE '{Date}%' and f.TeacherNo LIKE '%{TeacherNo}%' and f.IsUse = 1 \r\n"+
          "ORDER BY CAST(ISNULL(g.PrefixName, '') +f.Fname + ' ' + f.LName as nvarchar)"
             ,

         };
        public ReportEpensesAll()
        {
            InitializeComponent();
            dateTimePicker1_ValueChanged(new object(), new EventArgs());
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DGV.Rows.Clear();
            CheckMember = false;
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
                .Replace("{TeacherNo}", "")
                .Replace("{Date}", (Convert.ToDateTime(Year + '-' + Month + '-' + Day)).ToString("yyyy-MM-dd")));
            if (EpensesInfo.Tables[0].Rows.Count != 0 || EpensesInfo.Tables[1].Rows.Count != 0)
            {
                int SumAmount = 0;
                String name = "";
                if (EpensesInfo.Tables[0].Rows.Count != 0)
                {
                    int AmountLoan = 0;
                    for (int x = 0; x < EpensesInfo.Tables[0].Rows.Count; x++)
                    {
                        DGV.Rows.Add(EpensesInfo.Tables[0].Rows[x][0].ToString(),EpensesInfo.Tables[0].Rows[x][1].ToString(), "รายการกู้ " + EpensesInfo.Tables[0].Rows[x][2].ToString(), EpensesInfo.Tables[0].Rows[x][3], EpensesInfo.Tables[0].Rows[x][4]);
                            AmountLoan += Convert.ToInt32(EpensesInfo.Tables[0].Rows[x][4]);
                        SumAmount += Convert.ToInt32(EpensesInfo.Tables[0].Rows[x][4]);
                    }
                    DGV.Rows.Add("", "", "สรุปรายการกู้", "", AmountLoan.ToString());
                    DGV.Rows[DGV.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Cornsilk;
                    TBAmountLoan.Text = AmountLoan.ToString();
                }
                if (EpensesInfo.Tables[1].Rows.Count != 0)
                {
                    int AmountSaving = 0;
                    for (int x = 0; x < EpensesInfo.Tables[1].Rows.Count; x++)
                    {
                        DGV.Rows.Add(EpensesInfo.Tables[1].Rows[x][0].ToString(),EpensesInfo.Tables[1].Rows[x][1].ToString(), "รายการถอนหุ้นสะสม", EpensesInfo.Tables[1].Rows[x][2], EpensesInfo.Tables[1].Rows[x][3]);
                        AmountSaving += Convert.ToInt32(EpensesInfo.Tables[1].Rows[x][3]);
                        SumAmount += Convert.ToInt32(EpensesInfo.Tables[1].Rows[x][3]);
                    }
                    DGV.Rows.Add("", "", "สรุปรายการถอนหุ้นสะสม", "", AmountSaving.ToString());
                    DGV.Rows[DGV.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Cornsilk;
                    TBAmountWithDraw.Text = AmountSaving.ToString();
                }
                if(DGV.Rows.Count != 0)
                {
                    name = DGV.Rows[0].Cells[0].Value.ToString();
                    for(int x = 1; x < DGV.Rows.Count; x++)
                    {
                        if(DGV.Rows[x].Cells[0].Value.ToString() == name)
                        {
                            DGV.Rows[x].Cells[0].Value = "";
                        }
                        else
                        {
                            name = DGV.Rows[x].Cells[0].ToString();
                        }
                    }
                }
                TBAmount.Text = SumAmount.ToString();
                DGV.Rows.Add("", "", "สรุปรายการทั้งหมด", "",SumAmount.ToString());
                DGV.Rows[DGV.Rows.Count - 1].DefaultCellStyle.BackColor = Color.CornflowerBlue;
            }
        }

        private void ReportEpensesAll_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                BExitForm_Click(new object(), new EventArgs());
            }
        }

        private void BExitForm_Click(object sender, EventArgs e)
        {
            BankTeacher.Class.FromSettingMedtod.ReturntoHome(this);
        }

        private void ReportEpensesAll_SizeChanged(object sender, EventArgs e)
        {
            int x = this.Width / 2 - panel1.Size.Width / 2;
            int y = this.Height / 2 - panel1.Size.Height / 2;
            panel1.Location = new Point(x, y);
        }
    }
}
