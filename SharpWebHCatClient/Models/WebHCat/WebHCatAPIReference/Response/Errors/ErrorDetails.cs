using System;
using System.Collections.Generic;
using System.Text;

namespace SharpHive.Models.WebHCat.WebHCatAPIReference.Response.Errors
{
    public class ErrorDetails : Error
    {
        public string errorDetail { get; set; }

        public new string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.AppendFormat("{0}Details: {1}", base.ToString(), errorDetail);
            result.AppendLine();

            return result.ToString();
        }
    }
}
