using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace example.GOODS
{
    public partial class pay : Form
    {

        //------------------------- index -----------------
        public static int x = 0;
        public static int sum = 0;
        public static int SelectIndexRowDelete = -1;
        int Check = 0;
        //----------------------- index code -------------------- ////////


        /// <summary> 
        /// SQLDafault 
        /// <para>[0] SELECT MEMBER INPUT: {TeacherNo} </para> 
        /// <para>[1] SELECT TIME INPUT : - </para>
        /// <para>[2] INSERT Bill and BillDetail INPUT: {TeacherAddBy} {TeacherNo} {TypeNo} {LoanNo} {Amount} {Mount} {Year} {Payment}</para>
        /// <para>[3] UPDATE SavingAmount INPUT: {TeacherNo} {Amount}</para>
        /// <para>[4] UPDATE REMAIN INPUT: {TeacherNo} {Amount}</para>
        /// <para>[5] AmountpayANDAmountLoanINMonth INPUT: {TeacherNo}</para>
        /// <para>[6] SELECT Detail Member INPUT: {TeacherNo}</para>
        ///<para>[7] SELECT Type pay (2Table) INPUT : {Month} , {Year} , {TeacherNo} {DateSet} </para>
        ///<para>[8] Check BillDetailPayment INPUT: - </para>
        /// </summary> 
        private String[] SQLDefault = new String[]
         { 
          //[0] SELECT MEMBER INPUT: {TeacherNo} 
          "SELECT a.TeacherNo ,  CAST(c.PrefixName+' '+Fname +' '+ Lname as NVARCHAR),a.StartAmount \r\n"+
          "FROM EmployeeBank.dbo.tblMember as a \r\n"+
          "LEFT JOIN Personal.dbo.tblTeacherHis as b ON a.TeacherNo = b.TeacherNo \r\n"+
          "LEFT JOIN BaseData.dbo.tblPrefix as c ON c.PrefixNo = b.PrefixNo \r\n"+
          "WHERE a.TeacherNo LIKE 'T%' and MemberStatusNo = 1 \r\n"+
          "ORDER BY Fname;"
          ,
          //[1] SELECT TIME INPUT : -
          "SELECT CONVERT (DATE , CURRENT_TIMESTAMP); "
             ,
          //[2] INSERT Bill and BillDetail INPUT: {TeacherAddBy} {TeacherNo} {TypeNo} {LoanNo} {Amount} {Mount} {Year} {Payment}
          "DECLARE @BillNo INT; \r\n " +
          "DECLARE @TeacherNo VARCHAR(20); \r\n " +
          "DECLARE @TeacherNoAddBy VARCHAR (20); \r\n " +
          " \r\n " +
          "SET @TeacherNoAddBy = '{TeacherAddBy}'; \r\n " +
          "SET @TeacherNo = '{TeacherNo}'; \r\n " +
          " \r\n " +
          "INSERT INTO EmployeeBank.dbo.tblBill (TeacherNo, TeacherNoAddBy , DateAdd) \r\n " +
          "VALUES (@TeacherNo, @TeacherNoAddBy , CURRENT_TIMESTAMP); \r\n " +
          " \r\n " +
          "SELECT @BillNo = SCOPE_IDENTITY(); \r\n " +
          " \r\n " +
          "INSERT INTO EmployeeBank.dbo.tblBillDetail (BillNo,TypeNo,LoanNo,Amount,Mount,Year,BillDetailPaymentNo) \r\n " +
          "VALUES (@BillNo,'{TypeNo}','{LoanNo}','{Amount}','{Mount}','{Year}','{Payment}'); "
             ,
         //[3] UPDATE SavingAmount INPUT: {TeacherNo} {Amount}
          "DECLARE @Saving INT; \r\n " +
          " \r\n " +
          "SET @Saving = (SELECT SavingAmount \r\n " +
          "FROM EmployeeBank.dbo.tblShare \r\n " +
          "WHERE TeacherNo = '{TeacherNo}'); \r\n " +
          " \r\n " +
          "UPDATE EmployeeBank.dbo.tblShare \r\n " +
          "SET SavingAmount = @Saving + '{Amount}' \r\n " +
          "WHERE TeacherNo = '{TeacherNo}' \r\n " +
          " \r\n " +
          " "
          ,
         //[4] UPDATE REMAIN INPUT: {TeacherNo} {Amount}
          "DECLARE @Remain INT; \r\n " +
          " \r\n " +
          "SET @Remain = (SELECT Remain \r\n " +
          "FROM EmployeeBank.dbo.tblLoanPay \r\n " +
          "WHERE TeacherNo = '{TeacherNo}'); \r\n " +
          " \r\n " +
          "UPDATE EmployeeBank.dbo.tblLoanPay \r\n " +
          "SET Remain = @Remain - '{Amount}' \r\n " +
          "WHERE TeacherNo = '{TeacherNo}' \r\n " +
          " \r\n " +
          " "
          ,
          //[5] AmountpayANDAmountLoanINMonth INPUT: {TeacherNo}
          "SELECT Mb.TeacherNo,Mb.StartAmount AS  Amountpay,Mb.DateAdd AS Datepay,Lp.Amount AS AmountLoan,Ln.DateAdd AS DateLoan \r\n"+
          "FROM EmployeeBank.dbo.tblMember as Mb\r\n"+
          "LEFT JOIN EmployeeBank.dbo.tblLoan as Ln on Mb.TeacherNo = Ln.TeacherNo\r\n"+
          "LEFT JOIN EmployeeBank.dbo.tblLoanPay as Lp  on Ln.TeacherNo = Lp.TeacherNo\r\n" +
          "WHERE Mb.TeacherNo = '{TeacherNo}';"
             ,
          //[6] SELECT Detail Member INPUT: {TeacherNo}
          "SELECT a.TeacherNo , CAST(c.PrefixName+' '+[Fname] +' '+ [Lname] as NVARCHAR)AS Name, b.IdNo AS TeacherID,   \r\n " +
          " b.TeacherLicenseNo,b.IdNo AS IDNo,b.TelMobile ,a.StartAmount,CAST(d.MemberStatusName as nvarchar) AS UserStatususing   \r\n " +
          " FROM EmployeeBank.dbo.tblMember as a   \r\n " +
          " LEFT JOIN Personal.dbo.tblTeacherHis as b ON a.TeacherNo = b.TeacherNo   \r\n " +
          " LEFT JOIN BaseData.dbo.tblPrefix as c ON c.PrefixNo = b.PrefixNo  \r\n " +
          " INNER JOIN EmployeeBank.dbo.tblMemberStatus as d on a.MemberStatusNo = d.MemberStatusNo  \r\n " +
          " WHERE a.TeacherNo LIKE 'T{TeacherNo}%' and a.MemberStatusNo = 1   \r\n " +
          " ORDER BY a.TeacherNo;  "
          ,
          //[7] SELECT Type pay (2Table) INPUT : {Month} , {Year} , {TeacherNo} {DateSet}
          "SELECT a.TeacherNo, StartAmount , f.TypeName  \r\n " +
          " FROM EmployeeBank.dbo.tblMember as a   \r\n " +
          " LEFT JOIN EmployeeBank.dbo.tblBill as b on a.TeacherNo = b.TeacherNo  \r\n " +
          " LEFT JOIN EmployeeBank.dbo.tblBillDetail as c on b.BillNo = c.BillNo  \r\n " +
          " LEFT JOIN Personal.dbo.tblTeacherHis as d on a.TeacherNo = d.TeacherNo   \r\n " +
          " LEFT JOIN BaseData.dbo.tblPrefix as e on d.PrefixNo = e.PrefixNo   \r\n " +
          " LEFT JOIN EmployeeBank.dbo.tblBillDetailType as f on c.TypeNo = f.TypeNo  \r\n " +
          " WHERE a.TeacherNo NOT IN   \r\n " +
          " (SELECT aa.TeacherNo FROM EmployeeBank.dbo.tblBill as aa   \r\n " +
          " LEFT JOIN EmployeeBank.dbo.tblBillDetail as bb on aa.BillNo = bb.BillNo   \r\n " +
          " LEFT JOIN EmployeeBank.dbo.tblLoan as cc on aa.TeacherNo = cc.TeacherNo \r\n " +
          " WHERE bb.Mount = {Month} and bb.Year = {Year} and bb.TypeNo = 1 ) \r\n " +
          " and a.TeacherNo LIKE '{TeacherNo}' and MemberStatusNo = 1 and c.TypeNo = 1 and DATEADD(YYYY,0,'{DateSet}') >= a.DateAdd \r\n " +
          " GROUP BY a.TeacherNo,f.TypeName, StartAmount   \r\n " +
          " \r\n " +
          " \r\n " +
          " SELECT a.TeacherNo, CONVERT(float , (CONVERT(float , LoanAmount) / CONVERT(float , PayNo) + CONVERT(float,LoanAmount) * (InterestRate / 100)) )  , f.TypeName  \r\n " +
          " FROM EmployeeBank.dbo.tblMember as a   \r\n " +
          " LEFT JOIN EmployeeBank.dbo.tblBill as b on a.TeacherNo = b.TeacherNo  \r\n " +
          " LEFT JOIN EmployeeBank.dbo.tblBillDetail as c on b.BillNo = c.BillNo  \r\n " +
          " LEFT JOIN Personal.dbo.tblTeacherHis as d on a.TeacherNo = d.TeacherNo   \r\n " +
          " LEFT JOIN BaseData.dbo.tblPrefix as e on d.PrefixNo = e.PrefixNo   \r\n " +
          " LEFT JOIN EmployeeBank.dbo.tblBillDetailType as f on c.TypeNo = f.TypeNo  \r\n " +
          " LEFT JOIN EmployeeBank.dbo.tblLoan as g on a.TeacherNo = g.TeacherNo  \r\n " +
          " WHERE a.TeacherNo NOT IN   \r\n " +
          " (SELECT aa.TeacherNo FROM EmployeeBank.dbo.tblBill as aa   \r\n " +
          " LEFT JOIN EmployeeBank.dbo.tblBillDetail as bb on aa.BillNo = bb.BillNo   \r\n " +
          " LEFT JOIN EmployeeBank.dbo.tblLoan as cc on aa.TeacherNo = cc.TeacherNo \r\n " +
          " WHERE bb.Mount = {Month} and bb.Year = {Year} and bb.TypeNo = 2 ) \r\n " +
          " and a.TeacherNo LIKE '{TeacherNo}' and MemberStatusNo = 1 and c.TypeNo = 2 and DATEADD(YYYY,0,'{DateSet}') >= a.DateAdd\r\n " +
          " GROUP BY a.TeacherNo,f.TypeName, StartAmount ,CONVERT(float , (CONVERT(float , LoanAmount) / CONVERT(float , PayNo) + CONVERT(float,LoanAmount) * (InterestRate / 100)) ) "

          ,
          //[8] Check BillDetailPayment INPUT: -  
          "SELECT Name , BillDetailpaymentNo  \r\n " +
          "FROM EmployeeBank.dbo.tblBillDetailPayment \r\n " +
          "WHERE Status = 1 "
          ,

         };
        public pay(int TabIndex)
        {
            InitializeComponent();
            tabControl1.SelectedIndex = TabIndex;

            Font F = new Font("TH Sarabun New", 16, FontStyle.Regular);



            dataGridView1.ColumnHeadersDefaultCellStyle.Font = F;
            dataGridView2.ColumnHeadersDefaultCellStyle.Font = F;
        }
        //------------------------- FormSize -----------------
        // Comment!
        private void Menuf_SizeChanged(object sender, EventArgs e)
        {
            int x = this.Width / 2 - panel1.Size.Width / 2;
            int y = this.Height / 2 - panel1.Size.Height / 2;
            panel1.Location = new Point(x, y);
            //TB3.Location = new Point(TB+120, 15);
            //panel7.Size = new Size(TB+500,72); 
            //tabControl1.Location = new Point(x,y);

            //panel1.MinimumSize = new Size(columnSize * 40, rowSize * 40);
            //panel1.Height = rowSize * 40;
            //panel1.Width = columnSize * 40;
            //MessageBox.Show(this.Width + "" + this.Height);
        }
        //----------------------- End code -------------------- ////////



        //----------------------- PullSQL -------------------- ////////
        // Comment! Pull SQL Member & CheckTBTeacherNo

        // Comment! Pull SQL Member & CheckTBTeacherNo
        private void BSearchTeacher_Click(object sender, EventArgs e)
        {
            //try
            //{
            Bank.Search IN = new Bank.Search(SQLDefault[0].Replace("{TeacherNo}", ""));
            IN.ShowDialog();
            TBTeacherNo.Text = Bank.Search.Return[0];
            TBTeacherNo_KeyDown(sender, new KeyEventArgs(Keys.Enter));
            //}
            //catch (Exception x)
            //{
            //    Console.WriteLine(x);
            //}

            sum = 0; x = 0;
            label5.Text = sum.ToString();
            dataGridView1.Rows.Clear();
            TBStartAmountShare.Clear();
            CBStatus.SelectedIndex = -1;
            CByeartap1.SelectedIndex = -1;
            CBMonth.SelectedIndex = -1;
        }
        // บันทึกรายการเเล้ว ส่งขึ้นไปบนฐานข้อมูล
        private void BTsave_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count != 0)
            {
                DialogResult dialogResult = MessageBox.Show("ยืนยันการชำระ", "การเเจ้งเตือนการชำระ", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dialogResult == DialogResult.Yes)
                {
                    //do something /
                    String LoanNo = "";
                    int TypeNo = 0;

                    for (int x = 0; x < dataGridView1.Rows.Count; x++)
                    {
                        string[] MountandYear = dataGridView1.Rows[x].Cells[0].Value.ToString().Replace(" ", "").Split('/');
                        if (dataGridView1.Rows[x].Cells[1].Value.ToString().Contains("สะสม"))
                        {
                            DataTable LoanID = Class.SQLConnection.InputSQLMSSQL(SQLDefault[6].Replace("{TeacherNo}", TBTeacherNo.Text));
                            if (LoanID.Rows.Count == 1)
                            {
                                LoanNo = LoanID.Rows[0][0].ToString();
                            }
                            TypeNo = 1;
                        }
                        else if (dataGridView1.Rows[x].Cells[1].Value.ToString().Contains("หนี้"))
                        {
                            TypeNo = 2;
                        }
                        try
                        {
                            example.Class.ComboBoxPayment Payment = (CBB4Oppay.SelectedItem as example.Class.ComboBoxPayment);
                            Class.SQLConnection.InputSQLMSSQL(SQLDefault[2].Replace("{TeacherNo}", TBTeacherNo.Text)
                                .Replace("{TeacherAddBy}", Class.UserInfo.TeacherName)
                                .Replace("{TypeNo}", TypeNo.ToString())
                                .Replace("{LoanNo}", LoanNo)
                                .Replace("{Amount}", dataGridView1.Rows[x].Cells[2].Value.ToString())
                                .Replace("{Mount}", MountandYear[1])
                                .Replace("{Year}", MountandYear[0])
                                .Replace("{Payment}", Payment.No));
                            if (TypeNo == 1)
                            {
                                Class.SQLConnection.InputSQLMSSQL(SQLDefault[3].Replace("{TeacherNo}", TBTeacherNo.Text)
                                    .Replace("{Amount}", dataGridView1.Rows[x].Cells[2].Value.ToString()));
                            }
                            else if (TypeNo == 2)
                            {
                                Class.SQLConnection.InputSQLMSSQL(SQLDefault[4].Replace("{TeacherNo}", TBTeacherNo.Text)
                                     .Replace("{Amount}", dataGridView1.Rows[x].Cells[2].Value.ToString()));
                            }
                            MessageBox.Show("การชำระเสร็จสิ้น", "การเเจ้งเตือนการชำระ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CBMonth.SelectedIndex = -1;
                            CBStatus.SelectedIndex = -1;
                            CBB4Oppay.SelectedIndex = -1;
                            CByeartap1.SelectedIndex = -1;
                            CBB4Oppay.Enabled = false;
                            CBStatus.Enabled = false;
                            CBMonth.Enabled = false;
                            dataGridView1.Rows.Clear();
                            label5.Text = "0";
                        }
                        catch
                        {
                            MessageBox.Show("การชำระล้มเหลว", "การเเจ้งเตือนการชำระ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                else if (dialogResult == DialogResult.No)
                {
                    //do something else/
                    MessageBox.Show("การชำระล้มเหลว", "การเเจ้งเตือนการชำระ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            else
            {
                MessageBox.Show("รายการชำระไม่ถูกต้อง", "การเเจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        //----------------------- End code -------------------- ////////


        //-------------------------- if.Enbled Text ------------------------
        // if message in textCByer nothing will not Open next
        // ถ้า ไม่มีข้อความ ใน กล่องปี จะไม่เปิดใช่งานกล่อง ถัดไป
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CByeartap1.SelectedIndex != -1)
            {
                CBMonth.Enabled = true;
            }
            else
            {
                CBMonth.Enabled = false;

            }
            CBStatus.SelectedIndex = -1;
            TBStartAmountShare.Text = "";
        }
        // if message in textMonth nothing will not Open next
        // ถ้า ไม่มีข้อความ ใน กล่องเดือน จะไม่เปิดใช่งานกล่อง ถัดไป
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CBMonth.SelectedIndex != -1)
            {
                CBStatus.Items.Clear();
                CBStatus.Enabled = true;
                TBStartAmountShare.Text = "0";
                BTAdd.Enabled = false;
                int Same = 1;
                ComboBox[] cb = new ComboBox[] { CBStatus };
                DataSet ds = Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[7]
                    .Replace("{Month}", CBMonth.Text)
                    .Replace("{Year}", CByeartap1.Text)
                    .Replace("{TeacherNo}", TBTeacherNo.Text)
                    .Replace("{DateSet}", (Convert.ToDateTime(CByeartap1.Text + "-" + CBMonth.Text + "-" + 1)).ToString("yyyy-MM-dd")));
                for (int a = 0; a < ds.Tables[0].Rows.Count; a++)
                {
                    for (int x = 0; x < cb.Length; x++)
                    {
                        cb[x].Items.Add(new example.Class.ComboBoxPay(ds.Tables[0].Rows[a][2].ToString(),
                        ds.Tables[0].Rows[a][1].ToString(),
                        ds.Tables[0].Rows[a][0].ToString()));

                    }
                }
                for (int a = 0; a < ds.Tables[1].Rows.Count; a++)
                {
                    for (int x = 0; x < cb.Length; x++)
                    {
                        float.TryParse(ds.Tables[1].Rows[a][1].ToString(), out float Balance);
                        if (Balance >= Convert.ToInt32(Balance) + 0.5)
                        {
                            Balance++;
                        }
                        else
                        {
                            Balance = Convert.ToInt32(Balance);
                        }
                        cb[x].Items.Add(new example.Class.ComboBoxPay(ds.Tables[1].Rows[a][2].ToString() + " " + Same,
                        Balance.ToString(),
                        ds.Tables[1].Rows[a][0].ToString()));
                        Same++;
                    }
                }


                button4.Enabled = true;

            }
            else
            {
                CBStatus.Enabled = false;
                CBStatus.SelectedIndex = -1;
            }
        }
        // if message in Text nothing will not Open next
        // ถ้า ไม่มีข้อความ ใน กล่อง จะไม่เปิดใช่งานกล่อง ถัดไป
        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CByeartap2.SelectedIndex != -1)
            {
                BTResearchtap2.Enabled = true;
            }
        }
        // if message in Text nothing will not Open next
        // ถ้า ไม่มีข้อความ ใน กล่อง จะไม่เปิดใช่งานกล่อง ถัดไป
        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CByeartap3.SelectedIndex != -1)
            {
                BTResearchtap3.Enabled = true;
            }
        }
        // if message in Text nothing will not Open next
        // ถ้า ไม่มีข้อความ ใน กล่อง จะไม่เปิดใช่งานกล่อง ถัดไป
        private void CBB3_SelectedIndexChanged(object sender, EventArgs e)
        {
            example.Class.ComboBoxPay Status = (CBStatus.SelectedItem as example.Class.ComboBoxPay);
            if (CBStatus.SelectedIndex != -1 && TBTeacherNo.Text.Length == 6)
            {

                TBStartAmountShare.Text = Status.Balance;
                if (TBStartAmountShare.Text == "")
                    TBStartAmountShare.Text = "0";
                BTAdd.Enabled = true;
            }
            else
            {

                TBStartAmountShare.Text = "";
                CBStatus.Text = "";

                BTAdd.Enabled = false;
            }
        }
        // if message in Text nothing will not Open next
        // ถ้า ไม่มีข้อความ ใน กล่อง จะไม่เปิดใช่งานกล่อง ถัดไป
        private void CBB4Oppay_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount != 0)
            {
                CBB4Oppay.Enabled = true;
            }
            if (CBB4Oppay.SelectedIndex != -1)
            {
                BTsave.Enabled = true;

            }
            else { BTsave.Enabled = false; }
        }
        //----------------------- End code -------------------- ////////



        //------------------------- ClickDelete&Empty --------- //
        // Comment!
        private void button1_Click_1(object sender, EventArgs e)
        {
            CByeartap1.SelectedIndex = -1;
            CBMonth.SelectedIndex = -1;
            CBStatus.SelectedIndex = -1;
            CBStatus.Enabled = false;
            BTAdd.Enabled = false;
            TBStartAmountShare.Text = string.Empty;
            dataGridView1.Rows.Clear();
            label5.Text = "0";
        }
        // Comment!
        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int currentMouseOverRow = dataGridView1.HitTest(e.X, e.Y).RowIndex;
                if (currentMouseOverRow != -1)
                {
                    SelectIndexRowDelete = currentMouseOverRow;
                    ContextMenu m = new ContextMenu();
                    m.MenuItems.Add(new MenuItem("ลบออก"));
                    m.Show(dataGridView1, new Point(e.X, e.Y));
                    m.MenuItems[0].Click += new System.EventHandler(this.Delete_Click);
                }
            }
        }
        // Comment!
        private void Delete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count != 0)
            {
                sum -= int.Parse(dataGridView1.Rows[SelectIndexRowDelete].Cells[2].Value.ToString());
                x = sum;
                label5.Text = sum.ToString();
            }
            if (SelectIndexRowDelete != -1)
            {
                dataGridView1.Rows.RemoveAt(SelectIndexRowDelete);
                SelectIndexRowDelete = -1;
                if (dataGridView1.Rows.Count == 0)
                {
                    CBB4Oppay.Enabled = false;
                    CBB4Oppay.SelectedIndex = -1;
                }
            }
        }
        //----------------------- End code -------------------- ////////

        //------------------------- SUMAmountShare --------- //
        // Comment! //
        private void BTAdd_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.ToString() != "")
            {
                if (dataGridView1.Rows.Count == 0)
                {
                    CBB4Oppay.Enabled = true;
                    if (TBStartAmountShare.Text != "")
                    {
                        x += int.Parse(TBStartAmountShare.Text);
                        sum = x;
                        label5.Text = sum.ToString();
                    }
                    else
                    {
                        TBStartAmountShare.Text = "0";
                    }
                    String Time = CByeartap1.Text + "/" + CBMonth.Text;
                    dataGridView1.Rows.Add(Time, CBStatus.Text, TBStartAmountShare.Text);
                }
                else
                {
                    int TicketName = 0;
                    for (int x = 0; x < dataGridView1.Rows.Count; x++)
                    {
                        if (CBStatus.Text == dataGridView1.Rows[x].Cells[1].Value.ToString())
                        {
                            TicketName = 1;
                        }

                    }
                    if (TicketName == 0)
                    {
                        String Time = CByeartap1.Text + "/" + CBMonth.Text;
                        dataGridView1.Rows.Add(Time, CBStatus.Text, TBStartAmountShare.Text);
                    }
                }

            }


            //dataGridView1.Rows.Add(DateTime.Today.Date.ToString(), CBStatus.Text, TBStartAmountShare.Text);



            //DataGridViewRow row = (DataGridViewRow)dataGridView1.Rows[];
            //row.Cells[1].Value = CBStatus.SelectedItem.ToString();
            //row.Cells[2].Value = TBStartAmountShare.Text;
            //dataGridView1.Rows.Add(row);
        }
        //----------------------- End code -------------------- ////////













        // ------------------------ not working -------------- //
        private void button3_Click(object sender, EventArgs e)
        {

            //Menu p = new Menu();
            //CloseFrom(p);
            //p.MdiParent = this;
            //p.WindowState = FormWindowState.Maximized;
            //p.Show();
            //this.Hide();
        }
        public void tabPage2_Click(object sender, EventArgs e)
        {
            //TabPage t = tabControl1.TabPages[1];
            //tabControl1.SelectTab(t); 
            ////go to tab
        }
        private void TBStartAmountShare_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && (e.KeyChar != '\b'))
            {
                e.Handled = true;
            }
        }

        private void TBTeacherNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (TBTeacherNo.Text.Length == 6)
                {
                    DataTable dt = Class.SQLConnection.InputSQLMSSQL(SQLDefault[6].Replace("T{TeacherNo}", TBTeacherNo.Text));
                    if (dt.Rows.Count != 0)
                    {
                        TBTeacherName.Text = dt.Rows[0][1].ToString();
                        TBTeacherBill.Text = dt.Rows[0][2].ToString();
                        TBTeacherIDNo.Text = dt.Rows[0][3].ToString();
                        TBidno.Text = dt.Rows[0][4].ToString();
                        TBTel.Text = dt.Rows[0][5].ToString();
                        TBStartAmount2.Text = dt.Rows[0][6].ToString();
                        TBstatus.Text = dt.Rows[0][7].ToString();
                        Check = 1;
                        CBStatus.SelectedIndex = -1;
                        CByeartap1.Enabled = true;
                    }

                }

            }
            else if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
            {
                if (Check == 1)
                {
                    sum = 0;
                    x = 0;
                    label5.Text = sum.ToString();
                    dataGridView1.Rows.Clear();
                    TBStartAmountShare.Text = "";
                    CBStatus.SelectedIndex = -1;
                    TBTeacherBill.Text = "";
                    TBTeacherName.Text = "";
                    CBStatus.Enabled = false;
                    Check = 0;
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            label5.Text = "0";
            dataGridView1.Rows.Clear();
            DataSet ds = Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[7]
                .Replace("{Month}", CBMonth.Text)
                .Replace("{Year}", CByeartap1.Text)
                .Replace("{TeacherNo}", TBTeacherNo.Text)
                .Replace("{DateSet}", (Convert.ToDateTime(CByeartap1.Text + "-" + CBMonth.Text + "-" + 1)).ToString("yyyy-MM-dd")));
            if (ds.Tables[0].Rows.Count != 0 || ds.Tables[1].Rows.Count != 0)
            {
                String Time = CByeartap1.Text + "/" + CBMonth.Text;
                int Same = 1;
                for (int x = 0; x < ds.Tables[0].Rows.Count; x++)
                {
                    if (dataGridView1.Rows.ToString() != "")
                    {
                        dataGridView1.Rows.Add(Time, ds.Tables[0].Rows[x][2], ds.Tables[0].Rows[x][1]);
                        label5.Text = (Convert.ToInt32(label5.Text) + int.Parse(ds.Tables[0].Rows[x][1].ToString())).ToString();
                    }

                }
                for (int x = 0; x < ds.Tables[1].Rows.Count; x++)
                {
                    if (dataGridView1.Rows.ToString() != "")
                    {
                        float.TryParse(ds.Tables[1].Rows[x][1].ToString(), out float Balance);
                        if (Balance >= Convert.ToInt32(Balance) + 0.5)
                        {
                            Balance++;
                        }
                        else
                        {
                            Balance = Convert.ToInt32(Balance);
                        }
                        dataGridView1.Rows.Add(Time, ds.Tables[1].Rows[x][2] + " " + Same, Balance);
                        Same++;
                        label5.Text = (Convert.ToInt32(label5.Text) + Balance).ToString();
                    }
                }
                sum = Convert.ToInt32(label5.Text);
                CBB4Oppay.Enabled = true;
            }
            else
            {
                MessageBox.Show("ไม่พบรายการ", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void pay_Load(object sender, EventArgs e)
        {
            int Year = Convert.ToInt32(example.GOODS.Menu.Date[0]);
            for (int x = 0; x < 4; x++)
            {
                CByeartap1.Items.Add(Year);
                CByeartap2.Items.Add(Year);
                CByeartap3.Items.Add(Year);
                Year--;
            }
            ComboBox[] cb = new ComboBox[] { CBB4Oppay };
            DataTable dtPayment = Class.SQLConnection.InputSQLMSSQL(SQLDefault[8]);
            for (int a = 0; a < dtPayment.Rows.Count; a++)
                for (int x = 0; x < cb.Length; x++)
                    cb[x].Items.Add(new example.Class.ComboBoxPayment(dtPayment.Rows[a][0].ToString(),
                        dtPayment.Rows[a][1].ToString()));
        }
        //----------------------- End code -------------------//
    }
}
