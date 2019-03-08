using System;
namespace SharpHive.Models.WebHCat.WebHCatAPIReference.Response.Jobs
{
    public class MapReduceStreamingJob : Errors.Error
    {
        public string id { get; set; }
        public Errors.ErrorExec info { get; set; }

        public new string ToString()
        {
            if (string.IsNullOrEmpty(error))
                return string.Format("{0}\n{1}", id ?? string.Empty, info?.ToString());
            else
                return string.Format("Error: {0}", error);
        }
    }
}
