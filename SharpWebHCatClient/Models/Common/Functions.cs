using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
namespace SharpHive.Models.Common
{
    public static class Functions
    {
        public static string AppendQueryStringParameters(string url, params KeyValuePair<string, string>[] parameters)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(url);
            for (int i = 0; i < parameters.Length; i++)
            {
                builder.Append((i == 0) ? "?" : "&");
                builder.AppendFormat("{0}={1}", parameters[i].Key, parameters[i].Value);
            }
            return builder.ToString();
        }
		
		public static string CheckPath(string path)
        {
            string cleanPath = path.TrimEnd((RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) ? '\\' : '/');
            return cleanPath;
        }
    }
}
