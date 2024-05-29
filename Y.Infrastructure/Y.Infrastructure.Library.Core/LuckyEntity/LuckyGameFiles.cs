using System;
using System.Collections.Generic;
using Y.Infrastructure.Library.Core.Extensions;
using Y.Infrastructure.Library.Core.Helper;
using System.Collections.Concurrent;
using System.Linq;

namespace Y.Infrastructure.Library.Core.LuckyEntity
{
    public class LuckyGameFiles
    {
        /// <summary>
        /// 股市开市闭市时间
        /// </summary>
        public readonly static Dictionary<string, Dictionary<string, Dictionary<string, string>>> StockMarketTime =
            new Dictionary<string, Dictionary<string, Dictionary<string, string>>>()
            {
                {
                    "CN", new Dictionary<string, Dictionary<string, string>>()
                    {
                        {"First", new Dictionary<string, string>() {{"9:30", "11:30"}}},
                        {"Second", new Dictionary<string, string>() {{"13:00", "15:00"}}}
                    }
                },
                {
                    "JP", new Dictionary<string, Dictionary<string, string>>()
                    {
                        {"First", new Dictionary<string, string>() {{"8:00", "10:00"}}},
                        {"Second", new Dictionary<string, string>() {{"11:30", "14:00"}}}
                    }
                },
                {
                    "HK", new Dictionary<string, Dictionary<string, string>>()
                    {
                        {"First", new Dictionary<string, string>() {{"10:00", "12:30"}}},
                        {"Second", new Dictionary<string, string>() {{"14:30", "16:00"}}}
                    }
                },
                {
                    "KR", new Dictionary<string, Dictionary<string, string>>() // 韩国
                    {
                        {"First", new Dictionary<string, string>() {{"8:00", "10:00"}}},
                        {"Second", new Dictionary<string, string>() {{"11:30", "14:00"}}}
                    }
                },
                {
                    "SG", new Dictionary<string, Dictionary<string, string>>()
                    {
                        {"First", new Dictionary<string, string>() {{"9:00", "12:30"}}},
                        {"Second", new Dictionary<string, string>() {{"14:00", "17:00"}}}
                    }
                },
                {
                    "US_summer", new Dictionary<string, Dictionary<string, string>>()
                    {
                        {"First", new Dictionary<string, string>() {{"21:30", "5:00"}}}
                    }
                },
                {
                    "US_winter", new Dictionary<string, Dictionary<string, string>>()
                    {
                        {"First", new Dictionary<string, string>() {{"22:30", "5:00"}}}
                    }
                },
                {
                    "AU_summer", new Dictionary<string, Dictionary<string, string>>()
                    {
                        {"First", new Dictionary<string, string>() {{"7:00", "13:00"}}}
                    }
                },
                {
                    "AU_winter", new Dictionary<string, Dictionary<string, string>>()
                    {
                        {"First", new Dictionary<string, string>() {{"8:00", "14:00"}}}
                    }
                },
                {
                    "MY", new Dictionary<string, Dictionary<string, string>>()
                    {
                        {"First", new Dictionary<string, string>() {{"9:00", "12:30"}}},
                        {"Second", new Dictionary<string, string>() {{"14:00", "17:00"}}}
                    }
                },
                {
                    "TH", new Dictionary<string, Dictionary<string, string>>() // 泰国
                    {
                        {"First", new Dictionary<string, string>() {{"10:30", "13:30"}}},
                        {"Second", new Dictionary<string, string>() {{"15:00", "18:00"}}}
                    }
                },
                {
                    "IN", new Dictionary<string, Dictionary<string, string>>()
                    {
                        {"First", new Dictionary<string, string>() {{"10:30", "13:30"}}},
                        {"Second", new Dictionary<string, string>() {{"15:00", "18:00"}}}
                    }
                },
                {
                    "DE", new Dictionary<string, Dictionary<string, string>>()
                    {
                        {"First", new Dictionary<string, string>() {{"10:30", "13:30"}}},
                        {"Second", new Dictionary<string, string>() {{"15:00", "18:00"}}}
                    }
                },
                {
                    "FR", new Dictionary<string, Dictionary<string, string>>()
                    {
                        {"First", new Dictionary<string, string>() {{"10:30", "13:30"}}},
                        {"Second", new Dictionary<string, string>() {{"15:00", "18:00"}}}
                    }
                },
                {
                    "IT", new Dictionary<string, Dictionary<string, string>>()
                    {
                        {"First", new Dictionary<string, string>() {{"10:30", "13:30"}}},
                        {"Second", new Dictionary<string, string>() {{"15:00", "18:00"}}}
                    }
                },
                {
                    "UK", new Dictionary<string, Dictionary<string, string>>()
                    {
                        {"First", new Dictionary<string, string>() {{"10:30", "13:30"}}},
                        {"Second", new Dictionary<string, string>() {{"15:00", "18:00"}}}
                    }
                },
                //{
                //    "RU", new Dictionary<string, Dictionary<string, string>>()
                //    {
                //        {"First", new Dictionary<string, string>() {{"10:30", "13:30"}}},
                //        {"Second", new Dictionary<string, string>() {{"15:00", "18:00"}}}
                //    }
                //},
            };

