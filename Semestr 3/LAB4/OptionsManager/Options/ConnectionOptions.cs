using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptionsManager.Options
{
     public class ConnectionOptions
    {
        public string DataSource { get; set; } = "MSI\\SQLEXPRESS";
        public string DataBase { get; set; } = "AdventureWorks2017";
        public string User { get; set; } = "Skava";
        public string Password { get; set; } = "1111";
    }
}
