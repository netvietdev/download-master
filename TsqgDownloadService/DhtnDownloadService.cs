using DownloadMaster.Common;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace TsqgDownloadService
{
    public class DhtnDownloadService : PageFilesDownloadService
    {
        protected override string GetFileName(string targetUri, string filePattern)
        {
            // Get file name by using response header
            return string.Empty;
        }

        protected override IEnumerable<string> GetFileLinks(string pageContent, string filePattern)
        {
            return RegexHelper.GetLinks(pageContent).Where(link => Regex.IsMatch(link, filePattern)).Select(x => "http://tuyensinh.tnu.edu.vn" + x).Distinct();
        }
    }
}