using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ChoreHub2._0.Models
{
    internal class Note
    {
        public string Filename { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public int Priority { get; set; }
        public string AssignedTo { get; set; }
        public byte[] Photo { get; set; }

        public Note()
        {
            Filename = $"{Path.GetRandomFileName()}.notes.txt";
            Date = DateTime.Now;
            Text = "";
            Priority = 0;
            AssignedTo = "";
            Photo = null;
        }

        public void Save()
        {
            string filePath = Path.Combine(FileSystem.AppDataDirectory, Filename);

            // Combine the priority value, assigned to, and the text content with a delimiter to save them in a single file
            string content = $"{Priority}\n{AssignedTo}\n{Text}";

            File.WriteAllText(filePath, content);
        }

        public void Delete() =>
            File.Delete(Path.Combine(FileSystem.AppDataDirectory, Filename));

        public static Note Load(string filename)
        {
            filename = Path.Combine(FileSystem.AppDataDirectory, filename);

            if (!File.Exists(filename))
                throw new FileNotFoundException("Unable to find file on local storage", filename);

            var note = new Note()
            {
                Filename = Path.GetFileName(filename),
                Date = File.GetLastWriteTime(filename),
            };

            // Read the file contents, excluding the first line (priority) and the second line (assigned to)
            var fileLines = File.ReadAllLines(filename);
            note.Priority = int.Parse(fileLines[0]);
            note.AssignedTo = fileLines[1];
            note.Text = string.Join(Environment.NewLine, fileLines.Skip(2));

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
