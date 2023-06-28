using Microsoft.Maui.Controls;
using Microsoft.Maui.Media;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel;

namespace ChoreHub2._0.ViewModels
{
    internal class NoteViewModel : ObservableObject, IQueryAttributable
    {
        private Models.Note _note;

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

        public string Identifier => _note.Filename;

        public List<string> AssignedToList { get; set; } // List of available options for "Assigned To" dropdown
        private string _assignedTo;
        public string AssignedTo
        {
            get => _assignedTo;
            set
            {
                if (_assignedTo != value)
                {
                    _assignedTo = value;
                    OnPropertyChanged();
                }
            }
        }

        public byte[] Photo
        {
            get => _note.Photo;
            set
            {
                if (_note.Photo != value)
                {
                    _note.Photo = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand SaveCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand TakePhotoCommand { get; private set; } // Command for taking a photo

        public NoteViewModel()
        {
            _note = new Models.Note();
            SetupCommands();
            LoadAssignedToList();
        }

        public NoteViewModel(Models.Note note)
        {
            _note = note;
            SetupCommands();
            LoadAssignedToList();
        }

        private void SetupCommands()
        {
            SaveCommand = new AsyncRelayCommand(Save);
            DeleteCommand = new AsyncRelayCommand(Delete);
            TakePhotoCommand = new AsyncRelayCommand(TakePhoto);
        }

        private async Task Save()
        {
            _note.Date = DateTime.Now;
            _note.Save();
            await Shell.Current.GoToAsync($"..?saved={_note.Filename}");
        }

        private async Task Delete()
        {
            _note.Delete();
            await Shell.Current.GoToAsync($"..?deleted={_note.Filename}");
        }

        private async Task TakePhoto()
        {
            var mediaPicker = DependencyService.Get<IMediaPicker>();
            if (mediaPicker == null || !mediaPicker.IsCaptureSupported)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Device does not support taking photos.", "OK");
                return;
            }

            var fileResult = await mediaPicker.CapturePhotoAsync();

            if (fileResult != null && !string.IsNullOrEmpty(fileResult.FullPath))
            {
                using (var photoStream = await fileResult.OpenReadAsync())
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await photoStream.CopyToAsync(memoryStream);
                        Photo = memoryStream.ToArray();
                    }
                }
            }
        }

        void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("load"))
            {
                _note = Models.Note.Load(query["load"].ToString());
                RefreshProperties();
            }
        }

        public void Reload()
        {
            _note = Models.Note.Load(_note.Filename);
            RefreshProperties();
        }

        private void RefreshProperties()
        {
            OnPropertyChanged(nameof(Text));
            OnPropertyChanged(nameof(Date));
            OnPropertyChanged(nameof(Priority));
        }

        private void LoadAssignedToList()
        {
            // Load the list of available options for the "Assigned To" dropdown
            AssignedToList = new List<string>()
            {
                "Option 1",
                "Option 2",
                "Option 3"
            };
        }
    }
}
