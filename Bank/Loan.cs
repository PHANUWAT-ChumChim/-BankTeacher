﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace example.Bank
{
    public partial class Loan : Form
    {
        public Loan()
        {
            InitializeComponent();
        }

        private void Loan_SizeChanged(object sender, EventArgs e)
        {
            Class.FromSettingMedtod.ChangeSizePanal(this, panel1);
        }
        DateTime DateTime;
        String TeacherNoUser;
        /// <summary> 
        /// SQLDafaultLoan 
        /// <para>[0] SELECT TeacherName Data INPUT:{TeacherNo} </para> 
        /// <para>[1] SELECT Guarantor Credit Limit INPUT:{GuarantorNo} </para>
        /// <para>[2] SELECT Date Data </para>
        /// <para>[3] INSERT Loan and Get LoanNo INPUT: {TeacherNoAdd}, {TeacherNo}, {MonthPay}, {YearPay}, {LoanAmount}, {PayNo}, {InterestRate}</para>
        /// <para>[4] INSERT Guarantor INPUT: {LoanNo},{TeacherNo},{Amount},{RemainsAmount}</para>
        /// </summary> 
        private String[] SQLDefaultLoan = new String[]
        {
            //[0] SELECT TeacherName Data INPUT:{TeacherNo} 
            "select a.TeacherNo, CAST(ISNULL(c.PrefixNameFull,'') + b.Fname + ' ' + b.Lname as nvarchar) \r\n" +
            "from EmployeeBank.dbo.tblMember as a \r\n" +
            "left join Personal.dbo.tblTeacherHis as b on a.TeacherNo = b.TeacherNo \r\n" +
            "left join BaseData.dbo.tblPrefix as c on b.PrefixNo = c.PrefixNo \r\n" +
            "Where a.TeacherNo = '{TeacherNo}'; \r\n\r\n",

            //[1] SELECT CreditLimit Data INPUT:{GuarantorNo}
            "SELECT a.TeacherNo , d.SavingAmount ,d.SavingAmount - SUM(b.RemainsAmount)\r\n" + 
            "FROM EmployeeBank.dbo.tblMember as a\r\n" + 
            "LEFT JOIN EmployeeBank.dbo.tblGuarantor as b on a.TeacherNo = b.TeacherNo\r\n" + 
            "LEFT JOIN EmployeeBank.dbo.tblLoan as c on b.LoanNo = c.LoanNo\r\n" + 
            "LEFT JOIN EmployeeBank.dbo.tblShare as d on a.TeacherNo = d.TeacherNo\r\n" +
            "WHERE a.TeacherNo = '{GuarantorNo}'\r\n" +
            "GROUP BY a.TeacherNo , d.SavingAmount;\r\n"
            ,

            //[2] SELECT Date Data
            "SELECT CAST(CURRENT_TIMESTAMP as DATE); \r\n\r\n",

            //[3] INSERT Loan and Get LoanNo INPUT: {TeacherNoAdd}, {TeacherNo}, {MonthPay}, {YearPay}, {LoanAmount}, {PayNo}, {InterestRate}
            "DECLARE @LoanNo INT;\r\n" +
            "INSERT INTO EmployeeBank.dbo.tblLoan\r\n" +
            "(TeacherNoAddBy, TeacherNo, MonthPay, YearPay, LoanAmount, PayNo, InterestRate, DateAdd)\r\n" +
            "VALUES ('{TeacherNoAdd}', '{TeacherNo}', '{MonthPay}', '{YearPay}', '{LoanAmount}', '{PayNo}', '{InterestRate}', CAST(CURRENT_TIMESTAMP as DATE));\r\n" +
            "SELECT @LoanNo = SCOPE_IDENTITY();\r\n" +
            "SELECT LoanNo\r\n" +
            "FROM EmployeeBank.dbo.tblLoan\r\n" +
            "WHERE LoanNo = @LoanNo;\r\n"
            ,

            //INSERT Guarantor INPUT: {LoanNo},{TeacherNo},{Amount},{RemainsAmount}
            "INSERT INTO EmployeeBank.dbo.tblGuarantor (LoanNo,TeacherNo,Amount,RemainsAmount)\r\n" +
            "VALUES ('{LoanNo}','{TeacherNo}','{Amount}','{RemainsAmount}');\r\n"
            ,
        };
        
        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("เดี๋ยวใส่ตอนรวมโปรแกรมครับ", "System", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading, true);
        }
        private void CBB4Oppay_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Loan_Load(object sender, EventArgs e)
        {
            DataSet ds = Class.SQLConnection.InputSQLMSSQLDS(SQLDefaultLoan[2]);
            DataTable dt = ds.Tables[0];
            DateTime = DateTime.Parse(dt.Rows[0][0].ToString());
            int Year = int.Parse(DateTime.ToString("yyyy"));
            for(int Num = 0;Num < 5; Num++)
            {
                CBPayYear.Items.Add(Year);
                Year++;
            }
        }

        private void BSearchTeacher_Click(object sender, EventArgs e)
        {
            try
            {
                Bank.Search IN = new Bank.Search(2);
                IN.ShowDialog();
                TBTeacherNo.Text = Bank.Search.Return[0];
                TBTeacherName.Text = Bank.Search.Return[1];
                TBLoanNo.Text = Bank.Search.Return[6];
                TBLoanStatus.Text = Bank.Search.Return[7];
                TBLoanAmount.Text = Bank.Search.Return[9];
            }
            catch (Exception x)
            {
                Console.WriteLine(x);
            }
        }
        //int RowDGV;
        private void TBTeacherNo_TextChanged(object sender, EventArgs e)
        {
            //ต้องพิมพ์รหัสอาจารย์ถึง 6 ตัวถึงจะเข้าเงื่อนไข if
            
            int credit;
            if (TBTeacherNo.Text.Length == 6)
            {
                Class.SQLMethod.ReSearchLoan(TBTeacherNo.Text, TBTeacherName, TBLoanNo, TBLoanStatus, TBSavingAmount);

                DataSet ds = Class.SQLConnection.InputSQLMSSQLDS(
                    SQLDefaultLoan[0].Replace("{TeacherNo}", TBTeacherNo.Text) +

                    SQLDefaultLoan[1].Replace("{GuarantorNo}", TBTeacherNo.Text));

                DataTable dtTeacherName = ds.Tables[0];
                DataTable dtGuarantorCredit = ds.Tables[1];
                //String aa = dtGuarantorCredit.Rows[0][2].ToString();
                if (dtGuarantorCredit.Rows[0][2].ToString().Contains(""))
                {
                    credit = int.Parse(dtGuarantorCredit.Rows[0][1].ToString());
                }
                else
                {
                    credit = int.Parse(dtGuarantorCredit.Rows[0][2].ToString());
                }
                DGVGuarantor.Rows.Add(dtGuarantorCredit.Rows[0][0], dtTeacherName.Rows[0][1], credit);
                //RowDGV = DGVGuarantor.Rows.Count;
            }
            else
            {
                TBTeacherNo.Text = "";
                TBTeacherName.Text = "";
                TBLoanNo.Text = "";
                TBLoanStatus.Text = "";
                TBSavingAmount.Text = "";
                if(DGVGuarantor.Rows.Count > 0)
                {
                    //DGVGuarantor.Rows.Remove()\
                    DGVGuarantor.Rows.Clear();
                }
            }
        }
        
        private void TBGuarantorNo_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == (char)Keys.Enter)
            {
                bool CheckTeacherNo = false;
                for(int Num = 0; Num < DGVGuarantor.Rows.Count; Num++)
                {
                    String aa = DGVGuarantor.Rows[Num].Cells[1].Value.ToString();
                    CheckTeacherNo = TBGuarantorNo.Text.Contains(DGVGuarantor.Rows[Num].Cells[0].Value.ToString());
                    if (CheckTeacherNo)
                        break;
                }
                if ((DGVGuarantor.Rows.Count < 4) && (CheckTeacherNo == false))
                {
                    DataSet ds = Class.SQLConnection.InputSQLMSSQLDS(
                        SQLDefaultLoan[0]
                        .Replace("{TeacherNo}", TBGuarantorNo.Text) +

                        SQLDefaultLoan[1]
                        .Replace("{GuarantorNo}", TBGuarantorNo.Text));
                    DataTable dtGuarantorName = ds.Tables[0];
                    DataTable dtSavingAmount = ds.Tables[1];
                    if (dtGuarantorName.Rows.Count != 0 && dtSavingAmount.Rows.Count != 0)
                    {
                        int credit;
                        if (dtSavingAmount.Rows[0][2].ToString().Contains(""))
                        {
                            credit = int.Parse(dtSavingAmount.Rows[0][1].ToString());
                        }
                        else
                        {
                            credit = int.Parse(dtSavingAmount.Rows[0][2].ToString());
                        }

                        if (credit != 0)
                        {
                            DGVGuarantor.Rows.Add(dtSavingAmount.Rows[0][0].ToString(),
                                dtGuarantorName.Rows[0][1].ToString(),
                                credit);
                        }
                        else
                        {
                            MessageBox.Show("ไม่มียอดเงินที่ใช้ค้ำได้ โปรดเลือกบุคคลอื่น", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }

                    }
                    else
                    {
                        DialogResult Result = MessageBox.Show("ไม่มีข้อมูล", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    }
                }
                else if (CheckTeacherNo == true)
                    MessageBox.Show("รายชื่อซ้ำ", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else if(DGVGuarantor.Rows.Count >= 4)
                {
                    MessageBox.Show("ผู้ค้ำเกินกหนด", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                TBGuarantorNo.Text = "";
            }
        }

        private void DGVGuarantor_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {

            LGuarantorAmount.Text = DGVGuarantor.RowCount.ToString() + "/4";
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (tabControl1.SelectedIndex == 1 && DGVGuarantor.Rows.Count == 4)
            {
                int LoanAmount = 0;
                for (int Count = 0; Count < DGVGuarantor.Rows.Count; Count++)
                {
                    LoanAmount += int.Parse(DGVGuarantor.Rows[Count].Cells[2].Value.ToString());
                }
                LLoanAmount.Text = "( " + LoanAmount.ToString() + " )";

            }
            else if (tabControl1.SelectedIndex == 2 && (TBLoanAmount.Text != "" && CBPayMonth.Text != "" && CBPayYear.Text != "" && TBPayNo.Text != "" && TBInterestRate.Text != ""))
            {
                int Month = int.Parse(CBPayMonth.Text), Year = int.Parse(CBPayYear.Text);
                float Pay = float.Parse(TBLoanAmount.Text) / float.Parse(TBPayNo.Text);
                float Interest = float.Parse(TBLoanAmount.Text) * float.Parse(TBInterestRate.Text) / 100;
                
                for (int Num = 0; Num < int.Parse(TBPayNo.Text); Num++)
                {
                    float AllpayD = Pay + Interest;
                    int AllPay = 0;
                    Month++;
                    if (Month > 12)
                    {
                        Month = 1;
                        Year++;
                    }
                    if((!IsInt(AllpayD)) && (Num == (int.Parse(TBPayNo.Text) - 1)))
                    {
                        AllpayD -= 1;
                    }
                    else if (!IsInt(AllpayD))
                    {
                        AllpayD += 1;
                    }
                    AllPay = Convert.ToInt32(AllpayD);
                    DGVLoanDetail.Rows.Add($"{Month}/{Year}", Pay.ToString(), Interest.ToString(), AllPay.ToString());
                }

            }

        }
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
        //private void RBPresent_CheckedChanged(object sender, EventArgs e)
        //{
        //    CBPayMonth.Items.Clear();
        //    CBPayYear.Items.Clear();
        //    DataSet ds = Class.SQLConnection.InputSQLMSSQLDS(
        //        SQLDefault[2]);
        //    DataTable dt = ds.Tables[0];
        //    DateTime date = DateTime.Parse(dt.Rows[0][0].ToString());
        //    int Month = int.Parse(date.ToString("MM"));
        //    int Year = int.Parse(date.ToString("yyyy"));
        //    if (RBPresent.Checked == true)
        //    {

        //        if (Month == 12)
        //            Month = 1;
        //        for (; Month <= 12; Month++)
        //        {
        //            CBPayMonth.Items.Add(Month);
        //        }
        //        for (int count = 0; count < 10; count++)
        //        {
        //            CBPayYear.Items.Add(Year);
        //            Year++;
        //        }
        //        //for(int count = 0;)
        //        //CBPayMonth.Items.Add("asdasdasd");
        //    }
        //    else
        //    {
        //        for (int count = 1; count <= 12; count++)
        //        {
        //            CBPayMonth.Items.Add(count);
        //        }
        //        Year += 3;
        //        for (int count = 0; count < 15; count++)
        //        {
        //            CBPayYear.Items.Add(Year);
        //            Year--;
        //            //
        //        }
        //    }
        //}

        private void CBPayMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            int LoanAmount, LoanAmountLimit;
            bool checkNum, check;
            checkNum = int.TryParse(TBLoanAmount.Text, out LoanAmount);
            check = int.TryParse(LLoanAmount.Text, out LoanAmountLimit);
            if ((check && checkNum) && (CBPayYear.Text != "" && (int.Parse(TBLoanAmount.Text) > int.Parse(LLoanAmount.Text) || int.Parse(TBLoanAmount.Text) < 1)))
            {
                DialogResult result = MessageBox.Show("วงเงินกู้เกินกำหนดการค้ำ ต้องการทำต่อหรือไม่", "แจ้งเตือน", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.No)
                {
                    TBLoanAmount.Text = "";
                    TBLoanAmount.Focus();
                }
            }
            if(int.Parse(CBPayMonth.Text) < int.Parse(DateTime.ToString("MM")) && int.Parse(CBPayYear.Text) < int.Parse(DateTime.ToString("yyyy")))
            {
                if(MessageBox.Show("คุณยืนยันที่จะใส่เป็นเดือนที่ผ่านมาหรือไม่", "แจ้งเตือน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    CBPayMonth.Text = "";
                    CBPayMonth.Focus();
                }
            }
        }

        private void CBPayYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            int LoanAmount, LoanAmountLimit;
            bool checkNum, check;
            checkNum = int.TryParse(TBLoanAmount.Text, out LoanAmount);
            check = int.TryParse(LLoanAmount.Text, out LoanAmountLimit);
            if ((check && checkNum) && (CBPayMonth.Text != "" && (int.Parse(TBLoanAmount.Text) > int.Parse(LLoanAmount.Text) || int.Parse(TBLoanAmount.Text) < 1)))
            {
                DialogResult result = MessageBox.Show("วงเงินกู้เกินกำหนดการค้ำ ต้องการทำต่อหรือไม่", "แจ้งเตือน", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.No)
                {
                    TBLoanAmount.Text = "";
                    TBLoanAmount.Focus();
                }

            }
            if (int.Parse(CBPayMonth.Text) < int.Parse(DateTime.ToString("MM")) && int.Parse(CBPayYear.Text) < int.Parse(DateTime.ToString("yyyy")))
            {
                if (MessageBox.Show("คุณยืนยันที่จะใส่เป็นเดือนที่ผ่านมาแล้วหรือไม่", "แจ้งเตือน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    CBPayMonth.Text = "";
                    CBPayMonth.Focus();
                }
            }
        }

        //private void TBTeacherNo_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (TBTeacherNo.Text.Length == 6)
        //    {
        //        if (e.KeyCode == Keys.Enter)
        //        {
        //            DataSet ds = Class.SQLConnection.InputSQLMSSQLDS(
        //                SQLDefaultLoan[0]
        //                .Replace("{TeacherNo}", TBTeacherNo.Text));
        //            DataTable dtTeacher = ds.Tables[0];

        //            DataTable dt = ds.Tables[1];


        //        }
        //    }
        //    else
        //    {
        //        TBTeacherName.Text = "";
        //        TBLoanNo.Text = "";
        //        TBLoanStatus.Text = "";
        //        TBSavingAmount.Text = "";
        //    }

        //}

        public void StartCenter(System.Drawing.Printing.PrintPageEventArgs e, float LocY, String Text, Font fontText, Brush brush)
        {
            SizeF SizeString = e.Graphics.MeasureString(Text, fontText);
            float StartLoc = e.PageBounds.Width / 2;
            e.Graphics.DrawString(Text,
                fontText, brush, new PointF(StartLoc, LocY));
        }
        public void Center(System.Drawing.Printing.PrintPageEventArgs e, float LocY, String Text, Font fontText, Brush brush)
        {
            SizeF SizeString = e.Graphics.MeasureString(Text, fontText);
            float StartLoc = e.PageBounds.Width / 2 - SizeString.Width / 2;
            e.Graphics.DrawString(Text,
                fontText, brush, new PointF(StartLoc, LocY));
        }
        public void KeepRight(System.Drawing.Printing.PrintPageEventArgs e, float LocY, String Text, Font fontText, Brush brush)
        {
            SizeF SizeString = e.Graphics.MeasureString(Text, fontText);
            float StartLoc = (e.PageBounds.Width - 50) - SizeString.Width;
            e.Graphics.DrawString(Text,
                fontText, brush, new PointF(StartLoc, LocY));
        }
        public void KeepLeft(System.Drawing.Printing.PrintPageEventArgs e, float LocY, String Text, Font fontText, Brush brush)
        {
            SizeF SizeString = e.Graphics.MeasureString(Text, fontText);
            float StartLoc = 50;
            e.Graphics.DrawString(Text,
                fontText, brush, new PointF(StartLoc, LocY));
        }

        public void CenterKeepRight(System.Drawing.Printing.PrintPageEventArgs e, float LocY, String Text, Font fontText, Brush brush)
        {
            SizeF SizeString = e.Graphics.MeasureString(Text, fontText);
            float StartLoc = ((e.PageBounds.Width / 2) + (e.PageBounds.Width / 2) / 2) - SizeString.Width;
            e.Graphics.DrawString(Text,
                fontText, brush, new PointF(StartLoc, LocY));
        }
        public int CurrentRows = 0;
     
        public void Header(System.Drawing.Printing.PrintPageEventArgs e, Brush brush)
        {
            int Y = 50;
            int SpacePerRow = 25;
            //int CurrentRows = 0;
            Font Header01 = new Font("TH Sarabun New", 20, FontStyle.Bold);
            //Font Normal01 = new Font("TH Sarabun New", 18, FontStyle.Regular);
            String[] Head = new String[] { "APPLICATION FOR EMPLOYMENT", "ใบสมัครงาน", "กรอกข้อมูลด้วยตัวท่านเอง", "(To be completed in own handwriting)" };

            for (int Num = 0; Num < 4; Num++)
            {
                if (Num == 2)
                    Header01 = new Font("TH Sarabun New", 18, FontStyle.Regular);
                SizeF SizeString = e.Graphics.MeasureString(Head[Num], Header01);
                float StartLoc = e.PageBounds.Width / 2 - SizeString.Width / 2;
                e.Graphics.DrawString(Head[Num],
                Header01, brush, new PointF(StartLoc, Y + (SpacePerRow * CurrentRows++)));
            }
        }
        public void Rect(System.Drawing.Printing.PrintPageEventArgs e, Pen ColorRect, int WidthSize, int HeightSize, float LocY, float LocX)
        {
            //float x = e.PageBounds.Width - 50 - 125;
            e.Graphics.DrawRectangle(ColorRect, LocX, LocY, WidthSize, HeightSize);
        }
        public void PrintBody(System.Drawing.Printing.PrintPageEventArgs e, float LocY, String Text, Font font, Brush brush)
        {
            float LocX = 96;
            e.Graphics.DrawString(Text, font, brush, LocX, LocY);
        }
        public void PrintCheckBoxList(System.Drawing.Printing.PrintPageEventArgs e, float LocX, float LocY, Font font, Brush brush, List<String> AllCheckBox, float SpaceCheckList)
        {
            Pen ColorRect = new Pen(Color.Black);

            //for (int Num = 0; Num < AllCheckBox.Count; Num++)
            //{
            //    SizeF SizeText = e.Graphics.MeasureString(AllCheckBox[Num], Normal01);
            //    PrintCheckBoxList(e, SpaceX + (37 * (Num + 1)), Y + (SpacePerRow * CurrentRows), AllCheckBox[Num], Normal01, Normal);
            //    SpaceX += SizeText.Width;
            //}

            for (int Num = 0; Num < AllCheckBox.Count; Num++)
            {
                SizeF SizeText = e.Graphics.MeasureString(AllCheckBox[Num], font);
                e.Graphics.DrawString(AllCheckBox[Num], font, brush, LocX + (SpaceCheckList * (Num + 1)), LocY);
                Rect(e, ColorRect, 15, 15, LocY + 20, LocX + (SpaceCheckList * (Num + 1)) - 17);
                LocX += SizeText.Width;
            }

        }

        private void printDocument1_PrintPage_1(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            int PageX = (e.PageBounds.Width);
            int PageY = (e.PageBounds.Height);
            int X = 96;
            int Y = 50;
            int SpacePerRow = 25;
            //int CurrentRows = 0;

            Font Header01 = new Font("TH Sarabun New", 30, FontStyle.Bold);
            Font Normal01 = new Font("TH Sarabun New", 16, FontStyle.Regular);
            Font Toppic = new Font("TH Sarabun New", 18, FontStyle.Bold);
            Brush Normal = Brushes.Black;

            //*Page Header*
            Header(e, Normal);

            //String Text = "เอกสารสมัครสมาชิกสหกรณ์ครู";
            //Center(e, Y + (SpacePerRow * CurrentRows++) - 10, "เอกสารสมัครสมาชิกสหกรณ์ครู" + "\r\n" + 
            //    "aaaaaa" , Header01, Normal);

            //Header(e, Y + (SpacePerRow * CurrentRows++) - 10, Header01, Normal);
            //SizeF SizeString = e.Graphics.MeasureString(Text, Header01);
            //float StartLoc = PageX / 2 - SizeString.Width / 2;
            //e.Graphics.DrawString(Text,
            //    Header01, Normal, new PointF(StartLoc, Y + (SpacePerRow * CurrentRows++)-10));

            //*Rectangle Picture*
            Pen ColorRect = new Pen(Color.Black, 1);
            Rect(e, ColorRect, 125, SpacePerRow * CurrentRows, 50, e.PageBounds.Width - 96 - 125);

            //*NameText And PositionText*
            CurrentRows++;
            String NameTextPrint = "ชื่อ       : ................................................................................................................................................\r\n" +
                "Name\r\n" + "ตำแหน่งที่ต้องการ       1…………………………………………………... เงินเดือน ..........................บาท / เดือน\r\n" +
                "Position applied for       2 …………………………………………… Salary                   Bath / month";
            PrintBody(e, Y + (SpacePerRow * CurrentRows++), NameTextPrint, Normal01, Normal);

            CurrentRows += 4;

            //**Toppic Personal**
            PrintBody(e, Y + (SpacePerRow * CurrentRows++), "Personal information (ประวัติส่วนตัว)", Toppic, Normal);

            //**Detail Personal**
            PrintBody(e, Y + (SpacePerRow * CurrentRows++),
                "ที่อยู่ปัจจุบันเลขที่ ..............หมู่ที่ ........... ถนน ........................................... ตำบล/แขวง.........................\r\n" +
                "Present address           Moo          Road                                 District\r\n" +
                "อำเภอ/เขต ............................................. จังหวัด ........................................ รหัสไปรษณีย์ ....................\r\n" +
                "Amphur                                       Province                            Post code\r\n" +
                "โทรศัพท์ .................................................. เพจเจอร์ .................................... มือถือ .................................\r\n" +
                "Tel.                                             Pager                                Mobile\r\n" +
                "อีเมล์ .........................................................................................................................................................\r\n" +
                "E-mail", Normal01, Normal);

            CurrentRows += 8;

            //**Print Checkbox List**
            List<String> AllCheckBox = new List<string> {
                "อาศัยกับครอบครัว\r\n" + "Living with parent" , "บ้านตัวเอง\r\n" + "Own home", "บ้านเช่า\r\n" + "Hired house", "ห้องเช่า\r\n" + "Hiredflat / Hostel"};
            float SpaceX = X + 15;
            //for (int Num = 0;Num < AllCheckBox.Count; Num++)
            //{
            //    SizeF SizeText = e.Graphics.MeasureString(AllCheckBox[Num], Normal01);
            //    PrintCheckBoxList(e, SpaceX + (37 * (Num + 1)), Y + (SpacePerRow * CurrentRows), AllCheckBox[Num], Normal01, Normal);
            //    SpaceX += SizeText.Width;
            //}
            PrintCheckBoxList(e, SpaceX, Y + (SpacePerRow * CurrentRows), Normal01, Normal, AllCheckBox, 37);

            CurrentRows += 3;
            //**Print Personal02**
            PrintBody(e, Y + (SpacePerRow * CurrentRows),
                "วัน เดือน ปีเกิด ............................................. อายุ ...................... ปี          เชื้อชาติ...............................\r\n" +
                "Date of birth                                     Age                  Yrs.     Race\r\n" +
                "สัญชาติ ...................................................................... ศาสนา.................................................\r\n" +
                "Nationality                                                 Religion\r\n" +
                "บัตรประชาชนเลขที่ ................................................... บัตรหมดอายุ .....................................\r\n" +
                "Identity card no.                                         Expiration date\r\n" +
                "ส่วนสูง ............................ ชม.                            น้ำหนัก ........................... กก.\r\n" +
                "Height                       cm.                           Weight                      kgs.\r\n" +
                "ภาวะทางทหาร\r\n" +
                "Military status\r\n" +
                "สถานภาพ\r\n" +
                "Marital status\r\n" +
                "เพศ\r\n" +
                "Sex", Normal01, Normal);

            CurrentRows += 10;

            //**CheckBox Military status**
            AllCheckBox = new List<string> {
                "ได้รับการยกเว้น\r\n" + "Exempted" , "ปลดเป็นทหารกองหนุน\r\n" + "Served" , "ยังไม่ได้รับการเกณฑ์\r\n" + "Not yet served"};
            PrintCheckBoxList(e, SpaceX + 40, (Y + (SpacePerRow * CurrentRows)) - 10, Normal01, Normal, AllCheckBox, 74);

            CurrentRows += 3;

            //**CheckBox Marital status**
            AllCheckBox = new List<string> {
                "โสด\r\n" + "Single", "แต่งงาน\r\n" + "Married" , "หม้าย\r\n" + "Widowed" , "แยกกัน\r\n" + "Separated"};
            PrintCheckBoxList(e, SpaceX + 40, (Y + (SpacePerRow * CurrentRows)) - 25, Normal01, Normal, AllCheckBox, 74);

            CurrentRows += 3;

            AllCheckBox = new List<string> { "ชาย\r\n" + "Male", "หญิง\r\n" + "Female" };
            PrintCheckBoxList(e, SpaceX + 15, (Y + (SpacePerRow * CurrentRows)) - 45, Normal01, Normal, AllCheckBox, 100);
            //for (int x = 0; x < 20; x++)
            //{
            //    e.Graphics.DrawString("Test",
            //        Normal01, Normal, new PointF(X, Y + (SpacePerRow * CurrentRows++)));

            //    //KeepRight(e, Y + (SpacePerRow * CurrentRows), "Testasd", Normal01, Normal);
            //}

            //float gg = e.PageBounds.Width;
            //SizeF shadowSize = gg;
            //SizeF a = Convert.ChangeType(50, SizeF);
            //e.Graphics.DrawString("Testtttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttt",
            //    Normal01, Normal, new RectangleF(X, Y + (SpacePerRow * CurrentRows++), 200f, 200f));
            //e.Graphics.DrawString("Testtttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttttt",
            //    Normal01, Normal, new PointF(X, Y + (SpacePerRow * CurrentRows++) - 50));

            //e.Graphics.DrawString(DTPStartDate.Text, new Font("TH Sarabun New", 18, FontStyle.Regular), Brushes.Black, new PointF(0, 60));


        }

        private void DGVGuarantor_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            LGuarantorAmount.Text = DGVGuarantor.RowCount.ToString() + "/4";
        }

        private void TBLoanAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!Char.IsNumber(e.KeyChar)) && (!Char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void TBPayNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!Char.IsNumber(e.KeyChar)) && (!Char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void TBInterestRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!Char.IsNumber(e.KeyChar)) && (!Char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void BSave_Click(object sender, EventArgs e)
        {
            int LoanNo;
            DataSet dsInsertLoan = Class.SQLConnection.InputSQLMSSQLDS(SQLDefaultLoan[3]
                .Replace("{TeacherNoAdd}", TeacherNoUser)
                .Replace("{TeacherNo}",TBTeacherNo.Text)
                .Replace("{MonthPay}",CBPayMonth.Text)
                .Replace("{YearPay}",CBPayYear.Text)
                .Replace("{LoanAmount}",TBLoanAmount.Text)
                .Replace("{PayNo}",TBPayNo.Text)
                .Replace("{InterestRate}",TBInterestRate.Text) 
                );
            DataTable dtGetLoanNo = dsInsertLoan.Tables[0];
            LoanNo = int.Parse(dtGetLoanNo.Rows[0][0].ToString());

            for(int Num = 0; Num < DGVGuarantor.Rows.Count; Num++)
            {
                DataSet dsInsertGuarantor = Class.SQLConnection.InputSQLMSSQLDS(SQLDefaultLoan[4]
                .Replace("{LoanNo", LoanNo.ToString())
                .Replace("{TeacherNo", DGVGuarantor.Rows[Num].Cells[0].Value.ToString())
                .Replace("{Amount}", DGVGuarantor.Rows[Num].Cells[2].Value.ToString())
                .Replace("{RemainsAmount}", DGVGuarantor.Rows[Num].Cells[2].Value.ToString())
                );
            }
            

        }
        //private void BPrintLoanDoc_Click(object sender, EventArgs e)
        //{
        //    //Method.SQLMethod.TeacherMember(TBTeacherNo.Text,int.Parse(TBStartAmountShare.Text), button1.Text);
        //    CurrentRows = 0;
        //    DialogResult a = printPreviewDialog1.ShowDialog();
        //}
    }
}
