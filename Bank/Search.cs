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
    public partial class Search : Form
    {
        public static String[] Return = { "" };
        String SQLCODE;
       

        public Search(String SQLCode)
        {
            InitializeComponent();
            Console.WriteLine("==================Open Search Form======================");
            Return = new String[] { "" };
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            SQLCODE = SQLCode;
            DataTable dt = Class.SQLConnection.InputSQLMSSQL(SQLCode.Replace("{Text}",""));
            for (int x = 0; x < dt.Rows.Count; x++)
            {
                if (Decimal.TryParse(dt.Rows[x][2].ToString() , out decimal value))
                {
                    dataGridView1.Rows.Add(dt.Rows[x][0], dt.Rows[x][1], Decimal.Truncate(Convert.ToDecimal(dt.Rows[x][2].ToString())));
                }
                else
                {
                    dataGridView1.Rows.Add(dt.Rows[x][0], dt.Rows[x][1], Decimal.Truncate(Convert.ToDecimal(dt.Rows[x][2].ToString() + "0.0")));
                }
                if (x % 2 == 1)
                {
                    dataGridView1.Rows[x].DefaultCellStyle.BackColor = Color.AliceBlue;
                }
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                Return = new String[]
                {
                        dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString(),
                        dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString(),
                };

                this.Dispose();

            }
        }

        private void Search_Load(object sender, EventArgs e)
        {
        }

        private void TBTeacherNo_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = Class.SQLConnection.InputSQLMSSQL(SQLCODE
                .Replace("{Text}", TBSearch.Text));
            if (dataGridView1.Rows.Count != 0) { dataGridView1.Rows.Clear(); }
            for (int x = 0; x < dt.Rows.Count; x++)
            {
                if (Decimal.TryParse(dt.Rows[x][2].ToString(), out decimal value))
                {
                    dataGridView1.Rows.Add(dt.Rows[x][0], dt.Rows[x][1], Decimal.Truncate(Convert.ToDecimal(dt.Rows[x][2].ToString())));
                }
                else
                {
                    dataGridView1.Rows.Add(dt.Rows[x][0], dt.Rows[x][1], Decimal.Truncate(Convert.ToDecimal(dt.Rows[x][2].ToString() + "0.0")));
                }
                if (x % 2 == 1)
                {
                    dataGridView1.Rows[x].DefaultCellStyle.BackColor = Color.AliceBlue;
                }
            }
        }

        private void TBSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
            }
        }
    }
}
