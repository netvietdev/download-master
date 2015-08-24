﻿using DownloadMaster.Common;
using DownloadMaster.CustomLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace TsqgDownloadService
{
    public class DhtnmthcmCategoryDownloadService : CategoryFilesDownloadService
    {
        protected override void ProcessAllPages(IEnumerable<string> pages, string filePattern, string targetFolder)
        {
            var pageDownloadService = new DhtnmthcmPageDownloadService();

            foreach (var pageLink in pages)
            {
                var options = new DownloadServiceOption()
                {
                    TargetFolder = targetFolder,
                    UrlsAndPatterns = new Dictionary<string, string>()
                    {
                        {pageLink, filePattern}
                    }
                };

                pageDownloadService.Download(options);
            }
        }

        protected override IEnumerable<string> GetArticleLinks(string pageContent, string linkPattern)
        {
            var links = RegexHelper.GetLinks(pageContent).Where(link => Regex.IsMatch(link, linkPattern)).Distinct();
            return links.Select(link => "http://hcmunre.edu.vn" + link.Substring(link.IndexOf("/", 1, StringComparison.InvariantCultureIgnoreCase)));
        }
    }
}