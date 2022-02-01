using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static BankTeacher.Class.ProtocolSharing.ConnectSMB;

namespace BankTeacher.Bank.Loan
{
    public partial class loan : Form
    {
        //------------------------- index -----------------
        Bitmap bmp;
        //
        string name = "", id = "";
        int StatusBoxFile = 0;
        String imgeLocation = "";
        int Check = 0;
        int DefaultEdit = 0;
        public static int SelectIndexRowDelete;
        DialogResult CheckLimitLoan = DialogResult.No;
        bool CheckBReset = false;

        //----------------------- index code -------------------- ////////
        public loan()
        {
            InitializeComponent();
        }

        //------------------------- FormSize -----------------
        // Comment!
        private void Loan_SizeChanged(object sender, EventArgs e)
        {
            Class.FromSettingMedtod.ChangeSizePanal(this, panel1);
        }

        //----------------------- End code -------------------- ////////

        /// <summary> 
        /// SQLDafault
        /// <para>[0] SELECT TeacherName Data INPUT:{TeacherNo} , {TeacherNoNotLike} </para>
        /// <para>[1] SELECT Guarantor Credit Limit INPUT:{Text} , {TeacherNoNotLike} , {RemainAmount}</para>
        /// <para>[2] SELECT Date Data </para>
        /// <para>[3] INSERT Loan and Get LoanNo INPUT: {TeacherNoAdd}, {TeacherNo}, {MonthPay}, {YearPay}, {LoanAmount}, {PayNo}, {InterestRate}</para>
        /// <para>[4] INSERT Guarantor INPUT: {LoanNo},{TeacherNo},{Amount},{RemainsAmount}</para>
        /// <para>[5] Detail Loan Print  INPUT: TeacherNo</para>
        /// <para>[6] SELECT MemberLona  INPUT: {TeacherNo} </para>
        /// <para>[7] Delete Loan INPUT: NO </para>
        /// <para>[8] Check Dividend Year INPUT: </para>
        /// <para>[9] BSearch Teacher INPUT: {Text}  {TeacherNoNotLike}</para>
        /// <para>[10] SELECT CreditLimit Data (DGV) INPUT:{Text} , {TeacherNoNotLike} , {RemainAmount}</para>
        /// <para>[11] Get MinLoan INPUT: -</para>
        /// </summary>
        private String[] SQLDefault = new String[]
        {
            //[0] SELECT TeacherName Data INPUT:{TeacherNo}  
            "SELECT a.TeacherNo, CAST(ISNULL(c.PrefixNameFull,'') + b.Fname + ' ' + b.Lname as nvarchar) ,SavingAmount \r\n " +
            "FROM EmployeeBank.dbo.tblMember as a  \r\n " +
            "LEFT JOIN Personal.dbo.tblTeacherHis as b on a.TeacherNo = b.TeacherNo  \r\n " +
            "LEFT JOIN  BaseData.dbo.tblPrefix as c on b.PrefixNo = c.PrefixNo  \r\n " +
            "LEFT JOIN EmployeeBank.dbo.tblShare as d ON a.TeacherNo = d.TeacherNo \r\n " +
            "WHERE a.TeacherNo LIKE 'T{TeacherNo}%' and a.MemberStatusNo = 1 {TeacherNoNotLike}\r\n " +
            "ORDER BY b.Fname; "

            , 

            //[1] SELECT CreditLimit Data INPUT:{Text} , {TeacherNoNotLike} , {RemainAmount}
           "SELECT TOP(20)TeacherNo, Name, RemainAmount, ISNULL(a.LoanStatusNo , 0) as LoanS  \r\n " + 
          " FROM (SELECT a.TeacherNo , CAST(ISNULL(c.PrefixName,'')+' '+Fname +' '+ Lname as NVARCHAR)AS Name,   \r\n " + 
          " ROUND(ISNULL(e.SavingAmount,0) - ISNULL(SUM(d.RemainsAmount),0),0,1) as RemainAmount, Fname , f.LoanStatusNo  \r\n " + 
          " FROM EmployeeBank.dbo.tblMember as a    \r\n " + 
          " LEFT JOIN (  \r\n " + 
          " SELECT TeacherNo , Fname , Lname , PrefixNo  \r\n " + 
          " FROM Personal.dbo.tblTeacherHis   \r\n " + 
          " ) as b ON a.TeacherNo = b.TeacherNo    \r\n " + 
          " LEFT JOIN BaseData.dbo.tblPrefix as c ON b.PrefixNo = c.PrefixNo    \r\n " + 
          " LEFT JOIN EmployeeBank.dbo.tblGuarantor as d on a.TeacherNo = d.TeacherNo   \r\n " + 
          " LEFT JOIN EmployeeBank.dbo.tblShare as e ON e.TeacherNo = a.TeacherNo   \r\n " + 
          " LEFT JOIN (SELECT TeacherNo , LoanStatusNo \r\n " + 
          " FROM EmployeeBank.dbo.tblLoan  \r\n " + 
          " WHERE LoanStatusNo = 1 or LoanStatusNo = 2 GROUP BY TeacherNo , LoanStatusNo) as f on a.TeacherNo = f.TeacherNo  \r\n " +
          " WHERE (a.TeacherNo = '{Text}') and a.MemberStatusNo = 1  \r\n " + 
          " GROUP BY a.TeacherNo , CAST(ISNULL(c.PrefixName,'')+' '+Fname +' '+ Lname as NVARCHAR), e.SavingAmount, Fname, f.LoanStatusNo) as a   \r\n " +
          " WHERE {RemainAmount}  {TeacherNoNotLike}\r\n " + 
          " ORDER BY a.Fname; "

            , 

            //[2] SELECT Date Data
            "SELECT CAST(CURRENT_TIMESTAMP as DATE); \r\n\r\n"
            ,

            //[3] INSERT Loan and Get LoanNo INPUT: {TeacherNoAdd}, {TeacherNo}, {MonthPay}, {YearPay}, {LoanAmount}, {PayNo}, {InterestRate}
            "DECLARE @LoanNo INT;\r\n" +
            "INSERT INTO EmployeeBank.dbo.tblLoan\r\n" +
            "(TeacherNoAddBy, TeacherNo, MonthPay, YearPay, LoanAmount, PayNo, InterestRate, DateAdd)\r\n" +
            "VALUES ('{TeacherNoAdd}', '{TeacherNo}', {MonthPay}, {YearPay}, {LoanAmount}, {PayNo}, {InterestRate},CURRENT_TIMESTAMP);\r\n" +
            "SET @LoanNo = SCOPE_IDENTITY();\r\n" +
            "SELECT LoanNo\r\n" +
            "FROM EmployeeBank.dbo.tblLoan\r\n" +
            "WHERE LoanNo = @LoanNo;\r\n"
            ,

            //[4]INSERT Guarantor INPUT: {LoanNo},{TeacherNo},{Amount},{RemainsAmount}
            "INSERT INTO EmployeeBank.dbo.tblGuarantor (LoanNo,TeacherNo,Amount,RemainsAmount)\r\n" +
            "VALUES ('{LoanNo}','{TeacherNo}','{Amount}','{RemainsAmount}');\r\n"
            ,
            //[5] Detail Loan Print  INPUT: TeacherNo
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
            "WHERE a.TeacherNo = '{TeacherNo}' "
            ,
             //[6] SELECT MemberLona  INPUT: {TeacherNo}
            "SELECT a.TeacherNo,CAST(ISNULL(c.PrefixName+' ','')+b.Fname+' '+b.Lname as NVARCHAR),d.StartAmount  \r\n "+
            "FROM EmployeeBank.dbo.tblLoan as a  \r\n "+
            "LEFT JOIN Personal.dbo.tblTeacherHis as b on a.TeacherNo = b.TeacherNo  \r\n "+
            "LEFT JOIN BaseData.dbo.tblPrefix as c on b.PrefixNo = c.PrefixNo  \r\n "+
            "LEFT JOIN EmployeeBank.dbo.tblMember as d on a.TeacherNo = d.TeacherNo \r\n "+
            "WHERE a.TeacherNo LIKE 'T{TeacherNo}%'  \r\n "+
            "ORDER BY b.Fname;"
            ,
            //[7] Delete Loan INPUT: NO
            "DELETE FROM EmployeeBank.dbo.tblLoan;\r\n" +
            "DELETE FROM EmployeeBank.dbo.tblGuarantor\r\n"
            ,
            //[8] Check Dividend Year INPUT: 
           "SELECT TOP 1 ISNULL(MAX(a.Year) + 1 , 0) \r\n " +
          "FROM EmployeeBank.dbo.tblDividend as a  \r\n " +
          "WHERE a.Cancel = 1 ;"
           ,

           //[9] BSearch Teacher INPUT: {Text}  {TeacherNoNotLike}
           "SELECT TOP(20)TeacherNo, Name, RemainAmount \r\n " +
          "  FROM (SELECT a.TeacherNo , CAST(ISNULL(c.PrefixName,'')+' '+Fname +' '+ Lname as NVARCHAR)AS Name,    \r\n " +
          "  ROUND(ISNULL(e.SavingAmount,0) - ISNULL(SUM(d.RemainsAmount),0),0,1) as RemainAmount, Fname   \r\n " +
          "  FROM EmployeeBank.dbo.tblMember as a     \r\n " +
          "  LEFT JOIN (   \r\n " +
          "  SELECT TeacherNo , Fname , Lname , PrefixNo   \r\n " +
          "  FROM Personal.dbo.tblTeacherHis    \r\n " +
          "  ) as b ON a.TeacherNo = b.TeacherNo     \r\n " +
          "  LEFT JOIN BaseData.dbo.tblPrefix as c ON b.PrefixNo = c.PrefixNo     \r\n " +
          "  LEFT JOIN EmployeeBank.dbo.tblGuarantor as d on a.TeacherNo = d.TeacherNo    \r\n " +
          "  LEFT JOIN EmployeeBank.dbo.tblShare as e ON e.TeacherNo = a.TeacherNo    \r\n " +
          "  LEFT JOIN (SELECT TeacherNo  \r\n " +
          "  FROM EmployeeBank.dbo.tblLoan   \r\n " +
          "  WHERE LoanStatusNo = 1 or LoanStatusNo = 2 GROUP BY TeacherNo) as f on a.TeacherNo = f.TeacherNo   \r\n " +
          "  WHERE (a.TeacherNo LIKE '%{Text}%' or CAST(ISNULL(c.PrefixName,'')+' '+[Fname] +' '+ [Lname] as NVARCHAR) LIKE '%{Text}%') and a.MemberStatusNo = 1   \r\n " +
          "  GROUP BY a.TeacherNo , CAST(ISNULL(c.PrefixName,'')+' '+Fname +' '+ Lname as NVARCHAR), e.SavingAmount, Fname ) as a    \r\n " +
          "  WHERE RemainAmount IS NOT NULL {TeacherNoNotLike} \r\n " +
          "  GROUP BY TeacherNo, Name, RemainAmount ,a.Fname \r\n " +
          "  ORDER BY a.Fname; "
           ,
           //[10] SELECT CreditLimit Data (DGV) INPUT:{Text} , {TeacherNoNotLike} , {RemainAmount}
           "SELECT TOP(20)TeacherNo, Name, RemainAmount, ISNULL(a.LoanStatusNo , 0) as LoanS  \r\n " +
          " FROM (SELECT a.TeacherNo , CAST(ISNULL(c.PrefixName,'')+' '+Fname +' '+ Lname as NVARCHAR)AS Name,   \r\n " +
          " ROUND(ISNULL(e.SavingAmount,0) - ISNULL(SUM(d.RemainsAmount),0),0,1) as RemainAmount, Fname , f.LoanStatusNo  \r\n " +
          " FROM EmployeeBank.dbo.tblMember as a    \r\n " +
          " LEFT JOIN (  \r\n " +
          " SELECT TeacherNo , Fname , Lname , PrefixNo  \r\n " +
          " FROM Personal.dbo.tblTeacherHis   \r\n " +
          " ) as b ON a.TeacherNo = b.TeacherNo    \r\n " +
          " LEFT JOIN BaseData.dbo.tblPrefix as c ON b.PrefixNo = c.PrefixNo    \r\n " +
          " LEFT JOIN EmployeeBank.dbo.tblGuarantor as d on a.TeacherNo = d.TeacherNo   \r\n " +
          " LEFT JOIN EmployeeBank.dbo.tblShare as e ON e.TeacherNo = a.TeacherNo   \r\n " +
          " LEFT JOIN (SELECT TeacherNo , LoanStatusNo \r\n " +
          " FROM EmployeeBank.dbo.tblLoan  \r\n " +
          " WHERE LoanStatusNo = 1 or LoanStatusNo = 2 GROUP BY TeacherNo , LoanStatusNo) as f on a.TeacherNo = f.TeacherNo  \r\n " +
          " WHERE (a.TeacherNo LIKE '%{Text}%' or CAST(ISNULL(c.PrefixName,'')+' '+Fname +' '+ Lname as NVARCHAR) LIKE '%{Text}%') and a.MemberStatusNo = 1  \r\n " +
          " GROUP BY a.TeacherNo , CAST(ISNULL(c.PrefixName,'')+' '+Fname +' '+ Lname as NVARCHAR), e.SavingAmount, Fname, f.LoanStatusNo) as a   \r\n " +
          " WHERE {RemainAmount}  {TeacherNoNotLike}\r\n " +
          " ORDER BY a.Fname; "

            ,

           //[11] Get MinLoan INPUT: -
           "SELECT MinLoan \r\n " +
          "FROM EmployeeBank.dbo.tblSettingAmount;"
           ,

        };

