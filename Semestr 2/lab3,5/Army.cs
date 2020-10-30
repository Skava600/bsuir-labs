using System;
using System.Collections.Generic;
using System.Text;

namespace lab3
{
    //*****Перечисления*************************************************
    enum Type
    {
        HorseMan = 1,
        Infantry,
        Archer,
        HorseArcher
    }
    enum RangeUnits
    {
        HorseMan = 0,
        Infantry = HorseMan,
        HorseArcher = 100,
        Archer = HorseArcher
    }
    //***********************************************************

    //******ИНДЕКСАТОР*********************************************
    class Army
    {
        protected Unit[] army;
        public static int size;
        protected int MAXSIZE = 20;
        public Army()
        {
            army = new Unit[MAXSIZE];
            size = 0;
        }
        public Unit this[int index]
        {
            get
            {
                return army[index];
            }
            set
            {
                if (size == MAXSIZE)
                {
                    Console.WriteLine("Достигнут лимит");
                    return;
                }

                size++;
                army[index] = value;
            }
        }
        public void ShowArmyTypes()
        {
            for (int i = 0; i < size; i++)
            {
                Console.Write($"{i}.");
                this[i].ShowType();
            }
            Console.WriteLine("1 - Всадник\n2 - Пехота\n3 - Лучник \n4 - Конный лучник");
            for (int i = 0; i < size; i++)
            {
                Console.Write(this[i].GetIntType());
            }
        }




    };
    //***********************************************************8
}
