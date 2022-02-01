using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Text;
using System.Drawing;

namespace BankTeacher.Class
{
    class FromSettingMedtod
    {
        public static void ChangeSizePanal(Form myForm, Panel myPanal)
        {
            myPanal.Location = new System.Drawing.Point(myForm.Width / 2 - myPanal.Size.Width / 2,
            myForm.Height / 2 - myPanal.Size.Height / 2);
        }
        public static void Eb(MenuStrip Ms)
        {
            for(int r = 0; r < Ms.Items.Count; r++)
            {
                Ms.Items[r].Enabled = false;
            }
        }
        public static void SetFont(ComboBox CB,float Fs,Label Lb)
        {
            int Num = 0;
           Font F = new Font("TH Sarabun New",Fs, FontStyle.Regular);
            if(Num == 0)
            {
                CB.Items.Add(F.Name.ToString());
            }
            BankTeacher.Bank.Menu.FontSize = Fs.ToString();
            BankTeacher.Bank.Home.F = F;
        }
       
         public static void ResetAllControls(Control form)
         {
            foreach (Control control in form.Controls)
            {
                    if (control is TextBox)
                    {
                        TextBox textBox = (TextBox)control;
                        textBox.Text = null;
                    }

                    if (control is ComboBox)
                    {
                        ComboBox comboBox = (ComboBox)control;
                        if (comboBox.Items.Count > 0)
                            comboBox.SelectedIndex = 0;
                    }

                    if (control is CheckBox)
                    {
                        CheckBox checkBox = (CheckBox)control;
                        checkBox.Checked = false;
                    }

                    if (control is ListBox)
                    {
                        ListBox listBox = (ListBox)control;
                        listBox.ClearSelected();
                    }
            }    
         }  
        public static void ReturntoHome(Form FORM)
        {
            FORM.Close();
            BankTeacher.Bank.Menu.Parent.MainMenuStrip.Visible = true;
        }

        public static void ProtectedCtrlVTB(TextBox TB)
        {
            if (TB.Text.Length > 0)
            {
                if (Int32.TryParse(TB.Text, out int x) && x <= 0)
                {
                    TB.Text = "";
                }
                else if (!(Int32.TryParse(TB.Text, out int y)))
                {
                    TB.Text = "";
                }
            }
        }
    }
}
