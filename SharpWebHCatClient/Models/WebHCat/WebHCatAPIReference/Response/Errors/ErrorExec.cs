using System;
namespace SharpHive.Models.WebHCat.WebHCatAPIReference.Response.Errors
{
    public class ErrorExec
    {
        public string stdout { get; set; }
        public string stderr { get; set; }
        public int exitcode { get; set; }

        public new string ToString()
        {
            return string.Format("stdout: {0}\nstderr: {1}\nexit code: {2}", stdout, stderr, exitcode);
        }
    }
}
