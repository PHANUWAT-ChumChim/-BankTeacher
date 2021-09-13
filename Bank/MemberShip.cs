
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static example.Class.ProtocolSharing.ConnectSMB;

namespace example.Bank
{
    public partial class MemberShip : Form
    {
        //------------------------- index -----------------
        public static string Day;
        public static string Month;
        public static string Year;

        //----------------------- index code -------------------- ////////

        public MemberShip()
        {
            InitializeComponent();
        }

        //------------------------- FormSize -----------------
        // Comment!
        private void membership_SizeChanged(object sender, EventArgs e)
        {
            Class.FromSettingMedtod.ChangeSizePanal(this, panel1);
        }
        //----------------------- End code -------------------- ////////

        /// <summary>
        /// SQLDafault
        /// <para>[0] Insert Teacher Data INPUT:{TeacherNo}{TeacherAddBy}, {StartAmount} </para>
        /// <para>[1] SELECT Member  INPUT:{TeacherNo} </para>
        /// <para>[2]  Select Detail Memner INPUT: {TeacherNo} </para>
        /// <para>[3] INSERT Member To Member  Bill BillDetail  INPUT: {TeacherNo} {TeacherNoAddBy} {StartAmount} {Mount} {Year}  </para>
        /// <para>[4] DATE  INPUT: - </para>

        /// </summary>
        private String[] SQLDefault = new String[]
        {
			//[0] Insert Teacher Data INPUT:{TeacherNo},{TeacherAddBy},{StartAmount} 
			"INSERT INTO EmployeeBank.dbo.tblMember(TeacherNo,TeacherAddBy,StartAmount,DateAdd) \r\n"+
            "VALUES('{TeacherNo}','{TeacherAddBy}',{StartAmount}, CURRENT_TIMESTAMP); \r\n\r\n"
            ,
            //[1] SELECT Member  INPUT:{TeacherNo}
          "SELECT a.TeacherNo ,  CAST(b.PrefixName+' '+Fname +' '+ Lname as NVARCHAR), null \r\n " +
          "FROM Personal.dbo.tblTeacherHis as a \r\n " +
          "LEFT JOIN BaseData.dbo.tblPrefix as b ON a.PrefixNo = b.PrefixNo  \r\n " +
          "WHERE NOT a.TeacherNo IN(SELECT TeacherNo FROM EmployeeBank.dbo.tblMember) and a.TeacherNo LIKE 'T{TeacherNo}%' \r\n " +
          "ORDER BY Fname "
          ,

          //[2]  Select Detail Memner INPUT: {TeacherNo} 
          "SELECT a.TeacherNo ,CAST(b.PrefixName+' '+Fname +' '+ Lname as NVARCHAR)AS Name, a.IdNo ,a.cNo,a.cMu,c.TumBonName,d.AmPhurName,e.JangWatLongName,a.TelMobile \r\n " +
          "FROM Personal.dbo.tblTeacherHis as a \r\n " +
          "LEFT JOIN BaseData.dbo.tblPrefix as b ON a.PrefixNo = b.PrefixNo  \r\n " +
          "LEFT JOIN BaseData.dbo.tblTumBon as c on a.cTumBonNo = c.TumBonNo \r\n " +
          "LEFT JOIN BaseData.dbo.tblAmphur as d on a.cAmPhurNo = d.AmphurNo \r\n " +
          "LEFT JOIN BaseData.dbo.tblJangWat as e on a.cJangWatNo = e.JangWatNo \r\n " +
          "WHERE a.TeacherNo = '{TeacherNo}'; "

          ,

           //[3] INSERT Member To Member  Bill BillDetail  INPUT: {TeacherNo} {TeacherNoAddBy} {StartAmount} {Mount} {Year}
          "DECLARE @BillNo INT; \r\n"+
          "INSERT INTO EmployeeBank.dbo.tblMember(TeacherNo, TeacherAddBy, StartAmount, DateAdd) \r\n"+
          "VALUES('{TeacherNo}','{TeacherNoAddBy}',{StartAmount},CURRENT_TIMESTAMP)  \r\n"+
          "INSERT INTO EmployeeBank.dbo.tblShare(TeacherNo, SavingAmount) \r\n"+
          "VALUES('{TeacherNo}',{StartAmount}) \r\n"+
          "INSERT INTO EmployeeBank.dbo.tblBill(TeacherNo, TeacherNoAddBy, DateAdd) \r\n"+
          "VALUES('{TeacherNo}','{TeacherNoAddBy}', CURRENT_TIMESTAMP) \r\n"+
          "SELECT @BillNo = SCOPE_IDENTITY(); \r\n"+
          "INSERT INTO EmployeeBank.dbo.tblBillDetail(BillNo, TypeNo, Amount, Mount, Year,BillDetailPaymentNo) \r\n"+
          "VALUES(@BillNo,1,{StartAmount},{Month},{Year},1)"
          ,
           //[4] DATE  INPUT: -
           "SELECT CAST(CURRENT_TIMESTAMP as DATE);"
        };

