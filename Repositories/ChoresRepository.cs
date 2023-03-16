using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChoreHub2._0.Models;

namespace ChoreHub2._0.Repositories
{
    public class ChoresRepository
    {
        private SQLiteAsyncConnection _connection;

        public ChoresRepository(SQLiteAsyncConnection connection)
        {
            _connection = connection;
        }

        public Task<List<Chores>> GetAllAsync()
        {
            return _connection.Table<Chores>().ToListAsync();
        }

        public Task<Chores> GetByIdAsync(int id)
        {
            return _connection.FindAsync<Chores>(id);
        }

        public Task<int> InsertAsync(Chores chore)
        {
            return _connection.InsertAsync(chore);
        }

        public Task<int> UpdateAsync(Chores chore)
        {
            return _connection.UpdateAsync(chore);
        }

        public Task<int> DeleteAsync(Chores chore)
        {
            return _connection.DeleteAsync(chore);
        }
    }
}
