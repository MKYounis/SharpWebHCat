using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using SharpHive.Models.WebHCat;
using SharpHive.Models.WebHCat.WebHCatAPIReference.Request;
using SharpHive.Models.WebHCat.WebHCatAPIReference.Response.General;
using SharpHive.Models.WebHCat.WebHCatAPIReference.Response.HDatabase;
using SharpHive.Models.WebHCat.WebHCatAPIReference.Response.HTable;

using SharpHive.Models.WebHCat.WebHCatAPIReference.Request.HDatabase;
using SharpHive.Models.WebHCat.WebHCatAPIReference.Request.HTable;
using System.Dynamic;
using SharpHive.Models.WebHCat.WebHCatAPIReference.Response.Jobs;

namespace SharpHive.Managers
{
    public sealed class WebHCatRequestsManager : WebHCatRequester
    {
        public WebHCatRequestsManager() { }

        public async Task<WebHCatStatus> GetConnectionStatus()
        {

            WebHCatStatus webHCatStatus = await Get<WebHCatStatus>(RequestURL.GetStatus);
            return webHCatStatus;
        }

        /// <summary>
        /// Performs an HCatalog DDL command. The command is executed immediately upon request. Responses are limited to 1 MB. 
        /// </summary>
        /// <returns>The command.</returns>
        /// <param name="command">Command.</param>
        public async Task<HCatalogDDL> ExecuteCommand(string command)
        {

            List<KeyValuePair<string, string>> postParams = new List<KeyValuePair<string, string>>();

            KeyValuePair<string, string> exec = new KeyValuePair<string, string>("exec", command);

            postParams.Add(exec);

            HCatalogDDL hCatalogDDL = await Post<HCatalogDDL>(RequestURL.PostHCatalogDDL, postParams);
            return hCatalogDDL;

        }

        #region Database
        public async Task<DatabasesList> GetDatabases()
        {

            DatabasesList hDatabasesList = await Get<DatabasesList>(RequestURL.GetDatabasesList);
            return hDatabasesList;

        }

