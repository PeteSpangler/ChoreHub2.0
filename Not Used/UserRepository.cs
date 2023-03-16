using ChoreHub2._0.Models;
using SQLite;

namespace ChoreHub2._0
{
    public class UserRepository
    {
        string _dbPath;

        public string StatusMessage { get; set; }

        private SQLiteAsyncConnection conn;

        private async Task Init()
        {
            if (conn != null)
                return;

            conn = new SQLiteAsyncConnection(_dbPath);
            await conn.CreateTableAsync<User>();
        }

        public UserRepository(string dbPath)
        { _dbPath = dbPath; }

        public async Task AddNewUser(string name)
        {
            int result = 0;

            try
            {
                await Init();

                if (string.IsNullOrEmpty(name))
                    throw new Exception("Valid name required");

                result = await conn.InsertAsync(new User { FullName = name });

                StatusMessage = string.Format("{0} record(s) added (Name: {1})", result, name);
            }

            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to add {0}. Error: {1}", name, ex.Message);
            }
        }

        public async Task<List<User>> GetAllUsers()
        {

            try
            {
                await Init();
                return await conn.Table<User>().ToListAsync();
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }

            return new List<User>();
        }

        internal Task<List<User>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
