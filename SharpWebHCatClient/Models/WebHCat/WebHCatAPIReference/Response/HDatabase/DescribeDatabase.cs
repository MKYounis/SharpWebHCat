using SharpHive.Models.WebHCat.WebHCatAPIReference.Response.Errors;

namespace SharpHive.Models.WebHCat.WebHCatAPIReference.Response.HDatabase
{
    public class DescribeDatabase : Error
    {
        public string location { get; set; }
        public string @params { get; set; }
        public string comment { get; set; }
        public string database { get; set; }
    }
}