        //----------------------- PullSQL -------------------- ////////
        // Comment!
        // Available values| ResearchUserAllTLC / TB /
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //ต้องพิมพ์รหัสอาจารย์ถึง 6 ตัวถึงจะเข้าเงื่อนไข if
            if (TBTeacherNo.Text.Length == 6)
            {
                DataTable dt = Class.SQLConnection.InputSQLMSSQL(SQLDefault[1].Replace("T{TeacherNo}%", TBTeacherNo.Text));
                if(dt.Rows.Count != 0)
                {
                    TBTeacherName.Text = dt.Rows[0][1].ToString();
                    TBIDNo.Text = "รอใส่ครับ";
                }

            }
            else
            {
                TBIDNo.Text = "";
                TBTeacherName.Text = "";
            }

        }
        // Comment!
        // Available values|  BSearchTeacher / TB /
        private void BSearchTeacher_Click(object sender, EventArgs e)
        {
            try
            {
                Search IN = new Search(SQLDefault[1].Replace("{TeacherNo}", ""));
                IN.ShowDialog();
                if (Search.Return[0] != "")
                {
                    TBTeacherNo.Text = Search.Return[0];
                    TBTeacherNo_KeyDown(sender, new KeyEventArgs(Keys.Enter));
                }

            }
            catch (Exception x)
            {
                Console.WriteLine(x);
            }
        }
        // Comment!
        // Available values|  SQLDefault[1] / TB /
        private void BSave_Click_1(object sender, EventArgs e)
        {
            DataTable dt = Class.SQLConnection.InputSQLMSSQL(SQLDefault[1].Replace("{TeacherNo}", TBTeacherNo.Text));
            if (TBTeacherName.Text != "")
            {
                if (Convert.ToInt32(TBStartAmountShare.Text) >= example.GOODS.Menu.startAmountMin && Convert.ToInt32(TBStartAmountShare.Text) <= example.GOODS.Menu.startAmountMax)
                {
                    if (dt.Rows.Count == 0)
                    {

                        DialogResult dialogResult = MessageBox.Show("ยืนยันการสมัคร", "สมัครสมาชิก", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (dialogResult == DialogResult.Yes)
                        {
                            Class.SQLConnection.InputSQLMSSQL(SQLDefault[3].Replace("{TeacherNo}", TBTeacherNo.Text)
                            .Replace("{TeacherNoAddBy}", "Teacher")
                            .Replace("{StartAmount}",TBStartAmountShare.Text)
                            .Replace("{Month}", Month)
                            .Replace("{Year}", Year));
                            MessageBox.Show("สมัครเสร็จสิ้น", "สมัครสมาชิก", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            TBTeacherNo.Clear();
                            TBTeacherName.Clear();
                        }
                        else
                        {
                            MessageBox.Show("ยกเลิกการสมัคร", "สมัครสมาชิก", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                    }
                    else
                    {
                        MessageBox.Show("รายชื่อซ้ำ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("ไม่สามารถสมัครสมาชิกได้เนื่องจาก \r\n ราคาหุ้นเริ่มต้นต่ำหรือสูงเกินไป \r\n โปรดแก้ไข ราคาหุ้นขั้นต่ำ หรือ สูงสุด ที่หน้าตั้งค่า", "System Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("โปรดเลือกสมาชิกในการสมัคร", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        //----------------------- End code -------------------- ////////





        //----------------------- EventKey -------------------- ////////
        // Available values| TB /
        private void TBStartAmountShare_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || e.KeyChar > '9') && (e.KeyChar != '\b'))
            {
                e.Handled = true;
            }
        }

        //----------------------- End code -------------------- ////////


        //----------------------- Outgoing -------------------- ////////
        private void BExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        //----------------------- End code -------------------- ////////


        //----------------------- OpenPrintf -------------------- ////////
        private void BTPrintfShare_Click(object sender, EventArgs e)
        {
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();

            }
        }
        //----------------------- End code -------------------- ////////

        private void membership_Load(object sender, EventArgs e)
        {
            DataTable dt = Class.SQLConnection.InputSQLMSSQL(SQLDefault[4]);
            if(dt.Rows.Count != 0)
            {
                //DateTime a = DateTime.Parse(dt.Rows[0][0].ToString());
                String[] date = dt.Rows[0][0].ToString().Split('-');
                //DTPStartDate.Value = a;
                DTPStartDate.Value = new DateTime(Convert.ToInt32(date[0]), Convert.ToInt32(date[1]), Convert.ToInt32(date[2]));
                Day = date[2].ToString();
                Month = date[1].ToString();
                Year = date[0].ToString();
            }
        }


        // ใช้งานเเน่นอนไม่ต้องลบนะครับ
        private void T()
        {
            //private void button2_Click(object sender, EventArgs e)
            //{
            //    Image File;
            //    //String imgeLocation = "";
            //    try
            //    {
            //        OpenFileDialog dialog = new OpenFileDialog();
            //        dialog.Filter = "pdf files(*.pdf)|*.pdf";
            //        if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //        {
            //            //imgeLocation = dialog.FileName;
            //            File = Image.FromFile(dialog.FileName);
            //            //pictureBox1.Image = File;

            //            //        }
            //            //    }
            //            //    catch (Exception)
            //            //    {
            //            //        MessageBox.Show("An Error Occured","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            //            //    }
            //        }
            //    }
            //    catch
            //    {

            //    }
            //}
        }
        //----------------------- Printf -------------------- ////////
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            example.Class.Print.PrintPreviewDialog.PrintMember(e);
        }

        private void BTOpenfile_Click(object sender, EventArgs e)
        {

        }

        private void TBTeacherNo_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void TBTeacherNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DataTable dt = Class.SQLConnection.InputSQLMSSQL(SQLDefault[1].Replace("T{TeacherNo}", TBTeacherNo.Text));
                TBTeacherName.Text = dt.Rows[0][1].ToString();
                TBIDNo.Text = "รอใส่จ้าาา";
            }
        }

        private void BTdeletefile_Click(object sender, EventArgs e)
        {

        }

        private void TBStartAmountShare_TextChanged(object sender, EventArgs e)
        {
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
            {
                TBStartAmountShare.Text = example.GOODS.Menu.startAmountMin.ToString();
            }
        }
        //----------------------- End code -------------------- ////////




        // ------------------------ not working --------------
        //----------------------- End code -------------------- ////////


    }
}
            
            
       
          
