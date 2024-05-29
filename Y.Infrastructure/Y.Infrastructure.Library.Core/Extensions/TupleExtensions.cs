using System;
using System.Collections.Generic;
using Y.Infrastructure.Library.Core.Helper;
using Y.Infrastructure.Library.Core.ViewModel;
using Y.Infrastructure.Library.Core.ViewModel.LayuiTable;

namespace Y.Infrastructure.Library.Core.Extensions
{
    public static class TupleExtensions
    {
        public static string ToJsonResult(this ValueTuple<bool, string> value)
        {
            var obj = new { code = value.Item1 ? 1 : 0, msg = value.Item2 };
            return obj.ToJson();
        }


        public static string ToJsonResult(this Tuple<bool, string> value)
        {
            var obj = new { code = value.Item1 ? 1 : 0, msg = value.Item2 };
            return obj.ToJson();
        }

        public static string ToJsonResult(this ValueTuple<bool, dynamic> value)
        {
            return new
            {
                code = value.Item1 ? 1 : 0,
                msg = "",
                info = value.Item2
            }.ToJson();
        }


        public static string ToJsonResult(this ValueTuple<int, string> value)
        {
            return new
            {
                code = value.Item1,
                msg = value.Item2
            }.ToJson();
        }

        public static string ToJsonResult(this ValueTuple<bool, string, dynamic> value) //where T : class
        {
            return new
            {
                code = value.Item1 ? 1 : 0,
                msg = value.Item2,
                info = value.Item3
            }.ToJson();
        }


        public static string ToJsonVersionResult(this ValueTuple<bool, string, dynamic> value) //where T : class
        {
            return new
            {
                code = value.Item1 ? 1 : 0,
                v = value.Item2,
                info = value.Item3
            }.ToJson();
        }


        public static TableDataModel ToTableModel<T>(this ValueTuple<IEnumerable<T>, int> value) //where T : class
        {
            return new TableDataModel
            {
                count = value.Item2,
                data = value.Item1,
            };
        }


        public static string ToTableModelError(this string value)
        {
            return (new
            {
                code = 201,
                count = 0,
                data = "",
                msg = value
            }).ToJson();
        }


        public static BaseResult ToSingleModel<T>(this ValueTuple<bool, T, string> value) //where T : class
        {
            return new BaseResult()
            {
                code = value.Item1 ? 1 : 0,
                info = value.Item2,
                msg = value.Item3
            };
        }
    }
}