        //----------------------- PullSQLDate -------------------- ////////
        // ดึงขอมูลวันที่จากฐานข้อมูล
        int Month;
        private void Loan_Load(object sender, EventArgs e)
        {
            int Year = Convert.ToInt32(BankTeacher.Bank.Menu.Date[0]);
            Month = Convert.ToInt32(BankTeacher.Bank.Menu.Date[1]);

            DataSet DividendCheckYear = Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[8]);
            if (Year == Convert.ToInt32(DividendCheckYear.Tables[0].Rows[0][0].ToString()))
            {
                Year++;
                Month = 1;
            }

            for (int Num = 0; Num < 2; Num++)
            {
                CBPayYear.Items.Add(Year);
                Year++;
            }

            for (int a = Month; a <= 12; a++)
            {
                CBPayMonth.Items.Add(a);
            }

            this.DGVGuarantorCredit.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(DGVGuarantorCredit_EditingControlShowing);
        }
        //----------------------- End code -------------------- ////////

        //----------------------- INSERTSQLLoan -------------------- ////////
        // ส่งข้อมูลการกู้ขึ้นไปเก็บบนฐานข้อมูล
        private void BSave_Click(object sender, EventArgs e)
        {
            int SumPercentGuarantor = 0;
            bool CheckMinus = true;
            for (int Num = 0; Num < DGVGuarantorCredit.Rows.Count; Num++)
            {
                if (Double.TryParse(DGVGuarantorCredit.Rows[Num].Cells[3].Value.ToString(), out Double CreditPercent))
                {
                    SumPercentGuarantor += Convert.ToInt32(CreditPercent);
                    if(CreditPercent <= 0)
                    {
                        CheckMinus = false;
                        break;
                    }
                }

            }
            String AmountLimit = LLoanAmount.Text.Remove(0, 1);
            AmountLimit = AmountLimit.Remove(AmountLimit.Length - 1);
            if (TBTeacherNo.Text != "" && CBPayMonth.SelectedIndex != -1 && CBPayYear.SelectedIndex != -1 &&
                TBLoanAmount.Text != "" && TBPayNo.Text != "" && TBInterestRate.Text != "" && (DGVGuarantor.Rows.Count <= 4 && DGVGuarantor.Rows.Count != 0) && ((int.Parse(TBLoanAmount.Text) <= int.Parse(AmountLimit)) || UserOutCreditLimit == DialogResult.Yes) &&
                Convert.ToInt32(LLackAmount.Text) == 0 && Convert.ToInt32(LOutCredit.Text) == 0 && Int32.TryParse(TBLoanAmount.Text, out int x ) && x >= BankTeacher.Bank.Menu.MinLoan && CheckMinus == true
                && MessageBox.Show("ยืนยันการบันทึกหรือไม่","ระบบ",MessageBoxButtons.YesNo,MessageBoxIcon.Information) == DialogResult.Yes)
            {

                DataSet dt = Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[3]
                    .Replace("{TeacherNoAdd}", Class.UserInfo.TeacherNo)
                    .Replace("{TeacherNo}", TBTeacherNo.Text)
                    .Replace("{MonthPay}", CBPayMonth.SelectedItem.ToString())
                    .Replace("{YearPay}", CBPayYear.Text)
                    .Replace("{LoanAmount}", TBLoanAmount.Text)
                    .Replace("{PayNo}", TBPayNo.Text)
                    .Replace("{InterestRate}", TBInterestRate.Text));
                String LoanNo = dt.Tables[0].Rows[0][0].ToString();

                for (int Num = 0; Num < DGVGuarantorCredit.Rows.Count; Num++)
                {

                    Class.SQLConnection.InputSQLMSSQL(SQLDefault[4]
                        .Replace("{LoanNo}", LoanNo.ToString())
                        .Replace("{TeacherNo}", DGVGuarantor.Rows[Num].Cells[0].Value.ToString())
                        .Replace("{Amount}", DGVGuarantorCredit.Rows[Num].Cells[3].Value.ToString())
                        .Replace("{RemainsAmount}", DGVGuarantorCredit.Rows[Num].Cells[3].Value.ToString())
                    );
                }

                MessageBox.Show("บันทึกข้อมูลเสร็จเรียบร้อยแล้ว", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                {
                    printDocument1.Print();
                }
                TBLoanNo.Text = LoanNo;
                BSave.Enabled = false;
                label15.Visible = true;
                UserOutCreditLimit = DialogResult.No;
                TBTeacherNo_KeyDown(sender, new KeyEventArgs(Keys.Back));
                TBTeacherNo.Text = "";
            }
            else if(LLackAmount.Text != "0" || LOutCredit.Text != "0")
            {
                MessageBox.Show("กรอกจำนวนเงินค้ำไม่ถูกต้อง", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControl1.SelectedIndex = 2;
            }
            else if(CBPayMonth.SelectedIndex == -1 || CBPayYear.SelectedIndex == -1)
            {
                MessageBox.Show("กรอกข้อมูลวันที่เริ่มจ่ายไม่ถูกต้อง", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControl1.SelectedIndex = 1;
            }
            else if(TBLoanAmount.Text == "")
            {
                MessageBox.Show("กรอกจำนวนเงินกู้ไม่ถูกต้อง", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControl1.SelectedIndex = 1;
                TBLoanAmount.Focus();
            }
            else if(TBInterestRate.Text == "" || Double.TryParse(TBInterestRate.Text , out Double Interest) == false)
            {
                MessageBox.Show("กรอกข้อมูลเปอเซนต์ดอกเบี้ยไม่ถูกต้อง", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControl1.SelectedIndex = 1;
                TBInterestRate.Focus();
            }
            else if(TBPayNo.Text == "" || Int32.TryParse(TBPayNo.Text,out int PayNo))
            {
                MessageBox.Show("กรอกจำนวนงวดไม่ถูกต้อง", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControl1.SelectedIndex = 1;
                TBPayNo.Focus();
            }
            else
            {
                MessageBox.Show("กรอกข้อมูลไม่ถูกต้อง", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 3 && (CBPayMonth.SelectedIndex != -1
                && CBPayYear.SelectedIndex != -1 && TBPayNo.Text != "" && TBInterestRate.Text != ""))
            {
                if (TBLoanAmount.Text != "" && Convert.ToInt32(TBLoanAmount.Text) >= BankTeacher.Bank.Menu.MinLoan)
                {
                    DGVLoanDetail.Rows.Clear();
                    int Month = int.Parse(CBPayMonth.Text),
                        Year = int.Parse(CBPayYear.Text);

                    Double Interest = (Convert.ToDouble(TBLoanAmount.Text) * (Convert.ToDouble(TBInterestRate.Text) / 100)) / Convert.ToDouble(TBPayNo.Text);

                    int Pay = Convert.ToInt32(TBLoanAmount.Text) / Convert.ToInt32(TBPayNo.Text);
                    int SumInstallment = Convert.ToInt32(Pay + Interest);
                    int SumCheckInterest = 0;


                    for (int Num = 0; Num < int.Parse(TBPayNo.Text); Num++)
                    {
                        if (Month > 12)
                        {
                            Month = 1;
                            Year++;
                        }
                        
                        if(SumCheckInterest > Convert.ToInt32((Convert.ToDouble(TBLoanAmount.Text) * (Convert.ToDouble(TBInterestRate.Text) / 100))))
                        {
                            Interest = SumCheckInterest - Convert.ToInt32((Convert.ToDouble(TBLoanAmount.Text) * (Convert.ToDouble(TBInterestRate.Text) / 100)));
                        }
                        else if (SumCheckInterest == Convert.ToInt32((Convert.ToDouble(TBLoanAmount.Text) * (Convert.ToDouble(TBInterestRate.Text) / 100))))
                        {
                            Interest = 0;
                        }
                        if (Num == Convert.ToInt32(TBPayNo.Text) - 1)
                        {
                            //Interest = Convert.ToInt32((Convert.ToDouble(TBLoanAmount.Text) * (Convert.ToDouble(TBInterestRate.Text) / 100)) - (Convert.ToInt32(Interest) * Num));
                            Interest =(Convert.ToInt32((Convert.ToDouble(TBLoanAmount.Text) * (Convert.ToDouble(TBInterestRate.Text) / 100)))) - SumCheckInterest;
                            Pay = Pay * Num;
                            Pay = Convert.ToInt32(TBLoanAmount.Text) - Pay;
                            SumInstallment = Convert.ToInt32(Pay + Interest);
                        }
                        DGVLoanDetail.Rows.Add($"{Year}/{Month}", Pay, Convert.ToInt32(Interest), SumInstallment);
                        Month++;
                        SumCheckInterest += Convert.ToInt32(Interest);
                    }
                    
                }
                else
                {
                    tabControl1.SelectedIndex = 1;
                    TBLoanAmount.Focus();
                }

            }
            if(tabControl1.SelectedIndex >= 2)
            {
                try
                {
                    if(Convert.ToInt32(TBLoanAmount.Text) < BankTeacher.Bank.Menu.MinLoan && TBLoanAmount.Text == "")
                    {
                        tabControl1.SelectedIndex = 1;
                        TBLoanAmount.Focus();
                    }
                }
                catch
                {
                    tabControl1.SelectedIndex = 1;
                    TBLoanAmount.Focus();

                }

            }
        }
        // เลือกเดือนจ่าย
        private void CBPayMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            int LoanAmount = 0;
            bool Check = false, CheckLimit = true;
            if (TBLoanAmount.Text != "")
                Check = int.TryParse(TBLoanAmount.Text, out LoanAmount);

            String AmountLimit = LLoanAmount.Text.Remove(0, 1);
            AmountLimit = AmountLimit.Remove(AmountLimit.Length - 1);
            int NumAmountLimit;
            CheckLimit = int.TryParse(AmountLimit, out NumAmountLimit);
            if (Check && CheckLimit && DGVGuarantor.Rows.Count == 4)
            {
                if (CBPayYear.SelectedIndex != -1 && (LoanAmount > int.Parse(AmountLimit)))
                {
                    DialogResult result = MessageBox.Show("วงเงินกู้เกินกำหนดการค้ำ ต้องการทำต่อหรือไม่", "แจ้งเตือน", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.No)
                    {
                        TBLoanAmount.Text = "";
                        TBLoanAmount.Focus();
                    }
                }
                else if (CBPayYear.SelectedIndex != -1 && (LoanAmount < 1))
                {
                    MessageBox.Show("ใส่จำนวนเงินไม่ถูกต้อง", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    TBLoanAmount.Text = "";
                    TBLoanAmount.Focus();
                }
            }

        }
        // เลือกปีจ่าย
        private void CBPayYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            int LoanAmount = 0;

            bool Check = false, CheckLimit = true;
            String AmountLimit = LLoanAmount.Text.Remove(0, 1);
            AmountLimit = AmountLimit.Remove(AmountLimit.Length - 1);
            int NumAmountLimit;
            CheckLimit = int.TryParse(AmountLimit, out NumAmountLimit);
            if (TBLoanAmount.Text != "")
                Check = int.TryParse(TBLoanAmount.Text, out LoanAmount);

            if (Check && CheckLimit && DGVGuarantor.Rows.Count == 4)
            {
                if (CBPayMonth.SelectedIndex != -1 && (LoanAmount > int.Parse(AmountLimit)))
                {
                    DialogResult result = MessageBox.Show("วงเงินกู้เกินกำหนดการค้ำ ต้องการทำต่อหรือไม่", "แจ้งเตือน", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.No)
                    {
                        TBLoanAmount.Text = "";
                        TBLoanAmount.Focus();
                    }

                }
                else if (CBPayMonth.SelectedIndex != -1 && (LoanAmount < 1))
                {
                    MessageBox.Show("ใส่จำนวนเงินไม่ถูกต้อง", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    TBLoanAmount.Text = "";
                    TBLoanAmount.Focus();
                }
            }

            if (CBPayYear.SelectedIndex > 0)
            {
                CBPayMonth.Items.Clear();
                for (int Num = 1; Num <= 12; Num++)
                {
                    CBPayMonth.Items.Add(Num);
                }
                CBPayMonth.SelectedIndex = -1;
            }
            else
            {
                CBPayMonth.Items.Clear();
                for (int Num = Month; Num <= 12; Num++)
                {
                    CBPayMonth.Items.Add(Num);
                }
            }
        }
        //กดปุ่มคนหาอาจารย์ที่จะกู้จาก DGV
        private void BSearchTeacher_Click(object sender, EventArgs e)
        {
            try
            {
                Bank.Search IN;
                String NotLike = "";
                if (DGVGuarantor.Rows.Count != 0)
                {
                    for (int Num = 0; Num < DGVGuarantor.Rows.Count; Num++)
                    {
                        NotLike += " and a.TeacherNo NOT LIKE " + $"'{DGVGuarantor.Rows[Num].Cells[0].Value.ToString()}'";
                    }
                }
                IN = new Bank.Search(SQLDefault[9]
                        .Replace("{TeacherNoNotLike}", NotLike), "หุ้นสะสม");

                IN.ShowDialog();
                if (Bank.Search.Return[0] != "" /*&& CheckLimitLoan == DialogResult.No*/)
                {
                    TBTeacherNo.Text = Bank.Search.Return[0];
                    TBTeacherNo_KeyDown(sender, new KeyEventArgs(Keys.Enter));
                    tabControl1.SelectedIndex = 0;
                }
            }
            catch (Exception x)
            {
                Console.WriteLine(x);
            }
        }
        //TB ใส่ ID คนกู้ มี event การกด
        private void TBTeacherNo_KeyDown(object sender, KeyEventArgs e)
        {
            TBTeacherNo.Text = TBTeacherNo.Text.Replace("t", "T");
            if (BSave.Enabled == false)
                BSave.Enabled = true;
            if (e.KeyCode == Keys.Enter)
            {
                 DataTable dt = Class.SQLConnection.InputSQLMSSQL(SQLDefault[1].Replace("{Text}", TBTeacherNo.Text)
                    .Replace("{TeacherNoNotLike}", "")
                    .Replace("{RemainAmount}" , "RemainAmount IS NOT NULL"));
                if (dt.Rows.Count != 0)
                {
                    DialogResult RemainAmount = DialogResult.Yes;
                    if(Convert.ToInt32(dt.Rows[0][2].ToString()) < 500)
                    {
                        RemainAmount = MessageBox.Show("สมาชิกนี้มีเงินหุ้นสะสมลบกับเงินค้ำต่ำกว่าเกณท์การกู้ ต้องการจะกู้ต่อหรือไม่", "แจ้งเตือน", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    }
                    if ((int.Parse(dt.Rows[0][3].ToString()) == 2 || int.Parse(dt.Rows[0][3].ToString()) == 1) && RemainAmount == DialogResult.Yes)
                    {
                        CheckLimitLoan = MessageBox.Show("ผู้ใช้นี้มียอดกู้อยู่ในระบบ ต้องการจะกู้ต่อหรือไม่\r\n", "ระบบ", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (CheckLimitLoan == DialogResult.Yes)
                        {
                            TBTeacherName.Text = dt.Rows[0][1].ToString();
                            TBLoanNo.Text = "-";
                            TBLoanStatus.Text = "ดำเนินการ";
                            TBSavingAmount.Text = dt.Rows[0][2].ToString();

                            String[] Credit = new string[] { };
                            Credit = dt.Rows[0][2].ToString().Split('.');
                            // float Percent = 100 / DGVGuarantor.Rows.Count;
                            // ======= Tab 1 Clear ===============
                            TBLoanAmount.Text = "";
                            TBSavingAmount.Text = Credit[0];
                            TBGuarantorNo.Focus();
                            DGVGuarantor.Rows.Clear();
                            DGVGuarantor.Rows.Add(dt.Rows[0][0], dt.Rows[0][1], Credit[0]);
                            // ======= Tab 2 Clear ===============
                            CBPayMonth.SelectedIndex = -1;
                            CBPayYear.SelectedIndex = -1;
                            // ======= Tab 3 Clear ===============
                            // ======= Tab 4 Clear ===============
                            DGVLoanDetail.Rows.Clear();
                            Check = 1;
                            Checkmember(false);
                        }
                        else
                        {
                            TBTeacherNo.Text = "";
                        }
                    }
                    else if (RemainAmount == DialogResult.Yes)
                    {
                        TBTeacherName.Text = dt.Rows[0][1].ToString();
                        TBLoanNo.Text = "-";
                        TBLoanStatus.Text = "ดำเนินการ";
                        TBSavingAmount.Text = dt.Rows[0][2].ToString();

                        String[] Credit = new string[] { };
                        Credit = dt.Rows[0][2].ToString().Split('.');
                        // float Percent = 100 / DGVGuarantor.Rows.Count;
                        DGVLoanDetail.Rows.Clear();
                        DGVGuarantor.Rows.Clear();
                        DGVGuarantorCredit.Rows.Clear();
                        DGVGuarantor.Rows.Add(dt.Rows[0][0], dt.Rows[0][1], Credit[0]);
                        TBLoanAmount.Text = "";
                        CBPayMonth.SelectedIndex = -1;
                        CBPayYear.SelectedIndex = -1;
                        TBSavingAmount.Text = Credit[0];
                        tabControl1.SelectedIndex = 0;
                        TBGuarantorNo.Focus();
                        Check = 1;

                    }
                    else if (RemainAmount == DialogResult.No)
                        TBTeacherNo.Text = "";
                }
                else
                {
                    MessageBox.Show("รหัสไม่ถูกต้อง หรือยอดเงินที่ค้ำได้ไม่เพียงพอ \r\n", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    TBTeacherNo.Text = "";
                    TBTeacherNo.Focus();

                }

            }
            else if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back) 
            {
                if (Check == 1)
                {
                    Checkmember(true);
                    // ======= Tab 1 Clear ===============
                    CheckBReset = false;
                    TBLoanAmount.Text = "";
                    TBTeacherName.Text = "";
                    TBLoanNo.Text = "";
                    TBLoanStatus.Text = "";
                    TBSavingAmount.Text = "";
                    DGVGuarantor.Rows.Clear();
                    // ======= Tab 2 Clear ===============
                    CBPayMonth.SelectedIndex = -1;
                    CBPayYear.SelectedIndex = -1;
                    DGVGuarantorCredit.Rows.Clear();
                    // ======= Tab 3 Clear ===============
                    DGVGuarantorCredit.Rows.Clear();
                    // ======= Tab 4 Clear ===============
                    DGVLoanDetail.Rows.Clear();
                    Check = 0;
                }

            }
            else if (e.KeyCode == Keys.Escape)
            {
                TBTeacherNo.Focus();
            }
        }
        //TB คำค้ำ event กด
        private void TBGuarantorNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                String NotLike = "";

                if (DGVGuarantor.Rows.Count < 4) /*& (CheckTeacherNo == false)*/
                {

                    for (int Num = 0; Num < DGVGuarantor.Rows.Count; Num++)
                    {
                        NotLike += " and a.TeacherNo NOT LIKE " + $"'{DGVGuarantor.Rows[Num].Cells[0].Value.ToString()}'";
                    }

                    DataTable dtRemainAmount = Class.SQLConnection.InputSQLMSSQL(
                        SQLDefault[1]
                        .Replace("{Text}", TBGuarantorNo.Text)
                        .Replace("{TeacherNoNotLike}", NotLike)
                        .Replace("{RemainAmount}" , $"RemainAmount >= {Bank.Menu.MinLoan}"));
                    if (dtRemainAmount.Rows.Count != 0)
                    {
                        String[] Num = new string[] { };
                        Num = dtRemainAmount.Rows[0][2].ToString().Split('.');
                        DGVGuarantor.Rows.Add(dtRemainAmount.Rows[0][0].ToString(),dtRemainAmount.Rows[0][1].ToString(),Convert.ToInt32(Num[0]));
                    }
                    else
                    {
                        MessageBox.Show("ไม่มีข้อมูล หรือไม่มียอดเงินที่ค้ำได้", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else if (DGVGuarantor.Rows.Count >= 4)
                {
                    MessageBox.Show("ผู้ค้ำเกินกหนด", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                TBGuarantorNo.Text = "";
            }
        }
        // อีเว้นตัวเลข ในTB
        private void TBLoanAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!Char.IsNumber(e.KeyChar)) && (!Char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }
        // อีเว้นตัวเลข ในTB
        private void TBPayNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!Char.IsNumber(e.KeyChar)) && (!Char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }
        // อีเว้นตัวเลข ในTB
        private void TBInterestRate_KeyPress(object sender, KeyPressEventArgs e)
        {

            if ((!Char.IsNumber(e.KeyChar)) && (!Char.IsControl(e.KeyChar)) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }
        // อัพเอกสารส่ง เซิร์ฟเวอร์
        //private void BTOpenfile_Click(object sender, EventArgs e)
        //{
        //    if (StatusBoxFile == 0)
        //    {

        //        try
        //        {
        //            OpenFileDialog dialog = new OpenFileDialog();
        //            dialog.Filter = "pdf files(*.pdf)|*.pdf";
        //            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        //            {
        //                imgeLocation = dialog.FileName;
        //            }
        //            if (imgeLocation != "")
        //            {
                       
        //                StatusBoxFile = 1;
                
        //            }

        //        }
        //        catch
        //        {
        //            MessageBox.Show("An Error Occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }
        //    }
        //    else if (StatusBoxFile == 1)
        //    {
        //        var smb = new SmbFileContainer("Loan");
        //        if (smb.IsValidConnection())
        //        {
        //            String Return = smb.SendFile(imgeLocation, "Loan_" + TBTeacherNo.Text + ".pdf");
        //            MessageBox.Show(Return, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            StatusBoxFile = 0;
                 
        //            imgeLocation = "";
        //        }
        //        else
        //        {
        //            MessageBox.Show("ไม่สามารถสร้างไฟล์ในที่นั้นได้", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        }
        //    }
        //}
        // กระดาษปริ้น
        private void printDocument1_PrintPage_1(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
                Class.Print.PrintPreviewDialog.PrintLoan(e, SQLDefault[5].Replace("{TeacherNo}", TBTeacherNo.Text), 
                    Bank.Menu.Date[2],Bank.Menu.Monthname, (Convert.ToInt32(Bank.Menu.Date[0]) + 543).ToString(), 
                    TBTeacherNo.Text, TBLoanNo.Text,DGVGuarantor.RowCount,true,true);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("เดี๋ยวใส่ตอนรวมโปรแกรมครับ", "System", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading, true);
        }

        private void DGVGuarantor_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int currentMouseOverRow = DGVGuarantor.HitTest(e.X, e.Y).RowIndex;
                if (currentMouseOverRow > 0)
                {
                    SelectIndexRowDelete = currentMouseOverRow;
                    ContextMenu m = new ContextMenu();
                    m.MenuItems.Add(new MenuItem("ลบออก"));
                    m.Show(DGVGuarantor, new Point(e.X, e.Y));
                    m.MenuItems[0].Click += new System.EventHandler(this.Delete_Click);
                }
            }
        }
        private void Delete_Click(object sender, EventArgs e)
        {
            if (SelectIndexRowDelete > 0)
            {
                DGVGuarantor.Rows.RemoveAt(SelectIndexRowDelete);
                SelectIndexRowDelete = -1;

            }
            if (TBLoanAmount.Text != "")
            {
                String AmountLimit = LLoanAmount.Text.Remove(0, 1);
                AmountLimit = AmountLimit.Remove(AmountLimit.Length - 1);
                if (Convert.ToInt32(TBLoanAmount.Text) > Convert.ToInt32(AmountLimit))
                {
                    TBLoanAmount_Leave(sender, new EventArgs());
                }
                else
                {
                    BCalculate_Click(sender, new EventArgs());
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (DGVGuarantor.Rows.Count == 0)
            {
                MessageBox.Show("โปรดเลือกผู้กู้ก่อน", "ระบบ");
                TBTeacherNo.Focus();
            }
            else
            {
                try
                {
                    //
                    Bank.Search IN;
                    String NotLike = "";
                    if (DGVGuarantor.Rows.Count != 0)
                    {
                        for (int Num = 0; Num < DGVGuarantor.Rows.Count; Num++)
                        {
                            NotLike += " and a.TeacherNo NOT LIKE " + $"'{DGVGuarantor.Rows[Num].Cells[0].Value.ToString()}' ";
                        }
                        NotLike = NotLike.Remove(NotLike.Length - 1);
                    }
                    IN = new Bank.Search(SQLDefault[10]
                           .Replace("{TeacherNoNotLike}", NotLike)
                           .Replace("{RemainAmount}", $"RemainAmount > {Bank.Menu.MinLoan}"), "หุ้นสะสม");
                    
                    IN.ShowDialog();
                    if (Bank.Search.Return[0] != "")
                    {
                        TBGuarantorNo.Text = Bank.Search.Return[0];
                        TBGuarantorNo_KeyDown(sender, new KeyEventArgs(Keys.Enter));
                    }
                }
                catch (Exception x)
                {
                    Console.WriteLine(x);
                }
            }


        }
        private void TBGuarantorNo_KeyUp(object sender, KeyEventArgs e)
        {
            if (TBTeacherName.Text == "")
            {
                MessageBox.Show("โปรดใส่ข้อมูลด้านบนให้ครบถ้วนก่อน", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TBGuarantorNo.Text = "";
                TBTeacherNo.Focus();
            }
        }
        Double CreditLoanAmount;
        private void DGVGuarantor_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            CheckBReset = false;
            CreditLoanAmount = 0;
            LGuarantorAmount.Text = DGVGuarantor.Rows.Count + "/4";

            for (int Count = 0; Count < DGVGuarantor.Rows.Count; Count++)
            {
                CreditLoanAmount += int.Parse(DGVGuarantor.Rows[Count].Cells[2].Value.ToString());
            }
            int LoanAmount = Convert.ToInt32(CreditLoanAmount - CreditLoanAmount * (Convert.ToDouble(TBInterestRate.Text) / 100));
            LLoanAmount.Text = "(" + LoanAmount.ToString() + ")";

        }
        private void Checkmember(bool tf)
        {
            TBTeacherNo.Enabled = tf;
            BSearchTeacher.Enabled = tf;
        }
        private void DGVGuarantor_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            for (int Num = 0; Num < DGVRow.Count; Num++)
            {
                if (e.RowIndex == int.Parse(DGVRow[Num][0].ToString()))
                {
                    DGVRow.RemoveAt(Num);
                }
            }
            if(DGVGuarantorCredit.Rows.Count != 0)
                DGVGuarantorCredit.Rows.RemoveAt(e.RowIndex);
        }
        private void DGVGuarantor_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            DGVGuarantorCredit.Rows.Add(DGVGuarantor.Rows[e.RowIndex].Cells[0].Value, DGVGuarantor.Rows[e.RowIndex].Cells[1].Value, "0", "0", DGVGuarantor.Rows[e.RowIndex].Cells[2].Value);
        }

        DialogResult UserOutCreditLimit = DialogResult.No;
        private void TBLoanAmount_Leave(object sender, EventArgs e)
        {
            //BankTeacher.Bank.Menu.
            DataTable MinLoan = Class.SQLConnection.InputSQLMSSQL(SQLDefault[11]);
            UserOutCreditLimit = DialogResult.No;
            int LimitAmount = 0;
            int Amount;
            String AmountLimit = LLoanAmount.Text.Remove(0, 1);
            AmountLimit = AmountLimit.Remove(AmountLimit.Length - 1);
            bool Check = int.TryParse(AmountLimit, out LimitAmount);
            if (DGVGuarantor.Rows.Count != 0)
            {
                if (int.TryParse(TBLoanAmount.Text, out Amount) && (Check) && Amount >= Convert.ToInt32(MinLoan.Rows[0][0].ToString()))
                {
                    if (Amount > LimitAmount && UserOutCreditLimit == DialogResult.No && CheckBReset == false)
                    {
                        //Class.FromSettingMedtod.Eb(Bank.Menu.)
                        UserOutCreditLimit = MessageBox.Show("จำนวนเงินกู้ เกินกำหนดเงินค้ำ\r\n ต้องการทำต่อหรือไม่", "แจ้งเตือน", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (UserOutCreditLimit == DialogResult.No)
                        {
                            TBLoanAmount.Text = "";
                            if (tabControl1.SelectedIndex != 1)
                            {
                                tabControl1.SelectedIndex = 1;
                                TBLoanAmount.Focus();
                            }
                        }
                        else
                        {
                            //เช็คดูว่า User นี้ มีสิทหรือไม่
                            //ถ้าไม่ให้เปลี่ยน UserOutCreditLimit เป็น No
                            //และขึ้นแจ้งเตือนว่าไม่สามารถทำได้
                        }
                    }
                    else if (Amount < LimitAmount && UserOutCreditLimit == DialogResult.Yes)
                        UserOutCreditLimit = DialogResult.No;
                }
                else if (Amount < Convert.ToInt32(MinLoan.Rows[0][0]) && TBLoanAmount.Text != "")
                {
                    MessageBox.Show($"เงินกู้ขั้นต่ำต้องมากกว่าหรือเท่ากับ {MinLoan.Rows[0][0]}", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    TBLoanAmount.Text = "";
                    TBLoanAmount.Focus();
                }
                else if (!Check)
                {
                    TBTeacherNo.Focus();
                }
            }
            else if (!Check)
            {
                TBTeacherNo.Focus();
            }


            bool CheckNum = Double.TryParse(TBLoanAmount.Text, out Double LoanAmount);
            LoanAmount = LoanAmount * Convert.ToDouble((Convert.ToDouble(TBInterestRate.Text) / 100)) + LoanAmount;
            LTotal.Text = LoanAmount.ToString();
            if (Int32.TryParse(TBLoanAmount.Text, out int x) && x >= BankTeacher.Bank.Menu.MinLoan && ((UserOutCreditLimit != DialogResult.No) || Convert.ToInt32(TBLoanAmount.Text) <= LimitAmount) || CheckBReset == true)
            {
                if (CheckNum == true && DGVGuarantor.Rows.Count > 0)
                {
                    if (CheckNum == true && DGVGuarantor.Rows.Count > 0)
                    {
                        Double Percent = 100 / LoanAmount * int.Parse(DGVGuarantor.Rows[0].Cells[2].Value.ToString());
                        //if (int.Parse(DGVGuarantor.Rows[0].Cells[2].Value.ToString()) >= LoanAmount)
                        Percent = 50;
                        Double lastRow = 0;
                        for (int Num = 0; Num < DGVGuarantorCredit.Rows.Count; Num++)
                        {
                            if (Num == DGVGuarantorCredit.Rows.Count - 1 /*&& Percent != 100*/)
                            {
                                Percent = 100 - Convert.ToInt32(lastRow);
                            }
                            else if (Num >= 1 /*&& Percent != 100*/)
                            {
                                Percent = (100 - Math.Round(lastRow, 2)) / (DGVGuarantorCredit.Rows.Count - Num);
                            }

                            Percent = Convert.ToInt32(Percent);
                            Double Credit = Convert.ToInt32(LoanAmount) * Percent / 100;
                            if (Credit > Convert.ToDouble(DGVGuarantor.Rows[Num].Cells[2].Value.ToString()))
                            {
                                Credit = Convert.ToInt32(DGVGuarantor.Rows[Num].Cells[2].Value.ToString());
                                Percent = Credit * 100 / LoanAmount;
                            }

                            lastRow += Percent;

                            DGVGuarantorCredit.Rows[Num].Cells[2].Value = Convert.ToInt32(Percent);
                            DGVGuarantorCredit.Rows[Num].Cells[3].Value = Convert.ToInt32(LoanAmount * Percent / 100);


                            if (lastRow < 100 && Num == DGVGuarantorCredit.Rows.Count - 1)
                            {
                                Percent = 100 - Math.Round(lastRow, 2);

                                Credit = Convert.ToInt32(LoanAmount) * (Math.Round(Percent, 2) / 100);
                                for (int Count = 0; Count < DGVGuarantorCredit.Rows.Count; Count++)
                                {
                                    if (Convert.ToInt32(Credit) <= 0)
                                        break;
                                    if (Convert.ToDouble(DGVGuarantor.Rows[Count].Cells[2].Value.ToString()) > Convert.ToDouble(DGVGuarantorCredit.Rows[Count].Cells[3].Value.ToString()))
                                    {
                                        Double CreditResult = 0;
                                        Double CreditAdd = Convert.ToDouble(DGVGuarantor.Rows[Count].Cells[2].Value.ToString()) - Convert.ToDouble(DGVGuarantorCredit.Rows[Count].Cells[3].Value.ToString());
                                        if (CreditAdd >= Credit)
                                            CreditResult = Credit;
                                        else
                                            CreditResult = CreditAdd;
                                        DGVGuarantorCredit.Rows[Count].Cells[3].Value = Convert.ToInt32(Convert.ToDouble(DGVGuarantorCredit.Rows[Count].Cells[3].Value.ToString()) + CreditResult);
                                        Double Interrestrate = (Convert.ToDouble(Convert.ToDouble(TBInterestRate.Text) / 100)) * Convert.ToDouble(TBLoanAmount.Text);
                                        Double GetCredit = Convert.ToInt32(Convert.ToDouble(DGVGuarantorCredit.Rows[Count].Cells[3].Value.ToString()) * 100 / (Convert.ToDouble(TBLoanAmount.Text) + Interrestrate));
                                        DGVGuarantorCredit.Rows[Count].Cells[2].Value = Math.Round(GetCredit, 2);
                                        Credit -= CreditResult;

                                    }
                                }
                            }
                        }
                        LTotal.Text = "" + Convert.ToInt32(Convert.ToDouble(TBLoanAmount.Text) * (Convert.ToDouble(Convert.ToDouble(TBInterestRate.Text) / 100)) + Convert.ToDouble(TBLoanAmount.Text));
                        BCalculate_Click(sender, new EventArgs());
                    }
                    else if (DGVGuarantor.Rows.Count < 1)
                    {
                        TBLoanAmount.Text = "";
                        tabControl1.SelectedIndex = 0;
                        TBTeacherNo.Focus();
                        //for(int Num = 0; Num < DGVGuarantorCredit.Rows.Count; Num++)
                        //{
                        //    DGVGuarantorCredit.Rows[Num].Cells[2].Value = "";
                        //    DGVGuarantorCredit.Rows[Num].Cells[3].Value = "";
                        //}
                    }
                }
                else if (Int32.TryParse(TBLoanAmount.Text, out int y))
                {
                    MessageBox.Show("ยอดกู้ต่ำกว่ากำหนดกรุณาลองใหม่อีกครั้ง", "System", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    TBLoanAmount.Focus();
                    TBLoanAmount.Text = "";
                    tabControl1.SelectedIndex = 0;
                    TBTeacherNo.Focus();
                }
                
            }
          
        }

        List<String[]> DGVRow = new List<String[]> { };
        int ROWW = -1;
        int COLL = -1;
        private void DGVGuarantorCredit_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        { 
            int NumCell = 0;
            if (TBLoanAmount.Text != "" && TBInterestRate.Text != "" && TBPayNo.Text != "" )
            {
                Double Total = (Convert.ToDouble(TBLoanAmount.Text) * (Convert.ToDouble(TBInterestRate.Text) / 100) + Convert.ToDouble(TBLoanAmount.Text));
                bool Check;
                if (DGVGuarantorCredit.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    if (Int32.TryParse(DGVGuarantorCredit.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(), out NumCell))
                    {
                        Check = true;
                    }
                    else
                    {
                        Check = false;
                        NumCell = 0;
                    }


                    if (e.ColumnIndex == 2 && Check)
                    {
                        Double Credit = Convert.ToDouble(NumCell) / 100 * Total;
                        if (Credit <= int.Parse(DGVGuarantor.Rows[e.RowIndex].Cells[2].Value.ToString()))
                        {
                            DGVGuarantorCredit.Rows[e.RowIndex].Cells[3].Value = Convert.ToInt32(Credit);
                        }
                        else if (UserOutCreditLimit != DialogResult.Yes && Credit > int.Parse(DGVGuarantor.Rows[e.RowIndex].Cells[2].Value.ToString()))
                        {
                            MessageBox.Show("เกินวงเงินที่ค้ำได้ จะปรับเป็นยอดค้ำสูงสุด", "ระบบ");
                            DGVGuarantorCredit.Rows[e.RowIndex].Cells[3].Value = Convert.ToDouble(DGVGuarantor.Rows[e.RowIndex].Cells[2].Value.ToString());
                            //Double Interest = (Convert.ToDouble(TBLoanAmount.Text) * (Convert.ToDouble(TBInterestRate.Text) / 100)) + Convert.ToDouble(TBLoanAmount.Text);
                            DGVGuarantorCredit.Rows[e.RowIndex].Cells[2].Value = Convert.ToInt32((Convert.ToDouble(DGVGuarantorCredit.Rows[e.RowIndex].Cells[3].Value.ToString()) * 100 / Total));
                            //if(UserOutCreditLimit == DialogResult.Yes)
                            //    BCalculate_Click(sender, new EventArgs());
                        }
                        else if(UserOutCreditLimit == DialogResult.Yes && Credit <= Convert.ToInt32(LTotal.Text))
                        {
                            DGVGuarantorCredit.Rows[e.RowIndex].Cells[3].Value = Convert.ToInt32(Convert.ToDouble(DGVGuarantorCredit.Rows[e.RowIndex].Cells[2].Value) * Total / 100);
                        }
                        else if(UserOutCreditLimit == DialogResult.Yes && Credit > Convert.ToInt32(LTotal.Text))
                        {
                            MessageBox.Show("วงเงินเกินยอดกู้จะปรับเป็นสูงสุด", "แจ้งเตือน");
                            DGVGuarantorCredit.Rows[e.RowIndex].Cells[3].Value = Convert.ToInt32(LTotal.Text);
                            DGVGuarantorCredit.Rows[e.RowIndex].Cells[2].Value = 100;
                        }
                    }
                    else if (e.ColumnIndex == 3 && Check)
                    {
                        Double Percent = NumCell * 100 / Total;
                        //Double ppp = Convert.ToDouble(Convert.ToInt32(Percent)) + 0.5;
                        if (Math.Round(Percent, 0) > Convert.ToDouble(Convert.ToInt32(Percent)) + 0.5)
                            Percent += 1;
                        else
                        {
                            Percent = Math.Round(Percent, 0);
                        }

                        if (NumCell <= int.Parse(DGVGuarantor.Rows[e.RowIndex].Cells[2].Value.ToString()))
                        {
                            DGVGuarantorCredit.Rows[e.RowIndex].Cells[2].Value = Convert.ToInt32(Percent);
                        }
                        else if(UserOutCreditLimit != DialogResult.Yes)
                        {
                            MessageBox.Show("เกินวงเงินที่ค้ำได้ จะปรับเป็นยอดค้ำสูงสุด", "ระบบ");
                            DGVGuarantorCredit.Rows[e.RowIndex].Cells[3].Value = Convert.ToDouble(DGVGuarantor.Rows[e.RowIndex].Cells[2].Value.ToString());
                            //Double Interest = (Convert.ToDouble(TBLoanAmount.Text) * (Convert.ToDouble(TBInterestRate.Text) / 100)) + Convert.ToDouble(TBLoanAmount.Text);
                            DGVGuarantorCredit.Rows[e.RowIndex].Cells[2].Value = Convert.ToInt32((Convert.ToDouble(DGVGuarantorCredit.Rows[e.RowIndex].Cells[3].Value.ToString()) * 100 / Total));

                        }
                        else if(UserOutCreditLimit == DialogResult.Yes && NumCell <= Convert.ToInt32(LTotal.Text))
                        {
                            DGVGuarantorCredit.Rows[e.RowIndex].Cells[2].Value = Percent;
                        }
                        else if (UserOutCreditLimit == DialogResult.Yes && NumCell > Convert.ToInt32(LTotal.Text))
                        {
                            MessageBox.Show("วงเงินเกินยอดกู้จะปรับเป็นสูงสุด", "แจ้งเตือน");
                            DGVGuarantorCredit.Rows[e.RowIndex].Cells[3].Value = Convert.ToInt32(LTotal.Text);
                            DGVGuarantorCredit.Rows[e.RowIndex].Cells[2].Value = 100;
                        }
                    }
                }
                else
                {
                    if(Int32.TryParse(DGVGuarantorCredit.Rows[e.RowIndex].Cells[3].Value.ToString(), out NumCell))
                        {
                        Double Percent = NumCell * 100 / Total;
                        Double ppp = Convert.ToDouble(Convert.ToInt32(Percent)) + 0.5;
                        if (Math.Round(Percent, 0) > Convert.ToDouble(Convert.ToInt32(Percent)) + 0.5)
                            Percent += 1;
                        else
                        {
                            Percent = Math.Round(Percent, 0);
                        }
                        DGVGuarantorCredit.Rows[e.RowIndex].Cells[2].Value = Convert.ToInt32(Percent);
                    }
                        
                }
                
            }
            else
            {
                DGVGuarantor.Rows[e.RowIndex].Cells[2].Value = DefaultEdit;
                MessageBox.Show("โปรดกรอกหน้าข้อมูลการกู้ให้ครบถ้วนก่อน", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            int SumCreditEdit = 0;
            for (int Num = 0; Num < DGVGuarantorCredit.Rows.Count; Num++)
            {
                SumCreditEdit += Convert.ToInt32(DGVGuarantorCredit.Rows[Num].Cells[3].Value.ToString());
            }

            SumCreditEdit = Convert.ToInt32(LTotal.Text) - SumCreditEdit;
            if (SumCreditEdit > 0)
            {
                LLackAmount.ForeColor = Color.Red;
                LOutCredit.ForeColor = Color.Green;
                LLackAmount.Text = SumCreditEdit + "";
                LOutCredit.Text = 0 + "";
            }
            else if (SumCreditEdit < 0)
            {
                LLackAmount.ForeColor = Color.Green;
                LOutCredit.ForeColor = Color.Red;
                LLackAmount.Text = 0 + "";
                LOutCredit.Text = (-1 * SumCreditEdit) + "";
            }
            else
            {
                LLackAmount.ForeColor = Color.Green;
                LOutCredit.ForeColor = Color.Green;
                LLackAmount.Text = SumCreditEdit + "";
                LOutCredit.Text = SumCreditEdit + "";
            }
            if(!int.TryParse(DGVGuarantorCredit.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(),out int a))
            {
                DGVGuarantorCredit.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = DefaultEdit;
            }
        }
        public TextBox tb;
        private void DGVGuarantorCredit_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (DGVGuarantorCredit.CurrentCell.ColumnIndex >= 2)
            {
                tb = (TextBox)e.Control;
                tb.KeyPress += new KeyPressEventHandler(TBCellGuarantorCredit_KeyPress);
                tb.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TBKeyUp);
                tb.TextChanged += new System.EventHandler(this.TBTextChanged);
            }
        }
        void TBTextChanged(object sendet , EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(tb.Text, "[^0-9]"))
            {
                DGVGuarantorCredit.CancelEdit();
            }
        }
        void TBKeyUp(object sender , KeyEventArgs e)
        {
            if (e.KeyCode == Keys.V && e.Modifiers == Keys.Control)
            {
                DGVGuarantorCredit.CancelEdit();
            }
            else if(e.KeyCode == Keys.V)
            {
                DGVGuarantorCredit.CancelEdit();
            }
        }
        void TBCellGuarantorCredit_KeyPress(object sender, KeyPressEventArgs e)
        {

            if ((!Char.IsNumber(e.KeyChar)) && (!Char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }


        private void BTdeletefile_Click(object sender, EventArgs e)
        {
            StatusBoxFile = 0;
        
            imgeLocation = "";
        }

        private void TBLoanAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control == true)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void TBLoanAmount_Enter(object sender, EventArgs e)
        {
            if (DGVGuarantor.Rows.Count == 0)
            {
                MessageBox.Show("โปรดเลือกผู้กู้ ผู้ค้ำก่อน", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tabControl1.SelectedIndex = 0;
                TBTeacherNo.Focus();
            }
            else
                CheckBReset = false;
        }

        private void TBInterestRate_Leave(object sender, EventArgs e)
         {
            if(double.TryParse(TBInterestRate.Text , out double x) && x <= 100)
            {

                //Double aa = (Convert.ToDouble(TBInterestRate.Text) / 100);
                if(Double.TryParse(TBInterestRate.Text , out Double Interestrate) && Interestrate > 0)
                {
                    int LoanAmount = Convert.ToInt32(CreditLoanAmount - CreditLoanAmount * (Interestrate / 100));
                    LLoanAmount.Text = "(" + LoanAmount + ")";
                    LTotal.Text = "" + LoanAmount;

                    TBLoanAmount_Leave(sender, new EventArgs());
                }
                else if(Interestrate <= 0)
                {
                    MessageBox.Show("อัตราดอกเบี้ยต้องมากกว่า 0 ", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    TBInterestRate.Text = "";
                    TBInterestRate.Focus();
                }
                else
                {
                    MessageBox.Show("ใสจำนวนเปอร์เซ็นต์ไม่ถูกต้อง");
                    TBInterestRate.Text = "";
                    TBInterestRate.Focus();
                }
            }
            else
            {
                TBInterestRate.Text = "1";
                TBInterestRate.Focus();
            }
        }

        private void Delete_Click_1(object sender, EventArgs e)
        {
            if(DialogResult.OK == MessageBox.Show("คุณแน่ใจจะลบขช้อมูลทั้งหมด","ข้อมูล",MessageBoxButtons.OKCancel))
            Class.SQLConnection.InputSQLMSSQL(SQLDefault[7]);
        }


        private void TBPayNo_Leave(object sender, EventArgs e)
        {
            if(panel1.Focused)
                if (Int32.TryParse(TBPayNo.Text,out int aa) && aa <= 0)
                {
                    tabControl1.SelectedIndex = 1;
                    MessageBox.Show("จำนวนเดือนต้องไม่เท่ากับ 0", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    TBPayNo.Text = "";
                    TBPayNo.Focus();
                }
                else if(aa > 24)
                {
                    tabControl1.SelectedIndex = 1;
                    MessageBox.Show("ไม่สามารถกู้เกิน 24 เดือนได้", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    TBPayNo.Text = "";
                    TBPayNo.Focus();
                }
        }

        private void BReset_Click(object sender, EventArgs e)
        {
            CheckBReset = true;
            TBLoanAmount_Leave(sender, new EventArgs());
        }

        private void DGVGuarantorCredit_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            ROWW = e.RowIndex;
            COLL = e.ColumnIndex;
            if (Int32.TryParse(DGVGuarantorCredit.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(), out int Values))
                DefaultEdit = Values;
        }

        private void TBLoanAmount_TextChanged(object sender, EventArgs e)
        {
            BankTeacher.Class.FromSettingMedtod.ProtectedCtrlVTB(TBLoanAmount);
        }

        private void BTPrint_Click(object sender, EventArgs e)
        {
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }



        private void BCalculate_Click(object sender, EventArgs e)
        {
            if (DGVGuarantorCredit.Rows.Count > 0 && TBLoanAmount.Text != "")
            {
                Double SumCredit = 0;
                for (int Num = 0; Num < DGVGuarantorCredit.Rows.Count; Num++)
                {
                    SumCredit += Convert.ToDouble(DGVGuarantorCredit.Rows[Num].Cells[3].Value.ToString());
                }
                Double Difference = Convert.ToDouble(TBLoanAmount.Text) + (Convert.ToDouble(TBLoanAmount.Text) * (Convert.ToDouble(TBInterestRate.Text) / 100)) - SumCredit;
                Double Interest = (Convert.ToDouble(Convert.ToDouble(TBInterestRate.Text) / 100)) * Convert.ToDouble(TBLoanAmount.Text) + Convert.ToDouble(TBLoanAmount.Text);

                int SumAmountCredit = 0;
                for (int Num = 0; Num < DGVGuarantorCredit.Rows.Count; Num++)
                {
                    Double Result = 0;
                    if (Convert.ToDouble(DGVGuarantorCredit.Rows[Num].Cells[3].Value.ToString()) <= Convert.ToDouble(DGVGuarantor.Rows[Num].Cells[2].Value.ToString()) && SumCredit != Interest)
                    {
                        Double CreditAdd = Convert.ToDouble(DGVGuarantor.Rows[Num].Cells[2].Value.ToString()) - Convert.ToDouble(DGVGuarantorCredit.Rows[Num].Cells[3].Value.ToString());

                        
                        if (CreditAdd >= Difference && Difference > 0 /*&& CheckMinus == false*/)
                            Result = Difference;
                        else if (CreditAdd < Difference /*&& CheckMinus == false*/)
                            Result = CreditAdd;
                        else if (Difference < 0)
                        {
                            Result = Difference;
                        }
                        if(Convert.ToInt32(LOutCredit.Text) > 0)
                        {
                            if (Convert.ToInt32(Convert.ToDouble(DGVGuarantorCredit.Rows[Num].Cells[3].Value.ToString()) + Result) <= 0)
                            {
                                int ChangeMinus = Convert.ToInt32(Interest * 0.01);
                                Result = (Convert.ToInt32(DGVGuarantorCredit.Rows[Num].Cells[3].Value.ToString()) - ChangeMinus) * -1;
                            }
                            else
                                Result = Difference * -1;

                            if (Result < 0)
                                DGVGuarantorCredit.Rows[Num].Cells[3].Value = Convert.ToInt32(Convert.ToDouble(DGVGuarantorCredit.Rows[Num].Cells[3].Value.ToString()) + Result);
                            else
                                DGVGuarantorCredit.Rows[Num].Cells[3].Value = Convert.ToInt32(Convert.ToDouble(DGVGuarantorCredit.Rows[Num].Cells[3].Value.ToString()) - Result);

                            DGVGuarantorCredit.Rows[Num].Cells[2].Value = Convert.ToInt32(Convert.ToDouble(DGVGuarantorCredit.Rows[Num].Cells[3].Value.ToString()) * 100 / Interest);

                            if (Result < 0)
                                Difference -= Result;
                            else if (Result > 0 && Difference < 0)
                                Difference += Result;
                            else if (Difference > 0)
                                Difference -= Result;
                        }
                        else
                        {
                            DGVGuarantorCredit.Rows[Num].Cells[3].Value = Convert.ToInt32(Convert.ToDouble(DGVGuarantorCredit.Rows[Num].Cells[3].Value.ToString()) + Result);
                            DGVGuarantorCredit.Rows[Num].Cells[2].Value = Convert.ToInt32(Convert.ToDouble(DGVGuarantorCredit.Rows[Num].Cells[3].Value.ToString()) * 100 / Interest);

                            Difference -= Result;
                        }
                    }
                    SumAmountCredit += Convert.ToInt32(DGVGuarantorCredit.Rows[Num].Cells[3].Value.ToString());
                }

                SumAmountCredit = Convert.ToInt32(LTotal.Text) - SumAmountCredit;
                if (SumAmountCredit > 0)
                {
                    LLackAmount.ForeColor = Color.Red;
                    LOutCredit.ForeColor = Color.Green;
                    LLackAmount.Text = SumAmountCredit + "";
                    LOutCredit.Text = 0 + "";
                }
                else if(SumAmountCredit < 0)
                {
                    LLackAmount.ForeColor = Color.Green;
                    LOutCredit.ForeColor = Color.Red;
                    LLackAmount.Text = 0 + "";
                    LOutCredit.Text = (SumAmountCredit * -1).ToString();
                }
                else
                {
                    LLackAmount.ForeColor = Color.Green;
                    LOutCredit.ForeColor = Color.Green;
                    LLackAmount.Text = SumAmountCredit + "";
                    LOutCredit.Text = SumAmountCredit + "";
                }
                String AmountLimit = LLoanAmount.Text.Remove(0, 1);
                AmountLimit = AmountLimit.Remove(AmountLimit.Length - 1);
                DGVGuarantorCredit.Rows[0].Cells[3].Value = Convert.ToInt32(DGVGuarantorCredit.Rows[0].Cells[3].Value.ToString()) + SumAmountCredit;
                DGVGuarantorCredit.Rows[0].Cells[2].Value = Convert.ToInt32((Convert.ToDouble(DGVGuarantorCredit.Rows[0].Cells[3].Value.ToString()) * 100) / Interest);
                if(LLackAmount.Text != "0")
                {
                    LLackAmount.ForeColor = Color.Green;
                    LLackAmount.Text = "0";
                }
                else if(LOutCredit.Text != "0")
                {
                    LOutCredit.ForeColor = Color.Green;
                    LOutCredit.Text = "0";
                }
            }
        }

        private void loan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (TBTeacherNo.Text.Length != 0)
                {
                    Checkmember(true);
                    // ======= Tab 1 Clear ===============
                    CheckBReset = false;
                    TBLoanAmount.Text = "";
                    TBTeacherName.Text = "";
                    TBLoanNo.Text = "";
                    TBLoanStatus.Text = "";
                    TBSavingAmount.Text = "";
                    TBTeacherNo.Text = "";
                    DGVGuarantor.Rows.Clear();
                    // ======= Tab 2 Clear ===============
                    CBPayMonth.SelectedIndex = -1;
                    CBPayYear.SelectedIndex = -1;
                    DGVGuarantorCredit.Rows.Clear();
                    // ======= Tab 3 Clear ===============
                    DGVGuarantorCredit.Rows.Clear();
                    // ======= Tab 4 Clear ===============
                    DGVLoanDetail.Rows.Clear();
                    Check = 0;
                    tabControl1.SelectedIndex = 0;
                    TBTeacherNo.Focus();
                }
                else
                {
                    BExitForm_Click(new object(), new EventArgs());
                }
            }
        }

        private void loan_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                if (tabControl1.SelectedIndex == tabControl1.TabCount - 1)
                {
                    tabControl1.SelectedIndex = 0;
                }
                else
                {
                    tabControl1.SelectedIndex = tabControl1.SelectedIndex + 1;
                }
            }
        }

        private void TBPayNo_TextChanged(object sender, EventArgs e)
        {
            BankTeacher.Class.FromSettingMedtod.ProtectedCtrlVTB(TBPayNo);
        }

        private void TBInterestRate_TextChanged(object sender, EventArgs e)
        {
            BankTeacher.Class.FromSettingMedtod.ProtectedCtrlVTB(TBInterestRate);
        }

        private void BExitForm_Click(object sender, EventArgs e)
        {
            BankTeacher.Class.FromSettingMedtod.ReturntoHome(this);
        }
    }
}


