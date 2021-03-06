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
        bool CheckSave = false; 
        
        /// <summary> 
        /// SQLDafault 
        /// <para>[0] Select Bill (CancelBill) INPUT: {BillNo}</para>
        /// <para>[1] Update Cancel Bill INPUT: {BillNo} {CancelBy} {Note}</para>
        /// <para>[2] Update Saving (CancelBill) INPUT: {TeacherNo} {Amount} {BillNo} {DateTime}</para>
        /// <para>[3] plus RemainAmount In Guarantor (CancelBill) INPUT: {LoanNo} , {LoanAmount}</para>
        /// <para>[4] Search TeacherNo (CancelBill) INPUT: {BillNo} {today} {Text} {BillNoSelect}</para>
        /// <para>[5] Check Dividend Year INPUT: </para>
        /// <para>[6] Chcek LoanstatusNo INPUT : {TeacharNo} {BillNo} </para>
        /// <para>[7] Check TypeNo 0 = true , 1 = false INPUT: {BillNo}INPUT: {BillNo} </para>
        /// <para>[8] Search Bill INPUT: {TeacherNo} </para>
        /// </summary> 
        private String[] SQLDefault = new String[]
        {
              //[0] Select Bill (CancelBill) INPUT: {BillNo}
           "SELECT a.TeacherNo,CAST(ISNULL(e.PrefixName,'') + d. Fname + ' ' + d.LName as nvarchar(255))as Name ,CAST(a.DateAdd as date),b.Year ,b.Mount , TypeName,LoanNo , b.Amount ,CAST(h.PrefixName+''+g.Fname+''+g.Lname as nvarchar) as TeacherAddByName \r\n " +
          " FROM EmployeeBank.dbo.tblBill as a  \r\n " +
          " LEFT JOIN EmployeeBank.dbo.tblBillDetail as b on a.BillNo = b.BillNo  \r\n " +
          " LEFT JOIN EmployeeBank.dbo.tblMember as c on a.TeacherNo = c.TeacherNo  \r\n " +
          " LEFT JOIN Personal.dbo.tblTeacherHis as d on a.TeacherNo = d.TeacherNo  \r\n " +
          " LEFT JOIN BaseData.dbo.tblPrefix as e on d.PrefixNo = e.PrefixNo  \r\n " +
          " LEFT JOIN EmployeeBank.dbo.tblBillDetailType as f on b.TypeNo = f.TypeNo  \r\n " +
          " LEFT JOIN Personal.dbo.tblTeacherHis as g on a.TeacherNoAddBy = g.TeacherNo \r\n " +
          " LEFT JOIN BaseData.dbo.tblPrefix as h on g.PrefixNo = h.PrefixNo \r\n " +
          " WHERE  a.BillNo = '{BillNo}' and MemberStatusNo != 2 and Cancel = 1 and b.TypeNo != 3 and CAST(a.DateAdd as DATE) >= DATEADD(DAY,-7,CAST(GETDATE() as DATE)) "

            ,
            //[1] Update Cancel Bill INPUT: {BillNo} {CancelBy} {Note}
           "Update EmployeeBank.dbo.tblBill   \r\n " +
          "SET Cancel = 2 , CancelBy = '{CancelBy}' , CancelDate = CURRENT_TIMESTAMP , CancelNote = '{Note}'\r\n " +
          "WHERE BillNo = '{BillNo}' \r\n "

            ,
           //[2] Update Saving (CancelBill) INPUT: {TeacherNo} {Amount} {BillNo} {DateTime}
           "DECLARE @@SavingAmount INT; \r\n " +
          " \r\n " +
          "SET @@SavingAmount = (SELECT SavingAmount \r\n " +
          "FROM EmployeeBank.dbo.tblShare \r\n " +
          "WHERE TeacherNo = '{TeacherNo}') \r\n " +
          " \r\n " +
          "UPDATE EmployeeBank.dbo.tblShare \r\n " +
          "SET SavingAmount = @@SavingAmount - {Amount} \r\n " +
          "WHERE  TeacherNo = '{TeacherNo}' \r\n " +
          " UPDATE EmployeeBank.dbo.tblBill \r\n " +
          "SET CancelDate = '{DateTime}' , CancelTransactionDate = CURRENT_TIMESTAMP \r\n " +
         "WHERE BillNo = '{BillNo}' "
           ,
           //[3] + RemainAmount In Guarantor (CancelBill) INPUT: {LoanNo} , {LoanAmount} {BillNo} {DateTime}
          "UPDATE EmployeeBank.dbo.tblGuarantor  \r\n " +
          "SET RemainsAmount = EmployeeBank.dbo.tblGuarantor.RemainsAmount + (SELECT ((a.Amount * 100 ) / (b.LoanAmount * (b.InterestRate/100) + b.LoanAmount) * {LoanAmount} / 100) as AmountPerTeacher \r\n " +
          "FROM EmployeeBank.dbo.tblGuarantor as a \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblLoan as b on a.LoanNo = b.LoanNo \r\n " +
          "WHERE a.LoanNo = {LoanNo} and EmployeeBank.dbo.tblGuarantor.TeacherNo LIKE a.TeacherNo) \r\n " +
          "WHERE EmployeeBank.dbo.tblGuarantor.LoanNo = {LoanNo} \r\n " +

          " UPDATE EmployeeBank.dbo.tblBill \r\n " +
          "SET CancelDate = '{DateTime}' \r\n " +
          "WHERE BillNo = '{BillNo}' "
            ,
           //[4] Search All TeacherNo in to day (CancelBill) INPUT: {today} {Text}
           "SELECT a.TeacherNo,CAST(ISNULL(e.PrefixName,'') + d. Fname + ' ' + d.LName as nvarchar(255))as Name\r\n " + 
          "FROM EmployeeBank.dbo.tblBill as a   \r\n " +
          "  LEFT JOIN EmployeeBank.dbo.tblBillDetail as b on a.BillNo = b.BillNo   \r\n " +
          "  LEFT JOIN EmployeeBank.dbo.tblMember as c on a.TeacherNo = c.TeacherNo   \r\n " +
          "  LEFT JOIN Personal.dbo.tblTeacherHis as d on a.TeacherNo = d.TeacherNo   \r\n " +
          "  LEFT JOIN BaseData.dbo.tblPrefix as e on d.PrefixNo = e.PrefixNo   \r\n " +
          "  LEFT JOIN EmployeeBank.dbo.tblBillDetailType as f on b.TypeNo = f.TypeNo   \r\n " +
          "  WHERE  (a.TeacherNo LIKE '%{Text}%' or CAST(ISNULL(e.PrefixName,'') + d. Fname + ' ' + d.LName as nvarchar(255)) LIKE '%{Text}%' )and MemberStatusNo != 2 and Cancel = 1 and CAST(a.DateAdd as DATE) >= DATEADD(DAY,-7,CAST(GETDATE() as DATE)) and b.TypeNo != 3 and a.TeacherNo != '{TeacherNoNotLike}'\r\n " +
          " GROUP BY a.TeacherNo,CAST(ISNULL(e.PrefixName,'') + d. Fname + ' ' + d.LName as nvarchar(255))"
            ,
            //[5] Check Dividend Year INPUT: 
            "SELECT Year\r\n " +
            "FROM EmployeeBank.dbo.tblDividend \r\n" +
            "WHERE Cancel = 1 \r\n" +
            "GROUP BY Year;"
            ,
            //[6] Chcek LoanstatusNo INPUT : {TeacharNo} {BillNo}
            "DECLARE @LoanStatusNo int \r\n" +
            "DECLARE @LoanNo int \r\n" +
            "DECLARE @InterestRate int  \r\n" +
            "SELECT @LoanStatusNo = a.LoanStatusNo  FROM EmployeeBank.dbo.tblLoan as a WHERE TeacherNo = '{TeacharNo}' \r\n" +
            "SELECT @LoanNo = b.LoanNo  FROM EmployeeBank.dbo.tblBillDetail as b WHERE b.BillNo = '{BillNo}' \r\n" +
            "SELECT @InterestRate = c.InterestRate/100*c.LoanAmount FROM EmployeeBank.dbo.tblLoan as c WHERE c.LoanNo = @LoanNo \r\n" +
            "IF(@LoanStatusNo = 3) \r\n" +
            "BEGIN \r\n" +
            "--- เพิ่ม หุ้นสะสสม กลับหลังจากการลบ \r\n" +
            "update EmployeeBank.dbo.tblShare \r\n" +
            "set SavingAmount = SavingAmount + @InterestRate \r\n" +
            "WHERE TeacherNo = '{TeacharNo}' \r\n" +
            "--------------------------------------- \r\n" +
            "UPDATE EmployeeBank.dbo.tblLoan \r\n" +
            "SET LoanStatusNo = '2' \r\n" +
            "WHERE LoanNo = @LoanNo \r\n" +
            "END"
            ,
            //[7] Check TypeNo 0 = true , 1 = false INPUT: {BillNo}
           "SELECT COUNT(a.TypeNo) \r\n " +
          "FROM EmployeeBank.dbo.tblBillDetail as a \r\n " +
          "WHERE a.BillNo = 1333 and a.TypeNo = 3"
           

           ,
           //[8] Search Bill INPUT: {TeacherNo} 
           "SELECT a.BillNo \r\n " +
          " FROM EmployeeBank.dbo.tblBill as a    \r\n " +
          "   LEFT JOIN EmployeeBank.dbo.tblBillDetail as b on a.BillNo = b.BillNo    \r\n " +
          "   LEFT JOIN EmployeeBank.dbo.tblMember as c on a.TeacherNo = c.TeacherNo    \r\n " +
          "   LEFT JOIN EmployeeBank.dbo.tblBillDetailType as f on b.TypeNo = f.TypeNo    \r\n " +
          "   WHERE a.TeacherNo = '{TeacherNo}' and  MemberStatusNo != 2 and Cancel = 1 and CAST(a.DateAdd as DATE) >= DATEADD(DAY,-7,CAST(GETDATE() as DATE)) and b.TypeNo != 3  \r\n " +
          "  GROUP BY a.BillNo"
           ,

        };


        // SizePositionForm
        private void CancelBill_SizeChanged(object sender, EventArgs e)
        {
            Class.FromSettingMedtod.ChangeSizePanal(this, PL_Form);
        }

        private void Clear()
        {
            //tabpage 5 (Cancel Bill) ==============================================
            TBNote.Enabled = false;
            DGV_Cancelbill.Rows.Clear();
            LSumAmount_CancelBill.Text = "0";
            TBBIllDate_Cancelbill.Text = "";
            TBteacharnoby_billcancel.Text = "";
            //====================================================================
        }
        // searchbill
        private void TBBillNo_Cancelbill_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CBBill.Items.Clear();
                TBTeaacherNo.Text = TBTeaacherNo.Text.Replace("t", "T");
                DataTable dt = Class.SQLConnection.InputSQLMSSQL(SQLDefault[8]
                    .Replace("{TeacherNo}",TBTeaacherNo.Text));
                if(dt.Rows.Count != 0)
                {
                    for(int x=  0;  x < dt.Rows.Count; x++)
                    {
                        CBBill.Items.Add(dt.Rows[x][0].ToString());
                    }
                    CBBill.Enabled = true;
                    CBBill.SelectedIndex = 0; 
                }
                else
                {
                    CBBill.SelectedIndex = -1;
                    CBBill.Enabled = false;
                    CBBill.Items.Clear();
                }
            }
            else if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
            {
                CheckInputBill = false;
                BSave_Cancelbill.Enabled = false;
                Clear();
                CBBill.Enabled = false;
                CBBill.Items.Clear();
            }
        }

        private void BSave_Cancelbill_Click(object sender, EventArgs e)
        {
            if (TBTeaacherNo.Text != "")
            {
               DialogResult dr = MessageBox.Show("ยืนยันการยกเลิกบิลล์", "ระบบ", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if(dr == DialogResult.Yes)
                {
                    // Format yyyy-mm-dd EX: 2020-1-16
                    String today = (Convert.ToDateTime((Bank.Menu.Date[0] + '-' + Bank.Menu.Date[1] + '-' + Bank.Menu.Date[2]).ToString())).ToString("yyyy-MM-dd");

                    DataTable dtCheckTypeBill = Class.SQLConnection.InputSQLMSSQL(SQLDefault[7].Replace("{BillNo}", CBBill.SelectedItem.ToString()));

                    if (dtCheckTypeBill.Rows[0][0].ToString() == "0" && TBNote.Text != "")
                    {
                        if (DGV_Cancelbill.Rows.Count != 0)
                        {
                            try
                            {
                                String Date = DTPDate.Value.ToString("yyyy:MM:dd");
                                Date = Date.Replace(":", "/");
                                String Note = TBNote.Text;
                                if (Note == "")
                                    Note = "-";
                                Class.SQLConnection.InputSQLMSSQL(SQLDefault[1]
                                    .Replace("{BillNo}", CBBill.SelectedItem.ToString())
                                    .Replace("{CancelBy}" , BankTeacher.Class.UserInfo.TeacherNo)
                                    .Replace("{Note}", Note) );
                                for (int x = 0; x < DGV_Cancelbill.Rows.Count; x++)
                                {
                                    if (DGV_Cancelbill.Rows[x].Cells[1].Value.ToString().Contains("หุ้น"))
                                    {
                                        Class.SQLConnection.InputSQLMSSQL(SQLDefault[2]
                                            .Replace("{TeacherNo}", TBTeaacherNo.Text)
                                            .Replace("{Amount}", DGV_Cancelbill.Rows[x].Cells[2].Value.ToString())
                                            .Replace("{BillNo}", CBBill.SelectedItem.ToString())
                                            .Replace("{DateTime}", Date));
                                    }
                                    else if (DGV_Cancelbill.Rows[x].Cells[1].Value.ToString().Contains("กู้"))
                                    {
                                        Class.SQLConnection.InputSQLMSSQL(SQLDefault[6].Replace("{TeacharNo}", TBTeaacherNo.Text)
                                            .Replace("{BillNo}", CBBill.SelectedItem.ToString()));
                                        Class.SQLConnection.InputSQLMSSQL(SQLDefault[3]
                                        .Replace("{LoanNo}", DGV_Cancelbill.Rows[x].Cells[3].Value.ToString())
                                        .Replace("{LoanAmount}", DGV_Cancelbill.Rows[x].Cells[2].Value.ToString())
                                         .Replace("{BillNo}", CBBill.SelectedItem.ToString())
                                        .Replace("{DateTime}", Date));
                                    }
                                }
                                MessageBox.Show("ยกเลิกบิลล์สำเร็จ","ระบบ",MessageBoxButtons.OK,MessageBoxIcon.Information);
                                CheckSave = true;
                                CBBill.Enabled = false;
                                CBBill.Items.Clear();
                                BSave_Cancelbill.Enabled = false;
                            }
                            catch
                            {
                                MessageBox.Show("ยกเลิกบิลล์ล้มเหลว","ระบบ",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                            }
                            ClearForm();
                            //TBTeacherNO_Cancelbill.Focus();
                        }
                    }
                    else if(TBNote.Text == "")
                    {
                        MessageBox.Show("โปรดระบุหมายเหตุด้วยก่อนทำรายการ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        TBNote.Focus();
                    }
                    else if(dtCheckTypeBill.Rows[0][0].ToString() != "0")
                    {
                        DialogResult MSB = MessageBox.Show("ไม่สามารถยกเลิกได้เนื่องจาก\r\nบิลล์หมายเลขนี้คือบิลล์ที่เริ่มสมัคร\r\nคุณต้องการดำเนินการต่อหรือไม่", "ระบบ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (MSB == DialogResult.Yes)
                        {
                            //ก็อบวางอันบนเช็คสิทธ์ ใส่ Permision
                            MessageBox.Show("ขึ้นอยู่กับสิทธ์ของผู้ทำรายการ ","ระบบ",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                        }
                    }
                }
            }
        }

        private void BExitForm_Click(object sender, EventArgs e)
        {
            BankTeacher.Class.FromSettingMedtod.ReturntoHome(this);
        }

        private void CancelBill_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape || (e.KeyCode == Keys.Enter && CheckSave))
            {
                if (TBTeaacherNo.Text.Length != 0)
                {
                    Clear();
                    TBTeaacherNo.Text = "";
                    CBBill.Enabled = false;
                    CBBill.Items.Clear();
                    CheckSave = false;
                    TBTeaacherNo.Enabled = true;
                }
                else
                {
                    BExitForm_Click(new object(), new EventArgs());
                }
            }
        }

        private void BSearchTeacher_Click(object sender, EventArgs e)
        {
            String Year = DTPDate.Value.ToString("yyyy");
            String Month = DTPDate.Value.ToString("MM");
            String Day = DTPDate.Value.ToString("dd");
            if (Convert.ToInt32(Month) < 10)
            {
                Month = "0" + Convert.ToInt32(Month);
            }
            if (Convert.ToInt32(Day) < 10)
            {
                Day = "0" + Convert.ToInt32(Day);
            }
            String todaySQLSelect = (Convert.ToDateTime((Year + '-' + Month + '-' + Day).ToString())).ToString("yyyy-MM-dd");

            Bank.Search IN = new Bank.Search(SQLDefault[4]
                .Replace("{today}", todaySQLSelect)
                .Replace("{TeacherNoNotLike}",TBTeaacherNo.Text));
            IN.ShowDialog();
            //ถ้า ID สมาชิกที่เลือกไม่เป็นว่างเปล่า ให้ ใส่ลงใน TBTeacherNo และ ไปทำ event Keydown ของ TBTeacherNo
            if (Bank.Search.Return[0] != "")
            {
                TBTeaacherNo.Text = Bank.Search.Return[0];
                TBTeacherName_Cancelbill.Text = Bank.Search.Return[1];
                TBBillNo_Cancelbill_KeyDown(new object(), new KeyEventArgs(Keys.Enter));
            }
        }
        private void ClearForm()
        {
            TBNote.Text = "";
            TBTeacherName_Cancelbill.Text = "";
            TBteacharnoby_billcancel.Text = "";
            TBTeaacherNo.Text = "";
            TBBIllDate_Cancelbill.Text = "";
            LSumAmount_CancelBill.Text = "0";
            BSave_Cancelbill.Enabled = false;
            DGV_Cancelbill.Rows.Clear();
        }

        private void CancelBill_Load(object sender, EventArgs e)
        {
            DTPDate.Value = Convert.ToDateTime(Bank.Menu.Date[0] + "/" + Bank.Menu.Date[1] + "/" + Bank.Menu.Date[2]);
            if (BankTeacher.Bank.Menu.DateAmountChange == 1)
                DTPDate.Enabled = true;
            else
                DTPDate.Enabled = false;
        }

        private void CBBill_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = Class.SQLConnection.InputSQLMSSQL(SQLDefault[0]
                .Replace("{BillNo}", CBBill.SelectedItem.ToString())
                .Replace("{today}", ""));
            if (dt.Rows.Count != 0)
            {
                Clear();
                TBNote.Enabled = true;
                TBBIllDate_Cancelbill.Text = (Convert.ToDateTime(dt.Rows[0][2].ToString())).ToString("yyyy-MM-dd");
                TBteacharnoby_billcancel.Text = dt.Rows[0][8].ToString();
                DataTable DividendYear = Class.SQLConnection.InputSQLMSSQL(SQLDefault[5]);
                if (DividendYear.Rows.Count != 0)
                {
                    for (int xy = 0; xy < DividendYear.Rows.Count; xy++)
                    {
                        if (DividendYear.Rows[0][0].ToString().Contains((Convert.ToDateTime(dt.Rows[xy][2].ToString())).ToString("yyyy")))
                        {
                            MessageBox.Show("ไม่สามารถยกเลิกบิลล์หมายเลขนี้ได้เนื่องจากมีการปันผลไปแล้ว", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            Clear();
                            break;
                        }
                        else if (xy == DividendYear.Rows.Count - 1)
                        {
                            int Amount = 0;
                            for (int x = 0; x < dt.Rows.Count; x++)
                            {
                                if (dt.Rows[x][5].ToString().Contains("หุ้น"))
                                    DGV_Cancelbill.Rows.Add(dt.Rows[x][3].ToString() + '/' + dt.Rows[x][4].ToString(), "หุ้นสะสม", dt.Rows[x][7].ToString(), '-', dt.Rows[x][4].ToString(), dt.Rows[x][3].ToString());
                                else
                                    DGV_Cancelbill.Rows.Add(dt.Rows[x][3].ToString() + '/' + dt.Rows[x][4].ToString(), "รายการกู้ " + dt.Rows[x][6].ToString(), dt.Rows[x][7].ToString(), dt.Rows[x][6].ToString(), dt.Rows[x][4].ToString(), dt.Rows[x][3].ToString());
                                Amount = Amount + Convert.ToInt32(dt.Rows[x][7].ToString());
                                if (x % 2 == 1)
                                {
                                    DGV_Cancelbill.Rows[x].DefaultCellStyle.BackColor = Color.AliceBlue;
                                }
                            }
                            LSumAmount_CancelBill.Text = Amount.ToString();
                            BSave_Cancelbill.Enabled = true;
                            CheckInputBill = true;
                        }

                    }
                }
                else
                {
                    int Amount = 0;
                    for (int x = 0; x < dt.Rows.Count; x++)
                    {
                        if (dt.Rows[x][5].ToString().Contains("หุ้น"))
                            DGV_Cancelbill.Rows.Add(dt.Rows[x][3].ToString() + '/' + dt.Rows[x][4].ToString(), "หุ้นสะสม", dt.Rows[x][7].ToString(), '-', dt.Rows[x][4].ToString(), dt.Rows[x][3].ToString());
                        else
                            DGV_Cancelbill.Rows.Add(dt.Rows[x][3].ToString() + '/' + dt.Rows[x][4].ToString(), "รายการกู้ " + dt.Rows[x][6].ToString(), dt.Rows[x][7].ToString(), dt.Rows[x][6].ToString(), dt.Rows[x][4].ToString(), dt.Rows[x][3].ToString());
                        Amount = Amount + Convert.ToInt32(dt.Rows[x][7].ToString());
                        if (x % 2 == 1)
                        {
                            DGV_Cancelbill.Rows[x].DefaultCellStyle.BackColor = Color.AliceBlue;
                        }
                    }
                    LSumAmount_CancelBill.Text = Amount.ToString();
                    BSave_Cancelbill.Enabled = true;
                    CheckInputBill = true;
                    CheckSave = false;
                }
                TBTeaacherNo.Enabled = false;
                BSave_Cancelbill.Enabled = true;
            }
            else
            {
                MessageBox.Show("ไม่พบหมายเลขบิลล์นี้ \r\n หรือ รายการบิลล์ถูกยกเลิกไปแล้ว", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
