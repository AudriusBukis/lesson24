using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Lesson24.Methods
{
    public class FileService : FilePath
    {
        public FileService(string fileName) : base(fileName)
        {
        }
        public List<string> GetAllLines()
        {
            var lines = File.ReadAllLines(Path);
            return lines.ToList();
        }
        public void AppendText(string text)
        {
            using StreamWriter sw = File.AppendText(Path);
            sw.WriteLine(text);
        }
        public void WriteAllText(string[] lines)
        {
            File.WriteAllLines(Path, lines);
           
        }

    }
}
