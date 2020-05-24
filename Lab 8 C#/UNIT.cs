#define _INFOBATTLE  //    Remove define to see battle information(damage, speed and etc.)
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Text;
using Microsoft.VisualBasic.CompilerServices;

namespace lab3
{
    public enum Type
    {
        HorseMan = 1,
        Infantry,
        HorseArcher,
        Archer
    }
    public enum RangeUnit
    {
        HorseMan = 0,
        HorseArcher = 100,
        Infantry = HorseMan,     
        Archer = HorseArcher
    }

    //*****КЛАСС НОВОБРАНЦА***************
    public abstract class Unit : Person, IComparable
    {

        internal static Random rnd = new Random();
        public delegate void DieEvent(Unit u);
        public event DieEvent Died;
        public delegate void WinEvent(Unit u);
        public event WinEvent Win;

        public delegate void ActionHorse();
        protected float HealthUnit { get; set; }
        protected float AttackUnit { get; set; }
        protected float SpeedUnit { get; set; }
        protected float ArmourUnit { get; set; } 
        protected RangeUnit RangeUnit { get; set; }
        protected string TypeStr { get; set; }
        public Type IntType { get ; set; }
        protected float Speed
        {
            get
            { 
                return (1 - ArmourUnit/2); 
            }
            set 
            { 
                if (value > 0) { SpeedUnit = value; } 
            }
        }


