namespace SharpHive.Models.WebHCat.WebHCatAPIReference.Request.HDatabase
{
    public class CreateDatabaseParam : Parameters
    {
        /// <summary>
        /// The database location
        /// </summary>
        public string location { get; set; }
        /// <summary>
        /// A comment for the database, like a description
        /// </summary>
        public string comment { get; set; }
    }
}
