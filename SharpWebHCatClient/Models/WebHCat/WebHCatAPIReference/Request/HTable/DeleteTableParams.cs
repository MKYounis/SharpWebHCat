using System;
namespace SharpHive.Models.WebHCat.WebHCatAPIReference.Request.HTable
{
    public class DeleteTableParams : Parameters
    {
        public bool ifExists { get; set; }
    }
}
