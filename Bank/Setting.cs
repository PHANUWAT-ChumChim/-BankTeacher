using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;


namespace BankTeacher.Bank
{
    public partial class Setting : Form
    {
        static int Min;
        static int Max;
        static bool chb;
        static Font FontSetting;


        /// <summary>
        /// SQLDafault
        /// <para>[0]Edit Setting INPUT: {DateAmountChange} {StartAmountMin} {StartAmountMax} {PerShare}</para>
        /// </summary>
        private static String[] SQLDefault = new String[]
        { 
             //[0]Edit Setting INPUT: {DateAmountChange} {StartAmountMin} {StartAmountMax} {PerShare}  {MinLoan}
             "UPDATE EmployeeBank.dbo.tblSettingAmount \r\n" +
             "SET DateAmountChange = {DateAmountChange}, StartAmountMin = {StartAmountMin} , StartAmountMax = {StartAmountMax} , PerShare = {PerShare} , MinLoan = {MinLoan}\r\n" +
             "WHERE SettingNo = 1 ;"
            ,
        };
        public Setting()
        {
            InitializeComponent();
            Console.WriteLine("==================Open Setting Form======================");
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            TB_Min.Text = BankTeacher.Bank.Menu.startAmountMin.ToString();
            TB_Max.Text = BankTeacher.Bank.Menu.startAmountMax.ToString();
            TBPerShare.Text = BankTeacher.Bank.Menu.perShare.ToString();
            TBMinLoan.Text = BankTeacher.Bank.Menu.MinLoan.ToString();
            if (BankTeacher.Bank.Menu.DateAmountChange == 1)
            {
                CHB_edittime.Checked = true;
            }
            chb = CHB_edittime.Checked;
            Min = Convert.ToInt32(TB_Min.Text);
            Max = Convert.ToInt32(TB_Max.Text);
        }

        private void B_Cancel_Click_1(object sender, EventArgs e)
        {
            this.Hide();
        }
        private void B_Save_Click_1(object sender, EventArgs e)
        {
            if (TB_Min.Text != Min.ToString() || TB_Max.Text != Max.ToString() || CHB_edittime.Checked != chb)
            {
                if (Convert.ToInt32(TB_Min.Text) <= Convert.ToInt32(TB_Max.Text))
                {
                    int TranChbToInt;
                    if (CHB_edittime.Checked == true)
                    {
                        TranChbToInt = 1;
                        BankTeacher.Bank.Menu.DateAmountChange = TranChbToInt;
                    }
                    else
                    {
                        TranChbToInt = 0;
                        BankTeacher.Bank.Menu.DateAmountChange = TranChbToInt;
                    }

                    Class.SQLConnection.InputSQLMSSQL(SQLDefault[0].Replace("{DateAmountChange}", TranChbToInt.ToString())
                        .Replace("{StartAmountMin}", TB_Min.Text)
                        .Replace("{StartAmountMax}", TB_Max.Text)
                        .Replace("{MinLoan}",TBMinLoan.Text));
                    BankTeacher.Bank.Menu.startAmountMin = Convert.ToInt32(TB_Min.Text);
                    BankTeacher.Bank.Menu.startAmountMax = Convert.ToInt32(TB_Max.Text);
                    BankTeacher.Bank.Menu.MinLoan = Convert.ToInt32(TBMinLoan.Text);
                    this.Hide();
                }
                else
                    MessageBox.Show("ค่าสูงสุดต้องไม่น้อยกว่าค่าต่ำสุด", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void TB_Min_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && (e.KeyChar != '\b'))
            {
                e.Handled = true;
            }
        }

        private void TB_Max_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && (e.KeyChar != '\b'))
            {
                e.Handled = true;
            }
        }

        private void B_Save_Click(object sender, EventArgs e)
        {

            if (TB_Min.Text != Min.ToString() || TB_Max.Text != Max.ToString() || CHB_edittime.Checked != chb)
            {
                if (Convert.ToInt32(TB_Min.Text) <= Convert.ToInt32(TB_Max.Text))
                {
                    int TranChbToInt;
                    if (CHB_edittime.Checked == true)
                    {
                        TranChbToInt = 1;
                        BankTeacher.Bank.Menu.DateAmountChange = TranChbToInt;
                    }
                    else
                    {
                        TranChbToInt = 0;
                        BankTeacher.Bank.Menu.DateAmountChange = TranChbToInt;
                    }

                    Class.SQLConnection.InputSQLMSSQL(SQLDefault[0].Replace("{DateAmountChange}", TranChbToInt.ToString())
                        .Replace("{StartAmountMin}", TB_Min.Text)
                        .Replace("{StartAmountMax}", TB_Max.Text)
                        .Replace("{PerShare}",TBPerShare.Text)
                        .Replace("{MinLoan}" , TBMinLoan.Text));
                    BankTeacher.Bank.Menu.startAmountMin = Convert.ToInt32(TB_Min.Text);
                    BankTeacher.Bank.Menu.startAmountMax = Convert.ToInt32(TB_Max.Text);
                    MessageBox.Show("เสร็จสิ้น", "ตั้งค่า", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                }
                else
                    MessageBox.Show("ค่าสูงสุดต้องไม่น้อยกว่าค่าต่ำสุด", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }
        private void B_Cancel_Click(object sender, EventArgs e)
        {
            this.Hide();

        }

        private void Setting_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private static void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BankTeacher.Bank.SQLEditing f = new SQLEditing();
            f.ShowDialog();
        }

        private void tabControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape)
                this.Close();
        }

        private void TBMinLoan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && (e.KeyChar != '\b'))
            {
                e.Handled = true;
            }
        }
    }
}