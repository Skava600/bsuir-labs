using System;

namespace RandomEvents
{
    internal class Program
    {
        const int N = 1_000_000;
        static void Main(string[] args)
        {
            //task 1
            int probability = 60;
            SimpleEvent simpleEvent = new SimpleEvent(probability);
            int trueCount = 0;
            for (int i = 0; i < N; i++)
            {
                if (simpleEvent.Generate())
                {
                    trueCount++;
                }
            }

            int falseCount = N - trueCount;

            Console.WriteLine($"True count - {trueCount}, false count - {falseCount}\n" +
                $"frequency of true - {(double)trueCount / N}, false frequency - {(double)falseCount / N}");

            //task 2

            ComplexEvent complexEvent = new ComplexEvent(new List<int> { 60, 20 }); 

            int AB = 0, AnB = 0, nAB = 0, nAnB = 0;
            for (int i = 0; i < N; i++)
            {
                var list = complexEvent.Generate();
                if (list[0] == true && list[1] == true)
                    AB++;
                else if (list[0] == false && list[1] == false)
                    nAnB++;
                else if (list[0] == true && list[1] == false)
                    AnB++;
                else nAB++;
            }

            Console.WriteLine($"AB count - {AB}, AnB count  - {AnB} nAB count - {nAB}, nAnB count  - {nAnB}\n" +
               $"frequency of AB - {(double)AB / N}, AnB frequency - {(double)AnB / N}" +
               $"frequency of nAB - {(double)nAB / N}, nAnB frequency - {(double)nAnB / N}");
        }
    }
}