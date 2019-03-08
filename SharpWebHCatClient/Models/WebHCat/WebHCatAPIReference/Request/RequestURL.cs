using TamkeenCommon;

namespace SharpHive.Models.WebHCat.WebHCatAPIReference.Request
{
    public static class RequestURL
    {
        private static readonly string urlBase = string.Format("/templeton/{0}", Configurations.Instance.WebHCatURLVersion);
        private static readonly string ddlUrlBase = string.Format("{0}/ddl", urlBase);
        private static readonly string jobsUrlBase = string.Format("{0}/jobs", urlBase);
        private static readonly string databaseDdlUrlBase = string.Format("{0}/database", ddlUrlBase);

        #region General
        public static string GetStatus
        {
            get { return string.Format("{0}/status", urlBase); }
        }

        public static string GetSupportedVersions
        {
            get { return string.Format("{0}/version", urlBase); }
        }

        public static string GetHiveVersions
        {
            get { return string.Format("{0}/version/hive", urlBase); }
        }

        public static string GetHadoopVersions
        {
            get { return string.Format("{0}/version/hadoop", urlBase); }
        }
        #endregion

        #region DDL
        public static string PostHCatalogDDL
        {
            get { return string.Format("{0}?user.name={1}", ddlUrlBase, Configurations.Instance.WebHCatUserName); }
        }
        #region Database
        public static string GetDatabasesList
        {
            get { return string.Format("/{0}?user.name={1}", databaseDdlUrlBase, Configurations.Instance.WebHCatUserName); }
        }

        public static string DescribeDatabase(string databaseName)
        {
            return string.Format("{0}/{1}?user.name={2}", databaseDdlUrlBase, databaseName, Configurations.Instance.WebHCatUserName);
        }

        public static string CreateDatabase(string databaseName)
        {
            return string.Format("{0}/{1}?user.name={2}", databaseDdlUrlBase, databaseName, Configurations.Instance.WebHCatUserName);
        }
        public static string DeleteDatabase(string databaseName)
        {
            return string.Format("{0}/{1}?user.name={2}", databaseDdlUrlBase, databaseName, Configurations.Instance.WebHCatUserName);
        }
        #endregion

        #region Tables
        /// <summary>
        /// Lists the table.
        /// </summary>
        /// <returns>The table.</returns>
        /// <param name="databaseName">Database name.</param>
        /// <param name="like">List only tables whose names match the specified pattern, Optional, "*" (List all tables).</param>
        public static string ListTables(string databaseName, string like)
        {
            return string.Format("{0}/{1}/table?user.name={2}&like={3}", databaseDdlUrlBase, databaseName, Configurations.Instance.WebHCatUserName, (string.IsNullOrEmpty(like) ? "*" : like));
        }

        public static string DescribeTable(string databaseName, string tableName, bool extended)
        {
            return string.Format("{0}/{1}/table/{2}?user.name={3}{4}", databaseDdlUrlBase, databaseName, tableName, Configurations.Instance.WebHCatUserName, ((extended) ? string.Empty : "&format=extended"));
        }

        public static string CreateTable(string databaseName, string tableName)
        {
            return string.Format("{0}/{1}/table/{2}?user.name={3}", databaseDdlUrlBase, databaseName, tableName, Configurations.Instance.WebHCatUserName);
        }

        public static string CreateTableLike(string databaseName, string tableName, string newTableName)
        {
            return string.Format("{0}/{1}/table/{2}/like/{3}?user.name={4}", databaseDdlUrlBase, databaseName, tableName, newTableName, Configurations.Instance.WebHCatUserName);
        }

        public static string RenameTable(string databaseName, string oldTableName)
        {
            return CreateTable(databaseName, oldTableName);
        }

        public static string DeleteTable(string databaseName, string tableName)
        {
            return CreateTable(databaseName, tableName);
        }
        #region Partitions
        public static string ListTablePartitions(string databaseName, string tableName)
        {
            return string.Format("{0}/{1}/table/{2}/partition?user.name={3}", databaseDdlUrlBase, databaseName, tableName, Configurations.Instance.WebHCatUserName);
        }

        public static string DescribeTablePartition(string databaseName, string tableName, string partitionName)
        {
            return string.Format("{0}/{1}/table/{2}/partition/{3}?user.name={4}", databaseDdlUrlBase, databaseName, tableName, partitionName, Configurations.Instance.WebHCatUserName);
        }

        public static string CreateTablePartition(string databaseName, string tableName, string partitionName)
        {
            return string.Format("{0}/{1}/table/{2}/partition/{3}?user.name={4}", databaseDdlUrlBase, databaseName, tableName, partitionName, Configurations.Instance.WebHCatUserName);
        }

        public static string DeleteTablePartition(string databaseName, string tableName, string partitionName)
        {
            return string.Format("{0}/{1}/table/{2}/partition/{3}?user.name={4}", databaseDdlUrlBase, databaseName, tableName, partitionName, Configurations.Instance.WebHCatUserName);
        }
        #endregion
        #region Columns
        public static string ListTableColumns(string databaseName, string tableName)
        {
            return string.Format("{0}/{1}/table/{2}/column?user.name={3}", databaseDdlUrlBase, databaseName, tableName, Configurations.Instance.WebHCatUserName);
        }

        public static string DescribeTableColumn(string databaseName, string tableName, string columnName)
        {
            return string.Format("{0}/{1}/table/{2}/column/{3}?user.name={4}", databaseDdlUrlBase, databaseName, tableName, columnName, Configurations.Instance.WebHCatUserName);
        }

        public static string CreateTableColumn(string databaseName, string tableName, string columnName)
        {
            return string.Format("{0}/{1}/table/{2}/column/{3}?user.name={4}", databaseDdlUrlBase, databaseName, tableName, columnName, Configurations.Instance.WebHCatUserName);
        }

        #endregion
        #region Properties
        public static string ListTableProperties(string databaseName, string tableName)
        {
            return string.Format("{0}/{1}/table/{2}/property?user.name={3}", databaseDdlUrlBase, databaseName, tableName, Configurations.Instance.WebHCatUserName);
        }

        public static string GetTableProperty(string databaseName, string tableName, string propertyName)
        {
            return string.Format("{0}/{1}/table/{2}/property/{3}?user.name={4}", databaseDdlUrlBase, databaseName, tableName, propertyName, Configurations.Instance.WebHCatUserName);
        }

        public static string CreateTableProperty(string databaseName, string tableName, string propertyName)
        {
            return string.Format("{0}/{1}/table/{2}/property/{3}?user.name={4}", databaseDdlUrlBase, databaseName, tableName, propertyName, Configurations.Instance.WebHCatUserName);
        }

        #endregion
        #endregion
        #endregion

        #region Jobs
        #region MapReduce
        public static string MapReduceStreamingJob()
        {
            return string.Format("{0}/mapreduce/streaming?user.name={1}", urlBase, Configurations.Instance.WebHCatUserName);
        }

        public static string MapReduceJob()
        {
            return string.Format("{0}/mapreduce/jar?user.name={1}", urlBase, Configurations.Instance.WebHCatUserName);
        }
        #endregion

        public static string PigJob()
        {
            return string.Format("{0}/pig?user.name={1}", urlBase, Configurations.Instance.WebHCatUserName);
        }

        public static string HiveJob()
        {
            return string.Format("{0}/hive?user.name={1}", urlBase, Configurations.Instance.WebHCatUserName);
        }

        #endregion
    }
}