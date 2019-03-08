using System.Collections.Generic;

namespace SharpHive.Models.WebHCat.WebHCatAPIReference.Response.HTable
{
    public class TableList : Errors.Error 
    {
        public List<string> tables { get; set; }
        public string database { get; set; }
    }
}
