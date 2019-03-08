using System;
namespace SharpHive.Models.WebHCat.WebHCatAPIReference.Request.HTable
{
    public class CreatePartitionParams : CreateTableLikeParams
    {
        public string location { get; set; }
    }
}
