using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Converter;
using System.IO;

namespace OptionsManager
{
    public class OptionsManager<T> where T: new()
    {
        public string logs;
        T defaultOptions = new T();
        T jsonOptions;
        T xmlOptions;
            

        bool isJsonConnected = false;
        bool isXmlConnected = false;

        public OptionsManager(string path, IConverter converter)
        {
            string options;

            try
            {
                using (StreamReader sr = new StreamReader($"{path}\\appsettings.json"))
                {
                    options = sr.ReadToEnd();

                }

                jsonOptions = converter.DeserializeJson<T>(options);
                isJsonConnected = true;
                logs+=("appsettings.json is loaded.");

            }
            catch (Exception ex)
            {


                isJsonConnected = false;
                logs+=ex.Message;
            }

            try
            {
                using (StreamReader sr = new StreamReader($"{path}\\config.xml"))
                {
                    options = sr.ReadToEnd();

                }

                xmlOptions = converter.DeserializeXML<T>(options);
                isXmlConnected = true;
                logs+="\nconfig.xml is loaded.";
            }
            catch (Exception ex)
            {

                isXmlConnected = false;
                logs+=ex.Message;
            }

            if (!isJsonConnected && !isXmlConnected)
            {
                defaultOptions = new T();
                logs+="Default options is used.";
            }
        }

        object FindOption<T>(object options)
        {
            if (typeof(T) == defaultOptions.GetType())
            {
                return options;
            }

            try
            {
                return options.GetType().GetProperty(typeof(T).Name).GetValue(options, null);
            }
            catch
            {
                logs+=("FindOption didn't find the needed option and throw a NotImplementedException.");
                throw new NotImplementedException();
            }
        }

        public object GetOptions<T>()
        {
            if (isJsonConnected)
            {
                logs+=("\nJson configuration");
                return FindOption<T>(jsonOptions);
            }
            else if (isXmlConnected)
            {
                logs+="\nXML configuration";
                return FindOption<T>(xmlOptions);
            }

            else
            {
                logs+=("\nDefault configuration");
                return FindOption<T>(defaultOptions);
            }
        }
    }
}
