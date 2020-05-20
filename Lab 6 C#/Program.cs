using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Text;
using System.Xml.Schema;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Media;

namespace lab3
{
    class Program
    {
        public const int MAXLIST = 40;
        public static void ShowArmyTypes(List<Unit> army)
        {
            Console.WriteLine("1 - Cavalry 2 - Infantry 3 - Horse Archer 4 - Archer");
            Console.Write("|");
            for (int i = 0; i < army.Count; i++)
            {
                if (i == MAXLIST/2) Console.Write("|\n|"); //WIDTH OF FRONT.MAX TROOPS / 2 ( 2 LINES).
                Console.Write((int)army[i].IntType);
            }
            Console.WriteLine("|");
        }
        public static int ChooseTypeUnit()
        {
            Console.WriteLine("1 - Cavalry 2 - Infantry 3 - Horse Archer 4 - Archer");
            int ch;
            while (!int.TryParse(Console.ReadLine(), out ch) || ch < 1 || ch > 4)
            {
                Console.Write("Input Error! Enter  unsigned integer  before 5 ");
            }
            return ch;
        }
        public static int ChooseArmyUnit(List<Unit> army, int numUnit)
        {
            Console.WriteLine($"Choose index of unit, army's size - {army.Count}");
            int ch;
            while (!int.TryParse(Console.ReadLine(), out ch) || ch >= army.Count || ch < 0 || ch == numUnit)
            {
                Console.Write($"Input Error! Enter  unsigned integer  before {army.Count} ");
            }
            return ch;
        }

        public static void AddUnitDef(List<Unit> army)
        {
            if(army.Count == MAXLIST)
            {
                Console.WriteLine("Limit of Army!!");
                return;
            }
            int ch = ChooseTypeUnit();
            if (ch == (int)Type.HorseMan) army.Add(new HorseMan());
            else if (ch == (int)Type.Infantry) army.Add(new Infantry());
            else if (ch == (int)Type.Archer) army.Add(new Archer());
            else if (ch == (int)Type.HorseArcher) army.Add(new HorseArcher());

        }
        static void Main()
        {
            List<Unit> army = new List<Unit>();
            int ch;
            do
            {              
                Console.Clear();
                Console.WriteLine("Enter Action:\n1.Add unit " +
                    ".\n2.Actions with choosen unit" +
                    "\n3.Armie's Size.\n4.Show Army.\n5.Sort Army.\n6.Continue(BATTLE BETWEEN ARMIES)");
                while (!int.TryParse(Console.ReadLine(), out ch) || ch < 1 || ch > 6)
                {
                    Console.Write("Input Error! Enter  unsigned integer  before 7 ");
                }
                switch (ch)
                {
                    case 1: AddUnitDef(army); break;
                    case 2:
                        {
                            if (army.Count > 0)
                            {
                                int chUnit = ChooseArmyUnit(army, -1);                            
                                army[chUnit].ShowActionsUnit(army, chUnit);  // -1 because, unit didn't choosen       
                            }
                            else Console.WriteLine("Army is empty");
                            break;
                        }
                    case 3: Console.WriteLine($"Size of Army - {army.Count}"); break;
                    case 4: ShowArmyTypes(army);break;
                    case 5: army.Sort();break;
                    
                }
                Console.ReadLine();
            } while (ch != 6);
            List<Unit> army1 = new List<Unit>();
            List<Unit> army2 = new List<Unit>();
            Random rnd = new Random();
            for(int i = 0; i < 40; i++)
            {
                ch = rnd.Next(0, 4);
                switch (ch)
                {
                    case 0: army1.Add(new HorseMan());break;
                    case 1: army1.Add(new Infantry()); break;
                    case 2: army1.Add(new HorseArcher()); break;
                    case 3: army1.Add(new Archer()); break;
                }
                ch = rnd.Next(0, 4);
                switch (ch)
                {
                    case 0: army2.Add(new HorseMan()); break;
                    case 1: army2.Add(new Infantry()); break;
                    case 2: army2.Add(new HorseArcher()); break;
                    case 3: army2.Add(new Archer()); break;
                }
            }
            army1.Sort();
            army2.Sort();
            while (army1.Count != 0 && army2.Count != 0)
            {
                for(int i = 0; i < (army1.Count > army2.Count?army2.Count: army1.Count); i++)
                {
                    Sound.PlaySound("G:\\battle.wav", new System.IntPtr(),  Sound.PlaySoundFlags.SND_ASYNC);                  
                    Console.WriteLine("1 ARMY");
                    ShowArmyTypes(army1);
                    Console.WriteLine("2 ARMY");
                    ShowArmyTypes(army2);
                    Thread.Sleep(2500);
                    Unit.Duel(army1[i], army2[i], army1, army2);
                  
                }
                
            }
            if (army1.Count == 0)
            {
                Console.WriteLine("VICTORY OF 2 ARMY:");
                ShowArmyTypes(army2);
            }
            else 
            {
                Console.WriteLine("VICTORY OF 1 ARMY :");
                ShowArmyTypes(army1);
            }
                
            
        }
        
    }
}
