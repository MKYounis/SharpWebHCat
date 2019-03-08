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
        #region Source
        public string BaseURL
        {
            get
            {
                return Functions.CheckPath(configuration.GetSection("SourceNessus").GetSection("BaseURL").Value);
            }
        }
        public string ApplictionType
        {
            get
            {
                return configuration.GetSection("SourceNessus").GetSection("ApplictionType").Value;
            }
        }
        public string Xapikeys
        {
            get
            {
                return configuration.GetSection("SourceNessus").GetSection("x-apikeys").Value;
            }
        }
        public string HistoryFileCheckURL
        {
            get
            {
                return configuration.GetSection("SourceNessus").GetSection("HistoryFileCheckURL").Value;
            }
        }
        public string HistoryFileDownloadFormat
        {
            get
            {
                return configuration.GetSection("SourceNessus").GetSection("HistoryFileDownloadFormat").Value;
            }
        }
        public string HistoryFileStatusURL
        {
            get
            {
                return Functions.CheckPath(configuration.GetSection("SourceNessus").GetSection("HistoryFileStatusURL").Value);
            }
        }
        public string HistoryFileSaveURL
        {
            get
            {
                return Functions.CheckPath(configuration.GetSection("SourceNessus").GetSection("HistoryFileSaveURL").Value);
            }
        }
        public string FoldersPath
        {
            get
            {
                return Functions.CheckPath(configuration.GetSection("SourceNessus").GetSection("FoldersPath").Value);
            }
        }
        public string ScanPth
        {
            get
            {
                return configuration.GetSection("SourceNessus").GetSection("ScanPth").Value;
            }
        }
        public string ScanDetailsPath
        {
            get
            {
                return configuration.GetSection("SourceNessus").GetSection("ScanDetailsPath").Value;
            }
        }
        public string SourceSplitRegExForCommaDelimiter
        {
            get
            {
                return configuration.GetSection("SourceNessus").GetSection("SplitRegExForCommaDelimiter").Value;
            }
        }
        public string SourceSplitRegExForNewLine
        {
            get
            {
                return configuration.GetSection("SourceNessus").GetSection("SplitRegExForNewLine").Value;
            }
        }
        #endregion

        #region Export
        public string ExportSaveLocation
        {
            get
            {
                return Functions.CheckPath(configuration.GetSection("Export").GetSection("SaveLocation").Value);
            }
        }
        public string ExportCSVDelimiter
        {
            get
            {
                return configuration.GetSection("Export").GetSection("CSVDelimiter").Value;
            }
        }
        #endregion
        #region WebHCat
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

        #endregion
    }
}