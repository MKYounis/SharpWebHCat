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
        private static WebHCatRequestsManager hManager = new WebHCatRequestsManager("http://webhcat:50111", "v1", "hive");

        static void Main(string[] args)
        {


        }

        public async void CheckConnection()
        {
           
            WebHCatStatus status = await hManager.GetConnectionStatus();
            Console.WriteLine(string.Format("WebHCat Version: {0}, status: {1}", status.version, status.status));
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
            Console.WriteLine(job.ToString());
        }

        public async void MapReduceStreamingJobTest()
        {
            MapReduceStreamingJob job = await hManager.MapReduceStreamingJob(
                "/tmp/test",
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

            Console.WriteLine(job.ToString());
            TableProperty createProperty = await hManager.CreateTableProperty("testing", "table1", "animal", new PropertyValue() { value = "cat" });
            if (string.IsNullOrEmpty(createProperty.error))
            {
                Console.WriteLine(string.Format("Column {0}.{1}.{2} has been created", createProperty.database, createProperty.table, "animal"));
            }
            else
            {
                Console.WriteLine(((Error)createProperty).ToString());
            }
        }

        public async void ListTablePropertiesTest()
        {
            TableProperties tableProperties = await hManager.ListTableProperties("testing", "table1");
            if (string.IsNullOrEmpty(tableProperties.error))
            {
                Console.WriteLine(string.Format("table {0}.{1} properties:\n{2}", tableProperties.database, tableProperties.table, tableProperties.properties));

            }
            else
            {
                Console.WriteLine(((Error)tableProperties).ToString());
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
                Console.WriteLine(string.Format("Column {0}.{1}.{2} has been created", createColumn.database, createColumn.table, createColumn.column));
            }
            else
            {
                Console.WriteLine(((Error)createColumn).ToString());
            }
        }

        public async void ListTableColumnsTest()
        {
            TableColumnsList tableColumnsList = await hManager.ListTableColumns("testing", "table1");
            if (string.IsNullOrEmpty(tableColumnsList.error))
            {
                Console.WriteLine(string.Format("table {0}.{1} columns:", tableColumnsList.database, tableColumnsList.table));
                tableColumnsList.columns.ForEach(c =>
                {
                    Console.WriteLine(string.Format("{0}({1}) => {2}", c.name, c.type, c.comment));
                    Task<ColumnDescribe> describeTableColumnTask = hManager.DescribeTableColumn(tableColumnsList.database, tableColumnsList.table, c.name);
                    Task.WaitAll(describeTableColumnTask);
                    var describeColumn = describeTableColumnTask.GetAwaiter().GetResult();
                    if (string.IsNullOrEmpty(describeColumn.error))
                    {
                        Console.WriteLine(string.Format("{0}({1}) => {2}", describeColumn.column.name, describeColumn.column.type, describeColumn.column.comment));
                    }
                    else
                    {
                        Console.WriteLine(((Error)describeColumn).ToString());
                    }
                });
            }
            else
            {
                Console.WriteLine(((Error)tableColumnsList).ToString());
            }
        }

        public async void CreateTablePartitionTest()
        {
            PartitionDDL createPartition = await hManager.CreateTablePartition("testing", "table1", "family_name='123'", true, string.Empty, null, null);
            if (string.IsNullOrEmpty(createPartition.error))
            {
                Console.WriteLine(string.Format("Partition {0}.{1}.{2} has been created", createPartition.database, createPartition.table, createPartition.partition));
            }
            else
            {
                Console.WriteLine(((Error)createPartition).ToString());
            }
        }

        public async void ListTablePartitionsTest()
        {
            TablePartitionsList tablePartitionsList = await hManager.ListTablePartitions("testing", "table1");
            if (string.IsNullOrEmpty(tablePartitionsList.error))
            {
                Console.WriteLine(string.Format("table {0}.{1} partitions:", tablePartitionsList.database, tablePartitionsList.table));
                tablePartitionsList.partitions.ForEach(p =>
                {
                    Console.WriteLine(string.Format("Partition {0}:", p.name));
                    p.values.ForEach(v => Console.WriteLine(string.Format("Column: {0}, Value: {1}", v.columnName, v.columnValue)));
                    Task<PartitionDescribe> describeTablePartitionTask = hManager.DescribeTablePartition(tablePartitionsList.database, tablePartitionsList.table, p.name);
                    Task.WaitAll(describeTablePartitionTask);
                    var describePartition = describeTablePartitionTask.GetAwaiter().GetResult();
                    if (string.IsNullOrEmpty(describePartition.error))
                    {
                        Console.WriteLine(string.Format("Partition {0} Describe:", describePartition.partition));
                        Console.WriteLine(string.Format("Partitioned: {0}", describePartition.partitioned));
                        Console.WriteLine(string.Format("Output Format: {0}", describePartition.outputFormat));
                        Console.WriteLine(string.Format("Input Format: {0}", describePartition.inputFormat));
                        foreach (var column in describePartition.columns)
                        {
                            Console.WriteLine(string.Format("Column Name: {0}, Column Type: {1}", column.name, column.type));
                        }
                    }
                    else
                    {
                        Console.WriteLine(((Error)describePartition).ToString());
                    }
                });
            }
            else
            {
                Console.WriteLine(((Error)tablePartitionsList).ToString());
            }
        }

        public async void DeleteTablePartitionTest()
        {
            PartitionDDL deletePartition = await hManager.DeleteTablePartition("testing", "table1", "family_name='123'", true, null, null);
            if (string.IsNullOrEmpty(deletePartition.error))
            {
                Console.WriteLine(string.Format("Partition {0}.{1}.{2} has been deleted", deletePartition.database, deletePartition.table, deletePartition.partition));
            }
            else
            {
                Console.WriteLine(((Error)deletePartition).ToString());
            }
        }

        public async void CreateTableLikeTest()
        {
            TableDDL createTableLike = await hManager.CreateTableLike("testing", "table1", "table3", true, "hdfs", "777");
            if (string.IsNullOrEmpty(createTableLike.error))
            {
                Console.WriteLine(string.Format("table {0}.{1} has been creares", createTableLike.database, createTableLike.table));
            }
            else
            {
                Console.WriteLine(((Error)createTableLike).ToString());
            }
        }

        public async void RenameTableTest()
        {
            TableDDL tableRename = await hManager.RenameTable("testing", "table1", "table2");
            if (string.IsNullOrEmpty(tableRename.error))
            {
                Console.WriteLine(string.Format("table {0}.{1} has been renamed", tableRename.database, tableRename.table));
            }
            else
            {
                Console.WriteLine(((Error)tableRename).ToString());
            }
        }

        public async void DeleteTableTest()
        {
            TableDDL tableDelete = await hManager.DeleteTable("testing", "table1", false, string.Empty, string.Empty);
            if (string.IsNullOrEmpty(tableDelete.error))
            {
                Console.WriteLine(string.Format("table {0}.{1} has been deleted", tableDelete.database, tableDelete.table));
            }
            else
            {
                Console.WriteLine(((Error)tableDelete).ToString());
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
                Console.WriteLine(string.Format("table {0}.{1} has been created", t.database, t.table));
            }
            else
            {
                Console.WriteLine(((ErrorCreateTable)t).ToString());
            }

        }

        public async void CreateDatabaseTest()
        {
            var result3 = await hManager.CreateDatabase("Testing", "just testing", null, null, null);
            if (string.IsNullOrEmpty(result3.error))
            {
                Console.WriteLine(string.Format("Database {0} created.", result3.database));
            }
            else
            {
                Console.WriteLine(result3.error);
            }
        }

        public async void GetDatabaseTest()
        {
            var result2 = await hManager.GetDatabase("Testing");
            if (string.IsNullOrEmpty(result2.error))
            {
                Console.WriteLine(result2.location);
                Console.WriteLine(result2.@params);
                Console.WriteLine(result2.comment);
                Console.WriteLine(result2.database);
            }
            else
            {
                Console.WriteLine(result2.error);
            }
        }

        public async void DeleteDatabaseTest()
        {

            var delResult = await hManager.DeleteDatabase("delme6", true, null, null, null);
            if (string.IsNullOrEmpty(delResult.error))
            {
                Console.WriteLine(string.Format("Database {0} deleted.", delResult.database));
            }
            else
            {
                Console.WriteLine(delResult.error);
            }
        }

        public async void ListTablesTest()
        {
            var result = await hManager.ListTables("test","*");
            if (string.IsNullOrEmpty(result.error))
            {
                Console.WriteLine(string.Format("Database {0} tables:", result.database));
                foreach (var item in result.tables)
                {
                    Console.WriteLine(string.Format("{0}", item));
                }
            }
            else
            {
                Console.WriteLine(result.error);
            }
        }
    }
}
