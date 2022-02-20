using CaesarCipher.Encryptors;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CaesarCipher
{
    public static class Program
    {
        private static string? sourceFile;
        private static string? outputFile;
        private static string cipherKey = String.Empty;
        private static string cipherType = String.Empty;
        private static string cipherAlphabet = String.Empty;
        private static bool isEncrypting;

        private static readonly Dictionary<string, Action<string>> commandParameters = new Dictionary<string, Action<string>>()
        {
            ["--type"] = (string s) => Program.cipherType = s.ToLower(),
            ["--key"] = (string s) => Program.cipherKey = s.ToLower(),
            ["--alphabet"] = (string s) => Program.cipherAlphabet = s.ToLower(),
            ["--source"] = (string s) => Program.sourceFile = s,
            ["--output"] = (string s) => Program.outputFile = s,
            ["--action"] = (string s) =>
            {
                isEncrypting = s.ToLower() switch
                {
                    "enc" => true,
                    "dec" => false,
                    _ => false,
                };
            }
        };

        public static void Main(string[] args)
        {
            SetTaskFromArgs(args);

            IEncryptor encryptor;
            if (!cipherType.Equals("caesar", StringComparison.Ordinal) && !cipherType.Equals("vigenere", StringComparison.Ordinal))
            {
               cipherType = "caesar";
            }

            try
            {
                if (!File.Exists(sourceFile))
                {
                    throw new ArgumentException($"Source file '{sourceFile}' is not exist.");

                }

                if (File.Exists(outputFile))
                {
                    char answer;

                    do
                    {
                        Console.Write($"File is exist - rewrite {outputFile}? [Y/n] ");
                        answer = Console.ReadKey().KeyChar;
                        Console.WriteLine();
                    }
                    while (!char.ToLowerInvariant(answer).Equals('y') && !char.ToLowerInvariant(answer).Equals('n'));

                    if (char.ToLowerInvariant(answer).Equals('n'))
                    {
                        throw new ArgumentException("Output file is already exist.");
                    }
                }

                string source = File.ReadAllText(sourceFile);

                switch (cipherType)
                {
                    case "caesar":
                        encryptor = new CaesarEncryptor(Program.cipherKey, cipherAlphabet);
                        break;
                    case "vigenere":
                        encryptor = new VigenereEncryptor(Program.cipherKey, cipherAlphabet);
                        break;
                    default: throw new ArgumentException($"Invalid type of encryption {cipherType}.");

                }

                string result;
                if (Program.isEncrypting)
                {
                    result = encryptor.Encrypt(source);
                }
                else
                {
                    result = encryptor.Decrypt(source);
                }

                File.WriteAllText(outputFile!, result);
                Console.WriteLine(result);
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void SetTaskFromArgs(string [] args)
        {
            foreach (var arg in args)
            {
                string[] parameters = arg.Split('=', 2);
                if (parameters.Length < 1)
                {
                    continue;
                }

                string command = parameters[0];
                string value = parameters[1];

                if (commandParameters.TryGetValue(command, out Action<string>? action))
                {
                    action(value);
                }
            }
        }
    }


}