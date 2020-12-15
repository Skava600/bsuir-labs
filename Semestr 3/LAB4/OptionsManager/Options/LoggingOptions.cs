using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptionsManager.Options
{
    public class LoggingOptions
    {
        public string LoggingPath { get; set; } = $@"C:\FileWatcher\TargetDirectory\LogFile.txt";
        public LoggingOptions()
        {

        }
    }
}
