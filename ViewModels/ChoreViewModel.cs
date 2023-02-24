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

//create a viewmodel for a chore object
namespace ChoreHub2._0.ViewModels
{
    internal class ChoreViewModel : ObservableObject, IQueryAttributable
    {
        private Models.Chore _chore;
        public string Assigner
        {
            get => _chore.Assigner;
            set
            {
                if (_chore.Assigner != value)
                {
                    _chore.Assigner = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Assignee
        {
            get => _chore.Assignee;
            set
            {
                if (_chore.Assignee != value)
                {
                    _chore.Assignee = value;
                    OnPropertyChanged();
                }
            }
        }
        public byte Picture
        {
            get => _chore.Picture;
            set
            {
                if (_chore.Picture != value)
                {
                    _chore.Picture = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Description
        {
            get => _chore.Description;
            set
            {
                if (_chore.Description != value)
                {
                    _chore.Description = value;
                    OnPropertyChanged();
                }
            }
        }

        public int Priority
        {
            get => _chore.Priority;
            set
            {
                if (_chore.Priority != value)
                {
                    _chore.Priority = value;
                    OnPropertyChanged();
                }
            }
        }
        public int Rating
        {
            get => _chore.Rating;
            set
            {
                if (_chore.Rating != value)
                {
                    _chore.Rating = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Identifier => _chore.Filename;

        public ICommand SaveCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }

        public ChoreViewModel()
        {
            _chore = new Models.Chore();
            SetupCommands();
        }
        public ChoreViewModel(Models.Chore chore)
        {
            _chore = chore;
            SetupCommands();
        }
        private void SetupCommands()
        {
            SaveCommand = new AsyncRelayCommand(Save);
            DeleteCommand = new AsyncRelayCommand(Delete);
        }

        private async Task Save()
        {
            _chore.Date= DateTime.Now;
            _chore.Save();
            await Shell.Current.GoToAsync($"..?saved={_chore.Filename}");
        }

        private async Task Delete()
        {
            _chore.Delete();
            await Shell.Current.GoToAsync($"..?deleted={_chore.Filename}");
        }

        void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("load"))
            {
                _chore = new Chore();
                RefreshProperties();
            }
           
        }
        //set reload method
        public void Reload()
        {
            _chore = new Chore();
            RefreshProperties();
        }
        //set refresh properties method
        private void RefreshProperties()
        {
            OnPropertyChanged(nameof(Assigner));
            OnPropertyChanged(nameof(Assignee));
            OnPropertyChanged(nameof(Picture));
            OnPropertyChanged(nameof(Description));
            OnPropertyChanged(nameof(Priority));
            OnPropertyChanged(nameof(Rating));
        }
    }
}