using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB2.Options
{
    public class DirectoryOptions:Options
    {
        public string SourceDirectory { get; set; } = "C:\\FileWatcher\\SourceDirectory";
        public string TargetDirectory { get; set; } = "C:\\FileWatcher\\TargetDirectory";
        public string LogFile { get; set; } = "C:\\FileWatcher\\TargetDirectory\\Filelog.txt";
        public string ArchiveDirectory { get; set; } = "C:\\FileWatcher\\TargetDirectory\\archive"; //Директория с зашифрованными архивами
        public  DirectoryOptions()
        {

        }
    }
}
