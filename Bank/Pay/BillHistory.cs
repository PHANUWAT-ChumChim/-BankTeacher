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
    public partial class BillHistory : Form
    {
        public BillHistory()
        {
            InitializeComponent();
        }

        private void Billcancelhistory_SizeChanged(object sender, EventArgs e)
        {
            Class.FromSettingMedtod.ChangeSizePanal(this, PL_Form);
        }
        /// <summary> 
        /// SQLDafault 
        /// <para>[0] DGV  Select billType INPUT: {Year} {Month} {CancelNo} </para>
        /// <para>[1] Select Year INPUT: </para>
        /// <para>[2] Select Month INPUT: {Year}</para>
        /// NEW !
        /// <para>[3] select Bill INPUT : {Year} {Month} {Day} {CancelNo} </para>
        /// <para>[4] select List Year Month Day INPUT : {Year} {Month(Year)} {Day(Year)} </para>
        /// </summary> 
        private String[] SQLDefault = new String[]
        {
           //[0] DGV  Select billType INPUT: {Year} {Month} {CancelNo}
           "SELECT CAST(ISNULL(e.PrefixName,'')+''+d.Fname+' '+d.Lname as nvarchar(255)) as TeacherAddby,a.BillNo,CAST(a.DateAdd as date) as dateadd,c.TypeName,b.Amount,a.Cancel,YEAR(a.DateAdd) as Year,MONTH(a.DateAdd) as Month \r\n " +
          "FROM EmployeeBank.dbo.tblBill as a  \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblBillDetail as b ON a.BillNo = b.BillNo  \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblBillDetailType as c ON b.TypeNo = c.TypeNo  \r\n " +
          "LEFT JOIN Personal.dbo.tblTeacherHis as d on a.TeacherNoAddBy = d.TeacherNo  \r\n " +
          "LEFT JOIN BaseData.dbo.tblPrefix as e on d.PrefixNo = e.PrefixNo  \r\n " +
          "WHERE YEAR(a.DateAdd) = {Year} and MONTH(a.DateAdd) = {Month} and DAY(a.DateAdd) = '{Day}' and Cancel != {CancelNo}\r\n"+
          "GROUP By  CAST(ISNULL(e.PrefixName,'')+''+d.Fname+' '+d.Lname as nvarchar(255)),a.BillNo,CAST(a.DateAdd as date),c.TypeName,b.Amount,a.Cancel,DateAdd \r\n"+
          "ORDER BY a.DateAdd"
            ,
          //[1] Select Year INPUT: 
           "SELECT YEAR(DateAdd) \r\n " +
          "FROM EmployeeBank.dbo.tblBill as a \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblBillDetail as b on a.BillNo = b.BillNo \r\n " +
          "WHERE Year(DateAdd) > Year(CURRENT_TIMESTAMP) - 5 \r\n"+
          "GROUP BY YEAR(DateAdd) \r\n " +
          "ORDER BY YEAR(DateAdd)"
           ,
           //[2] Select Month INPUT: {Year}
           "SELECT MONTH(DateAdd) \r\n " +
          "FROM EmployeeBank.dbo.tblBill as a \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblBillDetail as b on a.BillNo = b.BillNo \r\n " +
          "WHERE Year(DateAdd) = {Year} \r\n " +
          "GROUP BY MONTH(DateAdd) \r\n " +
          "ORDER BY MONTH(DateAdd)"
           ,
           //[3] select Bill INPUT : {Year} {Month} {Day} {CancelNo}
           "--เลขบิล ชื่ออาจารย์ทำบิล วันที่ รายการ ประเภทเงิน ยอด Cancel \r\n " +
           "SELECT a.BillNo,CAST(c.PrefixNameFull+b.Fname+' '+b.Lname as nvarchar) as TeacharName, CAST(a.DateAdd as DATE) as BillDate, CAST(f.TypeName as nvarchar),CAST(e.Name as nvarchar), d.Amount, a.Cancel \r\n " +
           "FROM EmployeeBank.dbo.tblBill as a \r\n " +
           "LEFT JOIN Personal.dbo.tblTeacherHis as b ON a.TeacherNoAddBy = b.TeacherNo \r\n " +
           "LEFT JOIN BaseData.dbo.tblPrefix as c ON c.PrefixNo = b.PrefixNo \r\n " +
           "LEFT JOIN EmployeeBank.dbo.tblBillDetail as d ON a.BillNo = d.BillNo \r\n " +
           "LEFT JOIN EmployeeBank.dbo.tblBillDetailPayment as e ON e.BillDetailPaymentNo = d.BillDetailPaymentNo \r\n " +
           "LEFT JOIN EmployeeBank.dbo.tblBillDetailType as f ON f.TypeNo = d.TypeNo \r\n " +
           "WHERE YEAR(DateAdd) = '{Year}' and MONTH(DateAdd) = '{Month}' and DAY(DateAdd) = '{Day}' and Cancel != '{CancelNo}' \r\n " +
           "UNION ALL \r\n " +
           "SELECT WithDrawNo, c.PrefixNameFull+b.Fname+' '+b.Lname, CAST(a.DateAdd as DATE), 'ถอน', 'เงินสด', a.Amount, 1 \r\n " +
           "FROM EmployeeBank.dbo.tblShareWithdraw as a \r\n " +
           "LEFT JOIN Personal.dbo.tblTeacherHis as b ON a.TeacherNoAddBy = b.TeacherNo \r\n " +
           "LEFT JOIN BaseData.dbo.tblPrefix as c ON c.PrefixNo = b.PrefixNo \r\n " +
           "WHERE YEAR(DateAdd) = '{Year}' and MONTH(DateAdd) = '{Month}' and DAY(DateAdd) = '{Day}' AND 1 != '{CancelNo}' \r\n " +
           "ORDER BY BillDate"
           ,
           //[4] select List Year Month Day INPUT : {Year} {Month(Year)} {Day(Year)}
           "-- ====================== Year : {Year} ============== \r\n " +
           "SELECT CurrentYear \r\n " +
           "FROM (SELECT  Year(DateAdd) as CurrentYear \r\n " +
           "FROM EmployeeBank.dbo.tblBill \r\n " +
           "WHERE Year(DateAdd)  > YEAR(GETDATE()) - 5 \r\n " +
           "UNION ALL \r\n " +
           "SELECT Year(DateAdd) \r\n " +
           "FROM EmployeeBank.dbo.tblShareWithdraw \r\n " +
           "WHERE  Year(DateAdd) > YEAR(GETDATE()) - 5) as a \r\n " +
           "GROUP BY CurrentYear \r\n " +
           "-- ====================== Month : {Month} ============== \r\n " +
           "SELECT CurrentMount \r\n " +
           "FROM (SELECT  MONTH(DateAdd) as CurrentMount \r\n " +
           "FROM EmployeeBank.dbo.tblBill \r\n " +
           "WHERE Year(DateAdd)  = '{Month}' \r\n " +
           "UNION ALL \r\n " +
           "SELECT MONTH(DateAdd) \r\n " +
           "FROM EmployeeBank.dbo.tblShareWithdraw \r\n " +
           "WHERE  Year(DateAdd) = '{Month}') as a \r\n " +
           "GROUP BY CurrentMount \r\n " +
           "-- ====================== Day : {Day} ============== \r\n " +
           "SELECT CurrentDay \r\n " +
           "FROM (SELECT  DAY(DateAdd) as CurrentDay \r\n " +
           "FROM EmployeeBank.dbo.tblBill \r\n " +
           "WHERE Year(DateAdd)  = '{Day}' AND MONTH(DateAdd) = {Month} \r\n " +
           "UNION ALL \r\n " +
           "SELECT DAY(DateAdd) \r\n " +
           "FROM EmployeeBank.dbo.tblShareWithdraw \r\n " +
           "WHERE  Year(DateAdd) = '{Day}' AND MONTH(DateAdd) = {Month}) as a \r\n " +
           "GROUP BY CurrentDay"
           ,
        };
        // ประเภทบิลล์
        public static string TypeBill = "";
        public static int Mont = 0, Year = 0;
        private void Billcancelhistory_Load(object sender, EventArgs e)
        {
            // เลืกอ ปีที่อยู่ในฐานข้อมูลที่ไม่เกินจากปีปัจุบันลบไป 5 ปี เพื่อข้อมูลที่เรียงออกมาจะไม่เกินใน CB
            DataSet ds = BankTeacher.Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[4].Replace("{Month}","0").Replace("{Day}","0"));
            if(ds.Tables[0].Rows.Count != 0)
                for (int x = 0; x < ds.Tables[0].Rows.Count; x++)
                    CBYearSelection_Bill.Items.Add(ds.Tables[0].Rows[x][0].ToString());
            if (CBYearSelection_Bill.Items.Count != 0)
            {
                for (int x = 0; x < CBYearSelection_Bill.Items.Count; x++)
                    if (CBYearSelection_Bill.Items[x].ToString() == BankTeacher.Bank.Menu.Date[0].ToString())
                        CBYearSelection_Bill.SelectedIndex = x;
                if (CBYearSelection_Bill.SelectedIndex == -1)
                    CBYearSelection_Bill.SelectedIndex = 0;
                CBMonthSelection_Bill.Enabled = true;
            }
            else
            {
                CBMonthSelection_Bill.Enabled = false;
                CB_Typebill.Enabled = false;
            }       
        }
        // เลือกปี เเล้ว บอกข้อมูลรายการ เดือนที่มีบิลล์ย้อนหลัง ไม่เกิน 5 ปี
        private void CBYearSelection_Bill_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if(CBYearSelection_Bill.SelectedIndex != -1)
            {
                CBMonthSelection_Bill.Items.Clear();
                DataSet ds = BankTeacher.Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[4].Replace("{Month}", CBYearSelection_Bill.Text).Replace("{Day}", "0"));
                if(ds.Tables[1].Rows.Count != 0)
                    for(int x = 0; x < ds.Tables[1].Rows.Count; x++ )
                        CBMonthSelection_Bill.Items.Add(ds.Tables[1].Rows[x][0].ToString());
                else
                    CBMonthSelection_Bill.Enabled = false;

                if (CBMonthSelection_Bill.Items.Count != 0)
                {
                    for (int x = 0; x < CBMonthSelection_Bill.Items.Count; x++)
                        if (CBMonthSelection_Bill.Items[x].ToString() == BankTeacher.Bank.Menu.Date[1].ToString())
                        {
                            CBMonthSelection_Bill.SelectedIndex = x;
                            break;
                        }
                    if (CBMonthSelection_Bill.SelectedIndex == -1)
                        CBMonthSelection_Bill.SelectedIndex = 0;
                    CBMonthSelection_Bill.Enabled = true;
                }
                else
                {
                    CBMonthSelection_Bill.Enabled = true;
                    CB_Typebill.Enabled = false;
                }
            }
        }

        private void CBMonthSelection_Bill_SelectedIndexChanged(object sender, EventArgs e)
        {
            CB_DaySelection_Bill.Items.Clear();
            if(CBMonthSelection_Bill.SelectedIndex != -1)
            {
                CB_Typebill.Enabled = true;
                DataSet ds = Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[4].Replace("{Month}", CBMonthSelection_Bill.Text).Replace("{Day}", CBYearSelection_Bill.Text));
                if (ds.Tables[2].Rows.Count != 0)
                {
                    for (int loop = 0; loop < ds.Tables[2].Rows.Count; loop++)
                    {
                        CB_DaySelection_Bill.Items.Add(ds.Tables[2].Rows[loop][0]);
                    }
                }
                CB_DaySelection_Bill.SelectedIndex = 0;
                if (CB_Typebill.SelectedIndex == -1)
                {
                    CB_Typebill.SelectedIndex = 0;
                }
                else { CB_Typebill_SelectedIndexChanged(sender, e); }
                
                
            }
            else
            {
                CB_Typebill.Enabled = false;
                CB_Typebill.SelectedIndex = -1;
            }
        }
        private void CB_DaySelection_Bill_SelectedIndexChanged(object sender, EventArgs e)
        {
            CB_Typebill_SelectedIndexChanged(sender, e);
        }
        private void CB_Typebill_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(CB_Typebill.SelectedIndex != -1)
            {
                int PosHeader = 0;
                int CancelNo = 0;
                int AmountBill = 0;
                int Amountall = 0;
                if (CB_Typebill.SelectedItem.ToString().Contains("ทั้งหมด"))
                    CancelNo = 0;
                else if (CB_Typebill.SelectedItem.ToString().Contains("ยกเลิก"))
                    CancelNo = 1;
                else
                    CancelNo = 2;

                DGV_Bill.Rows.Clear();
                DataTable dt = Class.SQLConnection.InputSQLMSSQL(SQLDefault[3]
                    .Replace("{Month}", CBMonthSelection_Bill.Text)
                   .Replace("{Year}", CBYearSelection_Bill.Text)
                   .Replace("{CancelNo}", CancelNo.ToString())
                   .Replace("{Day}",CB_DaySelection_Bill.Text));
                if (dt.Rows.Count != 0)
                {
                    if (dt.Rows[0][6].ToString() == "1")
                    {
                        TypeBill = "ใช้งาน";
                    }
                    else if (dt.Rows[0][6].ToString() == "2")
                    {
                        TypeBill = "ยกเลิก";
                    }
                    DGV_Bill.Rows.Add(dt.Rows[0][1].ToString(), dt.Rows[0][0].ToString(), dt.Rows[0][2].ToString(), dt.Rows[0][3].ToString(), dt.Rows[0][5].ToString(), TypeBill);
                    AmountBill += Convert.ToInt32(DGV_Bill.Rows[0].Cells[4].Value.ToString());
                    PosHeader = DGV_Bill.Rows.Count - 1;
                    for (int Row = 1; Row < dt.Rows.Count; Row++)
                    {
                        if (dt.Rows[Row][6].ToString() == "1")
                        {
                            TypeBill = "ใช้งาน";
                        }
                        else if (dt.Rows[Row][6].ToString() == "2")
                        {
                            TypeBill = "ยกเลิก";
                        }
                        if(dt.Rows[Row][0].ToString() == DGV_Bill.Rows[PosHeader].Cells[1].Value.ToString())
                        {
                            DGV_Bill.Rows.Add("","","", dt.Rows[Row][3].ToString(), dt.Rows[Row][5].ToString(), "");
                            AmountBill += Convert.ToInt32(DGV_Bill.Rows[DGV_Bill.Rows.Count - 1].Cells[4].Value.ToString());
                        }
                        else
                        {
                            DGV_Bill.Rows.Add("", "", "", "สรุปรายการบิลล์  ", AmountBill, "");
                            DGV_Bill.Rows[DGV_Bill.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Cornsilk;
                            Amountall += AmountBill;
                            AmountBill = 0;
                            DGV_Bill.Rows.Add(dt.Rows[Row][1].ToString(), dt.Rows[Row][0].ToString(), dt.Rows[0][2].ToString(), dt.Rows[Row][3].ToString(), dt.Rows[Row][5].ToString(), TypeBill);
                            PosHeader = DGV_Bill.Rows.Count - 1;
                            AmountBill += Convert.ToInt32(DGV_Bill.Rows[DGV_Bill.Rows.Count - 1].Cells[4].Value.ToString());
                        }
                    }
                    Amountall += AmountBill;
                    DGV_Bill.Rows.Add("", "", "", "สรุปรายการบิลล์  ", AmountBill, "");
                    DGV_Bill.Rows[DGV_Bill.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Cornsilk;
                    DGV_Bill.Rows.Add("", "", "", "ยอดรวม  ", Amountall, "");
                    DGV_Bill.Rows[DGV_Bill.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Yellow;
                }
            }
        }

        private void Billcancelhistory_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                BExitForm_Click(new object(), new EventArgs());
            }
        }

        private void BTPrint_Click(object sender, EventArgs e)
        {
            if(DGV_Bill.RowCount != 0)
            {
                //Font printFont = new Font("Arial", 10);
                //System.Drawing.Printing.PrintDocument pd = new System.Drawing.Printing.PrintDocument();
                //pd.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(Class.Print.PrintPreviewDialog.Detailspayment(e, DGV_Bill,"รสยการ"));
                //// Print the document.
                //pd.Print();
                if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                {
                    printDocument1.Print();
                }
            }
            else
            {
                MessageBox.Show("ไม่พบรายการ กรุณา ตวรจสอบรายการในตาาราง", "เอกสาร", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //var a = printDocument1.PrinterSettings.PrintToFile = true;
            Class.Print.PrintPreviewDialog.Detailspayment(e, DGV_Bill, "รสยการ");
        }

        private void BExitForm_Click(object sender, EventArgs e)
        {
            BankTeacher.Class.FromSettingMedtod.ReturntoHome(this);
        }
    }
}
