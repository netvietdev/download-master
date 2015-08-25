using Rabbit.Net.WebCrawling;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace DownloadMaster.Common
{
    public abstract class PageFilesDownloadService : IDownloadService
    {
        private readonly IWebRequestWorker _worker;

        protected PageFilesDownloadService()
            : this(new WebRequestWorker())
        {
        }

        protected PageFilesDownloadService(IWebRequestWorker worker)
        {
            _worker = worker;
        }

        public void Download(DownloadServiceOption options)
        {
            var fileLinks = new Dictionary<string, IEnumerable<string>>();

            foreach (var urlAndPattern in options.UrlsAndPatterns)
            {
                var uri = urlAndPattern.Key;
                var filePattern = urlAndPattern.Value;

                Console.WriteLine("Start getting " + uri);

                var articleResult = _worker.DownloadResponse(new CrawlingOption(uri));

                if (articleResult.StatusCode == HttpStatusCode.OK)
                {
                    fileLinks.Add(uri, GetFileLinks(articleResult.ReadAsText(), filePattern));
                }
                else
                {
                    Console.WriteLine(articleResult.StatusDescription);
                }
            }

            var count = 0;
            var total = fileLinks.Values.SelectMany(x => x).Count();

            foreach (var kvp in fileLinks)
            {
                foreach (var file in kvp.Value)
                {
                    count += 1;
                    Console.WriteLine("Process " + count + "/" + total);

                    ProcessDownload(options, file, kvp);
                }
            }
        }

        private void ProcessDownload(DownloadServiceOption options, string file, KeyValuePair<string, IEnumerable<string>> kvp)
        {
            try
            {
                var pattern = options.UrlsAndPatterns.First(x => x.Key == kvp.Key).Value;
                var fileName = GetFileName(file, pattern);

                if (!string.IsNullOrWhiteSpace(fileName) &&
                    File.Exists(Path.Combine(options.TargetFolder, fileName)))
                {
                    Console.WriteLine(" -> Exists");
                    return;
                }

                var fileResult = _worker.DownloadResponse(new CrawlingOption(file));
                if (string.IsNullOrWhiteSpace(fileName) && !string.IsNullOrWhiteSpace(fileResult.ResponseUri))
                {
                    fileName = Path.GetFileName(fileResult.ResponseUri);
                }

                var filePath = Path.Combine(options.TargetFolder, fileName);
                if (File.Exists(filePath))
                {
                    Console.WriteLine(" -> Exists");
                    return;
                }

                if (!Directory.Exists(options.TargetFolder))
                {
                    Directory.CreateDirectory(options.TargetFolder);
                }

                File.WriteAllBytes(filePath, fileResult.Content);
                Console.WriteLine(fileName + " -> Done");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        protected abstract string GetFileName(string targetUri, string filePattern);

        protected abstract IEnumerable<string> GetFileLinks(string pageContent, string filePattern);
    }
}