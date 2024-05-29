using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Y.Infrastructure.Library.Core.Extensions;
using Y.Infrastructure.Library.Core.Helper;

namespace Y.Infrastructure.Library.Core.CacheFactory.Extension
{
    public static class RedisHelperExtensions
    {

        /// <summary>
        /// 将实体类转为HashEntry数组
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static HashEntry[] ToHashEntriesFromEntity(this object entity)
        {
            PropertyInfo[] properties = entity.GetType().GetProperties();
            return properties.Select(propertype =>
                                 new HashEntry(propertype.Name, propertype.GetValue(entity).ToString()))
                             .ToArray();
        }

        /// <summary>
        /// 将HashEntry数组转化为实体类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="hashEntries"></param>
        /// <returns></returns>
        public static T ConvertFromRedisToEntity<T>(this HashEntry[] hashEntries)
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            var obj = Activator.CreateInstance(typeof(T));
            foreach (var property in properties)
            {
                HashEntry entry = hashEntries.FirstOrDefault(g => g.Name.ToString().Equals(property.Name));
                if (entry.Equals(new HashEntry())) { continue; }
                property.SetValue(obj, Convert.ChangeType(entry.Value.ToString(), property.PropertyType));
            }
            return (T)obj;
        }

        /// <summary>
        /// 将字典的KeyValue的数据转成HashEntry 字典可以通过来查询list.Select(b =>b).ToDictionary(k=>k.key,v=>v.value);
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static HashEntry[] ToHashEntriesFromDic(this Dictionary<string, string> list)
        {
            return list.Select(b => new HashEntry(
                b.Key,
                b.Value
                )).ToArray();
        }
        public static HashEntry[] ToHashEntriesFromDic(this Dictionary<string, int> list)
        {
            return list.Select(b => new HashEntry(
                b.Key,
                b.Value.ToString()
                )).ToArray();
        }
        public static HashEntry[] ToHashEntriesFromDic(this Dictionary<int, string> list)
        {
            return list.Select(b => new HashEntry(
                b.Key,
                b.Value
                )).ToArray();
        }

        /// <summary>
        /// 将HashEntry数组转化为Dictionary
        /// </summary>
        /// <param name="hashEntries"></param>
        /// <returns></returns>
        public static Dictionary<string, string> ConvertFromRedisToDic(this HashEntry[] hashEntries)
        {
            return hashEntries.Select(b => b).ToDictionary(
                    k => k.Name.ToString(),
                    v => v.Value.ToString()
                );
        }
        public static Dictionary<string, int> ConvertFromRedisToDic2(this HashEntry[] hashEntries)
        {
            return hashEntries.Select(b => b).ToDictionary(
                    k => k.Name.ToString(),
                    v => (int)v.Value
                );
        }
        public static Dictionary<int, string> ConvertFromRedisToDic3(this HashEntry[] hashEntries)
        {
            return hashEntries.Select(b => b).ToDictionary(
                    k => Convert.ToInt32(k.Name),
                    v => v.Value.ToString()
                );
        }
    }
}
