using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp
{
    public class DB
    {
        //Execute connection to database and pulling data
        public static string ConnString {get; set;}

        public static List<T> PullData<T>(string sql, Func<SqlDataReader, T> processRecord, Action<SqlCommand> setParameter=null)
        {
            List<T> list = new List<T>();
            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    if (setParameter != null)
                        setParameter(cmd);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                            list.Add(processRecord(dr));
                    }
                }    
            }

            return list;
        }


        //Execute connection to databse and insert data, delete data or update data.
        public static int Modify(string sql, Action<SqlCommand> setParameters)
        {
            int rowsAffected = 0;
            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    if (setParameters != null)
                    {
                        setParameters(cmd);
                    }
                    rowsAffected = cmd.ExecuteNonQuery();
                }
            }
            return rowsAffected;
        }

    }
}
