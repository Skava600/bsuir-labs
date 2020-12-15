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
using LAB2.Options;
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
        
     
        protected override void OnStart(string[] args)
        {
            logger = new Logger();
            Thread loggerThread = new Thread(new ThreadStart(logger.Start));
            loggerThread.Start();
              
            
            logger.Log("Service started succsessfully", true, logger.logFile);

        }

        protected override void OnStop()
        {
            logger.Log("Service stopped", true, logger.logFile);
            logger.Stop();
            Thread.Sleep(1000);
        }
    }
}
