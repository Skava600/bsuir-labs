using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
    public class AsyncFileProcessor
    {
        private bool ended;
        private ConcurrentQueue<string> queue;

        public AsyncFileProcessor()
        {
            this.ended = false;
            this.queue = new ConcurrentQueue<string>();
        }

        public async Task MakeAnotherFile(string sourceFile, string destinationFile)
        {
            Task readTask = ReadAsync(sourceFile);
            Task writeTask = WriteAsync(destinationFile);
            await Task.WhenAll(readTask, writeTask).ConfigureAwait(false);
        }

        private async Task ReadAsync(string fileName)
        {
            using (FileStream fileStream = new FileStream(fileName, FileMode.Open))
            using (StreamReader streamReader = new StreamReader(fileStream))
            {
                while(!streamReader.EndOfStream)
                {
                    queue.Enqueue(await streamReader.ReadLineAsync().ConfigureAwait(false));
                }
            }

            ended = true;
        }

        private async Task WriteAsync(string fileName)
        {
            using (StreamWriter streamWriter = new StreamWriter(fileName, false))
            {
                while(!ended || !queue.IsEmpty)
                {
                    if (queue.TryDequeue(out string result))
                    {
                        await streamWriter.WriteLineAsync(Process(result)).ConfigureAwait(false);
                    }
                }
            }
        }

        private static string Process(string str)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }

            return str + $" text length - {str.Length}";
        }
    }
}
