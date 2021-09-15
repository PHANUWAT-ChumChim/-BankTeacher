using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace example.Bank.Loan
{
    public partial class CancelLoan : Form
    {
        public CancelLoan()
        {
            InitializeComponent();
        }

        private void CancelLoan_Load(object sender, EventArgs e)
        {
            Class.FromSettingMedtod.ChangeSizePanal(this, panel1);
        }
    }
}
