using DownloadMaster.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace TsqgDownloadService
{
    public class DhdhnDownloadService : PageFilesDownloadService
    {
        protected override string GetFileName(string targetUri, string filePattern)
        {
            var match = Regex.Matches(targetUri, filePattern, RegexOptions.Singleline).Cast<Match>().First();
            return Uri.UnescapeDataString(match.Groups[1].Value);
        }

        protected override IEnumerable<string> GetFileLinks(string pageContent, string filePattern)
        {
            return RegexHelper.GetLinks(pageContent).Where(link => Regex.IsMatch(link, filePattern)).Select(x => "http://www.hup.edu.vn" + x).Distinct();
        }
    }
}