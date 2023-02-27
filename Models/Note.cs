using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChoreHub2._0.Models
{
    internal class Note
    {
        public string Filename { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public int Priority { get; set; }

        public Note() 
        {
            Filename = $"{Path.GetRandomFileName()}.notes.txt";
            Date = DateTime.Now;
            Text = "";
            Priority = 0;
        }

        public void Save() =>
            File.WriteAllText(System.IO.Path.Combine(FileSystem.AppDataDirectory, Filename), Text);

        public void Delete() =>
            File.Delete(System.IO.Path.Combine(FileSystem.AppDataDirectory, Filename));

        public static Note Load(string filename)
        {
            filename = System.IO.Path.Combine(FileSystem.AppDataDirectory, filename);

            if (!File.Exists(filename))
                throw new FileNotFoundException("Unable to find file on local storage", filename);

            var note = new Note()
            {
                Filename = Path.GetFileName(filename),
                Text = File.ReadAllText(filename),
                Date = File.GetLastWriteTime(filename),
            };

            if (int.TryParse(File.ReadAllLines(filename).FirstOrDefault(), out int priority))
            {
                note.Priority = priority;
            }

            return note;
        }


        public static IEnumerable<Note> LoadAll() 
        {
            string appDataPath = FileSystem.AppDataDirectory;

            return Directory

                .EnumerateFiles(appDataPath, "*.notes.txt")

                .Select(filename => Note.Load(Path.GetFileName(filename)))

                .OrderByDescending(note => note.Date);
        }
    }
}
