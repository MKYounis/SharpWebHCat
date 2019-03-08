using System.Text;

namespace SharpHive.Models.WebHCat.WebHCatAPIReference.Response.Errors
{
    public class ErrorCreateTable : ErrorDetails
    {
        public string sqlState { get; set; }

        public new string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.AppendFormat("{0}SQL State: {1}", base.ToString(), sqlState);
            result.AppendLine();

            return result.ToString();
        }
    }
}
