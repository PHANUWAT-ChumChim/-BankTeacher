﻿using System;
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
    public partial class InfoLoan : Form
    {
        int Check = 0;
        /// <summary>
        /// <para>[0] SELECT MemberLonn  INPUT: {TeacherNo}</para>
        /// <para>[1] SELECT LOAN INPUT: {TeacherNo} </para>
        /// <para>[2] SELECT Detail Loan INPUT: {LoanID} </para>
        /// </summary>
        private String[] SQLDefault =
        {
             //[0] SELECT MemberLonn  INPUT: {Text}
          " SELECT TOP(20) TeacherNo , NAME , SavingAmount  \r\n" +
          " FROM(   \r\n " +
          " SELECT a.TeacherNo, CAST(c.PrefixName + ' ' + Fname + ' ' + Lname AS nvarchar)AS NAME,SavingAmount,Fname ,LoanStatusNo \r\n " +
          " FROM EmployeeBank.dbo.tblLoan as a  \r\n " +
          " LEFT JOIN Personal.dbo.tblTeacherHis as b on a.TeacherNo = b.TeacherNo  \r\n " +
          " LEFT JOIN BaseData.dbo.tblPrefix as c on b.PrefixNo = c.PrefixNo  \r\n " +
          " LEFT JOIN EmployeeBank.dbo.tblShare as d on a.TeacherNo = d.TeacherNo  \r\n " +
          " WHERE a.LoanStatusNo != 4 and a.LoanStatusNo != 3 \r\n " +
          " GROUP BY a.TeacherNo,CAST(c.PrefixName+' '+Fname+' '+Lname as NVARCHAR),d.SavingAmount ,Fname , LoanStatusNo) AS A   \r\n " +
          " WHERE a.TeacherNo LIKE '%{Text}%' or Fname LIKE '%{Text}%'  \r\n " +
          " ORDER BY Fname;   "
                ,
          //[1] SELECT LOAN INPUT: {TeacherNo} : 
          "SELECT a.LoanNo , CAST(d.PrefixName + ' ' + Fname + ' ' + Lname AS NVARCHAR) \r\n " +
          " FROM EmployeeBank.dbo.tblLoan as a  \r\n " +
          " LEFT JOIN EmployeeBank.dbo.tblGuarantor as b on a.LoanNo = b.LoanNo  \r\n " +
          " LEFT JOIN Personal.dbo.tblTeacherHis as c on a.TeacherNo = c.TeacherNo \r\n " +
          " LEFT JOIN BaseData.dbo.tblPrefix as d on c.PrefixNo = d.PrefixNo \r\n " +
          " WHERE a.TeacherNo = '{TeacherNo}' and LoanStatusNo != 4  \r\n " +
          " GROUP BY a.LoanNo , CAST(d.PrefixName + ' ' + Fname + ' ' + Lname AS NVARCHAR) \r\n " +
          " ORDER BY a.LoanNo  "

          ,
          //[2] SELECT Detail Loan INPUT: {LoanID}
          "SELECT b.TeacherNo , CAST(d.PrefixName + ' ' + c.Fname + ' ' + c.Lname AS NVARCHAR) AS NameTeacher,CAST(DateAdd as date), \r\n " +
          "a.PayDate,MonthPay,YearPay,PayNo,InterestRate,LoanAmount,b.Amount,a.LoanStatusNo  \r\n " +
          ",TeacherNoAddBy, CAST(f.PrefixName + ' ' + e.Fname + ' ' + e.Lname AS NVARCHAR) AS NameTeacherAddby  \r\n " +
          "FROM EmployeeBank.dbo.tblLoan as a   \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblGuarantor as b on a.LoanNo = b.LoanNo  \r\n " +
          "LEFT JOIN Personal.dbo.tblTeacherHis as c on b.TeacherNo = c.TeacherNo   \r\n " +
          "LEFT JOIN BaseData.dbo.tblPrefix as d on c.PrefixNo = d.PrefixNo    \r\n " +
          "LEFT JOIN Personal.dbo.tblTeacherHis as e on a.TeacherNoAddBy = e.TeacherNo  \r\n " +
          "LEFT JOIN BaseData.dbo.tblPrefix as f on e.PrefixNo = f.PrefixNo    \r\n " +
          "WHERE a.LoanNo = '{LoanID}' and LoanStatusNo != 4 ; \r\n " +
          " \r\n " +
          "SELECT Concat(b.Mount , '/' , Year) \r\n " +
          "FROM EmployeeBank.dbo.tblLoan as a  \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblBillDetail as b on a.LoanNo = b.LoanNo \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblBill as c on b.BillNo = c.BillNo \r\n " +
          "WHERE a.LoanNo = '{LoanID}' and TypeNo = '2' and Cancel != 0; "
          ,


        };
        public InfoLoan()
        {
            InitializeComponent();
            
        }

        private void BSearchTeacher_Click(object sender, EventArgs e)
        {
            Bank.Search IN;
            try
            {

                IN = new Bank.Search(SQLDefault[0]);
                IN.ShowDialog();
                TBTeacherNo.Text = Bank.Search.Return[0];
                TBTeacherName.Text = Bank.Search.Return[1];
                comboBox1.Enabled = true;
                comboBox1.Items.Clear();
                Check = 1;
                comboBox1.Items.Clear();
                comboBox1.SelectedIndex = -1;
                TBTeacherName.Text = "";
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                TBLoanStatus.Text = "";
                TBLoanNo.Text = "";
                TBSavingAmount.Text = "";
                DGVGuarantor.Rows.Clear();
                DGVLoanDetail.Rows.Clear();
                ComboBox[] cb = new ComboBox[] { comboBox1 };
                DataTable dt = Class.SQLConnection.InputSQLMSSQL(SQLDefault[1]
                    .Replace("{TeacherNo}", TBTeacherNo.Text));
                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    for (int aa = 0; aa < cb.Length; aa++)
                    {
                        cb[aa].Items.Add(new example.Class.ComboBoxPayment("รายการกู้ " + (x + 1), dt.Rows[x][0].ToString()));
                    }
                }
                if(Bank.Search.Return[0] == "")
                {
                    comboBox1.Enabled = false;
                    Check = 0;

                }
                    
            }
            catch (Exception x)
            {
                Console.WriteLine(x);
            }
        }

        private void TBTeacherNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (TBTeacherNo.Text.Length == 6)
                {
                    DGVGuarantor.Rows.Clear();
                    DataTable dt = Class.SQLConnection.InputSQLMSSQL(SQLDefault[1].Replace("{TeacherNo}", TBTeacherNo.Text));
                    if (dt.Rows.Count != 0)
                    {
                        TBTeacherName.Text = dt.Rows[0][1].ToString();
                        comboBox1.Enabled = true;
                        comboBox1.Items.Clear();
                        Check = 1;
                        ComboBox[] cb = new ComboBox[] { comboBox1 };
                        for (int x = 0; x < dt.Rows.Count; x++)
                        {
                            for (int aa = 0; aa < cb.Length; aa++)
                            {
                                cb[aa].Items.Add(new example.Class.ComboBoxPayment("รายการกู้ " + (x + 1), dt.Rows[x][0].ToString()));
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("รหัสผู้ใช้ไม่ถูกต้อง", "System", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                }

            }
            else if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
            {
                if (Check == 1)
                {
                    comboBox1.Items.Clear();
                    comboBox1.SelectedIndex = -1;
                    TBTeacherName.Text = "";
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    textBox4.Text = "";
                    textBox5.Text = "";
                    TBLoanStatus.Text = "";
                    TBLoanNo.Text = "";
                    TBSavingAmount.Text = "";
                    DGVGuarantor.Rows.Clear();
                    DGVLoanDetail.Rows.Clear();
                    comboBox1.Enabled = false;
                    Check = 0;
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            example.Class.ComboBoxPayment Loan = (comboBox1.SelectedItem as example.Class.ComboBoxPayment);
            DataSet ds = example.Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[2].Replace("{LoanID}", Loan.No));
            DGVGuarantor.Rows.Clear();
            if (ds.Tables[0].Rows.Count != 0)
            {
                for (int x = 0; x < ds.Tables[0].Rows.Count; x++)
                {
                    DGVGuarantor.Rows.Add(ds.Tables[0].Rows[x][0].ToString(), ds.Tables[0].Rows[x][1].ToString(), ds.Tables[0].Rows[x][9].ToString());
                }
                TBLoanNo.Text = Loan.No;
                textBox1.Text = ds.Tables[0].Rows[0][5].ToString();
                textBox3.Text = ds.Tables[0].Rows[0][4].ToString();
                textBox4.Text = (Convert.ToDouble(Convert.ToDouble(ds.Tables[0].Rows[0][8].ToString()) + (Convert.ToDouble(ds.Tables[0].Rows[0][8].ToString()) * Convert.ToDouble(ds.Tables[0].Rows[0][7].ToString()) / 100)).ToString());
                textBox5.Text = ds.Tables[0].Rows[0][6].ToString();
                TBInterestRate.Text = ds.Tables[0].Rows[0][7].ToString();
                TBLoanStatus.Text = ds.Tables[0].Rows[0][10].ToString();
                TBSavingAmount.Text = ds.Tables[0].Rows[0][2].ToString();
                DGVLoanDetail.Rows.Clear();
                TBTeacheraddbyNo.Text = ds.Tables[0].Rows[0][11].ToString();
                TBTeacheraddbyname.Text = ds.Tables[0].Rows[0][12].ToString();
                int Month = Convert.ToInt32(ds.Tables[0].Rows[0][4].ToString());
                int Year = Convert.ToInt32(ds.Tables[0].Rows[0][5].ToString());
                DGVLoanDetail.Rows.Clear();

                Double Interest = Convert.ToDouble(Convert.ToDouble(ds.Tables[0].Rows[0][8].ToString())) * (Convert.ToDouble(ds.Tables[0].Rows[0][7].ToString()) / 100) / Convert.ToDouble(ds.Tables[0].Rows[0][6].ToString());

                int Pay = Convert.ToInt32(Convert.ToDouble(ds.Tables[0].Rows[0][8].ToString()) / Convert.ToInt32(ds.Tables[0].Rows[0][6].ToString()));
                int SumInstallment = Convert.ToInt32(Pay + Interest);
                String StatusPay = "";

                for (int Num = 0; Num < int.Parse(ds.Tables[0].Rows[0][6].ToString()); Num++)
                {
                    if (Month > 12)
                    {
                        Month = 1;
                        Year++;
                    }
                    if (Num == Convert.ToInt32(ds.Tables[0].Rows[0][6].ToString()) - 1)
                    {
                        Interest = Convert.ToInt32((Convert.ToDouble(ds.Tables[0].Rows[0][8].ToString()) * (Convert.ToDouble(TBInterestRate.Text) / 100)) - (Convert.ToInt32(Interest) * Num));
                        Pay = Pay * Num;
                        Pay = Convert.ToInt32(ds.Tables[0].Rows[0][8].ToString()) - Pay;
                        SumInstallment = Convert.ToInt32(Pay + Interest);
                        
                    }
                    try
                    {
                        if (Month + "/" + Year == ds.Tables[1].Rows[Num][0].ToString())
                        {
                            StatusPay = "จ่ายแล้ว";
                        }
                        else
                        {
                            StatusPay = "ยังไม่จ่าย";
                        }
                    }
                    catch
                    {
                        StatusPay = "ยังไม่จ่าย";
                    }

                    DGVLoanDetail.Rows.Add($"{Month}/{Year}", Pay, Convert.ToInt32(Interest), SumInstallment,StatusPay);
                    Month++;
                }
            }
        }

        private void InfoLoan_SizeChanged(object sender, EventArgs e)
        {
            Class.FromSettingMedtod.ChangeSizePanal(this, panel1);
        }
    }
}
