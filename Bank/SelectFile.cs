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
        }

        private void BOpenFile_Click(object sender, EventArgs e)
        {
            if(comboBox1.SelectedIndex != -1)
            {
                BankTeacher.Class.ComboboxSelectFile SelectFile = (comboBox1.SelectedItem as BankTeacher.Class.ComboboxSelectFile);
                for(int x = 0; x < BankTeacher.Class.ProtocolSharing.ConnectSMB.file.Count; x++)
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
        }

        private void SelectFile_KeyDown(object sender, KeyEventArgs e)
        {
            this.Close();
        }
    }
}
