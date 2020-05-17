using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

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
        public static Random rnd = new Random();
        protected float HealthUnit { get; set; }
        protected float AttackUnit { get; set; }
        protected float SpeedUnit { get; set; }
        protected float ArmourUnit { get; set; } 
        protected RangeUnit RangeUnit { get; set; }
        protected string TypeStr { get; set; }
        public Type IntType { get; set; }
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
        public void CleanPotato() =>   Console.WriteLine($"{TypeStr} {Name} cleaned potatoe.");
        public void ShowType() => Console.WriteLine($"Unit {TypeStr}.");
        public int GetIntType()
        {
            return (int)IntType;
        }
        public void ShowHealth() => Console.WriteLine($"Unit {TypeStr} {Name} has  {HealthUnit} hp");
        public void ShowAttack() => Console.WriteLine($"Unit {TypeStr} {Name} deal  {AttackUnit} damage");
        public void ShowSpeed() => Console.WriteLine($"Unit {TypeStr} {Name} has speed {SpeedUnit} m/s ");
        public void ShowRange() => Console.WriteLine($"Unit {TypeStr} {Name}has attack's range {(int)RangeUnit}  m. ");
        public void ShowArmour() => Console.WriteLine($"Unit {TypeStr} {Name} has  {ArmourUnit} % armour. ");

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
            if (army!= null)
            {
                Console.Clear();
                int ch;
                int chUnit;
                Console.WriteLine("Enter Action :\n1.Train \n2.Show choosen unit.\n3.Clean Potatoe" +
                       "\n4.Stand up on horse \n5.Duel.\n6.Exit");
                while (!int.TryParse(Console.ReadLine(), out ch) || ch < 1 || ch > 6)
                {
                    Console.Write("Input Error! Enter  unsigned integer  before 7 ");
                }
                switch (ch)
                {
                    case 1: army[numUnit].Train(); army[numUnit].ShowInfo(); break;
                    case 2: army[numUnit].ShowInfo(); break;
                    case 3: army[numUnit].CleanPotato(); break;
                    case 4:

                        if (army[numUnit].TypeStr.Equals("Cavalry") || army[numUnit].TypeStr.Equals("Horse Archer"))
                            //ЕСЛИ ЮНИТ - КАВАЛЕРИЯ ИЛИ КОННЫЙ ЛУЧНИК
                            ((IHorse)army[numUnit]).StandUp();
                        else Console.WriteLine("no Horse"); ; break;

                    case 5:                          
                            while (!int.TryParse(Console.ReadLine(), out chUnit) || chUnit < 0 ||
                            chUnit > army.Count || chUnit == numUnit)
                            {
                                Console.Write($"Input Error! Enter  unsigned integer  before {army.Count} ");
                            }
                        Duel(army[numUnit], army[chUnit], army, army);
                            break;
                    default: break;
                }
            }
            
        }
        public int CompareTo(object obj)
        {
            Unit p = obj as Unit;
            if (p != null)
            {
                if (this.IntType < p.IntType)
                    return -1;
                if (this.IntType > p.IntType)
                    return 1;
                return 0;
            }
            else
            {
                throw new Exception("Parametr should be of type Unit");
            }
        }
       public static void Duel(Unit unit1, Unit unit2, List<Unit> army1, List<Unit> army2)
        {
            if (army1 != null && army2 != null && unit1 != null && unit2 != null)
            {
                float distance = 100;
                float curHp1 = unit1.HealthUnit;
                float curHp2 = unit2.HealthUnit;
                Console.WriteLine($"Battle between {unit1.Name} и {unit2.Name}");
                while (curHp1 > 0 && curHp2 > 0)
                {
                    if ((int)unit1.RangeUnit >= distance)                    //ЕСЛИ ЮНИТ ДОСТАЕТ ДО ВРАГА ТО НАНОСИТ
                    {                                                                       //ДАМАГ
                        curHp2 -= (1 - unit2.ArmourUnit) * unit1.AttackUnit;
                        Console.WriteLine($"{unit1.Name} dealed {unit1.AttackUnit * (1 - unit2.ArmourUnit)} damage." +
                            $"{unit2.Name}'s Health - {curHp2}.");
                    }
                    else //ЕСЛИ НЕТ ТО СОКРАЩАЕТ ДИСТАНЦИЮ НА СВОЮ СКОРОСТЬ
                    {
                        distance -= unit1.SpeedUnit;
                        if (distance < 0) distance = 0;
                        Console.WriteLine($"{unit1.Name} ran {unit1.SpeedUnit}.Distance now - {distance} metres.");
                    }
                    if (curHp2 <= 0) break;
                    if ((int)unit2.RangeUnit >= distance)
                    {
                        curHp1 -= (1 - unit1.ArmourUnit) * unit2.AttackUnit;
                        Console.WriteLine($"{unit2.Name} dealed {unit2.AttackUnit * (1 - unit1.ArmourUnit)} damage." +
                            $"{unit1.Name}'s Health - {curHp1}.");
                    }                       
                    else
                    {
                        distance -= unit2.SpeedUnit;
                        if (distance < 0) distance = 0;
                        Console.WriteLine($"{unit2.Name} ran {unit2.SpeedUnit}. Distance now - {distance}.");
                    }
                    
                }
                if (curHp1 <= 0)
                {
                    Console.WriteLine($"Victory of {unit2.Name}.");
                    army1.Remove(unit1);
                }
                else
                {
                    Console.WriteLine($"Victory of {unit1.Name}.");
                    army2.Remove(unit2);
                }
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
