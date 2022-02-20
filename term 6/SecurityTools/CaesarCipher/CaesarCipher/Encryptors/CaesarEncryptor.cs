using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaesarCipher.Encryptors
{
    internal class CaesarEncryptor : IEncryptor
    {
        private int AlphabetPower { get => alphabet[1] - alphabet[0] + 1; }
        private int key;
        private string alphabet;

        public string Alphabet { 
            get
            { 
                return alphabet;
            }
            set 
            {
                if (string.IsNullOrEmpty(value) || value.Length < 2)
                {
                    this.alphabet = "az";
                }
                else if (value.Length > 2)
                {
                    this.alphabet = value.Substring(0, 2);
                }
                else
                {
                    this.alphabet = value;
                }

                this.alphabet = this.alphabet[1] > this.alphabet[0] ? this.alphabet : new string(this.alphabet.Reverse().ToArray());
            }  
        }
        

        public CaesarEncryptor(string? key, string? alphabet)
        {
            this.key = Convert.ToInt32(key);
            this.Alphabet = alphabet;
        }

        public string Encrypt(string source)
        {
            return this.Caesar(source);
        }

        public string Decrypt(string source)
        {
            return this.Caesar(source, false);
        }

        private string Caesar(string source, bool encrypting = true)
        {
            StringBuilder sb = new StringBuilder(source);
            for (int i = 0; i < sb.Length; i++)
            {
                bool isUpper = false;
                if (char.IsUpper(sb[i]))
                {
                    isUpper = true;
                }

                sb[i] = char.ToLowerInvariant(sb[i]);
                int letterPos = sb[i] - this.alphabet[0];
                int k = encrypting ? this.key : -this.key + AlphabetPower;
                if (letterPos >= 0 && letterPos < this.AlphabetPower)
                {
                    sb[i] = (char)((letterPos + k) % this.AlphabetPower + this.alphabet[0]);
                }

                sb[i] = isUpper ? char.ToUpperInvariant(sb[i]) : sb[i];
            }

            return sb.ToString();
        }
    }
}
