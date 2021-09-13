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


namespace example.Bank
{
    public partial class Setting : Form
    {
        static int Min;
        static int Max;
        static bool chb;
        static Font FontSetting;
       

        /// <summary>
        /// SQLDafault
        /// <para>[0]Edit Setting INPUT: {DateAmountChange} {StartAmountMin} {StartAmountMax}</para>
        /// </summary>
        private static String[] SQLDefault = new String[]
        { 
             //[0]Edit Setting INPUT: {DateAmountChange} {StartAmountMin} {StartAmountMax}
             "UPDATE EmployeeBank.dbo.tblSettingAmount \r\n" +
             "SET DateAmountChange = {DateAmountChange}, StartAmountMin = {StartAmountMin} , StartAmountMax = {StartAmountMax} \r\n" +
             "WHERE SettingNo = 1 ;"
            ,
        };
        public Setting()
        {
            InitializeComponent();
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            TB_Min.Text = example.GOODS.Menu.startAmountMin.ToString();
            TB_Max.Text = example.GOODS.Menu.startAmountMax.ToString();
            comboBox2.Text = example.GOODS.Menu.FontSize;
            if (example.GOODS.Menu.DateAmountChange == 1)
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
                        example.GOODS.Menu.DateAmountChange = TranChbToInt;
                    }
                    else
                    {
                        TranChbToInt = 0;
                        example.GOODS.Menu.DateAmountChange = TranChbToInt;
                    }

                    Class.SQLConnection.InputSQLMSSQL(SQLDefault[0].Replace("{DateAmountChange}", TranChbToInt.ToString())
                        .Replace("{StartAmountMin}", TB_Min.Text)
                        .Replace("{StartAmountMax}", TB_Max.Text));
                    example.GOODS.Menu.startAmountMin = Convert.ToInt32(TB_Min.Text);
                    example.GOODS.Menu.startAmountMax = Convert.ToInt32(TB_Max.Text);
                    this.Hide();
                }
                else
                    MessageBox.Show("ค่าสูงสุดต้องไม่น้อยกว่าค่าต่ำสุด", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }
     
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        { 
            Class.FromSettingMedtod.SetFont(comboBox1, int.Parse(comboBox2.SelectedItem.ToString()),label3);
            FontSetting = example.GOODS.Home.F;
            label3.Font = FontSetting;
            label1.Font = FontSetting;
            label2.Font = FontSetting;
            label3.Font = FontSetting;
            label4.Font = FontSetting;
            comboBox1.Font  = FontSetting;
            comboBox2.Font = FontSetting;
            TB_Max.Font = FontSetting;
            TB_Min.Font = FontSetting;
            B_Save.Font = FontSetting;
            B_Cancel.Font = FontSetting;
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
                        example.GOODS.Menu.DateAmountChange = TranChbToInt;
                    }
                    else
                    {
                        TranChbToInt = 0;
                        example.GOODS.Menu.DateAmountChange = TranChbToInt;
                    }

                    Class.SQLConnection.InputSQLMSSQL(SQLDefault[0].Replace("{DateAmountChange}", TranChbToInt.ToString())
                        .Replace("{StartAmountMin}", TB_Min.Text)
                        .Replace("{StartAmountMax}", TB_Max.Text));
                    example.GOODS.Menu.startAmountMin = Convert.ToInt32(TB_Min.Text);
                    example.GOODS.Menu.startAmountMax = Convert.ToInt32(TB_Max.Text);
                    MessageBox.Show("เสร็จสิ้น", "ตั้งค่า", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
    }
}