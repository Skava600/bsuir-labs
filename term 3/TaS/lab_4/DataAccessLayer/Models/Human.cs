using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Human
    {
        public Person Person { get; set; }
        public Address Address { get; set; }
        public BusinessEntityAddress BusinessEntityAddress { get; set; }
        public Password Password { get; set; }
        public PersonPhone PersonPhone { get; set; }
        public Email EmailAdress { get; set; }

        public Human()
        {

        }
        public Human(Person person, Password password, PersonPhone personPhone, Address address, BusinessEntityAddress businessEntityAddress, Email emailAdress)
        {
            Person = person;
            Address = address;
            BusinessEntityAddress = businessEntityAddress;
            Password = password;
            PersonPhone = personPhone;
            EmailAdress = emailAdress;
        }
    }
}
