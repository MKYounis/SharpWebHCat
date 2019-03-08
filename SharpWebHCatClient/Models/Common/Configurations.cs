using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using SharpHive.Models.Common;

namespace TamkeenCommon
{
    public sealed class Configurations
    {
        private static readonly Configurations instance = new Configurations();
        private static IConfigurationRoot configuration;
        static Configurations() { }
        private Configurations()
        {
            string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var builder = new ConfigurationBuilder()
                                    .SetBasePath(Path.Combine(AppContext.BaseDirectory))
                                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                    .AddJsonFile($"appsettings.{environment}.json", optional: true);


            configuration = builder.Build();
        }

        public static Configurations Instance { get { return instance; } }

        public string ApplictionType
        {
            get
            {
                return configuration.GetSection("WebHCat").GetSection("ApplictionType").Value;
            }
        }

        public string WebHCatURLVersion
        {
            get
            {
                return configuration.GetSection("WebHCat").GetSection("Version").Value;
            }
        }

        public string WebHCatBaseURL
        {
            get
            {
                return configuration.GetSection("WebHCat").GetSection("BaseURL").Value;
            }
        }

        public string WebHCatUserName
        {
            get
            {
                return configuration.GetSection("WebHCat").GetSection("UserName").Value;
            }
        }
    }
}