        public Unit(float a, float  b, float c, float g, string d)
        {
            HealthUnit = a;
            AttackUnit = b;
            ArmourUnit = c;
            TypeStr = d;
            SpeedUnit = g * Speed;
        }
        public void Salute(Unit u)
        {
            if (u != null)
                Console.WriteLine($"{TypeStr} {Name} Salute to {u.TypeStr} {u.Name}.");
        }
        public void CleanPotato() =>   Console.WriteLine($"{TypeStr} {Name} cleaned potatoe.");
        public void ShowType() => Console.WriteLine($"Unit {TypeStr}.");
        public void ShowHealth() => Console.WriteLine($"Unit {TypeStr} {Name} has  {HealthUnit} hp");
        public void ShowAttack() => Console.WriteLine($"Unit {TypeStr} {Name} deal  {AttackUnit} damage");
        public void ShowSpeed() => Console.WriteLine($"Unit {TypeStr} {Name} has speed {SpeedUnit} m/s ");
        public void ShowRange() => Console.WriteLine($"Unit {TypeStr} {Name}has attack's range {(int)RangeUnit}  m. ");
        public void ShowArmour() => Console.WriteLine($"Unit {TypeStr} {Name} has  {ArmourUnit} % armour. ");
        public void StandUp() => Console.WriteLine($"Unit {TypeStr} {Name}'s horse Standed up");
        public void NoStandUp() => Console.WriteLine($"Unit {TypeStr} {Name} have no horse.");
        public void WhenDied(Unit dead)
        {
            Died?.Invoke(dead);
        }
        public void WhenWin(Unit win)
        {
            Win?.Invoke(win);
        }
        public override void ShowInfo()
        {
            base.ShowInfo();
            ShowType();
            ShowHealth();
            ShowAttack();
            ShowSpeed();
            ShowRange();
            ShowArmour();
            ShowID();
        }
        public abstract void Train();//ЮНИТ ТРЕНИРУЕТСЯ И УВЕЛИЧИВАЕТ ХАР-КИ В ЗАВИСИМОСТИ ОТ ТИПА (ДО ОПР. МАКС ЧИСЛА ХАР-КИ)

       
        public virtual void ShowActionsUnit(List<Unit> army, int numUnit)
        {
            if (army != null)
            {
                int ch;
                do
                {
                    Console.Clear();
                    Console.WriteLine("Enter Action :\n1.Train \n2.Show choosen unit.\n3.Clean Potatoe" +
                           "\n4.Stand up on horse \n5.Duel.\n6.Salute to allie.\n7.Exit");
                    while (!int.TryParse(Console.ReadLine(), out ch) || ch < 1 || ch > 7)
                    {
                        Console.Write("Input Error! Enter  unsigned integer  before 7 ");
                    }
                    switch (ch)
                    {
                        case 1: army[numUnit].Train(); army[numUnit].ShowInfo(); break;
                        case 2: army[numUnit].ShowInfo(); break;
                        case 3: army[numUnit].CleanPotato(); break;
                        case 4:
                            {
                                Action act;
                                if (Equals("Cavalry", army[numUnit].TypeStr) || Equals("Horse Archer", army[numUnit].TypeStr))
                                    act = StandUp; //ЕСЛИ ЮНИТ - КАВАЛЕРИЯ ИЛИ КОННЫЙ ЛУЧНИК
                                else act = NoStandUp;
                                act();
                                break;
                            }
                        case 5:
                            {
                                if (army.Count != 1)
                                {
                                    Console.WriteLine("Choose unit with whom duel.");
                                    Duel(army[numUnit], army[Program.ChooseArmyUnit(army, numUnit)], army, army);
                                }
                                else Console.WriteLine("Not enough troops");
                                break;
                            }
                        case 6:
                            {
                                if (army.Count != 1) army[numUnit].Salute(army[Program.ChooseArmyUnit(army, numUnit)]);
                                else Console.WriteLine("No unit to salute");
                                break;
                            }
                        default: break;
                    }
                    
                    Console.ReadLine();
                } while (ch != 7);
            }
              
            
        }

        
        public int CompareTo(object obj)
        {
            if (obj is Unit p)
            {
                if (this.IntType < p.IntType)
                    return -1;
                if (this.IntType > p.IntType)
                    return 1;
                return 0;
            }
            else
            {
                throw new Exception("Parametr should be of type Soldier");
            }
        }
       public static void Duel(Unit unit1, Unit unit2, List<Unit> army1, List<Unit> army2)
        {
            if (army1 != null && army2 != null && unit1 != null && unit2 != null)
            {
                float distance = 100;
                float curHp1 = unit1.HealthUnit;
                float curHp2 = unit2.HealthUnit;
                Console.WriteLine($"\nBattle between {unit1.Name} и {unit2.Name}");
                while (curHp1 > 0 && curHp2 > 0)
                {
                    if ((int)unit1.RangeUnit >= distance)                    //ЕСЛИ ЮНИТ ДОСТАЕТ ДО ВРАГА ТО НАНОСИТ
                    {                                                                       //ДАМАГ
                        curHp2 -= (1 - unit2.ArmourUnit) * unit1.AttackUnit;
#if !_INFOBATTLE
                        Console.WriteLine($"{unit1.Name} dealed {unit1.AttackUnit * (1 - unit2.ArmourUnit)} damage." +
                            $"{unit2.Name}'s Health - {curHp2}.");
#endif
                    }
                    else //ЕСЛИ НЕТ ТО СОКРАЩАЕТ ДИСТАНЦИЮ НА СВОЮ СКОРОСТЬ
                    {
                        distance -= unit1.SpeedUnit;
                        if (distance < 0) distance = 0;
#if !_INFOBATTLE
                        Console.WriteLine($"{unit1.Name} ran {unit1.SpeedUnit}.Distance now - {distance} metres.");
#endif
                    }

                    if (curHp2 <= 0) break;
                    if ((int)unit2.RangeUnit >= distance)
                    {
                        curHp1 -= (1 - unit1.ArmourUnit) * unit2.AttackUnit;
#if !_INFOBATTLE
                        Console.WriteLine($"{unit2.Name} dealed {unit2.AttackUnit * (1 - unit1.ArmourUnit)} damage." +
                        $"{unit1.Name}'s Health - {curHp1}.");
#endif
                    }                       
                    else
                    {
                        distance -= unit2.SpeedUnit;
                        if (distance < 0) distance = 0;
#if !_INFOBATTLE
                        Console.WriteLine($"{unit2.Name} ran {unit2.SpeedUnit}. Distance now - {distance}.");
#endif
                    }
                    
                }
                if (curHp1 <= 0)
                {
                    unit2.Win +=(unit2) => Program.DisplayYelMessage($"Victory of {unit2.Name}.");
                    unit1.Died += (unit1) => Program.DisplayRedMessage($"{unit1.TypeStr} {unit1.Name} died in battle ");// lambda expression
                    unit2.WhenWin(unit2);
                    unit1.WhenDied(unit1);
                    army1.Remove(unit1);
                }
                else if(curHp2 <= 0)
                {
                    unit1.Win += (unit1) => Program.DisplayYelMessage($"Victory of {unit1.Name}.");
                    unit2.Died += (unit2) => Program.DisplayRedMessage($"{unit2.TypeStr} {unit2.Name} died in battle ");//lambda expression
                    unit2.WhenDied(unit1);
                    unit1.WhenWin(unit2);
                    army2.Remove(unit2);
                }
                 Sound.PlaySound("G:\\kill.wav", new System.IntPtr(), Sound.PlaySoundFlags.SND_SYNC | Sound.PlaySoundFlags.SND_ASYNC); 
                // TO HEAR SOUND YOU NEED TO CHANGE FOLDER
                Thread.Sleep(1500);
            }
            else throw new Exception("Null exception ");

        }
    };
    public class TypeComparer : IComparer<Unit>
    {
        public int Compare(Unit x, Unit y)
        {
            if (x != null && y != null)
                return x.IntType.CompareTo(y.IntType);
            else return 0;
        }
    }

}
