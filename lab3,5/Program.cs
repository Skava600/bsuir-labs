using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Text;
using System.Xml.Schema;

namespace lab3
{ 

    class Program
    {
        public static int ChooseTypeUnit()
        {
            Console.WriteLine("1-Всадник, 2- Пехота, 3 - Лучник, 4 - Конный лучник");
            int ch;
            while (!int.TryParse(Console.ReadLine(), out ch) || ch < 1 || ch > 4)
            {
                Console.Write("Ошибка ввода! Введите целое  положительное число до 5 ");
            }
            return ch;
        }
        public static int ChooseArmyUnit(int size)
        {
            Console.WriteLine($"Выберите индекс юнита, размер армии - {size}");
            int ch;
            while (!int.TryParse(Console.ReadLine(), out ch) || ch >= size || ch < 0)
            {
                Console.Write($"Ошибка ввода! Введите целое  положительное число до {size} ");
            }
            return ch;
        }   
        public static void AddUnitDef(Army army)
        {

            int ch = ChooseTypeUnit();
            if (ch == (int)Type.HorseMan) army[Army.size] = new HorseMan();
            else if (ch == (int)Type.Infantry) army[Army.size] = new Infantry();
            else if (ch == (int)Type.Archer) army[Army.size] = new Archer();
            else if (ch == (int)Type.HorseArcher) army[Army.size] = new HorseArcher();

        }
        static void Main(string[] args)
        {
            Army army1 = new Army();    
            int ch;
            do
            {              
                Console.Clear();
                Console.WriteLine("Введите действие:\n1.Добавить Юнита c предустановленными " +
                    "характеристиками.\n2.Действия с Выбранным юнимтом." +
                    "\n3.Размер армии.\n4.Показать типы юнитов в армии.\n5.Сортировать");
                while (!int.TryParse(Console.ReadLine(), out ch) || ch < 1 || ch > 6)
                {
                    Console.Write("Ошибка ввода! Введите целое  положительное число до 7 ");
                }
                switch (ch)
                {
                    case 1: AddUnitDef(army1); break;
                    case 2:                       
                            int numUnit;
                            numUnit = ChooseArmyUnit(Army.size);
                            army1[numUnit].ShowActionsUnit(army1, numUnit);                       
                            break;
                    case 3: Console.WriteLine($"Размер армии - {Army.size}"); break;
                    case 4: army1.ShowArmyTypes();break;

                }
                Console.ReadLine();
            } while (ch != 6);
        }
    }
}