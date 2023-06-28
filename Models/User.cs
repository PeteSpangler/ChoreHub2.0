using System.Collections.Generic;
using System.Linq;

namespace ChoreHub2._0.Models
{
    public class User
    {
        private int totalChoresCompleted;
        private int totalChoresRating;
        private double choreScore;

        public int Id { get; set; }
        public string FullName { get; set; }

        public int TotalChoresCompleted
        {
            get => totalChoresCompleted;
            set
            {
                totalChoresCompleted = value;
                RecalculateChoreScore();
            }
        }

        public int TotalChoresRating
        {
            get => totalChoresRating;
            set
            {
                totalChoresRating = value;
                RecalculateChoreScore();
            }
        }

        public double ChoreScore
        {
            get => choreScore;
            set => choreScore = value;
        }

        public int GroupId { get; set; }

        public void RecalculateChoreScore()
        {
            if (totalChoresRating != 0)
                choreScore = (double)totalChoresCompleted / totalChoresRating;
            else
                choreScore = 0.0;
        }
    }


    public static class MockUsers
    {
        private static List<User> users = new List<User>
        {
            new User
            {
                Id = 1,
                FullName = "John Doe",
                TotalChoresCompleted = 5,
                TotalChoresRating = 37,
                ChoreScore = 8.3,
                GroupId = 1
            },
            new User
            {
                Id = 2,
                FullName = "Jane Smith",
                TotalChoresCompleted = 3,
                TotalChoresRating = 21,
                ChoreScore = 7.9,
                GroupId = 1
            },
            new User
            {
                Id = 3,
                FullName = "Bob Johnson",
                TotalChoresCompleted = 2,
                TotalChoresRating = 11,
                ChoreScore = 6.5,
                GroupId = 2
            },
            new User
            {
                Id = 4,
                FullName = "Shawon Dunston",
                TotalChoresCompleted = 72,
                TotalChoresRating = 324,
                ChoreScore = 7.6,
                GroupId = 2
            }
        };

        public static List<User> GetMockUsers()
        {
            return users;
        }

        public static User CreateUser(User user)
        {
            int nextId = users.Count > 0 ? users.Max(u => u.Id) + 1 : 1;
            user.Id = nextId;
            users.Add(user);
            return user;
        }

        public static void DeleteUser(int userId)
        {
            User user = users.FirstOrDefault(u => u.Id == userId);
            if (user != null)
                users.Remove(user);
        }

        public static void EditUser(User updatedUser)
        {
            User user = users.FirstOrDefault(u => u.Id == updatedUser.Id);
            if (user != null)
            {
                user.FullName = updatedUser.FullName;
                user.TotalChoresCompleted = updatedUser.TotalChoresCompleted;
                user.ChoreScore = updatedUser.ChoreScore;
                user.GroupId = updatedUser.GroupId;
            }
        }
    }
}
