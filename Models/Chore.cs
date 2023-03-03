using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChoreHub2._0.Models
{
    [SQLite.Table("chores")]
    public class Chore
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int AssignerId { get; set; }

        public int AssigneeId { get; set; }

        [NotNull]
        public string Description { get; set; }

        public byte[] Picture { get; set; }
    }
}
