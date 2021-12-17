using System;
using System.Linq;

namespace FileManager
{
    public class FileProcessor
    {
        public void MakeAnotherFile(string sourceFile, string destinationFile)
        {
            var strings = FileManager.ReadFile(sourceFile).Select(str => Process(str));
            FileManager.RewriteFile(destinationFile, strings);
        }

        private static string Process(string str)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }

            return str + $"text length - {str.Length}";
        }
    }
}
