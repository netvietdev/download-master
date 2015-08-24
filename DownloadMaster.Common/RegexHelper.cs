using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace DownloadMaster.Common
{
    public static class RegexHelper
    {
        private const string AllLinksPattern = "<(a|link).*?href=(\"|')(.+?)(\"|').*?>";

        public static IEnumerable<string> GetLinks(string content)
        {
            var links = Regex.Matches(content, AllLinksPattern, RegexOptions.Multiline);
            return from Match match in links select match.Groups[3].Value;
        }
    }
}