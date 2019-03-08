using System.Collections.Generic;

namespace SharpHive.Models.WebHCat.WebHCatAPIReference.Response.HTable
{
    public class TablePartitionsList : TableDDL
    {
        public List<Partition> partitions { get; set; }
    }
}
