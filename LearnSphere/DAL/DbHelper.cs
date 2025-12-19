using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Text.RegularExpressions;

namespace LearnSphere.DAL
{
    public class DbHelper
    {
        private static string connStr = ConfigurationManager.ConnectionStrings["LearnSphereDB"].ConnectionString;
        public const string ValuePlaceholder = "[]";

        public static int ExecuteNonQuery(string sql, object[] values)
        {
            sql = GetParameterizedSql(sql);
            SqlParameter[] parameters = GetSqlParameters(sql, values);

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

        public static DataTable ExecuteQuery(string sql, object[] values) 
        {
            sql = GetParameterizedSql(sql);
            SqlParameter[] parameters = GetSqlParameters(sql, values);

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    if (parameters != null) cmd.Parameters.AddRange(parameters);

                    DataTable dt = new DataTable();
                    new SqlDataAdapter(cmd).Fill(dt);
                    return dt;
                } 
            }
        }

        private static string GetParameterizedSql(string inputSql)
        {
            int placeholderCount = 0;
            int nextPlaceholderIndex = inputSql.IndexOf(ValuePlaceholder);
            string parameterizedSql = inputSql;

            while (nextPlaceholderIndex != -1)
            {
                placeholderCount++;
                parameterizedSql = parameterizedSql.Remove(nextPlaceholderIndex, ValuePlaceholder.Length).Insert(nextPlaceholderIndex, "@p" + placeholderCount);
                nextPlaceholderIndex = parameterizedSql.IndexOf(ValuePlaceholder, nextPlaceholderIndex + 1);
            }

            return parameterizedSql;
        }

        private static SqlParameter[] GetSqlParameters(string parameterizedSql, object[] values)
        {
            if (values is null) return null;

            SqlParameter[] parameters = new SqlParameter[values.Length];

            var matches = Regex.Matches(parameterizedSql, @"@\w+");

            if (matches.Count != values.Length) throw new ArgumentException("Number of values does not match number of parameters");

            for (int i=0; i<values.Length; i++)
            {
                string match = matches[i].Value;
                parameters[i] = new SqlParameter(match, values[i] ?? DBNull.Value);
            }

            return parameters;
        }
    }
}