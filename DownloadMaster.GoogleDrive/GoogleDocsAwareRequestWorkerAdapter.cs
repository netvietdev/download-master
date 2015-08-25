using Google.Apis.Services;
using Rabbit.Net.WebCrawling;

namespace DownloadMaster.GoogleDrive
{
    public class GoogleDocsAwareRequestWorkerAdapter : IWebRequestWorker
    {
        private readonly IWebRequestWorker _worker;
        private readonly IWebRequestWorker _googleWorker;

        public GoogleDocsAwareRequestWorkerAdapter(string serviceAccountEmail, string keyFilePath, string applicationName)
        {
            _worker = new WebRequestWorker();
            _googleWorker = new GoogleDriveRequestWorker(serviceAccountEmail, keyFilePath, applicationName);
        }

        public GoogleDocsAwareRequestWorkerAdapter(IClientService driveService)
        {
            _worker = new WebRequestWorker();
            _googleWorker = new GoogleDriveRequestWorker(driveService);
        }

        public ResponseData DownloadResponse(CrawlingOption option)
        {
            if (option.Uri.Contains("://docs.google.com"))
            {
                return _googleWorker.DownloadResponse(option);
            }

            return _worker.DownloadResponse(option);
        }
    }
}