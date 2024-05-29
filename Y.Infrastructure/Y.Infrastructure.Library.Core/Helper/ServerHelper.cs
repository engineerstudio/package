using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Y.Infrastructure.Library.Core.Helper
{
    /// <summary>
    /// Linux/Window
    /// </summary>
    public class ServerHelper
    {
        public static bool IsWindowRunTime()
        {
            return System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
        }

        public static bool IsLinuxRunTime()
        {
            return System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
        }


        public static string GetAssemblyPath()
        {
            dynamic type = (new ServerHelper()).GetType();
            return Path.GetDirectoryName(type.Assembly.Location);
        }
    }
}