using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankTeacher.Bank
{
    public partial class UnpaidLoanList : Form
    {
        public Form FDividend;
        public UnpaidLoanList()
        {
            InitializeComponent();

            
        }

        private void BExitForm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UnpaidLoanList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                BExitForm_Click(new object(), new EventArgs());
            }
        }

        private void UnpaidLoanList_FormClosed(object sender, FormClosedEventArgs e)
        {
            FDividend.Enabled = true;
            FDividend.Show();
        }

        private void DGV_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                BExitForm_Click(new object(), new EventArgs());
            }
        }
    }
}
