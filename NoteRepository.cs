using ChoreHub2._0.Models;
using SQLite;

namespace ChoreHub2._0
{
    public class NoteRepository
    {
        private readonly SQLiteAsyncConnection _database;

        public NoteRepository(SQLiteAsyncConnection database)
        {
            _database = database;
            _database.CreateTableAsync<Note>().Wait();
        }

        public async Task<List<Note>> GetAllNotes()
        {
            return await _database.Table<Note>().ToListAsync();
        }

        public async Task<Note> GetNoteById(int id)
        {
            return await _database.Table<Note>().Where(n => n.Id == id).FirstOrDefaultAsync();
        }

        public async Task AddOrUpdateNoteAsync(Note note)
        {
            if (note.Id == 0)
            {
                await _database.InsertAsync(note);
            }
            else
            {
                await _database.UpdateAsync(note);
            }
        }

        public async Task DeleteNoteAsync(Note note)
        {
            await _database.DeleteAsync(note);
        }
    }
}
