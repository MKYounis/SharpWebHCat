using System;
namespace SharpHive.Models.WebHCat.WebHCatAPIReference.Request
{
    public class Parameters
    {
        /// <summary>
        /// The user group to use
        /// </summary>
        /// <value>The group.</value>
        public string group { get; set; }

        /// <summary>
        /// The permissions string to use. The format is "rwxrw-r-x".
        /// </summary>
        /// <value>The permissions.</value>
        public string permissions { get; set; }
    }
}
