using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaesarCipher.Encryptors
{
    internal interface IEncryptor
    {
        string Encrypt(string source);
        string Decrypt(string source);
    }
}
