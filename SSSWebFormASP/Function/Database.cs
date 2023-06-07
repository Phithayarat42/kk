using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SSSWebFormASP.Function
{
    
    public class Database
    {
        SqlConnection conn = new SqlConnection(@"Data Source=PHITHAYARAT;Initial Catalog=TestASP;Integrated Security=True");

        public DataTable ConnectDatabase_Return(string sql)
        {
            //เปิดการเชื่อมต่อฐานข้อมูล
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            conn.Close();
            return dt;
        }
    }
}