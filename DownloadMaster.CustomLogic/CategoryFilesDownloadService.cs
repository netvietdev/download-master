using DownloadMaster.Common;
using Rabbit.Foundation.Data;
using Rabbit.Net.WebCrawling;
using System;
using System.Collections.Generic;
using System.Net;

namespace DownloadMaster.CustomLogic
{
    public abstract class CategoryFilesDownloadService : IDownloadService
    {
        public void Download(DownloadServiceOption options)
        {
            var worker = new WebRequestWorker();

            foreach (var urlAndPattern in options.UrlsAndPatterns)
            {
                var uri = urlAndPattern.Key;
                var configFile = urlAndPattern.Value;

                Console.WriteLine("Start processing " + uri);

                var categoryResult = worker.DownloadResponse(new CrawlingOption(uri));

                if (categoryResult.StatusCode == HttpStatusCode.OK)
                {
                    var configs = SerializationHelper.DeserializeFrom<List<DataItem>>(configFile);
                    var pageLinkPattern = configs.Get(Constants.PagePattern).Value;
                    var fileLinkPattern = configs.Get(Constants.FilePattern).Value;

                    var categoryPages = GetArticleLinks(categoryResult.ReadAsText(), pageLinkPattern);
                    ProcessAllPages(categoryPages, fileLinkPattern, options.TargetFolder);
                }
                else
                {
                    Console.WriteLine(categoryResult.StatusDescription);
                }
            }
        }

        protected abstract void ProcessAllPages(IEnumerable<string> pages, string filePattern, string targetFolder);

        protected abstract IEnumerable<string> GetArticleLinks(string pageContent, string linkPattern);
    }
}