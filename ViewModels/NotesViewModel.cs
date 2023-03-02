using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ChoreHub2._0.Models;
using ChoreHub2._0.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SQLite;

namespace ChoreHub2._0.ViewModels
{
    public class NotesViewModel : ObservableRecipient, IQueryAttributable
    {
        private readonly NoteRepository _noteRepository;

        public ObservableCollection<NoteViewModel> AllNotes { get; private set; }

        public ICommand NewCommand { get; }
        public ICommand SelectNoteCommand { get; }

        public NotesViewModel(NoteRepository noteRepository)
        {
            _noteRepository = noteRepository;

            NewCommand = new AsyncRelayCommand(NewNoteAsync);
            SelectNoteCommand = new AsyncRelayCommand(SelectNoteAsync);

            // Load all notes from the repository and order them by priority
            AllNotes = new ObservableCollection<NoteViewModel>(
                _noteRepository.GetAllNotes().OrderByDescending(n => n.Priority).Select(n => new NoteViewModel(n)));
        }

        private async Task NewNoteAsync()
        {
            await Task.CompletedTask;
            // Code to handle creating a new note goes here
        }

        private async Task SelectNoteAsync()
        {
            await Task.CompletedTask;
            // Code to handle selecting a note goes here
        }

        void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("deleted"))
            {
                string noteId = query["deleted"].ToString();
                NoteViewModel matchedNote = AllNotes.FirstOrDefault((n) => n.Note.Id == noteId)?.Note;

                // If note exists, delete it
                if (matchedNote != null)
                    AllNotes.Remove(matchedNote);
            }
            else if (query.ContainsKey("saved"))
            {
                string noteId = query["saved"].ToString();
                NoteViewModel matchedNote = AllNotes.FirstOrDefault((n) => n.Note.Id == noteId)?.Note;

                // If note is found, update it
                if (matchedNote != null)
                {
                    matchedNote.Reload();
                    AllNotes.Move(AllNotes.IndexOf(matchedNote), 0);
                    AllNotes = new ObservableCollection<NoteViewModel>(AllNotes.OrderByDescending(n => n.Priority));
                }
                // If note isn't found, it's new; add it.
                else
                {
                    var note = _noteRepository.GetNoteById(noteId);
                    AllNotes.Insert(0, new NoteViewModel(note));
                    AllNotes = new ObservableCollection<NoteViewModel>(AllNotes.OrderByDescending(n => n.Priority));
                }
            }
        }

    }
}
