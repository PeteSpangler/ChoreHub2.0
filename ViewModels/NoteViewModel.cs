using ChoreHub2._0.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ChoreHub2._0.Repositories;
using System.Windows.Input;

namespace ChoreHub2._0.ViewModels
{
    public class NoteViewModel : INotifyPropertyChanged
    {
        private readonly NoteRepository _noteRepository;

        public NoteViewModel(NoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
            SaveCommand = new Command(async () => await SaveNoteAsync());
            DeleteCommand = new Command(async () => await DeleteNoteAsync());
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

        private string _text;
        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                OnPropertyChanged();
            }
        }

        private int _priority;
        public int Priority
        {
            get { return _priority; }
            set
            {
                _priority = value;
                OnPropertyChanged();
            }
        }

        public ICommand SaveCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public async Task LoadNotesAsync()
        {
            Notes = await _noteRepository.GetAllNotes();
        }

        private async Task SaveNoteAsync()
        {
            Note note = new Note
            {
                Text = Text,
                Priority = Priority
            };
            await _noteRepository.AddOrUpdateNoteAsync(note);
            await LoadNotesAsync();
        }

        private async Task DeleteNoteAsync()
        {
            Note note = new Note
            {
                Text = Text,
                Priority = Priority
            };
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
