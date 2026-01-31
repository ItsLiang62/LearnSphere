using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using Microsoft.Data.SqlClient;
using System.Data;

namespace LearnSphere.DAL
{
    public class DbHelper
    {
        private static readonly string connStr = ConfigurationManager.ConnectionStrings["LearnSphereDB"].ConnectionString;
     
        public static int ExecuteNonQuery(string sql, Dictionary<string, object> paramToValue)
        {
            SqlParameter[] parameters = GetSqlParameters(paramToValue);

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    if (parameters != null) cmd.Parameters.AddRange(parameters);

                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public static DataTable ExecuteQuery(string sql, Dictionary<string, object> paramToValue) 
        {
            SqlParameter[] parameters = GetSqlParameters(paramToValue);

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    DataTable dt = new DataTable();
                    new SqlDataAdapter(cmd).Fill(dt);
                    return dt;
                } 
            }
        }

        private static SqlParameter[] GetSqlParameters(Dictionary<string, object> paramToValue)
        {
            if (paramToValue is null) return null;

            SqlParameter[] parameters = new SqlParameter[paramToValue.Count];

            int i = 0;
            foreach (var pair in paramToValue) 
            {
                parameters[i] = new SqlParameter(pair.Key, pair.Value ?? DBNull.Value);
                i++;
            }

            return parameters;
        }
    }
}