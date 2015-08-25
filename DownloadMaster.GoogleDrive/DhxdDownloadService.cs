using DownloadMaster.Common;
using Google.Apis.Services;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace DownloadMaster.GoogleDrive
{
    public class DhxdDownloadService : PageFilesDownloadService
    {
        public DhxdDownloadService(string serviceAccountEmail, string keyFilePath, string applicationName)
            : base(new GoogleDocsAwareRequestWorkerAdapter(serviceAccountEmail, keyFilePath, applicationName))
        {
        }

        public DhxdDownloadService(IClientService driveService)
            : base(new GoogleDocsAwareRequestWorkerAdapter(driveService))
        {
        }

        protected override string GetFileName(string targetUri, string filePattern)
        {
            // Get file name by using response header
            return string.Empty;
        }

        protected override IEnumerable<string> GetFileLinks(string pageContent, string filePattern)
        {
            const string GoogleFileId = "id=(.*?)$";

            var fileLinks = RegexHelper.GetLinks(pageContent).Where(link => Regex.IsMatch(link, filePattern));

            foreach (var link in fileLinks)
            {
                if (link.Contains("google.com/"))
                {
                    var match = Regex.Matches(link, GoogleFileId, RegexOptions.Singleline).Cast<Match>().First();

                }
                else
                {

                }
                yield return link;
            }
        }
    }
}