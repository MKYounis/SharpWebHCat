using System;
using SharpHive.Models.WebHCat.WebHCatAPIReference.Response.Errors;

namespace SharpHive.Models.WebHCat.WebHCatAPIReference.Response.HTable
{
    public class TableDDL : Error
    {
        public string table { get; set; }
        public string database { get; set; }
    }
}