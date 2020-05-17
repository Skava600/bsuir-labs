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
            ShowID();
        }
        public void ShowName()=>     Console.WriteLine($"Мое имя {Name}");

        public void ShowID() => Console.WriteLine($"Мое ID  - {ID} ");

    }
}
