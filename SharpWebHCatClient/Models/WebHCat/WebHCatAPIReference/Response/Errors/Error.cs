using System.Text;
namespace SharpHive.Models.WebHCat.WebHCatAPIReference.Response.Errors
{
    public class Error
    {
        public string error { get; set; }
        public int errorCode { get; set; }

        public new string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.AppendFormat("ERROR {0}: {1}", errorCode, error);
            result.AppendLine();

            return result.ToString();
        }

    }
}
