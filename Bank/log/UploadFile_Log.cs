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
    public partial class UploadFile_Log : Form
    {
        public UploadFile_Log()
        {
            InitializeComponent();
        }

        /// <summary> 
        /// SQLDefault 
        /// <para>[0] Get Info DGV INPUT: {TeacherNoAddBy}  {DateYearMonthDay} INPUT: </para> 
        /// <para>[1] GetDate INPUT: - </para>
        /// <para>[2] BSearch TecherAddBy INPUT: {Text} </para>
        /// </summary> 
        private String[] SQLDefault = new String[]
         { 
           //[0] Get Info DGV INPUT: {TeacherNoAddBy}  {DateYearMonthDay}
           "SELECT CAST(ISNULL(c.PrefixNameFull , '') + b.Fname + ' ' + b.Lname as NVARCHAR(255)) , d.Name , CAST(e.FileTypeName as NVARCHAR), ISNULL(CAST(a.LoanID as varchar) , '-') \r\n " +
          ", a.pathFile , a.DateAddFile \r\n " +
          "FROM EmployeeBank.dbo.tblFile as a \r\n " +
          "LEFT JOIN Personal.dbo.tblTeacherHis as b on a.TeacherAddBy = b.TeacherNo \r\n " +
          "LEFT JOIN BaseData.dbo.tblPrefix as c on b.PrefixNo = c.PrefixNo \r\n " +
          "LEFT JOIN (SELECT a.ID ,a.TeacherNo , CAST(ISNULL(c.PrefixNameFull , '') + b.Fname + ' ' + b.Lname as NVARCHAR(255)) as Name \r\n " +
          "FROM EmployeeBank.dbo.tblFile as a \r\n " +
          "LEFT JOIN Personal.dbo.tblTeacherHis as b on a.TeacherNo = b.TeacherNo \r\n " +
          "LEFT JOIN BaseData.dbo.tblPrefix as c on b.PrefixNo = c.PrefixNo) as d on a.ID = d.ID \r\n " +
          "LEFT JOIN EmployeeBank.dbo.tblFileType as e on a.FiletypeNo = e.FileTypeID \r\n " +
          "WHERE a.TeacherAddBy LIKE '%{TeacherNoAddBy}%' and a.DateAddFile LIKE CAST('{DateYearMonthDay}' as date)"
           ,

           //[1] GetDate INPUT: -
           "SELECT CAST(GETDATE() as date);"
           ,

           //[2] BSearch TecherAddBy INPUT: {Text}
           "SELECT a.TeacherAddBy ,CAST(ISNULL(c.PrefixNameFull , '') + b.Fname + ' ' + b.Lname as NVARCHAR(255)) \r\n " +
          "FROM EmployeeBank.dbo.tblFile as a \r\n " +
          "LEFT JOIN Personal.dbo.tblTeacherHis as b on a.TeacherAddBy = b.TeacherNo \r\n " +
          "LEFT JOIN BaseData.dbo.tblPrefix as c on b.PrefixNo = c.PrefixNo \r\n " +
          "WHERE a.TeacherAddBy LIKE '%{Text}%' or CAST(ISNULL(c.PrefixNameFull , '') + b.Fname + ' ' + b.Lname as NVARCHAR(255)) LIKE '%{Text}%' \r\n " +
          "GROUP BY a.TeacherAddBy ,CAST(ISNULL(c.PrefixNameFull , '') + b.Fname + ' ' + b.Lname as NVARCHAR(255));"
           ,

         };
    }
}
