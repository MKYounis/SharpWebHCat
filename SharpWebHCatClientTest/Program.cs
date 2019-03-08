using SharpHive.Managers;
using SharpHive.Models.WebHCat.WebHCatAPIReference.Request.HTable;
using SharpHive.Models.WebHCat.WebHCatAPIReference.Response.Errors;
using SharpHive.Models.WebHCat.WebHCatAPIReference.Response.General;
using SharpHive.Models.WebHCat.WebHCatAPIReference.Response.HTable;
using SharpHive.Models.WebHCat.WebHCatAPIReference.Response.Jobs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml;

namespace SharpWebHCatClientTest
{
    class Program
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(Program));
        private static WebHCatRequestsManager hManager = new WebHCatRequestsManager();

        static void Main(string[] args)
        {
            XmlDocument log4netConfig = new XmlDocument();
            log4netConfig.Load(File.OpenRead("log4net.config"));

            var repo = log4net.LogManager.CreateRepository(
                Assembly.GetEntryAssembly(), typeof(log4net.Repository.Hierarchy.Hierarchy));

            log4net.Config.XmlConfigurator.Configure(repo, log4netConfig["log4net"]);

            log.Info("Application - Main is invoked");

        }

        public async void CheckConnection()
        {
           
            WebHCatStatus status = await hManager.GetConnectionStatus();
            log.Info(string.Format("WebHCat Version: {0}, status: {1}", status.version, status.status));
        }
        public async void MapReduceJobTest()
        {
            Job job = await hManager.MapReduceJob(
                string.Empty,
                string.Empty,
                string.Empty,
                string.Empty,
                string.Empty,
                string.Empty,
                "/tmp/status",
                string.Empty,
                string.Empty,
                string.Empty
            );
            log.Info(job.ToString());
        }

        public async void MapReduceStreamingJobTest()
        {
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

            log.Info(job.ToString());
            TableProperty createProperty = await hManager.CreateTableProperty("testing", "table1", "animal", new PropertyValue() { value = "cat" });
            if (string.IsNullOrEmpty(createProperty.error))
            {
                log.Info(string.Format("Column {0}.{1}.{2} has been created", createProperty.database, createProperty.table, "animal"));
            }
            else
            {
                log.Info(((Error)createProperty).ToString());
            }
        }

        public async void ListTablePropertiesTest()
        {
            TableProperties tableProperties = await hManager.ListTableProperties("testing", "table1");
            if (string.IsNullOrEmpty(tableProperties.error))
            {
                log.Info(string.Format("table {0}.{1} properties:\n{2}", tableProperties.database, tableProperties.table, tableProperties.properties));

            }
            else
            {
                log.Info(((Error)tableProperties).ToString());
            }
        }

        public async void CreateTableColumnTes()
        {
            SharpHive.Models.WebHCat.WebHCatAPIReference.Request.HTable.Column column = new SharpHive.Models.WebHCat.WebHCatAPIReference.Request.HTable.Column()
            {
                name = "date_of_birth",
                type = "timestamp",
                comment = string.Empty
            };
            TableColumnDDL createColumn = await hManager.CreateTableColumn("testing", "table1", column);
            if (string.IsNullOrEmpty(createColumn.error))
            {
                log.Info(string.Format("Column {0}.{1}.{2} has been created", createColumn.database, createColumn.table, createColumn.column));
            }
            else
            {
                log.Info(((Error)createColumn).ToString());
            }
        }

        public async void ListTableColumnsTest()
        {
            TableColumnsList tableColumnsList = await hManager.ListTableColumns("testing", "table1");
            if (string.IsNullOrEmpty(tableColumnsList.error))
            {
                log.Info(string.Format("table {0}.{1} columns:", tableColumnsList.database, tableColumnsList.table));
                tableColumnsList.columns.ForEach(c =>
                {
                    log.Info(string.Format("{0}({1}) => {2}", c.name, c.type, c.comment));
                    Task<ColumnDescribe> describeTableColumnTask = hManager.DescribeTableColumn(tableColumnsList.database, tableColumnsList.table, c.name);
                    Task.WaitAll(describeTableColumnTask);
                    var describeColumn = describeTableColumnTask.GetAwaiter().GetResult();
                    if (string.IsNullOrEmpty(describeColumn.error))
                    {
                        log.Info(string.Format("{0}({1}) => {2}", describeColumn.column.name, describeColumn.column.type, describeColumn.column.comment));
                    }
                    else
                    {
                        log.Info(((Error)describeColumn).ToString());
                    }
                });
            }
            else
            {
                log.Info(((Error)tableColumnsList).ToString());
            }
        }

        public async void CreateTablePartitionTest()
        {
            PartitionDDL createPartition = await hManager.CreateTablePartition("testing", "table1", "family_name='123'", true, string.Empty, null, null);
            if (string.IsNullOrEmpty(createPartition.error))
            {
                log.Info(string.Format("Partition {0}.{1}.{2} has been created", createPartition.database, createPartition.table, createPartition.partition));
            }
            else
            {
                log.Info(((Error)createPartition).ToString());
            }
        }

        public async void ListTablePartitionsTest()
        {
            TablePartitionsList tablePartitionsList = await hManager.ListTablePartitions("testing", "table1");
            if (string.IsNullOrEmpty(tablePartitionsList.error))
            {
                log.Info(string.Format("table {0}.{1} partitions:", tablePartitionsList.database, tablePartitionsList.table));
                tablePartitionsList.partitions.ForEach(p =>
                {
                    log.Info(string.Format("Partition {0}:", p.name));
                    p.values.ForEach(v => log.Info(string.Format("Column: {0}, Value: {1}", v.columnName, v.columnValue)));
                    Task<PartitionDescribe> describeTablePartitionTask = hManager.DescribeTablePartition(tablePartitionsList.database, tablePartitionsList.table, p.name);
                    Task.WaitAll(describeTablePartitionTask);
                    var describePartition = describeTablePartitionTask.GetAwaiter().GetResult();
                    if (string.IsNullOrEmpty(describePartition.error))
                    {
                        log.Info(string.Format("Partition {0} Describe:", describePartition.partition));
                        log.Info(string.Format("Partitioned: {0}", describePartition.partitioned));
                        log.Info(string.Format("Output Format: {0}", describePartition.outputFormat));
                        log.Info(string.Format("Input Format: {0}", describePartition.inputFormat));
                        foreach (var column in describePartition.columns)
                        {
                            log.Info(string.Format("Column Name: {0}, Column Type: {1}", column.name, column.type));
                        }
                    }
                    else
                    {
                        log.Info(((Error)describePartition).ToString());
                    }
                });
            }
            else
            {
                log.Info(((Error)tablePartitionsList).ToString());
            }
        }

        public async void DeleteTablePartitionTest()
        {
            PartitionDDL deletePartition = await hManager.DeleteTablePartition("testing", "table1", "family_name='123'", true, null, null);
            if (string.IsNullOrEmpty(deletePartition.error))
            {
                log.Info(string.Format("Partition {0}.{1}.{2} has been deleted", deletePartition.database, deletePartition.table, deletePartition.partition));
            }
            else
            {
                log.Info(((Error)deletePartition).ToString());
            }
        }

        public async void CreateTableLikeTest()
        {
            TableDDL createTableLike = await hManager.CreateTableLike("testing", "table1", "table3", true, "hdfs", "777");
            if (string.IsNullOrEmpty(createTableLike.error))
            {
                log.Info(string.Format("table {0}.{1} has been creares", createTableLike.database, createTableLike.table));
            }
            else
            {
                log.Info(((Error)createTableLike).ToString());
            }
        }

        public async void RenameTableTest()
        {
            TableDDL tableRename = await hManager.RenameTable("testing", "table1", "table2");
            if (string.IsNullOrEmpty(tableRename.error))
            {
                log.Info(string.Format("table {0}.{1} has been renamed", tableRename.database, tableRename.table));
            }
            else
            {
                log.Info(((Error)tableRename).ToString());
            }
        }

        public async void DeleteTableTest()
        {
            TableDDL tableDelete = await hManager.DeleteTable("testing", "table1", false, string.Empty, string.Empty);
            if (string.IsNullOrEmpty(tableDelete.error))
            {
                log.Info(string.Format("table {0}.{1} has been deleted", tableDelete.database, tableDelete.table));
            }
            else
            {
                log.Info(((Error)tableDelete).ToString());
            }
        }

        public async void CreateTableTest()
        {
            TableExtended table = new TableExtended();
            table.comment = "Just Testing";
            table.columns = new List<SharpHive.Models.WebHCat.WebHCatAPIReference.Request.HTable.Column>();
            table.columns.Add(new SharpHive.Models.WebHCat.WebHCatAPIReference.Request.HTable.Column { name = "id", type = "INT", comment = "PK" });
            table.columns.Add(new SharpHive.Models.WebHCat.WebHCatAPIReference.Request.HTable.Column { name = "first_name", type = "string", comment = "any text" });
            table.partitionedBy = new List<PartitionedBy>();
            table.partitionedBy.Add(new PartitionedBy { name = "family_name", type = "string" });
            table.clusteredBy = new ClusteredBy();
            table.clusteredBy.columnNames = new List<string>
            {
                "id"
            };
            table.clusteredBy.sortedBy = new List<SortedBy>
            {
                new SortedBy { columnName = "id", order = "ASC" }
            };
            table.clusteredBy.numberOfBuckets = 10;
            table.format = new Format
            {
                storedAs = "rcfile",
                rowFormat = new RowFormat
                {
                    fieldsTerminatedBy = "\t",
                    linesTerminatedBy = "\n",
                    serde = new Serde
                    {
                        name = "org.apache.hadoop.hive.serde2.columnar.ColumnarSerDe",
                        properties = new { key = "value" }
                    }
                }
            };

            var t = await hManager.CreateTable("testing", "table1", table);
            if (string.IsNullOrEmpty(t.error))
            {
                log.Info(string.Format("table {0}.{1} has been created", t.database, t.table));
            }
            else
            {
                log.Info(((ErrorCreateTable)t).ToString());
            }

        }

        public async void CreateDatabaseTest()
        {
            var result3 = await hManager.CreateDatabase("Testing", "just testing", null, null, null);
            if (string.IsNullOrEmpty(result3.error))
            {
                log.Info(string.Format("Database {0} created.", result3.database));
            }
            else
            {
                log.Info(result3.error);
            }
        }

        public async void GetDatabaseTest()
        {
            var result2 = await hManager.GetDatabase("Testing");
            if (string.IsNullOrEmpty(result2.error))
            {
                log.Info(result2.location);
                log.Info(result2.@params);
                log.Info(result2.comment);
                log.Info(result2.database);
            }
            else
            {
                log.Info(result2.error);
            }
        }

        public async void DeleteDatabaseTest()
        {

            var delResult = await hManager.DeleteDatabase("delme6", true, null, null, null);
            if (string.IsNullOrEmpty(delResult.error))
            {
                log.Info(string.Format("Database {0} deleted.", delResult.database));
            }
            else
            {
                log.Info(delResult.error);
            }
        }

        public async void ListTablesTest()
        {
            var result = await hManager.ListTables("test","*");
            if (string.IsNullOrEmpty(result.error))
            {
                log.Info(string.Format("Database {0} tables:", result.database));
                foreach (var item in result.tables)
                {
                    log.Info(string.Format("{0}", item));
                }
            }
            else
            {
                log.Info(result.error);
            }
        }
    }
}
