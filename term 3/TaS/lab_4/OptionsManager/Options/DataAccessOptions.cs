using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptionsManager.Options
{
    public class DataAccessOptions
    {
        public SendingOptions SendingOptions { get; set; } = new SendingOptions();
        public LoggingOptions LoggingOptions { get; set; } = new LoggingOptions();
        public ConnectionOptions ConnectionOptions { get; set; } = new ConnectionOptions();
      
    }
}
