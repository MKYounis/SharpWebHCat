using System.Collections.Generic;
using SharpHive.Models.WebHCat.WebHCatAPIReference.Request.HTable;

namespace SharpHive.Models.WebHCat.WebHCatAPIReference.Response.HTable
{
    public class TableDescribeExtended : TableDescribe
    {
        public bool partitioned { get; set; }
        public string location { get; set; }
        public string outputFormat { get; set; }
        public string owner { get; set; }
        public List<PartitionColumn> partitionColumns { get; set; }
        public string inputFormat { get; set; }
    }
}
