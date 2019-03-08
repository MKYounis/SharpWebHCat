using System.Collections.Generic;

namespace SharpHive.Models.WebHCat.WebHCatAPIReference.Response.HTable
{
    public class Partition
    {
        public List<Value> values { get; set; }
        public string name { get; set; }
    }
}
