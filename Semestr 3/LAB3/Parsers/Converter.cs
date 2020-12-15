using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Threading.Tasks;
using LAB2.Options;

namespace LAB2.Parsers
{
    class Converter
    {
        public static T DeserializeJson<T>(string json) where T : new()
        {
            List<string> list = JsonParse(json);
            return Deserialize<T>(list);
        }

        public static T DeserializeXML<T>(string XML) where T : new()
        {
            List<string> list = XMLParse(XML, true);
            return Deserialize<T>(list);
        }


        public static T Deserialize<T>(List<string> list) where T : new()
        {
            T ans = new T();
            Type type = typeof(T);
            foreach (string obj in list)
            {
                using (StreamWriter writer = new StreamWriter(@"C:\FileWatchers\TargetDirectory\chel.txt", true))
                {
                    writer.WriteLine($"{typeof(T)} obj - {obj}");
                    writer.Flush();
                }
            }
            string key = "", value = "";

            Regex complex = new Regex(@"(\w+)\s*:\s*{(.*)}", RegexOptions.Singleline);
            Regex simple = new Regex(@"(\w+)\s*:\s*(.*)", RegexOptions.Singleline);
            Match match;

            foreach (string option in list)
            {
                if (complex.IsMatch(option))
                {
                    match = complex.Match(option);

                    key = match.Groups[1].Value;
                    value = match.Groups[2].Value;
                    
                       
                    info.SetValue(ans, typeof(Converter).GetMethod("DeserializeJson")
                    MakeGenericMethod(new Type[] { info.PropertyType }).Invoke(null, new object[] { value }));
                    
                   
                   
                }
                else if (simple.IsMatch(option))
                {
                    match = simple.Match(option);

                    key = match.Groups[1].Value;
                    value = match.Groups[2].Value;
                   
                    
                    PropertyInfo info = type.GetProperty(key);

                    if (info.PropertyType.IsEnum)
                    {
                        info.SetValue(ans, Enum.Parse(info.PropertyType, value));
                    }
                    else
                    {
                        info.SetValue(ans, Convert.ChangeType(value, info.PropertyType));
                    }
                }
            }
            return ans;
        }

        public static List<string> JsonParse(string json)
        {
            json = json.Trim(new char[] { ' ', '{', '}' });

            List<string> list = new List<string>();
            int brackets = 0;
            StringBuilder option = new StringBuilder();

            foreach (char symbol in json)
            {
                if (char.IsLetterOrDigit(symbol) || char.IsPunctuation(symbol))
                {
                    if (symbol == ',' && brackets == 0)
                    {
                        list.Add(option.ToString());
                        option.Clear();
                    }
                    else if (symbol == '{')
                    {
                        option.Append(symbol);
                        brackets++;
                    }
                    else if (symbol == '}')
                    {
                        option.Append(symbol);
                        brackets--;
                    }
                    else
                    {
                        option.Append(symbol);
                    }
                }
            }
            if (option.Length != 0)
                list.Add(option.ToString());
            return list;
        }


        static List<string> XMLParse(string xml, bool trim)
        {
            xml = xml.Trim(new char[] { '\n', '\t', '\r', ' ' });
            string name;
            Match match;
            name = GetNextTag(xml, 0);
            if (trim)
            {
                Regex clean = new Regex($"^<{name}>(.*)</{name}>$", RegexOptions.Singleline);
                match = clean.Match(xml);
                if (match.Success)
                {
                    xml = match.Groups[1].Value;
                }
            }
            List<string> objects = new List<string>();
            bool isTag = false;
            StringBuilder obj = new StringBuilder();
            string tag = "";
            string value = "";
            int count = 0;
            foreach (char ch in xml)
            {
                if (ch != '\t' && ch != '\r' && ch != '\n')
                {
                    if (ch == '<')
                    {
                        isTag = true;
                        count++;
                        continue;
                    }
                    if (ch == '>')
                    {
                        isTag = false;
                        if (count == 2)
                        {
                            tag = tag.Remove(tag.IndexOf('/'), tag.Length - tag.IndexOf('/'));
                            obj.Append(tag + ':' + value);
                            count = 0;
                            tag = "";
                            value = "";
                            objects.Add(obj.ToString());
                            obj.Clear();
                        }
                        continue;
                    }
                    if (isTag)
                    {
                        tag += ch;
                    }
                    else
                    {
                        value += ch;
                    }
                }
            }
            return objects;
        }

            static string GetNextTag(string xml, int startIndex)
        {
            StringBuilder tag = new StringBuilder("");
            bool isTag = false;
            for (int i = startIndex; i < xml.Length; i++)
            {
                if (xml[i] == '<')
                {
                    isTag = true;
                    continue;
                }
                else if (xml[i] == '>')
                {
                    return tag.ToString();
                }
                else if (isTag)
                {
                    tag.Append(xml[i]);
                }
            }
            throw new Exception("Tag wasn't found.");
        }
    }
}
