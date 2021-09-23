using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static example.Class.ProtocolSharing.ConnectSMB;

namespace example.Bank.Loan
{
    public partial class loan : Form
    {
        //------------------------- index -----------------
        string name = "", id = "";
        int StatusBoxFile = 0;
        String imgeLocation = "";
        int Check = 0;
        public static int SelectIndexRowDelete;

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
        /// <para>[1] SELECT Guarantor Credit Limit INPUT:T{TeacherNo} , {TeacherNoNotLike} </para>
        /// <para>[2] SELECT Date Data </para>
        /// <para>[3] INSERT Loan and Get LoanNo INPUT: {TeacherNoAdd}, {TeacherNo}, {MonthPay}, {YearPay}, {LoanAmount}, {PayNo}, {InterestRate}</para>
        /// <para>[4] INSERT Guarantor INPUT: {LoanNo},{TeacherNo},{Amount},{RemainsAmount}</para>
        /// <para>[5] Detail Loan Print  INPUT: TeacherNo</para>
        /// <para>[6] SELECT MemberLona  INPUT: {TeacherNo} </para>
        /// <para>[7] Delete Loan INPUT: NO </para>
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

            //[1] SELECT CreditLimit Data INPUT:T{TeacherNo} , {TeacherNoNotLike}
            "SELECT TeacherNo, Name, RemainAmount \r\n " +
            "FROM (SELECT a.TeacherNo , CAST(c.PrefixName+' '+Fname +' '+ Lname as NVARCHAR)AS Name,  \r\n " +
            "ISNULL(e.SavingAmount,0) - ISNULL(SUM(d.RemainsAmount),0) as RemainAmount, Fname \r\n " +
            "FROM EmployeeBank.dbo.tblMember as a  \r\n " +
            "LEFT JOIN Personal.dbo.tblTeacherHis as b ON a.TeacherNo = b.TeacherNo  \r\n " +
            "LEFT JOIN BaseData.dbo.tblPrefix as c ON b.PrefixNo = c.PrefixNo  \r\n " +
            "LEFT JOIN EmployeeBank.dbo.tblGuarantor as d on a.TeacherNo = d.TeacherNo \r\n " +
            "LEFT JOIN EmployeeBank.dbo.tblShare as e ON e.TeacherNo = a.TeacherNo \r\n " +
            "WHERE a.TeacherNo LIKE 'T{TeacherNo}%' and a.MemberStatusNo = 1 {TeacherNoNotLike}\r\n " +
            "GROUP BY a.TeacherNo , CAST(c.PrefixName+' '+Fname +' '+ Lname as NVARCHAR), e.SavingAmount, Fname) as a \r\n " +
            "WHERE RemainAmount >= 500 \r\n " +
            "ORDER BY a.Fname; "
            , 



            //[2] SELECT Date Data
            "SELECT CAST(CURRENT_TIMESTAMP as DATE); \r\n\r\n",

            //[3] INSERT Loan and Get LoanNo INPUT: {TeacherNoAdd}, {TeacherNo}, {MonthPay}, {YearPay}, {LoanAmount}, {PayNo}, {InterestRate}
            "DECLARE @LoanNo INT;\r\n" +
            "INSERT INTO EmployeeBank.dbo.tblLoan\r\n" +
            "(TeacherNoAddBy, TeacherNo, MonthPay, YearPay, LoanAmount, PayNo, InterestRate, DateAdd)\r\n" +
            "VALUES ('{TeacherNoAdd}', '{TeacherNo}', {MonthPay}, {YearPay}, {LoanAmount}, {PayNo}, {InterestRate}, CAST(CURRENT_TIMESTAMP as DATE));\r\n" +
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
            "SELECT a.TeacherNo,CAST(d.PrefixName+' '+Fname +' '+ Lname as NVARCHAR)AS Name,LoanAmount , \r\n " +
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
            "SELECT a.TeacherNo,CAST(c.PrefixName+''+b.Fname+''+b.Lname as NVARCHAR),d.StartAmount  \r\n "+
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

        };

