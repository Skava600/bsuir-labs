using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OptionsManager.Options;
using Converter;
namespace Logger
{
    public class Logger
    {
        public ConnectionOptions logOptions;
        DataAccessLayer.DataAccessLayer dal;
        public Logger()
        {

        }
        public Logger(ConnectionOptions options, IConverter converter)
        {
            logOptions = options;
            dal = new DataAccessLayer.DataAccessLayer(converter, options);
        }
      
        public void Log(string msg)
        {
           
            dal.Log(DateTime.Now, msg);
        
        }
       
    }
}
