using DownloadMaster.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TsqgDownloadService;

namespace ServicesTest
{
    [TestClass]
    public class DhtnDownloadServiceTest
    {
        [TestMethod]
        public void CanDownloadFiles()
        {
            var service = new DhtnDownloadService();
            var options = new DownloadServiceOption()
            {
                TargetFolder = @"D:\\Wip\\Practices\\OpenSource\\ts2015\\DHTN",
                UrlsAndPatterns = new Dictionary<string, string>()
                {
                    {"http://tuyensinh.tnu.edu.vn/article/details/363", @"/article/Download/\d+"}
                }
            };

            service.Download(options);
        }
    }
}