        /// <summary>
        /// 股市国家对应代码
        /// </summary>
        public readonly static Dictionary<string, string> StockCountryCode = new Dictionary<string, string>()
        {
            {"CN", "中国"},
            {"JP", "日本"},
            {"HK", "中国香港"},
            {"KR", "韩国"},
            {"SG", "新加坡"},
            {"US_summer", "美国夏令"},
            {"US_winter", "美国冬令"},
            {"AU_summer", "澳大利亚夏令"},
            {"AU_winter", "澳大利亚冬令"},
            {"MY", "马来西亚"},
            {"TH", "泰国"},
            {"IN", "印度"},
            {"DE", "德国"},
            {"FR", "法国"},
            {"IT", "意大利"},
            {"UK", "英国"},
            //{"RU", "俄罗斯"},
        };


        public readonly static Dictionary<string, string[]> StockMarketCode = new Dictionary<string, string[]>()
        {
            {"CN", new[] {"CN_SH", "CN_SZ", "CN_CY"}},
            {"JP", new[] {"JP_N225"}},
            {"HK", new[] {"HK_HSI", "HK_HSCEI"}},
            {"KR", new[] {"KR_KS11"}},
            {"SG", new[] {"SG_STI"}},
            {"US", new[] {""}},
            {"AU", new[] {""}},
            {"MY", new[] {"MY_KLSE"}},
            {"TH", new[] {"TH_SETI"}},
            {"IN", new[] {"IN_SENSEX"}},
            {"DE", new[] {"DE_GDAXI"}},
            {"FR", new[] {"FR_FCHI"}},
            {"IT", new[] {"IT_MIB"}},
            {"UK", new[] {"UK_FTSE"}},
            //{"RU", new[] {"RU_RTS"}},
        };


        public static string[] GetStockCodeStrings(LuckyStockCountry stockCountry)
        {
            return StockMarketCode[stockCountry.ToString()];
        }


        /// <summary>
        /// 股市开盘前禁止投注时间
        /// </summary>
        public readonly static Dictionary<string, int> StockCountryDisabledTime = new Dictionary<string, int>
        {
            {"CN", 15},
            {"JP", 15},
            {"HK", 15},
            {"KR", 15},
            {"SG", 15},
            {"US_summer", 15},
            {"US_winter", 15},
            {"AU_summer", 15},
            {"AU_winter", 15},
            {"MY", 15},
            {"TH", 15},
            {"IN", 15},
            {"DE", 15},
            {"FR", 15},
            {"IT", 15},
            {"UK", 15},
            //{"RU", 15},
        };

        /// <summary>
        /// 相对应国家股市时区
        /// </summary>
        public readonly static Dictionary<string, float> StockCountryUTCTimeZone = new Dictionary<string, float>
        {
            {"CN", +8},
            {"JP", +9},
            {"HK", +8},
            {"KR", +9},
            {"SG", +8},
            {"US_summer", -4},
            {"US_winter", -4},
            {"AU_summer", +10},
            {"AU_winter", +10},
            {"MY", +8},
            {"TH", +7},
            {"IN", +5.50f},
            {"DE", +1},
            {"FR", +1},
            {"IT", +1},
            {"UK", +1},
            //{"RU", +3},
        };


