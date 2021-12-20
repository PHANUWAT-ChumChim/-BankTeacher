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
        public static String[] Return = { "" ,"", ""};
        bool HaveCollumn3 = false;
        String SQLCODE;
       
        /// <summary>
        /// <para>Search Form</para>
        /// <para>SQL ที่ใส่มาต้องสามารถ ค้นหาได้ด้วย แทนที่ด้วย {Text}</para>
        /// <para>หากไม่ต้องการ Collumn 3 ที่เพิ้มขึ้นมาให้ใส่ค่าเป็น ""</para>
        /// </summary>
        public Search(String SQLCode , String Collumn3Text = "")
        {
            InitializeComponent();
            Return = new string[] { "", "", "" };
            if(Collumn3Text != "")
            {
                DGV.Columns.Add(Collumn3Text,Collumn3Text);
                HaveCollumn3 = true;
            }
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            SQLCODE = SQLCode;
            DataTable dt = Class.SQLConnection.InputSQLMSSQL(SQLCode.Replace("{Text}",""));
            for (int x = 0; x < dt.Rows.Count; x++)
            {
                if (HaveCollumn3)
                {
                    try
                    {
                        if (Decimal.TryParse(dt.Rows[x][2].ToString(), out decimal value))
                        {
                            DGV.Rows.Add(dt.Rows[x][0], dt.Rows[x][1], Decimal.Truncate(Convert.ToDecimal(dt.Rows[x][2].ToString())));
                        }
                        else
                        {
                            DGV.Rows.Add(dt.Rows[x][0], dt.Rows[x][1], Decimal.Truncate(Convert.ToDecimal(dt.Rows[x][2].ToString() + "0.0")));
                        }
                    }
                    catch
                    {
                            DGV.Rows.Add(dt.Rows[x][0], dt.Rows[x][1], dt.Rows[x][2].ToString());
                    }
                }
                else
                {
                    DGV.Rows.Add(dt.Rows[x][0], dt.Rows[x][1]);
                }
                if (x % 2 == 1)
                {
                    DGV.Rows[x].DefaultCellStyle.BackColor = Color.AliceBlue;
                }
            }
            TBSearch.Focus();
        }
        private void TBTeacherNo_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = Class.SQLConnection.InputSQLMSSQL(SQLCODE
                .Replace("{Text}", TBSearch.Text));
            if (DGV.Rows.Count != 0) { DGV.Rows.Clear(); }
            for (int x = 0; x < dt.Rows.Count; x++)
            {
                if (HaveCollumn3)
                {
                    try
                    {
                        if (Decimal.TryParse(dt.Rows[x][2].ToString(), out decimal value))
                        {
                            DGV.Rows.Add(dt.Rows[x][0], dt.Rows[x][1], Decimal.Truncate(Convert.ToDecimal(dt.Rows[x][2].ToString())));
                        }
                        else
                        {
                            DGV.Rows.Add(dt.Rows[x][0], dt.Rows[x][1], Decimal.Truncate(Convert.ToDecimal(dt.Rows[x][2].ToString() + "0.0")));
                        }
                    }
                    catch
                    {
                        DGV.Rows.Add(dt.Rows[x][0], dt.Rows[x][1], dt.Rows[x][2].ToString());
                    }
                }
                else
                {
                    DGV.Rows.Add(dt.Rows[x][0], dt.Rows[x][1]);
                }
                if (x % 2 == 1)
                {
                    DGV.Rows[x].DefaultCellStyle.BackColor = Color.AliceBlue;
                }
                if (x % 2 == 1)
                {
                    DGV.Rows[x].DefaultCellStyle.BackColor = Color.AliceBlue;
                }
            }
        }

        private void TBSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                if(DGV.Rows.Count != 0)
                {
                    try
                    {
                        Return = new String[]
                        {
                            DGV.Rows[0].Cells[0].Value.ToString(),
                            DGV.Rows[0].Cells[1].Value.ToString(),
                            DGV.Rows[0].Cells[2].Value.ToString(),
                        };
                    }
                    catch
                    {
                        Return = new String[]
                        {
                            DGV.Rows[0].Cells[0].Value.ToString(),
                            DGV.Rows[0].Cells[1].Value.ToString(),
                        };
                    }
                    this.Dispose();
                }
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void Search_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                try
                {
                    Return = new String[]
                    {
                            DGV.Rows[e.RowIndex].Cells[0].Value.ToString(),
                            DGV.Rows[e.RowIndex].Cells[1].Value.ToString(),
                            DGV.Rows[e.RowIndex].Cells[2].Value.ToString(),
                    };
                }
                catch
                {
                    Return = new String[]
                        {
                            DGV.Rows[e.RowIndex].Cells[0].Value.ToString(),
                            DGV.Rows[e.RowIndex].Cells[1].Value.ToString(),
                        };
                }
                this.Dispose();
            }
        }
    }
}
