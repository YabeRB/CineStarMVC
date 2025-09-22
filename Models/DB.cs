using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace CineStarMVC.Models
{
    public class DB
    {
        private readonly string _connectionString;

        public DB()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["CineStarDB"].ConnectionString;
        }

        public DataTable ExecuteStoredProcedure(string storedProcedure, params SqlParameter[] parameters)
        {
            var dt = new DataTable();
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(storedProcedure, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                if (parameters != null)
                    cmd.Parameters.AddRange(parameters);

                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    dt.Load(reader);
                }
            }
            return dt;
        }
    }
}
