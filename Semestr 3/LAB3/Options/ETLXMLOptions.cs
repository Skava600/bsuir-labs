using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LAB2.Parsers;
namespace LAB2.Options
{
    public class ETLXMLOptions : ETLOptions
    {
        public ETLXMLOptions(string xml) : base()
        {
            ETLOptions options = Converter.DeserializeXML<ETLOptions>(xml);
            ArchiveOptions = options.ArchiveOptions;
            CipherOptions = options.CipherOptions;
            DirectoryOptions = options.DirectoryOptions;
        }
    }
}
