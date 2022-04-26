using ISOB_2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISOB_2Client
{
    static class Program
    {
        static void Main()
        {
            var _client = new Client();
            var task = Task.Run(() => _client.Listen());
            try
            {
                _client.Register("user");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
            
            task.Wait();
        }
    }
}