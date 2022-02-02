using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankTeacher.Bank.log
{
    public partial class CancelDividend_Log : Form
    {
        public CancelDividend_Log()
        {
            InitializeComponent();
        }

        /// <summary> 
        /// SQLDefault 
        /// <para>[0] Get Year List INPUT: - INPUT: </para> 
        /// <para>[1] Search TeacherCancelBy Per Person or Per Year INPUT: {Year}  {TeacherNo}</para>
        /// </summary> 
        private String[] SQLDefault = new String[]
         { 
           //[0] Get Year List INPUT: -
           "SELECT TOP 5 YEAR(a.DateCancel) \r\n " +
          "FROM EmployeeBank.dbo.tblDividend as a \r\n " +
          "WHERE a.Cancel = 2 \r\n " +
          "GROUP BY a.DateCancel \r\n " +
          "ORDER BY a.DateCancel DESC;"
           ,

           //[1] Search TeacherCancelBy Per Person or Per Year INPUT: {Year}  {TeacherNo}
           "SELECT a.CancelBy , CAST(ISNULL(c.PrefixNameFull , '') + b.Fname + ' ' + b.Lname as NVARCHAR(255)) , a.DateCancel , a.Interest , a.AverageDividend \r\n " +
          "FROM EmployeeBank.dbo.tblDividend as a \r\n " +
          "LEFT JOIN Personal.dbo.tblTeacherHis as b on a.CancelBy = b.TeacherNo \r\n " +
          "LEFT JOIN BaseData.dbo.tblPrefix as c on b.PrefixNo = c.PrefixNo \r\n " +
          "WHERE a.Cancel = 2 and YEAR(a.DateCancel) = {Year} and a.CancelBy LIKE '%{TeacherNo}%';"
           ,

           //[2] Search TeacherCanCelBy INPUT: -
           "SELECT a.CancelBy , CAST(ISNULL(c.PrefixNameFull , '') + b.Fname + ' ' + b.Lname as NVARCHAR(255)) \r\n " +
          "FROM EmployeeBank.dbo.tblDividend as a \r\n " +
          "LEFT JOIN Personal.dbo.tblTeacherHis as b on a.CancelBy = b.TeacherNo \r\n " +
          "LEFT JOIN BaseData.dbo.tblPrefix as c on b.PrefixNo = c.PrefixNo \r\n " +
          "WHERE a.CancelBy LIKE '%{Text}%' or CAST(ISNULL(c.PrefixNameFull , '') + b.Fname + ' ' + b.Lname as NVARCHAR) LIKE '%{Text}%' \r\n " +
          "GROUP BY a.CancelBy , CAST(ISNULL(c.PrefixNameFull , '') + b.Fname + ' ' + b.Lname as NVARCHAR(255));"
           ,


         };
    }
}
