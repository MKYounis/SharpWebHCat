using System.Collections.Generic;

using SharpHive.Models.WebHCat.WebHCatAPIReference.Response.Errors;

namespace SharpHive.Models.WebHCat.WebHCatAPIReference.Response.HDatabase
{
    public class DatabasesList : Error
    {
        public List<string> databases { get; set; }
    }
}
