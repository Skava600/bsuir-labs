using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptionsManager.Options
{
    public class SendingOptions
    {
        public string TargetDirectory { get; set; } = $@"C:\FileWatcher\SourceDirectory";
        public SendingOptions()
        {

        }
    }
}
