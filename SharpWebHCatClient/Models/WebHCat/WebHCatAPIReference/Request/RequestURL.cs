
namespace SharpHive.Models.WebHCat.WebHCatAPIReference.Request
{
    public class RequestURL
    {
        private readonly string urlBase;
        private readonly string ddlUrlBase;
        private readonly string jobsUrlBase;
        private readonly string databaseDdlUrlBase;
        private readonly string _webHCatVersion;
        private readonly string _webHCatUserName;
        //string webHCatBaseUrl, string webHCatVersion, string webHCatUserName

        public RequestURL(string webHCatVersion, string webHCatUserName)
        {
            _webHCatVersion = webHCatVersion;
            _webHCatUserName = webHCatUserName;
            urlBase = string.Format("/templeton/{0}", webHCatVersion);
            ddlUrlBase = string.Format("{0}/ddl", urlBase);
            jobsUrlBase = string.Format("{0}/jobs", urlBase);
            databaseDdlUrlBase = string.Format("{0}/database", ddlUrlBase);
        }

        #region General
        public string GetStatus
        {
            get { return string.Format("{0}/status", urlBase); }
        }

        public string GetSupportedVersions
        {
            get { return string.Format("{0}/version", urlBase); }
        }

        public string GetHiveVersions
        {
            get { return string.Format("{0}/version/hive", urlBase); }
        }

        public string GetHadoopVersions
        {
            get { return string.Format("{0}/version/hadoop", urlBase); }
        }
        #endregion

        #region DDL
        public string PostHCatalogDDL
        {
            get { return string.Format("{0}?user.name={1}", ddlUrlBase, _webHCatUserName); }
        }
        #region Database
        public string GetDatabasesList
        {
            get { return string.Format("/{0}?user.name={1}", databaseDdlUrlBase, _webHCatUserName); }
        }

        public string DescribeDatabase(string databaseName)
        {
            return string.Format("{0}/{1}?user.name={2}", databaseDdlUrlBase, databaseName, _webHCatUserName);
        }

        public string CreateDatabase(string databaseName)
        {
            return string.Format("{0}/{1}?user.name={2}", databaseDdlUrlBase, databaseName, _webHCatUserName);
        }
        public string DeleteDatabase(string databaseName)
        {
            return string.Format("{0}/{1}?user.name={2}", databaseDdlUrlBase, databaseName, _webHCatUserName);
        }
        #endregion

        #region Tables
        /// <summary>
        /// Lists the table.
        /// </summary>
        /// <returns>The table.</returns>
        /// <param name="databaseName">Database name.</param>
        /// <param name="like">List only tables whose names match the specified pattern, Optional, "*" (List all tables).</param>
        public string ListTables(string databaseName, string like)
        {
            return string.Format("{0}/{1}/table?user.name={2}&like={3}", databaseDdlUrlBase, databaseName, _webHCatUserName, (string.IsNullOrEmpty(like) ? "*" : like));
        }

        public string DescribeTable(string databaseName, string tableName, bool extended)
        {
            return string.Format("{0}/{1}/table/{2}?user.name={3}{4}", databaseDdlUrlBase, databaseName, tableName, _webHCatUserName, ((extended) ? string.Empty : "&format=extended"));
        }

        public string CreateTable(string databaseName, string tableName)
        {
            return string.Format("{0}/{1}/table/{2}?user.name={3}", databaseDdlUrlBase, databaseName, tableName, _webHCatUserName);
        }

        public string CreateTableLike(string databaseName, string tableName, string newTableName)
        {
            return string.Format("{0}/{1}/table/{2}/like/{3}?user.name={4}", databaseDdlUrlBase, databaseName, tableName, newTableName, _webHCatUserName);
        }

        public string RenameTable(string databaseName, string oldTableName)
        {
            return CreateTable(databaseName, oldTableName);
        }

        public string DeleteTable(string databaseName, string tableName)
        {
            return CreateTable(databaseName, tableName);
        }
        #region Partitions
        public string ListTablePartitions(string databaseName, string tableName)
        {
            return string.Format("{0}/{1}/table/{2}/partition?user.name={3}", databaseDdlUrlBase, databaseName, tableName, _webHCatUserName);
        }

        public string DescribeTablePartition(string databaseName, string tableName, string partitionName)
        {
            return string.Format("{0}/{1}/table/{2}/partition/{3}?user.name={4}", databaseDdlUrlBase, databaseName, tableName, partitionName, _webHCatUserName);
        }

        public string CreateTablePartition(string databaseName, string tableName, string partitionName)
        {
            return string.Format("{0}/{1}/table/{2}/partition/{3}?user.name={4}", databaseDdlUrlBase, databaseName, tableName, partitionName, _webHCatUserName);
        }

        public string DeleteTablePartition(string databaseName, string tableName, string partitionName)
        {
            return string.Format("{0}/{1}/table/{2}/partition/{3}?user.name={4}", databaseDdlUrlBase, databaseName, tableName, partitionName, _webHCatUserName);
        }
        #endregion
        #region Columns
        public string ListTableColumns(string databaseName, string tableName)
        {
            return string.Format("{0}/{1}/table/{2}/column?user.name={3}", databaseDdlUrlBase, databaseName, tableName, _webHCatUserName);
        }

        public string DescribeTableColumn(string databaseName, string tableName, string columnName)
        {
            return string.Format("{0}/{1}/table/{2}/column/{3}?user.name={4}", databaseDdlUrlBase, databaseName, tableName, columnName, _webHCatUserName);
        }

        public string CreateTableColumn(string databaseName, string tableName, string columnName)
        {
            return string.Format("{0}/{1}/table/{2}/column/{3}?user.name={4}", databaseDdlUrlBase, databaseName, tableName, columnName, _webHCatUserName);
        }

        #endregion
        #region Properties
        public string ListTableProperties(string databaseName, string tableName)
        {
            return string.Format("{0}/{1}/table/{2}/property?user.name={3}", databaseDdlUrlBase, databaseName, tableName, _webHCatUserName);
        }

        public string GetTableProperty(string databaseName, string tableName, string propertyName)
        {
            return string.Format("{0}/{1}/table/{2}/property/{3}?user.name={4}", databaseDdlUrlBase, databaseName, tableName, propertyName, _webHCatUserName);
        }

        public string CreateTableProperty(string databaseName, string tableName, string propertyName)
        {
            return string.Format("{0}/{1}/table/{2}/property/{3}?user.name={4}", databaseDdlUrlBase, databaseName, tableName, propertyName, _webHCatUserName);
        }

        #endregion
        #endregion
        #endregion

        #region Jobs
        #region MapReduce
        public string MapReduceStreamingJob()
        {
            return string.Format("{0}/mapreduce/streaming?user.name={1}", urlBase, _webHCatUserName);
        }

        public string MapReduceJob()
        {
            return string.Format("{0}/mapreduce/jar?user.name={1}", urlBase, _webHCatUserName);
        }
        #endregion

        public string PigJob()
        {
            return string.Format("{0}/pig?user.name={1}", urlBase, _webHCatUserName);
        }

        public string HiveJob()
        {
            return string.Format("{0}/hive?user.name={1}", urlBase, _webHCatUserName);
        }

        #endregion
    }
}