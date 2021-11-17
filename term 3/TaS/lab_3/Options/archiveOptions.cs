using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;

namespace LAB2.Options
{
    public class ArchiveOptions:Options
    {
        public CompressionLevel CompressionLevel { get; set; } = CompressionLevel.Optimal;
        public ArchiveOptions()
        {
        }
    }
}
