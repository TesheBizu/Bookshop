using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


namespace Bookshop
{
    public class DbHelper
    {
        // Get data (SELECT)
        public static DataTable GetTable(string query, SqlParameter[] parameters = null)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = DbConnection.GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, con))
                {

                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {

                        da.Fill(dt);
                    }
                }
            
            return dt;
        }


        public static int ExecuteNonQuery(string query, SqlParameter[] parameters = null)
        {
            using (SqlConnection con = DbConnection.GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                
                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);
                con.Open();
                    return cmd.ExecuteNonQuery();
                }
            }


        // For SELECT single value (COUNT(*), MAX, etc.)
        public static object ExecuteScalar(string query, SqlParameter[] parameters = null)
        {

            using (SqlConnection con = DbConnection.GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, con))
            {

                   if (parameters != null)
                    cmd.Parameters.AddRange(parameters);

                con.Open();
                    return cmd.ExecuteScalar();
                }
            }

    }
}