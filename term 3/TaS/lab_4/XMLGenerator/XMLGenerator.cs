using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;
using System.Xml.Serialization;
using DataAccessLayer.Models;
using OptionsManager.Options;

namespace XMLGenerator
{
    
        public class XmlGenerator
        {
        SendingOptions sendingOptions;
            public XmlGenerator(SendingOptions sendingOptions)
            {
            this.sendingOptions = sendingOptions;
                Directory.CreateDirectory(this.sendingOptions.TargetDirectory);
            }
            public void CreateXML(List<Human> list)
            {
                XmlSerializer xml = new XmlSerializer(list.GetType());
                using (FileStream fs = new FileStream(Path.Combine(sendingOptions.TargetDirectory, "list.xml"), FileMode.Create))
                {
                    xml.Serialize(fs, list);
                }
            }
        }

}
