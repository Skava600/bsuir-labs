using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB2.Options
{
    public class ETLOptions:Options
    {
        public DirectoryOptions DirectoryOptions { get; set; } = new DirectoryOptions();
        public CipherOptions CipherOptions { get; set; } = new  CipherOptions();       
        public ArchiveOptions ArchiveOptions { get; set; } = new ArchiveOptions();
        public ETLOptions(DirectoryOptions directoryOptions, CipherOptions cipherOptions,
               ArchiveOptions archiveOptions)
        {
            DirectoryOptions = directoryOptions;
            CipherOptions = cipherOptions;
            ArchiveOptions = archiveOptions;
        }
        public ETLOptions() 
        {
        }
    }
}
