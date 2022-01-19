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
        /// <para>[3] for printback  INPUT : {TeacherNo} {LoanNo} </para>
        ///   //[4] BackPrint payLoan INPUT : {TeacherNo} {LoanNo}
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
          "WHERE (a.TeacherNo LIKE '%{Text}%' or CAST(ISNULL(c.PrefixName+' ','') + Fname + ' ' + Lname AS nvarchar) LIKE '%{Text}%') and MemberStatusNo = 1 \r\n " +
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
           //[3] for printback  INPUT : {TeacherNo} {LoanNo}
         "SELECT a.TeacherNo,CAST(ISNULL(d.PrefixName+' ','')+Fname +' '+ Lname as NVARCHAR)AS Name,LoanAmount , \r\n " +
         "CAST(cNo + ' หมู่ ' + cMu + 'ซอย  ' + cSoi + ' ถนน' + cRoad + ' ตำบล' +  TumBonName + ' อำเภอ'  + AmphurName + ' จังหวัด ' + JangWatLongName + ' รหัสไปรสณี ' + ZipCode as NVARCHAR(255)) AS ADDRESS, \r\n " +
         "MonthPay , YearPay , PayNo , InterestRate \r\n " +
         "FROM EmployeeBank.dbo.tblLoan as a \r\n " +
         "LEFT JOIN EmployeeBank.dbo.tblGuarantor as b on a.LoanNo = b.LoanNo \r\n " +
         "LEFT JOIN Personal.dbo.tblTeacherHis as c ON b.TeacherNo = c.TeacherNo \r\n " +
         "LEFT JOIN BaseData.dbo.tblPrefix as d ON c.PrefixNo = d.PrefixNo \r\n " +
         "LEFT JOIN BaseData.dbo.tblTumBon as e on c.cTumBonNo = e.TumBonNo \r\n " +
         "LEFT JOIN BaseData.dbo.tblAmphur as f on c.cAmPhurNo = f.AmphurNo \r\n " +
         "LEFT JOIN BaseData.dbo.tblJangWat as g on c.cJangWatNo = g.JangWatNo \r\n " +
         "WHERE a.TeacherNo = '{TeacherNo}' and a.LoanNo = {LoanNo}"
          ,
           //[4] BackPrint payLoan INPUT : {TeacherNo} {LoanNo}
          "SELECT a.LoanNo,CAST(a.PayDate as date),a.LoanAmount,b.LoanStatusName,CAST(d.PrefixName+' '+c.Fname+' '+c.Lname as nvarchar),CAST(f.Name as nvarchar)  \r\n " +
          "FROM EmployeeBank.dbo.tblLoan as a \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblLoanStatus b ON a.LoanStatusNo = b.LoanStatusNo \r\n " +
          "LEFT JOIN Personal.dbo.tblTeacherHis c ON a.TeacherNoAddBy = c.TeacherNo \r\n " +
          "LEFT JOIN BaseData.dbo.tblPrefix d ON c.PrefixNo = d.PrefixNo \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblBillDetailPayment f ON a.BillDetailPaymentNo = f.BillDetailPaymentNo \r\n " +
          "WHERE a.TeacherNo = '{TeacherNo}'AND LoanNo = '{LoanNo}'"


        };
        public InfoLoan()
        {
            InitializeComponent();


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

                IN = new Bank.Search(SQLDefault[0]);
                IN.ShowDialog();
                CB_LoanNo.Enabled = true;
                CB_LoanNo.Items.Clear();
                Check = 1;
                //Checkmember(false);
                CB_LoanNo.Items.Clear();
                CB_LoanNo.SelectedIndex = -1;
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
                //ComboBox[] cb = new ComboBox[] { CB_LoanNo };
                //DataTable dt = Class.SQLConnection.InputSQLMSSQL(SQLDefault[1]
                //    .Replace("{TeacherNo}", TBTeacherNo.Text));
                //for (int x = 0; x < dt.Rows.Count; x++)
                if(Bank.Search.Return[0] != "")
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
                TBTeacherNo.Text = TBTeacherNo.Text.Replace("t", "T");
                tabControl1.SelectedIndex = 0;
                CB_LoanNo.Enabled = true;
                CB_LoanNo.Items.Clear();
                Check = 1;
                CB_LoanNo.Items.Clear();
                CB_LoanNo.SelectedIndex = -1;
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
                    CB_LoanNo.Enabled = true;
                    tabControl1.Enabled = true;
                    CB_LoanNo.Items.Clear();
                    Check = 1;
                    ComboBox[] cb = new ComboBox[] { CB_LoanNo };
                    for (int x = 0; x < dt.Rows.Count; x++)
                    {
                        for (int aa = 0; aa < cb.Length; aa++)
                        {
                            cb[aa].Items.Add(new BankTeacher.Class.ComboBoxPayment("รายการกู้ " + dt.Rows[x][0].ToString(), dt.Rows[x][0].ToString()));
                        }
                    }
                    CB_LoanNo.SelectedIndex = 0;
                    Checkmember(false);
                    if (CB_LoanNo.Items.Count == 1)
                        CB_LoanNo.SelectedIndex = 0;
                    DataTable dt_ChcekFile = Class.SQLConnection.InputSQLMSSQL("SELECT a.LoanNo,a.DocStatusNo,a.DocUploadPath \r\n" +
                    "FROM EmployeeBank.dbo.tblLoan as a \r\n" +
                    "WHERE a.LoanNo = '{LoanNo}'".Replace("{LoanNo}",TBLoanNo.Text));
                    if (dt_ChcekFile.Rows[0][1].ToString() ==  "2")
                    {
                        LB_Flie.Text = "กรุณาอัพโหลดเอกสาร";
                        LB_Flie.ForeColor = Color.Red;
                    }
                    else
                    {
                        LB_Flie.Text = "อัพโหลดเอกสารสำเร็จ";
                        LB_Flie.ForeColor = Color.Green;
                    }
                }
                else
                {
                    MessageBox.Show("รหัสผู้ใช้ไม่ถูกต้อง", "System", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            else if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
            {
                if (Check == 1)
                {
                    CB_LoanNo.Items.Clear();
                    CB_LoanNo.SelectedIndex = -1;
                    TBTeacherName.Text = "";
                    DGVGuarantor.Rows.Clear();

                    CB_LoanNo.Enabled = false;

                    //comboBox1.Enabled = false;
                    button3.Enabled = false;
                    BTOpenfile_Reg.Enabled = false;
                    Checkmember(true);
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
        private void Checkmember(bool tf)
        {
            TBTeacherNo.Enabled = tf;
            BSearchTeacher.Enabled = tf;
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(CB_LoanNo.SelectedIndex != -1)
            {
                BankTeacher.Class.ComboBoxPayment Loan = (CB_LoanNo.SelectedItem as BankTeacher.Class.ComboBoxPayment);
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
                    button3.Enabled = true;
                    BTOpenfile_Reg.Enabled = true;
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

                        DGVLoanDetail.Rows.Add(Num+1,$"{Year}/{Month}", Pay, Convert.ToInt32(Interest), StatusPay, SumInstallment);
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

                    DGV_Historyloanpay.Rows.Clear();
                    //BTPrint_T.BackColor = Color.White;
                    DataTable dt = Class.SQLConnection.InputSQLMSSQL(SQLDefault[4].Replace("{TeacherNo}", TBTeacherNo.Text)
                         .Replace("{LoanNo}", TBLoanNo.Text));
                    Class.Print.PrintPreviewDialog.info_PayLoanBill = dt.Rows[0][0].ToString();
                    Class.Print.PrintPreviewDialog.info_PayLoandate = dt.Rows[0][1].ToString();
                    Class.Print.PrintPreviewDialog.info_id = TBTeacherNo.Text;
                    Class.Print.PrintPreviewDialog.info_name = TBTeacherName.Text;
                    Class.Print.PrintPreviewDialog.info_TeacherAdd = dt.Rows[0][4].ToString();
                    Class.Print.PrintPreviewDialog.info_Payment = dt.Rows[0][5].ToString();
                    string date = "";
                    if(dt.Rows[0][1].ToString() == "") { date = "รอดำเนินการ"; } else { date = dt.Rows[0][1].ToString(); }
                    for (int loop = 0; loop < dt.Rows.Count; loop++)
                    {
                        DGV_Historyloanpay.Rows.Add(dt.Rows[0][0].ToString(),date, dt.Rows[0][3].ToString(), dt.Rows[0][2].ToString());
                        //DGV_info.Rows.Add(loop + 1, "จ่ายกู้", dt.Rows[0][2].ToString());
                    }

                    how_many_laps = DGVGuarantor.RowCount - 1;
                }
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            if(CB_SelectPrint.SelectedIndex == 0)
            {
                Class.Print.PrintPreviewDialog.PrintReportGrid(e, DGVLoanDetail, tabControl1.SelectedTab.Text, this.AccessibilityObject.Name,false,false,"A4", 1);
            }
            else if(CB_SelectPrint.SelectedIndex == 1)
            {
                Class.Print.PrintPreviewDialog.PrintLoan(e, SQLDefault[3].Replace("{TeacherNo}", TBTeacherNo.Text).Replace("{LoanNo}",TBLoanNo.Text),
                   Bank.Menu.Date[2], Bank.Menu.Monthname, (Convert.ToInt32(Bank.Menu.Date[0]) + 543).ToString(),
                   TBTeacherNo.Text, TBLoanNo.Text, DGVGuarantor.RowCount,checkBox_scrip.Checked,checkBox_copy.Checked);
            }
            else
            {
                Class.Print.PrintPreviewDialog.details = 1;
                Class.Print.PrintPreviewDialog.PrintReportGrid(e, DGV_Historyloanpay, tabControl1.SelectedTab.Text, this.AccessibilityObject.Name,checkBox_scrip.Checked,checkBox_copy.Checked, "A4",0);
            }
            Class.Print.PrintPreviewDialog.details = 0;
        }
        private void BTPrint_Click_1(object sender, EventArgs e)
        {
            if(CB_SelectPrint.SelectedIndex == 0)
            {
                if(DGVLoanDetail.Rows.Count != 0)
                {
                    printDocument1.DefaultPageSettings.PaperSize = new PaperSize("Letter", 850, 1100);
                    printDocument1.DefaultPageSettings.Landscape = false;
                    if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                    {
                        printDocument1.Print();
                    }
                }
                else
                {
                    MessageBox.Show("โปรดเเจ้งผู้ทำระบบ", "เเจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if(CB_SelectPrint.SelectedIndex == 1)
            {
                printDocument1.DefaultPageSettings.PaperSize = new PaperSize("Letter", 850, 1100);
                printDocument1.DefaultPageSettings.Landscape = false;
                if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                {
                    printDocument1.Print();
                }
            }
            else
            {
                if (DGV_Historyloanpay.Rows.Count != 0)
                {
                    printDocument1.DefaultPageSettings.PaperSize = new PaperSize("A4", 595, 842);
                    printDocument1.DefaultPageSettings.Landscape = true;
                    if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                    {
                        printDocument1.Print();
                    }
                }
                else
                {
                    MessageBox.Show("โปรดเเจ้งผู้ทำระบบ", "เเจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        private void BExitForm_Click(object sender, EventArgs e)
        {
            BankTeacher.Class.FromSettingMedtod.ReturntoHome(this);
        }

        private void InfoLoan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (TBTeacherNo.Text.Length != 0)
                {
                    Checkmember(true);
                    TBTeacherNo.Text = "";
                    CB_LoanNo.Items.Clear();
                    CB_LoanNo.SelectedIndex = -1;
                    TBTeacherName.Text = "";
                    DGVGuarantor.Rows.Clear();

                    CB_LoanNo.Enabled = false;

                    //comboBox1.Enabled = false;
                    //button3.Enabled = false;
                    BTOpenfile_Reg.Enabled = false;

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
                else
                {
                    BExitForm_Click(new object(), new EventArgs());
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Bank.Add_Member.infoMeber.OroD = "เปิดไฟล์";
            if (CB_LoanNo.SelectedIndex != -1)
            {
                BankTeacher.Class.ComboBoxPayment Loan = (CB_LoanNo.SelectedItem as BankTeacher.Class.ComboBoxPayment);
                //Input Location Folder
                var smb = new BankTeacher.Class.ProtocolSharing.ConnectSMB.SmbFileContainer("Loan");
                if (smb.IsValidConnection())
                {
                    //Input Contain words แนะนำ เป็นรหัสอาจารย์ ในหน้าทั่วไปส่วนหน้าไหนถ้ามีการทำรายการเยอะๆให้เอาเป็นเลขบิลล์ของหน้านั้นๆเช่นหน้าดูเอกสารกู้ จะใส่เป็นเลขกู้ หน้าดูเอกสาร สมัครสมาชิกจะใส่เป็นชื่ออาจารย์
                    smb.GetFile(Loan.No);
                }
                else { MessageBox.Show("โปรดตรวจสอบการเชื่อมต่อ ไม่สามรถเข้าถึงโฟร์เดอร์ได้", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }
        }
        String imgeLocation = "";
        private void BTOpenfile_Reg_Click(object sender, EventArgs e)
        {
            DataTable dt = Class.SQLConnection.InputSQLMSSQL("SELECT a.DocStatusNo, a.DocUploadPath \r\n" +
            "FROM EmployeeBank.dbo.tblLoan as a \r\n" +
            "WHERE a.LoanNo = '{LoanNo}'".Replace("{LoanNo}", TBLoanNo.Text));
            if (CB_LoanNo.SelectedIndex != -1)
            {
                if (dt.Rows[0][0].ToString() != "1")
                {
                    BankTeacher.Class.ComboBoxPayment Loan = (CB_LoanNo.SelectedItem as BankTeacher.Class.ComboBoxPayment);
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

                            var smb = new BankTeacher.Class.ProtocolSharing.ConnectSMB.SmbFileContainer("Loan");
                            if (smb.IsValidConnection())
                            {
                                String Return = smb.SendFile(imgeLocation, "Loan" + Loan.No + ".pdf", TBTeacherNo.Text, 3, BankTeacher.Class.UserInfo.TeacherNo, Loan.No);
                                MessageBox.Show(Return, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                if (Return.Contains("อัพโหลดเอกสารสำเร็จ"))
                                {
                                    Class.SQLConnection.InputSQLMSSQL("UPDATE EmployeeBank.dbo.tblLoan \r\n" +
                                    "set DocStatusNo = 1, DocUploadPath = '{PathFile}' \r\n".Replace("{PathFile}", smb.networkPath + "Loan" + Loan.No + ".pdf") +
                                    "WHERE LoanNo = '{LoanNo}'".Replace("{LoanNo}", TBLoanNo.Text));
                                    imgeLocation = "";
                                    LB_Flie.Text = "อัพโหลดสำเร็จ";
                                    LB_Flie.ForeColor = Color.Green;
                                }
                            }
                            else
                            {
                                MessageBox.Show("ไม่สามารถสร้างไฟล์ในที่นั้นได้", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                    catch
                    {
                        MessageBox.Show("An Error Occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else { MessageBox.Show("ทำการส่งไฟล์แล้ว ไม่สามารถดำเนินการส่งไฟล์ซ้ำได้", "ไฟล์", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }
            else
            {
                MessageBox.Show("กรุณาเลือกรายการกู้ก่อนอัพโหลดเอกสาร", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void CB_SelectPrint_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(CB_SelectPrint.SelectedIndex > 0)
            {
                checkBox_scrip.Enabled = true;
                checkBox_copy.Enabled = true;
            }
            else
            {
                checkBox_scrip.Enabled = false;
                checkBox_copy.Enabled = false;
            }
        }

        private void BT_deleteflie_Click(object sender, EventArgs e)
        {
            do
            {
                Bank.SelectFile.info_File.No = TBLoanNo.Text;
                Bank.SelectFile.info_File.Type = "LoanNo";
                Bank.Add_Member.infoMeber.OroD = "ลบ";
                var smb = new BankTeacher.Class.ProtocolSharing.ConnectSMB.SmbFileContainer("Loan");
                if (smb.IsValidConnection())
                {
                    smb.GetFile(TBLoanNo.Text);
                }
                else { MessageBox.Show("โปรดตรวจสอบการเชื่อมต่อ ไม่สามรถเข้าถึงโฟร์เดอร์ได้", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Warning); }

            }
            while (!Bank.SelectFile.OpenEnableButton);
            
            //if (BankTeacher.Class.ProtocolSharing.ConnectSMB.StatusRetrun != "")
            //{
            //    MessageBox.Show(BankTeacher.Class.ProtocolSharing.ConnectSMB.StatusRetrun, "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
            //if (System.IO.File.Exists($@"{smb.networkPath}\Loan{TBLoanNo.Text}.pdf"))
            //{

            //}
            //else
            //{
            //    MessageBox.Show("ไม่พบไฟล์ โปรดตวรจสอบ การส่งไฟล์ใหม่ หรือ ดูที่อยู่ไฟล์","ไฟล์",MessageBoxButtons.OK,MessageBoxIcon.Information);
            //}
        }
    }
}