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
    public partial class AddMember_Log : Form
    {
        public AddMember_Log()
        {
            InitializeComponent();
        }
        List<String[]> ColumnDGVHeaderSize = new List<string[]> { };

        private void RBday_CheckedChanged(object sender, EventArgs e)
        {
            if (RBday.Checked)
            {
                DGV.Rows.Clear();
                DGV.Columns.Clear();
                for(int x = 0; x < ColumnDGVHeaderSize.Count; x++)
                {
                    DGV.Columns.Add($"Column{x + 1}", ColumnDGVHeaderSize[x][0]);
                    DGV.Columns[x].Width = Convert.ToInt32(ColumnDGVHeaderSize[x][1]);
                }
            }
            else
            {
                DGV.Rows.Clear();
                DGV.Columns.Clear();
                for (int x = 1; x < ColumnDGVHeaderSize.Count; x++)
                {
                    DGV.Columns.Add($"Column{x}", ColumnDGVHeaderSize[x][0]);
                    DGV.Columns[x - 1].Width = Convert.ToInt32(ColumnDGVHeaderSize[x][1]);
                }
            }
        }

        private void AddMember_Log_Load(object sender, EventArgs e)
        {
            for(int x = 0; x < DGV.Columns.Count; x++)
            {
                ColumnDGVHeaderSize.Add(new string[] { DGV.Columns[x].HeaderText, DGV.Columns[x].Width.ToString() });
            }
        }
    }
}
