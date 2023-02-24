using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace ChoreHub2._0.Models
{
    internal class Chore
    {
        public string Filename { get; set; }

        public string Assigner { get; set; }
        public string Assignee { get; set; }
        public byte Picture { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public int Rating { get; set; }
        public DateTime Date { get; set; }

        public Chore()
        {
            
            Filename = $"{Path.GetRandomFileName()}.chore.json";
            Assigner = "";
            Assignee = "";
            Picture = 0;
            Description = "";
            Priority = 0;
            Rating = 0;
            Date = DateTime.Now;
        }
        public void Save()
        {
            string json = JsonSerializer.Serialize(this);
            File.WriteAllText("chore.json", json);
        }
        public void Delete()
        {
            File.Delete("chore.json");
        }
        public static Chore Load(string filename)
        {
            filename = System.IO.Path.Combine(FileSystem.AppDataDirectory, filename);

            if (!File.Exists(filename))
                throw new FileNotFoundException("Unable to find file on local storage", filename);

            string json = File.ReadAllText(filename);

            return JsonSerializer.Deserialize<Chore>(json);
        }

        public static IEnumerable<Chore> LoadAll()
        {
            string appDataPath = FileSystem.AppDataDirectory;

            return Directory

                .EnumerateFiles(appDataPath, "*.chore.json")

                .Select(filename => Chore.Load(Path.GetFileName(filename)))

                .OrderByDescending(chore => chore.Priority);
        }
    }
}
