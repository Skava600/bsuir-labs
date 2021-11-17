using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converter
{
    public interface IConverter
    {
        T DeserializeJson<T>(string json) where T : new();
        T DeserializeXML<T>(string xml) where T : new();
        T Map<T>(Dictionary<string, object> dictionary);
    }
}
