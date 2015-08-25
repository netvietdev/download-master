using Google.Apis.Services;
using Rabbit.Global.Google;
using Rabbit.Net.WebCrawling;
using System;
using System.Net;

namespace DownloadMaster.GoogleDrive
{
    public class GoogleDriveRequestWorker : IWebRequestWorker
    {
        private readonly IClientService _driveService;

        public GoogleDriveRequestWorker(string serviceAccountEmail, string keyFilePath, string applicationName)
            : this(DriveServiceHelper.AuthenticateServiceAccount(serviceAccountEmail, keyFilePath, applicationName))
        {
        }

        public GoogleDriveRequestWorker(IClientService driveService)
        {
            _driveService = driveService;
        }

        public ResponseData DownloadResponse(CrawlingOption option)
        {
            var response = _driveService.HttpClient.GetStreamAsync(option.Uri);

            try
            {
                return new ResponseData()
                {
                    ResponseStream = response.Result,
                    StatusCode = HttpStatusCode.OK,
                    ResponseUri = option.Uri
                };
            }
            catch (Exception ex)
            {
                return new ResponseData()
                {
                    StatusDescription = ex.ToString()
                };
            }
        }
    }
}
