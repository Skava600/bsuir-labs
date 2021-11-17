using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models;

namespace ServiceLayer
{
     public interface IServiceLayer
     {
        List<Human> GetHumanList(int count);
     }
}
