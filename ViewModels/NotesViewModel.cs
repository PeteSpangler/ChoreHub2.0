using ChoreHub2._0.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ChoreHub2._0.ViewModels
{
    public class NotesViewModel
    {
        private readonly NoteRepository _noteRepository;

        public ObservableCollection<Note> Notes { get; set; }

        public NotesViewModel(NoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
            Notes = new ObservableCollection<Note>();
        }

        public async Task LoadNotesAsync()
        {
            var notes = await _noteRepository.GetAllNotes();
            Notes.Clear();
            foreach (var note in notes)
            {
                Notes.Add(note);
            }
        }

        public async Task AddOrUpdateNoteAsync(Note note)
        {
            await _noteRepository.AddOrUpdateNoteAsync(note);
            if (!Notes.Contains(note))
            {
                Notes.Add(note);
            }
        }

        public async Task DeleteNoteAsync(Note note)
        {
            await _noteRepository.DeleteNoteAsync(note);
            Notes.Remove(note);
        }
    }
}
