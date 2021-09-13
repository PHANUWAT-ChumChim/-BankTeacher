using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;
using System.Windows.Forms;
using System.Drawing;

namespace example.Class

{
    class SQLConnection
    {
        static OleDbConnection con = new OleDbConnection("Provider=SQLOLEDB; \r\n"+
            "Data Source=166.166.1.24; \r\n" +
            "Initial Catalog=Poon_Item; \r\n" +
            "User ID=Rice;Password=0854649141; \r\n" +
            "IntegratedSecurity=true; \r\n" +
            "Encrypt=True;TrustServerCertificate=True; \r\n" +
            "CharacterSet=SQL_Latin1_General_CP1_CI_AS;");

        public static DataSet InputSQLMSSQLDS(string SQLCode)
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
            OleDbDataAdapter da = new OleDbDataAdapter(SQLCode, con);
            con.Close();
            DataSet ds = new DataSet();
            da.Fill(ds, "Info");
            return ds;
        }
        public static DataTable InputSQLMSSQL(string SQLCode)
        {
            DataTable dt = new DataTable();
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                System.Data.OleDb.OleDbDataAdapter da = new System.Data.OleDb.OleDbDataAdapter(SQLCode, con);
                con.Close();
                da.Fill(dt);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return dt;
        }
    }
}