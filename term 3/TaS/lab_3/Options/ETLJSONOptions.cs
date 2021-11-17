using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LAB2.Parsers;
namespace LAB2.Options
{
    
        class ETLJSONOptions : ETLOptions
        {
            public ETLJSONOptions(string json) : base()
            {
                ETLOptions options = Converter.DeserializeJson<ETLOptions>(json);               
                ArchiveOptions = options.ArchiveOptions;
                CipherOptions = options.CipherOptions;
                DirectoryOptions = options.DirectoryOptions;
            }
        }
    
}
