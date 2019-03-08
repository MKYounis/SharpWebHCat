using System.Collections.Generic;

namespace SharpHive.Models.WebHCat.WebHCatAPIReference.Request.HTable
{
    public class ClusteredBy
    {
        public List<string> columnNames { get; set; }
        public List<SortedBy> sortedBy { get; set; }
        public int numberOfBuckets { get; set; }
    }
}
