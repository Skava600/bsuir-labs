using System;
using System.Collections.Generic;
using System.IO;

namespace FileManager
{
    public static class FileManager
    {
        private static int Count = 1;

        public static string[] ReadFile(string fileName)
        {
            List<string> strList = new List<string>();

            using (FileStream fileStream = new FileStream(fileName, FileMode.Open))
            using (StreamReader streamReader = new StreamReader(fileStream))
            { 
                while (!streamReader.EndOfStream)
                {
                    strList.Add(streamReader.ReadLine());
                }
            }

            return strList.ToArray();
        }

        public static void RewriteFile(string fileName, IEnumerable<string> strings)
        {
            if (strings == null)
            {
                throw new ArgumentNullException(nameof(strings));
            }

            if (fileName == null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            using (StreamWriter streamWriter = new StreamWriter(fileName, false))
            {
                foreach(var item in strings)
                {
                    streamWriter.WriteLine(item);
                }
            }
        }


        public static string MakeFile(int count)
        {
            if (count <= 0)
            {
                throw new ArgumentException($"{nameof(count)} can not be negative or zero.");
            }

            Random random = new Random();
            string fileName = $"File{Count++}.txt";

            using (StreamWriter streamWriter = new StreamWriter(fileName, false))
            {
                for (int i = 0; i < count; i++)
                {
                    streamWriter.WriteLine(random.Next(1000));
                }
            }

            return fileName;
        }
    }
}
