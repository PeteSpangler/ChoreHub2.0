using SQLite;
using ChoreHub2._0.Models;

namespace ChoreHub2._0.Repositories
{
    public class UserRepository
    {
        private SQLiteAsyncConnection _connection;

        public UserRepository(SQLiteAsyncConnection connection)
        {
            _connection = connection;
        }

        public Task<List<User>> GetAllAsync()
        {
            return _connection.Table<User>().ToListAsync();
        }

        public Task<User> GetByIdAsync(int id)
        {
            return _connection.FindAsync<User>(id);
        }

        public Task<int> InsertAsync(User user)
        {
            return _connection.InsertAsync(user);
        }

        public Task<int> UpdateAsync(User user)
        {
            return _connection.UpdateAsync(user);
        }

        public Task<int> DeleteAsync(User user)
        {
            return _connection.DeleteAsync(user);
        }
    }
}
