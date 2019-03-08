using System.Collections.Generic;

using SharpHive.Models.WebHCat.WebHCatAPIReference.Response.Errors;

namespace SharpHive.Models.WebHCat.WebHCatAPIReference.Response.General
{
    public class SupportedVersions : Error
    {
        public List<string> supportedVersions { get; set; }
        public string version { get; set; }
    }
}
