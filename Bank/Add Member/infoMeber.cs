using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankTeacher.Bank.Add_Member
{
    public partial class infoMeber : Form
    {
        public infoMeber()
        {
            InitializeComponent();
        }
        /// <summary> 
        /// SQLDefault 
        /// <para>[0] Get InfoMember INPUT: {TeacherNo}  </para> 
        /// <para>[1] Search Teacher INPUT: {TeacherNotLike}</para>
        /// <para>[2] Check Bill Teacher Have Ever Paid INPUT: {TeacherNo}</para>
        /// <para>[3] Save Edit Bsave INPUT: {Amount}  {TeacherNo} </para>
        /// </summary> 
        private String[] SQLDefault = new String[]
         { 
           //[0]Get InfoMember INPUT: {TeacherNo}  
           "SELECT CAST(ISNULL(c.PrefixNameFull , '') + b.Fname + ' ' + b.Lname as NVARCHAR) as Name , f.Name as TeacherAddByName , d.MemberStatusName , CAST(a.DateAdd as date)  \r\n " +
          ", a.StartAmount , e.SavingAmount \r\n " +
          "FROM EmployeeBank.dbo.tblMember as a \r\n " +
          "LEFT JOIN Personal.dbo.tblTeacherHis as b on a.TeacherNo = b.TeacherNo \r\n " +
          "LEFT JOIN BaseData.dbo.tblPrefix as c on b.PrefixNo = c.PrefixNo \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblMemberStatus as d on a.MemberStatusNo = d.MemberStatusNo \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblShare as e on a.TeacherNo = e.TeacherNo \r\n " +
          "LEFT JOIN (SELECT a.TeacherAddBy , CAST(ISNULL(c.PrefixNameFull , '') + b.Fname + ' ' + b.Lname as NVARCHAR) as Name \r\n " +
          "	FROM EmployeeBank.dbo.tblMember as a  \r\n " +
          "	LEFT JOIN Personal.dbo.tblTeacherHis as b on a.TeacherAddBy = b.TeacherNo \r\n " +
          "	LEFT JOIN BaseData.dbo.tblPrefix as c on b.PrefixNo = c.PrefixNo \r\n " +
          "	GROUP BY a.TeacherAddBy, CAST(ISNULL(c.PrefixNameFull , '') + b.Fname + ' ' + b.Lname as NVARCHAR)) as f on a.TeacherAddBy = f.TeacherAddBy \r\n " +
          "WHERE a.TeacherNo LIKE '%{TeacherNo}%' and a.MemberStatusNo = 1 "
           ,

           //[1]Search Teacher INPUT: {TeacherNotLike}
           "SELECT a.TeacherNo , CAST(ISNULL(c.PrefixNameFull , '') + b.Fname + ' ' + Lname as NVARCHAR) \r\n " +
          "FROM EmployeeBank.dbo.tblMember as a \r\n " +
          "LEFT JOIN Personal.dbo.tblTeacherHis as b on a.TeacherNo = b.TeacherNo \r\n " +
          "LEFT JOIN BaseData.dbo.tblPrefix as c on b.PrefixNo = c.PrefixNo \r\n " +
          "WHERE a.TeacherNo LIKE '%' and a.MemberStatusNo = 1 {TeacherNotLike};"
           ,
           //[2] Check Bill Teacher Have Ever Paid INPUT: {TeacherNo}
           "SELECT COUNT(a.BillNo)  \r\n " +
          "FROM EmployeeBank.dbo.tblBill as a  \r\n " +
          "LEFT JOIN (SELECT a.BillNo  \r\n " +
          "FROM EmployeeBank.dbo.tblBillDetail as a \r\n " +
          "WHERE a.TypeNo <> 3 \r\n " +
          "GROUP BY a.BillNo) as b on a.BillNo = b.BillNo  \r\n " +
          "WHERE a.Cancel = 1 and a.TeacherNo LIKE '%{TeacherNo}%' \r\n " +
          " \r\n " +
          "SELECT COUNT(c.WithDrawNo) \r\n " +
          "FROM EmployeeBank.dbo.tblMember as a \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblShare as b on a.TeacherNo = b.TeacherNo \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblShareWithdraw as c on b.ShareNo = c.ShareNo \r\n " +
          "WHERE a.TeacherNo LIKE '%{TeacherNo}%'"
           ,

           //[3]Save Edit Bsave INPUT: {Amount}  {TeacherNo}
           "-- BSave Edit \r\n " +
          "UPDATE EmployeeBank.dbo.tblMember  \r\n " +
          "SET StartAmount = {Amount} \r\n " +
          "WHERE TeacherNo = '{TeacherNo}'; \r\n " +
          " \r\n " +
          "UPDATE EmployeeBank.dbo.tblShare \r\n " +
          "SET SavingAmount = {Amount} \r\n " +
          "WHERE TeacherNo = '{TeacherNo}';"
           ,


         };
        private void infoMeber_SizeChanged(object sender, EventArgs e)
        {
            Class.FromSettingMedtod.ChangeSizePanal(this, PL);
        }

        private void BExitForm_Click(object sender, EventArgs e)
        {
            BankTeacher.Class.FromSettingMedtod.ReturntoHome(this);
        }

        private void infoMeber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (TBTeacherNo.Text.Length != 0)
                {
                    TBTeacherName.Text = "";
                    TBTeacherNo.Text = "";
                    TBNameInfo.Text = "";
                    TBTeacherAddByName.Text = "";
                    TBMemberStatus.Text = "";
                    TBDateAdd.Text = "";
                    TBStartAmount.Text = "";
                    TBSavingAmount.Text = "";
                    BSaveEdit.Enabled = false;
                    SavingAmountStart = "";
                    TBStartAmount.Enabled = false;
                }
                else
                {
                    BExitForm_Click(new object(), new EventArgs());
                }
            }
        }

        private void BSearchTeacher_Click(object sender, EventArgs e)
        {
            try
            {
                Bank.Search IN;
                String NotLike = "";
                if (TBTeacherNo.Text.Length == 6)
                {
                    NotLike += " and a.TeacherNo NOT LIKE " + $"'{TBTeacherNo.Text}'";
                }
                IN = new Bank.Search(SQLDefault[1]
                       .Replace("{TeacherNotLike}" , NotLike) , "");

                IN.ShowDialog();
                if (Bank.Search.Return[0] != "" )
                {
                    TBTeacherNo.Text = Bank.Search.Return[0];
                    TBTeacherNo_KeyDown(sender, new KeyEventArgs(Keys.Enter));
                    tabControl1.SelectedIndex = 0;
                }
            }
            catch (Exception x)
            {
                Console.WriteLine(x);
            }
        }

        String SavingAmountStart = "";

        private void TBStartAmount_Leave(object sender, EventArgs e)
        {
            if(SavingAmountStart != TBStartAmount.Text)
            {
                BSaveEdit.Enabled = true;
            }
        }

        private void TBNameInfo_Leave(object sender, EventArgs e)
        {
            if(SavingAmountStart != TBStartAmount.Text)
            {
                BSaveEdit.Enabled = true;
                SavingAmountStart = Convert.ToInt32(SavingAmountStart) - Convert.ToInt32(TBStartAmount.Text) + "";
            }
        }

        private void TBTeacherNo_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter && TBTeacherNo.Text.Length == 6)
            {
                try
                {
                    DataSet dsInfoMember = Class.SQLConnection.InputSQLMSSQLDS(SQLDefault[0]
                        .Replace("{TeacherNo}", TBTeacherNo.Text) +
                        "\r\n" + SQLDefault[2]
                        .Replace("{TeacherNo}" , TBTeacherNo.Text));

                    TBTeacherName.Text = dsInfoMember.Tables[0].Rows[0][0].ToString();
                    TBNameInfo.Text = dsInfoMember.Tables[0].Rows[0][0].ToString();
                    TBTeacherAddByName.Text = dsInfoMember.Tables[0].Rows[0][1].ToString();
                    TBMemberStatus.Text = dsInfoMember.Tables[0].Rows[0][2].ToString();
                    TBDateAdd.Text = dsInfoMember.Tables[0].Rows[0][3].ToString();
                    TBStartAmount.Text = dsInfoMember.Tables[0].Rows[0][4].ToString();
                    TBSavingAmount.Text = dsInfoMember.Tables[0].Rows[0][5].ToString();
                    SavingAmountStart = dsInfoMember.Tables[0].Rows[0][4].ToString();

                    if (Convert.ToInt32(dsInfoMember.Tables[1].Rows[0][0].ToString()) == 0 && Convert.ToInt32(dsInfoMember.Tables[2].Rows[0][0].ToString()) == 0)
                        TBStartAmount.Enabled = true;
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"-----------------------------{ex}-----------------------------------");
                }
            }
            else if(e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
            {
                TBTeacherName.Text = "";
                TBNameInfo.Text = "";
                TBTeacherAddByName.Text = "";
                TBMemberStatus.Text = "";
                TBDateAdd.Text = "";
                TBStartAmount.Text = "";
                TBSavingAmount.Text = "";
                BSaveEdit.Enabled = false;
                TBStartAmount.Enabled = false;
                SavingAmountStart = "";
            }
        }

        private void BSaveEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if(MessageBox.Show("ยืนยันการเปลี่ยนแปลง","แจ้งเตือน",MessageBoxButtons.YesNo,MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    Class.SQLConnection.InputSQLMSSQL(SQLDefault[3]
                .Replace("{Amount}", TBStartAmount.Text)
                .Replace("{TeacherNo}", TBTeacherNo.Text));
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"--------------------------{ex}----------------------------");
                MessageBox.Show("การบันทึกล้มเหลว", "แจ้งเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }
    }
}
