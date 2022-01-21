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
            B_Save.Enabled = false;
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
                BankTeacher.Bank.Menu.perShare = Convert.ToInt32(TBPerShare.Text);
                BankTeacher.Bank.Menu.MinLoan = Convert.ToInt32(TBMinLoan.Text);
                MessageBox.Show("เสร็จสิ้น", "ตั้งค่า", MessageBoxButtons.OK, MessageBoxIcon.Information);
                B_Save.Enabled = false;
            }
            else
                MessageBox.Show("ค่าสูงสุดต้องไม่น้อยกว่าค่าต่ำสุด", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            
        }
        private void B_Cancel_Click(object sender, EventArgs e)
        {
            BExitForm_Click(new object(), new EventArgs());
        }


        private void button1_Click(object sender, EventArgs e)
        {
            BankTeacher.Bank.SQLEditing f = new SQLEditing();
            f.ShowDialog();
        }

        private void TBMinLoan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && (e.KeyChar != '\b'))
            {
                e.Handled = true;
            }
        }

        private void BExitForm_Click(object sender, EventArgs e)
        {
            BankTeacher.Class.FromSettingMedtod.ReturntoHome(this);
        }


        private void tabControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (TB_Max.Text != "" || TB_Min.Text != "" || TBPerShare.Text != "" || TBMinLoan.Text != "")
                {
                    TBMinLoan.Text = "";
                    TBPerShare.Text = "";
                    TB_Max.Text = "";
                    TB_Min.Text = "";
                }
                else
                {
                    BExitForm_Click(new object(), new EventArgs());
                }
            }
        }

        private void TB_Min_TextChanged(object sender, EventArgs e)
        {
            B_Save.Enabled = true;
        }

        private void TB_Max_TextChanged(object sender, EventArgs e)
        {
            B_Save.Enabled = true;
        }

        private void TBPerShare_TextChanged(object sender, EventArgs e)
        {
            B_Save.Enabled = true;
        }

        private void TBMinLoan_TextChanged(object sender, EventArgs e)
        {
            B_Save.Enabled = true;
        }
    }
}