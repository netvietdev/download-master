using DownloadMaster.Common;
using DownloadMaster.GoogleDrive;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace ServicesTest
{
    [TestClass]
    public class DhxdDownloadServiceTest
    {
        [TestMethod]
        public void CanDownloadFiles()
        {
            var service =
                new DhxdDownloadService("798851632091-eqf7bgvqf797q5lnm7g47t0rq08prks9@developer.gserviceaccount.com",
                    @"D:\Wip\Practices\ts2015\Docs\GoogleServiceAccount\tuyensinhquocgia-f36c1d0e7788.p12", "TSQG");

            var options = new DownloadServiceOption()
            {
                TargetFolder = @"E:\Projects\github\ts2015\DHXD-HN",
                UrlsAndPatterns = new Dictionary<string, string>()
                {
                    {"http://tuyensinh.nuce.edu.vn/tin-tuc/thong-bao-diem-chuan-va-danh-sach-trung-tuyen-dai-hoc-he-chinh-quy", @".*\/(.*?.pdf)$|google.com"}
                }
            };

            service.Download(options);
        }
    }
}