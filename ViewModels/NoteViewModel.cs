using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;
using ChoreHub2._0.Models;

namespace ChoreHub2._0.ViewModels
{
    public class NoteViewModel : ObservableObject, IQueryAttributable
    {
        private Note _note;
        private readonly NoteRepository _noteRepository;

        public Note Note
        {
            get => _note;
            set => SetProperty(ref _note, value);
        }

        public string Text
        {
            get => _note.Text;
            set
            {
                if (_note.Text != value)
                {
                    _note.Text = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime Date => _note.Date;

        public int Priority
        {
            get => _note.Priority;
            set
            {
                if (_note.Priority != value)
                {
                    _note.Priority = value;
                    OnPropertyChanged();
                }
            }
        }

        public int Id => _note.Id;

        public ICommand SaveCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }

        public NoteViewModel(NoteRepository noteRepository)
        {
            _note = new Note();
            _noteRepository = noteRepository;
            SetupCommands();
        }

        public NoteViewModel(Note note, NoteRepository noteRepository)
        {
            _note = note;
            _noteRepository = noteRepository;
            SetupCommands();
        }

        private void SetupCommands()
        {
            SaveCommand = new AsyncRelayCommand(Save);
            DeleteCommand = new AsyncRelayCommand(Delete);
        }

        private async Task Save()
        {
            _note.Date = DateTime.Now;
            await _noteRepository.AddOrUpdateNoteAsync(_note);
            await Shell.Current.GoToAsync($"..?saved={_note.Id}");
        }

        private async Task Delete()
        {
            await _noteRepository.DeleteNoteAsync(_note);
            await Shell.Current.GoToAsync($"..?deleted={_note.Id}");
        }

        void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("load"))
            {
                int noteId = int.Parse(query["load"].ToString());
                _note = _noteRepository.GetNoteById(noteId).Result;
                RefreshProperties();
            }
        }

        public async Task Refresh()
        {
            _note = await _noteRepository.GetNoteById(_note.Id);
            RefreshProperties();
        }

        private void RefreshProperties()
        {
            OnPropertyChanged(nameof(Text));
            OnPropertyChanged(nameof(Date));
            OnPropertyChanged(nameof(Priority));
        }
    }
}
