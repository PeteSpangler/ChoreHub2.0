using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using ChoreHub2._0.Models;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;

namespace ChoreHub2._0.ViewModels
{
    internal class NotesViewModel : INotifyPropertyChanged, IQueryAttributable
    {
        private ObservableCollection<NoteViewModel> _allNotes;

        public ObservableCollection<NoteViewModel> AllNotes
        {
            get => _allNotes;
            private set
            {
                if (_allNotes != value)
                {
                    _allNotes = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand NewCommand { get; }
        public ICommand SelectNoteCommand { get; }

        public NotesViewModel()
        {
            LoadAllNotes();
            NewCommand = new AsyncRelayCommand(NewNoteAsync);
            SelectNoteCommand = new AsyncRelayCommand<NoteViewModel>(SelectNoteAsync);
        }

        private void LoadAllNotes()
        {
            var notes = Note.LoadAll()
                .OrderByDescending(n => n.Priority)
                .Select(n => new NoteViewModel(n));

            AllNotes = new ObservableCollection<NoteViewModel>(notes);
        }

        private async Task NewNoteAsync()
        {
            await Shell.Current.GoToAsync(nameof(Views.NotePage));
        }

        private async Task SelectNoteAsync(NoteViewModel note)
        {
            if (note != null)
                await Shell.Current.GoToAsync($"{nameof(Views.NotePage)}?load={note.Identifier}");
        }

        void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("deleted"))
            {
                string noteId = query["deleted"].ToString();
                NoteViewModel matchedNote = AllNotes.FirstOrDefault(n => n.Identifier == noteId);

                // If note exists, delete it
                if (matchedNote != null)
                    AllNotes.Remove(matchedNote);
            }
            else if (query.ContainsKey("saved"))
            {
                string noteId = query["saved"].ToString();
                NoteViewModel matchedNote = AllNotes.FirstOrDefault(n => n.Identifier == noteId);

                // If note is found, update it
                if (matchedNote != null)
                {
                    matchedNote.Reload();
                    AllNotes.Remove(matchedNote);
                }

                var indexOfNewElement = 0;
                // find index for new element
                var newNote = Note.Load(noteId);
                for (int i = 0; i < AllNotes.Count; i++)
                {
                    if (AllNotes[i].Priority < newNote.Priority)
                    {
                        indexOfNewElement = i;
                        break;
                    }
                }
                AllNotes.Insert(indexOfNewElement, new NoteViewModel(newNote));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
