using System;
using Y.Infrastructure.Library.Core.Helper;
using Y.Infrastructure.Library.Core.LuckyEntity;

namespace Y.Infrastructure.Library.Core.Extensions
{
    public static class TupleExtensionsForLuckyEntity
    {
        public static string ToLuckyWebApiJsonResult(this ValueTuple<bool, string, dynamic> value)
        {
            return new WebApiResultEntity()
            {
                Code = value.Item1 ? "Sucess" : "Failed",
                Error = value.Item1 ? "" : value.Item2,
                Results = value.Item3
            }.ToJson();
        }


        public static string ToLuckyWebApiJsonResult(this ValueTuple<bool, string> value)
        {
            return new WebApiResultEntity()
            {
                Code = value.Item1 ? "Sucess" : "Failed",
                Error = value.Item1 ? "" : value.Item2
            }.ToJson();
        }
    }
}