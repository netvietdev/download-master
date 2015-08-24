using System.IO;

namespace DownloadMaster.Common
{
    public class FileStatusChecker
    {
        private readonly string _folder;

        public FileStatusChecker(string folder)
        {
            _folder = folder;
        }

        public bool IsExists(string fileName)
        {
            return File.Exists(Path.Combine(_folder, fileName));
        }
    }
}