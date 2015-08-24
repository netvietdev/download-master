using System.Collections.Generic;

namespace DownloadMaster.Common
{
    public class DownloadServiceOption
    {
        public DownloadServiceOption()
        {
            UrlsAndPatterns = new Dictionary<string, string>();
        }

        public string ServiceType { get; set; }
        public string TargetFolder { get; set; }
        public IDictionary<string, string> UrlsAndPatterns { get; set; }
    }
}