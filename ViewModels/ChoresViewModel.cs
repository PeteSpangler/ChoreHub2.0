using System.Collections.Generic;
using System.Linq;
using ChoreHub2._0.Models;

namespace ChoreHub2._0.ViewModels
{
    public class ChoresViewModel
    {
        public int Id { get; set; }
        public int Priority { get; set; }
        public string Description { get; set; }
        public int AssignerId { get; set; }
        public int AssigneeId { get; set; }
        public int Rating { get; set; }
        public bool IsCompleted { get; set; }
        public byte[] Photo { get; set; }
        public List<ChoresViewModel> ChoresList { get; set; }

        public ChoresViewModel()
        {
            ChoresList = GetAllChores();
        }
        public static List<ChoresViewModel> GetAllChores()
        {
            List<ChoresViewModel> choresViewModels = new List<ChoresViewModel>();

            // Get the list of chores from the MockChores
            List<Chores> chores = MockChores.GetMockChores();

            // Map the Chores objects to ChoresViewModel objects
            foreach (Chores chore in chores)
            {
                ChoresViewModel choreViewModel = new ChoresViewModel
                {
                    Id = chore.Id,
                    Priority = chore.Priority,
                    Description = chore.Description,
                    AssignerId = chore.AssignerId,
                    AssigneeId = chore.AssigneeId,
                    Rating = chore.Rating,
                    IsCompleted = chore.IsCompleted,
                    Photo = chore.Photo
                };

                choresViewModels.Add(choreViewModel);
            }

            return choresViewModels;
        }

        public static ChoresViewModel GetChoreById(int choreId)
        {
            Chores chore = MockChores.GetMockChores().FirstOrDefault(c => c.Id == choreId);

            if (chore != null)
            {
                ChoresViewModel choreViewModel = new ChoresViewModel
                {
                    Id = chore.Id,
                    Priority = chore.Priority,
                    Description = chore.Description,
                    AssignerId = chore.AssignerId,
                    AssigneeId = chore.AssigneeId,
                    Rating = chore.Rating,
                    IsCompleted = chore.IsCompleted,
                    Photo = chore.Photo
                };

                return choreViewModel;
            }

            return null;
        }

        public void CreateChore()
        {
            Chores chore = new Chores
            {
                Priority = this.Priority,
                Description = this.Description,
                AssignerId = this.AssignerId,
                AssigneeId = this.AssigneeId,
                Rating = this.Rating,
                IsCompleted = this.IsCompleted,
                Photo = this.Photo
            };

            MockChores.AddChore(chore);
        }

        public void DeleteChore()
        {
            MockChores.DeleteChore(this.Id);
        }

        public void UpdateChore()
        {
            Chores chore = new Chores
            {
                Id = this.Id,
                Priority = this.Priority,
                Description = this.Description,
                AssignerId = this.AssignerId,
                AssigneeId = this.AssigneeId,
                Rating = this.Rating,
                IsCompleted = this.IsCompleted,
                Photo = this.Photo
            };

            MockChores.UpdateChore(chore);
        }
    }
}
