using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankTeacher.Bank.Add_Member
{
    public partial class CancelMemberCloseTheLoan : Form
    {
        /// <summary> 
        /// SQLDefault 
        /// <para>[0] Table[0] GetLoan Detail & Table[1] Get Lastest Loan BillDetail INPUT: {LoanNo} </para> 
        /// <para>[1] Save Bill & Get BillNo INPUT: {TeacherNoaddby}  {TeacherNo} </para>
        /// <para>[2] Insert BillDetail (Loop) INPUT:  {BillNo}, {LoanNo} , {Amount} , {Month} , {Year} </para>
        /// <para>[3] Update Saving Amount & Guarantor Amount INPUT: {Amount} {TeacherNo} {LoanNo} </para>
        /// <para>[4] Get SavingAmount INPUT: {TeacherNo} </para>
        /// <para>[5] Update Guarantor Amount INPUT: {LoanNo} </para>
        /// </summary> 
        private String[] SQLDefault = new String[]
         { 
           //[0] Table[0] GetLoan Detail & Table[1] Get MonthPay , YearPay Table[2] Bill paid Table[3] Last Month INPUT: {LoanNo}
           "SELECT a.PayNo , a.LoanAmount , ROUND(a.InterestRate / 100 * a.LoanAmount , 0) , a.MonthPay , a.YearPay \r\n " +
          "FROM EmployeeBank.dbo.tblLoan as a  \r\n " +
          "WHERE a.LoanNo = {LoanNo}  \r\n " +
          " \r\n " +
          "SELECT a.MonthPay , a.YearPay  \r\n " +
          "FROM EmployeeBank.dbo.tblLoan as a  \r\n " +
          "WHERE a.LoanNo = {LoanNo} and a.LoanStatusNo = 2 \r\n " +
          " \r\n " +
          "SELECT CAST(b.Mount as NVARCHAR) + '/' + CAST(b.Year as NVARCHAR) \r\n " +
          "FROM EmployeeBank.dbo.tblBill as a  \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblBillDetail as b on a.BillNo = b.BillNo \r\n " +
          "WHERE a.Cancel = 1 and b.LoanNo = {LoanNo} \r\n " +
          " \r\n " +
          "SELECT CAST(MONTH(EOMONTH(CAST(CAST(a.YearPay as nvarchar) + '/' + CAST(a.MonthPay - 1 as nvarchar) + '/01' as date) , a.PayNo)) as nvarchar) + '/' + CAST(YEAR(EOMONTH(CAST(CAST(a.YearPay as nvarchar) + '/' + CAST(a.MonthPay - 1 as nvarchar) + '/01' as date) , a.PayNo)) as nvarchar) \r\n " +
          "FROM EmployeeBank.dbo.tblLoan as a \r\n " +
          "WHERE a.LoanNo = {LoanNo}"
           ,

           //[1] Save Bill & Get BillNo INPUT: {TeacherNoAddBy}  {TeacherNo}
           "DECLARE @BIllNO INT;   \r\n " +
          " \r\n " +
          "INSERT INTO EmployeeBank.dbo.tblBill (TeacherNoAddBy,TeacherNo,DateAdd,Cancel,TransactionDate)  \r\n " +
          "VALUES('{TeacherNoAddBy}','{TeacherNo}',CAST(CURRENT_TIMESTAMP as DATE),1,CURRENT_TIMESTAMP); \r\n " +
          "SET @BIllNO = SCOPE_IDENTITY();  \r\n " +
          " \r\n " +
          "SELECT @BIllNO ;"
           ,

           //[2] Insert BillDetail (Loop) INPUT:  {BillNo}, {LoanNo} , {Amount} , {Month} , {Year}
           "INSERT INTO EmployeeBank.dbo.tblBillDetail (BillNo,TypeNo,LoanNo,Amount,Mount,Year,BillDetailPaymentNo)   \r\n " +
          "VALUES ({BillNo},2,{LoanNo},{Amount},{Month},{Year},4);"
           ,

           //[3] Update Saving Amount INPUT: {Amount} {TeacherNo} 
           "UPDATE EmployeeBank.dbo.tblShare \r\n " +
          "SET SavingAmount = SavingAmount - {Amount} \r\n " +
          "WHERE TeacherNo = '{TeacherNo}'; \r\n " 
           ,
           //[4] Get SavingAmount INPUT: {TeacherNo} 
           "SELECT a.SavingAmount \r\n " +
          "FROM EmployeeBank.dbo.tblShare as a \r\n " +
          "WHERE a.TeacherNo = '{TeacherNo}'"
           ,
           //[5] Update Guarantor Amount INPUT: {LoanNo}
           "UPDATE EmployeeBank.dbo.tblGuarantor \r\n " +
           "SET Amount = 0 \r\n " +
           "WHERE LoanNo = {LoanNo}"
             ,
         };
        public CancelMemberCloseTheLoan()
        {
            InitializeComponent();
        }

        public String TeacherNo;
        bool CheckSave = false;

        private void CBLoanNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(CBLoanNo.SelectedIndex != -1)
            {
                CheckSave = false;
                DGV_Pay.Rows.Clear();
                LBalance_Pay.Text = "0";
                DataSet dsLoanDetailBillDetail = Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[0]
                     .Replace("{LoanNo}", CBLoanNo.SelectedItem.ToString()));
                if(dsLoanDetailBillDetail.Tables[1].Rows.Count != 0)
                {
                    int Amount = Convert.ToInt32(dsLoanDetailBillDetail.Tables[0].Rows[0][1]), Interest = Convert.ToInt32(dsLoanDetailBillDetail.Tables[0].Rows[0][2]);
                    int Year = Convert.ToInt32(dsLoanDetailBillDetail.Tables[1].Rows[0][1]);
                    int PayNo = Convert.ToInt32(dsLoanDetailBillDetail.Tables[0].Rows[0][0]);
                    int Pay = Convert.ToInt32((Amount + Interest) / PayNo);
                    int SumPay = 0;
                    int Month = Convert.ToInt32(dsLoanDetailBillDetail.Tables[1].Rows[0][0]);

                    for (int x = 0; x < PayNo; x++)
                    {
                        bool CheckMonthSame = true;
                        
                        if (Month == 13)
                        {
                            Month = 1;
                            Year++;
                        }
                        for(int a = 0; a < dsLoanDetailBillDetail.Tables[2].Rows.Count; a++)
                        {
                            if($"{Month}/{Year}" == dsLoanDetailBillDetail.Tables[2].Rows[a][0].ToString())
                            {
                                CheckMonthSame = false;
                                break;
                            }
                        }
                        if(dsLoanDetailBillDetail.Tables[3].Rows[0][0].ToString() == $"{Month}/{Year}")
                            Pay = (Amount + Interest) - Pay * (PayNo - 1);


                        if (CheckMonthSame)
                        {
                            DGV_Pay.Rows.Add($"{Month}/{Year}", "รายการกู้", Pay, Month, Year);
                            SumPay += Pay;
                        }
                        Month++;
                    }
                    LBalance_Pay.Text = SumPay.ToString();
                }
            }
        }

        private void BSave_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("การจ่ายจะหักเงินเก็บสะสมจากระบบ ยืนยันการจ่ายหรือไม่" , "แต้งเตือน" , MessageBoxButtons.YesNo , MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                DataTable dtSavingAmount = Class.SQLConnection.InputSQLMSSQL(SQLDefault[4]
                .Replace("{TeacherNo}", TeacherNo));
                if (Convert.ToInt32(dtSavingAmount.Rows[0][0].ToString()) >= Convert.ToInt32(LBalance_Pay.Text))
                {
                    //try
                    //{
                        Class.SQLConnection.InputSQLMSSQL(SQLDefault[3]
                            .Replace("{Amount}", LBalance_Pay.Text)
                            .Replace("{TeacherNo}", TeacherNo));

                        Class.SQLConnection.InputSQLMSSQL(SQLDefault[5]
                            .Replace("{LoanNo}", CBLoanNo.SelectedItem.ToString()));

                        DataTable dtBillNo = Class.SQLConnection.InputSQLMSSQL(SQLDefault[1]
                            .Replace("{TeacherNoAddBy}", Class.UserInfo.TeacherNo)
                            .Replace("{TeacherNo}", TeacherNo));

                        for (int a = 0; a < DGV_Pay.Rows.Count; a++)
                        {
                            //{BillNo}, {LoanNo} , {Amount} , {Month} , {Year}
                            Class.SQLConnection.InputSQLMSSQL(SQLDefault[2]
                                .Replace("{BillNo}", dtBillNo.Rows[0][0].ToString())
                                .Replace("{LoanNo}", CBLoanNo.SelectedItem.ToString())
                                .Replace("{Amount}", DGV_Pay.Rows[a].Cells[2].Value.ToString())
                                .Replace("{Month}", DGV_Pay.Rows[a].Cells[3].Value.ToString())
                                .Replace("{Year}", DGV_Pay.Rows[a].Cells[4].Value.ToString()));
                        }
                        MessageBox.Show("ทำรายการสำเร็จ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        CheckSave = true;
                        CBLoanNo.Items.RemoveAt(CBLoanNo.SelectedIndex);
                    //}
                    //catch (Exception ex)
                    //{
                    //    MessageBox.Show("การบันทึกล้มเหลว", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //    Console.WriteLine($"--------------------------{ex}---------------------------");
                    //}
                }
                else
                {
                    MessageBox.Show("ยอดเงินไม่เพียงพอ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            
        }

        private void BExitForm_Click(object sender, EventArgs e)
        {
            FormCollection fc = Application.OpenForms;
            if (fc.Count > 2)
            {
                foreach (Form f in fc)
                {
                    if (f.Name == "Menu")
                    {
                        f.Enabled = true;
                        f.Show();
                        break;
                    }
                }
                this.Close();
            }
            else
                BankTeacher.Class.FromSettingMedtod.ReturntoHome(this);
            //for (int a = 0; a < Application.OpenForms.Count; a++)
            //{
            //    if (Application.OpenForms[a].Name == "CancelMember")
            //    {
            //        Application.OpenForms[a].Enabled = true;
            //        Application.OpenForms[a].Show();
            //        CBLoanNo.DroppedDown = false;
            //        this.Close();
            //    }
            //}
        }

        private void CancelMemberCloseTheLoan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape || (CheckSave && e.KeyCode == Keys.Enter))
            {
                if (CBLoanNo.SelectedIndex != -1)
                {
                    CheckSave = false;
                    DGV_Pay.Rows.Clear();
                    LBalance_Pay.Text = "0";
                    CBLoanNo.DroppedDown = false;
                    CBLoanNo.SelectedIndex = -1;
                    //RemoveClickEvent(CBYearSelection_BillInfo);

                }
                else
                {
                    BExitForm_Click(new object(), new EventArgs());
                }
            }
        }
    }
}
