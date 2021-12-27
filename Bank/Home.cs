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
    public partial class Home : Form
    {
        public static Font F = new Font("TH Sarabun New",16,FontStyle.Regular);
        int Check = 0;
        public static int SelectIndexRow = -1;

        /// <summary> 
        /// SQLDafault 
        /// <para>[0] SELECT Teachar IN Mont INPUT: {TeacherNo} </para> 
        /// <para>[1] Select Saving and Loan pay in year 0 = Saving 1 = loan(dataset) INPUT: {TeacherNo} {Year} </para> 
        /// <para>[2] Check has Loan INPUT : {TeacherNo}</para>
        /// <para>[3] SELECT MEMBER INPUT: {Text} </para>
        /// <para>[4] SELECT pay IN Mont INPUT:  {TeacherNo} {CByear} {CBMonth} </para>
        /// <para>[5] SELECT DATE Register Member INPUT : {TeacherNo}</para>
        /// <para>[6] Check last pay date INPUT: {TeacherNo} </para>
        /// <para>[7]  SELECT info of printf INPUT: {TeacherNo}  </para>
        /// </summary> 
        private String[] SQLDefault = new String[]
        {
          //[0] SELECT Teachar IN Mont INPUT: {TeacherNo}
            "SELECT a.TeacherNo , CAST(ISNULL(c.PrefixName+' ','')+Fname +' '+ Lname as NVARCHAR),f.TypeName,a.StartAmount,a.DateAdd \r\n" +
            "FROM EmployeeBank.dbo.tblMember as a \r\n" +
            "LEFT JOIN Personal.dbo.tblTeacherHis as b ON a.TeacherNo = b.TeacherNo \r\n" +
            "LEFT JOIN BaseData.dbo.tblPrefix as c ON b.PrefixNo = c.PrefixNo \r\n" +
            "LEFT JOIN EmployeeBank.dbo.tblBill as d ON a.TeacherNo = d.TeacherNo \r\n" +
            "LEFT JOIN EmployeeBank.dbo.tblBillDetail as e ON d.BillNo = e.BillNo \r\n" +
            "LEFT JOIN EmployeeBank.dbo.tblBillDetailType as f ON e.TypeNo = f.TypeNo \r\n" +
            "WHERE a.TeacherNo LIKE 'T%' AND e.Mount  IS NULL  AND e.Year IS NULL AND a.MemberStatusNo = 1 AND DATEPART(mm,a.DateAdd) = DATEPART(mm,GETDATE()); "
          ,          
           //[1] Select Saving and Loan pay in year 0 = Saving 1 = loan 2 = CHeck Loan(dataset) INPUT: {TeacherNo} {Year}
          "SELECT  Concat(d.Mount , '/' , d.Year)  , SUM(d.Amount)  \r\n " +
          "FROM EmployeeBank.dbo.tblMember as a  \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblShare as b on a.TeacherNo = b.TeacherNo  \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblBill as c on a.TeacherNo = c.TeacherNo  \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblBillDetail as d on c.BillNo = d.BillNo  \r\n " +
          "WHERE c.Cancel = 1 and d.TypeNo = 1 and d.Mount <= 12 and d.Year = {Year} and a.TeacherNo = '{TeacherNo}'  \r\n " +
          "GROUP BY a.TeacherNo , d.Amount , d.Mount , d.Year , a.StartAmount , CAST(a.DateAdd AS Date) , b.SavingAmount  \r\n " +
          "ORDER BY d.Mount; \r\n " +
          " \r\n " +
          "SELECT Concat(b.Mount , '/' , Year) ,EOMONTH(DATEADD(MONTH , a.PayNo-1,CAST(CAST(CAST(a.YearPay as nvarchar) +'/' + CAST(a.MonthPay AS nvarchar) + '/01' AS nvarchar) AS date)))  \r\n " +
          ", ROUND(Convert(float, ( (InterestRate / 100) * LoanAmount)/ PayNo) ,0) + ROUND(Convert(float , LoanAmount / PayNo),0)  \r\n " +
          ", (LoanAmount  + Convert(float , (InterestRate / 100) * LoanAmount)) - (ROUND(Convert(float, ( (InterestRate / 100) * LoanAmount)/ PayNo) ,0) + ROUND(Convert(float , LoanAmount / PayNo),0)) * (PayNo -1) ,a.LoanNo , a.MonthPay , YearPay , Amount \r\n " +
          "FROM EmployeeBank.dbo.tblLoan as a    \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblBillDetail as b on a.LoanNo = b.LoanNo   \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblBill as c on b.BillNo = c.BillNo   \r\n " +
          " WHERE a.TeacherNo = '{TeacherNo}'  and TypeNo = '2' and Cancel != 0  and Year = {Year} and a.LoanStatusNo = 2  \r\n " +
          "ORDER BY b.Mount;  \r\n " +
          " \r\n " +
          "SELECT CAST(DATEADD as date) , EOMONTH(DATEADD(MONTH , a.PayNo-1,CAST(CAST(CAST(a.YearPay as nvarchar) +'/' + CAST(a.MonthPay AS nvarchar) + '/01' AS nvarchar) AS date))) , ROUND(Convert(float, ( (InterestRate / 100) * LoanAmount)/ PayNo) ,0) + ROUND(Convert(float , LoanAmount / PayNo),0)  \r\n " +
          ", (LoanAmount  + Convert(float , (InterestRate / 100) * LoanAmount)) - (ROUND(Convert(float, ( (InterestRate / 100) * LoanAmount)/ PayNo) ,0) + ROUND(Convert(float , LoanAmount / PayNo),0)) * (PayNo -1) ,a.LoanNo , a.MonthPay , YearPay ,PayNo \r\n " +
          "FROM EmployeeBank.dbo.tblLoan as a \r\n " +
          "WHERE TeacherNo = '{TeacherNo}' and LoanStatusNo = 2  "
          , 


          //[2] Check has Loan INPUT : {TeacherNo}
          "SELECT EOMONTH(DATEADD(MONTH , PayNo,CAST(CAST(CAST(YearPay as nvarchar) +'/' + CAST(MonthPay AS nvarchar) + '/01' AS nvarchar) AS date))) ,ROUND(Convert(float, ( (InterestRate / 100) * LoanAmount)/ PayNo) ,0) + ROUND(Convert(float , LoanAmount / PayNo),0) , (LoanAmount  + Convert(float , (InterestRate / 100) * LoanAmount)) - (ROUND(Convert(float, ( (InterestRate / 100) * LoanAmount)/ PayNo) ,0) + ROUND(Convert(float , LoanAmount / PayNo),0)) * (PayNo -1),LoanNo , MonthPay , YearPay \r\n"+
          "FROM EmployeeBank.dbo.tblLoan\r\n "+
          "WHERE TeacherNo = '{TeacherNo}' and LoanStatusNo = 2"
          ,
          //[3] SELECT MEMBER INPUT: {Text}
          "SELECT TOP(20) a.TeacherNo , CAST(ISNULL(c.PrefixName+' ','')+[Fname] +' '+ [Lname] as NVARCHAR)AS Name, e.SavingAmount,    \r\n " +
          "b.TeacherLicenseNo,b.IdNo AS IDNo,b.TelMobile ,a.StartAmount,CAST(d.MemberStatusName as nvarchar) AS UserStatususing    \r\n " +
          "FROM EmployeeBank.dbo.tblMember as a    \r\n " +
          "LEFT JOIN Personal.dbo.tblTeacherHis as b ON a.TeacherNo = b.TeacherNo    \r\n " +
          "LEFT JOIN BaseData.dbo.tblPrefix as c ON c.PrefixNo = b.PrefixNo   \r\n " +
          "INNER JOIN EmployeeBank.dbo.tblMemberStatus as d on a.MemberStatusNo = d.MemberStatusNo  \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblShare as e on a.TeacherNo = e.TeacherNo \r\n " +
          "WHERE a.MemberStatusNo = 1 and a.TeacherNo LIKE '%{Text}%'  or CAST(c.PrefixName+' '+[Fname] +' '+ [Lname] as NVARCHAR) LIKE '%{Text}%'   and a.MemberStatusNo = 1         \r\n " +
          "GROUP BY a.TeacherNo , CAST(ISNULL(c.PrefixName+' ','')+[Fname] +' '+ [Lname] as NVARCHAR), e.SavingAmount,    \r\n " +
          "b.TeacherLicenseNo,b.IdNo ,b.TelMobile ,a.StartAmount,CAST(d.MemberStatusName as nvarchar)   \r\n " +
          "ORDER BY a.TeacherNo; "

          ,
          //[4] SELECT pay IN Mont INPUT: {TeacherNo} {CByear} {CBMonth}
          "SELECT a.TeacherNo , CAST(c.PrefixName + ' ' +[Fname] + ' ' + [Lname] as NVARCHAR)AS Name,f.TypeName,a.StartAmount \r\n" +
          "FROM EmployeeBank.dbo.tblMember as a \r\n" +
          "LEFT JOIN Personal.dbo.tblTeacherHis as b ON a.TeacherNo = b.TeacherNo \r\n" +
          "LEFT JOIN BaseData.dbo.tblPrefix as c ON c.PrefixNo = b.PrefixNo \r\n" +
          "LEFT JOIN EmployeeBank.dbo.tblBill as d ON b.TeacherNo = d.TeacherNo \r\n" +
          "LEFT JOIN EmployeeBank.dbo.tblBillDetail as e ON d.BillNo = e.BillNo \r\n" +
          "LEFT JOIN EmployeeBank.dbo.tblBillDetailType as f ON e.TypeNo = f.TypeNo \r\n" +
          "WHERE a.TeacherNo LIKE 'T{TeacherNo}%' AND e.Mount = {CBMonth} AND e.Year = {CByear} \r\n" +
          "ORDER BY a.TeacherNo;"
            ,
          //[5]SELECT DATE Register Member INPUT : {TeacherNo}
          "SELECT CAST(DateAdd as date) \r\n " +
          "FROM EmployeeBank.dbo.tblMember \r\n " +
          "WHERE TeacherNo = '{TeacherNo}' and MemberStatusNo != 2; "
          ,
          //[6] Check last pay date INPUT: {TeacherNo} 
          "SELECT EOMONTH(CAST(CAST(Year as nvarchar)+'-'+ CAST(Mount as nvarchar) +'-10' as nvarchar)) , b.BillNo ,DATEADD(MONTH,5,CAST(CAST(CAST(Year as nvarchar) +'/' + CAST(Mount AS nvarchar) + '/05' AS nvarchar) AS date)) \r\n " +
          "FROM EmployeeBank.dbo.tblBill as a \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblBillDetail as b on a.BillNo = b.BillNo \r\n " +
          "WHERE TeacherNo = '{TeacherNo}' \r\n " +
          "ORDER BY DateAdd DESC; "
          ,
          //[7]  SELECT info of printf INPUT: {TeacherNo}
          "SELECT a.TeacherNo,b.SavingAmount,IIF(c.Amount != 0,c.Amount,0) as startpay,IIF(ROUND(c.RemainsAmount,0) != 0,ROUND(c.RemainsAmount,0),0) as Remains \r\n " +
          "FROM EmployeeBank.dbo.tblMember as a \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblShare as b on a.TeacherNo = b.TeacherNo \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblGuarantor as c on b.TeacherNo = c.TeacherNo \r\n " +
          "WHERE a.TeacherNo = '{TeacherNo}'"
        };

        public Home()
        {
            InitializeComponent();
        }
     

        private void Form2_SizeChanged(object sender, EventArgs e)
        {
            Class.FromSettingMedtod.ChangeSizePanal(this, P1);
        }

        private void BSearchTeacher_Click(object sender, EventArgs e)
        {
            try
            {
                Bank.Search IN = new Bank.Search(SQLDefault[3], "หุ้นสะสม");
                IN.ShowDialog();
                TBTeacherNo.Text = Bank.Search.Return[0];
                TBTeacherNo_KeyDown(sender, new KeyEventArgs(Keys.Enter));
            }
            catch(Exception x)
            {
                Console.WriteLine( x );
            }

        }

        private void TBTeacherNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (TBTeacherNo.Text.Length >= 6)
                {
                    TBTeacherNo.Text = TBTeacherNo.Text.Replace("t", "T");
                    dataGridView3.Rows.Clear();
                    CByear.SelectedIndex = -1;
                    DataTable dt = Class.SQLConnection.InputSQLMSSQL(SQLDefault[3].Replace("{Text}", TBTeacherNo.Text));
                    if (dt.Rows.Count != 0)
                    {
                        TBTeacherName.Text = dt.Rows[0][1].ToString();
                        TBTeacherBill.Text = dt.Rows[0][4].ToString();
                        Check = 1;

                        CByear.Items.Clear();
                        CByear.SelectedIndex = -1;

                        DataTable dts = Class.SQLConnection.InputSQLMSSQL(SQLDefault[5]
                            .Replace("{TeacherNo}", TBTeacherNo.Text));
                        DataTable dtss = Class.SQLConnection.InputSQLMSSQL(SQLDefault[6]
                            .Replace("{TeacherNo}", TBTeacherNo.Text));
                        int YearRegister = Convert.ToInt32((Convert.ToDateTime(dts.Rows[0][0].ToString())).ToString("yyyy"));
                        int Yearlastofpay = Convert.ToInt32((Convert.ToDateTime(dtss.Rows[0][2].ToString())).ToString("yyyy"));
                        Yearlastofpay = Yearlastofpay - YearRegister;
                        if (YearRegister < Convert.ToInt32(BankTeacher.Bank.Menu.Date[0]) - 2)
                        {
                            int Yeard2 = Convert.ToInt32(BankTeacher.Bank.Menu.Date[0]) - 2;

                            while (Yeard2 <= Convert.ToInt32(BankTeacher.Bank.Menu.Date[0]) + Yearlastofpay)
                            {
                                CByear.Items.Add(Yeard2);
                                Yeard2++;
                            }
                        }
                        else if (YearRegister > Convert.ToInt32(BankTeacher.Bank.Menu.Date[0]) - 2)
                        {
                            while (YearRegister <= Convert.ToInt32(BankTeacher.Bank.Menu.Date[0]) + Yearlastofpay)
                            {
                                CByear.Items.Add(YearRegister);
                                YearRegister++;
                            }
                        }
                        CByear.SelectedIndex = 0;
                        CByear.Text = BankTeacher.Bank.Menu.Date[0];
                    }
                    else
                    {
                        MessageBox.Show("รหัสไม่ถูกต้อง", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                }
            }
            else if (e.KeyCode == Keys.Delete)
            {
                if (Check == 1)
                {
                    TBTeacherName.Text = "";
                    TBTeacherBill.Text = "";
                    Check = 0;
                }

            }

        }
        private void CByear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CByear.SelectedIndex != -1)
            {
                dataGridView3.Rows.Clear();
                DataSet ds = BankTeacher.Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[1]
                            .Replace("{TeacherNo}", TBTeacherNo.Text)
                            .Replace("{Year}", CByear.Text));
                DateTime RegisterDate = Convert.ToDateTime((Convert.ToDateTime(BankTeacher.Class.SQLConnection.InputSQLMSSQL(SQLDefault[5]
                        .Replace("{TeacherNo}", TBTeacherNo.Text)).Rows[0][0].ToString())).ToString("yyyy-MM-dd"));
                int lastYear = 0;
                int lastMonth = 0;
                if (ds.Tables[2].Rows.Count != 0)
                {
                    lastYear = Convert.ToInt32(Convert.ToDateTime(ds.Tables[2].Rows[0][1].ToString()).ToString("yyyy"));
                    lastMonth = Convert.ToInt32(Convert.ToDateTime(ds.Tables[2].Rows[0][1].ToString()).ToString("MM"));
                }
                int Num = 1;
                for (int x = 0; x < 12; x++) 
                {
                    //เช็คดูว่าต่ำกวว่าวันที่สมัครไหม
                    int Month = x + 1;
                    DateTime Now = Convert.ToDateTime(Convert.ToDateTime(CByear.Text + '-' + Month + '-' + DateTime.DaysInMonth(Convert.ToInt32(CByear.Text), Month)).ToString("yyyy-MM-dd"));
                    if (Now >= RegisterDate)
                    { 
                        //หุ้นสะสม 
                        if (ds.Tables[0].Rows.Count != 0)
                        { 
                            bool CheckSaving = false;
                            for(int y = 0;y < ds.Tables[0].Rows.Count;y++)
                            {
                                if (Month + "/" + CByear.Text == ds.Tables[0].Rows[y][0].ToString())
                                {
                                    dataGridView3.Rows.Add(Num++,Month + "/" + CByear.Text, "หุ้นสะสม", ds.Tables[0].Rows[y][1], "ชำระแล้ว");
                                    CheckSaving = true;
                                }
                            }
                            if (!(CheckSaving))
                            {
                                dataGridView3.Rows.Add(Num++,Month + "/" + CByear.Text, "หุ้นสะสม", "500", "ค้างชำระ");
                            }
                        }
                        else
                        {
                            dataGridView3.Rows.Add(Num++, Month + "/" + CByear.Text, "หุ้นสะสม", "500", "ค้างชำระ");
                        }
                        //Loan
                        if (ds.Tables[2].Rows.Count != 0)
                        {
                            bool CheckLoan = false;
                            int Balance = 0;
                            if (Now >= Convert.ToDateTime(Convert.ToDateTime(ds.Tables[2].Rows[0][0].ToString()).ToString("yyyy-MM-dd")) && Now <= Convert.ToDateTime(Convert.ToDateTime(ds.Tables[2].Rows[0][1].ToString()).ToString("yyyy-MM-dd")))
                            {
                                if (ds.Tables[1].Rows.Count != 0)
                                {
                                    for(int y = 0; y < ds.Tables[1].Rows.Count; y++)
                                    {
                                        if (Month + "/" + CByear.Text == ds.Tables[1].Rows[y][0].ToString())
                                        {
                                            try
                                            {
                                                Balance = Convert.ToInt32(ds.Tables[1].Rows[0][3].ToString());
                                            }
                                            catch
                                            {
                                                Balance = Convert.ToInt32(Decimal.Truncate(Convert.ToDecimal(Convert.ToDouble(ds.Tables[1].Rows[0][3].ToString()))) + 1);
                                            }

                                            if (Now != Convert.ToDateTime(Convert.ToDateTime(ds.Tables[2].Rows[0][1].ToString()).ToString("yyyy-MM-dd")))
                                                dataGridView3.Rows.Add(Num++, Month + "/" + CByear.Text, "รายการกู้" + ds.Tables[1].Rows[0][4].ToString(), ds.Tables[1].Rows[0][2].ToString(), "ชำระแล้ว");
                                            else
                                                dataGridView3.Rows.Add(Num++, Month + "/" + CByear.Text, "รายการกู้" + ds.Tables[1].Rows[0][4].ToString(), Balance, "ชำระแล้ว");
                                            CheckLoan = true;
                                        }
                                    }
                                }
                                else
                                {
                                    try
                                    {
                                        Balance = Convert.ToInt32(ds.Tables[2].Rows[0][3].ToString());
                                    }
                                    catch
                                    {
                                        Balance = Convert.ToInt32(Decimal.Truncate(Convert.ToDecimal(Convert.ToDouble(ds.Tables[2].Rows[0][3].ToString()))) + 1);
                                    }

                                    if (Now != Convert.ToDateTime(Convert.ToDateTime(ds.Tables[2].Rows[0][1].ToString()).ToString("yyyy-MM-dd")))
                                        dataGridView3.Rows.Add(Num++, Month + "/" + CByear.Text, "รายการกู้" + ds.Tables[2].Rows[0][4].ToString(), ds.Tables[2].Rows[0][2].ToString(), "ค้างชำระ");
                                    else
                                        dataGridView3.Rows.Add(Num++, Month + "/" + CByear.Text, "รายการกู้" + ds.Tables[2].Rows[0][4].ToString(), Balance, "ค้างชำระ");
                                    CheckLoan = true;
                                }
                                if(!(CheckLoan))
                                {
                                    try
                                    {
                                        Balance = Convert.ToInt32(ds.Tables[2].Rows[0][3].ToString());
                                    }
                                    catch
                                    {
                                        Balance = Convert.ToInt32(Decimal.Truncate(Convert.ToDecimal(Convert.ToDouble(ds.Tables[2].Rows[0][3].ToString()))) + 1);
                                    }

                                    if (Now != Convert.ToDateTime(Convert.ToDateTime(ds.Tables[2].Rows[0][1].ToString()).ToString("yyyy-MM-dd")))
                                        dataGridView3.Rows.Add(Num++, Month + "/" + CByear.Text, "รายการกู้" + ds.Tables[2].Rows[0][4].ToString(), ds.Tables[2].Rows[0][2].ToString(), "ค้างชำระ");
                                    else
                                        dataGridView3.Rows.Add(Num++, Month + "/" + CByear.Text, "รายการกู้" + ds.Tables[2].Rows[0][4].ToString(), Balance, "ค้างชำระ");
                                }
                                
                            }
                        }
                    }
                }
            }
            for(int x = 0; x < dataGridView3.Rows.Count; x++)
            {
                if (dataGridView3.Rows[x].Cells[3].Value.ToString() == "ค้างชำระ")
                {
                    dataGridView3.Rows[x].Cells[3].Style = new DataGridViewCellStyle { ForeColor = Color.Red };
                }
                else
                    dataGridView3.Rows[x].Cells[3].Style = new DataGridViewCellStyle { ForeColor = Color.Green };
            }
        }
        private void BTPrint_Click(object sender, EventArgs e)
        {
            if (dataGridView3.RowCount != 0)
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

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            DataTable dt = Class.SQLConnection.InputSQLMSSQL(SQLDefault[7]
                .Replace("{TeacherNo}", TBTeacherNo.Text));
            Class.Print.PrintPreviewDialog.info_name = TBTeacherName.Text;
            Class.Print.PrintPreviewDialog.info_id = TBTeacherNo.Text;
            Class.Print.PrintPreviewDialog.info_Savingtotel = dt.Rows[0][1].ToString();
            Class.Print.PrintPreviewDialog.info_Lona_AmountRemain = dt.Rows[0][3].ToString();
            Class.Print.PrintPreviewDialog.PrintReportGrid(e, dataGridView3, "ตารางการผ่อนชำระ", this.AccessibilityObject.Name,false,false, "A4",1);
        }
    }
}

