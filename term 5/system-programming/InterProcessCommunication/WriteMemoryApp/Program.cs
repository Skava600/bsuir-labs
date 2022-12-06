using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.MemoryMappedFiles;

namespace WriteMemoryAp
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true) { 
            Console.WriteLine("Enter message.");
            //Ввод выражения для записи в общую память
            char[] message = Console.ReadLine().ToCharArray();
            //Размер введенного сообщения
            int size = message.Length;


            MemoryMappedFile sharedMemory = MemoryMappedFile.CreateOrOpen("MemoryFile", size * 2 + 4);
            using (MemoryMappedViewAccessor writer = sharedMemory.CreateViewAccessor(0, size * 2 + 4))
            {
                //запись в разделяемую память
                //запись размера с нулевого байта в разделяемой памяти
                writer.Write(0, size);
                //запись сообщения с четвертого байта в разделяемой памяти
                writer.WriteArray<char>(4, message, 0, message.Length);
            }

            Console.WriteLine("Message is written to divided memory");
        }
            Console.WriteLine("To exit press any key");
            Console.ReadLine();
        }
    }
}