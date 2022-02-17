using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.IO;
using ExcelDataReader;
using System.Diagnostics;

namespace BankTeacher.Bank
{
    public partial class Setting : Form
    {
        static int Min;
        static int Max;
        static bool chb;
        static Font FontSetting;
        String Key = "";


        /// <summary>
        /// SQLDafault
        /// <para>[0]Edit Setting INPUT: {DateAmountChange} {StartAmountMin} {StartAmountMax} {PerShare}</para>
        /// <para>[1] INSERT Member To Member  Bill BillDetail  and Insert FileINPUT: {TeacherNo} {TeacherNoAddBy} {StartAmount} {Mount} {Year} {DateReg} {PaymentNo}</para>
        /// <para>[2] INSERT Loan and Get LoanNo INPUT: {TeacherNoAdd}, {TeacherNo}, {MonthPay}, {YearPay}, {LoanAmount}, {PayNo}, {InterestRate}</para>
        /// <para>[3]INSERT Guarantor INPUT: {LoanNo},{TeacherNo},{Amount},{RemainsAmount}</para>
        /// <para>[4] UPDATE Payment Loan INPUT: {LoanID} {TeacherNoPay} {PaymentNo} {PayDate}</para>
        /// <para>[5] UPDATE Share WithDraw and INSERT WithDraw History INPUT: {WithDraw} , {PayMent} {TeacherNoAddBy} {DateAdd}</para>
        /// <para>[7] Set StatusButton = false INPUT: </para>
        /// <para>[8]Edit DateAmountChange INPUT: {DateAmountChange} </para>
        /// </summary>
        private static String[] SQLDefault = new String[]
        { 
             //[0]Edit Setting INPUT: {DateAmountChange} {StartAmountMin} {StartAmountMax} {PerShare}  {MinLoan}
             "UPDATE EmployeeBank.dbo.tblSettingAmount \r\n" +
             "SET DateAmountChange = {DateAmountChange}, StartAmountMin = {StartAmountMin} , StartAmountMax = {StartAmountMax} , PerShare = {PerShare} , MinLoan = {MinLoan};\r\n"
            ,
           //[1] INSERT Member To Member  Bill BillDetail  and Insert FileINPUT: {TeacherNo} {TeacherNoAddBy} {StartAmount} {Mount} {Year} {DateReg} {PaymentNo}
           "DECLARE @CountTeacher INT; \r\n " +
          "DECLARE @BillNo INT; \r\n " +
          "DECLARE @CheckMember INT; \r\n " +
          " \r\n " +
          "SET @CheckMember  = (SELECT  COUNT(TeacherNo) \r\n " +
          "FROM EmployeeBank.dbo.tblMember \r\n " +
          "WHERE TeacherNo = '{TeacherNo}' and MemberStatusNo = 1) \r\n " +
          " \r\n " +
          "IF(@CheckMember  < 1) \r\n " +
          "BEGIN \r\n " +
          " \r\n " +
          "		SET @CheckMember = (SELECT COUNT(TeacherNo) FROM Personal.dbo.tblTeacherHis WHERE TeacherNo = '{TeacherNo}' and IsUse = 1) \r\n " +
          "		IF(@CheckMember != 0) \r\n " +
          "		BEGIN \r\n " +
          "				SET @CountTeacher = (SELECT Count(TeacherNo) \r\n " +
          "				FROM EmployeeBank.dbo.tblMember \r\n " +
          "				WHERE TeacherNo = '{TeacherNo}' and MemberStatusNo = 2) ; \r\n " +
          " \r\n " +
          "				IF(@CountTeacher > 0) \r\n " +
          "				BEGIN \r\n " +
          "				UPDATE EmployeeBank.dbo.tblMember \r\n " +
          "				SET MemberStatusNo = 1 ,DateAdd = '{DateReg}',DocStatusNo = 2,DocUploadPath = '' \r\n " +
          "				WHERE TeacherNo = '{TeacherNo}'; \r\n " +
          " \r\n " +
          "				UPDATE EmployeeBank.dbo.tblShare \r\n " +
          "				SET SavingAmount = '{StartAmount}' \r\n " +
          "				WHERE TeacherNo = '{TeacherNo}'; \r\n " +
          " \r\n " +
          "				INSERT INTO EmployeeBank.dbo.tblBill(TeacherNo, TeacherNoAddBy, DateAdd , TransactionDate)  \r\n " +
          "				VALUES('{TeacherNo}','{TeacherNoAddBy}', '{DateReg}', CURRENT_TIMESTAMP)  \r\n " +
          " \r\n " +
          "				SELECT @BillNo = SCOPE_IDENTITY();  \r\n " +
          " \r\n " +
          "				INSERT INTO EmployeeBank.dbo.tblBillDetail(BillNo, TypeNo, Amount, Mount, Year,BillDetailPaymentNo) \r\n " +
          "				VALUES(@BillNo,3,{StartAmount},{Month},{Year},{PaymentNo}) \r\n " +
          " \r\n " +
          "				END; \r\n " +
          " \r\n " +
          "				ELSE \r\n " +
          "				BEGIN \r\n " +
          " \r\n " +
          "				INSERT INTO EmployeeBank.dbo.tblMember(TeacherNo, TeacherAddBy, StartAmount, DateAdd,DocStatusNo,DocUploadPath)  \r\n " +
          "				VALUES('{TeacherNo}','{TeacherNoAddBy}',{StartAmount},'{DateReg}',2,'')   \r\n " +
          " \r\n " +
          "				INSERT INTO EmployeeBank.dbo.tblShare(TeacherNo, SavingAmount) \r\n " +
          "				VALUES('{TeacherNo}',{StartAmount})  \r\n " +
          " \r\n " +
          "				INSERT INTO EmployeeBank.dbo.tblBill(TeacherNo, TeacherNoAddBy, DateAdd,TransactionDate)  \r\n " +
          "				VALUES('{TeacherNo}','{TeacherNoAddBy}', '{DateReg}',CURRENT_TIMESTAMP)  \r\n " +
          " \r\n " +
          "				SELECT @BillNo = SCOPE_IDENTITY();  \r\n " +
          " \r\n " +
          "				INSERT INTO EmployeeBank.dbo.tblBillDetail(BillNo, TypeNo, Amount, Mount, Year,BillDetailPaymentNo) \r\n " +
          "				VALUES(@BillNo,3,{StartAmount},{Month},{Year},{PaymentNo}) \r\n " +
          " \r\n " +
          "				END;    \r\n " +
          "		END; \r\n " +
          "END;"
           ,
           
            //[2] INSERT Loan and Get LoanNo INPUT: {TeacherNoAdd}, {TeacherNo}, {MonthPay}, {YearPay}, {LoanAmount}, {PayNo}, {InterestRate} {DateAdd}
            "DECLARE @LoanNo INT;\r\n" +
            "INSERT INTO EmployeeBank.dbo.tblLoan\r\n" +
            "(TeacherNoAddBy, TeacherNo, MonthPay, YearPay, LoanAmount, PayNo, InterestRate, DateAdd)\r\n" +
            "VALUES ('{TeacherNoAdd}', '{TeacherNo}', {MonthPay}, {YearPay}, {LoanAmount}, {PayNo}, {InterestRate},'{DateAdd}');\r\n" +
            "SET @LoanNo = SCOPE_IDENTITY();\r\n" +
            "SELECT LoanNo\r\n" +
            "FROM EmployeeBank.dbo.tblLoan\r\n" +
            "WHERE LoanNo = @LoanNo;\r\n"
            ,

            //[3]INSERT Guarantor INPUT: {LoanNo},{TeacherNo},{Amount},{RemainsAmount}
            "INSERT INTO EmployeeBank.dbo.tblGuarantor (LoanNo,TeacherNo,Amount,RemainsAmount)\r\n" +
            "VALUES ('{LoanNo}','{TeacherNo}','{Amount}','{RemainsAmount}');\r\n"
            ,
            //[4] UPDATE Payment Loan INPUT: {LoanID} {TeacherNoPay} {PaymentNo} {PayDate}
            "UPDATE EmployeeBank.dbo.tblLoan \r\n " +
            "SET PayDate = '{PayDate}' , TeacherNoPay = '{TeacherNoPay}', BillDetailPaymentNo = '{PaymentNo}' , LoanStatusNo = 2 \r\n " +
            "WHERE LoanNo = '{LoanID}'; "
            ,
            //[5] UPDATE Share WithDraw & INSERT WithDraw History INPUT:  {WithDraw} , {PayMent} {TeacherNoAddBy} {DateAdd}
            "DECLARE @ShareNo INT; \r\n " +
             " \r\n " +
             "SET @ShareNo = (SELECT ShareNo FROM EmployeeBank.dbo.tblMember as a LEFT JOIN EmployeeBank.dbo.tblShare as b on a.TeacherNo = b.TeacherNo WHERE a.TeacherNo = '{TeacherNo}')"
             ,
            //[6]
            "UPDATE EmployeeBank.dbo.tblShare\r\n" +
            "SET SavingAmount = SavingAmount - {WithDraw}\r\n" +
            "WHERE ShareNo = @ShareNo; \r\n " +

            "INSERT INTO EmployeeBank.dbo.tblShareWithdraw (TeacherNoAddBy,ShareNo,DateAdd,Amount,BillDetailPayMentNo)\r\n" +
            "VALUES ('{TeacherNoAddBy}', '{ShareNo}','{DateAdd}','{WithDraw}',{PayMent});"

            ,
            //[7] Set StatusButton = false INPUT: 
           "UPDATE EmployeeBank.dbo.tblSettingAmount \r\n " +
          "SET StatusUploadExceltoSQL = 1;"
           ,
           //[8]Edit DateAmountChange INPUT: {DateAmountChange} 
             "UPDATE EmployeeBank.dbo.tblSettingAmount \r\n" +
             "SET DateAmountChange = {DateAmountChange};\r\n"
        };
        public Setting()
        {
            InitializeComponent();
            Console.WriteLine("==================Open Setting Form======================");
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            TB_Min.Text = BankTeacher.Bank.Menu.startAmountMin.ToString();
            TB_Max.Text = BankTeacher.Bank.Menu.startAmountMax.ToString();
            TBPerShare.Text = BankTeacher.Bank.Menu.perShare.ToString();
            TBMinLoan.Text = BankTeacher.Bank.Menu.MinLoan.ToString();
            if (BankTeacher.Bank.Menu.StatusActivateButtonExceltoSQL.ToString() == "0")
                BExceltoSQL.Enabled = true;
            if (BankTeacher.Bank.Menu.DateAmountChange == 1)
            {
                CHB_edittime.Checked = true;
            }
            chb = CHB_edittime.Checked;
            Min = Convert.ToInt32(TB_Min.Text);
            Max = Convert.ToInt32(TB_Max.Text);
            B_Save.Enabled = false;
        }


