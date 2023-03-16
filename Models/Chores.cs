using SQLite;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChoreHub2._0.Models
{
    public class Chores
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int Priority { get; set; }
        public string Description { get; set; }

        public int AssignerId { get; set; }

        public int AssigneeId { get; set; }
    }
}
