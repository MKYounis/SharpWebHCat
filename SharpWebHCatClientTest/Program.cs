using SharpHive.Managers;
using SharpHive.Models.WebHCat.WebHCatAPIReference.Response.General;
using SharpHive.Models.WebHCat.WebHCatAPIReference.Response.Jobs;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml;

namespace SharpWebHCatClientTest
{
    class Program
    {
             private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(Program));
       static void Main(string[] args)
        {
            XmlDocument log4netConfig = new XmlDocument();
            log4netConfig.Load(File.OpenRead("log4net.config"));

            var repo = log4net.LogManager.CreateRepository(
                Assembly.GetEntryAssembly(), typeof(log4net.Repository.Hierarchy.Hierarchy));

            log4net.Config.XmlConfigurator.Configure(repo, log4netConfig["log4net"]);

            log.Info("Application - Main is invoked");
            Console.WriteLine("Hello World!");
        }

        public async Task<WebHCatStatus> Test()
        {
            WebHCatRequestsManager hManager = new WebHCatRequestsManager();
            WebHCatStatus status = await hManager.GetConnectionStatus();
            //Logger.Log(string.Format("WebHCat Version: {0}, status: {1}", status.version, status.status));

            //Job job = await hManager.MapReduceJob(
            //    string.Empty,
            //    string.Empty,
            //    string.Empty,
            //    string.Empty,
            //    string.Empty,
            //    string.Empty,
            //    "/tmp/status",
            //    string.Empty,
            //    string.Empty,
            //    string.Empty
            //);
            //Logger.Log(job.ToString());

            MapReduceStreamingJob job = await hManager.MapReduceStreamingJob(
                string.Empty,//"/tmp/test",
                "/tmp/output/1",
                "/bin/cat",
                "/usr/bin/wc -w",
                string.Empty,
                string.Empty,
                string.Empty,
                string.Empty,
                "/tmp/status",
                string.Empty,
                string.Empty
            );
            //Logger.Log(job.ToString());
            //TableProperty createProperty = await hManager.CreateTableProperty("testing", "table1", "animal", new PropertyValue(){value="cat"});
            //if (string.IsNullOrEmpty(createProperty.error))
            //{
            //    Logger.Log(string.Format("Column {0}.{1}.{2} has been created", createProperty.database, createProperty.table, "animal"));
            //}
            //else
            //{
            //    Logger.Log(((Error)createProperty).ToString());
            //}

            //TableProperties tableProperties = await hManager.ListTableProperties("testing", "table1");
            //if (string.IsNullOrEmpty(tableProperties.error))
            //{
            //    Logger.Log(string.Format("table {0}.{1} properties:\n{2}", tableProperties.database, tableProperties.table, tableProperties.properties));

            //}
            //else
            //{
            //    Logger.Log(((Error)tableProperties).ToString());
            //}

            //Models.WebHCat.WebHCatAPIReference.Request.HTable.Column column = new Models.WebHCat.WebHCatAPIReference.Request.HTable.Column(){
            //    name = "date_of_birth",
            //    type = "timestamp",
            //    comment = string.Empty
            //};
            //TableColumnDDL createColumn = await hManager.CreateTableColumn("testing", "table1", column);
            //if (string.IsNullOrEmpty(createColumn.error))
            //{
            //    Logger.Log(string.Format("Column {0}.{1}.{2} has been created", createColumn.database, createColumn.table, createColumn.column));
            //}
            //else
            //{
            //    Logger.Log(((Error)createColumn).ToString());
            //}

            //TableColumnsList tableColumnsList = await hManager.ListTableColumns("testing", "table1");
            //if (string.IsNullOrEmpty(tableColumnsList.error))
            //{
            //    Logger.Log(string.Format("table {0}.{1} columns:", tableColumnsList.database, tableColumnsList.table));
            //    tableColumnsList.columns.ForEach( c => {
            //        Logger.Log(string.Format("{0}({1}) => {2}", c.name, c.type, c.comment));
            //        Task<ColumnDescribe> describeTableColumnTask = hManager.DescribeTableColumn(tableColumnsList.database, tableColumnsList.table, c.name);
            //        Task.WaitAll(describeTableColumnTask);
            //        var describeColumn = describeTableColumnTask.GetAwaiter().GetResult();
            //        if(string.IsNullOrEmpty(describeColumn.error))
            //        {
            //            Logger.Log(string.Format("{0}({1}) => {2}", describeColumn.column.name, describeColumn.column.type, describeColumn.column.comment));
            //        }
            //        else
            //        {
            //            Logger.Log(((Error)describeColumn).ToString());
            //        }
            //    });
            //}
            //else
            //{
            //    Logger.Log(((Error)tableColumnsList).ToString());
            //}

            //PartitionDDL createPartition = await hManager.CreateTablePartition("testing", "table1", "family_name='123'", true, string.Empty, null, null);
            //if (string.IsNullOrEmpty(createPartition.error))
            //{
            //    Logger.Log(string.Format("Partition {0}.{1}.{2} has been created", createPartition.database, createPartition.table, createPartition.partition));
            //}
            //else
            //{
            //    Logger.Log(((Error)createPartition).ToString());
            //}

            //TablePartitionsList tablePartitionsList = await hManager.ListTablePartitions("testing", "table1");
            //if (string.IsNullOrEmpty(tablePartitionsList.error))
            //{
            //    Logger.Log(string.Format("table {0}.{1} partitions:", tablePartitionsList.database, tablePartitionsList.table));
            //    tablePartitionsList.partitions.ForEach( p => {
            //        Logger.Log(string.Format("Partition {0}:", p.name));
            //        p.values.ForEach(v => Logger.Log(string.Format("Column: {0}, Value: {1}", v.columnName, v.columnValue)));
            //        Task<PartitionDescribe> describeTablePartitionTask = hManager.DescribeTablePartition(tablePartitionsList.database, tablePartitionsList.table, p.name);
            //        Task.WaitAll(describeTablePartitionTask);
            //        var describePartition = describeTablePartitionTask.GetAwaiter().GetResult();
            //        if(string.IsNullOrEmpty(describePartition.error))
            //        {
            //            Logger.Log(string.Format("Partition {0} Describe:", describePartition.partition));
            //            Logger.Log(string.Format("Partitioned: {0}", describePartition.partitioned));
            //            Logger.Log(string.Format("Output Format: {0}", describePartition.outputFormat));
            //            Logger.Log(string.Format("Input Format: {0}", describePartition.inputFormat));
            //            foreach (var column in describePartition.columns)
            //            {
            //                Logger.Log(string.Format("Column Name: {0}, Column Type: {1}", column.name, column.type));
            //            }
            //        }
            //        else
            //        {
            //            Logger.Log(((Error)describePartition).ToString());
            //        }
            //    });
            //}
            //else
            //{
            //    Logger.Log(((Error)tablePartitionsList).ToString());
            //}

            //PartitionDDL deletePartition = await hManager.DeleteTablePartition("testing", "table1", "family_name='123'", true, null, null);
            //if (string.IsNullOrEmpty(deletePartition.error))
            //{
            //    Logger.Log(string.Format("Partition {0}.{1}.{2} has been deleted", createPartition.database, createPartition.table, createPartition.partition));
            //}
            //else
            //{
            //    Logger.Log(((Error)createPartition).ToString());
            //}

            //TableDDL createTableLike = await hManager.CreateTableLike("testing", "table1", "table3", true, "hdfs", "777");
            //if (string.IsNullOrEmpty(createTableLike.error))
            //{
            //    Logger.Log(string.Format("table {0}.{1} has been creares", createTableLike.database, createTableLike.table));
            //}
            //else
            //{
            //    Logger.Log(((Error)createTableLike).ToString());
            //}

            //TableDDL tableRename = await hManager.RenameTable("testing", "table1", "table2");
            //if (string.IsNullOrEmpty(tableRename.error))
            //{
            //    Logger.Log(string.Format("table {0}.{1} has been renamed", tableRename.database, tableRename.table));
            //}
            //else
            //{
            //    Logger.Log(((Error)tableRename).ToString());
            //}

            //TableDDL tableDelete = await hManager.DeleteTable("testing", "table1", false, string.Empty, string.Empty);
            //if (string.IsNullOrEmpty(tableDelete.error))
            //{
            //    Logger.Log(string.Format("table {0}.{1} has been deleted", tableDelete.database, tableDelete.table));
            //}
            //else
            //{
            //    Logger.Log(((Error)tableDelete).ToString());
            //}

            //TableExtended table = new TableExtended();
            //table.comment = "Just Testing";
            //table.columns = new List<Column>();
            //table.columns.Add(new Column {name="id", type="INT", comment="PK" });
            //table.columns.Add(new Column { name = "first_name", type = "string", comment = "any text" });
            //table.partitionedBy = new List<PartitionedBy>();
            //table.partitionedBy.Add(new PartitionedBy { name = "family_name", type = "string" });
            //table.clusteredBy = new ClusteredBy();
            //table.clusteredBy.columnNames = new List<string>
            //{
            //    "id"
            //};
            //table.clusteredBy.sortedBy = new List<SortedBy>
            //{
            //    new SortedBy { columnName = "id", order = "ASC" }
            //};
            //table.clusteredBy.numberOfBuckets = 10;
            //table.format = new Format
            //{
            //    storedAs = "rcfile",
            //    rowFormat = new RowFormat
            //    {
            //        fieldsTerminatedBy = "\t",
            //        linesTerminatedBy = "\n",
            //        serde = new Serde
            //        {
            //            name = "org.apache.hadoop.hive.serde2.columnar.ColumnarSerDe", properties = new { key = "value"}
            //        }
            //    }
            //};

            ////Console.WriteLine(
            ////    Newtonsoft.Json.JsonConvert.SerializeObject(table));
            //var t = await hManager.CreateTable("testing", "table1", table);
            //if(string.IsNullOrEmpty(t.error))
            //{
            //    Logger.Log(string.Format("table {0}.{1} has been created", t.database, t.table));
            //}
            //else{
            //    Logger.Log(((ErrorCreateTable)t).ToString());
            //}
            ////var result3 = await hManager.CreateDatabase("Testing", "just testing", null, null, null);
            //if (string.IsNullOrEmpty(result3.error))
            //{
            //    Logger.Log(string.Format("Database {0} created.", result3.database));
            //}
            //else
            //{
            //    Logger.Log(result3.error);
            //}

            //var result2 = await hManager.GetDatabase("Testing");
            //if (string.IsNullOrEmpty(result2.error))
            //{
            //    Logger.Log(result2.location);
            //    Logger.Log(result2.@params);
            //    Logger.Log(result2.comment);
            //    Logger.Log(result2.database);
            //}
            //else
            //{
            //    Logger.Log(result2.error);
            //}

            //(await hManager.GetDatabases()).databases.ForEach(d =>
            //{
            //Console.WriteLine(string.Format("Database: {0}", d));
            //    Task<TableList> task = hManager.ListTables(d, null);
            //    Task.WaitAll(task);
            //    task.Result.tables.ForEach(t =>
            //    {
            //        Logger.Log(string.Format("Table: {0}", t));
            //        Task<TableDescribeExtended> t2 = hManager.GetTableDescriptionExtended(d, t);
            //        Task.WaitAll(t2);
            //        t2.Result.columns?.ForEach(c => Logger.Log(string.Format("Col: {0} => {1}", c.name, c.type)));
            //    });

            //});

            //var delResult = await hManager.DeleteDatabase("delme6", true, null, null, null);
            //if (string.IsNullOrEmpty(delResult.error))
            //{
            //    Logger.Log(string.Format("Database {0} deleted.", delResult.database));
            //}
            //else
            //{
            //    Logger.Log(delResult.error);
            //}

            //var result = await hManager.GetTables("musaned");
            //if (result.exitcode == 0)
            //    Logger.Log(result.stdout);
            //else
            //    Logger.Log(result.stderr);



            return status;
        }

    }
}
