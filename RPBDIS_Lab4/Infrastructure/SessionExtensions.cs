using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace RPBDIS_Lab4.Infrastructure
{
    public static class SessionExtensions
    {
        //Запись параметризованного объекта  в сессию
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }
        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonConvert.DeserializeObject<T>(value);
        }
        public static void Set(this ISession session, string key, Dictionary<string, string> dictionary)
        {
            session.SetString(key, JsonConvert.SerializeObject(dictionary));
        }
        //Считывание объекта типа Dictionary<string, string> из сессии
        public static Dictionary<string, string> Get(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonConvert.DeserializeObject<Dictionary<string, string>>(value);
        }
    }
}