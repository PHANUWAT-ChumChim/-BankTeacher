using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankTeacher.Bank.Pay
{
    public partial class CancelBill : Form
    {
        public CancelBill()
        {
            InitializeComponent();
        }
        bool CheckInputBill = false;
        /// <summary> 
        /// SQLDafault 
        /// <para>[0] Select Bill (CancelBill) INPUT: {BillNo}</para>
        /// <para>[1] Update Cancel Bill INPUT: {BillNo} {--} {---} </para>
        /// <para>[2] Update Saving (CancelBill) INPUT: {TeacherNo} {Amount}</para>
        /// <para>[3] + RemainAmount In Guarantor (CancelBill) INPUT: {LoanNo} , {LoanAmount}</para>
        /// <para>[4] datagrind  Select billType INPUT: {Mount} {Year} {Type}  </para>
        /// </summary> 
        private String[] SQLDefault = new String[]
        {
              //[0] Select Bill (CancelBill) INPUT: {BillNo} 
           "SELECT CAST(a.DateAdd as date),a.TeacherNo,CAST(ISNULL(e.PrefixName,'') +  Fname + ' ' + LName as nvarchar(255))as Name ,b.Year ,b.Mount , TypeName,LoanNo , b.Amount \r\n " +
          "FROM EmployeeBank.dbo.tblBill as a \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblBillDetail as b on a.BillNo = b.BillNo \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblMember as c on a.TeacherNo = c.TeacherNo \r\n " +
          "LEFT JOIN Personal.dbo.tblTeacherHis as d on a.TeacherNo = d.TeacherNo \r\n " +
          "LEFT JOIN BaseData.dbo.tblPrefix as e on d.PrefixNo = e.PrefixNo \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblBillDetailType as f on b.TypeNo = f.TypeNo \r\n " +
          "WHERE  a.BillNo = {BillNo} and MemberStatusNo != 2 and Cancel = 1 \r\n" +

          "SELECT a.TeacherNoAddBy,CAST(e.PrefixName+''+d.Fname+''+d.Lname as nvarchar) \r\n" +
          "FROM EmployeeBank.dbo.tblBill as a \r\n" +
          "LEFT JOIN Personal.dbo.tblTeacherHis as d on a.TeacherNoAddBy = d.TeacherNo \r\n" +
          "LEFT JOIN BaseData.dbo.tblPrefix as e on d.PrefixNo = e.PrefixNo \r\n" +
          "WHERE a.TeacherNoAddBy = CAST(a.TeacherNoAddBy as nvarchar) and a.BillNo = {BillNo};"
            ,
            //[1] Update Cancel Bill INPUT: {BillNo} {--} {---}
          "SELECT  CAST(c.Mount as nvarchar)+'/'+CAST(c.Year as nvarchar),CAST(MONTH(b.DateAdd) as nvarchar)+'/'+CAST(YEAR(b.DateAdd) as nvarchar)  \r\n" +
          "FROM EmployeeBank.dbo.tblBill as a  \r\n" +
          "LEFT JOIN EmployeeBank.dbo.tblMember b on a.TeacherNo = b.TeacherNo  \r\n" +
          "LEFT JOIN EmployeeBank.dbo.tblBillDetail as c on a.BillNo = c.BillNo  \r\n" +
          "{--}WHERE CAST(c.Mount as nvarchar)+'/'+CAST(c.Year as nvarchar) != CAST(MONTH(b.DateAdd) as nvarchar)+'/'+CAST(YEAR(b.DateAdd) as nvarchar) and a.BillNo = {BillNo}  \r\n" +
          "{---}WHERE a.BillNo = {BillNo}  \r\n" +


          "Update EmployeeBank.dbo.tblBill  \r\n" +
          "SET Cancel = 2  \r\n" +
          "FROM EmployeeBank.dbo.tblBill as a  \r\n" +
          "LEFT JOIN EmployeeBank.dbo.tblMember b on a.TeacherNo = b.TeacherNo  \r\n" +
          "LEFT JOIN EmployeeBank.dbo.tblBillDetail as c on a.BillNo = c.BillNo  \r\n" +
          "{--}WHERE CAST(c.Mount as nvarchar)+'/'+CAST(c.Year as nvarchar) != CAST(MONTH(b.DateAdd) as nvarchar)+'/'+CAST(YEAR(b.DateAdd) as nvarchar) and a.BillNo = {BillNo}   \r\n" +
          "{---}WHERE a.BillNo = {BillNo}"
            ,
           //[2] Update Saving (CancelBill) INPUT: {TeacherNo} {Amount}
           "DECLARE @@SavingAmount INT; \r\n " +
          " \r\n " +
          "SET @@SavingAmount = (SELECT SavingAmount \r\n " +
          "FROM EmployeeBank.dbo.tblShare \r\n " +
          "WHERE TeacherNo = '{TeacherNo}') \r\n " +
          " \r\n " +
          "UPDATE EmployeeBank.dbo.tblShare \r\n " +
          "SET SavingAmount = @@SavingAmount - {Amount} \r\n " +
          "WHERE  TeacherNo = '{TeacherNo}'"
           ,
           //[3] + RemainAmount In Guarantor (CancelBill) INPUT: {LoanNo} , {LoanAmount}
           "UPDATE EmployeeBank.dbo.tblGuarantor  \r\n " +
          "SET RemainsAmount = EmployeeBank.dbo.tblGuarantor.RemainsAmount + (SELECT ((a.Amount * 100 ) / (b.LoanAmount * (b.InterestRate/100) + b.LoanAmount) * {LoanAmount} / 100) as AmountPerTeacher \r\n " +
          "FROM EmployeeBank.dbo.tblGuarantor as a \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblLoan as b on a.LoanNo = b.LoanNo \r\n " +
          "WHERE a.LoanNo = {LoanNo} and EmployeeBank.dbo.tblGuarantor.TeacherNo LIKE a.TeacherNo) \r\n " +
          "WHERE EmployeeBank.dbo.tblGuarantor.LoanNo = {LoanNo}"
            ,
           //[4] datagrind  Select billType INPUT: {Mount} {Year} {Type}
          "SELECT CAST(e.PrefixName+''+d.Fname+''+d.Lname as nvarchar) as TeacherAddby,a.BillNo,a.DateAdd,c.TypeName,b.Amount,a.Cancel,CAST(Year(a.DateAdd) as nvarchar),CAST(MONTH(a.DateAdd) as nvarchar)  \r\n" +
          "FROM EmployeeBank.dbo.tblBill as a \r\n" +
          "LEFT JOIN EmployeeBank.dbo.tblBillDetail as b ON a.BillNo = b.BillNo \r\n" +
          "LEFT JOIN EmployeeBank.dbo.tblBillDetailType as c ON b.TypeNo = c.TypeNo \r\n" +
          "LEFT JOIN Personal.dbo.tblTeacherHis as d on a.TeacherNoAddBy = d.TeacherNo \r\n" +
          "LEFT JOIN BaseData.dbo.tblPrefix as e on d.PrefixNo = e.PrefixNo \r\n" +
          "WHERE CAST(Year(a.DateAdd) as nvarchar) = {Year} or CAST(MONTH(a.DateAdd) as nvarchar) = {Mount}  AND a.Cancel != {Type}"
        };


        // SizePositionForm
        private void CancelBill_SizeChanged(object sender, EventArgs e)
        {
            Class.FromSettingMedtod.ChangeSizePanal(this, PL_Form);
        }

        private void Clear()
        {
            //tabpage 5 (Cancel Bill) ==============================================
            DGV_Cancelbill.Rows.Clear();
            LSumAmount_CancelBill.Text = "0";
            TBBIllDate_Cancelbill.Text = "";
            TBTeacherName_Cancelbill.Text = "";
            TBTeacherNO_Cancelbill.Text = "";
            TBteacharnoby_billcancel.Text = "";
            //====================================================================
        }
        // searchbill
        private void TBBillNo_Cancelbill_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (Int32.TryParse(TBBillNo_Cancelbill.Text, out int BillNo))
                {
                    DataSet ds = Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[0]
                        .Replace("{BillNo}", BillNo.ToString()));
                    if (ds.Tables[0].Rows.Count != 0)
                    {
                        Clear();
                        TBBIllDate_Cancelbill.Text = (Convert.ToDateTime(ds.Tables[0].Rows[0][0].ToString())).ToString("yyyy-MM-dd");
                        TBTeacherNO_Cancelbill.Text = ds.Tables[0].Rows[0][1].ToString();
                        TBTeacherName_Cancelbill.Text = ds.Tables[0].Rows[0][2].ToString();
                        TBteacharnoby_billcancel.Text = ds.Tables[1].Rows[0][1].ToString();
                        int Amount = 0;
                        for (int x = 0; x < ds.Tables[0].Rows.Count; x++)
                        {
                            if (ds.Tables[0].Rows[x][5].ToString().Contains("หุ้น"))
                                DGV_Cancelbill.Rows.Add(ds.Tables[0].Rows[x][3].ToString() + '/' + ds.Tables[0].Rows[x][4].ToString(), "หุ้นสะสม", ds.Tables[0].Rows[x][7].ToString(), '-', ds.Tables[0].Rows[x][4].ToString(), ds.Tables[0].Rows[x][3].ToString());
                            else
                                DGV_Cancelbill.Rows.Add(ds.Tables[0].Rows[x][3].ToString() + '/' + ds.Tables[0].Rows[x][4].ToString(), "รายการกู้ " + ds.Tables[0].Rows[x][6].ToString(), ds.Tables[0].Rows[x][7].ToString(), ds.Tables[0].Rows[x][6].ToString(), ds.Tables[0].Rows[x][4].ToString(), ds.Tables[0].Rows[x][3].ToString());
                            Amount = Amount + Convert.ToInt32(ds.Tables[0].Rows[x][7].ToString());
                            if (x % 2 == 1)
                            {
                                //
                                DGV_Cancelbill.Rows[x].DefaultCellStyle.BackColor = Color.AliceBlue;
                            }
                        }
                        LSumAmount_CancelBill.Text = Amount.ToString();

                        CheckInputBill = true;
                    }
                    else
                    {
                        MessageBox.Show("ไม่มีหมายเลขบิลล์นี้ \r\n หรือผู้ใช้ได้ทำการยกเลิกสมาชิกไปแล้ว", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            else if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
            {
                CheckInputBill = false;
                Clear();
            }
        }

        private void BSave_Cancelbill_Click(object sender, EventArgs e)
        {
            if (TBBillNo_Cancelbill.Text != "")
            {
                MessageBox.Show("ยืนยันการยกเลิกบิลล์", "ระบบ", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                // Format yyyy-mm-dd EX: 2020-1-15
                String today = (Convert.ToDateTime((Bank.Menu.Date[0] + '-' + Bank.Menu.Date[1] + '-' + Bank.Menu.Date[2]).ToString())).ToString("yyyy-MM-dd");
                if (today == TBBIllDate_Cancelbill.Text && Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[1]
                            .Replace("{BillNo}", TBBillNo_Cancelbill.Text).Replace("{--}","").Replace("{---}","---")).Tables[0].Rows.Count != 0)
                {
                    string CheckType = "";
                    string CheckType1 = "";
                    if (DGV_Cancelbill.Rows.Count != 0)
                    {
                        //Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[1]
                        //    .Replace("{BillNo}", TBBillNo_Cancelbill.Text)
                        //    .Replace("{TypeNo}", "1"));
                        
                        for (int x = 0; x < DGV_Cancelbill.Rows.Count; x++)
                        {
                            if (DGV_Cancelbill.Rows[x].Cells[1].Value.ToString().Contains("หุ้น"))
                            {
                                CheckType1 = "---";
                                CheckType = "";
                                Class.SQLConnection.InputSQLMSSQL(SQLDefault[2]
                                    .Replace("{TeacherNo}", TBTeacherNO_Cancelbill.Text)
                                    .Replace("{Amount}", DGV_Cancelbill.Rows[x].Cells[2].Value.ToString()));
                            }
                            else if (DGV_Cancelbill.Rows[x].Cells[1].Value.ToString().Contains("กู้"))
                            {
                                CheckType = "--";
                                CheckType1 = "";
                                Class.SQLConnection.InputSQLMSSQL(SQLDefault[3]
                                .Replace("{LoanNo}", DGV_Cancelbill.Rows[x].Cells[3].Value.ToString())
                                .Replace("{LoanAmount}", DGV_Cancelbill.Rows[x].Cells[2].Value.ToString()));
                            }
                        }
                        Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[1]
                           .Replace("{BillNo}", TBBillNo_Cancelbill.Text)
                           .Replace("{--}", $"{CheckType}").Replace("{---}",$"{CheckType1}"));
                        CheckType = "";
                        MessageBox.Show("ยกเลิกบิลล์สำเร็จ");
                    }
                }
                else
                {
                    DialogResult MSB = MessageBox.Show("ไม่สามารถยกเลิกได้เนื่องจาก\r\nบิลล์หมายเลขนี้คือบิลล์ที่เริ่มสมัคร\r\nคุณต้องการดำเนินการต่อหรือไม่", "ระบบ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (MSB == DialogResult.Yes)
                    {
                        //เดี๋ยวมาแก้จ้าาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาาา
                        MessageBox.Show("ขึ้นอยู่กับสิทธ์ของผู้ทำรายการ");
                    }
                }
            }
        }
        private void CancelBill_Load(object sender, EventArgs e)
        {
            int Mount = 0, Year = 0;
            x:
            DataTable dt = Class.SQLConnection.InputSQLMSSQL(SQLDefault[4].Replace("{Mount}",Convert.ToInt32(DateTime.Today.Month - Mount).ToString())
                .Replace("{Year}",Convert.ToInt32(DateTime.Today.Year - Year).ToString()).Replace("{Type}","0"));
            if(dt.Rows.Count != 0)
            {
                CBYearSelection_Bill.Items.Add(Convert.ToInt32(DateTime.Today.Year - Year));
                CBYearSelection_Bill.SelectedIndex = 0;
                CBMonthSelection_Bill.Items.Add(DateTime.Today.Month - Mount);
                CBMonthSelection_Bill.SelectedIndex = 0;

                for(int Row = 0; Row < dt.Rows.Count; Row++)
                {
                    DGV_Bill.Rows.Add(dt.Rows[0][0].ToString(), dt.Rows[0][1].ToString(),dt.Rows[0][2].ToString(),dt.Rows[0][3].ToString(),dt.Rows[0][4].ToString(),dt.Rows[0][5].ToString());
                }
            }
            else
            {
                if (Mount < Convert.ToInt32(DateTime.Today.Month))
                   Mount++;
                else
                {
                    Mount = 0;
                    Year++;
                }
                goto x;
            }



        
          
        }
    }
}
