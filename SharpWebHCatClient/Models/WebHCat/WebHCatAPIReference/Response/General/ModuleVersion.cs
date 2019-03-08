using SharpHive.Models.WebHCat.WebHCatAPIReference.Response.Errors;

namespace SharpHive.Models.WebHCat.WebHCatAPIReference.Response.General
{
    public class ModuleVersion : Error
    {
        public string module { get; set; }
        public string version { get; set; }
    }
}
