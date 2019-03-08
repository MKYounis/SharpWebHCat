using System.Collections.Generic;
using SharpHive.Models.WebHCat.WebHCatAPIReference.Response.Errors;

namespace SharpHive.Models.WebHCat.WebHCatAPIReference.Response.HTable
{
    public class TableDescribe : TableDDL
    {
        public List<Column> columns { get; set; }
    }
}
