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
    public partial class Billcancelhistory : Form
    {
        public Billcancelhistory()
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

        };
        // ประเภทบิลล์
        public static string TypeBill = "";
        public static int Mont = 0, Year = 0;
        private void Billcancelhistory_Load(object sender, EventArgs e)
        {
            // เลืกอ ปีที่อยู่ในฐานข้อมูลที่ไม่เกินจากปีปัจุบันลบไป 5 ปี เพื่อข้อมูลที่เรียงออกมาจะไม่เกินใน CB
            DataTable dt = BankTeacher.Class.SQLConnection.InputSQLMSSQL(SQLDefault[1]);
            if(dt.Rows.Count != 0)
                for (int x = 0; x < dt.Rows.Count; x++)
                    CBYearSelection_Bill.Items.Add(dt.Rows[x][0].ToString());
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
                DataTable dt = BankTeacher.Class.SQLConnection.InputSQLMSSQL(SQLDefault[2].Replace("{Year}", CBYearSelection_Bill.Text));
                if(dt.Rows.Count != 0)
                    for(int x = 0; x < dt.Rows.Count; x++ )
                        CBMonthSelection_Bill.Items.Add(dt.Rows[x][0].ToString());
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
                DataTable dt = Class.SQLConnection.InputSQLMSSQL("SELECT DAY(DateAdd) as DAY,MONTH(DateAdd)  as MONTH \r\n " +
                "FROM EmployeeBank.dbo.tblBill as a \r\n " +
                "WHERE Year(DateAdd) = '{Year}' AND  MONTH(DateAdd) = '{Mount}' \r\n ".Replace("{Year}", CBYearSelection_Bill.Text).Replace("{Mount}", CBMonthSelection_Bill.Text) +
                "GROUP BY DAY(DateAdd), MONTH(DateAdd) \r\n " +
                "ORDER BY MONTH(DateAdd)");
                if (dt.Rows.Count != 0)
                {
                    for (int loop = 0; loop < dt.Rows.Count; loop++)
                    {
                        CB_DaySelection_Bill.Items.Add(dt.Rows[loop][0]);
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
                DataTable dt = Class.SQLConnection.InputSQLMSSQL(SQLDefault[0]
                    .Replace("{Month}", CBMonthSelection_Bill.Text)
                   .Replace("{Year}", CBYearSelection_Bill.Text)
                   .Replace("{CancelNo}", CancelNo.ToString())
                   .Replace("{Day}",CB_DaySelection_Bill.Text));
                if (dt.Rows.Count != 0)
                {
                    if (dt.Rows[0][5].ToString() == "1")
                    {
                        TypeBill = "ใช้งาน";
                    }
                    else if (dt.Rows[0][5].ToString() == "2")
                    {
                        TypeBill = "ยกเลิก";
                    }
                    DGV_Bill.Rows.Add(dt.Rows[0][0].ToString(), dt.Rows[0][1].ToString(), dt.Rows[0][2].ToString(), dt.Rows[0][3].ToString(), dt.Rows[0][4].ToString(), TypeBill);
                    AmountBill += Convert.ToInt32(DGV_Bill.Rows[0].Cells[4].Value.ToString());
                    PosHeader = DGV_Bill.Rows.Count - 1;
                    for (int Row = 1; Row < dt.Rows.Count; Row++)
                    {
                        if (dt.Rows[Row][5].ToString() == "1")
                        {
                            TypeBill = "ใช้งาน";
                        }
                        else if (dt.Rows[Row][5].ToString() == "2")
                        {
                            TypeBill = "ยกเลิก";
                        }
                        if(dt.Rows[Row][1].ToString() == DGV_Bill.Rows[PosHeader].Cells[1].Value.ToString())
                        {
                            DGV_Bill.Rows.Add("","","", dt.Rows[Row][3].ToString(), dt.Rows[Row][4].ToString(), "");
                            AmountBill += Convert.ToInt32(DGV_Bill.Rows[DGV_Bill.Rows.Count - 1].Cells[4].Value.ToString());
                        }
                        else
                        {
                            DGV_Bill.Rows.Add("", "", "", "สรุปรายการบิลล์", AmountBill, "");
                            DGV_Bill.Rows[DGV_Bill.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Cornsilk;
                            Amountall += AmountBill;
                            AmountBill = 0;
                            DGV_Bill.Rows.Add(dt.Rows[Row][0].ToString(), dt.Rows[Row][1].ToString(), dt.Rows[0][2].ToString(), dt.Rows[Row][3].ToString(), dt.Rows[Row][4].ToString(), TypeBill);
                            PosHeader = DGV_Bill.Rows.Count - 1;
                            AmountBill += Convert.ToInt32(DGV_Bill.Rows[DGV_Bill.Rows.Count - 1].Cells[4].Value.ToString());
                        }
                    }
                    Amountall += AmountBill;
                    DGV_Bill.Rows.Add("", "", "", "สรุปรายการบิลล์", AmountBill, "");
                    DGV_Bill.Rows[DGV_Bill.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Cornsilk;
                    DGV_Bill.Rows.Add("", "", "", "ยอดรวม", Amountall, "");
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
        private void BExitForm_Click(object sender, EventArgs e)
        {
            BankTeacher.Class.FromSettingMedtod.ReturntoHome(this);
        }
    }
}
