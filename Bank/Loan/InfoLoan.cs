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
    public partial class InfoLoan : Form
    {
        int Check = 0;
        // ======================= ข้อมูลเเบบปริ้น ====================
        public static string info_name;
        public static string info_id;
        public static string info_Loanid;
        public static string info_startdate;
        public static string info_duedate;
        // เงินตรา
        public static float info_Sum;
        public static string info_totelLoan;
        public static string info_Loanpayall;
        public static List<float> Amount = new List<float>();
        public static List<Double> Percent = new List<double>();
        // ผู้ค้ำ
        public static List<string> info_GuarantrN = new List<string>();
        public static List<string> info_GuarantrAmount = new List<string>();
        public static List<string> info_GuarantrPercent = new List<string>();
        public static List<string> info_GuarantRemains = new List<string>();
        // รอบ
        public static int how_many_laps;
        /// <summary>
        /// <para>[0] SELECT MemberLonn  INPUT: {TeacherNo}</para>
        /// <para>[1] SELECT LOAN INPUT: {TeacherNo} </para>
        /// <para>[2] SELECT Detail Loan INPUT: {LoanID} </para>
        /// </summary>
        private String[] SQLDefault =
        {
             //[0] SELECT MemberLonn  INPUT: {Text}
           "SELECT TOP(20) TeacherNo , NAME  \r\n " +
          "FROM(SELECT a.TeacherNo, CAST(ISNULL(c.PrefixName+' ','') + Fname + ' ' + Lname AS nvarchar)AS NAME,SavingAmount,Fname  \r\n " +
          "FROM (SELECT TeacherNo \r\n " +
          "FROM EmployeeBank.dbo.tblLoan \r\n " +
          "WHERE LoanStatusNo = 1 or LoanStatusNo = 2 \r\n " +
          "GROUP BY TeacherNo) as a   \r\n " +
          "LEFT JOIN Personal.dbo.tblTeacherHis as b on a.TeacherNo = b.TeacherNo   \r\n " +
          "LEFT JOIN BaseData.dbo.tblPrefix as c on b.PrefixNo = c.PrefixNo   \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblShare as d on a.TeacherNo = d.TeacherNo   \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblMember as e on b.TeacherNo = e.TeacherNo  \r\n " +
          "WHERE MemberStatusNo = 1 \r\n " +
          "GROUP BY a.TeacherNo,CAST(ISNULL(c.PrefixName+' ','')+Fname+' '+Lname as NVARCHAR),d.SavingAmount ,Fname ) AS A    \r\n " +
          "WHERE a.TeacherNo LIKE '%%' or Fname LIKE '%%'   \r\n " +
          "ORDER BY Fname;   "
           

                ,
          //[1] SELECT LOAN INPUT: {TeacherNo} : 
           "SELECT a.LoanNo , CAST(ISNULL(d.PrefixNameFull , '') + Fname + ' ' + Lname AS NVARCHAR)  \r\n " +
          "FROM EmployeeBank.dbo.tblLoan as a   \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblGuarantor as b on a.LoanNo = b.LoanNo   \r\n " +
          "LEFT JOIN Personal.dbo.tblTeacherHis as c on a.TeacherNo = c.TeacherNo  \r\n " +
          "LEFT JOIN BaseData.dbo.tblPrefix as d on c.PrefixNo = d.PrefixNo  \r\n " +
          "WHERE a.TeacherNo = '{TeacherNo}' and LoanStatusNo != 4   \r\n " +
          "GROUP BY a.LoanNo , CAST(ISNULL(d.PrefixNameFull , '') + Fname + ' ' + Lname AS NVARCHAR) \r\n " +
          "ORDER BY a.LoanNo  "
           ,

          //[2] SELECT Detail Loan INPUT: {LoanID}
           "SELECT b.TeacherNo , CAST(ISNULL(d.PrefixNameFull , '') + c.Fname + ' ' + c.Lname AS NVARCHAR) AS NameTeacher,CAST(DateAdd as date) as SignUpdate,  \r\n " +
          " a.PayDate,MonthPay,YearPay,PayNo,InterestRate,LoanAmount,b.Amount,a.LoanStatusNo   \r\n " +
          " ,TeacherNoAddBy, CAST(ISNULL(f.PrefixNameFull , '') + e.Fname + ' ' + e.Lname AS NVARCHAR) AS NameTeacherAddby   \r\n " +
          " , DATEADD(MONTH,a.PayNo-1,CAST(CAST(a.YearPay as varchar) + '/' + CAST(a.MonthPay as varchar) +'/01' as date)) as FinishDate \r\n " +
          " , (a.InterestRate / 100) * a.LoanAmount as Interest  \r\n " +
          " ,ROUND(((a.InterestRate / 100) * a.LoanAmount) + a.LoanAmount,0) as TotalLoanAmount,b.RemainsAmount  \r\n " +
          " FROM EmployeeBank.dbo.tblLoan as a    \r\n " +
          " LEFT JOIN EmployeeBank.dbo.tblGuarantor as b on a.LoanNo = b.LoanNo   \r\n " +
          " LEFT JOIN Personal.dbo.tblTeacherHis as c on b.TeacherNo = c.TeacherNo    \r\n " +
          " LEFT JOIN BaseData.dbo.tblPrefix as d on c.PrefixNo = d.PrefixNo     \r\n " +
          " LEFT JOIN Personal.dbo.tblTeacherHis as e on a.TeacherNoAddBy = e.TeacherNo   \r\n " +
          " LEFT JOIN BaseData.dbo.tblPrefix as f on e.PrefixNo = f.PrefixNo     \r\n " +
          " WHERE a.LoanNo = '{LoanID}' and LoanStatusNo != 4 ;  \r\n " +
          "   \r\n " +
          " SELECT Concat(b.Mount , '/' , Year)  \r\n " +
          " FROM EmployeeBank.dbo.tblLoan as a   \r\n " +
          " LEFT JOIN EmployeeBank.dbo.tblBillDetail as b on a.LoanNo = b.LoanNo  \r\n " +
          " LEFT JOIN EmployeeBank.dbo.tblBill as c on b.BillNo = c.BillNo  \r\n " +
          " WHERE a.LoanNo = '{LoanID}' and TypeNo = '2' and Cancel != 0;  \r\n" +
          " \r\n" +
          "SELECT ROUND((b.LoanAmount / 100) * b.InterestRate + b.LoanAmount,0) as LoanAmount  \r\n" +
          ",SUM(a.RemainsAmount) as totelLoan  \r\n" +
          ",IIF(LoanAmount != SUM(a.RemainsAmount),ROUND((b.LoanAmount / 100) * b.InterestRate + b.LoanAmount,0)-SUM(a.RemainsAmount),SUM(a.RemainsAmount)) AS TotelpayLoan  \r\n" +
          ",ROUND(((b.LoanAmount / 100) * b.InterestRate + b.LoanAmount) / b.PayNo,0) as payall  \r\n" +
          "FROM EmployeeBank.dbo.tblGuarantor as a  \r\n" +
          "LEFT JOIN EmployeeBank.dbo.tblLoan as b on a.LoanNo = b.LoanNo  \r\n" +
          "WHERE a.LoanNo = '{LoanID}'  \r\n" +
          "GROUP BY b.LoanAmount,b.InterestRate,b.PayNo"
           ,


        };
        public InfoLoan()
        {
            InitializeComponent();

            Console.WriteLine("==================Open InfoLoan Form======================");

        }

        //ChangeSizeForm
        private void InfoLoan_SizeChanged(object sender, EventArgs e)
        {
            Class.FromSettingMedtod.ChangeSizePanal(this, panel1);
        }

        private void BSearchTeacher_Click(object sender, EventArgs e)
        {
            Bank.Search IN;
            try
            {

                IN = new Bank.Search(SQLDefault[0],"");
                IN.ShowDialog();
                TBTeacherNo.Text = Bank.Search.Return[0];
                TBTeacherName.Text = Bank.Search.Return[1];
                comboBox1.Enabled = true;
                comboBox1.Items.Clear();
                Check = 1;
                comboBox1.Items.Clear();
                comboBox1.SelectedIndex = -1;
                TBTeacherName.Text = "";
                TBYearPay_Detail.Text = "";
                TBMonthPay_Detail.Text = "";
                TBTotalAmount_Detail.Text = "";
                TBPayNo_Detail.Text = "";
                TBLoanStatus.Text = "";
                TBLoanNo.Text = "";
                TBSavingAmount.Text = "";
                DGVGuarantor.Rows.Clear();
                DGVLoanDetail.Rows.Clear();
                ComboBox[] cb = new ComboBox[] { comboBox1 };
                DataTable dt = Class.SQLConnection.InputSQLMSSQL(SQLDefault[1]
                    .Replace("{TeacherNo}", TBTeacherNo.Text));
                for (int x = 0; x < dt.Rows.Count; x++)
                if(Bank.Search.Return[0].ToString() != "")
                {
                    TBTeacherNo.Text = Bank.Search.Return[0];
                    TBTeacherName.Text = Bank.Search.Return[1];
                    TBTeacherNo_KeyDown(sender, new KeyEventArgs(Keys.Enter));
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
                    comboBox1.Enabled = true;
                    comboBox1.Items.Clear();
                    Check = 1;
                    comboBox1.Items.Clear();
                    comboBox1.SelectedIndex = -1;
                    TBTeacherName.Text = "";
                    TBLoanStatus.Text = "";
                    TBLoanNo.Text = "";
                    TBSavingAmount.Text = "";
                    DGVGuarantor.Rows.Clear();
                    DGVLoanDetail.Rows.Clear();
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
                                cb[aa].Items.Add(new BankTeacher.Class.ComboBoxPayment("รายการกู้ " + dt.Rows[x][0].ToString(), dt.Rows[x][0].ToString()));
                            }
                        }
                        if (comboBox1.Items.Count == 1)
                            comboBox1.SelectedIndex = 0;
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
                    DGVGuarantor.Rows.Clear();
                    comboBox1.Enabled = false;
                    Check = 0;
                    TBLoanNo.Text = "";
                    TBYearPay_Detail.Text = "";
                    TBMonthPay_Detail.Text = "";
                    TBTotalAmount_Detail.Text = "";
                    TBPayNo_Detail.Text = "";
                    TBInterestRate_Detail.Text = "";
                    TBLoanStatus.Text = "";
                    TBSavingAmount.Text = "";
                    DGVLoanDetail.Rows.Clear();
                    TBTeacheraddbyNo.Text = "";
                    TBTeacheraddbyname.Text = "";
                    TBSignUpDate_Detail.Text = "";
                    TBFinishYearPay_Detail.Text = "";
                    TBFinishMonthPay_Detail.Text = "";
                    TBPaidNo_Detail.Text = "";
                    TBLoanAmount_Deatail.Text = "";
                    TBInterest_Detail.Text = "";

                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox1.SelectedIndex != -1)
            {
                BankTeacher.Class.ComboBoxPayment Loan = (comboBox1.SelectedItem as BankTeacher.Class.ComboBoxPayment);
                DataSet ds = BankTeacher.Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[2].Replace("{LoanID}", Loan.No));
                DGVGuarantor.Rows.Clear();
                if (ds.Tables[0].Rows.Count != 0)
                {
                    Amount.Clear();
                    Percent.Clear();
                    //==== Clear info ====
                    info_Sum = 0;
                    how_many_laps = 0;
                    info_GuarantrAmount.Clear();
                    info_GuarantrN.Clear();
                    info_GuarantrPercent.Clear();
                    info_GuarantRemains.Clear();
                    for (int loopPS = 0; loopPS < ds.Tables[0].Rows.Count; loopPS++)
                    {
                        info_Sum += Convert.ToSingle(ds.Tables[0].Rows[loopPS][9].ToString());
                        Amount.Add(Convert.ToSingle(ds.Tables[0].Rows[loopPS][9].ToString()));
                        //ผู้ค้ำ
                        if (loopPS != 0)
                        {
                            info_GuarantrN.Add(ds.Tables[0].Rows[loopPS][1].ToString());
                            info_GuarantrAmount.Add(ds.Tables[0].Rows[loopPS][9].ToString());
                            info_GuarantRemains.Add(ds.Tables[0].Rows[loopPS][16].ToString());
                        }
                    }
                    for (int looPS1 = 0; looPS1 < Amount.Count; looPS1++)
                    {
                        Double Percent = Amount[looPS1] / info_Sum * 100;
                        Double ppp = Convert.ToDouble(Convert.ToInt32(Percent)) + 0.5;
                        if (Math.Round(Percent, 0) > Convert.ToDouble(Convert.ToInt32(Percent)) + 0.5)
                        {
                            Percent += 1;
                            InfoLoan.Percent.Add(Percent);
                            // ผู้ค้ำ
                            if (looPS1 != 0)
                                info_GuarantrPercent.Add(Percent.ToString());
                        }
                        else
                        {
                            Percent = Math.Round(Percent, 0);
                            // ผู้ค้ำ
                            if (looPS1 != 0)
                                info_GuarantrPercent.Add(Percent.ToString());
                            InfoLoan.Percent.Add(Percent);
                        }
                    }
                    for (int x = 0; x < ds.Tables[0].Rows.Count; x++)
                    {
                        DGVGuarantor.Rows.Add(ds.Tables[0].Rows[x][0].ToString(), ds.Tables[0].Rows[x][1].ToString(), Percent[x], ds.Tables[0].Rows[x][9].ToString());
                    }
                    TBLoanNo.Text = Loan.No;
                    TBYearPay_Detail.Text = ds.Tables[0].Rows[0][5].ToString();
                    TBMonthPay_Detail.Text = ds.Tables[0].Rows[0][4].ToString();
                    TBTotalAmount_Detail.Text = ds.Tables[0].Rows[0][15].ToString();
                    TBPayNo_Detail.Text = ds.Tables[0].Rows[0][6].ToString();
                    TBInterestRate_Detail.Text = ds.Tables[0].Rows[0][7].ToString();
                    TBLoanStatus.Text = ds.Tables[0].Rows[0][10].ToString();
                    TBSavingAmount.Text = ds.Tables[0].Rows[0][2].ToString();
                    DGVLoanDetail.Rows.Clear();
                    TBTeacheraddbyNo.Text = ds.Tables[0].Rows[0][11].ToString();
                    TBTeacheraddbyname.Text = ds.Tables[0].Rows[0][12].ToString();
                    int Month = Convert.ToInt32(ds.Tables[0].Rows[0][4].ToString());
                    int Year = Convert.ToInt32(ds.Tables[0].Rows[0][5].ToString());
                    TBSignUpDate_Detail.Text = ds.Tables[0].Rows[0][2].ToString();
                    DateTime FinishDate = Convert.ToDateTime(ds.Tables[0].Rows[0][13].ToString());
                    TBFinishYearPay_Detail.Text = FinishDate.ToString("yyyy");
                    TBFinishMonthPay_Detail.Text = FinishDate.ToString("MM");
                    TBPaidNo_Detail.Text = ds.Tables[1].Rows.Count.ToString();
                    TBLoanAmount_Deatail.Text = ds.Tables[0].Rows[0][8].ToString();
                    TBInterest_Detail.Text = ds.Tables[0].Rows[0][14].ToString();
                    DGVLoanDetail.Rows.Clear();

                    Double Interest = Convert.ToDouble(Convert.ToDouble(ds.Tables[0].Rows[0][8].ToString())) * (Convert.ToDouble(ds.Tables[0].Rows[0][7].ToString()) / 100) / Convert.ToDouble(ds.Tables[0].Rows[0][6].ToString());

                    int Pay = Convert.ToInt32(Convert.ToDouble(ds.Tables[0].Rows[0][8].ToString()) / Convert.ToInt32(ds.Tables[0].Rows[0][6].ToString()));
                    //int Pay = Convert.ToInt32(Convert.ToInt32(ds.Tables[0].Rows[0][8].ToString()) / Convert.ToInt32(ds.Tables[0].Rows[0][6].ToString()));
                    int SumInstallment = Convert.ToInt32(Pay + Interest);
                    String StatusPay = "";

                    for (int Num = 0; Num < int.Parse(ds.Tables[0].Rows[0][6].ToString()); Num++)
                    {
                        if (Month > 12)
                        {
                            Month = 1;
                            Year++;
                        }
                        if (Num == Convert.ToInt32(ds.Tables[0].Rows[0][6].ToString()) - 1)
                        {
                            Interest = Convert.ToInt32((Convert.ToDouble(ds.Tables[0].Rows[0][8].ToString()) * (Convert.ToDouble(TBInterestRate_Detail.Text) / 100)) - (Convert.ToInt32(Interest) * Num));
                            Pay = Pay * Num;
                            Pay = Convert.ToInt32(ds.Tables[0].Rows[0][8].ToString()) - Pay;
                            SumInstallment = Convert.ToInt32(Pay + Interest);
                        
                        }
                        try
                        {
                            if (Month + "/" + Year == ds.Tables[1].Rows[Num][0].ToString())
                            {
                                StatusPay = "จ่ายแล้ว";
                            }
                            else
                            {
                                StatusPay = "ยังไม่จ่าย";
                            }
                        }
                        catch
                        {
                            StatusPay = "ยังไม่จ่าย";
                        }

                        DGVLoanDetail.Rows.Add($"{Month}/{Year}", Pay, Convert.ToInt32(Interest), StatusPay, SumInstallment);
                        Month++;
                    }
                    // ------ ของของข้า
                    info_name = TBTeacherName.Text;
                    info_id = TBTeacherNo.Text;
                    info_Loanid = TBLoanNo.Text;
                    info_Sum =  Convert.ToInt32(ds.Tables[2].Rows[0][0].ToString());
                    info_Loanpayall = ds.Tables[2].Rows[0][2].ToString();
                    info_startdate = ds.Tables[0].Rows[0][2].ToString();
                    info_duedate = ds.Tables[0].Rows[0][13].ToString();
                    info_totelLoan = ds.Tables[2].Rows[0][1].ToString();
                    // ตารางที่ 3
                    //TB_LoanAmount.Text = ds.Tables[2].Rows[0][0].ToString();
                    //TB_TotalLoan.Text = ds.Tables[2].Rows[0][1].ToString();
                    //TB_PayLoanAmount.Text = ds.Tables[2].Rows[0][2].ToString();

                    how_many_laps = DGVGuarantor.RowCount - 1;
                }
            }
        }
        
        private void CBPapersize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CBPapersize.SelectedItem.ToString() == "A4")
            {
                printDocument1.DefaultPageSettings.PaperSize = new PaperSize("A4", 794, 1123);
                printDocument1.DefaultPageSettings.Landscape = false;
            }
            else
            {
                //printDocument1.DefaultPageSettings.PaperSize = new PaperSize("A5",420,595);
                printDocument1.DefaultPageSettings.PaperSize = new PaperSize("A4", 595, 842);
                printDocument1.DefaultPageSettings.Landscape = true;
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Class.Print.PrintPreviewDialog.PrintReportGrid(e, DGVLoanDetail, tabControl1.SelectedTab.Text, this.AccessibilityObject.Name,0);
        }

        private void BTPrint_Click_1(object sender, EventArgs e)
        {
            if(DGVLoanDetail.RowCount != 0)
            {
                if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                {
                    printDocument1.Print();
                }
            }
            else
            {
                MessageBox.Show("ดูเหมือนคุณจะลืมอะไรนะ");
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 2)
            {
                BTPrint.Visible = true;
            }
            else
                BTPrint.Visible = false;
        }

      
    }
}
