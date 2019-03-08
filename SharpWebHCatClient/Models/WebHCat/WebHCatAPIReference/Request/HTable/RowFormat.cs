using System;
namespace SharpHive.Models.WebHCat.WebHCatAPIReference.Request.HTable
{
    public class RowFormat
    {
        public string fieldsTerminatedBy { get; set; }
        public string collectionItemsTerminatedBy { get; set; }
        public string mapKeysTerminatedBy { get; set; }
        public string linesTerminatedBy { get; set; }
        public Serde serde { get; set; }
    }
}
