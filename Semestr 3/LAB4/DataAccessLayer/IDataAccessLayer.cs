using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models;

namespace DataAccessLayer
{
    public interface IDataAccessLayer
    {
        Person GetPerson(int id);
        T GetPersonOpts<T>(int id) where T : new();
    }
}
