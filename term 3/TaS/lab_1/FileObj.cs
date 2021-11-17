using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1form
{
    class FileObj
    {
        private string name;
        private string path;
        private bool isFolder;
        public string Name { get { return name; } set { name = value; } }
        public string Path { get { return path; } set { path = value; } }
        public bool IsFolder => isFolder;
        public FileObj(string newName, bool newIsFolder)
        {
            name = newName;
            isFolder = newIsFolder;
            path = "";
        }
        public FileObj(bool newIsFolder, string newPath)
        {
            name = "";
            isFolder = newIsFolder;
            path = newPath;
        }
    }
}
