using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Y.Infrastructure.Library.Core.Extensions
{
    public static class DictionaryExtensions
    {
        public static string ToJson<Tkey, TValue>(this Dictionary<Tkey, TValue> dict)
        {
            return JsonConvert.SerializeObject(dict);
        }

        /// <summary>
        /// 根据key获取字典的值，如果没有则返回TValue的默认值
        /// </summary>
        /// <typeparam name="Tkey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dict"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static TValue GetByKey<Tkey, TValue>(this Dictionary<Tkey, TValue> dict, Tkey key)
        {
            var value = default(TValue);
            dict.TryGetValue(key, out value);
            return value;
        }

        public static string ToQueryString<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> list)
        {
            if (list == null) return string.Empty;
            return string.Join("&", list.Select(t => string.Format("{0}={1}", t.Key, t.Value)));
        }


        public static IEnumerable<KeyValuePair<TKey, TValue>> Sort<TKey, TValue>(
            this IEnumerable<KeyValuePair<TKey, TValue>> list)
        {
            list = from obj in list orderby obj.Key ascending select obj;
            return list;
        }


        /// <summary>
        /// 获取字典的第一个键
        /// </summary>
        /// <typeparam name="Tkey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dict"></param>
        /// <returns></returns>
        public static Tkey GetFirstKey<Tkey, TValue>(this Dictionary<Tkey, TValue> dict)
        {
            return dict.Keys.FirstOrDefault();
        }

        /// <summary>
        /// 获取字典的第一个值
        /// </summary>
        /// <typeparam name="Tkey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dict"></param>
        /// <returns></returns>
        public static TValue GetFirstValue<Tkey, TValue>(this Dictionary<Tkey, TValue> dict)
        {
            return dict.Values.FirstOrDefault();
        }
    }
}