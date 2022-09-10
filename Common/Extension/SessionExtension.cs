using System.Text;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Common.Extension
{
    public static class SessionExtension
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            var valueString = JsonConvert.SerializeObject(value);
            session.Set(key, Encoding.UTF8.GetBytes(valueString));
        }

        public static T? Get<T>(this ISession session, string key)
        {
            byte[] keyBytes;
            var result = session.TryGetValue(key, out keyBytes);

            return result ? JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(keyBytes)) : default;
        }
    }
}
