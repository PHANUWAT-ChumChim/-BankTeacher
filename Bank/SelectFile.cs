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

namespace BankTeacher.Bank
{
    public partial class SelectFile : Form
    {
        string Path = "";
        public SelectFile(String path)
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
            BOpenFile.Text = Bank.Add_Member.infoMeber.OroD;
        }
        public static string TeaNo;

        Thread ChangeEnableAnotherForm;
        private void BOpenFile_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
            {
                if (BOpenFile.Text == "เปิดไฟล์")
                {
                    BankTeacher.Class.ComboboxSelectFile SelectFile = (comboBox1.SelectedItem as BankTeacher.Class.ComboboxSelectFile);
                    for (int x = 0; x < BankTeacher.Class.ProtocolSharing.ConnectSMB.file.Count; x++)
                    {
                        if (BankTeacher.Class.ProtocolSharing.ConnectSMB.file[x].DateAdd.ToString("dd-MM-yyyy hh:mm:ss.fffffff") == SelectFile.FullDateAdd.ToString())
                        {
                            String file = Path + SelectFile.Name;
                            System.Diagnostics.Process.Start(file);
                            break;
                        }
                        else if (x == BankTeacher.Class.ProtocolSharing.ConnectSMB.file.Count - 1)
                            MessageBox.Show("ไม่พบไฟล์ที่ท่านเลือก", "ระบบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    BankTeacher.Class.ComboboxSelectFile SelectFile = (comboBox1.SelectedItem as BankTeacher.Class.ComboboxSelectFile);
                    //try
                    //{
                       DialogResult con = MessageBox.Show($"คุณต้องการลบชื่อ {SelectFile.Name} ไฟล์นี้ หรือ ไม่", "ไฟล์", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if(con == DialogResult.Yes)
                        {
                            String file = Path + SelectFile.Name;
                            System.IO.File.Delete(file);
                            comboBox1.Items.RemoveAt(comboBox1.SelectedIndex);
                            MessageBox.Show("ลบไฟล์เสร็จสิ้น", "ไฟล์", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            

                            ////var forr = (from Form form in Application.OpenForms
                            ////                  where form.Name == "infoMeber"
                            ////                  select form);

                            if (comboBox1.Items.Count <= 0)
                            {
                                DataTable dt = Class.SQLConnection.InputSQLMSSQL("UPDATE EmployeeBank.dbo.tblMember \r\n " +
                                "set DocStatusNo = '2' \r\n " +
                                "where TeacherNo = '{TeacherNo}'"
                                .Replace("{TeacherNo}", TeaNo));
                            }
                            ChangeEnableAnotherForm = new Thread(() => ChangevalueAnotherForm());
                            ChangeEnableAnotherForm.Start();

                        }
                    //}
                    //catch (System.IO.IOException ioExp)
                    //{
                    //    Console.WriteLine(ioExp.Message);
                    //}
                }
            }
        }
        public void ChangevalueAnotherForm()
        {
            try
            {
                for (int Num = 0; Num < Application.OpenForms.Count; Num++)
                {
                    if (Application.OpenForms[Num].Name == "infoMeber")
                    {
                        //if (BOpenFile.InvokeRequired)
                        //{
                        //    BOpenFile.Invoke(new Action(ChangevalueAnotherForm));
                        //    return;
                        //}
                        Bank.Add_Member.infoMeber InfoMember = (Bank.Add_Member.infoMeber)Application.OpenForms[Num];
                        InfoMember.label12.Text = "ยังไม่ได้อัพโหลดไฟล์";
                        InfoMember.label12.ForeColor = Color.Red;
                        InfoMember.TB_deletefile.Enabled = false;
                        InfoMember.TB_selectflie.Enabled = false;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"---------------------{ex}--------------------");
            }
            
        }
        private void SelectFile_KeyDown(object sender, KeyEventArgs e)
        {
            this.Close();
        }
    }
}
