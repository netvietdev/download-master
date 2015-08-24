using Rabbit.Net.WebCrawling;
using Rabbit.SerializationMaster;
using System.IO;
using System.Net;

namespace DownloadMaster.CustomLogic
{
    public static class SerializationHelper
    {
        public static T DeserializeFrom<T>(string filePath) where T : class
        {
            if (filePath.Contains("://"))
            {
                var result = new WebRequestWorker().DownloadResponse(new CrawlingOption(filePath));
                if (result.StatusCode == HttpStatusCode.OK)
                {
                    return result.ReadAsText().Deserialize<T>();
                }
            }
            else
            {
                return File.ReadAllText(filePath).Deserialize<T>();
            }

            return default(T);
        }
    }
}