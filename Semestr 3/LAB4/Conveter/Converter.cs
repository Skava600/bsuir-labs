using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Converter
{
    public class Converter: IConverter
    {
        public T DeserializeJson<T>(string json) where T : new()
        {
            List<string> list = JsonParse(json);
            return Deserialize<T>(list);
        }

        public T DeserializeXML<T>(string XML) where T : new()
        {
            List<string> list = XMLParse(XML, true);
            return Deserialize<T>(list);
        }


        public  T Deserialize<T>(List<string> list) where T : new()
        {
            T ans = new T();
            Type type = typeof(T);

            string key , value;

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
                    using (StreamWriter writer = new StreamWriter(@"C:\FileWatchers\TargetDirectory\chel.txt", true))
                    {
                        writer.WriteLine($"if1 {typeof(T)} value - {value} key - {key}");
                        writer.Flush();
                    }
                    PropertyInfo info = type.GetProperty(key);
                    try
                    {

                        info.SetValue(ans, typeof(Converter).GetMethod("DeserializeJson")
                       .MakeGenericMethod(new Type[] { info.PropertyType }).Invoke(null, new object[] { value }));
                    }
                    catch (Exception ex)
                    {
                        using (StreamWriter writer = new StreamWriter(@"C:\FileWatchers\TargetDirectory\Filelog.txt", true))
                        {
                            writer.WriteLine(ex + " error in getproperty- " + DateTime.Now.ToString("yyyy.mm.dd HH:mm:ss"));
                            writer.Flush();
                        }
                    }

                }
                else if (simple.IsMatch(option))
                {
                    match = simple.Match(option);

                    key = match.Groups[1].Value;
                    value = match.Groups[2].Value;
                    using (StreamWriter writer = new StreamWriter(@"C:\FileWatchers\TargetDirectory\chel.txt", true))
                    {
                        writer.WriteLine($"if2 {typeof(T)}value - {value} key - {key}");
                        writer.Flush();
                    }

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

            List<string> list = new List<string>();
            StringBuilder line = new StringBuilder();
            StringBuilder tag = new StringBuilder();
            StringBuilder value = new StringBuilder();
            List<string> tags = new List<string>();
            string baseTag = "";
            bool isTag = false;

            if (GetNextTag(xml, 0) == "root")
            {
                xml = new Regex(@"<root>(.*)</root>", RegexOptions.Singleline)
                    .Match(xml)
                    .Groups[1].ToString();
            }

            foreach (char ch in xml)
                if (!char.IsWhiteSpace(ch))
                {
                    if (ch == '<')
                    {
                        isTag = true;
                        continue;
                    }
                    if (ch == '>')
                    {
                        isTag = false;
                        if (tag[0] == '/')
                        {
                            string curTag = tag.ToString().Trim(new char[] { '/' });
                            if (value.ToString() != "")
                            {
                                if (tags.Contains(baseTag))
                                {
                                    line.Append($",\"{curTag}\":{value}");
                                    tags.Remove(curTag);
                                    value.Clear();
                                }
                                else
                                {
                                    for (int i = 0; tags[i] != curTag; i++)
                                    {
                                        line.Append($"\"{tags[i]}\":{{");
                                        baseTag = tags[i];
                                    }
                                    line.Append($"\"{curTag}\":{value}");
                                    tags.Remove(curTag);
                                    value.Clear();
                                }
                            }
                            else
                            {
                                tags.Remove(curTag);
                                line.Append("}");
                            }
                            if (tags.Count == 0)
                            {
                                list.Add(line.ToString());
                                line.Clear();
                            }
                        }
                        else
                        {
                            tags.Add(tag.ToString());
                        }
                        tag.Clear();
                        continue;
                    }
                    if (isTag)
                    {
                        tag.Append(ch);
                    }
                    else
                    {
                        value.Append(ch);
                    }
                }
            return list;
        }
        static string GetNextTag(string str, int i)
        {
            bool isTag = false;
            char ch;
            StringBuilder tag = new StringBuilder();
            for (; i < str.Length; i++)
            {
                ch = str[i];
                if (ch == '<')
                {
                    isTag = true;
                }
                else
                {
                    if (ch == '>')
                    {
                        break;
                    }
                    if (isTag)
                    {
                        tag.Append(ch);
                    }
                }
            }
            return tag.ToString();
        }
        public T Map<T>(Dictionary<string, object> dictionary)
        {
            T ans = (T)Activator.CreateInstance(typeof(T));
            foreach (KeyValuePair<string, object> pair in dictionary)
            {
                if (pair.Value.GetType() != typeof(DBNull))
                {
                    SetMemberValue(ans, pair.Key, pair.Value);
                }
                else
                {
                    SetMemberValue(ans, pair.Key, null);
                }
            }
            return ans;
        }
        private void SetMemberValue<T>(T obj, string key, object value)
        {
            Type type = typeof(T);
            if (type.GetProperty(key) != null)
            {
                PropertyInfo info = type.GetProperty(key);
                info.SetValue(obj, value);
            }
            else
            {
                if (type.GetField(key) != null)
                {
                    FieldInfo info = type.GetField(key);
                    info.SetValue(obj, value);
                }
                else
                {
                    throw new Exception($"{obj.GetType()} type does not contain member with {key} key");
                }
            }
        }
    }
}
