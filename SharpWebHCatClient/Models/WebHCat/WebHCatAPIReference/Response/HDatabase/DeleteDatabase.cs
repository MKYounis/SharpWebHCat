using SharpHive.Models.WebHCat.WebHCatAPIReference.Response.Errors;

namespace SharpHive.Models.WebHCat.WebHCatAPIReference.Response.HDatabase
{
    public class DeleteDatabase : ErrorDetails
    {
        public string database { get; set; }
    }
}
