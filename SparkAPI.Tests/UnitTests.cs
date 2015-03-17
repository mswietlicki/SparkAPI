using Microsoft.VisualStudio.TestTools.UnitTesting;
using SparkAPI.Security;
using System;

namespace SparkAPI.Tests
{
    [TestClass]
    public class UnitTests
    {
        public ISparkAccessTokenProvider AccessTokenProvider { get; set; }

        [TestInitialize]
        public void Init()
        {
            AccessTokenProvider = new LocalFileSparkAccessTokenProvider(@"C:\Users\Mateusz\AppData\Local\SparkAPI\AccessToken.dat");
        }

        [TestMethod]
        public void GetVariableIntTest()
        {
            var spark = new Spark("Ozon", AccessTokenProvider);
            var timeleft = spark.GetVariableInt("timeleft");
        }

        [TestMethod]
        public void CallFunctionTest()
        {
            var spark = new Spark("Ozon", AccessTokenProvider);
            spark.CallFunction("start-ozon", 1.ToString());
        }

        [TestMethod]
        public void CreateAccessTokenTest()
        {
            var token = Spark.CreateAccessToken("", "");
            Console.WriteLine(token);
        }
    }
}
