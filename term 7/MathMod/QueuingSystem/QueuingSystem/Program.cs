using System;

namespace QueuingSystem
{
    class Program
    {
        public static void Main(string[] args)
        {
            Canteen canteen = new Canteen();
            canteen.X = 2;
            canteen.p = 3.5;
            canteen.q = 0.7;
            canteen.t = 3;

            double averageQueueLength = canteen.GetAverageQueueLength();
            double averageStayTime = canteen.GetAverageStayTime();
            if (canteen.isStationary)
            {
                Console.WriteLine($"Система стабильна и стационарна ");
                Console.WriteLine($"средняя длина очереди {averageQueueLength}");
                Console.WriteLine($"средняя длина пребывания в столовой {averageStayTime}");
            }
            else
            {
                Console.WriteLine("Система нестабильна, очередь бесконечна.");
            }

           
        }
    }
}