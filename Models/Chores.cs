using System.Collections.Generic;
using System.Linq;

namespace ChoreHub2._0.Models
{
    public class Chores
    {
        public int Id { get; set; }
        public int Priority { get; set; }
        public string Description { get; set; }
        public int AssignerId { get; set; }
        public int AssigneeId { get; set; }
        public int Rating { get; set; }
        public bool IsCompleted { get; set; }

        public byte[] Photo { get; set; }
        public void CompleteChore(byte[] updatedPhoto, int rating)
        {
            Photo = updatedPhoto;
            IsCompleted = true;
            Rating = rating;
        }
    }

    public static class MockChores
    {
        private static List<Chores> chores = new List<Chores>
        {
            new Chores
            {
                Id = 1,
                Priority = 1,
                Description = "Clean the kitchen",
                AssignerId = 1,
                AssigneeId = 2,
                Rating = 1,
                IsCompleted = false,
                Photo = null
            },
            new Chores
            {
                Id = 2,
                Priority = 2,
                Description = "Take out the trash",
                AssignerId = 2,
                AssigneeId = 1,
                Rating = 1,
                IsCompleted = false,
                Photo = null
            },
            new Chores
            {
                Id = 3,
                Priority = 3,
                Description = "Mow the lawn",
                AssignerId = 3,
                AssigneeId = 4,
                Rating = 1,
                IsCompleted = false,
                Photo = null
            },
            new Chores
            {
                Id = 4,
                Priority = 1,
                Description = "Do the laundry",
                AssignerId = 4,
                AssigneeId = 3,
                Rating = 1,
                IsCompleted = false,
                Photo = null
            }
        };

        public static List<Chores> GetMockChores()
        {
            return chores;
        }

        public static List<Chores> GetChoresByUserId(int userId)
        {
            return chores.Where(chore => chore.AssigneeId == userId).ToList();
        }

        public static void AddChore(Chores chore)
        {
            chores.Add(chore);
        }

        public static void DeleteChore(int choreId)
        {
            var chore = chores.FirstOrDefault(c => c.Id == choreId);
            if (chore != null)
                chores.Remove(chore);
        }

        public static void UpdateChore(Chores chore)
        {
            var existingChore = chores.FirstOrDefault(c => c.Id == chore.Id);
            if (existingChore != null)
            {
                // Update the existing chore properties
                existingChore.Priority = chore.Priority;
                existingChore.Description = chore.Description;
                existingChore.AssignerId = chore.AssignerId;
                existingChore.AssigneeId = chore.AssigneeId;
                existingChore.Rating = chore.Rating;
                existingChore.IsCompleted = chore.IsCompleted;
                existingChore.Photo = chore.Photo;
            }
        }
    }
}
