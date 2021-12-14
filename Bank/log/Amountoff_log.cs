using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankTeacher.Bank.log
{
    public partial class Amountoff_log : Form
    {
        bool CheckMember = false;
        List<String> ColumsDGV = new List<string>();
        List<int> SizeColumsDGV = new List<int>();
        List<DataGridViewAutoSizeColumnMode> AutoSizeDGV = new List<DataGridViewAutoSizeColumnMode>();
        String DateSelected = "";

        /// <summary> 
        /// SQLDefault 
        /// <para>[0] Search TeacherAdd INPUT: {Text} {Date}</para>
        /// <para>[1] Select Bill INPUT: {TeacherAddbyNo} {Date} </para>
        /// </summary> 
        private String[] SQLDefault = new String[]
         { 
           //[0] Search TeacherAdd INPUT: {Text} {Date}
           "SELECT a.TeacherNo , CAST(ISNULL(c.Prefixname,'') + a.Fname + ' ' + a.Lname  as nvarchar(255)) , Fname \r\n " +
          "FROM Personal.dbo.tblTeacherHis as a \r\n " +
          "INNER JOIN EmployeeBank.dbo.tblShareWithdraw as b on a.TeacherNo = b.TeacherNoAddBy \r\n " +
          "LEFT JOIN BaseData.dbo.tblPrefix as c on a.PrefixNo = c.PrefixNo  \r\n " +
          "WHERE ( a.TeacherNo LIKE '%{Text}%' or CAST(ISNULL(c.Prefixname,'') + a.Fname + ' ' + a.Lname  as nvarchar(255)) LIKE  '%{Text}%' ) and CAST(b.DateAdd as Date) LIKE '%{Date}%'   \r\n " +
          "GROUP BY a.TeacherNo , CAST(ISNULL(c.Prefixname,'') + a.Fname + ' ' + a.Lname  as nvarchar(255)) ,Fname \r\n " +
          "ORDER BY Fname"
           ,
           //[1] Select Bill INPUT: {TeacherAddbyNo} {Date}
           "SELECT a.TeacherNo , CAST(ISNULL(c.Prefixname,'') + a.Fname + ' ' + a.Lname  as nvarchar(255)) ,b.WithDrawNo , d.Name , Amount, Fname \r\n " +
          "FROM Personal.dbo.tblTeacherHis as a \r\n " +
          "INNER JOIN EmployeeBank.dbo.tblShareWithdraw as b on a.TeacherNo = b.TeacherNoAddBy \r\n " +
          "LEFT JOIN BaseData.dbo.tblPrefix as c on a.PrefixNo = c.PrefixNo  \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblBillDetailPayment as d on b.BillDetailPayMentNo = d.BillDetailPaymentNo \r\n " +
          "WHERE  a.TeacherNo LIKE '%{TeacherAddbyNo}%' and CAST(b.DateAdd as Date) LIKE '%{Date}%'   \r\n " +
          "GROUP BY a.TeacherNo , CAST(ISNULL(c.Prefixname,'') + a.Fname + ' ' + a.Lname  as nvarchar(255)) ,b.WithDrawNo , d.Name , Amount, Fname \r\n " +
          "ORDER BY Fname"
           ,

         };
        public Amountoff_log()
        {
            InitializeComponent();
            for(int x = 0; x < DGV.Columns.Count; x++)
            {
                ColumsDGV.Add(DGV.Columns[x].HeaderText);
                SizeColumsDGV.Add(DGV.Columns[x].Width);
                AutoSizeDGV.Add(DGV.Columns[x].AutoSizeMode);
            }
            DTP_ValueChanged(new object(), new EventArgs());
            RBday_CheckedChanged(new object(), new EventArgs());
        }

        private void TBTeacherNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (TBTeacherNo.Enabled == true)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if(TBTeacherNo.Text.Length >= 6)
                    {
                        DataTable dt = Class.SQLConnection.InputSQLMSSQL(SQLDefault[1]
                            .Replace("{TeacherAddbyNo}", TBTeacherNo.Text)
                            .Replace("{Date}",DateSelected));
                        if (dt.Rows.Count != 0)
                        {
                            int PositionHeader = 0;
                            int AmountBill = 0;
                            int Amount = 0;
                            for (int x = 0; x < dt.Rows.Count; x++)
                            {
                                AmountBill += Convert.ToInt32(dt.Rows[x][4].ToString());
                                if (DGV.Rows.Count == 0)
                                {
                                    DGV.Rows.Add(dt.Rows[x][2].ToString(), dt.Rows[x][3].ToString(), dt.Rows[x][4].ToString());
                                    PositionHeader = x;
                                }
                                else
                                {
                                    if (DGV.Rows[PositionHeader].Cells[0].Value.ToString() == dt.Rows[x][2].ToString())
                                    {
                                        DGV.Rows.Add("", dt.Rows[x][3].ToString(), dt.Rows[x][4].ToString());
                                    }
                                    else
                                    {
                                        Amount += AmountBill;
                                        AmountBill = 0;
                                        DGV.Rows.Add(dt.Rows[x][2].ToString(), dt.Rows[x][3].ToString(), dt.Rows[x][4].ToString());
                                        PositionHeader = DGV.Rows.Count -1;
                                    }

                                }
                                if(x == dt.Rows.Count - 1)
                                {
                                    if(Amount == 0)
                                    {
                                        Amount = AmountBill;
                                    }
                                    DGV.Rows.Add("", "สรุปยอดทั้งหมด", Amount);
                                    DGV.Rows[DGV.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Yellow;
                                }
                            }
                            CheckMember = true;
                        }
                        else
                        {
                            MessageBox.Show("ไม่พบรายการ", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                else if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
                {
                    if (CheckMember)
                    {
                        DGV.Rows.Clear();
                        CheckMember = false;
                    }
                }
            }
        }

        private void BSearchTeacher_Click(object sender, EventArgs e)
        {
            if(BSearchTeacher.Enabled == true)
            {
                Bank.Search IN = new Bank.Search(SQLDefault[0]
                    .Replace("{Date}", DateSelected), "");
                IN.ShowDialog();
                if (Bank.Search.Return[0] != "")
                {
                    TBTeacherNo.Text = Bank.Search.Return[0];
                    TBTeacherName.Text = Bank.Search.Return[1];
                    CheckMember = false;
                    TBTeacherNo_KeyDown(new object(), new KeyEventArgs(Keys.Enter));
                }
            }
        }
        private void RBday_CheckedChanged(object sender, EventArgs e)
        {
            if (RBday.Checked)
            {
                CheckMember = false;
                TBTeacherName.Text = "-";
                TBTeacherNo.Text = "-";
                DGV.Rows.Clear();
                panel2.Enabled = false;
                DGV.Columns.Clear();
                DGV.Columns.Add("TeacherAddByID", "รหัส");
                DGV.Columns[0].Width = 100;
                DGV.Columns.Add("TeacherAddByName", "ชื่อ-สกุล");
                DGV.Columns[1].Width = 175;
                DGV.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                for(int x = 0; x < ColumsDGV.Count; x++)
                {
                    DGV.Columns.Add(ColumsDGV[x], ColumsDGV[x]);
                    DGV.Columns[x + 2].Width = SizeColumsDGV[x];
                    //DGV.Columns[x + 2].AutoSizeMode = AutoSizeDGV[x];
                }
                DataTable dt = Class.SQLConnection.InputSQLMSSQL(SQLDefault[1]
                            .Replace("{TeacherAddbyNo}", "")
                            .Replace("{Date}", DateSelected));
                if(dt.Rows.Count != 0)
                {
                    int PositionHeader = 0;
                    int AmountTeacher = 0;
                    int Amount = 0;
                    for (int x = 0; x < dt.Rows.Count; x++) 
                    {
                        AmountTeacher += Convert.ToInt32(dt.Rows[x][4].ToString());
                        if (DGV.Rows.Count == 0)
                        {
                            DGV.Rows.Add(dt.Rows[x][0].ToString(), dt.Rows[x][1].ToString(), dt.Rows[x][2].ToString(), dt.Rows[x][3].ToString(), dt.Rows[x][4].ToString());
                            PositionHeader = x;
                        }
                        else
                        {
                            if (DGV.Rows[PositionHeader].Cells[1].Value.ToString() == dt.Rows[x][1].ToString())
                            {
                                DGV.Rows.Add("", "", dt.Rows[x][2].ToString(), dt.Rows[x][3].ToString(), dt.Rows[x][4].ToString());
                            }
                            else
                            {
                                DGV.Rows.Add("", "",  "สรุปยอดต่อคน", "", AmountTeacher);
                                DGV.Rows[DGV.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Cornsilk;
                                Amount += AmountTeacher;
                                AmountTeacher = 0;
                                DGV.Rows.Add(dt.Rows[x][0].ToString() ,dt.Rows[x][1].ToString(), dt.Rows[x][2].ToString(), dt.Rows[x][3].ToString(), dt.Rows[x][4].ToString());
                                PositionHeader = DGV.Rows.Count - 1;
                            }

                        }
                        if (x == dt.Rows.Count - 1)
                        {
                            if (Amount == 0)
                            {
                                Amount = AmountTeacher;
                            }
                            DGV.Rows.Add("", "", "สรุปยอดต่อคน", "", AmountTeacher);
                            DGV.Rows[DGV.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Cornsilk;
                            DGV.Rows.Add("", "", "", "สรุปยอดทั้งหมด", Amount);
                            DGV.Rows[DGV.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Yellow;
                        }
                    }
                }
            }
        }

        private void RBSelectTeacherAdd_CheckedChanged(object sender, EventArgs e)
        {
            if (RBSelectTeacherAdd.Checked)
            {
                TBTeacherName.Text = "";
                TBTeacherNo.Text = "";
                DGV.Rows.Clear();
                panel2.Enabled = true;
                DGV.Columns.Clear();
                for(int x = 0; x < ColumsDGV.Count; x++)
                {
                    DGV.Columns.Add(ColumsDGV[x], ColumsDGV[x]);
                    DGV.Columns[x].Width = SizeColumsDGV[x];
                    DGV.Columns[x].AutoSizeMode = AutoSizeDGV[x];
                    if (DGV.Columns[x].HeaderText == "รูปแบบ")
                        DGV.Columns[x].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
        }

        private void ClearForm()
        {
            TBTeacherName.Text = "";
            TBTeacherNo.Text = "";
            DGV.Rows.Clear();
        }

        private void DTP_ValueChanged(object sender, EventArgs e)
        {
            String Year = DTP.Value.ToString("yyyy");
            String Month = DTP.Value.ToString("MM");
            String Day = DTP.Value.ToString("dd");
            if (Convert.ToInt32(Month) < 10)
            {
                Month = "0" + Convert.ToInt32(Month);
            }
            if (Convert.ToInt32(Day) < 10)
            {
                Day = "0" + Convert.ToInt32(Day);
            }
            DateSelected = (Convert.ToDateTime(Year + '-' + Month + '-' + Day)).ToString("yyyy-MM-dd");
            ClearForm();
            if (RBday.Checked)
                RBday_CheckedChanged(new object(), new EventArgs());
            else if (RBSelectTeacherAdd.Checked)
                RBday_CheckedChanged(new object(), new EventArgs());
        }
    }
}
