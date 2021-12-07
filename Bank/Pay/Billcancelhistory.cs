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
        /// <para>[4] datagrind  Select billType INPUT: {Mount} {Year} {Type} {--}  </para>
        /// </summary> 
        private String[] SQLDefault = new String[]
        {
           //[0] datagrind  Select billType INPUT: {Mount} {Year} {Type} {--}
          "SELECT CAST(e.PrefixName+''+d.Fname+''+d.Lname as nvarchar) as TeacherAddby,a.BillNo,CAST(a.DateAdd as date),c.TypeName,b.Amount,a.Cancel,CAST(Year(a.DateAdd) as nvarchar),CAST(MONTH(a.DateAdd) as nvarchar)  \r\n" +
          "FROM EmployeeBank.dbo.tblBill as a \r\n" +
          "LEFT JOIN EmployeeBank.dbo.tblBillDetail as b ON a.BillNo = b.BillNo \r\n" +
          "LEFT JOIN EmployeeBank.dbo.tblBillDetailType as c ON b.TypeNo = c.TypeNo \r\n" +
          "LEFT JOIN Personal.dbo.tblTeacherHis as d on a.TeacherNoAddBy = d.TeacherNo \r\n" +
          "LEFT JOIN BaseData.dbo.tblPrefix as e on d.PrefixNo = e.PrefixNo \r\n" +
          "WHERE CAST(Year(a.DateAdd) as nvarchar) = {Year}  \r\n" +
          "{--}AND CAST(MONTH(a.DateAdd) as nvarchar) = {Mount}  AND a.Cancel != {Type}"
        };
        // ประเภทบิลล์
        public static string TypeBill = "";
        public static int Mont = 0, Year = 0;
        private void Billcancelhistory_Load(object sender, EventArgs e)
        {
            Mont = 0; Year = 0;
        x:
            DataTable dt = Class.SQLConnection.InputSQLMSSQL(SQLDefault[0].Replace("{Mount}", Convert.ToInt32(DateTime.Today.Month - Mont).ToString())
                .Replace("{Year}", Convert.ToInt32(DateTime.Today.Year - Year).ToString()).Replace("{Type}", "0")
                .Replace("{--}", "--"));
            if (dt.Rows.Count != 0)
            {
                for (int Row = 0; Row < dt.Rows.Count; Row++)
                {

                    if (dt.Rows[Row][5].ToString() == "1")
                    {
                        TypeBill = "บิลล์";
                    }
                    else if (dt.Rows[Row][5].ToString() == "2")
                    {
                        TypeBill = "ยกเลิกบิลล์";
                    }
                    DGV_Bill.Rows.Add(dt.Rows[Row][0].ToString(), dt.Rows[Row][1].ToString(), dt.Rows[Row][2].ToString(), dt.Rows[Row][3].ToString(), TypeBill, dt.Rows[Row][4].ToString());
                }
            }
            else
            {
                if (Mont < Convert.ToInt32(DateTime.Today.Month))
                {
                    Mont++;
                }
                else
                {
                    Mont = 0;
                    Year++;
                }
                goto x;
            }
            Year = Convert.ToInt32(DateTime.Today.Year - Year);
            Mont = Convert.ToInt32(DateTime.Today.Month - Mont);
            for (int Y = 0; Y < 2; Y++)
            {
                dt = Class.SQLConnection.InputSQLMSSQL(SQLDefault[0]
                    .Replace("{Mount}", Convert.ToInt32(Mont).ToString())
                    .Replace("{Year}", Convert.ToInt32(Year - Y).ToString())
                    .Replace("{Type}", "0")
                    .Replace("{--}", "--"));
                if (dt.Rows.Count != 0)
                {
                    CBYearSelection_Bill.Items.Add(Convert.ToInt32(Year - Y));
                }
                else
                {
                    break;
                }
            }
            CBYearSelection_Bill.SelectedIndex = 0;
            CBMonthSelection_Bill.SelectedIndex = 0;
            CB_Typebill.SelectedIndex = 0;
        }
        // ค้นหารายการบิลล์
        private void BListAdd_Pay_Click_1(object sender, EventArgs e)
        {
            DGV_Bill.Rows.Clear();
            DataTable dt = Class.SQLConnection.InputSQLMSSQL(SQLDefault[0].Replace("{Mount}", CBMonthSelection_Bill.Text)
               .Replace("{Year}", CBYearSelection_Bill.Text).Replace("{Type}", CB_Typebill.SelectedIndex.ToString())
               .Replace("{--}", ""));
            if (dt.Rows.Count != 0)
            {
                for (int Row = 0; Row < dt.Rows.Count; Row++)
                {
                    if (dt.Rows[Row][5].ToString() == "1")
                    {
                        TypeBill = "บิลล์";
                    }
                    else if (dt.Rows[Row][5].ToString() == "2")
                    {
                        TypeBill = "ยกเลิกบิลล์";
                    }

                    DGV_Bill.Rows.Add(dt.Rows[Row][0].ToString(), dt.Rows[Row][1].ToString(), dt.Rows[0][2].ToString(), dt.Rows[Row][3].ToString(), TypeBill, dt.Rows[Row][4].ToString());
                }
            }
        }



        // เลือกปี เเล้ว บอกข้อมูลรายการ เดือนที่มีบิลล์ย้อนหลัง ไม่เกิน 2 ปี
        private void CBYearSelection_Bill_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            CBMonthSelection_Bill.Items.Clear();

            for (int M = 1; M <= 12; M++)
            {
                DataTable dt = Class.SQLConnection.InputSQLMSSQL(SQLDefault[0]
                  .Replace("{Mount}", Mont.ToString())
                  .Replace("{Year}", CBYearSelection_Bill.Text)
                  .Replace("{Type}", "0")
                  .Replace("{--}", ""));
                if (dt.Rows.Count != 0)
                {
                    CBMonthSelection_Bill.Items.Add(Mont);
                    Mont--;
                    if (Mont == 0)
                    {
                        Mont = 12;
                    }
                }
                else
                {
                    Mont--;
                    if (Mont == 0)
                    {
                        Mont = 12;
                    }
                }
            }
            CBMonthSelection_Bill.SelectedIndex = 0;
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
