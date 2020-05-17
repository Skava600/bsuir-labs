using System;
using System.Collections.Generic;
using System.Text;

namespace lab3
{
    
    //*****КЛАСС НОВОБРАНЦА***************
    abstract class Unit : Person
    {
        protected double healthUnit, attackUnit, speedUnit, armourUnit; 
        protected int rangeUnit;
        protected string type;
        protected Type intType { get; set; }
        protected double Speed
        {
            get
            { 
                return (1 - armourUnit/2); 
            }
            set 
            { 
                if (value > 0) { speedUnit = value; } 
            }
        }

        public Unit(double a, double b, double c, double g, string d) : base()
        {
            healthUnit = a;
            attackUnit = b;
            armourUnit = c;
            type = d;
            speedUnit = g * Speed;
            ID = SetId();
        }
        public void CleanPotato() =>   Console.WriteLine($"{type} почистил картошку.");

        public void ShowType() => Console.WriteLine($"Юнит {type}.");
        public int GetIntType()
        {
            return (int)intType;
        }
        public void ShowHealth() => Console.WriteLine($"Я имею  {healthUnit} здоровья");
        public void ShowAttack() => Console.WriteLine($"Я могу нанести  {attackUnit} дамага");
        public void ShowSpeed() => Console.WriteLine($"Я передвигаюсь со скоростью {speedUnit} м/c ");
        public void ShowRange() => Console.WriteLine($"Моя дальность атаки - {rangeUnit} м. ");
        public  override void ShowInfo()
        {
            base.ShowInfo();
            ShowType();
            ShowHealth();
            ShowAttack();
            ShowSpeed();
            ShowRange();
            ShowID();
        }
        public abstract void Train();//ЮНИТ ТРЕНИРУЕТСЯ И УВЕЛИЧИВАЕТ ХАР-КИ В ЗАВИСИМОСТИ ОТ ТИПА (ДО ОПР. МАКС ЧИСЛА ХАР-КИ)
        public virtual void ShowActionsUnit(Army army, int numUnit)
        {
            //Console.ReadLine();
            Console.Clear();
            int ch;
            Console.WriteLine("Введите действие:\n1.Тренировка \n2.Вывести выбранного юнита.\n3.Почистить картошку " +
                   "\n4.Встать на дыбы\n5.выход");
            while (!int.TryParse(Console.ReadLine(), out ch) || ch < 1 || ch > 5)
            {
                Console.Write("Ошибка ввода! Введите целое  положительное число до 6 ");
            }
            switch (ch)
            {
                case 1: army[numUnit].Train();army[numUnit].ShowInfo(); break;
                case 2: army[numUnit].ShowInfo(); break;
                case 3: army[numUnit].CleanPotato();break;
                case 4: if (army[numUnit].type.Equals("Кавалерия") || army[numUnit].type.Equals("Конный Лучник"))
                        //ЕСЛИ ЮНИТ - КАВАЛЕРИЯ ИЛИ КОННЫЙ ЛУЧНИК
                            ((IHorse)army[numUnit]).StandUp();
                        else Console.WriteLine("Нет лошади");break;
            }
        } 
        
        

    };
}
