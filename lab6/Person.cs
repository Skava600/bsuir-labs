using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace lab3
{
    public class Person
    {
        public string[] Names = new string[] {"Joe", " John", "Giorno", "Georgy", "Vladimir", "Wladzislaw", "Stanislaw",
            "Nikita", "Sigizmund", "Robert", "Ragnar", "Arthur" };
        public int Age { get; set; }
        public string Name { get; set; }
        protected  int ID { get; set; }
        public Person()
        {
            Random rnd = new Random();
            Age = rnd.Next(18, 50);
            ID = SetId();
            Name = Names[rnd.Next(0, 12)];
        }
        protected static int SetId()
        {
            Random rnd = new Random();
            return rnd.Next(100000, 999999);
        }
        public virtual void  ShowInfo()
        {
            ShowName();
            ShowAge();
            ShowID();
        }
        public void ShowName()=>     Console.WriteLine($"My Name is {Name}");
        public void ShowAge() => Console.WriteLine($"I am {Age} years old.");

        public void ShowID() => Console.WriteLine($"My  ID is - {ID} ");

    }
}
