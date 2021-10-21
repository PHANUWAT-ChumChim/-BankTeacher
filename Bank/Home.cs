using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace example.GOODS
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
        /// </summary> 
        private String[] SQLDefault = new String[]
        {
          //[0] SELECT Teachar IN Mont INPUT: {TeacherNo}
            "SELECT a.TeacherNo , CAST(c.PrefixName+' '+Fname +' '+ Lname as NVARCHAR),f.TypeName,a.StartAmount,a.DateAdd \r\n" +
            "FROM EmployeeBank.dbo.tblMember as a \r\n" +
            "LEFT JOIN Personal.dbo.tblTeacherHis as b ON a.TeacherNo = b.TeacherNo \r\n" +
            "LEFT JOIN BaseData.dbo.tblPrefix as c ON b.PrefixNo = c.PrefixNo \r\n" +
            "LEFT JOIN EmployeeBank.dbo.tblBill as d ON a.TeacherNo = d.TeacherNo \r\n" +
            "LEFT JOIN EmployeeBank.dbo.tblBillDetail as e ON d.BillNo = e.BillNo \r\n" +
            "LEFT JOIN EmployeeBank.dbo.tblBillDetailType as f ON e.TypeNo = f.TypeNo \r\n" +
            "WHERE a.TeacherNo LIKE 'T%' AND e.Mount  IS NULL  AND e.Year IS NULL AND a.MemberStatusNo = 1 AND DATEPART(mm,a.DateAdd) = DATEPART(mm,GETDATE()); "
          ,          
           //[1] Select Saving and Loan pay in year 0 = Saving 1 = loan(dataset) INPUT: {TeacherNo} {Year}
          "SELECT  Concat(d.Mount , '/' , d.Year)  , SUM(d.Amount) \r\n " +
          "FROM EmployeeBank.dbo.tblMember as a \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblShare as b on a.TeacherNo = b.TeacherNo \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblBill as c on a.TeacherNo = c.TeacherNo \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblBillDetail as d on c.BillNo = d.BillNo \r\n " +
          "WHERE c.Cancel = 1 and d.TypeNo = 1 and d.Mount <= 12 and d.Year = {Year} and a.TeacherNo LIKE '{TeacherNo}' \r\n " +
          "GROUP BY a.TeacherNo , d.Amount , d.Mount , d.Year , a.StartAmount , CAST(a.DateAdd AS Date) , b.SavingAmount \r\n " +
          "ORDER BY d.Mount; \r\n " +

          " \r\n " +

          "SELECT Concat(b.Mount , '/' , Year) ,EOMONTH(DATEADD(MONTH , a.PayNo-1,CAST(CAST(CAST(a.YearPay as nvarchar) +'/' + CAST(a.MonthPay AS nvarchar) + '/01' AS nvarchar) AS date))) \r\n " +
          ", ROUND(Convert(float, ( (InterestRate / 100) * LoanAmount)/ PayNo) ,0) + ROUND(Convert(float , LoanAmount / PayNo),0) " +
          ", (LoanAmount  + Convert(float , (InterestRate / 100) * LoanAmount)) - (ROUND(Convert(float, ( (InterestRate / 100) * LoanAmount)/ PayNo) ,0) + ROUND(Convert(float , LoanAmount / PayNo),0)) * (PayNo -1) ,a.LoanNo , a.MonthPay , YearPay , Amount\r\n " +
          "FROM EmployeeBank.dbo.tblLoan as a   \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblBillDetail as b on a.LoanNo = b.LoanNo  \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblBill as c on b.BillNo = c.BillNo  \r\n " +
          " WHERE a.TeacherNo = '{TeacherNo}'  and TypeNo = '2' and Cancel != 0  and Year = {Year} and a.LoanStatusNo = 2 or a.LoanAmount = 3\r\n "+
         "ORDER BY b.Mount; \r\n "
          , 

          //[2] Check has Loan INPUT : {TeacherNo}
          "SELECT EOMONTH(DATEADD(MONTH , PayNo,CAST(CAST(CAST(YearPay as nvarchar) +'/' + CAST(MonthPay AS nvarchar) + '/01' AS nvarchar) AS date))) ,ROUND(Convert(float, ( (InterestRate / 100) * LoanAmount)/ PayNo) ,0) + ROUND(Convert(float , LoanAmount / PayNo),0) , (LoanAmount  + Convert(float , (InterestRate / 100) * LoanAmount)) - (ROUND(Convert(float, ( (InterestRate / 100) * LoanAmount)/ PayNo) ,0) + ROUND(Convert(float , LoanAmount / PayNo),0)) * (PayNo -1),LoanNo , MonthPay , YearPay \r\n"+
          "FROM EmployeeBank.dbo.tblLoan\r\n "+
          "WHERE TeacherNo = '{TeacherNo}' and LoanStatusNo = 2"
          ,
          //[3] SELECT MEMBER INPUT: {Text}
          "SELECT TOP(20) a.TeacherNo , CAST(c.PrefixName+' '+[Fname] +' '+ [Lname] as NVARCHAR)AS Name, e.SavingAmount,    \r\n " +
          "b.TeacherLicenseNo,b.IdNo AS IDNo,b.TelMobile ,a.StartAmount,CAST(d.MemberStatusName as nvarchar) AS UserStatususing    \r\n " +
          "FROM EmployeeBank.dbo.tblMember as a    \r\n " +
          "LEFT JOIN Personal.dbo.tblTeacherHis as b ON a.TeacherNo = b.TeacherNo    \r\n " +
          "LEFT JOIN BaseData.dbo.tblPrefix as c ON c.PrefixNo = b.PrefixNo   \r\n " +
          "INNER JOIN EmployeeBank.dbo.tblMemberStatus as d on a.MemberStatusNo = d.MemberStatusNo  \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblShare as e on a.TeacherNo = e.TeacherNo \r\n " +
          "WHERE a.TeacherNo LIKE '%{Text}%'  or CAST(c.PrefixName+' '+[Fname] +' '+ [Lname] as NVARCHAR) LIKE '%{Text}%'   and a.MemberStatusNo = 1         \r\n " +
          "GROUP BY a.TeacherNo , CAST(c.PrefixName+' '+[Fname] +' '+ [Lname] as NVARCHAR), e.SavingAmount,    \r\n " +
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
        };

        public Home()
        {
            InitializeComponent();
        }
     

        private void Form2_SizeChanged(object sender, EventArgs e)
        {
            Class.FromSettingMedtod.ChangeSizePanal(this, P1);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        { 
        }

        private void BSearchTeacher_Click(object sender, EventArgs e)
        {
            try
            {
                Bank.Search IN = new Bank.Search(SQLDefault[3]);
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
                if (TBTeacherNo.Text.Length == 6)
                {
                    dataGridView3.Rows.Clear();
                    CByear.SelectedIndex = -1;
                    CBStatus.SelectedIndex = -1;
                    CBStatus.Enabled = false;
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
                        int YearRegister = Convert.ToInt32((Convert.ToDateTime(dts.Rows[0][0].ToString())).ToString("yyyy"));
                        if (YearRegister < Convert.ToInt32(example.GOODS.Menu.Date[0]) - 2)
                        {
                            int Yeard2 = Convert.ToInt32(example.GOODS.Menu.Date[0]) - 2;

                            while (Yeard2 <= Convert.ToInt32(example.GOODS.Menu.Date[0]) + 1)
                            {
                                CByear.Items.Add(Yeard2);
                                Yeard2++;
                            }
                        }
                        else if (YearRegister > Convert.ToInt32(example.GOODS.Menu.Date[0]) - 2)
                        {
                            while (YearRegister <= Convert.ToInt32(example.GOODS.Menu.Date[0]) + 1)
                            {
                                CByear.Items.Add(YearRegister);
                                YearRegister++;
                            }
                        }
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
                    CBStatus.SelectedIndex = -1;
                    CBStatus.Enabled = false;
                    Check = 0;
                }

            }

        }
        private void Home_Load(object sender, EventArgs e)
        {
        }

        private void automatic_Click(object sender, EventArgs e)
        {
            
        }

        private void CBMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CByear.SelectedIndex != -1)
            {
                CBStatus.Enabled = true;
            }
        }
        
        private void CByear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CByear.SelectedIndex != -1)
            {
                CBStatus.Enabled = true;
                if(CBStatus.SelectedIndex != -1)
                {
                    CBStatus_SelectedIndexChanged(sender,new EventArgs());
                }
            }
        }

        private void CBStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView3.Rows.Clear();
            if (CByear.SelectedIndex != -1 && CBStatus.SelectedIndex != -1)
            {
                DataSet ds = example.Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[1]
                            .Replace("{TeacherNo}", TBTeacherNo.Text)
                            .Replace("{Year}", CByear.Text)+
                            "\r\n" + 
                            SQLDefault[2]
                            .Replace("{TeacherNo}", TBTeacherNo.Text));
                //ชำระแล้ว
                if (CBStatus.SelectedIndex == 0)
                {

                    for (int x = 0; x < ds.Tables[0].Rows.Count || x < ds.Tables[1].Rows.Count; x++)
                    {
                        if (ds.Tables[0].Rows.Count != 0)
                        {
                            dataGridView3.Rows.Add(ds.Tables[0].Rows[x][0].ToString(), "หุ้นสะสม", ds.Tables[0].Rows[x][1].ToString());
                        }
                        if (ds.Tables[1].Rows.Count != 0)
                        {
                            if (x + 1 <= ds.Tables[1].Rows.Count)
                                dataGridView3.Rows.Add(ds.Tables[1].Rows[x][0].ToString(), "รายการกู้ " + ds.Tables[1].Rows[x][4].ToString(), ds.Tables[1].Rows[x][7].ToString());
                        }
                    }
                }
                //ค้างชำระ
                else if (CBStatus.SelectedIndex == 1)
                {
                    //วันที่สมัคร
                    DateTime RegisterDate = Convert.ToDateTime((Convert.ToDateTime(example.Class.SQLConnection.InputSQLMSSQL(SQLDefault[5]
                        .Replace("{TeacherNo}", TBTeacherNo.Text)).Rows[0][0].ToString())).ToString("yyyy-MM-dd") );
                    //สิ้นสุดกู้
                    int lastYear = 0;
                    int lastMonth = 0;
                    if(ds.Tables[1].Rows.Count != 0)
                    {
                        lastYear = Convert.ToInt32(Convert.ToDateTime(ds.Tables[1].Rows[0][1].ToString()).ToString("yyyy"));
                        lastMonth = Convert.ToInt32(Convert.ToDateTime(ds.Tables[1].Rows[0][1].ToString()).ToString("MM"));
                    }
                    bool CheckSaving = false;
                    bool CheckLoan = false;
                    for(int Month = 1; Month <= 12; Month++)
                    {
                        for(int x = 0; x < ds.Tables[0].Rows.Count || x < ds.Tables[1].Rows.Count || x < ds.Tables[2].Rows.Count; x++)
                        {
                            //DateRegister Check
                            if (Convert.ToDateTime(Convert.ToDateTime(CByear.Text + '-' + Month + '-' + DateTime.DaysInMonth(Convert.ToInt32(CByear.Text), Month)).ToString("yyyy-MM-dd")) >= RegisterDate)
                            {
                                //Saving--------------------------------------------------------------
                                if (ds.Tables[0].Rows.Count != 0)
                                {
                                    if(Month + "/" + CByear.Text != ds.Tables[0].Rows[x][0].ToString())
                                    {
                                        CheckSaving = true;
                                    }
                                    else
                                    {
                                        CheckSaving = false;
                                        break;
                                    }
                                }

                                DateTime EndLoan = Convert.ToDateTime("1999-1-1");
                                DateTime StartLoan = Convert.ToDateTime("1999-1-1");
                                DateTime Dateloop = Convert.ToDateTime("1999-1-1");
                                //Loan ----------------------------------------------------------
                                if (ds.Tables[1].Rows.Count != 0)
                                {
                                    if(x+1 <= ds.Tables[1].Rows.Count)
                                    {
                                        try
                                        {
                                            Dateloop = Convert.ToDateTime(Convert.ToDateTime(CByear.Text + '-' + Month.ToString() + '-' + DateTime.DaysInMonth(Convert.ToInt32(CByear.Text), Month)).ToString("yyyy-MM-dd"));
                                            StartLoan = Convert.ToDateTime((Convert.ToDateTime(ds.Tables[1].Rows[x][6].ToString() + '-' + ds.Tables[1].Rows[x][5].ToString() + '-' + DateTime.DaysInMonth(Convert.ToInt32(ds.Tables[1].Rows[x][6].ToString()), Convert.ToInt32(ds.Tables[1].Rows[x][5].ToString())))).ToString("yyyy-MM-dd"));
                                            EndLoan = Convert.ToDateTime((Convert.ToDateTime(lastYear.ToString() + '-' + lastMonth.ToString() + '-' + DateTime.DaysInMonth(lastYear, lastMonth))).ToString("yyyy-MM-dd"));
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine($"==========================={ex}==========================");
                                        }
                                        if (Dateloop >= StartLoan && Dateloop <= EndLoan)
                                        {
                                            if (Month + "/" + CByear.Text != ds.Tables[1].Rows[x][0].ToString())
                                            {
                                                CheckLoan = true;
                                            }
                                            else
                                            {
                                                CheckLoan = false;
                                                break;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if(ds.Tables[2].Rows.Count != 0)
                                    {
                                        lastYear = Convert.ToInt32(Convert.ToDateTime(ds.Tables[2].Rows[0][0].ToString()).ToString("yyyy"));
                                        lastMonth = Convert.ToInt32(Convert.ToDateTime(ds.Tables[2].Rows[0][0].ToString()).ToString("MM"));
                                        try
                                        {
                                            Dateloop = Convert.ToDateTime(Convert.ToDateTime(CByear.Text + '-' + Month.ToString() + '-' + DateTime.DaysInMonth(Convert.ToInt32(CByear.Text), Month)).ToString("yyyy-MM-dd"));
                                            StartLoan = Convert.ToDateTime((Convert.ToDateTime(ds.Tables[2].Rows[0][5].ToString() + '-' + ds.Tables[2].Rows[0][4].ToString() + '-' + DateTime.DaysInMonth(Convert.ToInt32(ds.Tables[2].Rows[0][5].ToString()), Convert.ToInt32(ds.Tables[2].Rows[0][4].ToString())))).ToString("yyyy-MM-dd"));
                                            EndLoan = Convert.ToDateTime((Convert.ToDateTime(lastYear.ToString() + '-' + lastMonth.ToString() + '-' + DateTime.DaysInMonth(lastYear, lastMonth))).ToString("yyyy-MM-dd"));
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine($"==========================={ex}==========================");
                                        }
                                        if (Dateloop >= StartLoan && Dateloop <= EndLoan)
                                        {
                                            if (Convert.ToDateTime(Convert.ToDateTime(CByear.Text + '-' + Month + '-' + DateTime.DaysInMonth(Convert.ToInt32(CByear.Text), Month)).ToString("yyyy-MM-dd")) != Convert.ToDateTime((Convert.ToDateTime(ds.Tables[2].Rows[0][0].ToString())).ToString()))
                                                dataGridView3.Rows.Add(Month + "/" + CByear.Text, "รายการกู้" + ds.Tables[2].Rows[0][3].ToString(), ds.Tables[2].Rows[0][1].ToString());
                                            else
                                                dataGridView3.Rows.Add(Month + "/" + CByear.Text, "รายการกู้" + ds.Tables[2].Rows[0][3].ToString(), ds.Tables[2].Rows[0][2].ToString());
                                        }
                                    }
                                    else
                                    {
                                        CheckLoan = false;
                                    }
                                }
                            }
                        }
                        if(CheckSaving)
                        {
                            dataGridView3.Rows.Add(Month + "/" + CByear.Text, "หุ้นสะสม", "500");
                            CheckSaving = false;
                        }
                        if (CheckLoan)
                        {
                            if (Convert.ToDateTime(Convert.ToDateTime(CByear.Text + '-' + Month + '-' + DateTime.DaysInMonth(Convert.ToInt32(CByear.Text), Month)).ToString("yyyy-MM-dd")) != Convert.ToDateTime((Convert.ToDateTime(ds.Tables[1].Rows[0][1].ToString())).ToString()))
                                dataGridView3.Rows.Add(Month + "/" + CByear.Text, "รายการกู้" + ds.Tables[1].Rows[0][4].ToString(),ds.Tables[1].Rows[0][2].ToString());
                            else
                            {
                                int Balance = 0;
                                try
                                {
                                    Balance = Convert.ToInt32(ds.Tables[1].Rows[0][3].ToString());
                                }
                                catch
                                {
                                    Balance = Convert.ToInt32(Decimal.Truncate(Convert.ToDecimal(Convert.ToDouble(ds.Tables[1].Rows[0][3].ToString()))) + 1);
                                }
                                dataGridView3.Rows.Add(Month + "/" + CByear.Text, "รายการกู้" + ds.Tables[1].Rows[0][4].ToString(),Balance);
                            }
                                
                            CheckSaving = false;
                        }
                        if(ds.Tables[0].Rows.Count == 0)
                        {
                            dataGridView3.Rows.Add(Month + "/" + CByear.Text, "หุ้นสะสม", "500");
                            CheckSaving = false;
                        }
                    }
                }
                if (dataGridView3.Rows.Count == 0 && CByear.SelectedIndex != -1)
                {
                    MessageBox.Show("ไม่พบรายการ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void BTClean_Click(object sender, EventArgs e)
        {
            CByear.SelectedIndex = -1;
            CBStatus.SelectedIndex = -1;
            dataGridView3.Rows.Clear();
            TBTeacherNo.Clear();
            TBTeacherName.Clear();
            TBTeacherBill.Clear();
        }

        private void TBTeacherNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView3_MouseClick(object sender, MouseEventArgs e)
        {
        }
    }
}

