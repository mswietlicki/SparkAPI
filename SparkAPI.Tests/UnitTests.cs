using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkAPI.Tests
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void GetVariableIntTest()
        {
            var spark = new Spark("Ozon");
            var timeleft = spark.GetVariableInt("timeleft");

            Debug.WriteLine(timeleft);
            
        }

        [TestMethod]
        public void CallFunctionTest()
        {
            var spark = new Spark("Ozon");
            var timeleft = spark.CallFunction("start-ozon", 1.ToString());

            Debug.WriteLine(timeleft);

        }
    }
}
