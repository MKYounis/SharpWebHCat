using SharpHive.Models.WebHCat.WebHCatAPIReference.Response.Errors;

namespace SharpHive.Models.WebHCat.WebHCatAPIReference.Response.HDatabase
{
    public class CreateDatabase : ErrorCreateTable
    {
        public string database { get; set; }
    }
}
