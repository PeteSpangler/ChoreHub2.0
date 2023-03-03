using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace ChoreHub2._0.Models
{
    [Table("notes")]
    public class Note
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public int Priority { get; set; }

        public Note()
        {
            Date = DateTime.Now;
            Text = " ";
            Priority = 0;
        }
    }
}
