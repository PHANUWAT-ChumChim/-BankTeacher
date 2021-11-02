using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankTeacher.Class
{
    class UserInfo
    {
        public static String TeacherNo;
        public static String TeacherName;
        public static String TeacherNoMYSQL;

        public static void SetTeacherInformation(String teacherNo, String teacherName, String teacherNoMYSQL)
        {
            TeacherNo = teacherNo;
            TeacherName = teacherName;
            TeacherNoMYSQL = teacherNoMYSQL;
        }

        public static String GetTeacherNo()
        {
            return TeacherNo;
        }
        public static String GetTeacherName()
        {
            return TeacherName;
        }
        public static String GetTeacherNoMYSQL()
        {
            return TeacherNoMYSQL;
        }
    }
}
