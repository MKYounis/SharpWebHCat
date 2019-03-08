using System;
namespace SharpHive.Models.WebHCat.WebHCatAPIReference.Response.HTable
{
    public class TableCreate : Errors.ErrorCreateTable
    {
        public string table { get; set; }
        public string database { get; set; }
    }
}
