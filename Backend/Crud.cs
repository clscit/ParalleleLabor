using Backend.Models;

namespace Backend
{
    public class Crud
    {
        private readonly string _connectionString;
        private DataAccess db = new();
        public Crud(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void CreateEntry(Entry e)
        {
            string sql = "INSERT INTO entry (Title, Content) VALUES (@Title, @Content);";
            db.SaveData(sql, new { e.Title, e.Content }, _connectionString);
        }

        public void DeleteEntry(int ID)
        {
            string sql = "DELETE FROM entry WHERE ID = @ID;";
            db.SaveData(sql, new { ID }, _connectionString);
        }

        public void UpdateEntry(Entry e)
        {
            string sql = "UPDATE entry SET Title = @Title, Content = @Content WHERE ID = @ID;";
            db.SaveData(sql, new { e.Title, e.Content, e.Id }, _connectionString);
        }

        public List<Entry> GetAllEntries()
        {
            string sql = "SELECT * FROM entry;";
            return db.LoadData<Entry, dynamic>(sql, new { }, _connectionString);
        }

        public Entry GetEntryById(int id)
        {
            string sql = "SELECT * FROM entry WHERE ID = @ID;";
            var parameters = new { ID = id };

            var result = db.LoadData<Entry, dynamic>(sql, parameters, _connectionString);

            return result.Count > 0 ? result[0] : null;
        }
    }
}