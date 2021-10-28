using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace example.Bank.Pay
{
    public partial class Calculator : Form
    {
        public static bool Return = false;
        public Calculator(int Balance)
        {
            InitializeComponent();
            TBAmount.Text = Balance.ToString();
        }

        private void TBGetAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if(TBGetAmount.Text != "")
                    if (Convert.ToInt32(TBTON.Text) > -1)
                    {
                        Return = true;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("กรุณากรอกจำนวนเงินที่รับมาให้ถูกต้อง", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        TBGetAmount.Text = "";
                        Return = false;
                    }
            }
            else if(e.KeyCode == Keys.Escape)
            {
                Return = false;
                this.Close();
            }
        }

        private void TBGetAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!Char.IsNumber(e.KeyChar)) && (!Char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void TBGetAmount_TextChanged(object sender, EventArgs e)
        {
            if (TBGetAmount.Text.Length > 0)
            {
                if (Int32.TryParse(TBGetAmount.Text, out int xx) && xx < 0)
                {
                    TBGetAmount.Text = "";
                }
                else if (!(Int32.TryParse(TBGetAmount.Text, out int y)))
                {
                    TBGetAmount.Text = "";
                }
            }
            if (Int32.TryParse(TBGetAmount.Text , out int x))
            {
                    TBTON.Text = (x - Convert.ToInt32(TBAmount.Text)).ToString();
                if (Convert.ToInt32(TBTON.Text) > 0)
                    TBTON.ForeColor = System.Drawing.Color.Green;
                else if (Convert.ToInt32(TBTON.Text) < 0)
                    TBTON.ForeColor = System.Drawing.Color.Red;
                else
                    TBTON.ForeColor = System.Drawing.Color.Black;
            }
        }
    }
}
