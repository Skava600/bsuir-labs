using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using LAB2.Parsers;
using LAB2.Options;
using System.Runtime.CompilerServices;

namespace LAB2
{
    class Logger
    {
        FileSystemWatcher watcher;
        string sourceDirectory = "";
        string targetDirectory = "";
        string archiveDirectory = "";
        public string logFile = "";
        OptionsManager optionsManager;
        object obj = new object();
        bool enabled = true;
        public void Log(string except, bool append)
        {
            using (StreamWriter writer = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory+"\\Filelog.txt", append))
            {
                writer.WriteLine(except + "-" + DateTime.Now.ToString("yyyy.mm.dd HH:mm:ss"));
                writer.Flush();
            }
        }
        public void Log(string except, bool append, string fileLog)
        {
            using (StreamWriter writer = new StreamWriter(fileLog, append))
            {
                writer.WriteLine(except + "-" + DateTime.Now.ToString("yyyy.mm.dd HH:mm:ss"));
                writer.Flush();
            }
        }
        public Logger()
        {
           
            string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
            optionsManager = new OptionsManager(appDirectory, this);

            ETLOptions options = optionsManager.GetOptions<ETLOptions>(this) as ETLOptions;
                
            Validator.Validate(options, this);
            sourceDirectory = options.DirectoryOptions.SourceDirectory;
            logFile = options.DirectoryOptions.LogFile;
            targetDirectory = options.DirectoryOptions.TargetDirectory;
            archiveDirectory = options.DirectoryOptions.ArchiveDirectory;
            using (StreamReader sr = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "\\Filelog.txt"))
            {
                string logs = sr.ReadToEnd();
                using (StreamWriter writer = new StreamWriter(logFile, false))
                {
                    writer.WriteLine(logs);
                    writer.Flush();
                }
            }
            File.Delete(AppDomain.CurrentDomain.BaseDirectory + "\\Filelog.txt");

         
            watcher = new FileSystemWatcher(sourceDirectory)
            {
                Filter = "*.txt"
            };
            watcher.Created += Watcher_Created;
        }

        public void Start()
        {
            watcher.EnableRaisingEvents = true;
            while (enabled)
            {
                Thread.Sleep(1000);
            }
        }
        public void Stop()
        {
            watcher.EnableRaisingEvents = false;
            enabled = false;
        }
        private void Watcher_Created(object sender, FileSystemEventArgs e)
        {
            RecordEntry(e.FullPath, "created");


        }

        private void RecordEntry(string filePath, string fileEvent)
        {
            ArchiveOptions archiveOptions = optionsManager.GetOptions<ArchiveOptions>(this) as ArchiveOptions;
            CipherOptions cipherOptions = optionsManager.GetOptions<CipherOptions>(this) as CipherOptions;
            string encrypted;
            FileInfo file = new FileInfo(filePath);
            string td = $"{targetDirectory}\\{file.LastWriteTime:yyyy\\\\MM\\\\dd}";

            string newName = $"Sales_{file.LastWriteTime:yyyy_MM_dd_HH_mm_ss}";
            int i = 0;
            while (File.Exists($"{td}\\{newName}.txt"))
            {
                i++;
                newName = $"{Path.GetFileNameWithoutExtension(filePath)}({i})_{file.LastWriteTime:yyyy_MM_dd_HH_mm_ss}";
            }
            if (!Directory.Exists(td))
            {
                Directory.CreateDirectory(td);
            }
            Log($"File {newName} {fileEvent}", true, logFile);
            using (StreamReader sr = new StreamReader(filePath))
            {
                encrypted = Cypher.Encrypt(sr.ReadToEnd(), cipherOptions.EncrypterKey);
            }
            using (StreamWriter sw = new StreamWriter(filePath))
            {
                sw.Write(encrypted);
            }
            string newPath = $"{td}\\{newName}";
            try
            {
                Archivate.Compress(filePath, newPath + ".gz", archiveOptions.CompressionLevel);
                Archivate.Compress(filePath, $"{archiveDirectory}\\{newName}.gz", archiveOptions.CompressionLevel);
                Archivate.Decompress(newPath + ".gz", newPath + ".txt");
            }
            catch (Exception ex)
            {
                Log($"Fatal error ocured while compressing - {ex}", true, logFile);
                return;
            }
            File.Delete(filePath);
            File.Delete(newPath + ".gz");
            using (StreamReader sr = new StreamReader(newPath + ".txt"))
            {
                encrypted = sr.ReadToEnd();
            }
            using (StreamWriter sw = new StreamWriter(newPath + ".txt"))
            {
                sw.Write(Cypher.Decrypt(encrypted, cipherOptions.EncrypterKey));
            }
            Log($"File {newName} sent successfully", true, logFile);

        }
        
    }




}

