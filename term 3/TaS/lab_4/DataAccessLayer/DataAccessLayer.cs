using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Converter;
using System.Transactions;
using DataAccessLayer.Models;
using OptionsManager.Options;
using Microsoft.Win32;

namespace DataAccessLayer
{
    public class DataAccessLayer: IDataAccessLayer
    {
        SqlConnection connection;
        IConverter converter;
        public DataAccessLayer(IConverter converter, ConnectionOptions opt)
        {
            this.converter = converter;
            string connectionString = $@"Data Source={opt.DataSource}; Database={opt.DataBase}; User={opt.User}; Password={opt.Password}; Integrated Security=False";
            using (TransactionScope scope = new TransactionScope())
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
                scope.Complete();
            }

        }

        public Person GetPerson(int personID)
        {
            Person person = GetPersonOpts<Person>(personID);
            return person;
        }
        public T GetPersonOpts<T>(int id) where T : new()
        {
            SqlCommand command = new SqlCommand($"Get{typeof(T).Name}", connection)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };
            command.Parameters.Add(new SqlParameter("id", id));
            using (TransactionScope scope = new TransactionScope())
            {
                SqlDataReader reader = command.ExecuteReader();
                List<T> opt = Map<T>(reader, converter);
                scope.Complete();
                if (opt == null)
                {
                    return new T();
                }
                else
                {
                    return opt.First();
                }
            }
        }
        public Email GetEmailAddress(int id)
        {
            SqlCommand command = new SqlCommand("GetEmailAddress", connection)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };
            command.Parameters.Add(new SqlParameter("id", id));
            using (TransactionScope scope = new TransactionScope())
            {
                List<Email> ans = Map<Email>(command.ExecuteReader(), converter);
                scope.Complete();
                if (ans.Count == 0)
                {
                    return new Email();
                }
                else
                {
                    return ans.First();
                }
            }
        }

        List<T> Map<T>(SqlDataReader reader, IConverter parser)
        {
            List<Dictionary<string, object>> parsed = Parse(reader);
            List<T> ans = new List<T>();
            foreach (Dictionary<string, object> dict in parsed)
            {
                ans.Add(parser.Map<T>(dict));
            }
            return ans;
        }

        List<Dictionary<string, object>> Parse(SqlDataReader reader)
        {
            List<Dictionary<string, object>> ans = new List<Dictionary<string, object>>();
            while (reader.Read())
            {
                Dictionary<string, object> dict = new Dictionary<string, object>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string name = reader.GetName(i);
                    object val = reader.GetValue(i);
                    dict.Add(name, val);
                }
                ans.Add(dict);
            }
            reader.Close();
            return ans;
        }
        public void Log(DateTime date, string message)
        {
            SqlCommand command = new SqlCommand("Log", connection)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };
            command.Parameters.Add(new SqlParameter("Date", date));
            command.Parameters.Add(new SqlParameter("Message", message));

            using (TransactionScope scope = new TransactionScope())
            {
                command.ExecuteNonQuery();
                scope.Complete();
            }
        }
        
    }
}
