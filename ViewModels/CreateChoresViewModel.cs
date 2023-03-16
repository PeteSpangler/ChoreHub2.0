using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ChoreHub2._0.Models;
using ChoreHub2._0.Repositories;

namespace ChoreHub2._0.ViewModels
{
    public class CreateChoreViewModel : INotifyPropertyChanged
    {
        private ChoresRepository _choresRepository;
        private UserRepository _userRepository;

        public CreateChoreViewModel()
        {
            SaveCommand = new Command(OnSave);
        }

        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }

        private int _priority;
        public int Priority
        {
            get => _priority;
            set
            {
                _priority = value;
                OnPropertyChanged();
            }
        }

        public List<User> Users { get; set; }

        private User _assigner;
        public User Assigner
        {
            get => _assigner;
            set
            {
                _assigner = value;
                OnPropertyChanged();
            }
        }

        private User _assignee;
        public User Assignee
        {
            get => _assignee;
            set
            {
                _assignee = value;
                OnPropertyChanged();
            }
        }

        public ICommand SaveCommand { get; }

        private async void OnSave()
        {
            var chore = new Chores { Description = Description, Priority = Priority };
            if (Assigner != null)
                chore.AssignerId = Assigner.Id;

            if (Assignee != null)
                chore.AssigneeId = Assignee.Id;

            await _choresRepository.InsertAsync(chore);
            // Navigate back or show success message
        }

        public async Task LoadDataAsync()
        {
            Users = await _userRepository.GetAllAsync();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