        public static Dictionary<string, string> GetCountryList()
        {
            Dictionary<string, string> ll = new Dictionary<string, string>();
            foreach (var pair in StockCountryCode)
            {
                switch (pair.Key)
                {
                    case "US_summer":
                    case "US_winter":
                        ll.TryAdd("US", "美国");
                        break;
                    case "AU_summer":
                    case "AU_winter":
                        ll.TryAdd("AU", "澳大利亚");
                        break;
                    default:
                        ll.Add(pair.Key, pair.Value);
                        break;
                }
            }

            return ll;
        }


        /// <summary>
        /// 获取股市的开始和结束时间
        /// </summary>
        /// <param name="date"></param>
        /// <param name="countryCode"></param>
        /// <returns></returns>
        public static (DateTime startTime, DateTime endTime) GetStartAndEndTime(DateTime date, string countryCode)
        {
            DateTime startTime = DefaultString.DefaultDateTime;
            DateTime endTime = DefaultString.DefaultDateTime;
            if (!StockCountryCode.ContainsKey(countryCode)) return (startTime, endTime);
            Dictionary<string, Dictionary<string, string>> dic = LuckyGameFiles.StockMarketTime[countryCode];
            if (dic.Count == 1)
            {
                string sdt = $"{date.ToDateString()} {dic["First"].GetFirstKey()}";
                startTime = sdt.ToDateTime().Value;

                string edt = $"{date.ToDateString()} {dic["First"].GetFirstValue()}";
                endTime = edt.ToDateTime().Value;

                if (endTime > startTime) endTime = endTime.AddDays(1);
            }
            else
            {
                string sdt = $"{date.ToDateString()} {dic["First"].GetFirstKey()}";
                startTime = sdt.ToDateTime().Value;

                string edt = $"{date.ToDateString()} {dic["Second"].GetFirstValue()}";
                endTime = edt.ToDateTime().Value;
            }

            return (startTime, endTime);
        }


        /// <summary>
        ///  当前时间是否是中国股市开盘时间
        /// </summary>
        /// <returns></returns>
        public static bool IsCNStockTime()
        {
            var dateTimeNow = DateTime.UtcNow.AddHours(8);
            var time930 = DateTime.UtcNow.AddHours(8).Date.AddHours(9.5);
            var tiem1130 = DateTime.UtcNow.AddHours(8).Date.AddHours(11.5);
            var time1300 = DateTime.UtcNow.AddHours(8).Date.AddHours(13);
            var time1500 = DateTime.UtcNow.AddHours(8).Date.AddHours(15);
            var v1 = (dateTimeNow > time930) && (dateTimeNow < tiem1130);
            var v3 = (dateTimeNow > time1300) && dateTimeNow < time1500;
            return v1 || v3;
        }
    }


    public class ExchangeInfo
    {
        public string Name { get; set; }
        public string CountryCodes { get; set; }
        public string District { get; set; }
        public string Abbr { get; set; }
        public string Utc8Time { get; set; }
        public string Utc8BreakTime { get; set; }
        public float UtcDiff { get; set; }

        /// <summary>
        /// Daylight Saving Time
        /// </summary>
        public string DST { get; set; }

        static object obj = new object();
        private static volatile List<ExchangeInfo> instance = null;

        public static List<ExchangeInfo> getInstance()
        {
            if (instance == null)
            {
                lock (obj)
                {
                    if (instance == null)
                    {
                        instance = new List<ExchangeInfo>();
                        ExchangeInfoInit();
                    }
                }
            }

            return instance;
        }

        private ExchangeInfo()
        {
        }

