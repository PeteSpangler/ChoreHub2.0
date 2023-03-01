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

        public void Save()
        {
            string filePath = System.IO.Path.Combine(FileSystem.AppDataDirectory, Filename);

            // Combine the priority value and the text content with a delimiter to save them in a single file
            string content = $"{Priority}\n{Text}";

            File.WriteAllText(filePath, content);
        }


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
                Date = File.GetLastWriteTime(filename),
            };

            // Read the file contents, excluding the first line (priority)
            var fileLines = File.ReadAllLines(filename);
            note.Priority = int.Parse(fileLines[0]);
            note.Text = string.Join(Environment.NewLine, fileLines.Skip(1));

            return note;
        }




        public static IEnumerable<Note> LoadAll() 
        {
            string appDataPath = FileSystem.AppDataDirectory;

            return Directory

                .EnumerateFiles(appDataPath, "*.notes.txt")

                .Select(filename => Note.Load(Path.GetFileName(filename)))

                .OrderByDescending(note => note.Priority);
        }
    }
}
