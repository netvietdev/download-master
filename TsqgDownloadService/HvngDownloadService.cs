using DownloadMaster.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace TsqgDownloadService
{
    public class HvngDownloadService : PageFilesDownloadService
    {
        protected override string GetFileName(string targetUri, string filePattern)
        {
            var match = Regex.Matches(targetUri, filePattern, RegexOptions.Singleline).Cast<Match>().First();
            var fileName = !string.IsNullOrWhiteSpace(match.Groups[1].Value) ? match.Groups[1].Value : match.Groups[2].Value;
            return Uri.UnescapeDataString(fileName);
        }

        protected override IEnumerable<string> GetFileLinks(string pageContent, string filePattern)
        {
            return RegexHelper.GetLinks(pageContent).Where(link => Regex.IsMatch(link, filePattern)).Select(x => "http://www.dav.edu.vn" + x).Distinct();
        }
    }
}
