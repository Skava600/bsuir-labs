using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using LAB2;
using LAB2.Options;
namespace LAB2.Parsers
{
   
        class OptionsManager
        {
            ETLOptions defaultOptions;
            ETLJSONOptions jsonOptions;
            ETLXMLOptions xmlOptions;
        

            bool isJsonConnected = false;
            bool isXmlConnected = false;
            
            public OptionsManager(string path, Logger logger)
            {
                string options;
               
                try
                {
                    using (StreamReader sr = new StreamReader($"{path}\\appsettings.json"))
                    {
                        options = sr.ReadToEnd();
                    
                    }
                
                   jsonOptions = new ETLJSONOptions(options);
                   isJsonConnected = true;
                   logger.Log("appsettings.json is loaded.", false);

                }
                catch (Exception ex)
                {
               

                    isJsonConnected = false;
                    logger.Log(ex.Message, false);
                }

            try
            {
                using (StreamReader sr = new StreamReader($"{path}\\config.xml"))
                {
                    options = sr.ReadToEnd(); 
                    
                }

                xmlOptions = new ETLXMLOptions(options);
                isXmlConnected = true;
                logger.Log("config.xml is loaded.", true);
            }
            catch (Exception ex)
            {
               
                isXmlConnected = false;
                logger.Log(ex.Message, true);
            }

            if (!isJsonConnected && !isXmlConnected)
                {
                    defaultOptions = new ETLOptions();
                    logger.Log("Default options is used.", true);
                }
            }

            Options.Options FindOption<T>(ETLOptions options, Logger logger)
            {
                if (typeof(T) == typeof(ETLOptions))
                {
                    return options;
                }

                try
                {
                    return options.GetType().GetProperty(typeof(T).Name).GetValue(options, null) as Options.Options;
                }
                catch
                {
                    logger.Log("FindOption didn't find the needed option and throw a NotImplementedException.", true);
                    throw new NotImplementedException();
                }
            }

            public Options.Options GetOptions<T>(Logger logger)
            {
                if (isJsonConnected)
                {
                    logger.Log("Json configuration", true);
                    return FindOption<T>(jsonOptions, logger);
                }
                else if (isXmlConnected)
                {
                    logger.Log("XML configuration", true);
                    return FindOption<T>(xmlOptions, logger);
                }
                
                else
                {
                       logger.Log("Default configuration", true);
                        return FindOption<T>(defaultOptions, logger);
                }
            }
        
    }
    

}
