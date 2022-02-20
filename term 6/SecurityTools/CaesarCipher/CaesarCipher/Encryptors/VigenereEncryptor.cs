using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaesarCipher.Encryptors
{
    internal class VigenereEncryptor : IEncryptor
    {
        private int AlphabetPower { get => alphabet[1] - alphabet[0] + 1; }
        private string key;
        private string alphabet;

        public string Key { 
            get => this.key; 
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("Key is null or epmpty.");
                }

                this.key = value;
            }
        }

        public string Alphabet
        {
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

        public VigenereEncryptor(string key, string alphabet)
        {
            this.Key = key;
            this.Alphabet = alphabet;
        }

        public string Decrypt(string source) => this.Vigenere(source, false);

        public string Encrypt(string source) => this.Vigenere(source);

        private string Vigenere(string source, bool encrypting = true)
        {
            StringBuilder output = new StringBuilder(source);
            int nonAlphaCharCount = 0;

            for (int i = 0; i < source.Length; ++i)
            {
                bool isUpper = false;
                if (char.IsUpper(output[i]))
                {
                    isUpper = true;
                }

                output[i] = char.ToLowerInvariant(output[i]);
                int letterPos = output[i] - this.alphabet[0];
                if (letterPos >= 0 && letterPos < this.AlphabetPower)
                {
                    int keyIndex = (i - nonAlphaCharCount) % key.Length;
                    int k = this.key[keyIndex] - this.alphabet[0];
                    k = encrypting ? k : -k + AlphabetPower;

                    output[i] = (char)((letterPos + k) % this.AlphabetPower + this.alphabet[0]);
                }
                else
                {
                    nonAlphaCharCount++;
                }

                output[i] = isUpper ? char.ToUpperInvariant(output[i]) : output[i];
            }

            return output.ToString();
        }
    }
}