        private void TB_Min_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && (e.KeyChar != '\b'))
            {
                e.Handled = true;
            }
        }

        private void TB_Max_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && (e.KeyChar != '\b'))
            {
                e.Handled = true;
            }
        }

        private void B_Save_Click(object sender, EventArgs e)
        {

            if (Convert.ToInt32(TB_Min.Text) <= Convert.ToInt32(TB_Max.Text))
            {
                int TranChbToInt;
                if (CHB_edittime.Checked == true)
                {
                    TranChbToInt = 1;
                    BankTeacher.Bank.Menu.DateAmountChange = TranChbToInt;
                }
                else
                {
                    TranChbToInt = 0;
                    BankTeacher.Bank.Menu.DateAmountChange = TranChbToInt;
                }

                Class.SQLConnection.InputSQLMSSQL(SQLDefault[0].Replace("{DateAmountChange}", TranChbToInt.ToString())
                    .Replace("{StartAmountMin}", TB_Min.Text)
                    .Replace("{StartAmountMax}", TB_Max.Text)
                    .Replace("{PerShare}", TBPerShare.Text)
                    .Replace("{MinLoan}", TBMinLoan.Text));
                BankTeacher.Bank.Menu.startAmountMin = Convert.ToInt32(TB_Min.Text);
                BankTeacher.Bank.Menu.startAmountMax = Convert.ToInt32(TB_Max.Text);
                BankTeacher.Bank.Menu.perShare = Convert.ToInt32(TBPerShare.Text);
                BankTeacher.Bank.Menu.MinLoan = Convert.ToInt32(TBMinLoan.Text);
                MessageBox.Show("เสร็จสิ้น", "ตั้งค่า", MessageBoxButtons.OK, MessageBoxIcon.Information);
                B_Save.Enabled = false;
            }
            else
                MessageBox.Show("ค่าสูงสุดต้องไม่น้อยกว่าค่าต่ำสุด", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);


        }
        private void B_Cancel_Click(object sender, EventArgs e)
        {
            BExitForm_Click(new object(), new EventArgs());
        }


        private void button1_Click(object sender, EventArgs e)
        {
            BankTeacher.Bank.SQLEditing f = new SQLEditing();
            f.ShowDialog();
        }

        private void TBMinLoan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && (e.KeyChar != '\b'))
            {
                e.Handled = true;
            }
        }

        private void BExitForm_Click(object sender, EventArgs e)
        {
            BankTeacher.Class.FromSettingMedtod.ReturntoHome(this);
        }
        private void tabControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                BExitForm_Click(new object(), new EventArgs());
            }
        }
        private void TB_Min_TextChanged(object sender, EventArgs e)
        {
            B_Save.Enabled = true;
        }

        private void TB_Max_TextChanged(object sender, EventArgs e)
        {
            B_Save.Enabled = true;
        }

        private void TBPerShare_TextChanged(object sender, EventArgs e)
        {
            B_Save.Enabled = true;
        }

        private void TBMinLoan_TextChanged(object sender, EventArgs e)
        {
            B_Save.Enabled = true;
        }
        private void Setting_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                BExitForm_Click(new object(), new EventArgs());
            }
            else if (e.KeyCode == Keys.R)
            {
                Key = "";
                button1.Visible = false;
            }
            else
                Key += e.KeyCode;
            if (Key.Length == 3)
                if (Key == "ABC" || Key == "abc")
                    button1.Visible = true;
        }

        private void BExceltoSQL_Click(object sender, EventArgs e)
        {
            if (BankTeacher.Bank.Menu.StatusActivateButtonExceltoSQL.ToString() != "1")
            {
                DateTime ToDay = Convert.ToDateTime(Convert.ToDateTime(BankTeacher.Bank.Menu.Date[0] + "/" + BankTeacher.Bank.Menu.Date[1] + "/" + BankTeacher.Bank.Menu.Date[2]).ToString("yyyy/MM/dd"));
                using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "Excel 97-2003 Workbook|*.xlsx|Excel Workbook|*.xls" })
                {
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        if (ofd.FileName != "")
                        {
                            try
                            {
                                FileStream stream = File.Open(ofd.FileName, FileMode.Open, FileAccess.Read);
                                IExcelDataReader excelReader;

                                if (Path.GetExtension(ofd.FileName).ToUpper() == ".XLS")
                                {
                                    excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
                                }
                                else
                                {
                                    excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                                }
                                DataSet result = excelReader.AsDataSet(new ExcelDataSetConfiguration()
                                {
                                    ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                                    {
                                        UseHeaderRow = true
                                    }
                                });
                                DataSet ds = result;
                                stream.Close();
                                try
                                {
                                    if (ds.Tables.Count != 0)
                                    {
                                        //Register Member
                                        if (ds.Tables[0].Rows.Count != 0)
                                        {
                                            for (int x = 0; x < ds.Tables[0].Rows.Count; x++)
                                            {
                                                String TeacherID = "";
                                                DateTime DateRegister = ToDay;
                                                int Amount = BankTeacher.Bank.Menu.startAmountMin;
                                                String Payment = "เงินสด";
                                                int PaymenyNo = 1;

                                                if (ds.Tables[0].Rows[x][0].ToString() == "" || ds.Tables[0].Rows[x][0].ToString() == null)
                                                    continue;
                                                else
                                                    TeacherID = ds.Tables[0].Rows[x][0].ToString().Replace("t", "T");
                                                if (ds.Tables[0].Rows[x][1].ToString() != "" && ds.Tables[0].Rows[x][1].ToString() != null)
                                                    DateRegister = Convert.ToDateTime(Convert.ToDateTime(ds.Tables[0].Rows[x][1].ToString()).ToString("yyyy/MM/dd"));
                                                if (ds.Tables[0].Rows[x][2].ToString() != "" && ds.Tables[0].Rows[x][2].ToString() != null)
                                                    Amount = Convert.ToInt32(ds.Tables[0].Rows[x][2].ToString());
                                                if (ds.Tables[0].Rows[x][3].ToString() != "" && ds.Tables[0].Rows[x][3].ToString() != null)
                                                    if (ds.Tables[0].Rows[x][3].ToString().Contains("โอน"))
                                                        Payment = "โอนเงิน";
                                                if (Payment.Contains("โอน"))
                                                    PaymenyNo = 2;

                                                Class.SQLConnection.InputSQLMSSQL(SQLDefault[1].Replace("{TeacherNo}", TeacherID)
                                                .Replace("{TeacherNoAddBy}", BankTeacher.Class.UserInfo.TeacherNo)
                                                .Replace("{StartAmount}", Amount.ToString())
                                                .Replace("{Month}", DateRegister.ToString("MM"))
                                                .Replace("{Year}", DateRegister.ToString("yyyy"))
                                                .Replace("{DateReg}", DateRegister.ToString("yyyy/MM/dd"))
                                                .Replace("{PaymentNo}", PaymenyNo.ToString()));
                                            }
                                        }
                                        //Loan
                                        if (ds.Tables[1].Rows.Count != 0)
                                        {
                                            for (int x = 0; x < ds.Tables[1].Rows.Count; x++)
                                            {
                                                //------------------------

                                                String TeacherID = "";

                                                String StartLoanMonth = BankTeacher.Bank.Menu.Date[1];
                                                String StartLoanYear = BankTeacher.Bank.Menu.Date[0];

                                                int AmountLoan = BankTeacher.Bank.Menu.MinLoan;

                                                int PayNo = 12;

                                                double Interest = 0.25;

                                                int CountGuarantor = 0;

                                                String[] GuarantorID = { "", "", "", "" };
                                                int[] GuarantorAmount = { 0, 0, 0, 0 };
                                                DateTime DateRegLoan = ToDay;

                                                String Payment = "เงินสด";
                                                int PaymentNo = 1;
                                                DateTime PayDate = ToDay;

                                                //---------------------------
                                                //Detail
                                                if (ds.Tables[1].Rows[x][0].ToString() == "" || ds.Tables[1].Rows[x][0].ToString() == null)
                                                    continue;
                                                else
                                                    TeacherID = ds.Tables[1].Rows[x][0].ToString().Replace("t", "T");
                                                if (ds.Tables[1].Rows[x][1].ToString() != "" && ds.Tables[1].Rows[x][1].ToString() != null)
                                                    StartLoanMonth = ds.Tables[1].Rows[x][1].ToString();
                                                if (ds.Tables[1].Rows[x][2].ToString() != "" && ds.Tables[1].Rows[x][2].ToString() != null)
                                                    StartLoanYear = ds.Tables[1].Rows[x][2].ToString();
                                                if (ds.Tables[1].Rows[x][3].ToString() != "" && ds.Tables[1].Rows[x][3].ToString() != null)
                                                    AmountLoan = Convert.ToInt32(ds.Tables[1].Rows[x][3].ToString());
                                                if (ds.Tables[1].Rows[x][4].ToString() != "" && ds.Tables[1].Rows[x][4].ToString() != null)
                                                    PayNo = Convert.ToInt32(ds.Tables[1].Rows[x][4].ToString());
                                                if (ds.Tables[1].Rows[x][5].ToString() != "" && ds.Tables[1].Rows[x][5].ToString() != null)
                                                    Interest = Convert.ToDouble(ds.Tables[1].Rows[x][5].ToString());

                                                //Guarantor
                                                if (ds.Tables[1].Rows[x][6].ToString() != "" && ds.Tables[1].Rows[x][6].ToString() != null &&
                                                    ds.Tables[1].Rows[x][7].ToString() != "" && ds.Tables[1].Rows[x][7].ToString() != null)
                                                {
                                                    GuarantorID[0] = ds.Tables[1].Rows[x][6].ToString();
                                                    GuarantorAmount[0] = Convert.ToInt32(ds.Tables[1].Rows[x][7]);
                                                    CountGuarantor++;
                                                }
                                                else
                                                    continue;

                                                if (ds.Tables[1].Rows[x][8].ToString() != "" && ds.Tables[1].Rows[x][8].ToString() != null &&
                                                    ds.Tables[1].Rows[x][9].ToString() != "" && ds.Tables[1].Rows[x][9].ToString() != null)
                                                {
                                                    GuarantorID[1] = ds.Tables[1].Rows[x][8].ToString();
                                                    GuarantorAmount[1] = Convert.ToInt32(ds.Tables[1].Rows[x][9]);
                                                    CountGuarantor++;
                                                }

                                                if (ds.Tables[1].Rows[x][10].ToString() != "" && ds.Tables[1].Rows[x][10].ToString() != null &&
                                                    ds.Tables[1].Rows[x][11].ToString() != "" && ds.Tables[1].Rows[x][11].ToString() != null)
                                                {
                                                    GuarantorID[2] = ds.Tables[1].Rows[x][10].ToString();
                                                    GuarantorAmount[2] = Convert.ToInt32(ds.Tables[1].Rows[x][11]);
                                                    CountGuarantor++;
                                                }

                                                if (ds.Tables[1].Rows[x][12].ToString() != "" && ds.Tables[1].Rows[x][12].ToString() != null &&
                                                    ds.Tables[1].Rows[x][13].ToString() != "" && ds.Tables[1].Rows[x][13].ToString() != null)
                                                {
                                                    GuarantorID[3] = ds.Tables[1].Rows[x][12].ToString();
                                                    GuarantorAmount[3] = Convert.ToInt32(ds.Tables[1].Rows[x][13]);
                                                    CountGuarantor++;
                                                }

                                                //RegisterDate
                                                if (ds.Tables[1].Rows[x][14].ToString() != "" && ds.Tables[1].Rows[x][14].ToString() != null)
                                                    DateRegLoan = Convert.ToDateTime(Convert.ToDateTime(ds.Tables[1].Rows[x][14].ToString()).ToString("yyyy/MM/dd"));

                                                //Pay
                                                if (ds.Tables[1].Rows[x][15].ToString() != "" && ds.Tables[1].Rows[x][15].ToString() != null)
                                                {
                                                    Payment = ds.Tables[1].Rows[x][15].ToString();
                                                    if (Payment.Contains("โอน"))
                                                        PaymentNo = 2;
                                                }

                                                if (ds.Tables[1].Rows[x][16].ToString() != "" && ds.Tables[1].Rows[x][16].ToString() != null)
                                                    PayDate = Convert.ToDateTime(Convert.ToDateTime(ds.Tables[1].Rows[x][16].ToString()).ToString("yyyy/MM/dd"));

                                                //SQL RegLoan
                                                String LoanID = Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[2]
                                                        .Replace("{TeacherNoAdd}", Class.UserInfo.TeacherNo)
                                                        .Replace("{TeacherNo}", TeacherID)
                                                        .Replace("{MonthPay}", StartLoanMonth.ToString())
                                                        .Replace("{YearPay}", StartLoanYear.ToString())
                                                        .Replace("{LoanAmount}", AmountLoan.ToString())
                                                        .Replace("{PayNo}", PayNo.ToString())
                                                        .Replace("{InterestRate}", Interest.ToString())
                                                        .Replace("{DateAdd}", DateRegLoan.ToString())).Tables[0].Rows[0][0].ToString();
                                                //Add Guarantor
                                                for (int y = 0; y < CountGuarantor; y++)
                                                {
                                                    Class.SQLConnection.InputSQLMSSQL(SQLDefault[3]
                                                        .Replace("{LoanNo}", LoanID)
                                                        .Replace("{TeacherNo}", GuarantorID[y])
                                                        .Replace("{Amount}", GuarantorAmount[y].ToString())
                                                        .Replace("{RemainsAmount}", GuarantorAmount[y].ToString()));
                                                }
                                                //PayLoan
                                                if (ds.Tables[1].Rows[x][15].ToString() != "" && ds.Tables[1].Rows[x][15].ToString() != null ||
                                                    ds.Tables[1].Rows[x][16].ToString() != "" && ds.Tables[1].Rows[x][16].ToString() != null)
                                                {
                                                    Class.SQLConnection.InputSQLMSSQL(SQLDefault[4]
                                                        .Replace("{LoanID}", LoanID)
                                                        .Replace("{TeacherNoPay}", Class.UserInfo.TeacherNo)
                                                        .Replace("{PaymentNo}", PaymentNo.ToString())
                                                        .Replace("{TeacherNoAddBy}", Class.UserInfo.TeacherNo)
                                                        .Replace("{PayDate}", PayDate.ToString("yyyy/MM/dd")));
                                                }
                                            }
                                        }
                                        //WithDraw
                                        if (ds.Tables[2].Rows.Count != 0)
                                        {
                                            for (int x = 0; x < ds.Tables[2].Rows.Count; x++)
                                            {
                                                String TeacherID = "";
                                                int WithDrawAmount = 0;
                                                DateTime DateAdd = ToDay;
                                                String Payment = "เงินสด";
                                                int PaymentNo = 1;
                                                if (ds.Tables[2].Rows[x][0].ToString() == "" || ds.Tables[2].Rows[x][0].ToString() == null)
                                                    continue;
                                                else
                                                    TeacherID = ds.Tables[2].Rows[x][0].ToString();
                                                if (ds.Tables[2].Rows[x][1].ToString() != "" && ds.Tables[2].Rows[x][1].ToString() != null)
                                                    WithDrawAmount = Convert.ToInt32(ds.Tables[2].Rows[x][1].ToString());

                                                if (ds.Tables[2].Rows[x][2].ToString() != "" && ds.Tables[2].Rows[x][2].ToString() != null)
                                                    DateAdd = Convert.ToDateTime(Convert.ToDateTime(ds.Tables[2].Rows[x][2].ToString()).ToString("yyyy/MM/dd"));

                                                if (ds.Tables[2].Rows[x][3].ToString() != "" && ds.Tables[2].Rows[x][3].ToString() != null)
                                                    Payment = "โอน";
                                                if (Payment.Contains("โอน"))
                                                    PaymentNo = 2;
                                                Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[5]
                                            .Replace("{WithDraw}", WithDrawAmount.ToString())
                                            .Replace("{TeacherNoAddBy}", Class.UserInfo.TeacherNo)
                                            .Replace("{PayMent}", PaymentNo.ToString())
                                            .Replace("{DateAdd}", DateAdd.ToString()));
                                            }
                                        }
                                        Class.SQLConnection.InputSQLMSSQL(SQLDefault[6]);
                                        BExceltoSQL.Enabled = false;
                                        MessageBox.Show("ส่งข้อมูลสำเร็จ", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                                    }
                                }
                                catch
                                {
                                    MessageBox.Show("เกิดข้อผิดพลาด Format Excel ไม่ถูกต้องกรุณาลองอีกครั้ง", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                            catch
                            {
                                MessageBox.Show("กรุณาปิด Excel ก่อนทำรายการ", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                }
            }
        }
        private void CHB_edittime_CheckedChanged(object sender, EventArgs e)
        {
            int Checked = 0;
            if (CHB_edittime.Checked == true)
                Checked = 1;
            BankTeacher.Class.SQLConnection.InputSQLMSSQL(SQLDefault[8]
                .Replace("{DateAmountChange}", Checked.ToString()));
            BankTeacher.Bank.Menu.DateAmountChange = Checked;
        }

        private void Setting_KeyUp(object sender, KeyEventArgs e)
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
    }
}