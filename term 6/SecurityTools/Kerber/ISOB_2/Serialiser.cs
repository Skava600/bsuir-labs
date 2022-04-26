using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace ISOB_2
{
    public class Serialiser <T>
    {
        public static byte[] Serialise(T source)
        {
            var json = JsonSerializer.Serialize<T>(source);
            return Encoding.UTF8.GetBytes(json);
        }
        public static T Deserialise(byte[] source)
        {
            var json = Encoding.UTF8.GetString(source);
            return JsonSerializer.Deserialize<T>(json);
        }
    }
}
