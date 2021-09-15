using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static example.Class.ProtocolSharing.SBM;

namespace example.Bank
{
    public partial class CancelMembership : Form
    {
        static String PathFile = "";

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
            //try
            //{
            //    Bank.Search IN = new Bank.Search(2);
            //    IN.ShowDialog();
            //    TBTeacherNo.Text = Bank.Search.Return[0];
            //}
            //catch (Exception x)
            //{
            //    Console.WriteLine(x);
            //}
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
                if (PathFile != "")
                { 
                    DocStaTus = 1;
                    
                    var smb = new example.Class.ProtocolSharing.ConnectSMB.SmbFileContainer();
                    if (smb.IsValidConnection())
                    {
                        smb.SendFile(PathFile, TBTeacherName.Text + " Cancel.pdf");
                    }
                    else
                    {
                        MessageBox.Show("ไม่สามารถสร้างไฟล์ในที่นั้นได้", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }


                String DocUpLoadPath = example.Class.ProtocolSharing.ConnectSMB.SmbFileContainer.PathFile + @"\" + TBTeacherName.Text + " Cancel.pdf";
                if (DocUpLoadPath == "") { DocUpLoadPath = "Null"; }
                Class.SQLConnection.InputSQLMSSQL(SQLDefault[0].Replace("{TeacherNoAddBy}", Class.UserInfo.TeacherNo)
                    .Replace("{TeacherNo}", TBTeacherNo.Text)
                    .Replace("{Note}", textBox1.Text)
                    .Replace("{DocStatusNo}", DocStaTus.ToString())
                    .Replace("{DocUploadPath}", DocUpLoadPath.ToString())
                    .Replace("{Status}", 2.ToString()));
                MessageBox.Show("ยกเลิกผู้ใช้เรียบร้อย","System",MessageBoxButtons.OK,MessageBoxIcon.Information);
                TBIDNo.Text = "";
                TBTeacherName.Text = "";
                TBTeacherNo.Text = "";
                textBox1.Text = "";
                PathFile = "";
            }
            else
            {
                MessageBox.Show("กรุณาใส่รหัสอาจารย์ให้ถูกต้อง","เตือน",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "pdf files(*.pdf)|*.pdf";
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    PathFile = dialog.FileName;
                }
                    
            }
            catch
            {
                MessageBox.Show("Error","Error",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }
    }
}
