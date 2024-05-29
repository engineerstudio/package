using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Rewrite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Y.Infrastructure.Library.Middleware
{
    public class RewriteRulesMiddleware
    {
        /// <summary>
        /// 用于Nginx访问请求路径的处理
        /// </summary>
        /// <param name="context"></param>
        public static void RewriteNginxRote(RewriteContext context)
        {
            string path = string.Empty;
            var request = context.HttpContext.Request;
            if (!request.Path.StartsWithSegments(new PathString("/page")))
                return;
            else
                path = request.Path.Value.Replace("/page", "");

            if (!request.Path.StartsWithSegments(new PathString("/api")))
                return;
            else
                path = request.Path.Value.Replace("/api", "");
            context.Result = RuleResult.SkipRemainingRules;
            request.Path = path;
        }
    }
}
