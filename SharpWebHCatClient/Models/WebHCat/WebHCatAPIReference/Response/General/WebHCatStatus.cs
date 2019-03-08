using SharpHive.Models.WebHCat.WebHCatAPIReference.Response.Errors;

namespace SharpHive.Models.WebHCat.WebHCatAPIReference.Response.General
{
    public class WebHCatStatus : Error
    {
        public string version { get; set; }
        public string status { get; set; }
    }
}
