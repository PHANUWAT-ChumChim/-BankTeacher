using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;
using System.Windows.Forms;
using System.Drawing;

namespace BankTeacher.Class

{
    class SQLConnection
    {
        public static String Data_Source = "166.166.1.24";
        public static String Initial_Catalog = "EmployeeBank";
        public static String User_ID = "Rice";
        public static String Password = "0854649141";
        private static readonly Object LockConection = new object();

        static OleDbConnection con = new OleDbConnection("Provider=SQLOLEDB; \r\n"+
            $"Data Source= {Data_Source}; \r\n" +
            $"Initial Catalog={Initial_Catalog}; \r\n" +
            $"User ID={User_ID};Password={Password}; \r\n" +
            "IntegratedSecurity=true; \r\n" +
            "Encrypt=True;TrustServerCertificate=True; \r\n" +
            "CharacterSet=SQL_Latin1_General_CP1_CI_AS;");

        public static DataSet InputSQLMSSQLDS(string SQLCode)
        {
            lock (LockConection)
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                OleDbDataAdapter da = new OleDbDataAdapter(SQLCode, con);
                con.Close();
                DataSet ds = new DataSet();
                da.Fill(ds, "Info");
                return ds;
            }
        }
        public static DataTable InputSQLMSSQL(string SQLCode)
        {
            lock (LockConection)
            {
                DataTable dt = new DataTable();
                if (con.State == ConnectionState.Closed)
                    con.Open();
                System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter(SQLCode, con);
                con.Close();
                da.Fill(dt);
                return dt;
            }
        }
        public static DataTable InputSQLMSSQLV2ForThread(string SQLCode)
        {
            lock (LockConection)
            {
                DataTable dt = new DataTable();
                if (con.State == ConnectionState.Closed)
                    con.Open();
                System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter(SQLCode, con);
                con.Close();
                da.Fill(dt);
                return dt;
            }
        }
    }
}