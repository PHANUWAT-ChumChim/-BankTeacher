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
    public partial class ReportIncomeAll : Form
    {
        bool CheckMember = false;
        /// <summary> 
        /// SQLDefault 
        /// <para>[0] Select all bill in Day INPUT: {Date} {TeacherNoAddBy} </para> 
        /// <para>[1] Select Bill Detail INPUT: {BillNo} </para>
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


         };
        public ReportIncomeAll()
        {
            InitializeComponent();
            dateTimePicker1_ValueChanged(new object(), new EventArgs());
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            TBAmountCash_All.Text = "0";
            TBAmountCradit_All.Text = "0";
            TBAmount_All.Text = "0";
            TBAmountTranfer_All.Text = "0";
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
            DGV_All.Rows.Clear();
            //ภาพรวม
            DataTable dtCheckBillInDay = Class.SQLConnection.InputSQLMSSQL(SQLDefault[0]
                .Replace("{Date}" , Year+'-'+Month+'-'+Day)
                .Replace("{TeacherNoAddBy}",""));
            if(dtCheckBillInDay.Rows.Count != 0)
            {
                int DGVPosition = -1;
                int SumAmount = 0;
                int Amountcash = 0;
                int AmountTranfer = 0;
                int AmountCradit = 0;
                for (int x = 0; x < dtCheckBillInDay.Rows.Count; x++) 
                {
                    int AmountBill = 0;
                    DGV_All.Rows.Add(x+1,dtCheckBillInDay.Rows[x][0].ToString(), dtCheckBillInDay.Rows[x][2].ToString(), dtCheckBillInDay.Rows[x][1].ToString());
                    DGVPosition = DGV_All.Rows.Count - 1 ;

                    DataTable dtCheckBillDetail = Class.SQLConnection.InputSQLMSSQL(SQLDefault[1]
                        .Replace("{BillNo}", dtCheckBillInDay.Rows[x][0].ToString()));
                    if(dtCheckBillDetail.Rows.Count != 0)
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
                                DGV_All.Rows[DGVPosition].Cells[4].Value = dtCheckBillDetail.Rows[y][1].ToString();
                                DGV_All.Rows[DGVPosition].Cells[5].Value = dtCheckBillDetail.Rows[y][2].ToString();
                                DGV_All.Rows[DGVPosition].Cells[6].Value = dtCheckBillDetail.Rows[y][3].ToString();

                                if (y == dtCheckBillDetail.Rows.Count - 1)
                                {
                                    DGV_All.Rows.Add("","", "", "", "สรุปยอดบิลล์","", AmountBill,"");
                                    DGV_All.Rows[DGV_All.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Cornsilk;
                                }

                                continue;
                            }
                            DGV_All.Rows.Add("", "", "", "", dtCheckBillDetail.Rows[y][1].ToString(), dtCheckBillDetail.Rows[y][2].ToString(), dtCheckBillDetail.Rows[y][3].ToString(),"");
                            if(y == dtCheckBillDetail.Rows.Count - 1)
                            {
                                DGV_All.Rows.Add("", "","","","สรุปยอดบิลล์","", AmountBill,"");
                                DGV_All.Rows[DGV_All.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Cornsilk;
                            }
                        }
                    }
                }
                TBAmount_All.Text = SumAmount.ToString();
                TBAmountCash_All.Text = Amountcash.ToString();
                TBAmountTranfer_All.Text = AmountTranfer.ToString();
                TBAmountCradit_All.Text = AmountCradit.ToString();
            }
        }
        private void BExitForm_Click(object sender, EventArgs e)
        {
            BankTeacher.Class.FromSettingMedtod.ReturntoHome(this);
        }

        private void ReportIncomeAll_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                BExitForm_Click(new object(), new EventArgs());
            }
        }

        private void ReportIncomeAll_SizeChanged(object sender, EventArgs e)
        {
            Class.FromSettingMedtod.ChangeSizePanal(this, panel1);
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Class.Print.PrintPreviewDialog.PrintReportGrid(e, DGV_All, "รายการบิลล์", AccessibilityObject.Name,false,false, "A4",1);
        }

        private void BTPrint_Click(object sender, EventArgs e)
        {
            if(DGV_All.Rows.Count != 0)
            {
                if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                {
                    printDocument1.Print();
                }
            }
            else
                 MessageBox.Show("ไม่พบรายการบิลล์ ในตาราง", "การเเจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
