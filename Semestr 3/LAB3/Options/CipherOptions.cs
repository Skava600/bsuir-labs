using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB2.Options
{
    public class CipherOptions:Options
    {
        public string EncrypterKey { get; set; } = "abc123";
        public CipherOptions()
        {

        }
    }
}
