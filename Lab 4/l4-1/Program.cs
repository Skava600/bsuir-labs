using System;


using System.Text;
using System.Threading;

namespace _4lab
{
    class Program
    {
        public static StringBuilder GetSystemTime()
        {
            SystemTime info = new SystemTime();
            SystemTime.GetLocalTime(out info);
            StringBuilder strTime = new StringBuilder();
            Console.ResetColor();
            strTime.Append($"Date: {info.Day}.{info.Month}.{info.Year} Time: " +
                $"{info.Hour}:{info.Minute}:{info.Second}:{info.Milliseconds} \n");
            return strTime;
        }

        public static StringBuilder DisplayMemory()
        {
            MEMORYSTATUSEX info = new MEMORYSTATUSEX();
            MEMORYSTATUSEX.GlobalMemoryStatusEx(info);
            StringBuilder strMem = new StringBuilder();
            Console.ResetColor();
            strMem.Append($"Memory load: {info.dwMemoryLoad}% \n");
            strMem.Append($"\nMemory architecture : {info.dwLength}\n");
            strMem.Append($"\nTotal Memory: {info.ullTotalPhys / (1.07 * Math.Pow(10, 9))} Gb.\n");
            strMem.Append($"\nAvailable Memory : {info.ullAvailPhys / (1.07 * Math.Pow(10, 9))} Gb.\n");
            strMem.Append($"\nCommited Memory : {info.ullTotalPageFile / (1.07 * Math.Pow(10, 9))} Gb.\n");
            strMem.Append($"\nAvailable Commited memory: {info.ullAvailPageFile / (1.07 * Math.Pow(10, 9))} Gb.\n");
            strMem.Append($"\nTotal virtual Memory: {info.ullTotalVirtual / (1.07 * Math.Pow(10, 9))} Gb.\n");
            strMem.Append($"\nAvailable virtual Memory : {info.ullAvailVirtual / (1.07 * Math.Pow(10, 9))} Gb.\n");
            strMem.Append($"\nAilable Extented virtual Memory : {info.ullAvailExtendedVirtual / (1.07 * Math.Pow(10, 9))} Gb.\n");
            return strMem;
            
        }

        public static StringBuilder GetInfoCpu()
       {
            CPU.GetSystemInfo(out CPU.SYSTEM_INFO info);
            StringBuilder strCPU = new StringBuilder();
            Console.ResetColor();
            strCPU.Append($"Processor architecture is {info.ProcessorArchitecture}\n");
            strCPU.Append($"\nNumber of processors: {info.dwNumberOfProcessors }\n");
            strCPU.Append($"\nPage size {info.dwPageSize }\n");
            strCPU.Append($"\nActive Allocation granularity {info.dwAllocationGranularity }\n");
            strCPU.Append($"\nProcessor level {info.wProcessorLevel }\n");
            strCPU.Append($"\nProcessor Type {info.dwProcessorType }\n");
            return strCPU;
        }
        
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Info about CPU: \n");
            Console.WriteLine(GetInfoCpu());
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Info about Memory:\n");
            Console.WriteLine(DisplayMemory());
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Current Time:\n");
            Console.WriteLine(GetSystemTime()); 
           
        }

    }
    
}

