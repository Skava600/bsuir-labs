using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.IO;
using System.Threading;
using System.Text;
using System.Threading.Tasks;

namespace LAB2
{
    public partial class FileWatcher : ServiceBase
    {
        Logger logger;
       
        public FileWatcher()
        {
            InitializeComponent();
            this.CanStop = true; // службу можно остановить
            this.CanPauseAndContinue = true; // службу можно приостановить и затем продолжить
            this.AutoLog = true; // служба может вести запись в лог
        }
        class Logger
        {
            FileSystemWatcher watcher;
            object obj = new object();
            bool enabled = true;
            string targetDirectory;
            string archiveDirectory;
            public Logger()
            {
                watcher = new FileSystemWatcher("C:\\FileWatcher\\SourceDirectory");
                watcher.Filter = "*.txt";
                targetDirectory = "C:\\FileWatcher\\TargetDirectory";
                archiveDirectory = $"{targetDirectory}\\archive";
                Directory.CreateDirectory(archiveDirectory);
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
            // переименование файлов
            private void Watcher_Created(object sender, FileSystemEventArgs e)
            {
                RecordEntry(e.FullPath, "created");

            }
            private void RecordEntry(string filePath, string fileEvent )
            {
               
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
                    Log($"File {newName} {fileEvent}");
                    using (StreamReader sr = new StreamReader(filePath))
                    {
                        encrypted = Cypher.Encrypt(sr.ReadToEnd());
                    }
                    using (StreamWriter sw = new StreamWriter(filePath))
                    {
                        sw.Write(encrypted);
                    }
                    string newPath = $"{td}\\{newName}";
                    try
                    {
                        Archivate.Compress(filePath, newPath + ".gz");
                        Archivate.Compress(filePath, $"{archiveDirectory}\\{newName}.gz");
                        Archivate.Decompress(newPath + ".gz", newPath + ".txt");
                    }
                    catch (Exception ex)
                    {
                        this.Log($"Fatal error ocured while compressing - {ex}");
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
                        sw.Write(Cypher.Decrypt(encrypted));
                    }
                    Log($"File {newName} sent successfully");
                



            }
            public void Log(string except)
            {
                using (StreamWriter writer = new StreamWriter("C:\\FileWatcher\\TargetDirectory\\Filelog.txt", true))
                {
                    writer.WriteLine(except + "-" + DateTime.Now.ToString("yyyy.mm.dd HH:mm:ss"));
                    writer.Flush();
                }
            }
        
        }
     
        protected override void OnStart(string[] args)
        {
            string source = "C:\\FileWatcher\\SourceDirectory";
            string target = "C:\\FileWatcher\\TargetDirectory";
            string logFile = "C:\\FileWatcher\\TargetDirectory\\Filelog.txt";
            Directory.CreateDirectory(source);
            Directory.CreateDirectory(target);
            string name = logFile.Split('\\').Last();
            string path = logFile.Substring(0, logFile.Length - name.Length);
            Directory.CreateDirectory(path);
            File.Create(logFile).Close();          
            logger = new Logger();
            Thread loggerThread = new Thread(new ThreadStart(logger.Start));
            loggerThread.Start();
            logger.Log("Service started succsessfully");

        }

        protected override void OnStop()
        {
            logger.Log("Service stopped");
            logger.Stop();
            Thread.Sleep(1000);
        }
    }
}
