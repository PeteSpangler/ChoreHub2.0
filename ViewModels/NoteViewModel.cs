using ChoreHub2._0.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace ChoreHub2._0.ViewModels
{
    public class NoteViewModel : INotifyPropertyChanged
    {
        private readonly NoteRepository _noteRepository;

        public NoteViewModel(NoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        private List<Note> _notes;
        public List<Note> Notes
        {
            get { return _notes; }
            set
            {
                _notes = value;
                OnPropertyChanged();
            }
        }

        public async Task LoadNotesAsync()
        {
            Notes = await _noteRepository.GetAllNotes();
        }

        public async Task AddOrUpdateNoteAsync(Note note)
        {
            await _noteRepository.AddOrUpdateNoteAsync(note);
            await LoadNotesAsync();
        }

        public async Task DeleteNoteAsync(Note note)
        {
            await _noteRepository.DeleteNoteAsync(note);
            await LoadNotesAsync();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
