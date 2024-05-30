using Microsoft.AspNetCore.Http;
using System.Text.Json;


namespace CeylonWellness.Web.Extensions
{
    public static class SessionExtensions
    {
        public static T? Get<T>(this ISession session, string key)
        {
            var data = session.GetString(key);
            return data == null ? default(T) : JsonSerializer.Deserialize<T>(data);
        }

        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }
    }
}
