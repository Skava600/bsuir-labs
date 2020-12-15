using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models;
using Converter;
using OptionsManager;
using XMLGenerator;
using OptionsManager.Options;
using Logger;

namespace LAB4
{
    class Program
    {
        static void Main(string[] args)
        {
            IConverter converter = new Converter.Converter();
            string based = AppDomain.CurrentDomain.BaseDirectory;
            OptionsManager<DataAccessOptions> optionsManager = new OptionsManager<DataAccessOptions>(based, converter);
            LoggingOptions loggingOptions = optionsManager.GetOptions<LoggingOptions>() as LoggingOptions;
            SendingOptions sendingOptions = optionsManager.GetOptions<SendingOptions>() as SendingOptions;
            ConnectionOptions connectionOptions = optionsManager.GetOptions<ConnectionOptions>() as ConnectionOptions;
            ServiceLayer.ServiceLayer sl = new ServiceLayer.ServiceLayer(converter, connectionOptions);
            Logger.Logger logger = new Logger.Logger(connectionOptions, converter);
            List<Human> people;
            logger.Log(optionsManager.logs);
            people = sl.GetHumanList(100);
            XmlGenerator generator = new XmlGenerator(sendingOptions);
            generator.CreateXML(people);
        }
    }
}