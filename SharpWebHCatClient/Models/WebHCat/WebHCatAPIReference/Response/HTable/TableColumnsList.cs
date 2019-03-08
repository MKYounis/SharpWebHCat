using System.Collections.Generic;

namespace SharpHive.Models.WebHCat.WebHCatAPIReference.Response.HTable
{
    public class TableColumnsList : TableDDL
    {
        public List<Column> columns { get; set; }
    }
}
