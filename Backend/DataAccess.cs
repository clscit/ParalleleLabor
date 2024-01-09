using Dapper;
using Npgsql;

namespace Backend
{
    public class DataAccess
    {
        public List<T> LoadData<T, U>(string sqlStatement, U parameters, string connectionString)
        {
            using (NpgsqlConnection conn = new(connectionString))
            {
                List<T> rows = conn.Query<T>(sqlStatement, parameters).ToList();
                return rows;
            }
        }

        public void SaveData<T>(string sqlStatement, T parameters, string connectionString)
        {
            using (NpgsqlConnection conn = new(connectionString))
            {
                conn.Execute(sqlStatement, parameters);
            }
        }
    }
}
