using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static example.Class.ProtocolSharing.ConnectSMB;

namespace example.Bank
{
    public partial class CancelMembership : Form
    {
        int StatusBoxFile = 0;
        String imgeLocation = "";
        int Check = 0;
        

        /// <summary>
        /// <para> SQLDefault </para>
        /// <para> [0] Change Status Member INPUT: {TeacherNoAddBy} {TeacherNo} {Note} {DocStatusNo} {DocUploadPath} {Status} {TeacherNo} </para>
        /// </summary>
        private String[] SQLDefault = new String[]
        {
            //[0] Change Status Member INPUT: {TeacherNoAddBy} {TeacherNo} {Note} {DocStatusNo} {DocUploadPath} {Status} {TeacherNo}
              "INSERT INTO EmployeeBank.dbo.tblMemberResignation (TeacherNoAddBy,TeacherNo,Date,Note,DocStatusNo,DocUploadPath) \r\n " +
              "VALUES ('{TeacherNoAddBy}','{TeacherNo}',CURRENT_TIMESTAMP,'{Note}','{DocStatusNo}','{DocUploadPath}'); \r\n " +
              " \r\n " +
              "UPDATE EmployeeBank.dbo.tblMember \r\n " +
              "SET MemberStatusNo = '{Status}' \r\n " +
              "WHERE TeacherNo = '{TeacherNo}' "
            ,
            //[1] SELECT MEMBER INPUT: {Text}
                "SELECT TOP(20) a.TeacherNo , CAST(c.PrefixName+' '+[Fname] +' '+ [Lname] as NVARCHAR)AS Name, e.SavingAmount,    \r\n " +
              "b.TeacherLicenseNo,b.IdNo AS IDNo,b.TelMobile ,a.StartAmount,CAST(d.MemberStatusName as nvarchar) AS UserStatususing    \r\n " +
              "FROM EmployeeBank.dbo.tblMember as a    \r\n " +
              "LEFT JOIN Personal.dbo.tblTeacherHis as b ON a.TeacherNo = b.TeacherNo    \r\n " +
              "LEFT JOIN BaseData.dbo.tblPrefix as c ON c.PrefixNo = b.PrefixNo   \r\n " +
              "INNER JOIN EmployeeBank.dbo.tblMemberStatus as d on a.MemberStatusNo = d.MemberStatusNo  \r\n " +
              "LEFT JOIN EmployeeBank.dbo.tblShare as e on a.TeacherNo = e.TeacherNo \r\n " +
              "WHERE a.TeacherNo LIKE '%{Text}%'  or CAST(c.PrefixName+' '+[Fname] +' '+ [Lname] as NVARCHAR) LIKE '%{Text}%'   and a.MemberStatusNo = 1         \r\n " +
              "GROUP BY a.TeacherNo , CAST(c.PrefixName+' '+[Fname] +' '+ [Lname] as NVARCHAR), e.SavingAmount,    \r\n " +
              "b.TeacherLicenseNo,b.IdNo ,b.TelMobile ,a.StartAmount,CAST(d.MemberStatusName as nvarchar)   \r\n " +
              "ORDER BY a.TeacherNo; "
        };

        public CancelMembership()
        {
            InitializeComponent();
        }

        private void CancelMembership_SizeChanged(object sender, EventArgs e)
        {
            Class.FromSettingMedtod.ChangeSizePanal(this, panel1);
        }

        private void CancelMembership_Load(object sender, EventArgs e)
        {

        }

        private void BSearchTeacher_Click(object sender, EventArgs e)
        {
            try
            {
                Bank.Search IN = new Bank.Search(SQLDefault[1]);
                IN.ShowDialog();
                TBTeacherNo.Text = Bank.Search.Return[0];
            }
            catch (Exception x)
            {
                Console.WriteLine(x);
            }
        }

        private void TBTeacherNo_TextChanged(object sender, EventArgs e)
        {
            ////ต้องพิมพ์รหัสอาจารย์ถึง 6 ตัวถึงจะเข้าเงื่อนไข if
            //if (TBTeacherNo.Text.Length == 6)
            //{
            //    Class.SQLMethod.ResearchUserAllTLC(TBTeacherNo.Text, TBTeacherName, TBIDNo,1);
            //}
            //else
            //{
            //    TBIDNo.Text = "";
            //    TBTeacherName.Text = "";
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(TBTeacherName.Text != "")
            {
                int DocStaTus = 2;
                if (imgeLocation != "")
                { 
                    DocStaTus = 1;
                    
                    var smb = new example.Class.ProtocolSharing.ConnectSMB.SmbFileContainer("CancelLoan");
                    if (smb.IsValidConnection())
                    {
                        smb.SendFile(imgeLocation, TBTeacherName.Text + " Cancel.pdf");
                    }
                    else
                    {
                        MessageBox.Show("ไม่สามารถสร้างไฟล์ในที่นั้นได้", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                MessageBox.Show("ยกเลิกผู้ใช้เรียบร้อย","System",MessageBoxButtons.OK,MessageBoxIcon.Information);
                TBIDNo.Text = "";
                TBTeacherName.Text = "";
                TBTeacherNo.Text = "";
                textBox1.Text = "";
                imgeLocation = "";
            }
            else
            {
                MessageBox.Show("กรุณาใส่รหัสอาจารย์ให้ถูกต้อง","เตือน",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (StatusBoxFile == 0)
            {

                try
                {
                    OpenFileDialog dialog = new OpenFileDialog();
                    dialog.Filter = "pdf files(*.pdf)|*.pdf";
                    if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        imgeLocation = dialog.FileName;
                    }
                    if (imgeLocation != "")
                    {
                        button3.Text = "ส่งไฟล์";
                        StatusBoxFile = 1;
                    }

                }
                catch
                {
                    MessageBox.Show("An Error Occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (StatusBoxFile == 1)
            {
                var smb = new SmbFileContainer("Loan");
                if (smb.IsValidConnection())
                {
                    String Return = smb.SendFile(imgeLocation, "Loan_" + TBTeacherNo.Text + ".pdf");
                    MessageBox.Show(Return, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    StatusBoxFile = 0;
                    button3.Text = "เปิดไฟล์";
                    //label6.Text = "Scan(  ไม่พบ  )";
                    imgeLocation = "";
                }
                else
                {
                    MessageBox.Show("ไม่สามารถสร้างไฟล์ในที่นั้นได้", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void TBTeacherNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (TBTeacherNo.Text.Length == 6)
                {
                    try
                    {
                        DataSet ds = Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[1].Replace("{Text}", TBTeacherNo.Text));
                        TBTeacherName.Text = ds.Tables[0].Rows[0][1].ToString();
                        TBIDNo.Text = "รอใส่จ้าาา";
                        Check = 1;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
            }
            else if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back && Check == 1)
            {
                TBIDNo.Text = "";
                TBTeacherName.Text = "";
                Check = 0;
            }
        }
    }
}