        //----------------------- PullSQLDate -------------------- ////////
        // ดึงขอมูลวันที่จากฐานข้อมูล
        int Month;
        private void Loan_Load(object sender, EventArgs e)
        {

            int Year = Convert.ToInt32(example.GOODS.Menu.Date[0]);
            Month = Convert.ToInt32(example.GOODS.Menu.Date[1]);


            for (int Num = 0; Num < 5; Num++)
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
            for (int Num = 0; Num < DGVGuarantorCredit.Rows.Count; Num++)
            {
                if (Double.TryParse(DGVGuarantorCredit.Rows[Num].Cells[3].Value.ToString(), out Double CreditPercent))
                {
                    SumPercentGuarantor += Convert.ToInt32(CreditPercent);
                }

            }
            if (TBTeacherNo.Text != "" && CBPayMonth.SelectedIndex != -1 && CBPayYear.SelectedIndex != -1 &&
                TBLoanAmount.Text != "" && TBPayNo.Text != "" && TBInterestRate.Text != "" && DGVGuarantor.Rows.Count == 4 && ((SumPercentGuarantor >= int.Parse(TBLoanAmount.Text)) || UserOutCreditLimit == DialogResult.Yes) &&
                Convert.ToInt32(LLackAmount.Text) == 0 && Convert.ToInt32(LOutCredit.Text) == 0 && Int32.TryParse(TBLoanAmount.Text, out int x ) && x >= example.GOODS.Menu.MinLoan)
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
                BPrintLoanDoc.Enabled = true;
                TBLoanNo.Text = LoanNo;
                //DGVGuarantor.Rows.Clear();
                //DGVGuarantorCredit.Rows.Clear();
                //DGVLoanDetail.Rows.Clear();
                //CBPayMonth.SelectedIndex = -1;
                //CBPayYear.SelectedIndex = -1;
                //TBLoanAmount.Text = "";
                //tabControl1.SelectedIndex = 0;
                //LGuarantorAmount.Text = "0/4";
                //LLoanAmount.Text = "( )";
                //BTOpenfile.Enabled = true;
                //BPrintLoanDoc.Enabled = true;

            }
            else
            {
                MessageBox.Show("โปรดใสข้อมุลให้ครบก่อนบันทึก", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


        }
        //----------------------- End code -------------------- ////////

        //------------------------- Pull SQL Member & CheckTBTeacherNo ---------
        // ค้นหารายชชื่อผู้สมัครสมาชิกครูสหกร์จากฐานข้อมูล

        //int RowDGV;
        //----------------------- End code -------------------- ////////

        //----------------------- DatagridView -------------------- ////////
        //-------------------- End code -------------------- ////////

        //----------------------- INNERTNumber in Labal -------------------- ////////
        // Comment!
        bool IsInt(float x)
        {
            try
            {
                int y = Int16.Parse(x.ToString());
                return true;
            }
            catch
            {
                return false;
            }
        }
        // Comment!
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 3 && (CBPayMonth.SelectedIndex != -1
                && CBPayYear.SelectedIndex != -1 && TBPayNo.Text != "" && TBInterestRate.Text != ""))
            {
                if (TBLoanAmount.Text != "" && Convert.ToInt32(TBLoanAmount.Text) >= example.GOODS.Menu.MinLoan)
                {
                    DGVLoanDetail.Rows.Clear();
                    int Month = int.Parse(CBPayMonth.Text),
                        Year = int.Parse(CBPayYear.Text);

                    Double Interest = (Convert.ToDouble(TBLoanAmount.Text) * (Convert.ToDouble(TBInterestRate.Text) / 100)) / Convert.ToDouble(TBPayNo.Text);

                    int Pay = Convert.ToInt32(TBLoanAmount.Text) / Convert.ToInt32(TBPayNo.Text);
                    int SumInstallment = Convert.ToInt32(Pay + Interest);

                    for (int Num = 0; Num < int.Parse(TBPayNo.Text); Num++)
                    {
                        if (Month > 12)
                        {
                            Month = 1;
                            Year++;
                        }
                        if (Num == Convert.ToInt32(TBPayNo.Text) - 1)
                        {
                            Interest = Convert.ToInt32((Convert.ToDouble(TBLoanAmount.Text) * (Convert.ToDouble(TBInterestRate.Text) / 100)) - (Convert.ToInt32(Interest) * Num));
                            Pay = Pay * Num;
                            Pay = Convert.ToInt32(TBLoanAmount.Text) - Pay;
                            SumInstallment = Convert.ToInt32(Pay + Interest);
                        }
                        DGVLoanDetail.Rows.Add($"{Month}/{Year}", Pay, Convert.ToInt32(Interest), SumInstallment);
                        Month++;
                    }

                }
                else
                {
                    tabControl1.SelectedIndex = 1;
                    TBLoanAmount.Focus();
                }

            }
            if(tabControl1.SelectedIndex == 2)
            {
                try
                {
                    if(Convert.ToInt32(TBLoanAmount.Text) < example.GOODS.Menu.MinLoan && TBLoanAmount.Text == "")
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
        //----------------------- End code -------------------- ////////

        //----------------------- select pay date -------------------- ////////
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
                    //NotLike = NotLike.Remove(NotLike.Length - 1);
                }
                IN = new Bank.Search(SQLDefault[1]
                       .Replace("{TeacherNo}", "")
                       .Replace("{TeacherNoNotLike}", NotLike));

                IN.ShowDialog();
                if (Bank.Search.Return[0] != "")
                {
                    TBTeacherNo.Text = Bank.Search.Return[0];
                    TBTeacherNo_KeyDown(sender, new KeyEventArgs(Keys.Enter));
                }
            }
            catch (Exception x)
            {
                Console.WriteLine(x);
            }
        }
        private void BSearchTeacher2_Click(object sender, EventArgs e)
        {
            Bank.Search IN;
            try
            {
                IN = new Bank.Search(SQLDefault[6]
                     .Replace("{TeacherNo}", ""));
                IN.ShowDialog();
                name = Bank.Search.Return[0];
                //TBTeacherNo_KeyDown(sender, new KeyEventArgs(Keys.Enter));
                if (name.Length == 6)
                {
                    DataTable dt = Class.SQLConnection.InputSQLMSSQL(SQLDefault[6].Replace("T{TeacherNo}%", name));
                    if (dt.Rows.Count != 0)
                    {
                        TBTeacherNamePrint.Text = dt.Rows[0][1].ToString();
                        id = dt.Rows[0][0].ToString();
                        BPrintLoanDoc.Enabled = true;
                        BTOpenfile.Enabled = true;
                    }

                }
            }
            catch (Exception x)
            {
                Console.WriteLine(x);
            }
        }
        private void BTdeleteText_Click(object sender, EventArgs e)
        {
            TBTeacherNamePrint.Clear();
            BPrintLoanDoc.Enabled = false;
            BTOpenfile.Enabled = false;
            label9.Text = "Scan(  ไม่พบ  )";

        }
        //TB ใส่ ID คนกู้ มี event การกด
        private void TBTeacherNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && TBTeacherNo.Text.Length == 6)
            {
                DataTable dt = Class.SQLConnection.InputSQLMSSQL(SQLDefault[1].Replace("T{TeacherNo}", TBTeacherNo.Text)
                    .Replace("{TeacherNoNotLike}", ""));
                if (dt.Rows.Count != 0)
                {
                    TBTeacherName.Text = dt.Rows[0][1].ToString();
                    TBLoanNo.Text = "-";
                    TBLoanStatus.Text = "ดำเนินการ";
                    TBSavingAmount.Text = dt.Rows[0][2].ToString();

                    int credit;

                    //DataSet ds = Class.SQLConnection.InputSQLMSSQLDS(
                    //    SQLDefault[1].Replace("T{TeacherNo}", TBTeacherNo.Text));

                    //DataTable dtGuarantorCredit = ds.Tables[0];
                    //String aa = dtGuarantorCredit.Rows[0][2].ToString();
                    //if (dtGuarantorCredit.Rows.Count != 0/* && dtTeacherName.Rows.Count != 0*/)
                    //{
                    credit = int.Parse(dt.Rows[0][2].ToString());
                    //float Percent = 100 / DGVGuarantor.Rows.Count;
                    DGVGuarantorCredit.Rows.Clear();
                    DGVGuarantor.Rows.Clear();
                    DGVGuarantor.Rows.Add(dt.Rows[0][0], dt.Rows[0][1], credit);
                    TBSavingAmount.Text = credit.ToString();
                    tabControl1.SelectedIndex = 0;
                    //}
                    //else
                    //{
                    //    MessageBox.Show("ไม่พบข้อมูล", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //    TBTeacherNo.Text = "";
                    //    TBTeacherName.Text = "";
                    //    TBSavingAmount.Text = "";
                    //    TBPayNo.Text = "";
                    //    TBLoanNo.Text = "";
                    //    TBLoanAmount.Text = "";
                    //    TBInterestRate.Text = "";
                    //    TBGuarantorNo.Text = "";
                    //    TBTeacherNo.Focus();
                    //}
                    TBGuarantorNo.Focus();
                    Check = 1;

                }
                else
                {
                    MessageBox.Show("รหัสไม่ถูกต้อง หรือยอดเงินที่ค้ำได้ไม่เพียงพอ", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    TBTeacherNo.Text = "";
                    TBTeacherNo.Focus();

                }

            }
            else if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
            {
                if (Check == 1)
                {
                    TBTeacherName.Text = "";
                    TBLoanNo.Text = "";
                    TBLoanStatus.Text = "";
                    TBSavingAmount.Text = "";
                    DGVGuarantor.Rows.Clear();
                    DGVGuarantorCredit.Rows.Clear();
                    Check = 0;
                    BTOpenfile.Enabled = false;
                    BPrintLoanDoc.Enabled = false;
                }

            }
        }
        //TB คำค้ำ event กด
        private void TBGuarantorNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                String NotLike = "";

                //for (int Num = 0; Num < DGVGuarantor.Rows.Count; Num++)
                //{
                //    String aa = DGVGuarantor.Rows[Num].Cells[1].Value.ToString();
                //    CheckTeacherNo = TBGuarantorNo.Text.Contains(DGVGuarantor.Rows[Num].Cells[0].Value.ToString());
                //    if (CheckTeacherNo)
                //        break;
                //}
                if (DGVGuarantor.Rows.Count < 4) /*& (CheckTeacherNo == false)*/
                {

                    for (int Num = 0; Num < DGVGuarantor.Rows.Count; Num++)
                    {
                        NotLike += " and a.TeacherNo NOT LIKE " + $"'{DGVGuarantor.Rows[Num].Cells[0].Value.ToString()}'";
                    }
                    //NotLike = NotLike.Remove(NotLike.Length - 1);

                    DataSet ds = Class.SQLConnection.InputSQLMSSQLDS(
                        SQLDefault[1]
                        .Replace("T{TeacherNo}", TBGuarantorNo.Text)
                        .Replace("{TeacherNoNotLike}", NotLike));
                    DataTable dataTable = ds.Tables[0];
                    DataTable dtRemainAmount = dataTable;
                    if (dtRemainAmount.Rows.Count != 0)
                    {

                        DGVGuarantor.Rows.Add(dtRemainAmount.Rows[0][0].ToString(),
                            dtRemainAmount.Rows[0][1].ToString(),
                            int.Parse(dtRemainAmount.Rows[0][2].ToString()));
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
        //----------------------- End code -------------------- ////////

        //----------------------- select pay EventKey -------------------- ////////
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
        //----------------------- Printf -------------------- ////////
        // พิมพ์เอกสารกู้
        private void BPrintLoanDoc_Click_2(object sender, EventArgs e)
        {
            label9.Text = "Scan(  พบไฟล์  )";
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }
        // อัพเอกสารส่ง เซิร์ฟเวอร์
        private void BTOpenfile_Click(object sender, EventArgs e)
        {
            if (StatusBoxFile == 0)
            {

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
                        BTOpenfile.Text = "ส่งไฟล์";
                        StatusBoxFile = 1;
                        label6.Text = "Scan(  พบไฟล์  )";
                    }

                }
                catch
                {
                    MessageBox.Show("An Error Occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (StatusBoxFile == 1)
            {
                var smb = new SmbFileContainer("Loan");
                if (smb.IsValidConnection())
                {
                    String Return = smb.SendFile(imgeLocation, "Loan_" + TBTeacherNo.Text + ".pdf");
                    MessageBox.Show(Return, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    StatusBoxFile = 0;
                    BTOpenfile.Text = "เปิดไฟล์";
                    label6.Text = "Scan(  ไม่พบ  )";
                    imgeLocation = "";
                }
                else
                {
                    MessageBox.Show("ไม่สามารถสร้างไฟล์ในที่นั้นได้", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        // กระดาษปริ้น
        private void printDocument1_PrintPage_1(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Class.Print.PrintPreviewDialog.PrintLoan(e, SQLDefault[5].Replace("{TeacherNo}", TBTeacherNo.Text), example.GOODS.Menu.Date[2], example.GOODS.Menu.Monthname, (Convert.ToInt32(example.GOODS.Menu.Date[0]) + 543).ToString(), TBTeacherNo.Text,TBLoanNo.Text);
            //e.HasMorePages = true;
            //Class.Print.PrintPreviewDialog.ExamplePrint(sender,e);


        }
        //----------------------- End Printf -------------------- ////////

        // ------------------------ not working --------------
        // โค้ดที่ไม่ได้ใช้งานหรือเก็บไว้ศึกษาต่อ
        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("เดี๋ยวใส่ตอนรวมโปรแกรมครับ", "System", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading, true);
        }

        private void DGVGuarantor_MouseClick(object sender, MouseEventArgs e)
        {
            //kkkk
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
                    IN = new Bank.Search(SQLDefault[1]
                           .Replace("{TeacherNo}", "")
                           .Replace("{TeacherNoNotLike}", NotLike));

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
            CreditLoanAmount = 0;
            LGuarantorAmount.Text = DGVGuarantor.Rows.Count + "/4";

            for (int Count = 0; Count < DGVGuarantor.Rows.Count; Count++)
            {
                CreditLoanAmount += int.Parse(DGVGuarantor.Rows[Count].Cells[2].Value.ToString());
            }
            //Double aa = Convert.ToDouble(1) / Convert.ToDouble(100);
            int LoanAmount = Convert.ToInt32(CreditLoanAmount - CreditLoanAmount * (Convert.ToDouble(TBInterestRate.Text) / 100));
            LLoanAmount.Text = "(" + LoanAmount.ToString() + ")";

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
            DGVGuarantorCredit.Rows.RemoveAt(e.RowIndex);
        }
        private void DGVGuarantor_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            DGVGuarantorCredit.Rows.Add(DGVGuarantor.Rows[e.RowIndex].Cells[0].Value, DGVGuarantor.Rows[e.RowIndex].Cells[1].Value, "0", "0", DGVGuarantor.Rows[e.RowIndex].Cells[2].Value);
        }

        DialogResult UserOutCreditLimit = DialogResult.No;
        private void TBLoanAmount_Leave(object sender, EventArgs e)
        {
            bool CheckNum = Double.TryParse(TBLoanAmount.Text, out Double LoanAmount);
            LoanAmount = LoanAmount * Convert.ToDouble((Convert.ToDouble(TBInterestRate.Text) / 100)) + LoanAmount;
            LTotal.Text = LoanAmount.ToString();
            if (Int32.TryParse(TBLoanAmount.Text, out int x) && x >= example.GOODS.Menu.MinLoan)
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

                if (DGVGuarantor.Rows.Count != 0)
                {
                    int Amount;
                    String AmountLimit = LLoanAmount.Text.Remove(0, 1);
                    AmountLimit = AmountLimit.Remove(AmountLimit.Length - 1);
                    bool Check = int.TryParse(AmountLimit, out int LimitAmount);
                    if (int.TryParse(TBLoanAmount.Text, out Amount) && (Check))
                    {
                        if (Amount > LimitAmount)
                        {
                            UserOutCreditLimit = MessageBox.Show("จำนวนเงินกู้ เกินกำหนดเงินค้ำ\r\n ต้องการทำต่อหรือไม่", "แจ้งเตือน", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            if (UserOutCreditLimit == DialogResult.No)
                            {
                                TBLoanAmount.Text = "";
                                TBLoanAmount.Focus();
                            }
                        }
                    }
                    else if (!Check)
                    {
                        TBTeacherNo.Focus();
                    }
                }
                else if (DGVGuarantor.Rows.Count == 0)
                {
                    MessageBox.Show("โปรดเลือกผู้กู้ ผู้ค้ำก่อน", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tabControl1.SelectedIndex = 0;
                    TBTeacherNo.Focus();
                }
            }
            else if (Int32.TryParse(TBLoanAmount.Text, out int y))
            {
                MessageBox.Show("ยอดกู้ต่ำกว่ากำหนดกรุณาลองใหม่อีกครั้ง","System",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                TBLoanAmount.Focus();
                TBLoanAmount.Text = "";
                tabControl1.SelectedIndex = 1;
            }

        }

        List<String[]> DGVRow = new List<String[]> { };
        private void DGVGuarantorCredit_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            int NumCell = 0;
            if (TBLoanAmount.Text != "" && TBInterestRate.Text != "" && TBPayNo.Text != "")
            {
                Double Total = (Convert.ToDouble(TBLoanAmount.Text) * (Convert.ToDouble(TBInterestRate.Text) / 100) + Convert.ToDouble(TBLoanAmount.Text));
                bool Check;
                try
                {
                    NumCell = int.Parse(DGVGuarantorCredit.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                    Check = true;
                }
                catch
                {
                    Check = false;
                }

                if (e.ColumnIndex == 2 && Check)
                {
                    Double Credit = Convert.ToDouble(NumCell) / 100 * Total;
                    if (Credit <= int.Parse(DGVGuarantor.Rows[e.RowIndex].Cells[2].Value.ToString()))
                    {
                        DGVGuarantorCredit.Rows[e.RowIndex].Cells[3].Value = Convert.ToInt32(Credit);
                    }
                    else
                    {
                        MessageBox.Show("เกินวงเงินที่ค้ำได้ จะปรับเป็นยอดค้ำสูงสุด", "ระบบ");
                        DGVGuarantorCredit.Rows[e.RowIndex].Cells[3].Value = Convert.ToDouble(DGVGuarantor.Rows[e.RowIndex].Cells[2].Value.ToString());
                        //Double Interest = (Convert.ToDouble(TBLoanAmount.Text) * (Convert.ToDouble(TBInterestRate.Text) / 100)) + Convert.ToDouble(TBLoanAmount.Text);
                        DGVGuarantorCredit.Rows[e.RowIndex].Cells[2].Value = Convert.ToInt32((Convert.ToDouble(DGVGuarantorCredit.Rows[e.RowIndex].Cells[3].Value.ToString()) * 100 / Total));
                    }
                }
                else if (e.ColumnIndex == 3 && Check)
                {
                    if (NumCell <= int.Parse(DGVGuarantor.Rows[e.RowIndex].Cells[2].Value.ToString()))
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
                    else
                    {
                        MessageBox.Show("เกินวงเงินที่ค้ำได้ จะปรับเป็นยอดค้ำสูงสุด", "ระบบ");
                        DGVGuarantorCredit.Rows[e.RowIndex].Cells[3].Value = Convert.ToDouble(DGVGuarantor.Rows[e.RowIndex].Cells[2].Value.ToString());
                        //Double Interest = (Convert.ToDouble(TBLoanAmount.Text) * (Convert.ToDouble(TBInterestRate.Text) / 100)) + Convert.ToDouble(TBLoanAmount.Text);
                        DGVGuarantorCredit.Rows[e.RowIndex].Cells[2].Value = Convert.ToInt32((Convert.ToDouble(DGVGuarantorCredit.Rows[e.RowIndex].Cells[3].Value.ToString()) * 100 / Total));

                    }
                }
            }
            else
            {
                DGVGuarantorCredit.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "";
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
                LOutCredit.Text = SumCreditEdit + "";
            }
            else
            {
                LLackAmount.ForeColor = Color.Green;
                LOutCredit.ForeColor = Color.Green;
                LLackAmount.Text = SumCreditEdit + "";
                LOutCredit.Text = SumCreditEdit + "";
            }
        }

        private void DGVGuarantorCredit_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (DGVGuarantorCredit.CurrentCell.ColumnIndex >= 2)
            {
                TextBox tb = (TextBox)e.Control;
                tb.KeyPress += new KeyPressEventHandler(TBCellGuarantorCredit_KeyPress);
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
            BTOpenfile.Text = "เปิดไฟล์";
            label6.Text = "Scan(  ไม่พบ  )";
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
        }

        private void TBInterestRate_Leave(object sender, EventArgs e)
         {
            //Double aa = (Convert.ToDouble(TBInterestRate.Text) / 100);
            if(Double.TryParse(TBInterestRate.Text , out Double Interestrate))
            {
                int LoanAmount = Convert.ToInt32(CreditLoanAmount - CreditLoanAmount * (Interestrate / 100));
                LLoanAmount.Text = "(" + LoanAmount + ")";
                LTotal.Text = "" + LoanAmount;

                TBLoanAmount_Leave(sender, new EventArgs());
            }
            else
            {
                MessageBox.Show("ใสจำนวนเปอร์เซ็นต์ไม่ถูกต้อง");
                TBInterestRate.Text = "";
            }
        }

        private void Delete_Click_1(object sender, EventArgs e)
        {
            //String DeleteLoan = "DELETE FROM EmployeeBank.dbo.tblLoan";
            //String DeleteGuarantor = "DELETE FROM EmployeeBank.dbo.tblGuarantor";
            Class.SQLConnection.InputSQLMSSQL(SQLDefault[7]);
        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

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
                            Result = Difference/* / (DGVGuarantorCredit.Rows.Count - Num)*/;
                        }
                        if(Convert.ToInt32(LLackAmount.Text) < 0)
                        {
                            if (Convert.ToInt32(Convert.ToDouble(DGVGuarantorCredit.Rows[Num].Cells[3].Value.ToString()) + Result) <= 0)
                            {
                                int ChangeMinus = Convert.ToInt32(Interest * 0.01);
                                //ChangeMinus += Convert.ToInt32(Result) * -1;
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
                    LOutCredit.Text = SumAmountCredit + "";
                }
                else
                {
                    LLackAmount.ForeColor = Color.Green;
                    LOutCredit.ForeColor = Color.Green;
                    LLackAmount.Text = SumAmountCredit + "";
                    LOutCredit.Text = SumAmountCredit + "";
                }
                
            }
        }
    }
}


