using DownloadMaster.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace TsqgDownloadService
{
    public class DhtnmthnDownloadService : PageFilesDownloadService
    {
        protected override string GetFileName(string targetUri, string filePattern)
        {
            var match = Regex.Matches(targetUri, filePattern, RegexOptions.Singleline).Cast<Match>().First();
            return Uri.UnescapeDataString(match.Groups[1].Value + "_" + match.Groups[2].Value);
        }

        protected override IEnumerable<string> GetFileLinks(string pageContent, string filePattern)
        {
            return
                RegexHelper.GetLinks(pageContent)
                    .Where(link => Regex.IsMatch(link, filePattern))
                    .Select(x => string.Concat("http://hunre.edu.vn", x))
                    .Distinct();
        }
    }
}