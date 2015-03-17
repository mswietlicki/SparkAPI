using System.IO;

namespace SparkAPI.Security
{
    public class LocalFileSparkAccessTokenProvider : ISparkAccessTokenProvider
    {
        public string FileName { get; set; }
        public LocalFileSparkAccessTokenProvider(string fileName)
        {
            FileName = fileName;
        }
        public string GetAccessToken()
        {
            return File.ReadAllText(FileName);
        }
    }

    //public class RegistrySparkAccessTokenProvider : ISparkAccessTokenProvider
    //{
    //    public string GetAccessToken()
    //    {
    //        return File.ReadAllText(FileName);
    //    }
    //}
}
