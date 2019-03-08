using System;
namespace SharpHive.WebHCatAPIReference.Response.HTable
{
    public class PartitionCreate
    {
        public string partition { get; set; }
        public string table { get; set; }
        public string database { get; set; }
    }
}
