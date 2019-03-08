using System;
using System.Collections.Generic;
using System.Text;

namespace SharpHive.Models.WebHCat.WebHCatAPIReference.Request.HDatabase
{
    public class DeleteDatabaseParam : Parameters
    {
        /// <summary>
        /// Hive returns an error if the database specified does not exist, unless ifExists is set to true.
        /// </summary>
        public bool ifExists { get; set; }
        /// <summary>
        /// Parameter set to either "restrict" or "cascade". Restrict will remove the schema if all the tables are empty. Cascade removes everything including data and definitions.
        /// </summary>
        public string option { get; set; }
    }
}
