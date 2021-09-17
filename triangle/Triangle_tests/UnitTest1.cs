using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;
using System.Linq;
using System.IO;
using Triangle;

namespace Triangle_tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            FileStream fileIn = new FileStream("tests.txt", FileMode.Open);
            FileStream fileOut = new FileStream("tests_results.txt", FileMode.Create);
            StreamReader reader = new StreamReader(fileIn);
            StreamWriter writer = new StreamWriter(fileOut);
            var triangle = new Triangle;
        }
    }
}
