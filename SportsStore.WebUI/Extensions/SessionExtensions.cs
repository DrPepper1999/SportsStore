using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using SportsStore.Domain.Entities;

namespace SportsStore.WebUI.Extensions
{
    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);

            if (value != null)
            {
                return JsonSerializer.Deserialize<T>(value);
            }
            else return default(T);
        }
    }
}
