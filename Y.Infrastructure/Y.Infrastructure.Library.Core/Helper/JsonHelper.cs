using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Y.Infrastructure.Library.Core.Helper
{
    public static class JsonHelper
    {
        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj, new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });
        }


        public static T JSONToObject<T>(string input)
        {
            return JsonConvert.DeserializeObject<T>(input);
        }


        public static bool IsJson(string str)
        {
            try
            {
                JObject.Parse(str);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static Dictionary<string, object> DataRowFromJSON(string jsonText)
        {
            return JSONToObject<Dictionary<string, object>>(jsonText);
        }
    }
}