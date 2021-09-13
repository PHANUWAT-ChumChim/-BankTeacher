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
        //------------------------- index -----------------
        DateTime DateTime;
        int Check = 0;
        public static int SelectIndexRowDelete;

        //----------------------- index code -------------------- ////////
        public Loan()
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
        /// SQLDafaultLoan 
        /// <para>[0] SELECT TeacherName Data INPUT:{TeacherNo} , {TeacherNoNotLike} </para>
        /// <para>[1] SELECT Guarantor Credit Limit INPUT:T{TeacherNo} , {TeacherNoNotLike} </para>
        /// <para>[2] SELECT Date Data </para>
        /// <para>[3] INSERT Loan and Get LoanNo INPUT: {TeacherNoAdd}, {TeacherNo}, {MonthPay}, {YearPay}, {LoanAmount}, {PayNo}, {InterestRate}</para>
        /// <para>[4] INSERT Guarantor INPUT: {LoanNo},{TeacherNo},{Amount},{RemainsAmount}</para>
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

        //----------------------- PullSQLDate -------------------- ////////
        // ดึงขอมูลวันที่จากฐานข้อมูล
        int Month;
        private void Loan_Load(object sender, EventArgs e)
        {
            DataSet ds = Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[2]);
            DataTable dt = ds.Tables[0];
            DateTime = DateTime.Parse(dt.Rows[0][0].ToString());
            int Year = int.Parse(DateTime.ToString("yyyy"));
            Month = int.Parse(DateTime.ToString("MM"));
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
           
            if (TBTeacherNo.Text != "" && CBPayMonth.SelectedIndex != -1 && CBPayYear.SelectedIndex != -1 &&
                TBLoanAmount.Text != "" && TBPayNo.Text != "" && TBInterestRate.Text != "" /*&& DGVGuarantor.Rows.Count == 4*/ /*&& CheckDBNull == true*/)
            {
                

                for (int Num = 0; Num < DGVGuarantor.Rows.Count; Num++)
                {

                    float Percent = int.Parse(TBLoanAmount.Text) * (float.Parse(DGVGuarantor.Rows[Num].Cells[3].Value.ToString()) / 100);
                    bool CheckInt = IsInt(Percent);
                    
                    Int32.TryParse(Percent.ToString(),out int GuarantorCredit);
                    Double aaaa = Convert.ToInt32(Percent)+0.5;
                    if (!CheckInt && Percent >= aaaa)
                    {
                        GuarantorCredit = Convert.ToInt32(Percent) + 1;
                    }
                    else if(!CheckInt && Percent < Convert.ToDouble(Percent.ToString($"########.50")))
                    {
                        GuarantorCredit = Convert.ToInt32(Percent) - 1;
                    }
                    DataSet dsInsertGuarantor = Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[4]

                    //.Replace("{LoanNo}", LoanNo.ToString())
                    .Replace("{TeacherNo}", DGVGuarantor.Rows[Num].Cells[0].Value.ToString())
                    .Replace("{Amount}", GuarantorCredit.ToString())
                    .Replace("{RemainsAmount}", GuarantorCredit.ToString())
                    );
                }

                MessageBox.Show("บันทึกข้อมูลเสร็จเรียบร้อยแล้ว", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TBTeacherNo.Text = "";
                TBTeacherName.Text = "";
                TBLoanNo.Text = "";
                TBLoanStatus.Text = "";
                TBSavingAmount.Text = "";
                DGVGuarantor.Rows.Clear();
                CBPayMonth.SelectedIndex = -1;
                CBPayYear.SelectedIndex = -1;
                TBLoanAmount.Text = "";

            }
            else
            {
                MessageBox.Show("โปรดใสข้อมุลให้ครบก่อนบันทึก", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


        }
        //----------------------- End code -------------------- ////////

        //------------------------- Pull SQL Member & CheckTBTeacherNo ---------
        // ค้นหารายชชื่อผู้สมัครสมาชิกครูสหกร์จากฐานข้อมูล
        private void TBTeacherNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void TBGuarantorNo_KeyPress(object sender, KeyPressEventArgs e)
        {

        }
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
            if (tabControl1.SelectedIndex == 3 && (TBLoanAmount.Text != "" && CBPayMonth.SelectedIndex != -1 && CBPayYear.SelectedIndex != -1 && TBPayNo.Text != "" && TBInterestRate.Text != ""))
            {
                DGVLoanDetail.Rows.Clear();
                int Month = int.Parse(CBPayMonth.Text), Year = int.Parse(CBPayYear.Text);
                float Pay = float.Parse(TBLoanAmount.Text) / float.Parse(TBPayNo.Text);
                float Interest = float.Parse(TBLoanAmount.Text) * float.Parse(TBInterestRate.Text) / 100;

                for (int Num = 0; Num < int.Parse(TBPayNo.Text); Num++)
                {
                    float AllpayD = Pay + Interest;
                    int AllPay = 0;
                    //Month++;
                    if (Month > 12)
                    {
                        Month = 1;
                        Year++;
                    }
                    if ((!IsInt(AllpayD)) && (Num == (int.Parse(TBPayNo.Text) - 1)))
                    {
                        AllpayD -= 1;
                    }
                    else if (!IsInt(AllpayD))
                    {
                        AllpayD += 1;
                    }
                    //if(AllpayD > Convert.ToDouble(AllpayD.ToString("##")))
                    AllPay = Convert.ToInt32(AllpayD);
                    DGVLoanDetail.Rows.Add($"{Month}/{Year}", Pay.ToString("########.##"), Interest.ToString("#########.##"), AllPay.ToString());
                    Month++;
                }

            }
        }
        //----------------------- End code -------------------- ////////

        //----------------------- select pay date -------------------- ////////
        // เลือกเดือนจ่าย
        private void CBPayMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            int LoanAmount = 0;
            bool Check = false , CheckLimit = true;
            if (TBLoanAmount.Text != "")
                Check = int.TryParse(TBLoanAmount.Text , out LoanAmount);

            String AmountLimit = LLoanAmount.Text.Remove(0, 1);
            AmountLimit = AmountLimit.Remove(AmountLimit.Length - 1);
            int NumAmountLimit;
            CheckLimit = int.TryParse(AmountLimit, out NumAmountLimit);
            if(Check && CheckLimit && DGVGuarantor.Rows.Count == 4)
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
            //else if (!CheckLimit)
            //{
            //    tabControl1.SelectedIndex = 0;
            //    TBTeacherNo.Focus();
            //}
            //else if (!Check)
            //{
            //    TBLoanAmount.Focus();
            //}
            
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
                for (int Num = Month; Num < 12; Num++)
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
                       .Replace("{TeacherNoNotLike}",NotLike));

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
                    Check = 0;
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
                        DialogResult Result = MessageBox.Show("ไม่มีข้อมูล หรือไม่มียอดเงินที่ค้ำได้", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        //----------------------- End code -------------------- ////////

   
        // Comment!
        public void Rect(System.Drawing.Printing.PrintPageEventArgs e, Pen ColorRect, int WidthSize, int HeightSize, float LocY, float LocX)
        {
            //float x = e.PageBounds.Width - 50 - 125;
            e.Graphics.DrawRectangle(ColorRect, LocX, LocY, WidthSize, HeightSize);
        }
        // Comment!
        public void PrintBody(System.Drawing.Printing.PrintPageEventArgs e, float LocY, String Text, Font font, Brush brush)
        {
            float LocX = 96;
            e.Graphics.DrawString(Text, font, brush, LocX, LocY);
        }
        // Comment!
        public void PrintCheckBoxList(System.Drawing.Printing.PrintPageEventArgs e, float LocX, float LocY, Font font, Brush brush, List<String> AllCheckBox, float SpaceCheckList)
        {
            Pen ColorRect = new Pen(Color.Black);

            for (int Num = 0; Num < AllCheckBox.Count; Num++)
            {
                SizeF SizeText = e.Graphics.MeasureString(AllCheckBox[Num], font);
                e.Graphics.DrawString(AllCheckBox[Num], font, brush, LocX + (SpaceCheckList * (Num + 1)), LocY);
                Rect(e, ColorRect, 15, 15, LocY + 20, LocX + (SpaceCheckList * (Num + 1)) - 17);
                LocX += SizeText.Width;
            }

        }

        //----------------------- End Medtod -------------------- ////////

        //----------------------- Printf -------------------- ////////
        // พิมพ์เอกสารกู้
        private void BPrintLoanDoc_Click_1(object sender, EventArgs e)
        {
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }
        // อัพเอกสารส่ง เซิร์ฟเวอร์
        private void BLoanDocUpload_Click(object sender, EventArgs e)
        {
            MessageBox.Show("รอก่อนนะยังใช่งานไม่ได้งับ", "ตัวส่ง", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        // กระดาษปริ้น
        private void printDocument1_PrintPage_1(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Class.Print.PrintPreviewDialog.PrintLoan(e);
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
        private void Recycle()
        {
        }

        private void DGVGuarantor_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int currentMouseOverRow =  DGVGuarantor.HitTest(e.X, e.Y).RowIndex;
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
            if(DGVGuarantor.Rows.Count == 0)
            {
                MessageBox.Show("โปรดเลือกผู้กู้ก่อน", "ระบบ");
                TBTeacherNo.Focus();
            }
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

        private void TBGuarantorNo_KeyUp(object sender, KeyEventArgs e)
        {
            if (TBTeacherName.Text == "")
            {
                MessageBox.Show("โปรดใส่ข้อมูลด้านบนให้ครบถ้วนก่อน", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TBGuarantorNo.Text = "";
                TBTeacherNo.Focus();
            }
        }

        private void TBLoanAmount_TextChanged(object sender, EventArgs e)
        {
            if (CBPayMonth.SelectedIndex != -1 && CBPayYear.SelectedIndex != -1)
            {
                int Amount;
                String AmountLimit = LLoanAmount.Text.Remove(0, 1);
                AmountLimit = AmountLimit.Remove(AmountLimit.Length - 1);
                bool Check = int.TryParse(AmountLimit, out int LimitAmount);
                if (int.TryParse(TBLoanAmount.Text, out Amount) && (Check))
                {
                    if (Amount > LimitAmount)
                    {
                        if (MessageBox.Show("จำนวนเงินกู้ เกินกำหนดเงินค้ำ\r\n ต้องการทำต่อหรือไม่", "แจ้งเตือน", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
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

            
        }




        

        
        

        private void DGVGuarantor_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            LGuarantorAmount.Text = DGVGuarantor.Rows.Count + "/4";
            int LoanAmount = 0;
            for (int Count = 0; Count < DGVGuarantor.Rows.Count; Count++)
            {
                LoanAmount += int.Parse(DGVGuarantor.Rows[Count].Cells[2].Value.ToString());
            }
            LLoanAmount.Text = "(" + LoanAmount.ToString() + ")";

            //DGVGuarantor.Rows[0].Cells[3].Selected = true;


            //calculate Percent Cell Row


        }
        //DataGridViewRow RowGuarantor;
        //private void DGVGuarantor_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        //{
        //    MessageBox.Show("asdasd");
        //    //RowGuarantor = DGVGuarantor.CurrentRow;
        //    if (DGVGuarantor.CurrentCell.ColumnIndex == 2)
        //    {
        //        TextBox tb = (TextBox)e.Control;
        //        tb.KeyPress += new KeyPressEventHandler(TBCellGuarantorCredit_KeyPress);
        //    }
        //}
        

        private void DGVGuarantor_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            for(int Num = 0; Num < DGVRow.Count; Num++)
            {
                if(e.RowIndex == int.Parse(DGVRow[Num][0].ToString()))
                {
                    DGVRow.RemoveAt(Num);
                }
            }
            DGVGuarantorCredit.Rows.RemoveAt(e.RowIndex);
        }

        private void DGVGuarantor_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            DGVGuarantorCredit.Rows.Add(DGVGuarantor.Rows[e.RowIndex].Cells[0].Value, DGVGuarantor.Rows[e.RowIndex].Cells[1].Value);
        }

        
        private void TBLoanAmount_Leave(object sender, EventArgs e)
        {
            bool CheckNum = Double.TryParse(TBLoanAmount.Text, out Double LoanAmount);
            LoanAmount = LoanAmount * (Convert.ToDouble(TBInterestRate.Text) / 100);
            if(CheckNum == true)
            {
                Double Percent = 100 / LoanAmount * int.Parse(DGVGuarantor.Rows[0].Cells[2].Value.ToString());
                if (int.Parse(DGVGuarantor.Rows[0].Cells[2].Value.ToString()) >= LoanAmount)
                    Percent = 50;
                


                    //int AVGPercent = Convert.ToInt32(Percent) / (DGVGuarantorCredit.Rows.Count - 1);
                    Double lastRow = 0;
                for(int Num = 0;Num < DGVGuarantorCredit.Rows.Count; Num++)
                {
                    
                    //if (Percent > Convert.ToDouble(Convert.ToInt32(Percent)) + 0.5)
                    //    Percent += 1;
                    //else if (Percent <= Convert.ToDouble(Convert.ToInt32(Percent)) + 0.5)
                    //    Percent -= 1;
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
                    if(Credit > int.Parse(DGVGuarantor.Rows[Num].Cells[2].Value.ToString()))
                    {
                        Credit = Convert.ToInt32(DGVGuarantor.Rows[Num].Cells[2].Value.ToString());
                        Percent = Credit * 100 / LoanAmount;
                    }

                    lastRow += Percent;

                    //if (lastRow > Convert.ToDouble(Convert.ToInt32(lastRow)) + 0.5)
                    //    lastRow += 1;
                    //else if (lastRow <= Convert.ToDouble(Convert.ToInt32(lastRow)) + 0.5)
                    //    lastRow -= 1;

                    DGVGuarantorCredit.Rows[Num].Cells[2].Value = Convert.ToInt32(Percent);
                    DGVGuarantorCredit.Rows[Num].Cells[3].Value = Convert.ToInt32(LoanAmount * Percent / 100);
                    
                    if (lastRow < 100 && Num == DGVGuarantorCredit.Rows.Count - 1)
                    {
                        Percent = 100 - lastRow;
                        //int nnn = int.
                        //Double ddd = Convert.ToInt32(Percent);

                        Credit = Convert.ToInt32(LoanAmount) * Math.Round(Percent, 2) / 100;
                        for (int Count = 0; Count < 4; Count++)
                        {
                            if (Convert.ToInt32(DGVGuarantor.Rows[Count].Cells[2].Value.ToString()) - Convert.ToInt32(DGVGuarantorCredit.Rows[Count].Cells[3].Value.ToString()) >= Convert.ToInt32(Credit))
                            {
                                DGVGuarantorCredit.Rows[Count].Cells[2].Value = Convert.ToInt32(DGVGuarantorCredit.Rows[Count].Cells[2].Value.ToString()) + Convert.ToInt32(Percent);
                                DGVGuarantorCredit.Rows[Count].Cells[3].Value = Convert.ToInt32(int.Parse(DGVGuarantorCredit.Rows[Count].Cells[3].Value.ToString()) + Credit);
                                break;
                            }
                        }
                    }
                }

            }
            else
            {
                for(int Num = 0; Num < DGVGuarantorCredit.Rows.Count; Num++)
                {
                    DGVGuarantorCredit.Rows[Num].Cells[2].Value = "";
                    DGVGuarantorCredit.Rows[Num].Cells[3].Value = "";
                }
            }

        }

        List<String[]> DGVRow = new List<String[]> { };
        private void DGVGuarantorCredit_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //Double SumPercentEdit = 0;
            ////float Percent;
            //Double Percent = 0;
            //bool CheckDouble;
            int NumCell = 0;
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

            if(e.ColumnIndex == 2 && Check)
            {
                Double Credit = Convert.ToDouble(NumCell) / 100 * int.Parse(TBLoanAmount.Text);
                if (Credit <= int.Parse(DGVGuarantor.Rows[e.RowIndex].Cells[2].Value.ToString()))
                {
                    DGVGuarantorCredit.Rows[e.RowIndex].Cells[3].Value = Credit;
                }
                else
                {
                    MessageBox.Show("เกินวงเงินที่ค้ำได้", "ระบบ");
                    DGVGuarantorCredit.Rows[e.RowIndex].Cells[2].Value = "";
                    DGVGuarantorCredit.Rows[e.RowIndex].Cells[3].Value = "";

                }
            }
            else if(e.ColumnIndex == 3 && Check)
            {
                if(NumCell <= int.Parse(DGVGuarantor.Rows[e.RowIndex].Cells[2].Value.ToString()))
                {
                    Double Percent = NumCell * 100 / int.Parse(TBLoanAmount.Text);
                    if (Percent > Convert.ToDouble(Convert.ToInt32(Percent)) + 0.5)
                        Percent += 1;
                    DGVGuarantorCredit.Rows[e.RowIndex].Cells[2].Value = Convert.ToInt32(Percent);
                }
                else
                {
                    MessageBox.Show("เกินวงเงินที่ค้ำได้", "ระบบ");
                    DGVGuarantorCredit.Rows[e.RowIndex].Cells[2].Value = "";
                    DGVGuarantorCredit.Rows[e.RowIndex].Cells[3].Value = "";
                }
            }
            //try
            //{
            //    if (/*DGVRow.Count == 0*/DGVGuarantor.Rows[e.RowIndex].Cells[4].Value.ToString() == "1")
            //    {
            //        DGVRow.Add(new String[] { e.RowIndex.ToString(), DGVGuarantor.Rows[e.RowIndex].Cells[3].Value.ToString() });
            //        DGVGuarantor.Rows[e.RowIndex].Cells[4].Value = 0;
            //    }
            //    else
            //    {
            //        DGVRow[e.RowIndex][1] = DGVGuarantor.Rows[e.RowIndex].Cells[3].Value.ToString();
            //    }
            //    Percent = Double.Parse(DGVGuarantor.Rows[e.RowIndex].Cells[3].Value.ToString());
            //    CheckDouble = true;
            //}
            //catch
            //{
            //    Percent = 100 / DGVGuarantor.Rows.Count;
            //    for (int Num = 0; Num < DGVGuarantor.Rows.Count; Num++)
            //    {
            //        DGVGuarantor.Rows[Num].Cells[3].Value = Percent.ToString("###.##");
            //        DGVGuarantor.Rows[Num].Cells[4].Value = 1;
            //    }
            //    DGVRow.Clear();
            //    CheckDouble = false;
            //}

            //if (Percent.ToString("###.##") != "" && (CheckDouble))
            //{
            //    //DGVRow.Add(new String[] { e.RowIndex.ToString(), Percent.ToString() });
            //    bool RepeatRow = false;
            //    for (int Num = 0; Num < DGVRow.Count; Num++)
            //    {
            //        //ถ้าแถวซ้ำกับที่เคยเปลี่ยนให้แทนค่าใหม่เข้าไป
            //        if (DGVRow[Num][0] == e.RowIndex.ToString())
            //        {
            //            DGVRow[Num][1] = DGVGuarantor.Rows[e.RowIndex].Cells[3].Value.ToString();
            //            RepeatRow = true;
            //        }
            //        SumPercentEdit += Double.Parse(DGVRow[Num][1]);
            //    }

            //    if (DGVRow.Count == DGVGuarantor.Rows.Count)
            //    {
            //        SumPercentEdit = Double.Parse(DGVGuarantor.Rows[e.RowIndex].Cells[3].Value.ToString());
            //        DGVRow.Clear();
            //        DGVRow.Add(new String[] { e.RowIndex.ToString(), DGVGuarantor.Rows[e.RowIndex].Cells[3].Value.ToString() });
            //        for (int Num = 0; Num < DGVGuarantor.Rows.Count; Num++)
            //        {
            //            if (Num != e.RowIndex)
            //            {
            //                DGVGuarantor.Rows[Num].Cells[4].Value = 1;
            //            }
            //        }

            //    }
            //    //ถ้าแถวที่เคยเปลี่ยนไปไม่ซ้ำเลย เพิ่มListเข้าไป และบวกรวมเปอเซนต์ แต่ rowDatagrid ต้องน้อยกว่า 4
            //    else if (!RepeatRow && DGVRow.Count < DGVGuarantor.Rows.Count)
            //    {
            //        //DGVRow.Add(new String[] { e.RowIndex.ToString(), Percent.ToString() });
            //        //DGVGuarantor.Rows[e.RowIndex].Cells[4].Value = 0;
            //        SumPercentEdit += Double.Parse(DGVGuarantor.Rows[e.RowIndex].Cells[3].Value.ToString());
            //    }


            //    //เปอร์เซต์ที่ขาดอยู่ / กับจำนวนคนที่ยังไม่เคยแก้
            //    Percent = (100 - SumPercentEdit) / (DGVGuarantor.Rows.Count - DGVRow.Count);
            //    if (Percent > 100 || Percent < 0)
            //    {

            //        DGVGuarantor.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "";
            //        //DGVGuarantor.CurrentCell = DGVGuarantor.Rows[e.RowIndex].Cells[e.ColumnIndex];
            //        for (int Num = 0; Num < DGVRow.Count; Num++)
            //        {
            //            if (DGVRow[Num][0] == e.RowIndex.ToString())
            //            {
            //                DGVRow.RemoveAt(Num);
            //                DGVGuarantor.Rows[e.RowIndex].Cells[4].Value = 1;
            //                break;
            //            }
            //        }
            //        DGVGuarantor.Rows[e.RowIndex].Cells[3].Selected = true;
            //    }
            //    else
            //    {
            //        for (int Count = 0; Count < DGVGuarantor.Rows.Count; Count++)
            //        {
            //            //for (int Num = 0; Num < DGVRow.Count; Num++)
            //            //{
            //            //    if(DGVRow[Num][0])
            //            //}
            //            if (int.Parse(DGVGuarantor.Rows[Count].Cells[4].Value.ToString()) == 1)
            //            {
            //                DGVGuarantor.Rows[Count].Cells[3].Value = Percent.ToString("###.##");
            //            }
            //        }

            //    }

            //    //MessageBox.Show("sdfsdf");
            //}
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

        private void BCalculate_Click(object sender, EventArgs e)
        {
            if(DGVGuarantorCredit.Rows.Count > 0 && TBLoanAmount.Text != "")
            {
                Double SumCredit = 0;
                for (int Num = 0; Num < DGVGuarantorCredit.Rows.Count; Num++)
                {
                    SumCredit += Convert.ToDouble(DGVGuarantorCredit.Rows[Num].Cells[3].Value.ToString());
                }
                Double Difference = (Convert.ToDouble(TBLoanAmount.Text) * (Convert.ToDouble(TBInterestRate.Text) / 100)) - SumCredit;
                for (int Num = 0; Num < DGVGuarantorCredit.Rows.Count; Num++)
                {
                    Double Result = 0;
                    if(Convert.ToDouble(DGVGuarantorCredit.Rows[Num].Cells[3].Value.ToString()) < Convert.ToDouble(DGVGuarantor.Rows[Num].Cells[2].Value.ToString()))
                    {
                        Difference = Convert.ToDouble(DGVGuarantor.Rows[Num].Cells[2].Value.ToString()) - Convert.ToDouble(DGVGuarantorCredit.Rows[Num].Cells[3].Value.ToString());
                        
                    }
                }
            }
        }
    }
}


