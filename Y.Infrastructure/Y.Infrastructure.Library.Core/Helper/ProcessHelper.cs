using Microsoft.AspNetCore.Http;

namespace Y.Infrastructure.Library.Core.Helper
{
    public class ProcessHelper
    {
        public static HttpContext HttpContextInstance { get; set; }
    }
}