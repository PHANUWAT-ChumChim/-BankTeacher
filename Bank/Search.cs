using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace example.Bank
{
    public partial class Search : Form
    {
        public static String[] Return = { "" };
        String SQLCODE;
       

        public Search(String SQLCode)
        {
            InitializeComponent();
            Return = new String[] { "" };
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            SQLCODE = SQLCode;
            DataTable dt = Class.SQLConnection.InputSQLMSSQL(SQLCode.Replace("{Text}",""));
            for (int x = 0; x < dt.Rows.Count; x++)
            {
                dataGridView1.Rows.Add(dt.Rows[x][0], dt.Rows[x][1],dt.Rows[x][2]);

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
            if (TBSearch.Text != "")
            {
                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    dataGridView1.Rows.Add(dt.Rows[x][0], dt.Rows[x][1],dt.Rows[x][2]);
                }
            }
            else
            {
                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    dataGridView1.Rows.Add(dt.Rows[x][0], dt.Rows[x][1], dt.Rows[x][2]);
                }


            }
        }
    }
}
