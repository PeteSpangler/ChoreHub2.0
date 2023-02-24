using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using ChoreHub2._0.Models;

namespace ChoreHub2._0.ViewModels
{
    internal class AllChoresViewModel : IQueryAttributable
    {
        public ObservableCollection<ViewModels.ChoreViewModel> AllChores { get; private set; }
        public ICommand NewChoreCommand { get; }
        public ICommand SelectChoreCommand { get; }

        public AllChoresViewModel()
        {
            AllChores = new ObservableCollection<ViewModels.ChoreViewModel>(Models.Chore.LoadAll().Select(chore => new ViewModels.ChoreViewModel(chore)));
            NewChoreCommand = new AsyncRelayCommand(NewChoreAsync);
            SelectChoreCommand = new AsyncRelayCommand<ViewModels.ChoreViewModel>(SelectChoreAsync);
        }
        private async Task NewChoreAsync()
        {
            await Shell.Current.GoToAsync(nameof(Views.ChorePage));
        }
        private async Task SelectChoreAsync(ChoreViewModel chore)
        {
            if(chore != null)
            await Shell.Current.GoToAsync($"{nameof(Views.ChorePage)}?load={chore.Identifier}");
        }
        void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("deleted"))
            {
                string choreId = query["deleted"].ToString();
                ChoreViewModel matchedChore = AllChores.Where((n) => n.Identifier == choreId).FirstOrDefault();

                if (matchedChore != null)
                    AllChores.Remove(matchedChore);
            }
            else if (query.ContainsKey("saved"))
            {
                string choreId = query["saved"].ToString();
                ChoreViewModel matchedChore = AllChores.Where((n) => n.Identifier == choreId).FirstOrDefault();

                if (matchedChore != null)
                {
                    matchedChore.Reload();
                    AllChores.Move(AllChores.IndexOf(matchedChore), 0);
                }
                else
                {
                    AllChores.Insert(0, new ChoreViewModel(Chore.Load(choreId)));
                }
            }
            
        }
    }    
}