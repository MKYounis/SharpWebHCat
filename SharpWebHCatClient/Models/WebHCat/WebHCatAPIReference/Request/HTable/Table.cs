using System.Collections.Generic;
using SharpHive.Models.WebHCat.WebHCatAPIReference.Response.Errors;

namespace SharpHive.Models.WebHCat.WebHCatAPIReference.Request.HTable
{
    public class Table
    {
        public string comment { get; set; }
        public List<Column> columns { get; set; }
        public List<PartitionedBy> partitionedBy { get; set; }
        public Format format { get; set; }
    }
}