        static void ExchangeInfoInit()
        {
            instance.Add(new ExchangeInfo()
            {
                Name = "上海证券交易所",
                CountryCodes = "CN",
                Abbr = "SSE",
                District = "CN",
                Utc8Time = "9:30-15:00",
                Utc8BreakTime = "11:30-13:00",
                UtcDiff = 8
            }
            );
            instance.Add(new ExchangeInfo()
            {
                Name = "深圳证券交易所",
                CountryCodes = "CN",
                Abbr = "SZSE",
                District = "CN",
                Utc8Time = "9:30-15:00",
                Utc8BreakTime = "11:30-13:00",
                UtcDiff = 8
            }
            );
            instance.Add(new ExchangeInfo()
            {
                Name = "香港证券交易所",
                CountryCodes = "HK",
                Abbr = "HKEX",
                District = "HK",
                Utc8Time = "8:00-14:00",
                Utc8BreakTime = "10:30-11:30",
                UtcDiff = 8
            });
            instance.Add(new ExchangeInfo()
            {
                Name = "东京证券交易所",
                CountryCodes = "JP",
                Abbr = "TSE",
                District = "JP",
                Utc8Time = "9:30-15:00",
                Utc8BreakTime = "12:00-13:00",
                UtcDiff = 9
            });
            instance.Add(new ExchangeInfo()
            {
                Name = "韩国证券交易所",
                CountryCodes = "KR",
                Abbr = "KRX",
                District = "KR",
                Utc8Time = "8:00-14:30",
                Utc8BreakTime = "",
                UtcDiff = 9
            });
            instance.Add(new ExchangeInfo()
            {
                Name = "马来西亚股票交易所",
                CountryCodes = "MY",
                Abbr = "MYX",
                District = "MY",
                Utc8Time = "9:00-17:00",
                Utc8BreakTime = "12:30-14:30",
                UtcDiff = 8
            });
            instance.Add(new ExchangeInfo()
            {
                Name = "新加坡交易所",
                CountryCodes = "SG",
                Abbr = "SGX",
                District = "SG",
                Utc8Time = "9:00-17:00",
                Utc8BreakTime = "12:00-13:00",
                UtcDiff = 8
            });

            instance.Add(new ExchangeInfo()
            {
                Name = "泰国证券交易所",
                CountryCodes = "TH",
                Abbr = "SET",
                District = "TH",
                Utc8Time = "11:00-17:30",
                Utc8BreakTime = "13:30-15:30",
                UtcDiff = 7
            });
            instance.Add(new ExchangeInfo()
            {
                Name = "孟买证券交易所",
                CountryCodes = "IN",
                Abbr = "BSE",
                District = "IN",
                Utc8Time = "11:45-18:00",
                Utc8BreakTime = "",
                UtcDiff = 7
            });

            instance.Add(new ExchangeInfo()
            {
                Name = "印度国家证券交易所",
                CountryCodes = "IN",
                Abbr = "NSE",
                District = "IN",
                Utc8Time = "11:45-18:00",
                Utc8BreakTime = "",
                UtcDiff = 5.5f
            });

            instance.Add(new ExchangeInfo()
            {
                Name = "澳大利亚证券交易所",
                CountryCodes = "AU",
                Abbr = "ASX",
                District = "AU",
                Utc8Time = "08:00-14:00",
                Utc8BreakTime = "",
                UtcDiff = 5.5f
            });
            instance.Add(new ExchangeInfo()
            {
                Name = "伦敦证券交易所",
                CountryCodes = "UK",
                Abbr = "LSE",
                District = "UK",
                Utc8Time = "16:00-00:30",
                Utc8BreakTime = "",
                UtcDiff = 0f
            });

            instance.Add(new ExchangeInfo()
            {
                Name = "法兰克福证券交易所",
                CountryCodes = "DE",
                Abbr = "FSX",
                District = "DE",
                Utc8Time = "15:00-03:00",
                Utc8BreakTime = "",
                UtcDiff = 1
            });

            instance.Add(new ExchangeInfo()
            {
                Name = "巴黎泛欧交易所",
                CountryCodes = "FR",
                Abbr = "EPA",
                District = "FR",
                Utc8Time = "16:00-00:30",
                Utc8BreakTime = "",
                UtcDiff = 1
            });


            instance.Add(new ExchangeInfo()
            {
                Name = "米兰证券交易所",
                CountryCodes = "IT",
                Abbr = "MTA",
                District = "IT",
                Utc8Time = "16:00-00:30",
                Utc8BreakTime = "",
                UtcDiff = 1
            });
        }
    }
}