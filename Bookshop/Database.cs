using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Bookshop.Data
{
   public static class Database
    {
        // CONNECTION STRING
        private static readonly string connectionString =
            @"Server=.;Database=BookShop;Trusted_Connection=True;";

        // INSERT / UPDATE / DELETE
        public static int ExecuteNonQuery(
            string query,
            SqlParameter[] parameters = null)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                if (parameters != null)
                    cmd.Parameters.AddRange(parameters);

                con.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        // SELECT SINGLE VALUE
        public static object ExecuteScalar(
            string query,
            SqlParameter[] parameters = null)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                if (parameters != null)
                    cmd.Parameters.AddRange(parameters);

                con.Open();
                return cmd.ExecuteScalar();
            }
        }

        // SELECT TABLE (DataGridView)
        public static DataTable GetDataTable(
            string query,
            SqlParameter[] parameters = null)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                if (parameters != null)
                    cmd.Parameters.AddRange(parameters);

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        // SELECT USING DATAREADER (OPTIONAL)
        public static SqlDataReader ExecuteReader(
            string query,
            SqlParameter[] parameters = null)
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(query, con);

            if (parameters != null)
                cmd.Parameters.AddRange(parameters);

            con.Open();
            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }
    }

}