        public async Task<DescribeDatabase> GetDatabase(string dbName)
        {
            var describeDatabase = await Get<DescribeDatabase>(RequestURL.DescribeDatabase(dbName));
            return describeDatabase;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="database">new database name => Required</param>
        /// <param name="comment">Required</param>
        /// <param name="group"></param>
        /// <param name="permissions"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        public async Task<CreateDatabase> CreateDatabase(string database, string comment, string group, string permissions, string location)
        {
            if (string.IsNullOrEmpty(database))
                throw new Exception("database and comment are required.");

            CreateDatabaseParam jsonParams = new CreateDatabaseParam
            {
                comment = comment,
                group = group,
                permissions = permissions,
                location = location
            };

            CreateDatabase createDatabase = await Put<CreateDatabase>(RequestURL.CreateDatabase(database), jsonParams);
            return createDatabase;

        }

        public async Task<DeleteDatabase> DeleteDatabase(string database, bool ifExists, string option, string group, string permissions)
        {
            if (string.IsNullOrEmpty(database))
                throw new Exception("database and comment are required.");

            DeleteDatabaseParam jsonParams = new DeleteDatabaseParam
            {
                ifExists = ifExists,
                option = option,
                group = group,
                permissions = permissions
            };

            DeleteDatabase deleteDatabase = await Delete<DeleteDatabase>(RequestURL.CreateDatabase(database), jsonParams);
            return deleteDatabase;

        }
        #endregion

        #region Table
        public async Task<TableList> ListTables(string database, string like)
        {
            if (string.IsNullOrEmpty(database))
                throw new Exception("database is required.");

            TableList tableList = await Get<TableList>(RequestURL.ListTables(database, like));
            return tableList;

        }

        public async Task<TableDescribe> GetTableDescription(string database, string table)
        {
            if (string.IsNullOrEmpty(database))
                throw new Exception("database is required.");
            if (string.IsNullOrEmpty(table))
                throw new Exception("table is required.");

            TableDescribe tableDescribe = await Get<TableDescribe>(RequestURL.DescribeTable(database, table, false));
            return tableDescribe;

        }

        public async Task<TableDescribeExtended> GetTableDescriptionExtended(string database, string table)
        {
            if (string.IsNullOrEmpty(database))
                throw new Exception("database is required.");
            if (string.IsNullOrEmpty(table))
                throw new Exception("table is required.");

            TableDescribeExtended tableDescribeExtended = await Get<TableDescribeExtended>(RequestURL.DescribeTable(database, table, true));
            return tableDescribeExtended;

        }

        public async Task<TableCreate> CreateTable(string database, string table, Table hTable)
        {
            if (string.IsNullOrEmpty(database))
                throw new Exception("database is required.");
            if (string.IsNullOrEmpty(table))
                throw new Exception("table is required.");

            TableCreate tableCreate = await Put<TableCreate>(RequestURL.CreateTable(database, table), hTable);
            return tableCreate;

        }

        public async Task<TableDDL> RenameTable(string database, string oldTableName, string newTableName)
        {
            List<KeyValuePair<string, string>> postParams = new List<KeyValuePair<string, string>>();

            KeyValuePair<string, string> rename = new KeyValuePair<string, string>("rename", newTableName);

            postParams.Add(rename);

            TableDDL tableRename = await Post<TableDDL>(RequestURL.RenameTable(database, oldTableName), postParams);
            return tableRename;

        }

        public async Task<TableDDL> CreateTableLike(string database, string tableName, string newTableName, bool ifNotExists, string group, string permissions)
        {
            if (string.IsNullOrEmpty(database) || string.IsNullOrEmpty(tableName))
                throw new Exception("database and table are required.");

            CreateTableLikeParams jsonParams = new CreateTableLikeParams
            {
                ifNotExists = ifNotExists,
                group = group,
                permissions = permissions
            };

            TableDDL createTableLike = await Put<TableDDL>(RequestURL.CreateTableLike(database, tableName, newTableName), jsonParams);
            return createTableLike;

        }

        public async Task<TableDDL> DeleteTable(string database, string tableName, bool ifExists, string group, string permissions)
        {

            if (string.IsNullOrEmpty(database) || string.IsNullOrEmpty(tableName))
                throw new Exception("database and table are required.");

            DeleteTableParams jsonParams = new DeleteTableParams
            {
                ifExists = ifExists,
                group = group,
                permissions = permissions
            };

            TableDDL deleteTable = await Delete<TableDDL>(RequestURL.DeleteTable(database, tableName), jsonParams);
            return deleteTable;
        }

        #region Partitions
        public async Task<TablePartitionsList> ListTablePartitions(string database, string tableName)
        {
            if (string.IsNullOrEmpty(database) || string.IsNullOrEmpty(tableName))
                throw new Exception("database and table are required.");

            TablePartitionsList tablePartitionsList = await Get<TablePartitionsList>(RequestURL.ListTablePartitions(database, tableName));
            return tablePartitionsList;

        }
        /// <summary>
        /// Describes the table partitions.
        /// </summary>
        /// <returns>The table partitions.</returns>
        /// <param name="database">Database.</param>
        /// <param name="tableName">Table name.</param>
        /// <param name="partitionName">The partition name, col_name='value' list. Be careful to properly encode the quote for http, for example, country=%27algeria%27.</param>
        public async Task<PartitionDescribe> DescribeTablePartition(string database, string tableName, string partitionName)
        {
            if (string.IsNullOrEmpty(database) || string.IsNullOrEmpty(tableName))
                throw new Exception("database and table are required.");

            PartitionDescribe partitionDescribe = await Get<PartitionDescribe>(RequestURL.DescribeTablePartition(database, tableName, partitionName));
            return partitionDescribe;

        }

        public async Task<PartitionDDL> CreateTablePartition(string database, string tableName, string partitionName, bool ifNotExists, string group, string permissions, string location)
        {
            if (string.IsNullOrEmpty(database) || string.IsNullOrEmpty(tableName) || string.IsNullOrEmpty(partitionName))
                throw new Exception("database, table and partition are required.");

            CreatePartitionParams jsonParams = new CreatePartitionParams
            {
                ifNotExists = ifNotExists,
                group = group,
                permissions = permissions,
                location = location
            };

            PartitionDDL createPartition = await Put<PartitionDDL>(RequestURL.CreateTablePartition(database, tableName, partitionName), jsonParams);
            return createPartition;
        }

        public async Task<PartitionDDL> DeleteTablePartition(string database, string tableName, string partitionName, bool ifExists, string group, string permissions)
        {
            if (string.IsNullOrEmpty(database) || string.IsNullOrEmpty(tableName) || string.IsNullOrEmpty(partitionName))
                throw new Exception("database, table and partition are required.");

            DeleteTablePartitionParams jsonParams = new DeleteTablePartitionParams
            {
                ifExists = ifExists,
                group = group,
                permissions = permissions
            };

            PartitionDDL deleteTablePartition = await Delete<PartitionDDL>(RequestURL.DeleteTablePartition(database, tableName, partitionName), jsonParams);
            return deleteTablePartition;
        }

        #endregion
        #region Columns
        public async Task<TableColumnsList> ListTableColumns(string database, string tableName)
        {
            if (string.IsNullOrEmpty(database) || string.IsNullOrEmpty(tableName))
                throw new Exception("database and table are required.");

            TableColumnsList tableColumnsList = await Get<TableColumnsList>(RequestURL.ListTableColumns(database, tableName));
            return tableColumnsList;
        }

        public async Task<ColumnDescribe> DescribeTableColumn(string database, string tableName, string columnName)
        {
            if (string.IsNullOrEmpty(database) || string.IsNullOrEmpty(tableName) || string.IsNullOrEmpty(columnName))
                throw new Exception("database, table and column are required.");

            ColumnDescribe columnDescribe = await Get<ColumnDescribe>(RequestURL.DescribeTableColumn(database, tableName, columnName));
            return columnDescribe;
        }

        public async Task<TableColumnDDL> CreateTableColumn(string database, string tableName, Models.WebHCat.WebHCatAPIReference.Request.HTable.Column column)
        {
            if (string.IsNullOrEmpty(database) || string.IsNullOrEmpty(tableName) || null == column)
                throw new Exception("database, table and column are required.");

            CreateColumnParams createParams = new CreateColumnParams();
            createParams.type = column.type;
            createParams.comment = column.comment;

            TableColumnDDL createColumn = await Put<TableColumnDDL>(RequestURL.CreateTableColumn(database, tableName, column.name), createParams);
            return createColumn;
        }
        #endregion
        #region Properties
        public async Task<TableProperties> ListTableProperties(string database, string tableName)
        {
            if (string.IsNullOrEmpty(database) || string.IsNullOrEmpty(tableName))
                throw new Exception("database and table are required.");

            TableProperties tableProperties = await Get<TableProperties>(RequestURL.ListTableProperties(database, tableName));
            return tableProperties;
        }

        public async Task<TableProperty> GetTableProperty(string database, string tableName, string propertyName)
        {
            if (string.IsNullOrEmpty(database) || string.IsNullOrEmpty(tableName) || string.IsNullOrEmpty(propertyName))
                throw new Exception("database, table and property are required.");

            TableProperty tableProperty = await Get<TableProperty>(RequestURL.GetTableProperty(database, tableName, propertyName));
            return tableProperty;
        }

        public async Task<TableProperty> CreateTableProperty(string database, string tableName, string propertyName, PropertyValue propertyValue)
        {
            if (string.IsNullOrEmpty(database) || string.IsNullOrEmpty(tableName) || string.IsNullOrEmpty(propertyName))
                throw new Exception("database, table and property are required.");

            TableProperty createProperty = await Put<TableProperty>(RequestURL.CreateTableProperty(database, tableName, propertyName), propertyValue);
            return createProperty;
        }
        #endregion
        #endregion

        #region Jobs
        #region MapReduce
        /// <summary>
        /// Create and queue a Hadoop streaming MapReduce job.
        /// </summary>
        /// <returns>The reduce streaming job.</returns>
        /// <param name="input">Location of the input data in Hadoop.</param>
        /// <param name="output">Location in which to store the output data. If not specified, WebHCat will store the output in a location that can be discovered using the queue resource.</param>
        /// <param name="mapper">Location of the mapper program in Hadoop.</param>
        /// <param name="reducer">Location of the reducer program in Hadoop.</param>
        /// <param name="file">Add an HDFS file to the distributed cache.</param>
        /// <param name="define">Set a Hadoop configuration variable using the syntax define=NAME=VALUE.</param>
        /// <param name="cmdenv">Set an environment variable using the syntax cmdenv=NAME=VALUE.</param>
        /// <param name="arg">Set a program argument.</param>
        /// <param name="statusDir">A directory where WebHCat will write the status of the Map Reduce job. If provided, it is the caller's responsibility to remove this directory when done.</param>
        /// <param name="enableLog">If statusdir is set and enablelog is "true", collect Hadoop job configuration and logs into a directory named $statusdir/logs after the job finishes. Both completed and failed attempts are logged. The layout of subdirectories in $statusdir/logs is: 
        ///                             logs/$job_id(directory for $job_id)
        ///                             logs/$job_id/job.xml.html
        ///                             logs/$job_id/$attempt_id(directory for $attempt_id)
        ///                             logs/$job_id/$attempt_id/stderr
        ///                             logs/$job_id/$attempt_id/stdout
        ///                             logs/$job_id/$attempt_id/syslog</param>
        /// <param name="callBack">Define a URL to be called upon job completion. You may embed a specific job ID into this URL using $jobId. This tag will be replaced in the callback URL with this job's job ID.</param>
        public async Task<MapReduceStreamingJob> MapReduceStreamingJob(string input, string output, string mapper, string reducer, string file, string define, string cmdenv, string arg, string statusDir, string enableLog, string callBack)
        {
            List<KeyValuePair<string, string>> postParams = new List<KeyValuePair<string, string>>();
            if (!string.IsNullOrEmpty(input))
                postParams.Add(new KeyValuePair<string, string>("input", input));
            if (!string.IsNullOrEmpty(output))
                postParams.Add(new KeyValuePair<string, string>("output", output));
            if (!string.IsNullOrEmpty(mapper))
                postParams.Add(new KeyValuePair<string, string>("mapper", mapper));
            if (!string.IsNullOrEmpty(reducer))
                postParams.Add(new KeyValuePair<string, string>("reducer", reducer));
            if (!string.IsNullOrEmpty(file))
                postParams.Add(new KeyValuePair<string, string>("file", file));
            if (!string.IsNullOrEmpty(define))
                postParams.Add(new KeyValuePair<string, string>("define", define));
            if (!string.IsNullOrEmpty(cmdenv))
                postParams.Add(new KeyValuePair<string, string>("cmdenv", cmdenv));
            if (!string.IsNullOrEmpty(arg))
                postParams.Add(new KeyValuePair<string, string>("arg", arg));
            if (!string.IsNullOrEmpty(statusDir))
                postParams.Add(new KeyValuePair<string, string>("statusDir", statusDir));
            if (!string.IsNullOrEmpty(enableLog))
                postParams.Add(new KeyValuePair<string, string>("enableLog", enableLog));
            if (!string.IsNullOrEmpty(callBack))
                postParams.Add(new KeyValuePair<string, string>("callBack", callBack));

            MapReduceStreamingJob tableRename = await Post<MapReduceStreamingJob>(RequestURL.MapReduceStreamingJob(), postParams);
            return tableRename;
        }

        /// <summary>
        /// Creates and queues a standard Hadoop MapReduce job.
        /// </summary>
        /// <param name="jar">Name of the jar file for Map Reduce to use.</param>
        /// <param name="class">Name of the class for Map Reduce to use.</param>
        /// <param name="libjars">Comma separated jar files to include in the classpath.</param>
        /// <param name="files">Comma separated files to be copied to the map reduce cluster.</param>
        /// <param name="arg">Set a program argument.</param>
        /// <param name="define">Set a Hadoop configuration variable using the syntax define=NAME=VALUE</param>
        /// <param name="statusDir">A directory where WebHCat will write the status of the Map Reduce job. If provided, it is the caller's responsibility to remove this directory when done.</param>
        /// <param name="enableLog">If statusdir is set and enablelog is "true", collect Hadoop job configuration and logs into a directory named $statusdir/logs after the job finishes. Both completed and failed attempts are logged. The layout of subdirectories in $statusdir/logs is: 
        ///                             logs/$job_id(directory for $job_id)
        ///                             logs/$job_id/job.xml.html
        ///                             logs/$job_id/$attempt_id(directory for $attempt_id)
        ///                             logs/$job_id/$attempt_id/stderr
        ///                             logs/$job_id/$attempt_id/stdout
        ///                             logs/$job_id/$attempt_id/syslog</param>
        /// <param name="callBack">Define a URL to be called upon job completion. You may embed a specific job ID into this URL using $jobId. This tag will be replaced in the callback URL with this job's job ID.</param>
        /// <param name="usehcatalog">
        /// Specify that the submitted job uses HCatalog and therefore needs to access the metastore, which requires additional steps for WebHCat to perform in a secure cluster. (See HIVE-5133.) This parameter will be introduced in Hive 0.13.0.
        /// Also, if webhcat-site.xml defines the parameters templeton.hive.archive, templeton.hive.home and templeton.hcat.home then WebHCat will ship the Hive tar to the target node where the job runs. (See HIVE-5547.) This means that Hive doesn't need to be installed on every node in the Hadoop cluster. This is independent of security, but improves manageability.
        /// The webhcat-site.xml parameters are documented in webhcat-default.xml.
        /// </param>
        /// <returns></returns>
        public async Task<Job> MapReduceJob(string jar, string @class, string libjars, string files, string arg, string define, string statusDir, string enableLog, string callBack, string usehcatalog)
        {
            List<KeyValuePair<string, string>> postParams = new List<KeyValuePair<string, string>>();
            if (!string.IsNullOrEmpty(jar))
                postParams.Add(new KeyValuePair<string, string>("jar", jar));
            if (!string.IsNullOrEmpty(@class))
                postParams.Add(new KeyValuePair<string, string>("class", @class));
            if (!string.IsNullOrEmpty(libjars))
                postParams.Add(new KeyValuePair<string, string>("libjars", libjars));
            if (!string.IsNullOrEmpty(files))
                postParams.Add(new KeyValuePair<string, string>("files", files));
            if (!string.IsNullOrEmpty(define))
                postParams.Add(new KeyValuePair<string, string>("define", define));
            if (!string.IsNullOrEmpty(arg))
                postParams.Add(new KeyValuePair<string, string>("arg", arg));
            if (!string.IsNullOrEmpty(statusDir))
                postParams.Add(new KeyValuePair<string, string>("statusDir", statusDir));
            if (!string.IsNullOrEmpty(enableLog))
                postParams.Add(new KeyValuePair<string, string>("enableLog", enableLog));
            if (!string.IsNullOrEmpty(callBack))
                postParams.Add(new KeyValuePair<string, string>("callBack", callBack));
            if (!string.IsNullOrEmpty(usehcatalog))
                postParams.Add(new KeyValuePair<string, string>("usehcatalog", usehcatalog));

            Job tableRename = await Post<Job>(RequestURL.MapReduceJob(), postParams);
            return tableRename;
        }
        #endregion

        /// <summary>
        /// Creates and queues a standard Hadoop MapReduce job.
        /// </summary>
        /// <param name="execute">String containing an entire, short Pig program to run.</param>
        /// <param name="file">HDFS file name of a Pig program to run.</param>
        /// <param name="arg">Set a program argument.</param>
        /// <param name="files">Comma separated files to be copied to the map reduce cluster.</param>
        /// <param name="statusDir">A directory where WebHCat will write the status of the Map Reduce job. If provided, it is the caller's responsibility to remove this directory when done.</param>
        /// <param name="enableLog">If statusdir is set and enablelog is "true", collect Hadoop job configuration and logs into a directory named $statusdir/logs after the job finishes. Both completed and failed attempts are logged. The layout of subdirectories in $statusdir/logs is: 
        ///                             logs/$job_id(directory for $job_id)
        ///                             logs/$job_id/job.xml.html
        ///                             logs/$job_id/$attempt_id(directory for $attempt_id)
        ///                             logs/$job_id/$attempt_id/stderr
        ///                             logs/$job_id/$attempt_id/stdout
        ///                             logs/$job_id/$attempt_id/syslog</param>
        /// <param name="callBack">Define a URL to be called upon job completion. You may embed a specific job ID into this URL using $jobId. This tag will be replaced in the callback URL with this job's job ID.</param>
        /// <param name="usehcatalog">
        /// Specify that the submitted job uses HCatalog and therefore needs to access the metastore, which requires additional steps for WebHCat to perform in a secure cluster. (See HIVE-5133.) This parameter will be introduced in Hive 0.13.0.
        /// Also, if webhcat-site.xml defines the parameters templeton.hive.archive, templeton.hive.home and templeton.hcat.home then WebHCat will ship the Hive tar to the target node where the job runs. (See HIVE-5547.) This means that Hive doesn't need to be installed on every node in the Hadoop cluster. This is independent of security, but improves manageability.
        /// The webhcat-site.xml parameters are documented in webhcat-default.xml.
        /// </param>
        /// <returns></returns>
        public async Task<Job> PigJob(string execute, string file, string arg, string files, string statusDir, string enableLog, string callBack, string usehcatalog)
        {
            List<KeyValuePair<string, string>> postParams = new List<KeyValuePair<string, string>>();
            if (!string.IsNullOrEmpty(execute))
                postParams.Add(new KeyValuePair<string, string>("execute", execute));
            if (!string.IsNullOrEmpty(file))
                postParams.Add(new KeyValuePair<string, string>("file", file));
            if (!string.IsNullOrEmpty(arg))
                postParams.Add(new KeyValuePair<string, string>("arg", arg));
            if (!string.IsNullOrEmpty(files))
                postParams.Add(new KeyValuePair<string, string>("files", files));
            if (!string.IsNullOrEmpty(statusDir))
                postParams.Add(new KeyValuePair<string, string>("statusDir", statusDir));
            if (!string.IsNullOrEmpty(enableLog))
                postParams.Add(new KeyValuePair<string, string>("enableLog", enableLog));
            if (!string.IsNullOrEmpty(callBack))
                postParams.Add(new KeyValuePair<string, string>("callBack", callBack));
            if (!string.IsNullOrEmpty(usehcatalog))
                postParams.Add(new KeyValuePair<string, string>("usehcatalog", usehcatalog));

            Job tableRename = await Post<Job>(RequestURL.PigJob(), postParams);
            return tableRename;
        }

        /// <summary>
        /// Creates and queues a standard Hadoop MapReduce job.
        /// </summary>
        /// <param name="execute">String containing an entire, short Pig program to run.</param>
        /// <param name="file">HDFS file name of a Pig program to run.</param>
        /// <param name="define">Set a Hive configuration variable using the syntax define=NAME=VALUE. See a note CURL and "=".</param>
        /// <param name="arg">Set a program argument.</param>
        /// <param name="files">Comma separated files to be copied to the map reduce cluster.</param>
        /// <param name="statusDir">A directory where WebHCat will write the status of the Map Reduce job. If provided, it is the caller's responsibility to remove this directory when done.</param>
        /// <param name="enableLog">If statusdir is set and enablelog is "true", collect Hadoop job configuration and logs into a directory named $statusdir/logs after the job finishes. Both completed and failed attempts are logged. The layout of subdirectories in $statusdir/logs is: 
        ///                             logs/$job_id(directory for $job_id)
        ///                             logs/$job_id/job.xml.html
        ///                             logs/$job_id/$attempt_id(directory for $attempt_id)
        ///                             logs/$job_id/$attempt_id/stderr
        ///                             logs/$job_id/$attempt_id/stdout
        ///                             logs/$job_id/$attempt_id/syslog</param>
        /// <param name="callBack">Define a URL to be called upon job completion. You may embed a specific job ID into this URL using $jobId. This tag will be replaced in the callback URL with this job's job ID.</param>
        /// <returns></returns>
        public async Task<Job> HiveJob(string execute, string file, string define, string arg, string files, string statusDir, string enableLog, string callBack)
        {
            List<KeyValuePair<string, string>> postParams = new List<KeyValuePair<string, string>>();
            if (!string.IsNullOrEmpty(execute))
                postParams.Add(new KeyValuePair<string, string>("execute", execute));
            if (!string.IsNullOrEmpty(file))
                postParams.Add(new KeyValuePair<string, string>("file", file));
            if (!string.IsNullOrEmpty(define))
                postParams.Add(new KeyValuePair<string, string>("define", define));
            if (!string.IsNullOrEmpty(arg))
                postParams.Add(new KeyValuePair<string, string>("arg", arg));
            if (!string.IsNullOrEmpty(files))
                postParams.Add(new KeyValuePair<string, string>("files", files));
            if (!string.IsNullOrEmpty(statusDir))
                postParams.Add(new KeyValuePair<string, string>("statusDir", statusDir));
            if (!string.IsNullOrEmpty(enableLog))
                postParams.Add(new KeyValuePair<string, string>("enableLog", enableLog));
            if (!string.IsNullOrEmpty(callBack))
                postParams.Add(new KeyValuePair<string, string>("callBack", callBack));

            Job tableRename = await Post<Job>(RequestURL.HiveJob(), postParams);
            return tableRename;
        }

        #endregion
    }
}