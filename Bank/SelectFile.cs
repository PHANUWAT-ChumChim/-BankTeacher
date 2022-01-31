using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace BankTeacher.Bank
{
    public partial class SelectFile : Form
    {
        string Path = "";
        public SelectFile(String path )
        {
            InitializeComponent();
            Path = path;
            ComboBox[] cb = new ComboBox[] { comboBox1 };
            for (int x = 0; x < BankTeacher.Class.ProtocolSharing.ConnectSMB.file.Count; x++) 
                for(int y = 0; y < cb.Length; y++)
                     cb[y].Items.Add(new BankTeacher.Class.ComboboxSelectFile(BankTeacher.Class.ProtocolSharing.ConnectSMB.file[x].DateAdd.ToString("dd-MM-yyyy hh:mm"),BankTeacher.Class.ProtocolSharing.ConnectSMB.file[x].DateAdd.ToString("dd-MM-yyyy hh:mm:ss.fffffff") , BankTeacher.Class.ProtocolSharing.ConnectSMB.file[x].Name));

            if (comboBox1.Items.Count == 0)
                comboBox1.Enabled = false;
            else
                comboBox1.SelectedIndex = 0;
        }
        //Thread ChangeEnableAnotherForm;
        private void BOpenFile_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
            {
                var ThisPcFilePath = "";
                BankTeacher.Class.ComboboxSelectFile SelectFile = (comboBox1.SelectedItem as BankTeacher.Class.ComboboxSelectFile);
                for (int x = 0; x < BankTeacher.Class.ProtocolSharing.ConnectSMB.file.Count; x++)
                {
                    if (BankTeacher.Class.ProtocolSharing.ConnectSMB.file[x].DateAdd.ToString("dd-MM-yyyy hh:mm:ss.fffffff") == SelectFile.FullDateAdd.ToString())
                    {
                        String FileServerPath = Path + SelectFile.Name;
                        // Create Folder
                        System.IO.Directory.CreateDirectory(System.IO.Path.Combine(@"C:\", "BankTeacher"));
                        System.IO.Directory.CreateDirectory(System.IO.Path.Combine(@"C:\BankTeacher", "RegMember"));
                        System.IO.Directory.CreateDirectory(System.IO.Path.Combine(@"C:\BankTeacher", "Loan"));
                        //Copy Right File
                        if (FileServerPath.Contains("RegMember"))
                        {
                            if (!File.Exists(@"C:\BankTeacher\RegMember\" + SelectFile.Name))
                                System.IO.File.Copy(FileServerPath, @"C:\BankTeacher\RegMember\" + SelectFile.Name);
                            ThisPcFilePath = @"C:\BankTeacher\RegMember\" + SelectFile.Name;
                        }
                        else if (FileServerPath.Contains("Loan"))
                        {
                            if (!File.Exists(@"C:\BankTeacher\Loan\" + SelectFile.Name))
                                System.IO.File.Copy(FileServerPath, @"C:\BankTeacher\Loan\" + SelectFile.Name);
                            ThisPcFilePath = @"C:\BankTeacher\Loan\" + SelectFile.Name;
                        }
                        System.Diagnostics.Process.Start(ThisPcFilePath);
                        break;
                    }
                    else if (x == BankTeacher.Class.ProtocolSharing.ConnectSMB.file.Count - 1)
                        MessageBox.Show("ไม่พบไฟล์ที่ท่านเลือก", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                //else
                //{
                //    BankTeacher.Class.ComboboxSelectFile SelectFile = (comboBox1.SelectedItem as BankTeacher.Class.ComboboxSelectFile);
                //    String file = Path + SelectFile.Name;
                //    string SQLUPDATE,SQLSELCET;
                //    if (info_File.Type == "MemberNo")
                //    {
                //        SQLSELCET = "select a.DocStatusNo, a.DocUploadPath \r\n " +
                //         "from EmployeeBank.dbo.tblMember as a \r\n" +
                //         $"where a.TeacherNo = '{info_File.No}'";
                //        SQLUPDATE = "UPDATE EmployeeBank.dbo.tblMember \r\n " +
                //    "set DocStatusNo = '2',DocUploadPath = '' \r\n " +
                //    "where TeacherNo = '{TeacherNo}'"
                //    .Replace("{TeacherNo}", info_File.No);
                //    }
                //    else
                //    {
                //        SQLSELCET = "SELECT a.DocStatusNo,a.DocUploadPath \r\n " +
                //       "FROM EmployeeBank.dbo.tblLoan as a \r\n " +
                //       "WHERE a.LoanNo = '{LoanNo}'".Replace("{LoanNo}",info_File.No);
                //        SQLUPDATE = "UPDATE EmployeeBank.dbo.tblLoan \r\n" +
                //     "set DocStatusNo = 2, DocUploadPath = '' \r\n" +
                //     "WHERE LoanNo = '{LoanNo}'".Replace("{LoanNo}", info_File.No);
                //    }
                //    try
                //    {
                //        DialogResult con = MessageBox.Show($"คุณต้องการลบชื่อ {SelectFile.Name} ไฟล์นี้ หรือ ไม่", "ไฟล์", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                //        if(con == DialogResult.Yes)
                //        {
                //            DataTable dt_file = Class.SQLConnection.InputSQLMSSQL(SQLSELCET);
                //            if (dt_file.Rows.Count != 0)
                //            {
                //                if (dt_file.Rows[0][1] != "")
                //                {
                //                    System.IO.FileInfo f = new System.IO.FileInfo(dt_file.Rows[0][1].ToString());
                //                    DateTime Date = Convert.ToDateTime(f.CreationTime);
                //                    string a = Date.ToString("dd-MM-yyyy hh:mm");
                //                    string b = comboBox1.SelectedItem.ToString();
                //                    if (comboBox1.SelectedItem.ToString() == Date.ToString("dd-MM-yyyy hh:mm"))
                //                    {
                //                        if (file == dt_file.Rows[0][1].ToString())
                //                        {
                //                            System.IO.File.Delete(file);
                //                            comboBox1.Items.RemoveAt(comboBox1.SelectedIndex);
                //                            MessageBox.Show("ลบไฟล์เสร็จสิ้น", "ไฟล์", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //                            Class.SQLConnection.InputSQLMSSQL(SQLUPDATE);
                //                            OpenEnableButton = false;
                //                        }
                //                    }
                //                    else { MessageBox.Show("ไม่สามรถลบไฟล์ที่ไม่เกี่ยวข้องกับเอกสารปัจุบันได้", "ไฟล์", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                //                }
                //                else
                //                {
                //                    DataTable dt = Class.SQLConnection.InputSQLMSSQL("SELECT a.TeacherNo, a.DocUploadPath " +
                //                 "FROM EmployeeBank.dbo.tblMemberResignation as a " +
                //                 "WHERE a.TeacherNo = '{TeacherNo}'".Replace("{TeacherNo}", info_File.No));
                //                    bool IS = false;
                //                    for (int loop = 0; loop < dt.Rows.Count; loop++)
                //                    {
                //                        if (file == dt.Rows[loop][1].ToString())
                //                        {
                //                            IS = true;
                //                            break;
                //                        }
                //                    }
                //                    if (IS)
                //                    {
                //                        MessageBox.Show("คุณไม่สามรถลบ ไฟล์การสมัครเอกสารเก่าได้ คุณสามารถลบเอกสาร    ที่ทำการสมัคร ณ ปัจุบันเท่านั้น  เว้นเเต่ขอสิทธ์การลบเอกสารได้ที่ ผู้อำนวยการ", "ไฟล์", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //                    }
                //                    else
                //                    {
                //                        System.IO.File.Delete(file);
                //                        MessageBox.Show("ลบไฟล์เสร็จสิ้น", "ไฟล์", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //                        comboBox1.Items.RemoveAt(comboBox1.SelectedIndex);
                //                        OpenEnableButton = false;
                //                    }
                //                }
                //            }
                //            else {
                //                System.IO.File.Delete(file);
                //                MessageBox.Show("ลบไฟล์เสร็จสิ้น", "ไฟล์", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //                comboBox1.Items.RemoveAt(comboBox1.SelectedIndex);
                //                OpenEnableButton = false;
                //            }
                //        }
                //    }
                //    catch (System.IO.IOException ioExp)
                //    {
                //        Console.WriteLine(ioExp.Message);
                //    }
                //}
            }
        }
        private void SelectFile_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape)
                this.Close();
        }
    }
}
