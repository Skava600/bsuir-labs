using Converter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models;
using DataAccessLayer;
using OptionsManager.Options;

namespace ServiceLayer
{
    public class ServiceLayer:  IServiceLayer
    {
        public DataAccessLayer.DataAccessLayer dataAccessLayer;
        public IConverter Converter;
        public ServiceLayer(IConverter converter, ConnectionOptions options)
        {
            dataAccessLayer = new DataAccessLayer.DataAccessLayer(converter, options);
        }
        public Human GetHuman(int id)
        {
            Person person = dataAccessLayer.GetPerson(id);
            Human personInfo = GetInfo(person);
            return personInfo;
        }
        Human GetInfo(Person person)
        {
            Human human = new Human();
            int id = person.BusinessEntityID;

            human.Address = dataAccessLayer.GetPersonOpts<Address>(id);
            human.BusinessEntityAddress = dataAccessLayer.GetPersonOpts<BusinessEntityAddress>(id);
            human.Password = dataAccessLayer.GetPersonOpts<Password>(id);
            human.EmailAdress = dataAccessLayer.GetEmailAddress(id);
            human.Person = dataAccessLayer.GetPersonOpts<Person>(id);
            human.PersonPhone = dataAccessLayer.GetPersonOpts<PersonPhone>(id);
            return human;
        }
        public List<Human> GetHumanList(int count)
        {
            List<Human> list = new List<Human>();
            Human human;
            for (int id = 1; id < count; id++)
            {
                human = GetHuman(id);
                list.Add(human);
            }
            return list;
        }
        public List<Human> GetHumanList(int startIndex, int count)
        {
            List<Human> list = new List<Human>();
            Human human;
            for (int id = startIndex; id < startIndex + count; id++)
            {
                human = GetHuman(id);
                list.Add(human);
            }
            return list;
        }
    }
}
