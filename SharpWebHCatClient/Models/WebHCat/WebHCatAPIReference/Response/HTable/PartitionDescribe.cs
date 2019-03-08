using System.Collections.Generic;

namespace SharpHive.Models.WebHCat.WebHCatAPIReference.Response.HTable
{
    public class PartitionDescribe : TableDDL
    {
        public bool partitioned { get; set; }
        public string location { get; set; }
        public string outputFormat { get; set; }
        public List<Column> columns { get; set; }
        public string owner { get; set; }
        public List<PartitionColumn> partitionColumns { get; set; }
        public string inputFormat { get; set; }
        public string partition { get; set; }
    }
}
