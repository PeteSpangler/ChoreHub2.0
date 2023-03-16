using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace ChoreHub2._0.Models
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string FullName { get; set; }
        public int TotalChoresCompleted { get; set; }
        public double ChoreScore { get; set; }
        public int GroupId { get; set; }
    }
}