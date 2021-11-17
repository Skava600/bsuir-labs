using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using LAB2.Options;

namespace LAB2
{
    public static class Validator

    {
        public static bool MakeValidDir(string path)
        {
            if (!Directory.Exists(path))
            {
                try
                {
                    Directory.CreateDirectory(path);
                    
                }
                catch
                {
                    return false;
                }
            }

            return true;
        }

        public static bool MakeValidFile(string path)
        {
            if (!File.Exists(path))
            {
                if (!MakeValidDir(Path.GetDirectoryName(path)))
                {
                    return false;
                }

                try
                {
                    File.Create(path).Close();
                }
                catch
                {
                    return false;
                }
            }

            return true;
        }

        internal static void Validate(ETLOptions options, Logger logger)
        {
            DirectoryOptions directoryOptions = options.DirectoryOptions;
            if (!MakeValidDir(directoryOptions.SourceDirectory))
            {
                directoryOptions.SourceDirectory = @"C:\FileWatcher\SourceDirectory";
                MakeValidDir(directoryOptions.SourceDirectory);

                logger.Log("Using default directory. Error in creating sourseDirectory ", true);
            }

            if (!MakeValidDir(directoryOptions.TargetDirectory))
            {
                directoryOptions.TargetDirectory = @"C:\FileWatcher\TargetDirectory";
                MakeValidDir(directoryOptions.TargetDirectory);

                logger.Log("Using default directory. Error in creating targetDirectory", true);
            }
            if (!MakeValidDir(directoryOptions.ArchiveDirectory))
            {
                directoryOptions.ArchiveDirectory = @"C:\FileWatcher\TargetDirectory\archive";
                MakeValidDir(directoryOptions.ArchiveDirectory);

                logger.Log("The access to archive directory is denied, using default directory.", true);
            }


            if (!MakeValidFile(directoryOptions.LogFile))
            {
                directoryOptions.LogFile = @"C:\FileWtcher\TargetDirectory\Logfile.txt";
                MakeValidFile(directoryOptions.LogFile);

                logger.Log("The access to log file is denied, using default log file.", true);
            }


            ArchiveOptions archivationOptions = options.ArchiveOptions;
           

            if ((int)archivationOptions.CompressionLevel < 0 || (int)archivationOptions.CompressionLevel > 2)
            {
                archivationOptions.CompressionLevel = System.IO.Compression.CompressionLevel.Optimal;

                logger.Log("Incorrect value of compression level. Default value is set.", true);
            }
        }
    }
}
